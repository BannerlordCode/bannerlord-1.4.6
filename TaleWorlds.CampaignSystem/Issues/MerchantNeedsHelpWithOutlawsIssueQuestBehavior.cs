using System;
using System.Collections.Generic;
using Helpers;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Conversation;
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
	// Token: 0x02000374 RID: 884
	public class MerchantNeedsHelpWithOutlawsIssueQuestBehavior : CampaignBehaviorBase
	{
		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x060033F3 RID: 13299 RVA: 0x000D617E File Offset: 0x000D437E
		private static float ValidBanditPartyDistance
		{
			get
			{
				return MerchantNeedsHelpWithOutlawsIssueQuestBehavior.NeededHideoutDistanceToSpawnTheQuest * 0.75f;
			}
		}

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x060033F4 RID: 13300 RVA: 0x000D618B File Offset: 0x000D438B
		private static float NeededHideoutDistanceToSpawnTheQuest
		{
			get
			{
				return Campaign.Current.EstimatedAverageBanditPartySpeed * (float)CampaignTime.HoursInDay;
			}
		}

		// Token: 0x060033F5 RID: 13301 RVA: 0x000D61A0 File Offset: 0x000D43A0
		public override void RegisterEvents()
		{
			CampaignEvents.OnCheckForIssueEvent.AddNonSerializedListener(this, new Action<Hero>(this.OnCheckForIssue));
			CampaignEvents.OnNewGameCreatedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnNewGameCreated));
			CampaignEvents.OnGameEarlyLoadedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnGameEarlyLoaded));
		}

		// Token: 0x060033F6 RID: 13302 RVA: 0x000D61F2 File Offset: 0x000D43F2
		private void OnGameEarlyLoaded(CampaignGameStarter obj)
		{
			this.InitializeCache();
		}

		// Token: 0x060033F7 RID: 13303 RVA: 0x000D61FA File Offset: 0x000D43FA
		private void OnNewGameCreated(CampaignGameStarter obj)
		{
			this.InitializeCache();
		}

		// Token: 0x060033F8 RID: 13304 RVA: 0x000D6204 File Offset: 0x000D4404
		private void InitializeCache()
		{
			foreach (Settlement settlement in Settlement.All)
			{
				if (settlement.IsTown || settlement.IsVillage)
				{
					foreach (Hideout hideout in Hideout.All)
					{
						if (Campaign.Current.Models.MapDistanceModel.GetDistance(hideout.Settlement, settlement, false, false, MobileParty.NavigationType.Default) < Campaign.Current.GetAverageDistanceBetweenClosestTwoTownsWithNavigationType(MobileParty.NavigationType.Default) * 1.25f)
						{
							if (!this._closestHideoutsToSettlements.ContainsKey(settlement))
							{
								this._closestHideoutsToSettlements.Add(settlement, new List<Hideout> { hideout });
							}
							else
							{
								this._closestHideoutsToSettlements[settlement].Add(hideout);
							}
						}
					}
				}
			}
		}

		// Token: 0x060033F9 RID: 13305 RVA: 0x000D6310 File Offset: 0x000D4510
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x060033FA RID: 13306 RVA: 0x000D6314 File Offset: 0x000D4514
		private bool ConditionsHold(Hero issueGiver, out Hideout hideout)
		{
			hideout = null;
			List<Hideout> list;
			if ((issueGiver.IsMerchant || issueGiver.IsRuralNotable) && this._closestHideoutsToSettlements.TryGetValue(issueGiver.CurrentSettlement, out list))
			{
				foreach (Hideout hideout2 in list)
				{
					if (hideout2.IsInfested && !hideout2.Settlement.IsSettlementBusy(this))
					{
						hideout = hideout2;
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060033FB RID: 13307 RVA: 0x000D63A4 File Offset: 0x000D45A4
		public void OnCheckForIssue(Hero hero)
		{
			Hideout hideout;
			if (this.ConditionsHold(hero, out hideout))
			{
				Campaign.Current.IssueManager.AddPotentialIssueData(hero, new PotentialIssueData(new PotentialIssueData.StartIssueDelegate(this.OnSelected), typeof(MerchantNeedsHelpWithOutlawsIssueQuestBehavior.MerchantNeedsHelpWithOutlawsIssue), IssueBase.IssueFrequency.VeryCommon, hideout));
				return;
			}
			Campaign.Current.IssueManager.AddPotentialIssueData(hero, new PotentialIssueData(typeof(MerchantNeedsHelpWithOutlawsIssueQuestBehavior.MerchantNeedsHelpWithOutlawsIssue), IssueBase.IssueFrequency.VeryCommon));
		}

		// Token: 0x060033FC RID: 13308 RVA: 0x000D640C File Offset: 0x000D460C
		private IssueBase OnSelected(in PotentialIssueData pid, Hero issueOwner)
		{
			PotentialIssueData potentialIssueData = pid;
			Hideout hideout = potentialIssueData.RelatedObject as Hideout;
			return new MerchantNeedsHelpWithOutlawsIssueQuestBehavior.MerchantNeedsHelpWithOutlawsIssue(issueOwner, hideout);
		}

		// Token: 0x04000EDD RID: 3805
		private const IssueBase.IssueFrequency MerchantNeedsHelpWithOutlawsIssueFrequency = IssueBase.IssueFrequency.VeryCommon;

		// Token: 0x04000EDE RID: 3806
		private readonly Dictionary<Settlement, List<Hideout>> _closestHideoutsToSettlements = new Dictionary<Settlement, List<Hideout>>();

		// Token: 0x02000744 RID: 1860
		public class MerchantNeedsHelpWithOutlawsIssue : IssueBase
		{
			// Token: 0x06005E0B RID: 24075 RVA: 0x001B55B4 File Offset: 0x001B37B4
			internal static void AutoGeneratedStaticCollectObjectsMerchantNeedsHelpWithOutlawsIssue(object o, List<object> collectedObjects)
			{
				((MerchantNeedsHelpWithOutlawsIssueQuestBehavior.MerchantNeedsHelpWithOutlawsIssue)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x06005E0C RID: 24076 RVA: 0x001B55C2 File Offset: 0x001B37C2
			protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				base.AutoGeneratedInstanceCollectObjects(collectedObjects);
				collectedObjects.Add(this.RelatedHideout);
			}

			// Token: 0x06005E0D RID: 24077 RVA: 0x001B55D7 File Offset: 0x001B37D7
			internal static object AutoGeneratedGetMemberValueRelatedHideout(object o)
			{
				return ((MerchantNeedsHelpWithOutlawsIssueQuestBehavior.MerchantNeedsHelpWithOutlawsIssue)o).RelatedHideout;
			}

			// Token: 0x170012C5 RID: 4805
			// (get) Token: 0x06005E0E RID: 24078 RVA: 0x001B55E4 File Offset: 0x001B37E4
			public override IssueBase.AlternativeSolutionScaleFlag AlternativeSolutionScaleFlags
			{
				get
				{
					return IssueBase.AlternativeSolutionScaleFlag.Casualties | IssueBase.AlternativeSolutionScaleFlag.FailureRisk;
				}
			}

			// Token: 0x170012C6 RID: 4806
			// (get) Token: 0x06005E0F RID: 24079 RVA: 0x001B55E8 File Offset: 0x001B37E8
			private int TotalPartyCount
			{
				get
				{
					return (int)(2f + 6f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x170012C7 RID: 4807
			// (get) Token: 0x06005E10 RID: 24080 RVA: 0x001B55FD File Offset: 0x001B37FD
			public override int AlternativeSolutionBaseNeededMenCount
			{
				get
				{
					return 8 + MathF.Ceiling(11f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x170012C8 RID: 4808
			// (get) Token: 0x06005E11 RID: 24081 RVA: 0x001B5612 File Offset: 0x001B3812
			protected override int AlternativeSolutionBaseDurationInDaysInternal
			{
				get
				{
					return 5 + MathF.Ceiling(7f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x170012C9 RID: 4809
			// (get) Token: 0x06005E12 RID: 24082 RVA: 0x001B5627 File Offset: 0x001B3827
			protected override int RewardGold
			{
				get
				{
					return (int)(400f + 1500f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x170012CA RID: 4810
			// (get) Token: 0x06005E13 RID: 24083 RVA: 0x001B563C File Offset: 0x001B383C
			public override TextObject IssueBriefByIssueGiver
			{
				get
				{
					return new TextObject("{=ib6ltlM0}Yes... We've always had trouble with bandits, but recently we've had a lot more than our share. The hills outside of town are infested. A lot of us are afraid to take their goods to market. Some have been murdered. People tell me, 'I'm getting so desperate, maybe I'll turn bandit myself.' It's bad...[ib:demure2][if:convo_dismayed]", null);
				}
			}

			// Token: 0x170012CB RID: 4811
			// (get) Token: 0x06005E14 RID: 24084 RVA: 0x001B5649 File Offset: 0x001B3849
			public override TextObject IssueAcceptByPlayer
			{
				get
				{
					return new TextObject("{=qNxdWLFY}So you want me to hunt them down?", null);
				}
			}

			// Token: 0x170012CC RID: 4812
			// (get) Token: 0x06005E15 RID: 24085 RVA: 0x001B5658 File Offset: 0x001B3858
			public override TextObject IssueQuestSolutionExplanationByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=DlRMT7XD}Well, {?PLAYER.GENDER}my lady{?}sir{\\?}, you'll never get all those outlaws,[if:convo_thinking] but if word gets around that you took down some of the most vicious ones - let's say {TOTAL_COUNT} bands of brigands - robbing us wouldn't seem so lucrative. Maybe the rest would go bother someone else... Do you think you can help us?", null);
					StringHelpers.SetCharacterProperties("PLAYER", CharacterObject.PlayerCharacter, textObject, false);
					textObject.SetTextVariable("TOTAL_COUNT", this.TotalPartyCount);
					return textObject;
				}
			}

			// Token: 0x170012CD RID: 4813
			// (get) Token: 0x06005E16 RID: 24086 RVA: 0x001B5696 File Offset: 0x001B3896
			public override TextObject IssueAlternativeSolutionExplanationByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=5RjvnQ3d}I bet even a party of {ALTERNATIVE_COUNT} properly trained men accompanied by one of your lieutenants can handle any band they find. Give them {TOTAL_DAYS} days, say... That will make a difference.[if:convo_undecided_open]", null);
					textObject.SetTextVariable("ALTERNATIVE_COUNT", base.GetTotalAlternativeSolutionNeededMenCount());
					textObject.SetTextVariable("TOTAL_DAYS", base.GetTotalAlternativeSolutionDurationInDays());
					return textObject;
				}
			}

			// Token: 0x170012CE RID: 4814
			// (get) Token: 0x06005E17 RID: 24087 RVA: 0x001B56C7 File Offset: 0x001B38C7
			public override TextObject IssuePlayerResponseAfterAlternativeExplanation
			{
				get
				{
					return new TextObject("{=BPfuSkCl}That depends. How many men do you think are required to get the job done?", null);
				}
			}

			// Token: 0x170012CF RID: 4815
			// (get) Token: 0x06005E18 RID: 24088 RVA: 0x001B56D4 File Offset: 0x001B38D4
			public override TextObject IssueQuestSolutionAcceptByPlayer
			{
				get
				{
					TextObject textObject = new TextObject("{=2ApU6iCB}I'll hunt down {TOTAL_COUNT} bands of brigands for you.", null);
					textObject.SetTextVariable("TOTAL_COUNT", this.TotalPartyCount);
					return textObject;
				}
			}

			// Token: 0x170012D0 RID: 4816
			// (get) Token: 0x06005E19 RID: 24089 RVA: 0x001B56F3 File Offset: 0x001B38F3
			public override TextObject IssueAlternativeSolutionAcceptByPlayer
			{
				get
				{
					TextObject textObject = new TextObject("{=DLbFbYkR}I will have one of my companions and {ALTERNATIVE_COUNT} of my men patrol the area for {TOTAL_DAYS} days.", null);
					textObject.SetTextVariable("ALTERNATIVE_COUNT", base.GetTotalAlternativeSolutionNeededMenCount());
					textObject.SetTextVariable("TOTAL_DAYS", base.GetTotalAlternativeSolutionDurationInDays());
					return textObject;
				}
			}

			// Token: 0x170012D1 RID: 4817
			// (get) Token: 0x06005E1A RID: 24090 RVA: 0x001B5724 File Offset: 0x001B3924
			public override TextObject IssueDiscussAlternativeSolution
			{
				get
				{
					return new TextObject("{=PexmGuOd}{?PLAYER.GENDER}Madam{?}Sir{\\?}, I am happy to tell that the men you left are patrolling, and already we feel safer. Thank you again.[ib:demure][if:convo_grateful]", null);
				}
			}

			// Token: 0x170012D2 RID: 4818
			// (get) Token: 0x06005E1B RID: 24091 RVA: 0x001B5734 File Offset: 0x001B3934
			public override TextObject IssueAlternativeSolutionResponseByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=FYfZFve3}Thank you, {?PLAYER.GENDER}my lady{?}my lord{\\?}. Hopefully, we can travel safely again.", null);
					StringHelpers.SetCharacterProperties("PLAYER", CharacterObject.PlayerCharacter, textObject, false);
					return textObject;
				}
			}

			// Token: 0x170012D3 RID: 4819
			// (get) Token: 0x06005E1C RID: 24092 RVA: 0x001B5760 File Offset: 0x001B3960
			public override bool IsThereAlternativeSolution
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170012D4 RID: 4820
			// (get) Token: 0x06005E1D RID: 24093 RVA: 0x001B5763 File Offset: 0x001B3963
			public override bool IsThereLordSolution
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170012D5 RID: 4821
			// (get) Token: 0x06005E1E RID: 24094 RVA: 0x001B5766 File Offset: 0x001B3966
			protected override int CompanionSkillRewardXP
			{
				get
				{
					return (int)(600f + 800f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x170012D6 RID: 4822
			// (get) Token: 0x06005E1F RID: 24095 RVA: 0x001B577C File Offset: 0x001B397C
			protected override TextObject AlternativeSolutionStartLog
			{
				get
				{
					TextObject textObject = new TextObject("{=Bdt41knf}You have accepted {QUEST_GIVER.LINK}'s request to find at least {TOTAL_COUNT} different parties of brigands around {QUEST_SETTLEMENT} and sent {COMPANION.LINK} and with {?COMPANION.GENDER}her{?}his{\\?} {ALTERNATIVE_COUNT} of your men to deal with them. They should return with the reward of {GOLD_AMOUNT}{GOLD_ICON} denars as promised by {QUEST_GIVER.LINK} after dealing with them in {RETURN_DAYS} days.", null);
					textObject.SetTextVariable("TOTAL_COUNT", this.TotalPartyCount);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.IssueOwner.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("COMPANION", base.AlternativeSolutionHero.CharacterObject, textObject, false);
					textObject.SetTextVariable("QUEST_SETTLEMENT", base.IssueOwner.CurrentSettlement.EncyclopediaLinkWithName);
					textObject.SetTextVariable("ALTERNATIVE_COUNT", this.AlternativeSolutionSentTroops.TotalManCount - 1);
					textObject.SetTextVariable("GOLD_AMOUNT", this.RewardGold);
					textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"6\">");
					textObject.SetTextVariable("RETURN_DAYS", base.GetTotalAlternativeSolutionDurationInDays());
					return textObject;
				}
			}

			// Token: 0x170012D7 RID: 4823
			// (get) Token: 0x06005E20 RID: 24096 RVA: 0x001B5844 File Offset: 0x001B3A44
			public override TextObject Title
			{
				get
				{
					TextObject textObject = new TextObject("{=ABmCO23x}{QUEST_GIVER.NAME} Needs Help With Brigands", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.IssueOwner.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x170012D8 RID: 4824
			// (get) Token: 0x06005E21 RID: 24097 RVA: 0x001B5876 File Offset: 0x001B3A76
			public override TextObject Description
			{
				get
				{
					return new TextObject("{=sAobCa9U}Brigands are disturbing travelers outside the town. Someone needs to hunt them down.", null);
				}
			}

			// Token: 0x06005E22 RID: 24098 RVA: 0x001B5883 File Offset: 0x001B3A83
			public MerchantNeedsHelpWithOutlawsIssue(Hero issueOwner, Hideout relatedHideout)
				: base(issueOwner, CampaignTime.DaysFromNow(15f))
			{
				this.RelatedHideout = relatedHideout;
			}

			// Token: 0x06005E23 RID: 24099 RVA: 0x001B589D File Offset: 0x001B3A9D
			protected override float GetIssueEffectAmountInternal(IssueEffect issueEffect)
			{
				if (issueEffect == DefaultIssueEffects.SettlementProsperity)
				{
					return -0.2f;
				}
				if (issueEffect == DefaultIssueEffects.IssueOwnerPower)
				{
					return -0.1f;
				}
				if (issueEffect == DefaultIssueEffects.SettlementSecurity)
				{
					return -1f;
				}
				return 0f;
			}

			// Token: 0x06005E24 RID: 24100 RVA: 0x001B58CE File Offset: 0x001B3ACE
			public override ValueTuple<SkillObject, int> GetAlternativeSolutionSkill(Hero hero)
			{
				return new ValueTuple<SkillObject, int>((hero.GetSkillValue(DefaultSkills.Tactics) >= hero.GetSkillValue(DefaultSkills.Scouting)) ? DefaultSkills.Tactics : DefaultSkills.Scouting, 120);
			}

			// Token: 0x06005E25 RID: 24101 RVA: 0x001B58FB File Offset: 0x001B3AFB
			public override bool DoTroopsSatisfyAlternativeSolution(TroopRoster troopRoster, out TextObject explanation)
			{
				return QuestHelper.CheckRosterForAlternativeSolution(troopRoster, base.GetTotalAlternativeSolutionNeededMenCount(), out explanation, 2, false);
			}

			// Token: 0x06005E26 RID: 24102 RVA: 0x001B590C File Offset: 0x001B3B0C
			public override bool AlternativeSolutionCondition(out TextObject explanation)
			{
				return QuestHelper.CheckRosterForAlternativeSolution(MobileParty.MainParty.MemberRoster, base.GetTotalAlternativeSolutionNeededMenCount(), out explanation, 2, false);
			}

			// Token: 0x06005E27 RID: 24103 RVA: 0x001B5926 File Offset: 0x001B3B26
			public override bool IsTroopTypeNeededByAlternativeSolution(CharacterObject character)
			{
				return character.Tier >= 2;
			}

			// Token: 0x06005E28 RID: 24104 RVA: 0x001B5934 File Offset: 0x001B3B34
			protected override void AlternativeSolutionEndWithSuccessConsequence()
			{
				this.RelationshipChangeWithIssueOwner = 3;
				if (base.IssueOwner.CurrentSettlement.IsVillage && base.IssueOwner.CurrentSettlement.Village.TradeBound != null)
				{
					base.IssueOwner.CurrentSettlement.Village.Bound.Town.Security += 5f;
					base.IssueOwner.CurrentSettlement.Village.Bound.Town.Prosperity += 5f;
				}
				else if (base.IssueOwner.CurrentSettlement.IsTown)
				{
					base.IssueOwner.CurrentSettlement.Town.Security += 5f;
					base.IssueOwner.CurrentSettlement.Town.Prosperity += 5f;
				}
				Hero.MainHero.Clan.AddRenown(1f, true);
			}

			// Token: 0x06005E29 RID: 24105 RVA: 0x001B5A34 File Offset: 0x001B3C34
			protected override void AlternativeSolutionEndWithFailureConsequence()
			{
				if (base.IssueOwner.CurrentSettlement.IsVillage)
				{
					base.IssueOwner.CurrentSettlement.Village.Bound.Town.Prosperity -= 10f;
				}
				else if (base.IssueOwner.CurrentSettlement.IsTown)
				{
					base.IssueOwner.CurrentSettlement.Town.Prosperity -= 10f;
				}
				this.RelationshipChangeWithIssueOwner = -5;
			}

			// Token: 0x06005E2A RID: 24106 RVA: 0x001B5ABB File Offset: 0x001B3CBB
			public override IssueBase.IssueFrequency GetFrequency()
			{
				return IssueBase.IssueFrequency.VeryCommon;
			}

			// Token: 0x06005E2B RID: 24107 RVA: 0x001B5AC0 File Offset: 0x001B3CC0
			protected override bool CanPlayerTakeQuestConditions(Hero issueGiver, out IssueBase.PreconditionFlags flag, out Hero relationHero, out SkillObject skill, out int requiredGold)
			{
				flag = IssueBase.PreconditionFlags.None;
				relationHero = null;
				requiredGold = 0;
				skill = null;
				if (issueGiver.GetRelationWithPlayer() < -10f)
				{
					flag |= IssueBase.PreconditionFlags.Relation;
					relationHero = issueGiver;
				}
				if (MobileParty.MainParty.MemberRoster.TotalHealthyCount < 5)
				{
					flag |= IssueBase.PreconditionFlags.NotEnoughTroops;
				}
				if (issueGiver.MapFaction.IsAtWarWith(Hero.MainHero.MapFaction))
				{
					flag |= IssueBase.PreconditionFlags.AtWar;
				}
				return flag == IssueBase.PreconditionFlags.None;
			}

			// Token: 0x06005E2C RID: 24108 RVA: 0x001B5B30 File Offset: 0x001B3D30
			public override bool IssueStayAliveConditions()
			{
				return !base.IssueOwner.CurrentSettlement.IsRaided && !base.IssueOwner.CurrentSettlement.IsUnderRaid && this.RelatedHideout != null && this.RelatedHideout.IsInfested;
			}

			// Token: 0x06005E2D RID: 24109 RVA: 0x001B5B6B File Offset: 0x001B3D6B
			protected override void OnGameLoad()
			{
				if (MBSaveLoad.IsUpdatingGameVersion && MBSaveLoad.LastLoadedGameVersion < ApplicationVersion.FromString("v1.2.9", 0) && this.RelatedHideout == null)
				{
					base.CompleteIssueWithCancel(null);
				}
			}

			// Token: 0x06005E2E RID: 24110 RVA: 0x001B5B9A File Offset: 0x001B3D9A
			public override void IsSettlementBusy(Settlement settlement, object asker, ref int priority)
			{
				if (asker != this && settlement == this.RelatedHideout.Settlement)
				{
					priority = Math.Max(priority, 100);
				}
			}

			// Token: 0x06005E2F RID: 24111 RVA: 0x001B5BB9 File Offset: 0x001B3DB9
			protected override void HourlyTick()
			{
			}

			// Token: 0x06005E30 RID: 24112 RVA: 0x001B5BBB File Offset: 0x001B3DBB
			protected override QuestBase GenerateIssueQuest(string questId)
			{
				return new MerchantNeedsHelpWithOutlawsIssueQuestBehavior.MerchantNeedsHelpWithOutlawsIssueQuest(questId, base.IssueOwner, CampaignTime.DaysFromNow(20f), this.RewardGold, this.TotalPartyCount, this.RelatedHideout);
			}

			// Token: 0x06005E31 RID: 24113 RVA: 0x001B5BE5 File Offset: 0x001B3DE5
			protected override void CompleteIssueWithTimedOutConsequences()
			{
			}

			// Token: 0x06005E32 RID: 24114 RVA: 0x001B5BE7 File Offset: 0x001B3DE7
			protected override void OnIssueFinalized()
			{
			}

			// Token: 0x04001DD1 RID: 7633
			private const int IssueDuration = 15;

			// Token: 0x04001DD2 RID: 7634
			private const int QuestTimeLimit = 20;

			// Token: 0x04001DD3 RID: 7635
			private const int MinimumRequiredMenCount = 5;

			// Token: 0x04001DD4 RID: 7636
			private const int AlternativeSolutionMinimumSkillValue = 120;

			// Token: 0x04001DD5 RID: 7637
			private const int AlternativeSolutionTroopTierRequirement = 2;

			// Token: 0x04001DD6 RID: 7638
			[SaveableField(10)]
			private Hideout RelatedHideout;
		}

		// Token: 0x02000745 RID: 1861
		public class MerchantNeedsHelpWithOutlawsIssueQuest : QuestBase
		{
			// Token: 0x06005E33 RID: 24115 RVA: 0x001B5BE9 File Offset: 0x001B3DE9
			internal static void AutoGeneratedStaticCollectObjectsMerchantNeedsHelpWithOutlawsIssueQuest(object o, List<object> collectedObjects)
			{
				((MerchantNeedsHelpWithOutlawsIssueQuestBehavior.MerchantNeedsHelpWithOutlawsIssueQuest)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x06005E34 RID: 24116 RVA: 0x001B5BF7 File Offset: 0x001B3DF7
			protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				base.AutoGeneratedInstanceCollectObjects(collectedObjects);
				collectedObjects.Add(this._validPartiesList);
				collectedObjects.Add(this._relatedHideout);
				collectedObjects.Add(this._questProgressLogTest);
			}

			// Token: 0x06005E35 RID: 24117 RVA: 0x001B5C24 File Offset: 0x001B3E24
			internal static object AutoGeneratedGetMemberValue_totalPartyCount(object o)
			{
				return ((MerchantNeedsHelpWithOutlawsIssueQuestBehavior.MerchantNeedsHelpWithOutlawsIssueQuest)o)._totalPartyCount;
			}

			// Token: 0x06005E36 RID: 24118 RVA: 0x001B5C36 File Offset: 0x001B3E36
			internal static object AutoGeneratedGetMemberValue_destroyedPartyCount(object o)
			{
				return ((MerchantNeedsHelpWithOutlawsIssueQuestBehavior.MerchantNeedsHelpWithOutlawsIssueQuest)o)._destroyedPartyCount;
			}

			// Token: 0x06005E37 RID: 24119 RVA: 0x001B5C48 File Offset: 0x001B3E48
			internal static object AutoGeneratedGetMemberValue_recruitedPartyCount(object o)
			{
				return ((MerchantNeedsHelpWithOutlawsIssueQuestBehavior.MerchantNeedsHelpWithOutlawsIssueQuest)o)._recruitedPartyCount;
			}

			// Token: 0x06005E38 RID: 24120 RVA: 0x001B5C5A File Offset: 0x001B3E5A
			internal static object AutoGeneratedGetMemberValue_validPartiesList(object o)
			{
				return ((MerchantNeedsHelpWithOutlawsIssueQuestBehavior.MerchantNeedsHelpWithOutlawsIssueQuest)o)._validPartiesList;
			}

			// Token: 0x06005E39 RID: 24121 RVA: 0x001B5C67 File Offset: 0x001B3E67
			internal static object AutoGeneratedGetMemberValue_relatedHideout(object o)
			{
				return ((MerchantNeedsHelpWithOutlawsIssueQuestBehavior.MerchantNeedsHelpWithOutlawsIssueQuest)o)._relatedHideout;
			}

			// Token: 0x06005E3A RID: 24122 RVA: 0x001B5C74 File Offset: 0x001B3E74
			internal static object AutoGeneratedGetMemberValue_questProgressLogTest(object o)
			{
				return ((MerchantNeedsHelpWithOutlawsIssueQuestBehavior.MerchantNeedsHelpWithOutlawsIssueQuest)o)._questProgressLogTest;
			}

			// Token: 0x170012D9 RID: 4825
			// (get) Token: 0x06005E3B RID: 24123 RVA: 0x001B5C84 File Offset: 0x001B3E84
			public override TextObject Title
			{
				get
				{
					TextObject textObject = new TextObject("{=PBGiIbEM}{ISSUE_GIVER.NAME} Needs Help With Brigands", null);
					StringHelpers.SetCharacterProperties("ISSUE_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x170012DA RID: 4826
			// (get) Token: 0x06005E3C RID: 24124 RVA: 0x001B5CB6 File Offset: 0x001B3EB6
			public override bool IsRemainingTimeHidden
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170012DB RID: 4827
			// (get) Token: 0x06005E3D RID: 24125 RVA: 0x001B5CB9 File Offset: 0x001B3EB9
			private int _questPartyProgress
			{
				get
				{
					return this._destroyedPartyCount + this._recruitedPartyCount;
				}
			}

			// Token: 0x170012DC RID: 4828
			// (get) Token: 0x06005E3E RID: 24126 RVA: 0x001B5CC8 File Offset: 0x001B3EC8
			private TextObject PlayerStartsQuestLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=6iLxrDBa}You have accepted {QUEST_GIVER.LINK}'s request to find at least {TOTAL_COUNT} different parties of brigands around {QUEST_SETTLEMENT} and decided to hunt them down personally. {?QUEST_GIVER.GENDER}She{?}He{\\?} will reward you {AMOUNT}{GOLD_ICON} gold once you have dealt with them.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("TOTAL_COUNT", this._totalPartyCount);
					textObject.SetTextVariable("QUEST_SETTLEMENT", base.QuestGiver.CurrentSettlement.EncyclopediaLinkWithName);
					textObject.SetTextVariable("AMOUNT", this.RewardGold);
					textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"6\">");
					return textObject;
				}
			}

			// Token: 0x170012DD RID: 4829
			// (get) Token: 0x06005E3F RID: 24127 RVA: 0x001B5D4C File Offset: 0x001B3F4C
			private TextObject SuccessQuestLogText1
			{
				get
				{
					TextObject textObject = new TextObject("{=cQ6CzXKM}You have defeated all the brigands as {QUEST_GIVER.LINK} has asked. {?QUEST_GIVER.GENDER}She{?}He{\\?} is grateful. And sends you the reward, {GOLD_AMOUNT}{GOLD_ICON} gold as promised.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("SETTLEMENT", base.QuestGiver.CurrentSettlement.Name);
					textObject.SetTextVariable("GOLD_AMOUNT", this.RewardGold);
					textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"6\">");
					return textObject;
				}
			}

			// Token: 0x170012DE RID: 4830
			// (get) Token: 0x06005E40 RID: 24128 RVA: 0x001B5DC0 File Offset: 0x001B3FC0
			private TextObject SuccessQuestLogText2
			{
				get
				{
					TextObject textObject = new TextObject("{=dSHgU9gD}You have defeated some of the brigands and recruited the rest into your party. {QUEST_GIVER.LINK} is grateful and sends you the {GOLD_AMOUNT}{GOLD_ICON} as promised. ", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("GOLD_AMOUNT", this.RewardGold);
					textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"6\">");
					return textObject;
				}
			}

			// Token: 0x170012DF RID: 4831
			// (get) Token: 0x06005E41 RID: 24129 RVA: 0x001B5E18 File Offset: 0x001B4018
			private TextObject SuccessQuestLogText3
			{
				get
				{
					TextObject textObject = new TextObject("{=3V5udYJO}You have recruited the brigands into your party. {QUEST_GIVER.LINK} finds your solution acceptable and sends you the {GOLD_AMOUNT}{GOLD_ICON} as promised.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("GOLD_AMOUNT", this.RewardGold);
					textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"6\">");
					return textObject;
				}
			}

			// Token: 0x170012E0 RID: 4832
			// (get) Token: 0x06005E42 RID: 24130 RVA: 0x001B5E70 File Offset: 0x001B4070
			private TextObject TimeoutLog
			{
				get
				{
					TextObject textObject = new TextObject("{=Tcux6Sru}You have failed to defeat all {TOTAL_COUNT} outlaw parties in time as {QUEST_GIVER.LINK} asked. {?QUEST_GIVER.GENDER}She{?}He{\\?} is very disappointed.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("TOTAL_COUNT", this._totalPartyCount);
					return textObject;
				}
			}

			// Token: 0x170012E1 RID: 4833
			// (get) Token: 0x06005E43 RID: 24131 RVA: 0x001B5EB4 File Offset: 0x001B40B4
			private TextObject QuestGiverVillageRaided
			{
				get
				{
					TextObject textObject = new TextObject("{=4rCIZ6e5}{QUEST_SETTLEMENT} was raided, Your agreement with {QUEST_GIVER.LINK} is canceled.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("QUEST_SETTLEMENT", base.QuestGiver.CurrentSettlement.EncyclopediaLinkWithName);
					return textObject;
				}
			}

			// Token: 0x170012E2 RID: 4834
			// (get) Token: 0x06005E44 RID: 24132 RVA: 0x001B5F04 File Offset: 0x001B4104
			private TextObject QuestCanceledWarDeclaredLog
			{
				get
				{
					TextObject textObject = new TextObject("{=vW6kBki9}Your clan is now at war with {QUEST_GIVER.LINK}'s realm. Your agreement with {QUEST_GIVER.LINK} is canceled.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x170012E3 RID: 4835
			// (get) Token: 0x06005E45 RID: 24133 RVA: 0x001B5F38 File Offset: 0x001B4138
			private TextObject PlayerDeclaredWarQuestLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=DDur6mHb}Your actions have started a war with {ISSUE_GIVER.LINK}'s faction. Your agreement with {ISSUE_GIVER.LINK} is failed.", null);
					StringHelpers.SetCharacterProperties("ISSUE_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x06005E46 RID: 24134 RVA: 0x001B5F6C File Offset: 0x001B416C
			public MerchantNeedsHelpWithOutlawsIssueQuest(string questId, Hero giverHero, CampaignTime duration, int rewardGold, int totalPartyCount, Hideout relatedHideout)
				: base(questId, giverHero, duration, rewardGold)
			{
				this._totalPartyCount = totalPartyCount;
				this._destroyedPartyCount = 0;
				this._recruitedPartyCount = 0;
				this._validPartiesList = new List<MobileParty>();
				this._relatedHideout = relatedHideout;
				this.AddHideoutPartiesToValidPartiesList();
				this.SetDialogs();
				base.InitializeQuestOnCreation();
			}

			// Token: 0x06005E47 RID: 24135 RVA: 0x001B5FC0 File Offset: 0x001B41C0
			protected override void SetDialogs()
			{
				TextObject textObject = new TextObject("{=PQIYPCDn}Very good. I will be waiting for the good news then. Once you return, I'm ready to offer a reward of {REWARD_GOLD}{GOLD_ICON} denars. Just make sure that you defeat at least {TROOP_COUNT} bands no more than a day's ride away from here.[ib:normal][if:convo_bemused]", null);
				textObject.SetTextVariable("REWARD_GOLD", this.RewardGold);
				textObject.SetTextVariable("TROOP_COUNT", this._totalPartyCount);
				textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"6\">");
				this.OfferDialogFlow = DialogFlow.CreateDialogFlow("issue_classic_quest_start", 100).NpcLine(textObject, null, null, null, null).Condition(() => Hero.OneToOneConversationHero == base.QuestGiver)
					.Consequence(new ConversationSentence.OnConsequenceDelegate(this.QuestAcceptedConsequences))
					.CloseDialog();
				this.DiscussDialogFlow = DialogFlow.CreateDialogFlow("quest_discuss", 100).NpcLine(new TextObject("{=jjTcNhKE}Have you been able to find any bandits yet?[if:convo_undecided_open]", null), null, null, null, null).Condition(() => Hero.OneToOneConversationHero == base.QuestGiver)
					.BeginPlayerOptions(null, false)
					.PlayerOption(new TextObject("{=mU45Th70}We're off to hunt them now.", null), null, null, null)
					.NpcLine(new TextObject("{=u9vtceCV}You are a savior.[if:convo_astonished]", null), null, null, null, null)
					.CloseDialog()
					.Condition(() => Hero.OneToOneConversationHero == base.QuestGiver)
					.PlayerOption(new TextObject("{=QPv1b7f8}I haven't had the time yet.", null), null, null, null)
					.NpcLine(new TextObject("{=6ba4n9n6}We are waiting for your good news {?PLAYER.GENDER}my lady{?}sir{\\?}.[if:convo_focused_happy]", null), null, null, null, null)
					.CloseDialog()
					.Condition(() => Hero.OneToOneConversationHero == base.QuestGiver)
					.EndPlayerOptions()
					.CloseDialog();
			}

			// Token: 0x06005E48 RID: 24136 RVA: 0x001B6113 File Offset: 0x001B4313
			private void QuestAcceptedConsequences()
			{
				base.StartQuest();
				this._questProgressLogTest = base.AddDiscreteLog(this.PlayerStartsQuestLogText, new TextObject("{=HzcLsnYn}Destroyed parties", null), this._destroyedPartyCount, this._totalPartyCount, null, true);
			}

			// Token: 0x06005E49 RID: 24137 RVA: 0x001B6148 File Offset: 0x001B4348
			private void AddQuestStepLog()
			{
				this._questProgressLogTest.UpdateCurrentProgress(this._questPartyProgress);
				if (this._questPartyProgress >= this._totalPartyCount)
				{
					this.SuccessConsequences();
					return;
				}
				TextObject textObject = new TextObject("{=xbVCRbUu}You hunted {CURRENT_COUNT}/{TOTAL_COUNT} gang of brigands.", null);
				textObject.SetTextVariable("CURRENT_COUNT", this._questPartyProgress);
				textObject.SetTextVariable("TOTAL_COUNT", this._totalPartyCount);
				MBInformationManager.AddQuickInformation(textObject, 0, null, null, "");
			}

			// Token: 0x06005E4A RID: 24138 RVA: 0x001B61B7 File Offset: 0x001B43B7
			protected override void HourlyTick()
			{
			}

			// Token: 0x06005E4B RID: 24139 RVA: 0x001B61BC File Offset: 0x001B43BC
			protected override void RegisterEvents()
			{
				CampaignEvents.MobilePartyDestroyed.AddNonSerializedListener(this, new Action<MobileParty, PartyBase>(this.MobilePartyDestroyed));
				CampaignEvents.WarDeclared.AddNonSerializedListener(this, new Action<IFaction, IFaction, DeclareWarAction.DeclareWarDetail>(this.OnWarDeclared));
				CampaignEvents.OnClanChangedKingdomEvent.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, ChangeKingdomAction.ChangeKingdomActionDetail, bool>(this.OnClanChangedKingdom));
				CampaignEvents.VillageBeingRaided.AddNonSerializedListener(this, new Action<Village>(this.OnVillageRaided));
				CampaignEvents.MapEventStarted.AddNonSerializedListener(this, new Action<MapEvent, PartyBase, PartyBase>(this.OnMapEventStarted));
				CampaignEvents.BanditPartyRecruited.AddNonSerializedListener(this, new Action<MobileParty>(this.OnBanditPartyRecruited));
				CampaignEvents.SettlementEntered.AddNonSerializedListener(this, new Action<MobileParty, Settlement, Hero>(this.OnSettlementEntered));
				CampaignEvents.OnSettlementLeftEvent.AddNonSerializedListener(this, new Action<MobileParty, Settlement>(this.OnSettlementLeft));
				CampaignEvents.OnGameLoadFinishedEvent.AddNonSerializedListener(this, new Action(this.OnGameLoadFinished));
				CampaignEvents.IsSettlementBusyEvent.AddNonSerializedListener(this, new ReferenceAction<Settlement, object, int>(this.IsSettlementBusy));
			}

			// Token: 0x06005E4C RID: 24140 RVA: 0x001B62AF File Offset: 0x001B44AF
			private void IsSettlementBusy(Settlement settlement, object asker, ref int priority)
			{
				if (asker != this && settlement == this._relatedHideout.Settlement)
				{
					priority = Math.Max(priority, 200);
				}
			}

			// Token: 0x06005E4D RID: 24141 RVA: 0x001B62D4 File Offset: 0x001B44D4
			private void OnGameLoadFinished()
			{
				if (this._relatedHideout == null && MBSaveLoad.IsUpdatingGameVersion && MBSaveLoad.LastLoadedGameVersion < ApplicationVersion.FromString("v1.2.9", 0))
				{
					Hideout hideout = SettlementHelper.FindNearestHideoutToMobileParty(MobileParty.MainParty, MobileParty.NavigationType.Default, (Settlement x) => x.Hideout.IsInfested);
					if (hideout != null && Campaign.Current.Models.MapDistanceModel.GetDistance(base.QuestGiver.CurrentSettlement, hideout.Settlement, false, false, MobileParty.NavigationType.Default) < Campaign.Current.GetAverageDistanceBetweenClosestTwoTownsWithNavigationType(MobileParty.NavigationType.Default) * 1.25f)
					{
						this._relatedHideout = hideout;
					}
					if (this._relatedHideout != null)
					{
						this.AddHideoutPartiesToValidPartiesList();
					}
					else
					{
						base.CompleteQuestWithCancel(null);
					}
				}
				if (this._relatedHideout != null && base.IsOngoing && MBSaveLoad.IsUpdatingGameVersion && MBSaveLoad.LastLoadedGameVersion < ApplicationVersion.FromString("v1.2.9", 0) && this._relatedHideout.Settlement.IsSettlementBusy(this))
				{
					base.CompleteQuestWithCancel(null);
				}
			}

			// Token: 0x06005E4E RID: 24142 RVA: 0x001B63E0 File Offset: 0x001B45E0
			private void AddHideoutPartiesToValidPartiesList()
			{
				foreach (MobileParty mobileParty in this._relatedHideout.Settlement.Parties)
				{
					if (mobileParty.IsBandit)
					{
						this._validPartiesList.Add(mobileParty);
					}
				}
			}

			// Token: 0x06005E4F RID: 24143 RVA: 0x001B644C File Offset: 0x001B464C
			private void OnSettlementLeft(MobileParty party, Settlement settlement)
			{
				if (this._validPartiesList.Contains(party) && settlement.IsHideout && settlement.Hideout == this._relatedHideout)
				{
					this._validPartiesList.Remove(party);
				}
			}

			// Token: 0x06005E50 RID: 24144 RVA: 0x001B647F File Offset: 0x001B467F
			private void OnSettlementEntered(MobileParty party, Settlement settlement, Hero hero)
			{
				if (party != null && party.IsBandit && settlement.IsHideout && settlement.Hideout == this._relatedHideout)
				{
					this._validPartiesList.Add(party);
				}
			}

			// Token: 0x06005E51 RID: 24145 RVA: 0x001B64AE File Offset: 0x001B46AE
			private void OnBanditPartyRecruited(MobileParty banditParty)
			{
				if (this._validPartiesList.Contains(banditParty))
				{
					this._recruitedPartyCount++;
					this._validPartiesList.Remove(banditParty);
					this.AddQuestStepLog();
				}
			}

			// Token: 0x06005E52 RID: 24146 RVA: 0x001B64DF File Offset: 0x001B46DF
			private void OnMapEventStarted(MapEvent mapEvent, PartyBase attackerParty, PartyBase defenderParty)
			{
				if (QuestHelper.CheckMinorMajorCoercion(this, mapEvent, attackerParty))
				{
					QuestHelper.ApplyGenericMinorMajorCoercionConsequences(this, mapEvent);
				}
			}

			// Token: 0x06005E53 RID: 24147 RVA: 0x001B64F2 File Offset: 0x001B46F2
			private void OnVillageRaided(Village village)
			{
				if (village == base.QuestGiver.CurrentSettlement.Village)
				{
					base.AddLog(this.QuestGiverVillageRaided, false);
					base.CompleteQuestWithCancel(null);
				}
			}

			// Token: 0x06005E54 RID: 24148 RVA: 0x001B651C File Offset: 0x001B471C
			private void OnClanChangedKingdom(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail detail, bool showNotification = true)
			{
				if (base.QuestGiver.CurrentSettlement.MapFaction.IsAtWarWith(Hero.MainHero.MapFaction))
				{
					base.CompleteQuestWithCancel(this.QuestCanceledWarDeclaredLog);
				}
			}

			// Token: 0x06005E55 RID: 24149 RVA: 0x001B654B File Offset: 0x001B474B
			private void OnWarDeclared(IFaction faction1, IFaction faction2, DeclareWarAction.DeclareWarDetail detail)
			{
				QuestHelper.CheckWarDeclarationAndFailOrCancelTheQuest(this, faction1, faction2, detail, this.PlayerDeclaredWarQuestLogText, this.QuestCanceledWarDeclaredLog, false);
			}

			// Token: 0x06005E56 RID: 24150 RVA: 0x001B6563 File Offset: 0x001B4763
			private void MobilePartyDestroyed(MobileParty mobileParty, PartyBase destroyerParty)
			{
				if (destroyerParty == PartyBase.MainParty && this._validPartiesList.Contains(mobileParty))
				{
					this._destroyedPartyCount++;
					this.AddQuestStepLog();
				}
			}

			// Token: 0x06005E57 RID: 24151 RVA: 0x001B6590 File Offset: 0x001B4790
			protected override void HourlyTickParty(MobileParty mobileParty)
			{
				if (base.IsOngoing && mobileParty.IsBandit && mobileParty.MapEvent == null && mobileParty.MapFaction.IsBanditFaction && !mobileParty.IsCurrentlyUsedByAQuest && !mobileParty.IsCurrentlyAtSea)
				{
					float num;
					if (((mobileParty.CurrentSettlement != null) ? Campaign.Current.Models.MapDistanceModel.GetDistance(mobileParty.CurrentSettlement, base.QuestGiver.CurrentSettlement, false, false, MobileParty.NavigationType.Default, out num) : Campaign.Current.Models.MapDistanceModel.GetDistance(mobileParty, base.QuestGiver.CurrentSettlement, false, MobileParty.NavigationType.Default, out num)) <= MerchantNeedsHelpWithOutlawsIssueQuestBehavior.ValidBanditPartyDistance)
					{
						if (!this._validPartiesList.Contains(mobileParty))
						{
							if (!base.IsTracked(mobileParty))
							{
								base.AddTrackedObject(mobileParty);
							}
							this._validPartiesList.Add(mobileParty);
							if (mobileParty.CurrentSettlement == null && MBRandom.RandomFloat < 1f / (float)this._validPartiesList.Count)
							{
								SetPartyAiAction.GetActionForPatrollingAroundSettlement(mobileParty, base.QuestGiver.CurrentSettlement, MobileParty.NavigationType.Default, false, false);
								mobileParty.Ai.SetDoNotMakeNewDecisions(true);
								mobileParty.IgnoreForHours(500f);
								return;
							}
						}
						else if (MBRandom.RandomFloat < 0.05f)
						{
							mobileParty.Ai.SetDoNotMakeNewDecisions(false);
							return;
						}
					}
					else if (base.IsTracked(mobileParty))
					{
						base.RemoveTrackedObject(mobileParty);
						this._validPartiesList.Remove(mobileParty);
						mobileParty.Ai.SetDoNotMakeNewDecisions(false);
					}
				}
			}

			// Token: 0x06005E58 RID: 24152 RVA: 0x001B6704 File Offset: 0x001B4904
			private void SuccessConsequences()
			{
				if (this._destroyedPartyCount == this._totalPartyCount)
				{
					base.AddLog(this.SuccessQuestLogText1, false);
				}
				else if (this._recruitedPartyCount != 0 && this._recruitedPartyCount < this._totalPartyCount)
				{
					base.AddLog(this.SuccessQuestLogText2, false);
				}
				else
				{
					base.AddLog(this.SuccessQuestLogText3, false);
				}
				this.RelationshipChangeWithQuestGiver = 3;
				GiveGoldAction.ApplyBetweenCharacters(null, Hero.MainHero, this.RewardGold, false);
				if (base.QuestGiver.CurrentSettlement.IsVillage && base.QuestGiver.CurrentSettlement.Village.TradeBound != null)
				{
					base.QuestGiver.CurrentSettlement.Village.Bound.Town.Security += 5f;
					base.QuestGiver.CurrentSettlement.Village.Bound.Town.Prosperity += 5f;
				}
				else if (base.QuestGiver.CurrentSettlement.IsTown)
				{
					base.QuestGiver.CurrentSettlement.Town.Security += 5f;
					base.QuestGiver.CurrentSettlement.Town.Prosperity += 5f;
				}
				Hero.MainHero.Clan.AddRenown(1f, true);
				base.CompleteQuestWithSuccess();
			}

			// Token: 0x06005E59 RID: 24153 RVA: 0x001B686C File Offset: 0x001B4A6C
			protected override void OnTimedOut()
			{
				this.RelationshipChangeWithQuestGiver = -5;
				if (base.QuestGiver.CurrentSettlement.IsVillage)
				{
					base.QuestGiver.CurrentSettlement.Village.Bound.Town.Prosperity -= 10f;
				}
				else if (base.QuestGiver.CurrentSettlement.IsTown)
				{
					base.QuestGiver.CurrentSettlement.Town.Prosperity -= 10f;
				}
				base.AddLog(this.TimeoutLog, false);
			}

			// Token: 0x06005E5A RID: 24154 RVA: 0x001B6901 File Offset: 0x001B4B01
			protected override void InitializeQuestOnGameLoad()
			{
				this.SetDialogs();
			}

			// Token: 0x06005E5B RID: 24155 RVA: 0x001B690C File Offset: 0x001B4B0C
			protected override void OnFinalize()
			{
				foreach (MobileParty mobileParty in this._validPartiesList)
				{
					mobileParty.Ai.SetDoNotMakeNewDecisions(false);
					mobileParty.IgnoreForHours(0f);
					if (base.IsTracked(mobileParty))
					{
						base.RemoveTrackedObject(mobileParty);
					}
				}
				this._validPartiesList.Clear();
			}

			// Token: 0x04001DD7 RID: 7639
			[SaveableField(10)]
			private readonly int _totalPartyCount;

			// Token: 0x04001DD8 RID: 7640
			[SaveableField(30)]
			private int _destroyedPartyCount;

			// Token: 0x04001DD9 RID: 7641
			[SaveableField(50)]
			private int _recruitedPartyCount;

			// Token: 0x04001DDA RID: 7642
			[SaveableField(40)]
			private List<MobileParty> _validPartiesList;

			// Token: 0x04001DDB RID: 7643
			[SaveableField(70)]
			private Hideout _relatedHideout;

			// Token: 0x04001DDC RID: 7644
			private const float ValidBanditPartyEnableAiChance = 0.05f;

			// Token: 0x04001DDD RID: 7645
			[SaveableField(60)]
			private JournalLog _questProgressLogTest;
		}

		// Token: 0x02000746 RID: 1862
		public class MerchantNeedsHelpWithOutlawsIssueTypeDefiner : SaveableTypeDefiner
		{
			// Token: 0x06005E60 RID: 24160 RVA: 0x001B69C8 File Offset: 0x001B4BC8
			public MerchantNeedsHelpWithOutlawsIssueTypeDefiner()
				: base(590000)
			{
			}

			// Token: 0x06005E61 RID: 24161 RVA: 0x001B69D5 File Offset: 0x001B4BD5
			protected override void DefineClassTypes()
			{
				base.AddClassDefinition(typeof(MerchantNeedsHelpWithOutlawsIssueQuestBehavior.MerchantNeedsHelpWithOutlawsIssue), 1, null);
				base.AddClassDefinition(typeof(MerchantNeedsHelpWithOutlawsIssueQuestBehavior.MerchantNeedsHelpWithOutlawsIssueQuest), 2, null);
			}
		}
	}
}
