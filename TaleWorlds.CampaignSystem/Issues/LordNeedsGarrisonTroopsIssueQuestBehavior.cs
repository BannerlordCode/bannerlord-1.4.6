using System;
using System.Collections.Generic;
using Helpers;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.Issues
{
	// Token: 0x0200036F RID: 879
	public class LordNeedsGarrisonTroopsIssueQuestBehavior : CampaignBehaviorBase
	{
		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x060033C3 RID: 13251 RVA: 0x000D5224 File Offset: 0x000D3424
		private static LordNeedsGarrisonTroopsIssueQuestBehavior.LordNeedsGarrisonTroopsIssueQuest Instance
		{
			get
			{
				LordNeedsGarrisonTroopsIssueQuestBehavior campaignBehavior = Campaign.Current.GetCampaignBehavior<LordNeedsGarrisonTroopsIssueQuestBehavior>();
				if (campaignBehavior._cachedQuest != null && campaignBehavior._cachedQuest.IsOngoing)
				{
					return campaignBehavior._cachedQuest;
				}
				using (List<QuestBase>.Enumerator enumerator = Campaign.Current.QuestManager.Quests.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						LordNeedsGarrisonTroopsIssueQuestBehavior.LordNeedsGarrisonTroopsIssueQuest lordNeedsGarrisonTroopsIssueQuest;
						if ((lordNeedsGarrisonTroopsIssueQuest = enumerator.Current as LordNeedsGarrisonTroopsIssueQuestBehavior.LordNeedsGarrisonTroopsIssueQuest) != null)
						{
							campaignBehavior._cachedQuest = lordNeedsGarrisonTroopsIssueQuest;
							return campaignBehavior._cachedQuest;
						}
					}
				}
				return null;
			}
		}

		// Token: 0x060033C4 RID: 13252 RVA: 0x000D52BC File Offset: 0x000D34BC
		public override void RegisterEvents()
		{
			CampaignEvents.OnCheckForIssueEvent.AddNonSerializedListener(this, new Action<Hero>(this.OnCheckForIssue));
			CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnSessionLaunched));
		}

		// Token: 0x060033C5 RID: 13253 RVA: 0x000D52EC File Offset: 0x000D34EC
		private void OnSessionLaunched(CampaignGameStarter gameStarter)
		{
			string text = "{=FirEOQaI}Talk to the garrison commander";
			gameStarter.AddGameMenuOption("town", "talk_to_garrison_commander_town", text, new GameMenuOption.OnConditionDelegate(this.talk_to_garrison_commander_on_condition), new GameMenuOption.OnConsequenceDelegate(this.talk_to_garrison_commander_on_consequence), false, 0, false, null);
			gameStarter.AddGameMenuOption("town_guard", "talk_to_garrison_commander_town", text, new GameMenuOption.OnConditionDelegate(this.talk_to_garrison_commander_on_condition), new GameMenuOption.OnConsequenceDelegate(this.talk_to_garrison_commander_on_consequence), false, 2, false, null);
			gameStarter.AddGameMenuOption("castle_guard", "talk_to_garrison_commander_castle", text, new GameMenuOption.OnConditionDelegate(this.talk_to_garrison_commander_on_condition), new GameMenuOption.OnConsequenceDelegate(this.talk_to_garrison_commander_on_consequence), false, 2, false, null);
		}

		// Token: 0x060033C6 RID: 13254 RVA: 0x000D5388 File Offset: 0x000D3588
		private bool talk_to_garrison_commander_on_condition(MenuCallbackArgs args)
		{
			if (LordNeedsGarrisonTroopsIssueQuestBehavior.Instance != null)
			{
				if (Settlement.CurrentSettlement == LordNeedsGarrisonTroopsIssueQuestBehavior.Instance._settlement)
				{
					Town town = LordNeedsGarrisonTroopsIssueQuestBehavior.Instance._settlement.Town;
					if (((town != null) ? town.GarrisonParty : null) == null)
					{
						args.IsEnabled = false;
						args.Tooltip = new TextObject("{=JmoOJX4e}There is no one in the garrison to receive the troops requested. You should wait until someone arrives.", null);
					}
				}
				args.optionLeaveType = GameMenuOption.LeaveType.LeaveTroopsAndFlee;
				args.OptionQuestData = GameMenuOption.IssueQuestFlags.ActiveIssue;
				return Settlement.CurrentSettlement == LordNeedsGarrisonTroopsIssueQuestBehavior.Instance._settlement;
			}
			return false;
		}

		// Token: 0x060033C7 RID: 13255 RVA: 0x000D5404 File Offset: 0x000D3604
		private void talk_to_garrison_commander_on_consequence(MenuCallbackArgs args)
		{
			CharacterObject characterObject = LordNeedsGarrisonTroopsIssueQuestBehavior.Instance._settlement.OwnerClan.Culture.EliteBasicTroop;
			foreach (TroopRosterElement troopRosterElement in LordNeedsGarrisonTroopsIssueQuestBehavior.Instance._settlement.Town.GarrisonParty.MemberRoster.GetTroopRoster())
			{
				if (troopRosterElement.Character.IsInfantry && characterObject.Level < troopRosterElement.Character.Level)
				{
					characterObject = troopRosterElement.Character;
				}
			}
			LordNeedsGarrisonTroopsIssueQuestBehavior.Instance._selectedCharacterToTalk = characterObject;
			ConversationCharacterData conversationCharacterData = new ConversationCharacterData(CharacterObject.PlayerCharacter, PartyBase.MainParty, false, false, false, false, false, false);
			CharacterObject selectedCharacterToTalk = LordNeedsGarrisonTroopsIssueQuestBehavior.Instance._selectedCharacterToTalk;
			Town town = LordNeedsGarrisonTroopsIssueQuestBehavior.Instance._settlement.Town;
			CampaignMapConversation.OpenConversation(conversationCharacterData, new ConversationCharacterData(selectedCharacterToTalk, (town != null) ? town.GarrisonParty.Party : null, false, false, false, false, false, false));
		}

		// Token: 0x060033C8 RID: 13256 RVA: 0x000D5504 File Offset: 0x000D3704
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x060033C9 RID: 13257 RVA: 0x000D5508 File Offset: 0x000D3708
		private bool ConditionsHold(Hero issueGiver, out Settlement selectedSettlement)
		{
			selectedSettlement = null;
			if (issueGiver.IsLord && issueGiver.Clan.Leader == issueGiver && !issueGiver.IsMinorFactionHero && issueGiver.Clan != Clan.PlayerClan)
			{
				foreach (Settlement settlement in issueGiver.Clan.Settlements)
				{
					if (settlement.IsCastle)
					{
						MobileParty garrisonParty = settlement.Town.GarrisonParty;
						if (garrisonParty != null && garrisonParty.MemberRoster.TotalHealthyCount < 120)
						{
							selectedSettlement = settlement;
							break;
						}
					}
					if (settlement.IsTown)
					{
						MobileParty garrisonParty2 = settlement.Town.GarrisonParty;
						if (garrisonParty2 != null && garrisonParty2.MemberRoster.TotalHealthyCount < 150)
						{
							selectedSettlement = settlement;
							break;
						}
					}
				}
				return selectedSettlement != null;
			}
			return false;
		}

		// Token: 0x060033CA RID: 13258 RVA: 0x000D55FC File Offset: 0x000D37FC
		public void OnCheckForIssue(Hero hero)
		{
			Settlement settlement;
			if (this.ConditionsHold(hero, out settlement))
			{
				Campaign.Current.IssueManager.AddPotentialIssueData(hero, new PotentialIssueData(new PotentialIssueData.StartIssueDelegate(this.OnSelected), typeof(LordNeedsGarrisonTroopsIssueQuestBehavior.LordNeedsGarrisonTroopsIssue), IssueBase.IssueFrequency.Common, settlement));
				return;
			}
			Campaign.Current.IssueManager.AddPotentialIssueData(hero, new PotentialIssueData(typeof(LordNeedsGarrisonTroopsIssueQuestBehavior.LordNeedsGarrisonTroopsIssue), IssueBase.IssueFrequency.Common));
		}

		// Token: 0x060033CB RID: 13259 RVA: 0x000D5664 File Offset: 0x000D3864
		private IssueBase OnSelected(in PotentialIssueData pid, Hero issueOwner)
		{
			PotentialIssueData potentialIssueData = pid;
			return new LordNeedsGarrisonTroopsIssueQuestBehavior.LordNeedsGarrisonTroopsIssue(issueOwner, potentialIssueData.RelatedObject as Settlement);
		}

		// Token: 0x04000ED1 RID: 3793
		private const IssueBase.IssueFrequency LordNeedsGarrisonTroopsIssueFrequency = IssueBase.IssueFrequency.Common;

		// Token: 0x04000ED2 RID: 3794
		private LordNeedsGarrisonTroopsIssueQuestBehavior.LordNeedsGarrisonTroopsIssueQuest _cachedQuest;

		// Token: 0x02000733 RID: 1843
		public class LordNeedsGarrisonTroopsIssue : IssueBase
		{
			// Token: 0x06005C36 RID: 23606 RVA: 0x001ADC61 File Offset: 0x001ABE61
			internal static void AutoGeneratedStaticCollectObjectsLordNeedsGarrisonTroopsIssue(object o, List<object> collectedObjects)
			{
				((LordNeedsGarrisonTroopsIssueQuestBehavior.LordNeedsGarrisonTroopsIssue)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x06005C37 RID: 23607 RVA: 0x001ADC6F File Offset: 0x001ABE6F
			protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				base.AutoGeneratedInstanceCollectObjects(collectedObjects);
				collectedObjects.Add(this._settlement);
				collectedObjects.Add(this._neededTroopType);
			}

			// Token: 0x06005C38 RID: 23608 RVA: 0x001ADC90 File Offset: 0x001ABE90
			internal static object AutoGeneratedGetMemberValue_settlement(object o)
			{
				return ((LordNeedsGarrisonTroopsIssueQuestBehavior.LordNeedsGarrisonTroopsIssue)o)._settlement;
			}

			// Token: 0x06005C39 RID: 23609 RVA: 0x001ADC9D File Offset: 0x001ABE9D
			internal static object AutoGeneratedGetMemberValue_neededTroopType(object o)
			{
				return ((LordNeedsGarrisonTroopsIssueQuestBehavior.LordNeedsGarrisonTroopsIssue)o)._neededTroopType;
			}

			// Token: 0x1700123D RID: 4669
			// (get) Token: 0x06005C3A RID: 23610 RVA: 0x001ADCAA File Offset: 0x001ABEAA
			public override IssueBase.AlternativeSolutionScaleFlag AlternativeSolutionScaleFlags
			{
				get
				{
					return IssueBase.AlternativeSolutionScaleFlag.FailureRisk;
				}
			}

			// Token: 0x1700123E RID: 4670
			// (get) Token: 0x06005C3B RID: 23611 RVA: 0x001ADCAD File Offset: 0x001ABEAD
			private int NumberOfTroopToBeRecruited
			{
				get
				{
					return 3 + (int)(base.IssueDifficultyMultiplier * 18f);
				}
			}

			// Token: 0x1700123F RID: 4671
			// (get) Token: 0x06005C3C RID: 23612 RVA: 0x001ADCBE File Offset: 0x001ABEBE
			public override int AlternativeSolutionBaseNeededMenCount
			{
				get
				{
					return 5 + MathF.Ceiling(8f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x17001240 RID: 4672
			// (get) Token: 0x06005C3D RID: 23613 RVA: 0x001ADCD3 File Offset: 0x001ABED3
			protected override int AlternativeSolutionBaseDurationInDaysInternal
			{
				get
				{
					return 3 + MathF.Ceiling(4f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x17001241 RID: 4673
			// (get) Token: 0x06005C3E RID: 23614 RVA: 0x001ADCE8 File Offset: 0x001ABEE8
			protected override int RewardGold
			{
				get
				{
					int num = Campaign.Current.Models.PartyWageModel.GetTroopRecruitmentCost(this._neededTroopType, Hero.MainHero, false).RoundedResultNumber * this.NumberOfTroopToBeRecruited;
					return (int)(1500f + (float)num * 1.5f);
				}
			}

			// Token: 0x17001242 RID: 4674
			// (get) Token: 0x06005C3F RID: 23615 RVA: 0x001ADD34 File Offset: 0x001ABF34
			public override TextObject IssueBriefByIssueGiver
			{
				get
				{
					return new TextObject("{=ZuTvTGsh}These wars have taken a toll on my men. The bravest often fall first, they say, and fewer and fewer families are willing to let their sons join my banner. But the wars don't stop because I have problems.[if:convo_undecided_closed][ib:closed]", null);
				}
			}

			// Token: 0x17001243 RID: 4675
			// (get) Token: 0x06005C40 RID: 23616 RVA: 0x001ADD44 File Offset: 0x001ABF44
			public override TextObject IssueAcceptByPlayer
			{
				get
				{
					TextObject textObject = new TextObject("{=tTM6nPul}What can I do for you, {?ISSUE_OWNER.GENDER}madam{?}sir{\\?}?", null);
					StringHelpers.SetCharacterProperties("ISSUE_OWNER", base.IssueOwner.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x17001244 RID: 4676
			// (get) Token: 0x06005C41 RID: 23617 RVA: 0x001ADD78 File Offset: 0x001ABF78
			public override TextObject IssueQuestSolutionExplanationByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=driH06vI}I need more recruits in {SETTLEMENT}'s garrison. Since I'll be elsewhere... maybe you can recruit {NUMBER_OF_TROOP_TO_BE_RECRUITED} {TROOP_TYPE} and bring them to the garrison for me?[if:convo_undecided_open][ib:normal]", null);
					textObject.SetTextVariable("SETTLEMENT", this._settlement.Name);
					textObject.SetTextVariable("TROOP_TYPE", this._neededTroopType.EncyclopediaLinkWithName);
					textObject.SetTextVariable("NUMBER_OF_TROOP_TO_BE_RECRUITED", this.NumberOfTroopToBeRecruited);
					return textObject;
				}
			}

			// Token: 0x17001245 RID: 4677
			// (get) Token: 0x06005C42 RID: 23618 RVA: 0x001ADDD0 File Offset: 0x001ABFD0
			public override TextObject IssueAlternativeSolutionExplanationByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=igXcCqdo}One of your trusted companions who knows how to lead men can go around with {ALTERNATIVE_SOLUTION_MAN_COUNT} horsemen and pick some up. One way or the other I will pay {REWARD_GOLD}{GOLD_ICON} denars in return for your services. What do you say?[if:convo_thinking]", null);
					textObject.SetTextVariable("ALTERNATIVE_SOLUTION_MAN_COUNT", base.GetTotalAlternativeSolutionNeededMenCount());
					textObject.SetTextVariable("REWARD_GOLD", this.RewardGold);
					textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"6\">");
					return textObject;
				}
			}

			// Token: 0x17001246 RID: 4678
			// (get) Token: 0x06005C43 RID: 23619 RVA: 0x001ADE1D File Offset: 0x001AC01D
			public override TextObject IssueQuestSolutionAcceptByPlayer
			{
				get
				{
					TextObject textObject = new TextObject("{=YHSm72Ln}I'll find your recruits and bring them to {SETTLEMENT} garrison.", null);
					textObject.SetTextVariable("SETTLEMENT", this._settlement.Name);
					return textObject;
				}
			}

			// Token: 0x17001247 RID: 4679
			// (get) Token: 0x06005C44 RID: 23620 RVA: 0x001ADE44 File Offset: 0x001AC044
			public override TextObject IssueAlternativeSolutionAcceptByPlayer
			{
				get
				{
					TextObject textObject = new TextObject("{=JPclWyyr}My companion can handle it... So, {NUMBER_OF_TROOP_TO_BE_RECRUITED} {TROOP_TYPE} to {SETTLEMENT}.", null);
					textObject.SetTextVariable("SETTLEMENT", this._settlement.Name);
					textObject.SetTextVariable("TROOP_TYPE", this._neededTroopType.EncyclopediaLinkWithName);
					textObject.SetTextVariable("NUMBER_OF_TROOP_TO_BE_RECRUITED", this.NumberOfTroopToBeRecruited);
					return textObject;
				}
			}

			// Token: 0x17001248 RID: 4680
			// (get) Token: 0x06005C45 RID: 23621 RVA: 0x001ADE9C File Offset: 0x001AC09C
			public override TextObject IssueDiscussAlternativeSolution
			{
				get
				{
					TextObject textObject = new TextObject("{=lWrmxsYR}I haven't heard any news from {SETTLEMENT}, but I realize it might take some time for your men to deliver the recruits.", null);
					textObject.SetTextVariable("SETTLEMENT", this._settlement.Name);
					return textObject;
				}
			}

			// Token: 0x17001249 RID: 4681
			// (get) Token: 0x06005C46 RID: 23622 RVA: 0x001ADEC0 File Offset: 0x001AC0C0
			public override TextObject IssueAlternativeSolutionResponseByIssueGiver
			{
				get
				{
					return new TextObject("{=WUWzyzWI}Thank you. Your help will be remembered.", null);
				}
			}

			// Token: 0x1700124A RID: 4682
			// (get) Token: 0x06005C47 RID: 23623 RVA: 0x001ADED0 File Offset: 0x001AC0D0
			protected override TextObject AlternativeSolutionStartLog
			{
				get
				{
					TextObject textObject = new TextObject("{=M560TDza}{ISSUE_OWNER.LINK}, the {?ISSUE_OWNER.GENDER}lady{?}lord{\\?} of {QUEST_SETTLEMENT}, told you that {?ISSUE_OWNER.GENDER}she{?}he{\\?} needs more troops in {?ISSUE_OWNER.GENDER}her{?}his{\\?} garrison. {?ISSUE_OWNER.GENDER}She{?}He{\\?} is willing to pay {REWARD}{GOLD_ICON} for your services. You asked your companion to deploy {NUMBER_OF_TROOP_TO_BE_RECRUITED} {TROOP_TYPE} troops to {QUEST_SETTLEMENT}'s garrison.", null);
					StringHelpers.SetCharacterProperties("ISSUE_OWNER", base.IssueOwner.CharacterObject, textObject, false);
					textObject.SetTextVariable("QUEST_SETTLEMENT", this._settlement.EncyclopediaLinkWithName);
					textObject.SetTextVariable("TROOP_TYPE", this._neededTroopType.EncyclopediaLinkWithName);
					textObject.SetTextVariable("REWARD", this.RewardGold);
					textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"6\">");
					textObject.SetTextVariable("NUMBER_OF_TROOP_TO_BE_RECRUITED", this.NumberOfTroopToBeRecruited);
					return textObject;
				}
			}

			// Token: 0x1700124B RID: 4683
			// (get) Token: 0x06005C48 RID: 23624 RVA: 0x001ADF65 File Offset: 0x001AC165
			public override bool IsThereAlternativeSolution
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700124C RID: 4684
			// (get) Token: 0x06005C49 RID: 23625 RVA: 0x001ADF68 File Offset: 0x001AC168
			public override bool IsThereLordSolution
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700124D RID: 4685
			// (get) Token: 0x06005C4A RID: 23626 RVA: 0x001ADF6C File Offset: 0x001AC16C
			public override TextObject Title
			{
				get
				{
					TextObject textObject = new TextObject("{=g6Ra6LUY}{ISSUE_OWNER.NAME} Needs Garrison Troops in {SETTLEMENT}", null);
					StringHelpers.SetCharacterProperties("ISSUE_OWNER", base.IssueOwner.CharacterObject, textObject, false);
					textObject.SetTextVariable("SETTLEMENT", this._settlement.Name);
					return textObject;
				}
			}

			// Token: 0x1700124E RID: 4686
			// (get) Token: 0x06005C4B RID: 23627 RVA: 0x001ADFB8 File Offset: 0x001AC1B8
			public override TextObject Description
			{
				get
				{
					TextObject textObject = new TextObject("{=BOAaF6x5}{ISSUE_OWNER.NAME} asks for help to increase troop levels in {SETTLEMENT}", null);
					StringHelpers.SetCharacterProperties("ISSUE_OWNER", base.IssueOwner.CharacterObject, textObject, false);
					textObject.SetTextVariable("SETTLEMENT", this._settlement.Name);
					return textObject;
				}
			}

			// Token: 0x1700124F RID: 4687
			// (get) Token: 0x06005C4C RID: 23628 RVA: 0x001AE004 File Offset: 0x001AC204
			public override TextObject IssueAlternativeSolutionSuccessLog
			{
				get
				{
					TextObject textObject = new TextObject("{=sfFkYm0a}Your companion has successfully brought the troops {ISSUE_OWNER.LINK} requested. You received {REWARD}{GOLD_ICON}.", null);
					StringHelpers.SetCharacterProperties("ISSUE_OWNER", base.IssueOwner.CharacterObject, textObject, false);
					textObject.SetTextVariable("REWARD", this.RewardGold);
					textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"6\">");
					return textObject;
				}
			}

			// Token: 0x06005C4D RID: 23629 RVA: 0x001AE05C File Offset: 0x001AC25C
			public LordNeedsGarrisonTroopsIssue(Hero issueOwner, Settlement selectedSettlement)
				: base(issueOwner, CampaignTime.DaysFromNow(30f))
			{
				this._settlement = selectedSettlement;
				this._neededTroopType = CharacterHelper.GetTroopTree(base.IssueOwner.Culture.BasicTroop, 3f, 3f).GetRandomElementInefficiently<CharacterObject>();
			}

			// Token: 0x06005C4E RID: 23630 RVA: 0x001AE0AB File Offset: 0x001AC2AB
			protected override float GetIssueEffectAmountInternal(IssueEffect issueEffect)
			{
				if (issueEffect == DefaultIssueEffects.SettlementSecurity)
				{
					return -0.5f;
				}
				return 0f;
			}

			// Token: 0x06005C4F RID: 23631 RVA: 0x001AE0C0 File Offset: 0x001AC2C0
			public override ValueTuple<SkillObject, int> GetAlternativeSolutionSkill(Hero hero)
			{
				return new ValueTuple<SkillObject, int>((hero.GetSkillValue(DefaultSkills.Leadership) >= hero.GetSkillValue(DefaultSkills.Steward)) ? DefaultSkills.Leadership : DefaultSkills.Steward, 120);
			}

			// Token: 0x06005C50 RID: 23632 RVA: 0x001AE0ED File Offset: 0x001AC2ED
			public override bool DoTroopsSatisfyAlternativeSolution(TroopRoster troopRoster, out TextObject explanation)
			{
				return QuestHelper.CheckRosterForAlternativeSolution(troopRoster, base.GetTotalAlternativeSolutionNeededMenCount(), out explanation, 0, true);
			}

			// Token: 0x06005C51 RID: 23633 RVA: 0x001AE0FE File Offset: 0x001AC2FE
			public override bool IsTroopTypeNeededByAlternativeSolution(CharacterObject character)
			{
				return character.IsMounted;
			}

			// Token: 0x06005C52 RID: 23634 RVA: 0x001AE106 File Offset: 0x001AC306
			public override bool AlternativeSolutionCondition(out TextObject explanation)
			{
				return QuestHelper.CheckRosterForAlternativeSolution(MobileParty.MainParty.MemberRoster, base.GetTotalAlternativeSolutionNeededMenCount(), out explanation, 0, true);
			}

			// Token: 0x17001250 RID: 4688
			// (get) Token: 0x06005C53 RID: 23635 RVA: 0x001AE120 File Offset: 0x001AC320
			protected override int CompanionSkillRewardXP
			{
				get
				{
					return (int)(800f + 900f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x06005C54 RID: 23636 RVA: 0x001AE135 File Offset: 0x001AC335
			protected override void AlternativeSolutionEndWithSuccessConsequence()
			{
				GainRenownAction.Apply(Hero.MainHero, 1f, false);
				this.RelationshipChangeWithIssueOwner = 2;
			}

			// Token: 0x06005C55 RID: 23637 RVA: 0x001AE14E File Offset: 0x001AC34E
			public override IssueBase.IssueFrequency GetFrequency()
			{
				return IssueBase.IssueFrequency.Common;
			}

			// Token: 0x06005C56 RID: 23638 RVA: 0x001AE154 File Offset: 0x001AC354
			public override bool IssueStayAliveConditions()
			{
				bool flag = false;
				if (this._settlement.IsTown)
				{
					MobileParty garrisonParty = this._settlement.Town.GarrisonParty;
					flag = garrisonParty != null && garrisonParty.MemberRoster.TotalRegulars < 200;
				}
				else if (this._settlement.IsCastle)
				{
					MobileParty garrisonParty2 = this._settlement.Town.GarrisonParty;
					flag = garrisonParty2 != null && garrisonParty2.MemberRoster.TotalRegulars < 160;
				}
				return this._settlement.OwnerClan == base.IssueOwner.Clan && flag && !base.IssueOwner.IsDead && base.IssueOwner.Clan != Clan.PlayerClan;
			}

			// Token: 0x06005C57 RID: 23639 RVA: 0x001AE214 File Offset: 0x001AC414
			protected override bool CanPlayerTakeQuestConditions(Hero issueGiver, out IssueBase.PreconditionFlags flags, out Hero relationHero, out SkillObject skill, out int requiredGold)
			{
				skill = null;
				relationHero = null;
				requiredGold = 0;
				flags = IssueBase.PreconditionFlags.None;
				if (issueGiver.GetRelationWithPlayer() < -10f)
				{
					flags |= IssueBase.PreconditionFlags.Relation;
					relationHero = issueGiver;
				}
				if (Hero.MainHero.IsKingdomLeader)
				{
					flags |= IssueBase.PreconditionFlags.MainHeroIsKingdomLeader;
				}
				if (issueGiver.MapFaction.IsAtWarWith(Hero.MainHero.MapFaction))
				{
					flags |= IssueBase.PreconditionFlags.AtWar;
				}
				return flags == IssueBase.PreconditionFlags.None;
			}

			// Token: 0x06005C58 RID: 23640 RVA: 0x001AE27E File Offset: 0x001AC47E
			protected override void OnGameLoad()
			{
			}

			// Token: 0x06005C59 RID: 23641 RVA: 0x001AE280 File Offset: 0x001AC480
			protected override void HourlyTick()
			{
			}

			// Token: 0x06005C5A RID: 23642 RVA: 0x001AE282 File Offset: 0x001AC482
			protected override QuestBase GenerateIssueQuest(string questId)
			{
				return new LordNeedsGarrisonTroopsIssueQuestBehavior.LordNeedsGarrisonTroopsIssueQuest(questId, base.IssueOwner, CampaignTime.DaysFromNow(30f), this.RewardGold, this._settlement, this.NumberOfTroopToBeRecruited, this._neededTroopType);
			}

			// Token: 0x06005C5B RID: 23643 RVA: 0x001AE2B2 File Offset: 0x001AC4B2
			protected override void AlternativeSolutionEndWithFailureConsequence()
			{
				this.RelationshipChangeWithIssueOwner = -5;
			}

			// Token: 0x06005C5C RID: 23644 RVA: 0x001AE2BC File Offset: 0x001AC4BC
			protected override void CompleteIssueWithTimedOutConsequences()
			{
			}

			// Token: 0x04001D85 RID: 7557
			private const int QuestDurationInDays = 30;

			// Token: 0x04001D86 RID: 7558
			private const int CompanionRequiredSkillLevel = 120;

			// Token: 0x04001D87 RID: 7559
			[SaveableField(60)]
			private Settlement _settlement;

			// Token: 0x04001D88 RID: 7560
			[SaveableField(30)]
			private CharacterObject _neededTroopType;
		}

		// Token: 0x02000734 RID: 1844
		public class LordNeedsGarrisonTroopsIssueQuest : QuestBase
		{
			// Token: 0x06005C5D RID: 23645 RVA: 0x001AE2BE File Offset: 0x001AC4BE
			internal static void AutoGeneratedStaticCollectObjectsLordNeedsGarrisonTroopsIssueQuest(object o, List<object> collectedObjects)
			{
				((LordNeedsGarrisonTroopsIssueQuestBehavior.LordNeedsGarrisonTroopsIssueQuest)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x06005C5E RID: 23646 RVA: 0x001AE2CC File Offset: 0x001AC4CC
			protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				base.AutoGeneratedInstanceCollectObjects(collectedObjects);
				collectedObjects.Add(this._requestedTroopType);
				collectedObjects.Add(this._playerStartsQuestLog);
			}

			// Token: 0x06005C5F RID: 23647 RVA: 0x001AE2ED File Offset: 0x001AC4ED
			internal static object AutoGeneratedGetMemberValue_settlementStringID(object o)
			{
				return ((LordNeedsGarrisonTroopsIssueQuestBehavior.LordNeedsGarrisonTroopsIssueQuest)o)._settlementStringID;
			}

			// Token: 0x06005C60 RID: 23648 RVA: 0x001AE2FA File Offset: 0x001AC4FA
			internal static object AutoGeneratedGetMemberValue_requestedTroopAmount(object o)
			{
				return ((LordNeedsGarrisonTroopsIssueQuestBehavior.LordNeedsGarrisonTroopsIssueQuest)o)._requestedTroopAmount;
			}

			// Token: 0x06005C61 RID: 23649 RVA: 0x001AE30C File Offset: 0x001AC50C
			internal static object AutoGeneratedGetMemberValue_rewardGold(object o)
			{
				return ((LordNeedsGarrisonTroopsIssueQuestBehavior.LordNeedsGarrisonTroopsIssueQuest)o)._rewardGold;
			}

			// Token: 0x06005C62 RID: 23650 RVA: 0x001AE31E File Offset: 0x001AC51E
			internal static object AutoGeneratedGetMemberValue_requestedTroopType(object o)
			{
				return ((LordNeedsGarrisonTroopsIssueQuestBehavior.LordNeedsGarrisonTroopsIssueQuest)o)._requestedTroopType;
			}

			// Token: 0x06005C63 RID: 23651 RVA: 0x001AE32B File Offset: 0x001AC52B
			internal static object AutoGeneratedGetMemberValue_playerStartsQuestLog(object o)
			{
				return ((LordNeedsGarrisonTroopsIssueQuestBehavior.LordNeedsGarrisonTroopsIssueQuest)o)._playerStartsQuestLog;
			}

			// Token: 0x17001251 RID: 4689
			// (get) Token: 0x06005C64 RID: 23652 RVA: 0x001AE338 File Offset: 0x001AC538
			public override TextObject Title
			{
				get
				{
					TextObject textObject = new TextObject("{=g6Ra6LUY}{ISSUE_OWNER.NAME} Needs Garrison Troops in {SETTLEMENT}", null);
					StringHelpers.SetCharacterProperties("ISSUE_OWNER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("SETTLEMENT", this._settlement.Name);
					return textObject;
				}
			}

			// Token: 0x17001252 RID: 4690
			// (get) Token: 0x06005C65 RID: 23653 RVA: 0x001AE381 File Offset: 0x001AC581
			public override bool IsRemainingTimeHidden
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001253 RID: 4691
			// (get) Token: 0x06005C66 RID: 23654 RVA: 0x001AE384 File Offset: 0x001AC584
			private TextObject PlayerStartsQuestLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=FViaQrbV}{QUEST_GIVER.LINK}, the {?QUEST_GIVER.GENDER}lady{?}lord{\\?} of {QUEST_SETTLEMENT}, told you that {?QUEST_GIVER.GENDER}she{?}he{\\?} needs more troops in {?QUEST_GIVER.GENDER}her{?}his{\\?} garrison. {?QUEST_GIVER.GENDER}She{?}He{\\?} is willing to pay {REWARD}{GOLD_ICON} for your services. {?QUEST_GIVER.GENDER}She{?}He{\\?} asked you to deliver {NUMBER_OF_TROOP_TO_BE_RECRUITED} {TROOP_TYPE} troops to garrison commander in {QUEST_SETTLEMENT}.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("TROOP_TYPE", this._requestedTroopType.Name);
					textObject.SetTextVariable("REWARD", this._rewardGold);
					textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"6\">");
					textObject.SetTextVariable("NUMBER_OF_TROOP_TO_BE_RECRUITED", this._requestedTroopAmount);
					textObject.SetTextVariable("QUEST_SETTLEMENT", this._settlement.EncyclopediaLinkWithName);
					return textObject;
				}
			}

			// Token: 0x17001254 RID: 4692
			// (get) Token: 0x06005C67 RID: 23655 RVA: 0x001AE41C File Offset: 0x001AC61C
			private TextObject SuccessQuestLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=UEn466Y6}You have successfully brought the troops {QUEST_GIVER.LINK} requested. You received {REWARD} gold in return for your service.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("REWARD", this._rewardGold);
					return textObject;
				}
			}

			// Token: 0x17001255 RID: 4693
			// (get) Token: 0x06005C68 RID: 23656 RVA: 0x001AE460 File Offset: 0x001AC660
			private TextObject QuestGiverLostTheSettlementLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=zS68eOsl}{QUEST_GIVER.LINK} has lost {SETTLEMENT} and your agreement with {?QUEST_GIVER.GENDER}her{?}his{\\?} canceled.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("SETTLEMENT", this._settlement.EncyclopediaLinkWithName);
					return textObject;
				}
			}

			// Token: 0x17001256 RID: 4694
			// (get) Token: 0x06005C69 RID: 23657 RVA: 0x001AE4AC File Offset: 0x001AC6AC
			private TextObject QuestFailedWarDeclaredLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=JIWVeTMD}Your clan is now at war with {QUEST_GIVER.LINK}'s realm. Your agreement with {QUEST_GIVER.LINK} was canceled.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("SETTLEMENT", this._settlement.EncyclopediaLinkWithName);
					return textObject;
				}
			}

			// Token: 0x17001257 RID: 4695
			// (get) Token: 0x06005C6A RID: 23658 RVA: 0x001AE4F8 File Offset: 0x001AC6F8
			private TextObject PlayerDeclaredWarQuestLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=bqeWVVEE}Your actions have started a war with {QUEST_GIVER.LINK}'s faction. {?QUEST_GIVER.GENDER}She{?}He{\\?} cancels your agreement and the quest is a failure.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x17001258 RID: 4696
			// (get) Token: 0x06005C6B RID: 23659 RVA: 0x001AE52A File Offset: 0x001AC72A
			private TextObject TimeOutLogText
			{
				get
				{
					return new TextObject("{=cnaxgN5b}You have failed to bring the troops in time.", null);
				}
			}

			// Token: 0x06005C6C RID: 23660 RVA: 0x001AE538 File Offset: 0x001AC738
			public LordNeedsGarrisonTroopsIssueQuest(string questId, Hero giverHero, CampaignTime duration, int rewardGold, Settlement selectedSettlement, int requestedTroopAmount, CharacterObject requestedTroopType)
				: base(questId, giverHero, duration, rewardGold)
			{
				this._settlement = selectedSettlement;
				this._settlementStringID = selectedSettlement.StringId;
				this._requestedTroopAmount = requestedTroopAmount;
				this._collectedTroopAmount = 0;
				this._requestedTroopType = requestedTroopType;
				this._rewardGold = rewardGold;
				this.SetDialogs();
				base.AddTrackedObject(this._settlement);
				base.InitializeQuestOnCreation();
			}

			// Token: 0x06005C6D RID: 23661 RVA: 0x001AE59C File Offset: 0x001AC79C
			private bool DialogCondition()
			{
				return Hero.OneToOneConversationHero == base.QuestGiver;
			}

			// Token: 0x06005C6E RID: 23662 RVA: 0x001AE5AC File Offset: 0x001AC7AC
			protected override void SetDialogs()
			{
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetGarrisonCommanderDialogFlow(), this);
				this.OfferDialogFlow = DialogFlow.CreateDialogFlow("issue_classic_quest_start", 100).NpcLine(new TextObject("{=9iZg4vpz}Thank you. You will be rewarded when you are done.[if:convo_mocking_aristocratic]", null), null, null, null, null).Condition(new ConversationSentence.OnConditionDelegate(this.DialogCondition))
					.Consequence(new ConversationSentence.OnConsequenceDelegate(this.QuestAcceptedConsequences))
					.CloseDialog();
				this.DiscussDialogFlow = DialogFlow.CreateDialogFlow("quest_discuss", 100).NpcLine(new TextObject("{=o6BunhbE}Have you brought my troops?[if:convo_undecided_open]", null), null, null, null, null).Condition(new ConversationSentence.OnConditionDelegate(this.DialogCondition))
					.Consequence(delegate
					{
						Campaign.Current.ConversationManager.ConversationEndOneShot += MapEventHelper.OnConversationEnd;
					})
					.BeginPlayerOptions(null, false)
					.PlayerOption(new TextObject("{=eC4laxrj}I'm still out recruiting.", null), null, null, null)
					.NpcLine(new TextObject("{=TxxbCbUc}Good. I have faith in you...[if:convo_mocking_aristocratic]", null), null, null, null, null)
					.CloseDialog()
					.PlayerOption(new TextObject("{=DbraLcwM}I need more time to find proper men.", null), null, null, null)
					.NpcLine(new TextObject("{=Mw5bJ5Fb}Every day without a proper garrison is a day that we're vulnerable. Do hurry, if you can.[if:convo_normal]", null), null, null, null, null)
					.CloseDialog()
					.EndPlayerOptions();
			}

			// Token: 0x06005C6F RID: 23663 RVA: 0x001AE6DB File Offset: 0x001AC8DB
			private void QuestAcceptedConsequences()
			{
				base.StartQuest();
				this._playerStartsQuestLog = base.AddDiscreteLog(this.PlayerStartsQuestLogText, new TextObject("{=WIb9VvEM}Collected Troops", null), this._collectedTroopAmount, this._requestedTroopAmount, null, false);
			}

			// Token: 0x06005C70 RID: 23664 RVA: 0x001AE710 File Offset: 0x001AC910
			private DialogFlow GetGarrisonCommanderDialogFlow()
			{
				TextObject textObject = new TextObject("{=abda9slW}We were waiting for you, {?PLAYER.GENDER}madam{?}sir{\\?}. Have you brought the troops that our {?ISSUE_OWNER.GENDER}lady{?}lord{\\?} requested?", null);
				StringHelpers.SetCharacterProperties("ISSUE_OWNER", base.QuestGiver.CharacterObject, textObject, false);
				return DialogFlow.CreateDialogFlow("start", 300).NpcLine(textObject, null, null, null, null).Condition(() => CharacterObject.OneToOneConversationCharacter == this._selectedCharacterToTalk)
					.BeginPlayerOptions(null, false)
					.PlayerOption(new TextObject("{=ooHbl6JU}Here are your men.", null), null, null, null)
					.ClickableCondition(new ConversationSentence.OnClickableConditionDelegate(this.PlayerGiveTroopsToGarrisonCommanderCondition))
					.NpcLine(new TextObject("{=Ouy4sN5b}Thank you.[if:convo_mocking_aristocratic]", null), null, null, null, null)
					.Consequence(delegate
					{
						Campaign.Current.ConversationManager.ConversationEndOneShot += this.PlayerTransferredTroopsToGarrisonCommander;
					})
					.CloseDialog()
					.PlayerOption(new TextObject("{=G5tyQj6N}Not yet.", null), null, null, null)
					.NpcLine(new TextObject("{=yPOZd1wb}Very well. We'll keep waiting.[if:convo_normal]", null), null, null, null, null)
					.CloseDialog()
					.EndPlayerOptions();
			}

			// Token: 0x06005C71 RID: 23665 RVA: 0x001AE7F4 File Offset: 0x001AC9F4
			private void PlayerTransferredTroopsToGarrisonCommander()
			{
				using (List<TroopRosterElement>.Enumerator enumerator = MobileParty.MainParty.MemberRoster.GetTroopRoster().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Character == this._requestedTroopType)
						{
							MobileParty.MainParty.MemberRoster.AddToCounts(this._requestedTroopType, -this._requestedTroopAmount, false, 0, 0, true, -1);
							break;
						}
					}
				}
				base.AddLog(this.SuccessQuestLogText, false);
				this.RelationshipChangeWithQuestGiver = 2;
				GiveGoldAction.ApplyBetweenCharacters(null, Hero.MainHero, this._rewardGold, false);
				GainRenownAction.Apply(Hero.MainHero, 1f, false);
				base.CompleteQuestWithSuccess();
			}

			// Token: 0x06005C72 RID: 23666 RVA: 0x001AE8B8 File Offset: 0x001ACAB8
			private bool PlayerGiveTroopsToGarrisonCommanderCondition(out TextObject explanation)
			{
				int num = 0;
				foreach (TroopRosterElement troopRosterElement in MobileParty.MainParty.MemberRoster.GetTroopRoster())
				{
					if (troopRosterElement.Character == this._requestedTroopType)
					{
						num = troopRosterElement.Number;
						break;
					}
				}
				if (num < this._requestedTroopAmount)
				{
					explanation = new TextObject("{=VFO2aQ4l}You don't have enough men.", null);
					return false;
				}
				explanation = null;
				return true;
			}

			// Token: 0x06005C73 RID: 23667 RVA: 0x001AE944 File Offset: 0x001ACB44
			protected override void InitializeQuestOnGameLoad()
			{
				this._settlement = Settlement.Find(this._settlementStringID);
				this.CalculateTroopAmount();
				this.SetDialogs();
			}

			// Token: 0x06005C74 RID: 23668 RVA: 0x001AE964 File Offset: 0x001ACB64
			protected override void RegisterEvents()
			{
				CampaignEvents.OnSettlementOwnerChangedEvent.AddNonSerializedListener(this, new Action<Settlement, bool, Hero, Hero, Hero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail>(this.OnSettlementOwnerChanged));
				CampaignEvents.WarDeclared.AddNonSerializedListener(this, new Action<IFaction, IFaction, DeclareWarAction.DeclareWarDetail>(this.OnWarDeclared));
				CampaignEvents.OnClanChangedKingdomEvent.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, ChangeKingdomAction.ChangeKingdomActionDetail, bool>(this.OnClanChangedKingdom));
				CampaignEvents.MapEventStarted.AddNonSerializedListener(this, new Action<MapEvent, PartyBase, PartyBase>(this.OnMapEventStarted));
				CampaignEvents.OnPartySizeChangedEvent.AddNonSerializedListener(this, new Action<PartyBase>(this.OnPartySizeChanged));
			}

			// Token: 0x06005C75 RID: 23669 RVA: 0x001AE9E4 File Offset: 0x001ACBE4
			private void OnPartySizeChanged(PartyBase party)
			{
				if (party.IsMobile && party.MobileParty.IsMainParty)
				{
					this.CalculateTroopAmount();
					this._collectedTroopAmount = MBMath.ClampInt(this._collectedTroopAmount, 0, this._requestedTroopAmount);
					this._playerStartsQuestLog.UpdateCurrentProgress(this._collectedTroopAmount);
				}
			}

			// Token: 0x06005C76 RID: 23670 RVA: 0x001AEA35 File Offset: 0x001ACC35
			private void OnMapEventStarted(MapEvent mapEvent, PartyBase attackerParty, PartyBase defenderParty)
			{
				if (QuestHelper.CheckMinorMajorCoercion(this, mapEvent, attackerParty))
				{
					QuestHelper.ApplyGenericMinorMajorCoercionConsequences(this, mapEvent);
				}
			}

			// Token: 0x06005C77 RID: 23671 RVA: 0x001AEA48 File Offset: 0x001ACC48
			private void OnSettlementOwnerChanged(Settlement settlement, bool openToClaim, Hero newOwner, Hero oldOwner, Hero capturerHero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail detail)
			{
				if (settlement == this._settlement && this._settlement.OwnerClan != base.QuestGiver.Clan)
				{
					base.AddLog(this.QuestGiverLostTheSettlementLogText, false);
					base.CompleteQuestWithCancel(null);
				}
			}

			// Token: 0x06005C78 RID: 23672 RVA: 0x001AEA80 File Offset: 0x001ACC80
			protected override void HourlyTick()
			{
				if (base.IsOngoing)
				{
					this.CalculateTroopAmount();
					this._collectedTroopAmount = MBMath.ClampInt(this._collectedTroopAmount, 0, this._requestedTroopAmount);
					this._playerStartsQuestLog.UpdateCurrentProgress(this._collectedTroopAmount);
				}
			}

			// Token: 0x06005C79 RID: 23673 RVA: 0x001AEABC File Offset: 0x001ACCBC
			private void CalculateTroopAmount()
			{
				foreach (TroopRosterElement troopRosterElement in MobileParty.MainParty.MemberRoster.GetTroopRoster())
				{
					if (troopRosterElement.Character == this._requestedTroopType)
					{
						this._collectedTroopAmount = MobileParty.MainParty.MemberRoster.GetTroopCount(troopRosterElement.Character);
						break;
					}
				}
			}

			// Token: 0x06005C7A RID: 23674 RVA: 0x001AEB3C File Offset: 0x001ACD3C
			private void OnClanChangedKingdom(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail detail, bool showNotification = true)
			{
				if (base.QuestGiver.MapFaction.IsAtWarWith(Hero.MainHero.MapFaction))
				{
					base.CompleteQuestWithCancel(this.QuestFailedWarDeclaredLogText);
				}
			}

			// Token: 0x06005C7B RID: 23675 RVA: 0x001AEB66 File Offset: 0x001ACD66
			private void OnWarDeclared(IFaction faction1, IFaction faction2, DeclareWarAction.DeclareWarDetail detail)
			{
				QuestHelper.CheckWarDeclarationAndFailOrCancelTheQuest(this, faction1, faction2, detail, this.PlayerDeclaredWarQuestLogText, this.QuestFailedWarDeclaredLogText, false);
			}

			// Token: 0x06005C7C RID: 23676 RVA: 0x001AEB7E File Offset: 0x001ACD7E
			protected override void OnTimedOut()
			{
				base.AddLog(this.TimeOutLogText, false);
				this.RelationshipChangeWithQuestGiver = -5;
			}

			// Token: 0x04001D89 RID: 7561
			internal Settlement _settlement;

			// Token: 0x04001D8A RID: 7562
			[SaveableField(10)]
			private string _settlementStringID;

			// Token: 0x04001D8B RID: 7563
			private int _collectedTroopAmount;

			// Token: 0x04001D8C RID: 7564
			[SaveableField(20)]
			private int _requestedTroopAmount;

			// Token: 0x04001D8D RID: 7565
			[SaveableField(30)]
			private int _rewardGold;

			// Token: 0x04001D8E RID: 7566
			[SaveableField(40)]
			private CharacterObject _requestedTroopType;

			// Token: 0x04001D8F RID: 7567
			internal CharacterObject _selectedCharacterToTalk;

			// Token: 0x04001D90 RID: 7568
			[SaveableField(50)]
			private JournalLog _playerStartsQuestLog;
		}

		// Token: 0x02000735 RID: 1845
		public class LordNeedsGarrisonTroopsIssueQuestTypeDefiner : SaveableTypeDefiner
		{
			// Token: 0x06005C7F RID: 23679 RVA: 0x001AEBC2 File Offset: 0x001ACDC2
			public LordNeedsGarrisonTroopsIssueQuestTypeDefiner()
				: base(5080000)
			{
			}

			// Token: 0x06005C80 RID: 23680 RVA: 0x001AEBCF File Offset: 0x001ACDCF
			protected override void DefineClassTypes()
			{
				base.AddClassDefinition(typeof(LordNeedsGarrisonTroopsIssueQuestBehavior.LordNeedsGarrisonTroopsIssue), 1, null);
				base.AddClassDefinition(typeof(LordNeedsGarrisonTroopsIssueQuestBehavior.LordNeedsGarrisonTroopsIssueQuest), 2, null);
			}
		}
	}
}
