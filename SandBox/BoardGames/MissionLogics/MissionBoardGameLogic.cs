using System;
using System.Linq;
using Helpers;
using SandBox.BoardGames.AI;
using SandBox.Conversation;
using SandBox.Conversation.MissionLogics;
using SandBox.Objects.Usables;
using SandBox.Source.Missions.AgentBehaviors;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Source.Missions.Handlers;

namespace SandBox.BoardGames.MissionLogics
{
	// Token: 0x02000102 RID: 258
	public class MissionBoardGameLogic : MissionLogic
	{
		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000CD1 RID: 3281 RVA: 0x0005E314 File Offset: 0x0005C514
		// (remove) Token: 0x06000CD2 RID: 3282 RVA: 0x0005E34C File Offset: 0x0005C54C
		public event Action GameStarted;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000CD3 RID: 3283 RVA: 0x0005E384 File Offset: 0x0005C584
		// (remove) Token: 0x06000CD4 RID: 3284 RVA: 0x0005E3BC File Offset: 0x0005C5BC
		public event Action GameEnded;

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x0005E3F1 File Offset: 0x0005C5F1
		// (set) Token: 0x06000CD6 RID: 3286 RVA: 0x0005E3F9 File Offset: 0x0005C5F9
		public BoardGameBase Board { get; private set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x0005E402 File Offset: 0x0005C602
		// (set) Token: 0x06000CD8 RID: 3288 RVA: 0x0005E40A File Offset: 0x0005C60A
		public BoardGameAIBase AIOpponent { get; private set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x0005E413 File Offset: 0x0005C613
		public bool IsOpposingAgentMovingToPlayingChair
		{
			get
			{
				return BoardGameAgentBehavior.IsAgentMovingToChair(this.OpposingAgent);
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x0005E420 File Offset: 0x0005C620
		// (set) Token: 0x06000CDB RID: 3291 RVA: 0x0005E428 File Offset: 0x0005C628
		public bool IsGameInProgress { get; private set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x0005E431 File Offset: 0x0005C631
		public BoardGameHelper.BoardGameState BoardGameFinalState
		{
			get
			{
				return this._boardGameState;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x0005E439 File Offset: 0x0005C639
		// (set) Token: 0x06000CDE RID: 3294 RVA: 0x0005E441 File Offset: 0x0005C641
		public CultureObject.BoardGameType CurrentBoardGame { get; private set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x0005E44A File Offset: 0x0005C64A
		// (set) Token: 0x06000CE0 RID: 3296 RVA: 0x0005E452 File Offset: 0x0005C652
		public BoardGameHelper.AIDifficulty Difficulty { get; private set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x0005E45B File Offset: 0x0005C65B
		// (set) Token: 0x06000CE2 RID: 3298 RVA: 0x0005E463 File Offset: 0x0005C663
		public int BetAmount { get; private set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x0005E46C File Offset: 0x0005C66C
		// (set) Token: 0x06000CE4 RID: 3300 RVA: 0x0005E474 File Offset: 0x0005C674
		public Agent OpposingAgent { get; private set; }

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0005E480 File Offset: 0x0005C680
		public override void AfterStart()
		{
			base.AfterStart();
			this._opposingChair = base.Mission.Scene.FindEntityWithTag("gambler_npc").CollectScriptComponentsIncludingChildrenRecursive<Chair>().FirstOrDefault<Chair>();
			this._playerChair = base.Mission.Scene.FindEntityWithTag("gambler_player").CollectScriptComponentsIncludingChildrenRecursive<Chair>().FirstOrDefault<Chair>();
			foreach (StandingPoint standingPoint in this._opposingChair.StandingPoints)
			{
				standingPoint.IsDisabledForPlayers = true;
			}
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0005E528 File Offset: 0x0005C728
		public void SetStartingPlayer(bool playerOneStarts)
		{
			this._startingPlayer = (playerOneStarts ? PlayerTurn.PlayerOne : PlayerTurn.PlayerTwo);
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x0005E537 File Offset: 0x0005C737
		public void StartBoardGame()
		{
			this._startingBoardGame = true;
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x0005E540 File Offset: 0x0005C740
		private void BoardGameInit(CultureObject.BoardGameType game)
		{
			if (this.Board == null)
			{
				switch (game)
				{
				case CultureObject.BoardGameType.Seega:
					this.Board = new BoardGameSeega(this, this._startingPlayer);
					this.AIOpponent = new BoardGameAISeega(this.Difficulty, this);
					break;
				case CultureObject.BoardGameType.Puluc:
					this.Board = new BoardGamePuluc(this, this._startingPlayer);
					this.AIOpponent = new BoardGameAIPuluc(this.Difficulty, this);
					break;
				case CultureObject.BoardGameType.Konane:
					this.Board = new BoardGameKonane(this, this._startingPlayer);
					this.AIOpponent = new BoardGameAIKonane(this.Difficulty, this);
					break;
				case CultureObject.BoardGameType.MuTorere:
					this.Board = new BoardGameMuTorere(this, this._startingPlayer);
					this.AIOpponent = new BoardGameAIMuTorere(this.Difficulty, this);
					break;
				case CultureObject.BoardGameType.Tablut:
					this.Board = new BoardGameTablut(this, this._startingPlayer);
					this.AIOpponent = new BoardGameAITablut(this.Difficulty, this);
					break;
				case CultureObject.BoardGameType.BaghChal:
					this.Board = new BoardGameBaghChal(this, this._startingPlayer);
					this.AIOpponent = new BoardGameAIBaghChal(this.Difficulty, this);
					break;
				default:
					Debug.FailedAssert("[DEBUG]No board with this name was found.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\SandBox\\BoardGames\\MissionLogics\\MissionBoardGameLogic.cs", "BoardGameInit", 119);
					break;
				}
				this.Board.Initialize();
				if (this.AIOpponent != null)
				{
					this.AIOpponent.Initialize();
				}
			}
			else
			{
				this.Board.SetStartingPlayer(this._startingPlayer);
				this.Board.InitializeUnits();
				this.Board.InitializeCapturedUnitsZones();
				this.Board.Reset();
				if (this.AIOpponent != null)
				{
					this.AIOpponent.SetDifficulty(this.Difficulty);
					this.AIOpponent.Initialize();
				}
			}
			if (this.Handler != null)
			{
				this.Handler.Install();
			}
			this._boardGameState = BoardGameHelper.BoardGameState.None;
			this.IsGameInProgress = true;
			this._isTavernGame = CampaignMission.Current.Location == Settlement.CurrentSettlement.LocationComplex.GetLocationWithId("tavern");
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x0005E73C File Offset: 0x0005C93C
		public override void OnMissionTick(float dt)
		{
			if (base.Mission.IsInPhotoMode)
			{
				return;
			}
			if (this._startingBoardGame)
			{
				this._startingBoardGame = false;
				this.BoardGameInit(this.CurrentBoardGame);
				Action gameStarted = this.GameStarted;
				if (gameStarted == null)
				{
					return;
				}
				gameStarted();
				return;
			}
			else
			{
				if (this.IsGameInProgress)
				{
					this.Board.Tick(dt);
					return;
				}
				if (this.OpposingAgent != null && this.OpposingAgent.IsHero && Hero.OneToOneConversationHero == null && this.CheckIfBothSidesAreSitting())
				{
					this.StartBoardGame();
				}
				return;
			}
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x0005E7C4 File Offset: 0x0005C9C4
		public void DetectOpposingAgent()
		{
			foreach (Agent agent in Mission.Current.Agents)
			{
				if (agent == ConversationMission.OneToOneConversationAgent)
				{
					this.OpposingAgent = agent;
					if (agent.IsHero)
					{
						BoardGameAgentBehavior.AddTargetChair(this.OpposingAgent, this._opposingChair);
					}
					AgentNavigator agentNavigator = this.OpposingAgent.GetComponent<CampaignAgentComponent>().AgentNavigator;
					this._specialTagCacheOfOpposingHero = agentNavigator.SpecialTargetTag;
					agentNavigator.SpecialTargetTag = "gambler_npc";
					break;
				}
			}
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x0005E868 File Offset: 0x0005CA68
		public bool CheckIfBothSidesAreSitting()
		{
			return Agent.Main != null && this.OpposingAgent != null && this._playerChair.IsAgentFullySitting(Agent.Main) && this._opposingChair.IsAgentFullySitting(this.OpposingAgent);
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x0005E8A0 File Offset: 0x0005CAA0
		public void PlayerOneWon(string message = "str_boardgame_victory_message")
		{
			Agent opposingAgent = this.OpposingAgent;
			this.SetGameOver(GameOverEnum.PlayerOneWon);
			this.ShowInquiry(message, opposingAgent);
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x0005E8C4 File Offset: 0x0005CAC4
		public void PlayerTwoWon(string message = "str_boardgame_defeat_message")
		{
			Agent opposingAgent = this.OpposingAgent;
			this.SetGameOver(GameOverEnum.PlayerTwoWon);
			this.ShowInquiry(message, opposingAgent);
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x0005E8E8 File Offset: 0x0005CAE8
		public void GameWasDraw(string message = "str_boardgame_draw_message")
		{
			Agent opposingAgent = this.OpposingAgent;
			this.SetGameOver(GameOverEnum.Draw);
			this.ShowInquiry(message, opposingAgent);
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x0005E90C File Offset: 0x0005CB0C
		private void ShowInquiry(string message, Agent conversationAgent)
		{
			InformationManager.ShowInquiry(new InquiryData(GameTexts.FindText("str_boardgame", null).ToString(), GameTexts.FindText(message, null).ToString(), true, false, GameTexts.FindText("str_ok", null).ToString(), "", delegate
			{
				this.StartConversationWithOpponentAfterGameEnd(conversationAgent);
			}, null, "", 0f, null, null, null), false, false);
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x0005E986 File Offset: 0x0005CB86
		private void StartConversationWithOpponentAfterGameEnd(Agent conversationAgent)
		{
			MissionConversationLogic.Current.StartConversation(conversationAgent, false, false);
			this._boardGameState = BoardGameHelper.BoardGameState.None;
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x0005E99C File Offset: 0x0005CB9C
		public void SetGameOver(GameOverEnum gameOverInfo)
		{
			base.Mission.MainAgent.ClearTargetFrame();
			if (this.Handler != null && gameOverInfo != GameOverEnum.PlayerCanceledTheGame)
			{
				this.Handler.Uninstall();
			}
			Hero hero = (this.OpposingAgent.IsHero ? ((CharacterObject)this.OpposingAgent.Character).HeroObject : null);
			switch (gameOverInfo)
			{
			case GameOverEnum.PlayerOneWon:
				this._boardGameState = BoardGameHelper.BoardGameState.Win;
				break;
			case GameOverEnum.PlayerTwoWon:
				this._boardGameState = BoardGameHelper.BoardGameState.Loss;
				break;
			case GameOverEnum.Draw:
				this._boardGameState = BoardGameHelper.BoardGameState.Draw;
				break;
			case GameOverEnum.PlayerCanceledTheGame:
				this._boardGameState = BoardGameHelper.BoardGameState.None;
				break;
			}
			if (gameOverInfo != GameOverEnum.PlayerCanceledTheGame)
			{
				CampaignEventDispatcher.Instance.OnPlayerBoardGameOver(hero, this._boardGameState);
			}
			Action gameEnded = this.GameEnded;
			if (gameEnded != null)
			{
				gameEnded();
			}
			BoardGameAgentBehavior.RemoveBoardGameBehaviorOfAgent(this.OpposingAgent);
			this.OpposingAgent.GetComponent<CampaignAgentComponent>().AgentNavigator.SpecialTargetTag = this._specialTagCacheOfOpposingHero;
			this.OpposingAgent = null;
			this.IsGameInProgress = false;
			BoardGameAIBase aiopponent = this.AIOpponent;
			if (aiopponent == null)
			{
				return;
			}
			aiopponent.OnSetGameOver();
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x0005EAA0 File Offset: 0x0005CCA0
		public void ForfeitGame()
		{
			this.Board.SetGameOverInfo(GameOverEnum.PlayerTwoWon);
			Agent opposingAgent = this.OpposingAgent;
			this.SetGameOver(this.Board.GameOverInfo);
			this.StartConversationWithOpponentAfterGameEnd(opposingAgent);
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x0005EAD8 File Offset: 0x0005CCD8
		public void AIForfeitGame()
		{
			this.Board.SetGameOverInfo(GameOverEnum.PlayerOneWon);
			this.SetGameOver(this.Board.GameOverInfo);
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x0005EAF7 File Offset: 0x0005CCF7
		public void RollDice()
		{
			this.Board.RollDice();
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x0005EB04 File Offset: 0x0005CD04
		public bool RequiresDiceRolling()
		{
			switch (this.CurrentBoardGame)
			{
			case CultureObject.BoardGameType.Seega:
				return false;
			case CultureObject.BoardGameType.Puluc:
				return true;
			case CultureObject.BoardGameType.Konane:
				return false;
			case CultureObject.BoardGameType.MuTorere:
				return false;
			case CultureObject.BoardGameType.Tablut:
				return false;
			case CultureObject.BoardGameType.BaghChal:
				return false;
			default:
				return false;
			}
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x0005EB45 File Offset: 0x0005CD45
		public void SetBetAmount(int bet)
		{
			this.BetAmount = bet;
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x0005EB4E File Offset: 0x0005CD4E
		public void SetCurrentDifficulty(BoardGameHelper.AIDifficulty difficulty)
		{
			this.Difficulty = difficulty;
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x0005EB57 File Offset: 0x0005CD57
		public void SetBoardGame(CultureObject.BoardGameType game)
		{
			this.CurrentBoardGame = game;
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0005EB60 File Offset: 0x0005CD60
		protected override void OnEndMission()
		{
			base.OnEndMission();
			if (this.IsGameInProgress)
			{
				this.SetGameOver(GameOverEnum.PlayerCanceledTheGame);
			}
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x0005EB77 File Offset: 0x0005CD77
		public override InquiryData OnEndMissionRequest(out bool canLeave)
		{
			canLeave = true;
			return null;
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x0005EB80 File Offset: 0x0005CD80
		public static bool IsBoardGameAvailable()
		{
			Mission mission = Mission.Current;
			MissionBoardGameLogic missionBoardGameLogic = ((mission != null) ? mission.GetMissionBehavior<MissionBoardGameLogic>() : null);
			Mission mission2 = Mission.Current;
			return ((mission2 != null) ? mission2.Scene : null) != null && missionBoardGameLogic != null && Mission.Current.Scene.FindEntityWithTag("boardgame") != null && missionBoardGameLogic.OpposingAgent == null;
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x0005EBE4 File Offset: 0x0005CDE4
		public static bool IsThereActiveBoardGameWithHero(Hero hero)
		{
			Mission mission = Mission.Current;
			MissionBoardGameLogic missionBoardGameLogic = ((mission != null) ? mission.GetMissionBehavior<MissionBoardGameLogic>() : null);
			Mission mission2 = Mission.Current;
			if (((mission2 != null) ? mission2.Scene : null) != null && Mission.Current.Scene.FindEntityWithTag("boardgame") != null && missionBoardGameLogic != null)
			{
				Agent opposingAgent = missionBoardGameLogic.OpposingAgent;
				return ((opposingAgent != null) ? opposingAgent.Character : null) == hero.CharacterObject;
			}
			return false;
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x0005EC57 File Offset: 0x0005CE57
		public override void OnAgentInteraction(Agent userAgent, Agent agent, sbyte agentBoneIndex)
		{
			if (Campaign.Current.GameMode == CampaignGameMode.Campaign && !Campaign.Current.ConversationManager.IsConversationInProgress && this.IsThereAgentAction(userAgent, agent))
			{
				Mission.Current.GetMissionBehavior<MissionConversationLogic>().StartConversation(agent, false, false);
			}
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x0005EC93 File Offset: 0x0005CE93
		public override bool IsThereAgentAction(Agent userAgent, Agent otherAgent)
		{
			return userAgent.IsMainAgent && this._playerChair.IsAgentFullySitting(Agent.Main) && this._opposingChair.IsAgentFullySitting(otherAgent);
		}

		// Token: 0x04000595 RID: 1429
		private const string BoardGameEntityTag = "boardgame";

		// Token: 0x04000596 RID: 1430
		private const string SpecialTargetGamblerNpcTag = "gambler_npc";

		// Token: 0x04000599 RID: 1433
		public IBoardGameHandler Handler;

		// Token: 0x0400059A RID: 1434
		private PlayerTurn _startingPlayer = PlayerTurn.PlayerTwo;

		// Token: 0x0400059B RID: 1435
		private Chair _playerChair;

		// Token: 0x0400059C RID: 1436
		private Chair _opposingChair;

		// Token: 0x0400059D RID: 1437
		private string _specialTagCacheOfOpposingHero;

		// Token: 0x0400059E RID: 1438
		private bool _isTavernGame;

		// Token: 0x0400059F RID: 1439
		private bool _startingBoardGame;

		// Token: 0x040005A0 RID: 1440
		private BoardGameHelper.BoardGameState _boardGameState;
	}
}
