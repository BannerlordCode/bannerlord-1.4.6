using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.ObjectSystem;
using TaleWorlds.PlatformService;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001CA RID: 458
	public abstract class MBGameManager : GameManagerBase
	{
		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001B6E RID: 7022 RVA: 0x0005FA3D File Offset: 0x0005DC3D
		// (set) Token: 0x06001B6F RID: 7023 RVA: 0x0005FA45 File Offset: 0x0005DC45
		public bool IsEnding { get; private set; }

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001B70 RID: 7024 RVA: 0x0005FA4E File Offset: 0x0005DC4E
		public new static MBGameManager Current
		{
			get
			{
				return (MBGameManager)GameManagerBase.Current;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001B71 RID: 7025 RVA: 0x0005FA5A File Offset: 0x0005DC5A
		// (set) Token: 0x06001B72 RID: 7026 RVA: 0x0005FA62 File Offset: 0x0005DC62
		public bool IsLoaded { get; protected set; }

		// Token: 0x06001B73 RID: 7027 RVA: 0x0005FA6B File Offset: 0x0005DC6B
		protected MBGameManager()
		{
			this.IsEnding = false;
			NativeConfig.OnConfigChanged();
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x0005FA8A File Offset: 0x0005DC8A
		protected static void StartNewGame()
		{
			MBAPI.IMBGame.StartNew();
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x0005FA96 File Offset: 0x0005DC96
		protected static void LoadModuleData(bool isLoadGame)
		{
			MBAPI.IMBGame.LoadModuleData(isLoadGame);
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x0005FAA4 File Offset: 0x0005DCA4
		public static void StartNewGame(MBGameManager gameLoader)
		{
			Module.CurrentModule.OnBeforeGameStart(gameLoader);
			GameLoadingState gameLoadingState = GameStateManager.Current.CreateState<GameLoadingState>();
			gameLoadingState.SetLoadingParameters(gameLoader);
			GameStateManager.Current.CleanAndPushState(gameLoadingState, 0);
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x0005FADC File Offset: 0x0005DCDC
		public override void BeginGameStart(Game game)
		{
			foreach (MBSubModuleBase mbsubModuleBase in Module.CurrentModule.CollectSubModules())
			{
				mbsubModuleBase.BeginGameStart(game);
			}
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x0005FB34 File Offset: 0x0005DD34
		public override void OnNewCampaignStart(Game game, object starterObject)
		{
			foreach (MBSubModuleBase mbsubModuleBase in Module.CurrentModule.CollectSubModules())
			{
				mbsubModuleBase.OnCampaignStart(game, starterObject);
			}
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x0005FB8C File Offset: 0x0005DD8C
		public override void InitializeSubModuleGameObjects(Game game)
		{
			foreach (MBSubModuleBase mbsubModuleBase in Module.CurrentModule.CollectSubModules())
			{
				mbsubModuleBase.InitializeSubModuleGameObjects(game);
			}
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x0005FBE4 File Offset: 0x0005DDE4
		public override void RegisterSubModuleObjects(bool isSavedCampaign)
		{
			foreach (MBSubModuleBase mbsubModuleBase in Module.CurrentModule.CollectSubModules())
			{
				mbsubModuleBase.RegisterSubModuleObjects(isSavedCampaign);
			}
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x0005FC3C File Offset: 0x0005DE3C
		public override void RegisterSubModuleTypes()
		{
			foreach (MBSubModuleBase mbsubModuleBase in Module.CurrentModule.CollectSubModules())
			{
				mbsubModuleBase.RegisterSubModuleTypes();
			}
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x0005FC90 File Offset: 0x0005DE90
		public override void AfterRegisterSubModuleObjects(bool isSavedCampaign)
		{
			foreach (MBSubModuleBase mbsubModuleBase in Module.CurrentModule.CollectSubModules())
			{
				mbsubModuleBase.AfterRegisterSubModuleObjects(isSavedCampaign);
			}
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x0005FCE8 File Offset: 0x0005DEE8
		public override void InitializeGameStarter(Game game, IGameStarter starterObject)
		{
			foreach (MBSubModuleBase mbsubModuleBase in Module.CurrentModule.CollectSubModules())
			{
				mbsubModuleBase.InitializeGameStarter(game, starterObject);
			}
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x0005FD40 File Offset: 0x0005DF40
		public override void OnGameInitializationFinished(Game game)
		{
			foreach (MBSubModuleBase mbsubModuleBase in Module.CurrentModule.CollectSubModules())
			{
				mbsubModuleBase.OnGameInitializationFinished(game);
			}
			foreach (SkeletonScale skeletonScale in Game.Current.ObjectManager.GetObjectTypeList<SkeletonScale>())
			{
				sbyte[] array = new sbyte[skeletonScale.BoneNames.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = Skeleton.GetBoneIndexFromName(skeletonScale.SkeletonModel, skeletonScale.BoneNames[i]);
				}
				skeletonScale.SetBoneIndices(array);
			}
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x0005FE20 File Offset: 0x0005E020
		public override void OnAfterGameInitializationFinished(Game game, object initializerObject)
		{
			foreach (MBSubModuleBase mbsubModuleBase in Module.CurrentModule.CollectSubModules())
			{
				mbsubModuleBase.OnAfterGameInitializationFinished(game, initializerObject);
			}
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x0005FE78 File Offset: 0x0005E078
		public override void OnGameLoaded(Game game, object initializerObject)
		{
			foreach (MBSubModuleBase mbsubModuleBase in Module.CurrentModule.CollectSubModules())
			{
				mbsubModuleBase.OnGameLoaded(game, initializerObject);
			}
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x0005FED0 File Offset: 0x0005E0D0
		public override void OnAfterGameLoaded(Game game)
		{
			foreach (MBSubModuleBase mbsubModuleBase in Module.CurrentModule.CollectSubModules())
			{
				mbsubModuleBase.OnAfterGameLoaded(game);
			}
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x0005FF28 File Offset: 0x0005E128
		public override void OnNewGameCreated(Game game, object initializerObject)
		{
			foreach (MBSubModuleBase mbsubModuleBase in Module.CurrentModule.CollectSubModules())
			{
				mbsubModuleBase.OnNewGameCreated(game, initializerObject);
			}
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x0005FF80 File Offset: 0x0005E180
		public override void OnGameStart(Game game, IGameStarter gameStarter)
		{
			Game.Current.MonsterMissionDataCreator = new MonsterMissionDataCreator();
			foreach (MBSubModuleBase mbsubModuleBase in Module.CurrentModule.CollectSubModules())
			{
				mbsubModuleBase.OnGameStart(game, gameStarter);
			}
			Game.Current.AddGameModelsManager<MissionGameModels>(gameStarter.Models);
			Monster.GetBoneIndexWithId = new Func<string, string, sbyte>(MBActionSet.GetBoneIndexWithId);
			Monster.GetBoneHasParentBone = new Func<string, sbyte, bool>(MBActionSet.GetBoneHasParentBone);
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x00060018 File Offset: 0x0005E218
		public override void OnGameEnd(Game game)
		{
			foreach (MBSubModuleBase mbsubModuleBase in Module.CurrentModule.CollectSubModules())
			{
				mbsubModuleBase.OnGameEnd(game);
			}
			Module.CurrentModule.OnGameEnd();
			MissionGameModels.Clear();
			base.OnGameEnd(game);
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x00060084 File Offset: 0x0005E284
		public static async void EndGame()
		{
			for (;;)
			{
				MBGameManager mbgameManager = MBGameManager.Current;
				if (mbgameManager == null || mbgameManager.IsLoaded)
				{
					break;
				}
				await Task.Delay(100);
			}
			MBGameManager mbgameManager2 = MBGameManager.Current;
			if (mbgameManager2 == null || mbgameManager2.CheckAndSetEnding())
			{
				if (Game.Current.GameStateManager != null)
				{
					while (Mission.Current != null && !(Game.Current.GameStateManager.ActiveState is MissionState))
					{
						Game.Current.GameStateManager.PopState(0);
					}
					if (Game.Current.GameStateManager.ActiveState is MissionState)
					{
						((MissionState)Game.Current.GameStateManager.ActiveState).CurrentMission.EndMission();
						while (Mission.Current != null)
						{
							await Task.Delay(1);
						}
					}
					else
					{
						Game.Current.GameStateManager.CleanStates(0);
					}
				}
			}
		}

		// Token: 0x06001B86 RID: 7046 RVA: 0x000600B5 File Offset: 0x0005E2B5
		public override void OnLoadFinished()
		{
			this.IsLoaded = true;
		}

		// Token: 0x06001B87 RID: 7047 RVA: 0x000600C0 File Offset: 0x0005E2C0
		public bool CheckAndSetEnding()
		{
			object lockObject = this._lockObject;
			bool flag2;
			lock (lockObject)
			{
				if (this.IsEnding)
				{
					flag2 = false;
				}
				else
				{
					this.IsEnding = true;
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x06001B88 RID: 7048 RVA: 0x00060110 File Offset: 0x0005E310
		public virtual void OnSessionInvitationAccepted(SessionInvitationType targetGameType)
		{
			if (targetGameType != SessionInvitationType.None)
			{
				MBGameManager.EndGame();
			}
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x0006011A File Offset: 0x0005E31A
		public virtual void OnPlatformRequestedMultiplayer()
		{
			MBGameManager.EndGame();
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x00060121 File Offset: 0x0005E321
		protected List<MbObjectXmlInformation> GetXmlInformationFromModule()
		{
			return XmlResource.XmlInformationList;
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001B8B RID: 7051 RVA: 0x00060128 File Offset: 0x0005E328
		public override float ApplicationTime
		{
			get
			{
				return MBCommon.GetApplicationTime();
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001B8C RID: 7052 RVA: 0x0006012F File Offset: 0x0005E32F
		public override bool CheatMode
		{
			get
			{
				return NativeConfig.CheatMode;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001B8D RID: 7053 RVA: 0x00060136 File Offset: 0x0005E336
		public override bool IsDevelopmentMode
		{
			get
			{
				return NativeConfig.IsDevelopmentMode;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001B8E RID: 7054 RVA: 0x0006013D File Offset: 0x0005E33D
		public override bool IsEditModeOn
		{
			get
			{
				return MBEditor.IsEditModeOn;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001B8F RID: 7055 RVA: 0x00060144 File Offset: 0x0005E344
		public override UnitSpawnPrioritizations UnitSpawnPrioritization
		{
			get
			{
				return (UnitSpawnPrioritizations)BannerlordConfig.UnitSpawnPrioritization;
			}
		}

		// Token: 0x04000906 RID: 2310
		private readonly object _lockObject = new object();
	}
}
