using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.TournamentGames;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.Issues
{
	// Token: 0x0200035C RID: 860
	public class BettingFraudIssueBehavior : CampaignBehaviorBase
	{
		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06003334 RID: 13108 RVA: 0x000D3324 File Offset: 0x000D1524
		private static BettingFraudIssueBehavior.BettingFraudQuest Instance
		{
			get
			{
				BettingFraudIssueBehavior campaignBehavior = Campaign.Current.GetCampaignBehavior<BettingFraudIssueBehavior>();
				if (campaignBehavior._cachedQuest != null && campaignBehavior._cachedQuest.IsOngoing)
				{
					return campaignBehavior._cachedQuest;
				}
				using (List<QuestBase>.Enumerator enumerator = Campaign.Current.QuestManager.Quests.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						BettingFraudIssueBehavior.BettingFraudQuest bettingFraudQuest;
						if ((bettingFraudQuest = enumerator.Current as BettingFraudIssueBehavior.BettingFraudQuest) != null)
						{
							campaignBehavior._cachedQuest = bettingFraudQuest;
							return campaignBehavior._cachedQuest;
						}
					}
				}
				return null;
			}
		}

		// Token: 0x06003335 RID: 13109 RVA: 0x000D33BC File Offset: 0x000D15BC
		public override void RegisterEvents()
		{
			CampaignEvents.OnCheckForIssueEvent.AddNonSerializedListener(this, new Action<Hero>(this.CheckForIssue));
			CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnSessionLaunched));
		}

		// Token: 0x06003336 RID: 13110 RVA: 0x000D33EC File Offset: 0x000D15EC
		private void OnSessionLaunched(CampaignGameStarter gameStarter)
		{
			gameStarter.AddGameMenu("menu_town_tournament_join_betting_fraud", "{=5Adr6toM}{MENU_TEXT}", new OnInitDelegate(this.game_menu_tournament_join_on_init), GameMenu.MenuOverlayType.SettlementWithBoth, GameMenu.MenuFlags.None, null);
			gameStarter.AddGameMenuOption("menu_town_tournament_join_betting_fraud", "mno_tournament_event_1", "{=es0Y3Bxc}Join", delegate(MenuCallbackArgs args)
			{
				args.optionLeaveType = GameMenuOption.LeaveType.Mission;
				args.OptionQuestData = GameMenuOption.IssueQuestFlags.ActiveIssue;
				return true;
			}, new GameMenuOption.OnConsequenceDelegate(this.game_menu_tournament_join_current_game_on_consequence), false, -1, false, null);
			gameStarter.AddGameMenuOption("menu_town_tournament_join_betting_fraud", "mno_tournament_leave", "{=3sRdGQou}Leave", delegate(MenuCallbackArgs args)
			{
				args.optionLeaveType = GameMenuOption.LeaveType.Leave;
				return true;
			}, delegate(MenuCallbackArgs args)
			{
				GameMenu.SwitchToMenu("town_arena");
			}, true, -1, false, null);
		}

		// Token: 0x06003337 RID: 13111 RVA: 0x000D34B4 File Offset: 0x000D16B4
		private void game_menu_tournament_join_on_init(MenuCallbackArgs args)
		{
			TournamentGame tournamentGame = Campaign.Current.TournamentManager.GetTournamentGame(Settlement.CurrentSettlement.Town);
			tournamentGame.UpdateTournamentPrize(true, false);
			GameTexts.SetVariable("MENU_TEXT", tournamentGame.GetMenuText());
		}

		// Token: 0x06003338 RID: 13112 RVA: 0x000D34F4 File Offset: 0x000D16F4
		private void game_menu_tournament_join_current_game_on_consequence(MenuCallbackArgs args)
		{
			CampaignMapConversation.OpenConversation(new ConversationCharacterData(CharacterObject.PlayerCharacter, null, false, false, false, false, false, false), new ConversationCharacterData(BettingFraudIssueBehavior.Instance._thug, null, false, false, false, false, false, false));
		}

		// Token: 0x06003339 RID: 13113 RVA: 0x000D3530 File Offset: 0x000D1730
		[GameMenuInitializationHandler("menu_town_tournament_join_betting_fraud")]
		private static void game_menu_ui_town_ui_on_init(MenuCallbackArgs args)
		{
			Settlement currentSettlement = Settlement.CurrentSettlement;
			args.MenuContext.SetBackgroundMeshName(currentSettlement.Town.WaitMeshName);
		}

		// Token: 0x0600333A RID: 13114 RVA: 0x000D3559 File Offset: 0x000D1759
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x0600333B RID: 13115 RVA: 0x000D355C File Offset: 0x000D175C
		private void CheckForIssue(Hero hero)
		{
			if (this.ConditionsHold(hero))
			{
				Campaign.Current.IssueManager.AddPotentialIssueData(hero, new PotentialIssueData(new PotentialIssueData.StartIssueDelegate(this.OnStartIssue), typeof(BettingFraudIssueBehavior.BettingFraudIssue), IssueBase.IssueFrequency.Rare, null));
				return;
			}
			Campaign.Current.IssueManager.AddPotentialIssueData(hero, new PotentialIssueData(typeof(BettingFraudIssueBehavior.BettingFraudIssue), IssueBase.IssueFrequency.Rare));
		}

		// Token: 0x0600333C RID: 13116 RVA: 0x000D35C0 File Offset: 0x000D17C0
		private bool ConditionsHold(Hero issueGiver)
		{
			return issueGiver.IsGangLeader && issueGiver.CurrentSettlement != null && issueGiver.CurrentSettlement.Town != null && issueGiver.CurrentSettlement.Town.Security < 45f;
		}

		// Token: 0x0600333D RID: 13117 RVA: 0x000D35F8 File Offset: 0x000D17F8
		private IssueBase OnStartIssue(in PotentialIssueData pid, Hero issueOwner)
		{
			return new BettingFraudIssueBehavior.BettingFraudIssue(issueOwner);
		}

		// Token: 0x04000EAF RID: 3759
		private const IssueBase.IssueFrequency BettingFraudIssueFrequency = IssueBase.IssueFrequency.Rare;

		// Token: 0x04000EB0 RID: 3760
		private const string JoinTournamentMenuId = "menu_town_tournament_join";

		// Token: 0x04000EB1 RID: 3761
		private const string JoinTournamentForBettingFraudQuestMenuId = "menu_town_tournament_join_betting_fraud";

		// Token: 0x04000EB2 RID: 3762
		private const int SettlementSecurityLimit = 55;

		// Token: 0x04000EB3 RID: 3763
		private const int SettlementSecurityMin = 45;

		// Token: 0x04000EB4 RID: 3764
		private BettingFraudIssueBehavior.BettingFraudQuest _cachedQuest;

		// Token: 0x020006F2 RID: 1778
		public class BettingFraudIssue : IssueBase
		{
			// Token: 0x0600554E RID: 21838 RVA: 0x00192D48 File Offset: 0x00190F48
			internal static void AutoGeneratedStaticCollectObjectsBettingFraudIssue(object o, List<object> collectedObjects)
			{
				((BettingFraudIssueBehavior.BettingFraudIssue)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x0600554F RID: 21839 RVA: 0x00192D56 File Offset: 0x00190F56
			protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				base.AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x17000FFE RID: 4094
			// (get) Token: 0x06005550 RID: 21840 RVA: 0x00192D5F File Offset: 0x00190F5F
			public override TextObject IssueBriefByIssueGiver
			{
				get
				{
					return new TextObject("{=kru5Vpog}Yes. I'm glad to have the chance to talk to you. I keep an eye on the careers of champions like yourself for professional reasons, and I have a proposal that might interest a good fighter like you. Interested?[ib:confident3][if:convo_bemused]", null);
				}
			}

			// Token: 0x17000FFF RID: 4095
			// (get) Token: 0x06005551 RID: 21841 RVA: 0x00192D6C File Offset: 0x00190F6C
			public override TextObject IssueAcceptByPlayer
			{
				get
				{
					return new TextObject("{=YWXkgDSd}What kind of a partnership are we talking about?", null);
				}
			}

			// Token: 0x17001000 RID: 4096
			// (get) Token: 0x06005552 RID: 21842 RVA: 0x00192D79 File Offset: 0x00190F79
			public override TextObject IssueQuestSolutionExplanationByIssueGiver
			{
				get
				{
					return new TextObject("{=vLaoZhkF}I follow tournaments, you see, and like to both place and take bets. But of course I need someone who can not only win those tournaments but lose if necessary... if you understand what I mean. Not all the time. That would be too obvious. Here's what I propose. We enter into a partnership for five tournaments. Don't bother memorizing which ones you win and which ones you lose. Before each fight, an associate of my mine will let you know how you should place. Follow my instructions and I promise you will be rewarded handsomely. What do you say?[if:convo_bemused][ib:demure2]", null);
				}
			}

			// Token: 0x17001001 RID: 4097
			// (get) Token: 0x06005553 RID: 21843 RVA: 0x00192D86 File Offset: 0x00190F86
			public override TextObject IssueQuestSolutionAcceptByPlayer
			{
				get
				{
					return new TextObject("{=cL9BX7ph}As long as the payment is good, I agree.", null);
				}
			}

			// Token: 0x17001002 RID: 4098
			// (get) Token: 0x06005554 RID: 21844 RVA: 0x00192D93 File Offset: 0x00190F93
			public override bool IsThereAlternativeSolution
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001003 RID: 4099
			// (get) Token: 0x06005555 RID: 21845 RVA: 0x00192D96 File Offset: 0x00190F96
			public override bool IsThereLordSolution
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001004 RID: 4100
			// (get) Token: 0x06005556 RID: 21846 RVA: 0x00192D99 File Offset: 0x00190F99
			public override TextObject Title
			{
				get
				{
					return new TextObject("{=xhVrxgC4}Betting Fraud", null);
				}
			}

			// Token: 0x17001005 RID: 4101
			// (get) Token: 0x06005557 RID: 21847 RVA: 0x00192DA6 File Offset: 0x00190FA6
			public override TextObject Description
			{
				get
				{
					TextObject textObject = new TextObject("{=3j8pV58L}{ISSUE_GIVER.NAME} offers you a deal to fix {TOURNAMENT_COUNT} tournaments and share the profit from the bet winnings.", null);
					textObject.SetCharacterProperties("ISSUE_GIVER", base.IssueOwner.CharacterObject, false);
					textObject.SetTextVariable("TOURNAMENT_COUNT", 5);
					return textObject;
				}
			}

			// Token: 0x06005558 RID: 21848 RVA: 0x00192DD7 File Offset: 0x00190FD7
			public BettingFraudIssue(Hero issueOwner)
				: base(issueOwner, CampaignTime.DaysFromNow(45f))
			{
			}

			// Token: 0x06005559 RID: 21849 RVA: 0x00192DEA File Offset: 0x00190FEA
			protected override float GetIssueEffectAmountInternal(IssueEffect issueEffect)
			{
				if (issueEffect == DefaultIssueEffects.IssueOwnerPower)
				{
					return -0.2f;
				}
				return 0f;
			}

			// Token: 0x0600555A RID: 21850 RVA: 0x00192DFF File Offset: 0x00190FFF
			protected override void OnGameLoad()
			{
			}

			// Token: 0x0600555B RID: 21851 RVA: 0x00192E01 File Offset: 0x00191001
			protected override void HourlyTick()
			{
			}

			// Token: 0x0600555C RID: 21852 RVA: 0x00192E03 File Offset: 0x00191003
			protected override QuestBase GenerateIssueQuest(string questId)
			{
				return new BettingFraudIssueBehavior.BettingFraudQuest(questId, base.IssueOwner, CampaignTime.DaysFromNow(45f), 0);
			}

			// Token: 0x0600555D RID: 21853 RVA: 0x00192E1C File Offset: 0x0019101C
			public override IssueBase.IssueFrequency GetFrequency()
			{
				return IssueBase.IssueFrequency.Rare;
			}

			// Token: 0x0600555E RID: 21854 RVA: 0x00192E20 File Offset: 0x00191020
			protected override bool CanPlayerTakeQuestConditions(Hero issueOwner, out IssueBase.PreconditionFlags flag, out Hero relationHero, out SkillObject skill, out int requiredGold)
			{
				relationHero = null;
				skill = null;
				requiredGold = 0;
				flag = IssueBase.PreconditionFlags.None;
				if (Clan.PlayerClan.Renown < 50f)
				{
					flag |= IssueBase.PreconditionFlags.Renown;
				}
				if (issueOwner.GetRelationWithPlayer() < -10f)
				{
					flag |= IssueBase.PreconditionFlags.Relation;
					relationHero = issueOwner;
				}
				if (Hero.MainHero.GetSkillValue(DefaultSkills.OneHanded) < 50 && Hero.MainHero.GetSkillValue(DefaultSkills.TwoHanded) < 50 && Hero.MainHero.GetSkillValue(DefaultSkills.Polearm) < 50 && Hero.MainHero.GetSkillValue(DefaultSkills.Bow) < 50 && Hero.MainHero.GetSkillValue(DefaultSkills.Crossbow) < 50 && Hero.MainHero.GetSkillValue(DefaultSkills.Throwing) < 50)
				{
					if (Hero.MainHero.GetSkillValue(DefaultSkills.OneHanded) < 50)
					{
						flag |= IssueBase.PreconditionFlags.Skill;
						skill = DefaultSkills.OneHanded;
					}
					else if (Hero.MainHero.GetSkillValue(DefaultSkills.TwoHanded) < 50)
					{
						flag |= IssueBase.PreconditionFlags.Skill;
						skill = DefaultSkills.TwoHanded;
					}
					else if (Hero.MainHero.GetSkillValue(DefaultSkills.Polearm) < 50)
					{
						flag |= IssueBase.PreconditionFlags.Skill;
						skill = DefaultSkills.Polearm;
					}
					else if (Hero.MainHero.GetSkillValue(DefaultSkills.Bow) < 50)
					{
						flag |= IssueBase.PreconditionFlags.Skill;
						skill = DefaultSkills.Bow;
					}
					else if (Hero.MainHero.GetSkillValue(DefaultSkills.Crossbow) < 50)
					{
						flag |= IssueBase.PreconditionFlags.Skill;
						skill = DefaultSkills.Crossbow;
					}
					else if (Hero.MainHero.GetSkillValue(DefaultSkills.Throwing) < 50)
					{
						flag |= IssueBase.PreconditionFlags.Skill;
						skill = DefaultSkills.Throwing;
					}
				}
				return flag == IssueBase.PreconditionFlags.None;
			}

			// Token: 0x0600555F RID: 21855 RVA: 0x00192FC0 File Offset: 0x001911C0
			public override bool IssueStayAliveConditions()
			{
				return base.IssueOwner.CurrentSettlement.Town.Security < 55f;
			}

			// Token: 0x06005560 RID: 21856 RVA: 0x00192FDE File Offset: 0x001911DE
			protected override void CompleteIssueWithTimedOutConsequences()
			{
			}

			// Token: 0x04001C14 RID: 7188
			private const int NeededTournamentCount = 5;

			// Token: 0x04001C15 RID: 7189
			private const int IssueDuration = 45;

			// Token: 0x04001C16 RID: 7190
			private const int MainHeroSkillLimit = 50;

			// Token: 0x04001C17 RID: 7191
			private const int MainClanRenownLimit = 50;

			// Token: 0x04001C18 RID: 7192
			private const int RelationLimitWithIssueOwner = -10;

			// Token: 0x04001C19 RID: 7193
			private const float IssueOwnerPowerPenaltyForIssueEffect = -0.2f;
		}

		// Token: 0x020006F3 RID: 1779
		public class BettingFraudQuest : QuestBase
		{
			// Token: 0x06005561 RID: 21857 RVA: 0x00192FE0 File Offset: 0x001911E0
			internal static void AutoGeneratedStaticCollectObjectsBettingFraudQuest(object o, List<object> collectedObjects)
			{
				((BettingFraudIssueBehavior.BettingFraudQuest)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x06005562 RID: 21858 RVA: 0x00192FEE File Offset: 0x001911EE
			protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				base.AutoGeneratedInstanceCollectObjects(collectedObjects);
				collectedObjects.Add(this._thug);
				collectedObjects.Add(this._startLog);
				collectedObjects.Add(this._counterOfferNotable);
			}

			// Token: 0x06005563 RID: 21859 RVA: 0x0019301B File Offset: 0x0019121B
			internal static object AutoGeneratedGetMemberValue_thug(object o)
			{
				return ((BettingFraudIssueBehavior.BettingFraudQuest)o)._thug;
			}

			// Token: 0x06005564 RID: 21860 RVA: 0x00193028 File Offset: 0x00191228
			internal static object AutoGeneratedGetMemberValue_startLog(object o)
			{
				return ((BettingFraudIssueBehavior.BettingFraudQuest)o)._startLog;
			}

			// Token: 0x06005565 RID: 21861 RVA: 0x00193035 File Offset: 0x00191235
			internal static object AutoGeneratedGetMemberValue_counterOfferNotable(object o)
			{
				return ((BettingFraudIssueBehavior.BettingFraudQuest)o)._counterOfferNotable;
			}

			// Token: 0x06005566 RID: 21862 RVA: 0x00193042 File Offset: 0x00191242
			internal static object AutoGeneratedGetMemberValue_fixedTournamentCount(object o)
			{
				return ((BettingFraudIssueBehavior.BettingFraudQuest)o)._fixedTournamentCount;
			}

			// Token: 0x06005567 RID: 21863 RVA: 0x00193054 File Offset: 0x00191254
			internal static object AutoGeneratedGetMemberValue_minorOffensiveCount(object o)
			{
				return ((BettingFraudIssueBehavior.BettingFraudQuest)o)._minorOffensiveCount;
			}

			// Token: 0x06005568 RID: 21864 RVA: 0x00193066 File Offset: 0x00191266
			internal static object AutoGeneratedGetMemberValue_counterOfferConversationDone(object o)
			{
				return ((BettingFraudIssueBehavior.BettingFraudQuest)o)._counterOfferConversationDone;
			}

			// Token: 0x17001006 RID: 4102
			// (get) Token: 0x06005569 RID: 21865 RVA: 0x00193078 File Offset: 0x00191278
			public override TextObject Title
			{
				get
				{
					return new TextObject("{=xhVrxgC4}Betting Fraud", null);
				}
			}

			// Token: 0x17001007 RID: 4103
			// (get) Token: 0x0600556A RID: 21866 RVA: 0x00193085 File Offset: 0x00191285
			public override bool IsRemainingTimeHidden
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001008 RID: 4104
			// (get) Token: 0x0600556B RID: 21867 RVA: 0x00193088 File Offset: 0x00191288
			private TextObject StartLog
			{
				get
				{
					TextObject textObject = new TextObject("{=6rweIvZS}{QUEST_GIVER.LINK}, a gang leader from {SETTLEMENT} offers you to fix 5 tournaments together and share the profit.{newline}{?QUEST_GIVER.GENDER}She{?}He{\\?} asked you to enter 5 tournaments and follow the instructions given by {?QUEST_GIVER.GENDER}her{?}his{\\?} associate.", null);
					textObject.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, false);
					textObject.SetTextVariable("SETTLEMENT", base.QuestGiver.CurrentSettlement.EncyclopediaLinkWithName);
					return textObject;
				}
			}

			// Token: 0x17001009 RID: 4105
			// (get) Token: 0x0600556C RID: 21868 RVA: 0x001930C8 File Offset: 0x001912C8
			private TextObject CurrentDirectiveLog
			{
				get
				{
					TextObject textObject = new TextObject("{=dnZekyZI}Directive from {QUEST_GIVER.LINK}: {DIRECTIVE}", null);
					textObject.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, false);
					textObject.SetTextVariable("DIRECTIVE", this.GetDirectiveText());
					return textObject;
				}
			}

			// Token: 0x1700100A RID: 4106
			// (get) Token: 0x0600556D RID: 21869 RVA: 0x00193100 File Offset: 0x00191300
			private TextObject QuestFailedWithTimeOutLog
			{
				get
				{
					TextObject textObject = new TextObject("{=2brAaeFh}You failed to complete tournaments in time. {QUEST_GIVER.LINK} will certainly be disappointed.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x0600556E RID: 21870 RVA: 0x00193134 File Offset: 0x00191334
			public BettingFraudQuest(string questId, Hero questGiver, CampaignTime duration, int rewardGold)
				: base(questId, questGiver, duration, rewardGold)
			{
				this._counterOfferNotable = null;
				this._fixedTournamentCount = 0;
				this._minorOffensiveCount = 0;
				this._counterOfferAccepted = false;
				this._readyToStartTournament = false;
				this._startTournamentEndConversation = false;
				this._counterOfferConversationDone = false;
				this._currentDirective = BettingFraudIssueBehavior.BettingFraudQuest.Directives.None;
				this._afterTournamentConversationState = BettingFraudIssueBehavior.BettingFraudQuest.AfterTournamentConversationState.None;
				this._thug = MBObjectManager.Instance.GetObject<CharacterObject>((MBRandom.RandomFloat > 0.5f) ? "betting_fraud_thug_male" : "betting_fraud_thug_female");
				this._startLog = null;
				this.SetDialogs();
				base.InitializeQuestOnCreation();
			}

			// Token: 0x0600556F RID: 21871 RVA: 0x001931C6 File Offset: 0x001913C6
			protected override void InitializeQuestOnGameLoad()
			{
				this.SetDialogs();
			}

			// Token: 0x06005570 RID: 21872 RVA: 0x001931CE File Offset: 0x001913CE
			protected override void HourlyTick()
			{
			}

			// Token: 0x06005571 RID: 21873 RVA: 0x001931D0 File Offset: 0x001913D0
			private void SelectCounterOfferNotable(Settlement settlement)
			{
				this._counterOfferNotable = settlement.Notables.GetRandomElement<Hero>();
			}

			// Token: 0x06005572 RID: 21874 RVA: 0x001931E3 File Offset: 0x001913E3
			private void IncreaseMinorOffensive()
			{
				this._minorOffensiveCount++;
				this._currentDirective = BettingFraudIssueBehavior.BettingFraudQuest.Directives.None;
				if (this._minorOffensiveCount >= 2)
				{
					this._afterTournamentConversationState = BettingFraudIssueBehavior.BettingFraudQuest.AfterTournamentConversationState.SecondMinorOffense;
					return;
				}
				this._afterTournamentConversationState = BettingFraudIssueBehavior.BettingFraudQuest.AfterTournamentConversationState.MinorOffense;
			}

			// Token: 0x06005573 RID: 21875 RVA: 0x00193212 File Offset: 0x00191412
			private void IncreaseFixedTournamentCount()
			{
				this._fixedTournamentCount++;
				this._startLog.UpdateCurrentProgress(this._fixedTournamentCount);
				this._currentDirective = BettingFraudIssueBehavior.BettingFraudQuest.Directives.None;
				if (this._fixedTournamentCount >= 5)
				{
					this._afterTournamentConversationState = BettingFraudIssueBehavior.BettingFraudQuest.AfterTournamentConversationState.BigReward;
					return;
				}
				this._afterTournamentConversationState = BettingFraudIssueBehavior.BettingFraudQuest.AfterTournamentConversationState.SmallReward;
			}

			// Token: 0x06005574 RID: 21876 RVA: 0x00193252 File Offset: 0x00191452
			private void SetCurrentDirective()
			{
				this._currentDirective = ((MBRandom.RandomFloat <= 0.33f) ? BettingFraudIssueBehavior.BettingFraudQuest.Directives.LoseAt3RdRound : ((MBRandom.RandomFloat < 0.5f) ? BettingFraudIssueBehavior.BettingFraudQuest.Directives.LoseAt4ThRound : BettingFraudIssueBehavior.BettingFraudQuest.Directives.WinTheTournament));
				base.AddLog(this.CurrentDirectiveLog, false);
			}

			// Token: 0x06005575 RID: 21877 RVA: 0x00193288 File Offset: 0x00191488
			private void StartTournamentMission()
			{
				TournamentGame tournamentGame = Campaign.Current.TournamentManager.GetTournamentGame(Settlement.CurrentSettlement.Town);
				GameMenu.SwitchToMenu("town");
				tournamentGame.PrepareForTournamentGame(true);
				Campaign.Current.TournamentManager.OnPlayerJoinTournament(tournamentGame.GetType(), Settlement.CurrentSettlement);
			}

			// Token: 0x06005576 RID: 21878 RVA: 0x001932DC File Offset: 0x001914DC
			protected override void RegisterEvents()
			{
				CampaignEvents.PlayerEliminatedFromTournament.AddNonSerializedListener(this, new Action<int, Town>(this.OnPlayerEliminatedFromTournament));
				CampaignEvents.TournamentFinished.AddNonSerializedListener(this, new Action<CharacterObject, MBReadOnlyList<CharacterObject>, Town, ItemObject>(this.OnTournamentFinished));
				CampaignEvents.GameMenuOpened.AddNonSerializedListener(this, new Action<MenuCallbackArgs>(this.OnGameMenuOpened));
			}

			// Token: 0x06005577 RID: 21879 RVA: 0x0019332E File Offset: 0x0019152E
			private void OnPlayerEliminatedFromTournament(int round, Town town)
			{
				this._startTournamentEndConversation = true;
				if (round == (int)this._currentDirective)
				{
					this.IncreaseFixedTournamentCount();
					return;
				}
				if (round < (int)this._currentDirective)
				{
					this.IncreaseMinorOffensive();
					return;
				}
				if (round > (int)this._currentDirective)
				{
					this._afterTournamentConversationState = BettingFraudIssueBehavior.BettingFraudQuest.AfterTournamentConversationState.MajorOffense;
				}
			}

			// Token: 0x06005578 RID: 21880 RVA: 0x00193368 File Offset: 0x00191568
			private void OnTournamentFinished(CharacterObject winner, MBReadOnlyList<CharacterObject> participants, Town town, ItemObject prize)
			{
				if (participants.Contains(CharacterObject.PlayerCharacter) && this._currentDirective != BettingFraudIssueBehavior.BettingFraudQuest.Directives.None)
				{
					this._startTournamentEndConversation = true;
					if (this._currentDirective == BettingFraudIssueBehavior.BettingFraudQuest.Directives.WinTheTournament)
					{
						if (winner == CharacterObject.PlayerCharacter)
						{
							this.IncreaseFixedTournamentCount();
							return;
						}
						this.IncreaseMinorOffensive();
						return;
					}
					else if (winner == CharacterObject.PlayerCharacter)
					{
						this._afterTournamentConversationState = BettingFraudIssueBehavior.BettingFraudQuest.AfterTournamentConversationState.MajorOffense;
					}
				}
			}

			// Token: 0x06005579 RID: 21881 RVA: 0x001933C0 File Offset: 0x001915C0
			private void OnGameMenuOpened(MenuCallbackArgs args)
			{
				if (args.MenuContext.GameMenu.StringId == "menu_town_tournament_join")
				{
					GameMenu.SwitchToMenu("menu_town_tournament_join_betting_fraud");
				}
				if (args.MenuContext.GameMenu.StringId == "menu_town_tournament_join_betting_fraud")
				{
					if (this._readyToStartTournament)
					{
						if (this._fixedTournamentCount == 4 && !this._counterOfferConversationDone && this._counterOfferNotable != null && this._currentDirective != BettingFraudIssueBehavior.BettingFraudQuest.Directives.WinTheTournament)
						{
							CampaignMapConversation.OpenConversation(new ConversationCharacterData(CharacterObject.PlayerCharacter, null, false, false, false, false, false, false), new ConversationCharacterData(this._counterOfferNotable.CharacterObject, null, false, false, false, false, false, false));
						}
						else
						{
							this.StartTournamentMission();
							this._readyToStartTournament = false;
						}
					}
					if (this._fixedTournamentCount == 4 && (this._counterOfferNotable == null || this._counterOfferNotable.CurrentSettlement != Settlement.CurrentSettlement))
					{
						this.SelectCounterOfferNotable(Settlement.CurrentSettlement);
					}
				}
				if (this._startTournamentEndConversation)
				{
					CampaignMapConversation.OpenConversation(new ConversationCharacterData(CharacterObject.PlayerCharacter, null, false, false, false, false, false, false), new ConversationCharacterData(this._thug, null, false, false, false, false, false, false));
				}
			}

			// Token: 0x0600557A RID: 21882 RVA: 0x001934D6 File Offset: 0x001916D6
			protected override void OnTimedOut()
			{
				base.OnTimedOut();
				this.PlayerDidNotCompleteTournaments();
			}

			// Token: 0x0600557B RID: 21883 RVA: 0x001934E4 File Offset: 0x001916E4
			protected override void SetDialogs()
			{
				this.OfferDialogFlow = this.GetOfferDialogFlow();
				this.DiscussDialogFlow = this.GetDiscussDialogFlow();
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetDialogWithThugStart(), this);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetDialogWithThugEnd(), this);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetCounterOfferDialog(), this);
			}

			// Token: 0x0600557C RID: 21884 RVA: 0x0019354C File Offset: 0x0019174C
			private DialogFlow GetOfferDialogFlow()
			{
				return DialogFlow.CreateDialogFlow("issue_classic_quest_start", 100).NpcLine(new TextObject("{=sp52g5AQ}Very good, very good. Try to enter five tournaments over the next 45 days or so. Right before the fight you'll hear from my associate how far I want you to go in the rankings before you lose.[if:convo_delighted][ib:hip]", null), null, null, null, null).Condition(() => Hero.OneToOneConversationHero == base.QuestGiver)
					.NpcLine(new TextObject("{=ADIYnC4u}Now, I know you can't win every fight, so if you underperform once or twice, I'd understand. But if you lose every time, or worse, if you overperform, well, then I'll be a bit angry.[if:convo_nonchalant][ib:normal2]", null), null, null, null, null)
					.NpcLine(new TextObject("{=1hOPCf8I}But I'm sure you won't disappoint me. Enjoy your riches![if:convo_focused_happy][ib:confident]", null), null, null, null, null)
					.Consequence(new ConversationSentence.OnConsequenceDelegate(this.OfferDialogFlowConsequence))
					.CloseDialog();
			}

			// Token: 0x0600557D RID: 21885 RVA: 0x001935C8 File Offset: 0x001917C8
			private void OfferDialogFlowConsequence()
			{
				base.StartQuest();
				this._startLog = base.AddDiscreteLog(this.StartLog, new TextObject("{=dLfWFa61}Fix 5 Tournaments", null), 0, 5, null, false);
			}

			// Token: 0x0600557E RID: 21886 RVA: 0x001935F4 File Offset: 0x001917F4
			private DialogFlow GetDiscussDialogFlow()
			{
				return DialogFlow.CreateDialogFlow("quest_discuss", 100).NpcLine(new TextObject("{=!}{RESPONSE_TEXT}", null), null, null, null, null).Condition(new ConversationSentence.OnConditionDelegate(this.DiscussDialogCondition))
					.BeginPlayerOptions(null, false)
					.PlayerOption(new TextObject("{=abLgPWzf}I will continue to honor our deal. Do not forget to do your end, that's all.", null), null, null, null)
					.BeginNpcOptions(null, false)
					.NpcOption(new TextObject("{=ZLPEsMUx}Well, there are tournament happening in {NEARBY_TOURNAMENTS_LIST} right now. You can go there and do the job. Your denars will be waiting for you.", null), new ConversationSentence.OnConditionDelegate(this.NpcTournamentLocationCondition), null, null, null, null)
					.CloseDialog()
					.NpcDefaultOption("{=sUfSCLQx}Sadly, I've heard no news of an upcoming tournament. I am sure one will be held before too long.")
					.CloseDialog()
					.EndNpcOptions()
					.CloseDialog()
					.PlayerOption(new TextObject("{=XUS5wNsD}I feel like I do all the job and you get your denars.", null), null, null, null)
					.BeginNpcOptions(null, false)
					.NpcOption(new TextObject("{=ZLPEsMUx}Well, there are tournament happening in {NEARBY_TOURNAMENTS_LIST} right now. You can go there and do the job. Your denars will be waiting for you.", null), new ConversationSentence.OnConditionDelegate(this.NpcTournamentLocationCondition), null, null, null, null)
					.CloseDialog()
					.NpcDefaultOption("{=sUfSCLQx}Sadly, I've heard no news of an upcoming tournament. I am sure one will be held before too long.")
					.CloseDialog()
					.EndNpcOptions()
					.CloseDialog()
					.EndPlayerOptions()
					.CloseDialog();
			}

			// Token: 0x0600557F RID: 21887 RVA: 0x001936F4 File Offset: 0x001918F4
			private bool DiscussDialogCondition()
			{
				bool flag = Hero.OneToOneConversationHero == base.QuestGiver;
				if (flag)
				{
					if (this._minorOffensiveCount > 0)
					{
						MBTextManager.SetTextVariable("RESPONSE_TEXT", new TextObject("{=7SPwGYvf}I had expected better of you. But even the best can fail sometimes. Just make sure it does not happen again.[if:convo_bored][ib:closed2] ", null), false);
						return flag;
					}
					MBTextManager.SetTextVariable("RESPONSE_TEXT", new TextObject("{=vo0uhUsZ}I have high hopes for you, friend. Just follow my directives and we will be rich.[if:convo_relaxed_happy][ib:demure2]", null), false);
				}
				return flag;
			}

			// Token: 0x06005580 RID: 21888 RVA: 0x00193748 File Offset: 0x00191948
			private bool NpcTournamentLocationCondition()
			{
				List<Town> list = Town.AllTowns.Where<Town>((Town x) => Campaign.Current.TournamentManager.GetTournamentGame(x) != null && x != Settlement.CurrentSettlement.Town).ToList<Town>();
				list = list.OrderBy<Town, float>((Town x) => DistanceHelper.FindClosestDistanceFromSettlementToSettlement(x.Settlement, Settlement.CurrentSettlement, MobileParty.NavigationType.Default)).ToList<Town>();
				if (list.Count > 0)
				{
					MBTextManager.SetTextVariable("NEARBY_TOURNAMENTS_LIST", list[0].Name, false);
					return true;
				}
				return false;
			}

			// Token: 0x06005581 RID: 21889 RVA: 0x001937D4 File Offset: 0x001919D4
			private DialogFlow GetDialogWithThugStart()
			{
				return DialogFlow.CreateDialogFlow("start", 125).NpcLine(new TextObject("{=!}{GREETING_LINE}", null), null, null, null, null).Condition(new ConversationSentence.OnConditionDelegate(this.DialogWithThugStartCondition))
					.BeginPlayerOptions(null, false)
					.PlayerOption(new TextObject("{=!}{POSITIVE_OPTION}", null), null, null, null)
					.Condition(new ConversationSentence.OnConditionDelegate(this.PositiveOptionCondition))
					.Consequence(new ConversationSentence.OnConsequenceDelegate(this.PositiveOptionConsequences))
					.CloseDialog()
					.PlayerOption(new TextObject("{=!}{NEGATIVE_OPTION}", null), null, null, null)
					.Condition(new ConversationSentence.OnConditionDelegate(this.NegativeOptionCondition))
					.Consequence(new ConversationSentence.OnConsequenceDelegate(this.NegativeOptionConsequence))
					.CloseDialog()
					.EndPlayerOptions()
					.CloseDialog();
			}

			// Token: 0x06005582 RID: 21890 RVA: 0x00193898 File Offset: 0x00191A98
			private bool DialogWithThugStartCondition()
			{
				bool flag = CharacterObject.OneToOneConversationCharacter == this._thug && !this._startTournamentEndConversation;
				if (flag)
				{
					this.SetCurrentDirective();
					if (this._fixedTournamentCount < 2)
					{
						TextObject textObject = new TextObject("{=xYu4yVRU}Hey there friend. So... You don't need to know my name, but suffice to say that we're both friends of {QUEST_GIVER.LINK}. Here's {?QUEST_GIVER.GENDER}her{?}his{\\?} message for you: {DIRECTIVE}.[ib:confident][if:convo_nonchalant]", null);
						textObject.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, false);
						textObject.SetTextVariable("DIRECTIVE", this.GetDirectiveText());
						MBTextManager.SetTextVariable("GREETING_LINE", textObject, false);
						return flag;
					}
					if (this._fixedTournamentCount < 4)
					{
						TextObject textObject2 = new TextObject("{=cQE9tQOy}My friend! Good to see you. You did very well in that last fight. People definitely won't be expecting you to \"{DIRECTIVE}\". What a surprise that would be. Well, I should not keep you from your tournament. You know what to do.[if:convo_happy][ib:closed2]", null);
						textObject2.SetTextVariable("DIRECTIVE", this.GetDirectiveText());
						MBTextManager.SetTextVariable("GREETING_LINE", textObject2, false);
						return flag;
					}
					TextObject textObject3 = new TextObject("{=RVLPQ4rm}My friend. I am almost sad that these meetings are going to come to an end. Well, a deal is a deal. I won't beat around the bush. Here's your final message: {DIRECTIVE}. I wish you luck, right up until the moment that you have to go down.[if:convo_mocking_teasing][ib:closed]", null);
					textObject3.SetTextVariable("DIRECTIVE", this.GetDirectiveText());
					MBTextManager.SetTextVariable("GREETING_LINE", textObject3, false);
				}
				return flag;
			}

			// Token: 0x06005583 RID: 21891 RVA: 0x00193974 File Offset: 0x00191B74
			private bool PositiveOptionCondition()
			{
				if (this._fixedTournamentCount < 2)
				{
					MBTextManager.SetTextVariable("POSITIVE_OPTION", new TextObject("{=PrUauabl}As long as the payment is as we talked, you got nothing to worry about.", null), false);
				}
				else if (this._fixedTournamentCount < 4)
				{
					MBTextManager.SetTextVariable("POSITIVE_OPTION", new TextObject("{=TKRsPVMU}Yes, I did. Be around when the tournament is over.", null), false);
				}
				else
				{
					MBTextManager.SetTextVariable("POSITIVE_OPTION", new TextObject("{=26XPQw2v}I will miss this little deal we had. See you at the end", null), false);
				}
				return true;
			}

			// Token: 0x06005584 RID: 21892 RVA: 0x001939DA File Offset: 0x00191BDA
			private void PositiveOptionConsequences()
			{
				this._readyToStartTournament = true;
			}

			// Token: 0x06005585 RID: 21893 RVA: 0x001939E3 File Offset: 0x00191BE3
			private bool NegativeOptionCondition()
			{
				bool flag = this._fixedTournamentCount >= 4;
				if (flag)
				{
					MBTextManager.SetTextVariable("NEGATIVE_OPTION", new TextObject("{=vapdvRQO}This deal was a mistake. We will not talk again after this last tournament.", null), false);
				}
				return flag;
			}

			// Token: 0x06005586 RID: 21894 RVA: 0x00193A0A File Offset: 0x00191C0A
			private void NegativeOptionConsequence()
			{
				this._readyToStartTournament = true;
			}

			// Token: 0x06005587 RID: 21895 RVA: 0x00193A14 File Offset: 0x00191C14
			private TextObject GetDirectiveText()
			{
				if (this._currentDirective == BettingFraudIssueBehavior.BettingFraudQuest.Directives.LoseAt3RdRound)
				{
					return new TextObject("{=aHlcBLYB}Lose this tournament at 3rd round", null);
				}
				if (this._currentDirective == BettingFraudIssueBehavior.BettingFraudQuest.Directives.LoseAt4ThRound)
				{
					return new TextObject("{=hc1mnqOx}Lose this tournament at 4th round", null);
				}
				if (this._currentDirective == BettingFraudIssueBehavior.BettingFraudQuest.Directives.WinTheTournament)
				{
					return new TextObject("{=hl4pTsaO}Win this tournament", null);
				}
				return TextObject.GetEmpty();
			}

			// Token: 0x06005588 RID: 21896 RVA: 0x00193A68 File Offset: 0x00191C68
			private DialogFlow GetDialogWithThugEnd()
			{
				return DialogFlow.CreateDialogFlow("start", 125).NpcLine(new TextObject("{=!}{GREETING_LINE}", null), null, null, null, null).Condition(new ConversationSentence.OnConditionDelegate(this.DialogWithThugEndCondition))
					.Consequence(new ConversationSentence.OnConsequenceDelegate(this.DialogWithThugEndConsequence))
					.CloseDialog();
			}

			// Token: 0x06005589 RID: 21897 RVA: 0x00193ABC File Offset: 0x00191CBC
			private bool DialogWithThugEndCondition()
			{
				bool flag = CharacterObject.OneToOneConversationCharacter == this._thug && this._startTournamentEndConversation;
				if (flag)
				{
					TextObject textObject = TextObject.GetEmpty();
					switch (this._afterTournamentConversationState)
					{
					case BettingFraudIssueBehavior.BettingFraudQuest.AfterTournamentConversationState.SmallReward:
						textObject = new TextObject("{=ZM8t4ZW2}We are very impressed, my friend. Here is the payment as promised. I hope we can continue this profitable partnership. See you at the next tournament.[if:convo_happy][ib:demure]", null);
						GiveGoldAction.ApplyBetweenCharacters(null, Hero.MainHero, 250, false);
						break;
					case BettingFraudIssueBehavior.BettingFraudQuest.AfterTournamentConversationState.BigReward:
						textObject = new TextObject("{=9vOZWY25}What an exciting result! I will definitely miss these tournaments. Well, maybe after some time goes by and memories get a little hazy we can continue. Here is the last payment. Very well deserved.[if:convo_happy][ib:demure]", null);
						break;
					case BettingFraudIssueBehavior.BettingFraudQuest.AfterTournamentConversationState.MinorOffense:
						textObject = new TextObject("{=d8bGHJnZ}This was not we were expecting. We lost some money. Well, Lady Fortune always casts her ballot too in these contests. But try to reassure us that this was her plan, and not yours, eh?[if:convo_grave][ib:closed2]", null);
						break;
					case BettingFraudIssueBehavior.BettingFraudQuest.AfterTournamentConversationState.SecondMinorOffense:
						textObject = new TextObject("{=bNAG2t8S}Well, my friend, either you're playing us false or you're just not very good at this. Either way, {QUEST_GIVER.LINK} wishes to tell you that {?QUEST_GIVER.GENDER}her{?}his{\\?} association with you is over.[if:convo_predatory][ib:closed2]", null);
						textObject.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, false);
						break;
					case BettingFraudIssueBehavior.BettingFraudQuest.AfterTournamentConversationState.MajorOffense:
						textObject = new TextObject("{=Lyqx3NYE}Well... What happened back there... That wasn't bad luck or incompetence. {QUEST_GIVER.LINK} trusted in you and {?QUEST_GIVER.GENDER}She{?}He{\\?} doesn't take well to betrayal.[if:convo_angry][ib:warrior]", null);
						textObject.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, false);
						break;
					default:
						Debug.FailedAssert("After tournament conversation state is not set!", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Issues\\BettingFraudIssueBehavior.cs", "DialogWithThugEndCondition", 723);
						break;
					}
					MBTextManager.SetTextVariable("GREETING_LINE", textObject, false);
				}
				return flag;
			}

			// Token: 0x0600558A RID: 21898 RVA: 0x00193BC0 File Offset: 0x00191DC0
			private void DialogWithThugEndConsequence()
			{
				this._startTournamentEndConversation = false;
				switch (this._afterTournamentConversationState)
				{
				case BettingFraudIssueBehavior.BettingFraudQuest.AfterTournamentConversationState.SmallReward:
				case BettingFraudIssueBehavior.BettingFraudQuest.AfterTournamentConversationState.MinorOffense:
					break;
				case BettingFraudIssueBehavior.BettingFraudQuest.AfterTournamentConversationState.BigReward:
					this.MainHeroSuccessfullyFixedTournaments();
					return;
				case BettingFraudIssueBehavior.BettingFraudQuest.AfterTournamentConversationState.SecondMinorOffense:
					this.MainHeroFailToFixTournaments();
					return;
				case BettingFraudIssueBehavior.BettingFraudQuest.AfterTournamentConversationState.MajorOffense:
					if (this._counterOfferAccepted)
					{
						this.MainHeroAcceptsCounterOffer();
						return;
					}
					this.MainHeroChooseNotToFixTournaments();
					break;
				default:
					return;
				}
			}

			// Token: 0x0600558B RID: 21899 RVA: 0x00193C1C File Offset: 0x00191E1C
			private DialogFlow GetCounterOfferDialog()
			{
				return DialogFlow.CreateDialogFlow("start", 125).NpcLine(new TextObject("{=bUfBHSsz}Hold on a moment, friend. I need to talk to you.[ib:aggressive]", null), null, null, null, null).Condition(new ConversationSentence.OnConditionDelegate(this.CounterOfferConversationStartCondition))
					.Consequence(new ConversationSentence.OnConsequenceDelegate(this.CounterOfferConversationStartConsequence))
					.PlayerLine(new TextObject("{=PZfR7hEK}What do you want? I have a tournament to prepare for.", null), null, null, null)
					.NpcLine(new TextObject("{=GN9F316V}Oh of course you do. {QUEST_GIVER.LINK}'s people have been running around placing bets - we know all about your arrangement, you see. And let me tell you something: as these arrangements go, {QUEST_GIVER.LINK} is getting you cheap. Do you want to see real money? Win this tournament and I will pay you what you're worth. And isn't it better to win than to lose?[if:convo_mocking_aristocratic][ib:confident2]", null), null, null, null, null)
					.Condition(new ConversationSentence.OnConditionDelegate(this.AccusationCondition))
					.BeginPlayerOptions(null, false)
					.PlayerOption(new TextObject("{=MacG8ikN}I will think about it.", null), null, null, null)
					.Consequence(new ConversationSentence.OnConsequenceDelegate(this.CounterOfferAcceptedConsequence))
					.CloseDialog()
					.PlayerOption(new TextObject("{=bT279pk9}I have no idea what you talking about. Be on your way, friend.", null), null, null, null)
					.CloseDialog()
					.EndPlayerOptions()
					.CloseDialog();
			}

			// Token: 0x0600558C RID: 21900 RVA: 0x00193CF5 File Offset: 0x00191EF5
			private bool CounterOfferConversationStartCondition()
			{
				return this._counterOfferNotable != null && CharacterObject.OneToOneConversationCharacter == this._counterOfferNotable.CharacterObject;
			}

			// Token: 0x0600558D RID: 21901 RVA: 0x00193D13 File Offset: 0x00191F13
			private void CounterOfferConversationStartConsequence()
			{
				this._counterOfferConversationDone = true;
			}

			// Token: 0x0600558E RID: 21902 RVA: 0x00193D1C File Offset: 0x00191F1C
			private bool AccusationCondition()
			{
				StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, null, false);
				return true;
			}

			// Token: 0x0600558F RID: 21903 RVA: 0x00193D37 File Offset: 0x00191F37
			private void CounterOfferAcceptedConsequence()
			{
				this._counterOfferAccepted = true;
			}

			// Token: 0x06005590 RID: 21904 RVA: 0x00193D40 File Offset: 0x00191F40
			private void MainHeroSuccessfullyFixedTournaments()
			{
				TextObject textObject = new TextObject("{=aCA83avL}You have placed in the tournaments as {QUEST_GIVER.LINK} wished.", null);
				textObject.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, false);
				base.AddLog(textObject, false);
				GiveGoldAction.ApplyBetweenCharacters(null, Hero.MainHero, 2500, false);
				Clan.PlayerClan.AddRenown(2f, true);
				base.QuestGiver.AddPower(10f);
				base.QuestGiver.CurrentSettlement.Town.Security += -20f;
				this.RelationshipChangeWithQuestGiver = 5;
				base.CompleteQuestWithSuccess();
			}

			// Token: 0x06005591 RID: 21905 RVA: 0x00193DD8 File Offset: 0x00191FD8
			private void MainHeroFailToFixTournaments()
			{
				TextObject textObject = new TextObject("{=ETbToaZC}You have failed to place in the tournaments as {QUEST_GIVER.LINK} wished.", null);
				textObject.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, false);
				base.AddLog(textObject, false);
				base.QuestGiver.AddPower(-10f);
				base.QuestGiver.CurrentSettlement.Town.Security += 10f;
				this.RelationshipChangeWithQuestGiver = -5;
				base.CompleteQuestWithFail(null);
			}

			// Token: 0x06005592 RID: 21906 RVA: 0x00193E54 File Offset: 0x00192054
			private void MainHeroChooseNotToFixTournaments()
			{
				TextObject textObject = new TextObject("{=52smwnzz}You have chosen not to place in the tournaments as {QUEST_GIVER.LINK} wished.", null);
				textObject.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, false);
				base.AddLog(textObject, false);
				base.QuestGiver.AddPower(-15f);
				base.QuestGiver.CurrentSettlement.Town.Security += 15f;
				this.RelationshipChangeWithQuestGiver = -10;
				base.CompleteQuestWithFail(null);
			}

			// Token: 0x06005593 RID: 21907 RVA: 0x00193ED0 File Offset: 0x001920D0
			private void MainHeroAcceptsCounterOffer()
			{
				TextObject textObject = new TextObject("{=nb0wqaGA}You have made a deal with {NOTABLE.LINK} to betray {QUEST_GIVER.LINK}.", null);
				textObject.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, false);
				textObject.SetCharacterProperties("NOTABLE", this._counterOfferNotable.CharacterObject, false);
				base.AddLog(textObject, false);
				GiveGoldAction.ApplyBetweenCharacters(null, Hero.MainHero, 4500, false);
				base.QuestGiver.AddPower(-15f);
				base.QuestGiver.CurrentSettlement.Town.Security += 15f;
				ChangeRelationAction.ApplyPlayerRelation(this._counterOfferNotable, 2, true, true);
				this.RelationshipChangeWithQuestGiver = -10;
				base.CompleteQuestWithFail(null);
			}

			// Token: 0x06005594 RID: 21908 RVA: 0x00193F7F File Offset: 0x0019217F
			private void PlayerDidNotCompleteTournaments()
			{
				base.AddLog(this.QuestFailedWithTimeOutLog, false);
				ChangeRelationAction.ApplyPlayerRelation(base.QuestGiver, -5, true, true);
			}

			// Token: 0x04001C1A RID: 7194
			private const int TournamentFixCount = 5;

			// Token: 0x04001C1B RID: 7195
			private const int MinorOffensiveLimit = 2;

			// Token: 0x04001C1C RID: 7196
			private const int SmallReward = 250;

			// Token: 0x04001C1D RID: 7197
			private const int BigReward = 2500;

			// Token: 0x04001C1E RID: 7198
			private const int CounterOfferReward = 4500;

			// Token: 0x04001C1F RID: 7199
			private const string MaleThug = "betting_fraud_thug_male";

			// Token: 0x04001C20 RID: 7200
			private const string FemaleThug = "betting_fraud_thug_female";

			// Token: 0x04001C21 RID: 7201
			[SaveableField(100)]
			private JournalLog _startLog;

			// Token: 0x04001C22 RID: 7202
			[SaveableField(1)]
			private Hero _counterOfferNotable;

			// Token: 0x04001C23 RID: 7203
			[SaveableField(10)]
			internal readonly CharacterObject _thug;

			// Token: 0x04001C24 RID: 7204
			[SaveableField(20)]
			private int _fixedTournamentCount;

			// Token: 0x04001C25 RID: 7205
			[SaveableField(30)]
			private int _minorOffensiveCount;

			// Token: 0x04001C26 RID: 7206
			private BettingFraudIssueBehavior.BettingFraudQuest.Directives _currentDirective;

			// Token: 0x04001C27 RID: 7207
			private BettingFraudIssueBehavior.BettingFraudQuest.AfterTournamentConversationState _afterTournamentConversationState;

			// Token: 0x04001C28 RID: 7208
			private bool _counterOfferAccepted;

			// Token: 0x04001C29 RID: 7209
			private bool _readyToStartTournament;

			// Token: 0x04001C2A RID: 7210
			private bool _startTournamentEndConversation;

			// Token: 0x04001C2B RID: 7211
			[SaveableField(40)]
			private bool _counterOfferConversationDone;

			// Token: 0x020008CC RID: 2252
			private enum Directives
			{
				// Token: 0x04002534 RID: 9524
				None,
				// Token: 0x04002535 RID: 9525
				LoseAt3RdRound = 2,
				// Token: 0x04002536 RID: 9526
				LoseAt4ThRound,
				// Token: 0x04002537 RID: 9527
				WinTheTournament
			}

			// Token: 0x020008CD RID: 2253
			private enum AfterTournamentConversationState
			{
				// Token: 0x04002539 RID: 9529
				None,
				// Token: 0x0400253A RID: 9530
				SmallReward,
				// Token: 0x0400253B RID: 9531
				BigReward,
				// Token: 0x0400253C RID: 9532
				MinorOffense,
				// Token: 0x0400253D RID: 9533
				SecondMinorOffense,
				// Token: 0x0400253E RID: 9534
				MajorOffense
			}
		}

		// Token: 0x020006F4 RID: 1780
		public class BettingFraudIssueTypeDefiner : SaveableTypeDefiner
		{
			// Token: 0x06005596 RID: 21910 RVA: 0x00193FAD File Offset: 0x001921AD
			public BettingFraudIssueTypeDefiner()
				: base(600327)
			{
			}

			// Token: 0x06005597 RID: 21911 RVA: 0x00193FBA File Offset: 0x001921BA
			protected override void DefineClassTypes()
			{
				base.AddClassDefinition(typeof(BettingFraudIssueBehavior.BettingFraudIssue), 1, null);
				base.AddClassDefinition(typeof(BettingFraudIssueBehavior.BettingFraudQuest), 2, null);
			}
		}
	}
}
