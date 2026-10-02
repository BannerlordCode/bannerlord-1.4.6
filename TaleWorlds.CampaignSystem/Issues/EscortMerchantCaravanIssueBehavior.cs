using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Party.PartyComponents;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.Issues
{
	// Token: 0x0200035F RID: 863
	public class EscortMerchantCaravanIssueBehavior : CampaignBehaviorBase
	{
		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06003350 RID: 13136 RVA: 0x000D39E4 File Offset: 0x000D1BE4
		private static EscortMerchantCaravanIssueBehavior Instance
		{
			get
			{
				return Campaign.Current.GetCampaignBehavior<EscortMerchantCaravanIssueBehavior>();
			}
		}

		// Token: 0x06003351 RID: 13137 RVA: 0x000D39F0 File Offset: 0x000D1BF0
		public override void RegisterEvents()
		{
			CampaignEvents.OnCheckForIssueEvent.AddNonSerializedListener(this, new Action<Hero>(this.OnCheckForIssue));
			CampaignEvents.OnNewGameCreatedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnNewGameCreated));
			CampaignEvents.OnGameLoadedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnGameLoaded));
		}

		// Token: 0x06003352 RID: 13138 RVA: 0x000D3A44 File Offset: 0x000D1C44
		private void OnGameLoaded(CampaignGameStarter campaignGameStarter)
		{
			this.InitializeOnStart();
			if (MBSaveLoad.IsUpdatingGameVersion && MBSaveLoad.LastLoadedGameVersion < ApplicationVersion.FromString("e1.9.1", 0))
			{
				for (int i = MobileParty.All.Count - 1; i >= 0; i--)
				{
					MobileParty mobileParty = MobileParty.All[i];
					if (mobileParty.StringId.Contains("defend_caravan_quest"))
					{
						if (mobileParty.MapEvent != null)
						{
							mobileParty.MapEvent.FinalizeEvent();
						}
						DestroyPartyAction.Apply(null, MobileParty.All[i]);
					}
				}
			}
		}

		// Token: 0x06003353 RID: 13139 RVA: 0x000D3AD0 File Offset: 0x000D1CD0
		private void InitializeOnStart()
		{
			if (MBObjectManager.Instance.GetObject<ItemObject>("hardwood") == null || MBObjectManager.Instance.GetObject<ItemObject>("sumpter_horse") == null)
			{
				CampaignEventDispatcher.Instance.RemoveListeners(this);
				using (List<KeyValuePair<Hero, IssueBase>>.Enumerator enumerator = Campaign.Current.IssueManager.Issues.Where<KeyValuePair<Hero, IssueBase>>((KeyValuePair<Hero, IssueBase> x) => x.Value.GetType() == typeof(EscortMerchantCaravanIssueBehavior.EscortMerchantCaravanIssue)).ToList<KeyValuePair<Hero, IssueBase>>().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<Hero, IssueBase> keyValuePair = enumerator.Current;
						keyValuePair.Value.CompleteIssueWithStayAliveConditionsFailed();
					}
					return;
				}
			}
			this.DefaultCaravanItems.Add(DefaultItems.Grain);
			foreach (string text in new string[] { "cotton", "velvet", "oil", "linen", "date_fruit" })
			{
				ItemObject @object = MBObjectManager.Instance.GetObject<ItemObject>(text);
				if (@object != null)
				{
					this.DefaultCaravanItems.Add(@object);
				}
			}
		}

		// Token: 0x06003354 RID: 13140 RVA: 0x000D3C00 File Offset: 0x000D1E00
		private void OnNewGameCreated(CampaignGameStarter campaignGameStarter)
		{
			this.InitializeOnStart();
		}

		// Token: 0x06003355 RID: 13141 RVA: 0x000D3C08 File Offset: 0x000D1E08
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06003356 RID: 13142 RVA: 0x000D3C0C File Offset: 0x000D1E0C
		private bool ConditionsHold(Hero issueGiver)
		{
			return issueGiver.IsMerchant && issueGiver.CurrentSettlement != null && issueGiver.CurrentSettlement.IsTown && !issueGiver.CurrentSettlement.HasPort && issueGiver.CurrentSettlement.Town.Security <= 50f && issueGiver.OwnedCaravans.Count < 2;
		}

		// Token: 0x06003357 RID: 13143 RVA: 0x000D3C6C File Offset: 0x000D1E6C
		public void OnCheckForIssue(Hero hero)
		{
			if (this.ConditionsHold(hero))
			{
				Campaign.Current.IssueManager.AddPotentialIssueData(hero, new PotentialIssueData(new PotentialIssueData.StartIssueDelegate(this.OnSelected), typeof(EscortMerchantCaravanIssueBehavior.EscortMerchantCaravanIssue), IssueBase.IssueFrequency.VeryCommon, null));
				return;
			}
			Campaign.Current.IssueManager.AddPotentialIssueData(hero, new PotentialIssueData(typeof(EscortMerchantCaravanIssueBehavior.EscortMerchantCaravanIssue), IssueBase.IssueFrequency.VeryCommon));
		}

		// Token: 0x06003358 RID: 13144 RVA: 0x000D3CD0 File Offset: 0x000D1ED0
		private IssueBase OnSelected(in PotentialIssueData pid, Hero issueOwner)
		{
			return new EscortMerchantCaravanIssueBehavior.EscortMerchantCaravanIssue(issueOwner);
		}

		// Token: 0x04000EB7 RID: 3767
		private const IssueBase.IssueFrequency EscortMerchantCaravanIssueFrequency = IssueBase.IssueFrequency.VeryCommon;

		// Token: 0x04000EB8 RID: 3768
		internal readonly List<ItemObject> DefaultCaravanItems = new List<ItemObject>();

		// Token: 0x020006FE RID: 1790
		public class EscortMerchantCaravanIssue : IssueBase
		{
			// Token: 0x06005643 RID: 22083 RVA: 0x001962C5 File Offset: 0x001944C5
			internal static void AutoGeneratedStaticCollectObjectsEscortMerchantCaravanIssue(object o, List<object> collectedObjects)
			{
				((EscortMerchantCaravanIssueBehavior.EscortMerchantCaravanIssue)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x06005644 RID: 22084 RVA: 0x001962D3 File Offset: 0x001944D3
			protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				base.AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x06005645 RID: 22085 RVA: 0x001962DC File Offset: 0x001944DC
			internal static object AutoGeneratedGetMemberValue_companionRewardRandom(object o)
			{
				return ((EscortMerchantCaravanIssueBehavior.EscortMerchantCaravanIssue)o)._companionRewardRandom;
			}

			// Token: 0x17001047 RID: 4167
			// (get) Token: 0x06005646 RID: 22086 RVA: 0x001962EE File Offset: 0x001944EE
			public override IssueBase.AlternativeSolutionScaleFlag AlternativeSolutionScaleFlags
			{
				get
				{
					return IssueBase.AlternativeSolutionScaleFlag.Casualties | IssueBase.AlternativeSolutionScaleFlag.FailureRisk;
				}
			}

			// Token: 0x17001048 RID: 4168
			// (get) Token: 0x06005647 RID: 22087 RVA: 0x001962F2 File Offset: 0x001944F2
			public override int AlternativeSolutionBaseNeededMenCount
			{
				get
				{
					return 10 + MathF.Ceiling(16f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x17001049 RID: 4169
			// (get) Token: 0x06005648 RID: 22088 RVA: 0x00196308 File Offset: 0x00194508
			protected override int AlternativeSolutionBaseDurationInDaysInternal
			{
				get
				{
					return 6 + MathF.Ceiling(10f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x1700104A RID: 4170
			// (get) Token: 0x06005649 RID: 22089 RVA: 0x0019631D File Offset: 0x0019451D
			protected int DailyQuestRewardGold
			{
				get
				{
					return 250 + MathF.Ceiling(1000f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x1700104B RID: 4171
			// (get) Token: 0x0600564A RID: 22090 RVA: 0x00196336 File Offset: 0x00194536
			protected override int RewardGold
			{
				get
				{
					return Math.Min(this.DailyQuestRewardGold * this._companionRewardRandom, 8000);
				}
			}

			// Token: 0x1700104C RID: 4172
			// (get) Token: 0x0600564B RID: 22091 RVA: 0x00196350 File Offset: 0x00194550
			public override TextObject IssueBriefByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=CSqaF7tz}There's been a real surge of banditry around here recently. I don't know if it's because the lords are away fighting or something else, but it's a miracle if a traveler can make three leagues beyond the gates without being set upon by highwaymen.[if:convo_annoyed][ib:hip]", null);
					if (base.IssueOwner.CharacterObject.GetPersona() == DefaultTraits.PersonaCurt || base.IssueOwner.CharacterObject.GetPersona() == DefaultTraits.PersonaSoftspoken)
					{
						textObject = new TextObject("{=xwc9mJdC}Things have gotten a lot worse recently with the brigands on the roads around town. My caravans get looted as soon as they're out of sight of the gates.[if:convo_stern][ib:hip]", null);
					}
					return textObject;
				}
			}

			// Token: 0x1700104D RID: 4173
			// (get) Token: 0x0600564C RID: 22092 RVA: 0x001963A4 File Offset: 0x001945A4
			public override TextObject IssueAcceptByPlayer
			{
				get
				{
					return new TextObject("{=TGYJUUn0}Go on.", null);
				}
			}

			// Token: 0x1700104E RID: 4174
			// (get) Token: 0x0600564D RID: 22093 RVA: 0x001963B1 File Offset: 0x001945B1
			public override TextObject IssueQuestSolutionExplanationByIssueGiver
			{
				get
				{
					return new TextObject("{=8ym6UvxE}I'm of a mind to send out a new caravan but I fear it will be plundered before it can turn a profit. So I am looking for some good fighters who can escort it until it finds its footing and visits a couple of settlements.", null);
				}
			}

			// Token: 0x1700104F RID: 4175
			// (get) Token: 0x0600564E RID: 22094 RVA: 0x001963C0 File Offset: 0x001945C0
			public override TextObject IssueAlternativeSolutionExplanationByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=ytdZutjw}I will be willing to pay generously {BASE_REWARD}{GOLD_ICON} for each day the caravan is on the road. It will be more than I usually pay for caravan guards, but you look like the type who send a message to these brigands, that my caravans aren't to be messed with.[if:convo_undecided_closed]", null);
					if (base.IssueOwner.CharacterObject.GetPersona() == DefaultTraits.PersonaCurt || base.IssueOwner.CharacterObject.GetPersona() == DefaultTraits.PersonaSoftspoken)
					{
						textObject = new TextObject("{=YbbfaHqd}I will be willing to pay generously {BASE_REWARD}{GOLD_ICON} for each day the caravan is on the road. It will be more than I usually pay for guards, but figure maybe you can scare these bandits off. I'm sick of choosing between sending my men to the their deaths or letting them go because I've lost my goods and can't pay their wages.[if:convo_undecided_closed]", null);
					}
					textObject.SetTextVariable("BASE_REWARD", this.DailyQuestRewardGold);
					return textObject;
				}
			}

			// Token: 0x17001050 RID: 4176
			// (get) Token: 0x0600564F RID: 22095 RVA: 0x00196426 File Offset: 0x00194626
			public override TextObject IssueQuestSolutionAcceptByPlayer
			{
				get
				{
					return new TextObject("{=a7fEPW5Y}Don't worry, I'll escort the caravan myself.", null);
				}
			}

			// Token: 0x17001051 RID: 4177
			// (get) Token: 0x06005650 RID: 22096 RVA: 0x00196433 File Offset: 0x00194633
			public override TextObject IssueAlternativeSolutionAcceptByPlayer
			{
				get
				{
					TextObject textObject = new TextObject("{=N4p2GCsG}I'll assign one of my companions and {NEEDED_MEN_COUNT} of my men to protect your caravan for {RETURN_DAYS} days.", null);
					textObject.SetTextVariable("NEEDED_MEN_COUNT", base.GetTotalAlternativeSolutionNeededMenCount());
					textObject.SetTextVariable("RETURN_DAYS", base.GetTotalAlternativeSolutionDurationInDays());
					return textObject;
				}
			}

			// Token: 0x17001052 RID: 4178
			// (get) Token: 0x06005651 RID: 22097 RVA: 0x00196464 File Offset: 0x00194664
			public override TextObject IssueDiscussAlternativeSolution
			{
				get
				{
					return new TextObject("{=hU5j7b3e}I am sure your men are as capable as you are and will look after my caravan. Thanks again for your help, my friend.[if:convo_focused_happy]", null);
				}
			}

			// Token: 0x17001053 RID: 4179
			// (get) Token: 0x06005652 RID: 22098 RVA: 0x00196471 File Offset: 0x00194671
			public override TextObject IssueAlternativeSolutionResponseByIssueGiver
			{
				get
				{
					return new TextObject("{=iny76Ifh}Thank you, {?PLAYER.GENDER}madam{?}sir{\\?}, I think they will be enough.", null);
				}
			}

			// Token: 0x17001054 RID: 4180
			// (get) Token: 0x06005653 RID: 22099 RVA: 0x0019647E File Offset: 0x0019467E
			public override bool IsThereAlternativeSolution
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001055 RID: 4181
			// (get) Token: 0x06005654 RID: 22100 RVA: 0x00196481 File Offset: 0x00194681
			public override bool IsThereLordSolution
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001056 RID: 4182
			// (get) Token: 0x06005655 RID: 22101 RVA: 0x00196484 File Offset: 0x00194684
			protected override TextObject AlternativeSolutionStartLog
			{
				get
				{
					TextObject textObject = new TextObject("{=6y59FBgL}{ISSUEGIVER.LINK}, a merchant from {SETTLEMENT}, has told you about {?ISSUEGIVER.GENDER}her{?}his{\\?} recent problems with bandits. {?ISSUEGIVER.GENDER}She{?}he{\\?} asked you to guard {?ISSUEGIVER.GENDER}her{?}his{\\?} caravan for a while and deal with any attackers. In return {?ISSUEGIVER.GENDER}she{?}he{\\?} offered you {GOLD}{GOLD_ICON} for each day your troops spend on escort duty.{newline}You agreed to lend {?ISSUEGIVER.GENDER}her{?}him{\\?} {NEEDED_MEN_COUNT} men. They should be enough to turn away most of the bandits. Your troops should return after {RETURN_DAYS} days.", null);
					StringHelpers.SetCharacterProperties("ISSUEGIVER", base.IssueOwner.CharacterObject, textObject, false);
					textObject.SetTextVariable("SETTLEMENT", base.IssueOwner.CurrentSettlement.Name);
					textObject.SetTextVariable("NEEDED_MEN_COUNT", this.AlternativeSolutionSentTroops.TotalManCount);
					textObject.SetTextVariable("RETURN_DAYS", base.GetTotalAlternativeSolutionDurationInDays());
					textObject.SetTextVariable("GOLD", this.DailyQuestRewardGold);
					textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"6\">");
					return textObject;
				}
			}

			// Token: 0x17001057 RID: 4183
			// (get) Token: 0x06005656 RID: 22102 RVA: 0x0019651E File Offset: 0x0019471E
			public override TextObject Title
			{
				get
				{
					return new TextObject("{=VpLzd69e}Escort Merchant Caravan", null);
				}
			}

			// Token: 0x17001058 RID: 4184
			// (get) Token: 0x06005657 RID: 22103 RVA: 0x0019652B File Offset: 0x0019472B
			public override TextObject Description
			{
				get
				{
					return new TextObject("{=8RNueEmy}A merchant caravan needs an escort for protection against bandits and brigands.", null);
				}
			}

			// Token: 0x17001059 RID: 4185
			// (get) Token: 0x06005658 RID: 22104 RVA: 0x00196538 File Offset: 0x00194738
			public override TextObject IssueAlternativeSolutionFailLog
			{
				get
				{
					return new TextObject("{=KLauwaRJ}The caravan was destroyed despite your companion's efforts. Quest failed.", null);
				}
			}

			// Token: 0x1700105A RID: 4186
			// (get) Token: 0x06005659 RID: 22105 RVA: 0x00196548 File Offset: 0x00194748
			public override TextObject IssueAlternativeSolutionSuccessLog
			{
				get
				{
					TextObject textObject = new TextObject("{=3NX8H4TJ}Your companion has protected the caravan that belongs to {ISSUE_GIVER.LINK} from {SETTLEMENT} as promised. {?ISSUE_GIVER.GENDER}She{?}He{\\?} was happy with your work.", null);
					StringHelpers.SetCharacterProperties("ISSUE_GIVER", base.IssueOwner.CharacterObject, textObject, false);
					textObject.SetTextVariable("SETTLEMENT", base.IssueSettlement.EncyclopediaLinkWithName);
					return textObject;
				}
			}

			// Token: 0x0600565A RID: 22106 RVA: 0x00196591 File Offset: 0x00194791
			public EscortMerchantCaravanIssue(Hero issueOwner)
				: base(issueOwner, CampaignTime.DaysFromNow(30f))
			{
				this._companionRewardRandom = MBRandom.RandomInt(3, 10);
			}

			// Token: 0x0600565B RID: 22107 RVA: 0x001965B2 File Offset: 0x001947B2
			protected override float GetIssueEffectAmountInternal(IssueEffect issueEffect)
			{
				if (issueEffect == DefaultIssueEffects.SettlementProsperity)
				{
					return -0.4f;
				}
				if (issueEffect == DefaultIssueEffects.IssueOwnerPower)
				{
					return -0.2f;
				}
				return 0f;
			}

			// Token: 0x0600565C RID: 22108 RVA: 0x001965D5 File Offset: 0x001947D5
			public override ValueTuple<SkillObject, int> GetAlternativeSolutionSkill(Hero hero)
			{
				return new ValueTuple<SkillObject, int>((hero.GetSkillValue(DefaultSkills.Scouting) >= hero.GetSkillValue(DefaultSkills.Riding)) ? DefaultSkills.Scouting : DefaultSkills.Riding, 120);
			}

			// Token: 0x0600565D RID: 22109 RVA: 0x00196602 File Offset: 0x00194802
			public override bool DoTroopsSatisfyAlternativeSolution(TroopRoster troopRoster, out TextObject explanation)
			{
				return QuestHelper.CheckRosterForAlternativeSolution(troopRoster, base.GetTotalAlternativeSolutionNeededMenCount(), out explanation, 2, false);
			}

			// Token: 0x0600565E RID: 22110 RVA: 0x00196613 File Offset: 0x00194813
			public override bool AlternativeSolutionCondition(out TextObject explanation)
			{
				return QuestHelper.CheckRosterForAlternativeSolution(MobileParty.MainParty.MemberRoster, base.GetTotalAlternativeSolutionNeededMenCount(), out explanation, 2, false);
			}

			// Token: 0x1700105B RID: 4187
			// (get) Token: 0x0600565F RID: 22111 RVA: 0x0019662D File Offset: 0x0019482D
			protected override int CompanionSkillRewardXP
			{
				get
				{
					return (int)(800f + 1000f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x06005660 RID: 22112 RVA: 0x00196642 File Offset: 0x00194842
			public override bool IsTroopTypeNeededByAlternativeSolution(CharacterObject character)
			{
				return character.Tier >= 2;
			}

			// Token: 0x06005661 RID: 22113 RVA: 0x00196650 File Offset: 0x00194850
			public override IssueBase.IssueFrequency GetFrequency()
			{
				return IssueBase.IssueFrequency.VeryCommon;
			}

			// Token: 0x06005662 RID: 22114 RVA: 0x00196654 File Offset: 0x00194854
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
				if (issueGiver.MapFaction.IsAtWarWith(Hero.MainHero.MapFaction))
				{
					flags |= IssueBase.PreconditionFlags.AtWar;
				}
				if (MobileParty.MainParty.MemberRoster.TotalHealthyCount < 20)
				{
					flags |= IssueBase.PreconditionFlags.NotEnoughTroops;
				}
				return flags == IssueBase.PreconditionFlags.None;
			}

			// Token: 0x06005663 RID: 22115 RVA: 0x001966C5 File Offset: 0x001948C5
			public override bool IssueStayAliveConditions()
			{
				return base.IssueOwner.OwnedCaravans.Count < 2 && base.IssueOwner.CurrentSettlement.Town.Security <= 80f;
			}

			// Token: 0x06005664 RID: 22116 RVA: 0x001966FB File Offset: 0x001948FB
			protected override void OnGameLoad()
			{
			}

			// Token: 0x06005665 RID: 22117 RVA: 0x001966FD File Offset: 0x001948FD
			protected override void HourlyTick()
			{
			}

			// Token: 0x06005666 RID: 22118 RVA: 0x001966FF File Offset: 0x001948FF
			protected override QuestBase GenerateIssueQuest(string questId)
			{
				return new EscortMerchantCaravanIssueBehavior.EscortMerchantCaravanIssueQuest(questId, base.IssueOwner, CampaignTime.DaysFromNow(30f), base.IssueDifficultyMultiplier, this.DailyQuestRewardGold);
			}

			// Token: 0x06005667 RID: 22119 RVA: 0x00196724 File Offset: 0x00194924
			protected override void AlternativeSolutionEndWithFailureConsequence()
			{
				base.IssueOwner.AddPower(-5f);
				this.RelationshipChangeWithIssueOwner = -5;
				TraitLevelingHelper.OnIssueFailed(base.IssueOwner, new Tuple<TraitObject, int>[]
				{
					new Tuple<TraitObject, int>(DefaultTraits.Honor, -20)
				});
				base.IssueSettlement.Town.Prosperity -= 20f;
			}

			// Token: 0x06005668 RID: 22120 RVA: 0x00196785 File Offset: 0x00194985
			protected override void AlternativeSolutionEndWithSuccessConsequence()
			{
				base.IssueOwner.AddPower(10f);
				this.RelationshipChangeWithIssueOwner = 5;
				base.IssueSettlement.Town.Prosperity += 10f;
			}

			// Token: 0x06005669 RID: 22121 RVA: 0x001967BA File Offset: 0x001949BA
			protected override void CompleteIssueWithTimedOutConsequences()
			{
			}

			// Token: 0x04001C58 RID: 7256
			private const int MinimumRequiredMenCount = 20;

			// Token: 0x04001C59 RID: 7257
			private const int AlternativeSolutionTroopTierRequirement = 2;

			// Token: 0x04001C5A RID: 7258
			private const int NeededCompanionSkillAmount = 120;

			// Token: 0x04001C5B RID: 7259
			private const int QuestTimeLimit = 30;

			// Token: 0x04001C5C RID: 7260
			private const int IssueDuration = 30;

			// Token: 0x04001C5D RID: 7261
			[SaveableField(10)]
			private int _companionRewardRandom;
		}

		// Token: 0x020006FF RID: 1791
		public class EscortMerchantCaravanIssueQuest : QuestBase
		{
			// Token: 0x0600566A RID: 22122 RVA: 0x001967BC File Offset: 0x001949BC
			internal static void AutoGeneratedStaticCollectObjectsEscortMerchantCaravanIssueQuest(object o, List<object> collectedObjects)
			{
				((EscortMerchantCaravanIssueBehavior.EscortMerchantCaravanIssueQuest)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x0600566B RID: 22123 RVA: 0x001967CC File Offset: 0x001949CC
			protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				base.AutoGeneratedInstanceCollectObjects(collectedObjects);
				collectedObjects.Add(this._visitedSettlements);
				collectedObjects.Add(this._questCaravanMobileParty);
				collectedObjects.Add(this._questBanditMobileParty);
				collectedObjects.Add(this._otherBanditParty);
				collectedObjects.Add(this._playerStartsQuestLog);
			}

			// Token: 0x0600566C RID: 22124 RVA: 0x0019681C File Offset: 0x00194A1C
			internal static object AutoGeneratedGetMemberValue_requiredSettlementNumber(object o)
			{
				return ((EscortMerchantCaravanIssueBehavior.EscortMerchantCaravanIssueQuest)o)._requiredSettlementNumber;
			}

			// Token: 0x0600566D RID: 22125 RVA: 0x0019682E File Offset: 0x00194A2E
			internal static object AutoGeneratedGetMemberValue_visitedSettlements(object o)
			{
				return ((EscortMerchantCaravanIssueBehavior.EscortMerchantCaravanIssueQuest)o)._visitedSettlements;
			}

			// Token: 0x0600566E RID: 22126 RVA: 0x0019683B File Offset: 0x00194A3B
			internal static object AutoGeneratedGetMemberValue_questCaravanMobileParty(object o)
			{
				return ((EscortMerchantCaravanIssueBehavior.EscortMerchantCaravanIssueQuest)o)._questCaravanMobileParty;
			}

			// Token: 0x0600566F RID: 22127 RVA: 0x00196848 File Offset: 0x00194A48
			internal static object AutoGeneratedGetMemberValue_questBanditMobileParty(object o)
			{
				return ((EscortMerchantCaravanIssueBehavior.EscortMerchantCaravanIssueQuest)o)._questBanditMobileParty;
			}

			// Token: 0x06005670 RID: 22128 RVA: 0x00196855 File Offset: 0x00194A55
			internal static object AutoGeneratedGetMemberValue_difficultyMultiplier(object o)
			{
				return ((EscortMerchantCaravanIssueBehavior.EscortMerchantCaravanIssueQuest)o)._difficultyMultiplier;
			}

			// Token: 0x06005671 RID: 22129 RVA: 0x00196867 File Offset: 0x00194A67
			internal static object AutoGeneratedGetMemberValue_isPlayerNotifiedForDanger(object o)
			{
				return ((EscortMerchantCaravanIssueBehavior.EscortMerchantCaravanIssueQuest)o)._isPlayerNotifiedForDanger;
			}

			// Token: 0x06005672 RID: 22130 RVA: 0x00196879 File Offset: 0x00194A79
			internal static object AutoGeneratedGetMemberValue_otherBanditParty(object o)
			{
				return ((EscortMerchantCaravanIssueBehavior.EscortMerchantCaravanIssueQuest)o)._otherBanditParty;
			}

			// Token: 0x06005673 RID: 22131 RVA: 0x00196886 File Offset: 0x00194A86
			internal static object AutoGeneratedGetMemberValue_questBanditPartyFollowDuration(object o)
			{
				return ((EscortMerchantCaravanIssueBehavior.EscortMerchantCaravanIssueQuest)o)._questBanditPartyFollowDuration;
			}

			// Token: 0x06005674 RID: 22132 RVA: 0x00196898 File Offset: 0x00194A98
			internal static object AutoGeneratedGetMemberValue_otherBanditPartyFollowDuration(object o)
			{
				return ((EscortMerchantCaravanIssueBehavior.EscortMerchantCaravanIssueQuest)o)._otherBanditPartyFollowDuration;
			}

			// Token: 0x06005675 RID: 22133 RVA: 0x001968AA File Offset: 0x00194AAA
			internal static object AutoGeneratedGetMemberValue_daysSpentForEscorting(object o)
			{
				return ((EscortMerchantCaravanIssueBehavior.EscortMerchantCaravanIssueQuest)o)._daysSpentForEscorting;
			}

			// Token: 0x06005676 RID: 22134 RVA: 0x001968BC File Offset: 0x00194ABC
			internal static object AutoGeneratedGetMemberValue_questBanditPartyAlreadyAttacked(object o)
			{
				return ((EscortMerchantCaravanIssueBehavior.EscortMerchantCaravanIssueQuest)o)._questBanditPartyAlreadyAttacked;
			}

			// Token: 0x06005677 RID: 22135 RVA: 0x001968CE File Offset: 0x00194ACE
			internal static object AutoGeneratedGetMemberValue_playerStartsQuestLog(object o)
			{
				return ((EscortMerchantCaravanIssueBehavior.EscortMerchantCaravanIssueQuest)o)._playerStartsQuestLog;
			}

			// Token: 0x1700105C RID: 4188
			// (get) Token: 0x06005678 RID: 22136 RVA: 0x001968DB File Offset: 0x00194ADB
			private float BanditPartyAttackRadiusMin
			{
				get
				{
					return Campaign.Current.Models.EncounterModel.GetEncounterJoiningRadius * 2.5f;
				}
			}

			// Token: 0x1700105D RID: 4189
			// (get) Token: 0x06005679 RID: 22137 RVA: 0x001968F7 File Offset: 0x00194AF7
			private float QuestBanditPartySpawnDistance
			{
				get
				{
					return Campaign.Current.GetAverageDistanceBetweenClosestTwoTownsWithNavigationType(MobileParty.NavigationType.Default) * 1.25f;
				}
			}

			// Token: 0x1700105E RID: 4190
			// (get) Token: 0x0600567A RID: 22138 RVA: 0x0019690A File Offset: 0x00194B0A
			public override TextObject Title
			{
				get
				{
					return new TextObject("{=VpLzd69e}Escort Merchant Caravan", null);
				}
			}

			// Token: 0x1700105F RID: 4191
			// (get) Token: 0x0600567B RID: 22139 RVA: 0x00196917 File Offset: 0x00194B17
			public override bool IsRemainingTimeHidden
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001060 RID: 4192
			// (get) Token: 0x0600567C RID: 22140 RVA: 0x0019691A File Offset: 0x00194B1A
			private int BanditPartyTroopCount
			{
				get
				{
					return (int)MathF.Min(40f, (float)(MobileParty.MainParty.MemberRoster.TotalHealthyCount + this._questCaravanMobileParty.MemberRoster.TotalHealthyCount) * 0.7f);
				}
			}

			// Token: 0x17001061 RID: 4193
			// (get) Token: 0x0600567D RID: 22141 RVA: 0x0019694E File Offset: 0x00194B4E
			private int CaravanPartyTroopCount
			{
				get
				{
					return (int)(5f * this._difficultyMultiplier) + 10;
				}
			}

			// Token: 0x17001062 RID: 4194
			// (get) Token: 0x0600567E RID: 22142 RVA: 0x00196960 File Offset: 0x00194B60
			private bool CaravanIsInsideSettlement
			{
				get
				{
					return this._questCaravanMobileParty.CurrentSettlement != null;
				}
			}

			// Token: 0x17001063 RID: 4195
			// (get) Token: 0x0600567F RID: 22143 RVA: 0x00196970 File Offset: 0x00194B70
			private int TotalRewardGold
			{
				get
				{
					return MathF.Min(8000, this.RewardGold * this._daysSpentForEscorting);
				}
			}

			// Token: 0x17001064 RID: 4196
			// (get) Token: 0x06005680 RID: 22144 RVA: 0x00196989 File Offset: 0x00194B89
			private CustomPartyComponent CaravanCustomPartyComponent
			{
				get
				{
					if (this._customPartyComponent == null)
					{
						this._customPartyComponent = this._questCaravanMobileParty.PartyComponent as CustomPartyComponent;
					}
					return this._customPartyComponent;
				}
			}

			// Token: 0x17001065 RID: 4197
			// (get) Token: 0x06005681 RID: 22145 RVA: 0x001969B0 File Offset: 0x00194BB0
			private TextObject PlayerStartsQuestLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=YXbKXUDu}{ISSUE_GIVER.LINK}, a merchant from {SETTLEMENT}, has told you about {?ISSUE_GIVER.GENDER}her{?}his{\\?} recent problems with bandits. {?ISSUE_GIVER.GENDER}She{?}He{\\?} asked you to guard {?ISSUE_GIVER.GENDER}her{?}his{\\?} caravan for a while and deal with any attackers. In return {?ISSUE_GIVER.GENDER}she{?}he{\\?} offered you {GOLD}{GOLD_ICON} denars for each day you spend on escort duty.{newline}You have agreed to guard it yourself until it visits {NUMBER_OF_SETTLEMENTS} settlements.", null);
					StringHelpers.SetCharacterProperties("ISSUE_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("SETTLEMENT", Settlement.CurrentSettlement.Name);
					textObject.SetTextVariable("NUMBER_OF_SETTLEMENTS", this._requiredSettlementNumber);
					textObject.SetTextVariable("GOLD", this.RewardGold);
					textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"6\">");
					return textObject;
				}
			}

			// Token: 0x17001066 RID: 4198
			// (get) Token: 0x06005682 RID: 22146 RVA: 0x00196A2D File Offset: 0x00194C2D
			private TextObject CaravanDestroyedQuestLogText
			{
				get
				{
					return new TextObject("{=zk9QyKIz}The caravan was destroyed. Quest failed.", null);
				}
			}

			// Token: 0x17001067 RID: 4199
			// (get) Token: 0x06005683 RID: 22147 RVA: 0x00196A3C File Offset: 0x00194C3C
			private TextObject CaravanLostTheTrackLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=y62dyzH6}You have lost the track of caravan. Your agreement with {ISSUE_GIVER.LINK} is failed.", null);
					StringHelpers.SetCharacterProperties("ISSUE_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x17001068 RID: 4200
			// (get) Token: 0x06005684 RID: 22148 RVA: 0x00196A70 File Offset: 0x00194C70
			private TextObject CaravanDestroyedByBanditsLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=MhvyTcrH}The caravan is destroyed by some bandits. Your agreement with {ISSUE_GIVER.LINK} is failed.", null);
					StringHelpers.SetCharacterProperties("ISSUE_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x17001069 RID: 4201
			// (get) Token: 0x06005685 RID: 22149 RVA: 0x00196AA2 File Offset: 0x00194CA2
			private TextObject CaravanDestroyedByPlayerQuestLogText
			{
				get
				{
					return new TextObject("{=Rd3m5kyk}You have attacked the caravan.", null);
				}
			}

			// Token: 0x1700106A RID: 4202
			// (get) Token: 0x06005686 RID: 22150 RVA: 0x00196AB0 File Offset: 0x00194CB0
			private TextObject SuccessQuestLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=dKEADOhG}You have protected the caravan belonging to {QUEST_GIVER.LINK} from {SETTLEMENT} as promised. {?QUEST_GIVER.GENDER}She{?}He{\\?} was happy with your work.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("SETTLEMENT", base.QuestGiver.CurrentSettlement.Name);
					return textObject;
				}
			}

			// Token: 0x1700106B RID: 4203
			// (get) Token: 0x06005687 RID: 22151 RVA: 0x00196B00 File Offset: 0x00194D00
			private TextObject CancelByWarQuestLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=KhNkBd9O}Your clan is now at war with the {QUEST_GIVER.LINK}’s lord. Your agreement with {QUEST_GIVER.LINK} was canceled.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x06005688 RID: 22152 RVA: 0x00196B34 File Offset: 0x00194D34
			public EscortMerchantCaravanIssueQuest(string questId, Hero giverHero, CampaignTime duration, float difficultyMultiplier, int rewardGold)
				: base(questId, giverHero, duration, rewardGold)
			{
				this._difficultyMultiplier = difficultyMultiplier;
				this._requiredSettlementNumber = MathF.Round(2f + 4f * this._difficultyMultiplier);
				this._visitedSettlements = new List<Settlement>();
				this.SetDialogs();
				base.InitializeQuestOnCreation();
			}

			// Token: 0x06005689 RID: 22153 RVA: 0x00196B90 File Offset: 0x00194D90
			protected override void SetDialogs()
			{
				this.OfferDialogFlow = DialogFlow.CreateDialogFlow("issue_classic_quest_start", 100).NpcLine(new TextObject("{=TdwKwExD}Thank you. You can find the caravan just outside the settlement.[if:convo_grateful]", null), null, null, null, null).Condition(() => Hero.OneToOneConversationHero == base.QuestGiver)
					.Consequence(new ConversationSentence.OnConsequenceDelegate(this.QuestAcceptedConsequences))
					.CloseDialog();
				this.DiscussDialogFlow = DialogFlow.CreateDialogFlow("quest_discuss", 100).NpcLine(new TextObject("{=vtZYmAaR}I feel good knowing that you're looking after my caravan. Safe journeys, my friend![if:convo_grateful]", null), null, null, null, null).Condition(() => Hero.OneToOneConversationHero == base.QuestGiver)
					.CloseDialog();
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetCaravanPartyDialogFlow(), this);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetCaravanGreetingDialogFlow(), this);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetCaravanTradeDialogFlow(), this);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetCaravanLootDialogFlow(), this);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetCaravanFarewellDialogFlow(), this);
			}

			// Token: 0x1700106C RID: 4204
			// (get) Token: 0x0600568A RID: 22154 RVA: 0x00196C94 File Offset: 0x00194E94
			private TextObject CaravanNoTargetLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=1FOmvEdf}All profitable trade routes of the caravan are blocked by recent wars. {QUEST_GIVER.LINK} decided to recall the caravan until the situation gets better. {?QUEST_GIVER.GENDER}She{?}He{\\?} was happy with your service and sent you {REWARD}{GOLD_ICON} as promised.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("REWARD", this.TotalRewardGold);
					return textObject;
				}
			}

			// Token: 0x0600568B RID: 22155 RVA: 0x00196CD8 File Offset: 0x00194ED8
			private DialogFlow GetCaravanPartyDialogFlow()
			{
				TextObject textObject = new TextObject("{=ZAqEJI9T}About the task {QUEST_GIVER.LINK} gave me.", null);
				StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
				return DialogFlow.CreateDialogFlow("escort_caravan_talk", 125).BeginPlayerOptions(null, false).PlayerOption(textObject, null, null, null)
					.Condition(new ConversationSentence.OnConditionDelegate(this.caravan_talk_on_condition))
					.NpcLine("{=heWYa9Oq}I feel safe knowing that you're looking after us. Please continue to follow us my friend!", null, null, null, null)
					.Consequence(delegate
					{
						PlayerEncounter.LeaveEncounter = true;
					})
					.CloseDialog()
					.EndPlayerOptions();
			}

			// Token: 0x0600568C RID: 22156 RVA: 0x00196D74 File Offset: 0x00194F74
			private bool caravan_talk_on_condition()
			{
				bool flag = this._questCaravanMobileParty.MemberRoster.Contains(CharacterObject.OneToOneConversationCharacter) && this._questCaravanMobileParty == MobileParty.ConversationParty && MobileParty.ConversationParty != null && MobileParty.ConversationParty.IsCustomParty && !CharacterObject.OneToOneConversationCharacter.IsHero && MobileParty.ConversationParty.Party.Owner != Hero.MainHero;
				if (flag)
				{
					MBTextManager.SetTextVariable("HOMETOWN", MobileParty.ConversationParty.HomeSettlement.EncyclopediaLinkWithName, false);
					StringHelpers.SetCharacterProperties("MERCHANT", MobileParty.ConversationParty.Party.Owner.CharacterObject, null, false);
					StringHelpers.SetCharacterProperties("PROTECTOR", MobileParty.ConversationParty.HomeSettlement.OwnerClan.Leader.CharacterObject, null, false);
				}
				return flag;
			}

			// Token: 0x0600568D RID: 22157 RVA: 0x00196E44 File Offset: 0x00195044
			private DialogFlow GetCaravanFarewellDialogFlow()
			{
				TextObject textObject = new TextObject("{=1IJouNaM}Carry on, then. Farewell.", null);
				return DialogFlow.CreateDialogFlow("escort_caravan_talk", 125).BeginPlayerOptions(null, false).PlayerOption(textObject, null, null, null)
					.Condition(new ConversationSentence.OnConditionDelegate(this.caravan_talk_on_condition))
					.NpcLine("{=heWYa9Oq}I feel safe knowing that you're looking after us. Please continue to follow us my friend!", null, null, null, null)
					.Consequence(delegate
					{
						PlayerEncounter.LeaveEncounter = true;
					})
					.CloseDialog()
					.EndPlayerOptions();
			}

			// Token: 0x0600568E RID: 22158 RVA: 0x00196EC8 File Offset: 0x001950C8
			private DialogFlow GetCaravanLootDialogFlow()
			{
				TextObject textObject = new TextObject("{=WOBy5UfY}Hand over your goods, or die!", null);
				return DialogFlow.CreateDialogFlow("escort_caravan_talk", 125).BeginPlayerOptions(null, false).PlayerOption(textObject, null, null, null)
					.Condition(new ConversationSentence.OnConditionDelegate(this.caravan_loot_on_condition))
					.NpcLine("{=QNaKmkt9}We're paid to guard this caravan. If you want to rob it, it's going to be over our dead bodies![if:convo_angry][ib:aggressive]", null, null, null, null)
					.BeginPlayerOptions(null, false)
					.PlayerOption("{=EhxS7NQ4}So be it. Attack!", null, null, null)
					.Consequence(new ConversationSentence.OnConsequenceDelegate(this.conversation_caravan_fight_on_consequence))
					.CloseDialog()
					.PlayerOption("{=bfPsE9M1}You must have misunderstood me. Go in peace.", null, null, null)
					.Consequence(new ConversationSentence.OnConsequenceDelegate(this.caravan_talk_leave_on_consequence))
					.CloseDialog()
					.EndPlayerOptions()
					.EndPlayerOptions();
			}

			// Token: 0x0600568F RID: 22159 RVA: 0x00196F73 File Offset: 0x00195173
			private void conversation_caravan_fight_on_consequence()
			{
				BeHostileAction.ApplyEncounterHostileAction(PartyBase.MainParty, MobileParty.ConversationParty.Party);
			}

			// Token: 0x06005690 RID: 22160 RVA: 0x00196F89 File Offset: 0x00195189
			private void caravan_talk_leave_on_consequence()
			{
				if (PlayerEncounter.Current != null)
				{
					PlayerEncounter.LeaveEncounter = true;
				}
			}

			// Token: 0x06005691 RID: 22161 RVA: 0x00196F98 File Offset: 0x00195198
			private DialogFlow GetCaravanTradeDialogFlow()
			{
				TextObject textObject = new TextObject("{=t0UGXPV4}I'm interested in trading. What kind of products do you have?", null);
				return DialogFlow.CreateDialogFlow("escort_caravan_talk", 125).BeginPlayerOptions(null, false).PlayerOption(textObject, null, null, null)
					.Condition(new ConversationSentence.OnConditionDelegate(this.caravan_buy_products_on_condition))
					.NpcLine("{=tlLDHAIu}Very well. A pleasure doing business with you.[if:convo_relaxed_happy][ib:demure]", null, null, null, null)
					.Condition(new ConversationSentence.OnConditionDelegate(this.conversation_caravan_player_trade_end_on_condition))
					.NpcLine("{=DQBaaC0e}Is there anything else?", null, null, null, null)
					.GotoDialogState("escort_caravan_talk")
					.EndPlayerOptions();
			}

			// Token: 0x06005692 RID: 22162 RVA: 0x0019701C File Offset: 0x0019521C
			private bool caravan_buy_products_on_condition()
			{
				if (MobileParty.ConversationParty != null && MobileParty.ConversationParty == this._questCaravanMobileParty && !MobileParty.ConversationParty.IsCaravan)
				{
					for (int i = 0; i < MobileParty.ConversationParty.ItemRoster.Count; i++)
					{
						if (MobileParty.ConversationParty.ItemRoster.GetElementNumber(i) > 0)
						{
							return true;
						}
					}
				}
				return false;
			}

			// Token: 0x06005693 RID: 22163 RVA: 0x00197079 File Offset: 0x00195279
			private bool conversation_caravan_player_trade_end_on_condition()
			{
				if (MobileParty.ConversationParty != null && MobileParty.ConversationParty == this._questCaravanMobileParty && !MobileParty.ConversationParty.IsCaravan)
				{
					InventoryScreenHelper.OpenTradeWithCaravanOrAlleyParty(MobileParty.ConversationParty, InventoryScreenHelper.InventoryCategoryType.None);
				}
				return true;
			}

			// Token: 0x06005694 RID: 22164 RVA: 0x001970A8 File Offset: 0x001952A8
			private DialogFlow GetCaravanGreetingDialogFlow()
			{
				TextObject textObject = new TextObject("{=FpUybbSk}Greetings. This caravan is owned by {MERCHANT.LINK}. We trade under the protection of {PROTECTOR.LINK}, master of {HOMETOWN}. How may we help you?[if:convo_normal]", null);
				if (MobileParty.ConversationParty != null && MobileParty.ConversationParty.IsCurrentlyAtSea)
				{
					textObject = new TextObject("{=yGttYe7g}Greetings. This ship is owned by {MERCHANT.LINK}. We sail under the protection of {PROTECTOR.LINK}, master of {HOMETOWN}. How may we help you?[if:convo_normal]", null);
				}
				return DialogFlow.CreateDialogFlow("start", 125).NpcLine(textObject, null, null, null, null).Condition(new ConversationSentence.OnConditionDelegate(this.caravan_talk_on_condition))
					.GotoDialogState("escort_caravan_talk");
			}

			// Token: 0x06005695 RID: 22165 RVA: 0x00197111 File Offset: 0x00195311
			private void QuestAcceptedConsequences()
			{
				base.StartQuest();
				this.SpawnCaravan();
				this._playerStartsQuestLog = base.AddDiscreteLog(this.PlayerStartsQuestLogText, new TextObject("{=r2y3n7dR}Visited Settlements", null), this._visitedSettlements.Count, this._requiredSettlementNumber, null, false);
			}

			// Token: 0x06005696 RID: 22166 RVA: 0x00197150 File Offset: 0x00195350
			private bool caravan_loot_on_condition()
			{
				bool flag = MobileParty.ConversationParty != null && MobileParty.ConversationParty.Party.MapFaction != Hero.MainHero.MapFaction && !MobileParty.ConversationParty.IsCaravan && MobileParty.ConversationParty == this._questCaravanMobileParty;
				if (flag)
				{
					MBTextManager.SetTextVariable("HOMETOWN", MobileParty.ConversationParty.HomeSettlement.EncyclopediaLinkWithName, false);
					StringHelpers.SetCharacterProperties("MERCHANT", MobileParty.ConversationParty.Party.Owner.CharacterObject, null, false);
					StringHelpers.SetCharacterProperties("PROTECTOR", MobileParty.ConversationParty.HomeSettlement.OwnerClan.Leader.CharacterObject, null, false);
				}
				return flag;
			}

			// Token: 0x06005697 RID: 22167 RVA: 0x00197200 File Offset: 0x00195400
			private void SpawnCaravan()
			{
				ItemRoster itemRoster = new ItemRoster();
				foreach (ItemObject itemObject in EscortMerchantCaravanIssueBehavior.Instance.DefaultCaravanItems)
				{
					itemRoster.AddToCounts(itemObject, 7);
				}
				string text;
				string text2;
				this.GetAdditionalVisualsForParty(base.QuestGiver.Culture, out text, out text2);
				TextObject textObject = GameTexts.FindText("str_caravan_party_name", null);
				textObject.SetCharacterProperties("OWNER", base.QuestGiver.CharacterObject, false);
				this._questCaravanMobileParty = CustomPartyComponent.CreateCustomPartyWithTroopRoster(base.QuestGiver.CurrentSettlement.GatePosition, 0f, base.QuestGiver.CurrentSettlement, textObject, base.QuestGiver.Clan, TroopRoster.CreateDummyTroopRoster(), TroopRoster.CreateDummyTroopRoster(), base.QuestGiver, text, text2, 4f, false);
				this.InitializeCaravanOnCreation(this._questCaravanMobileParty, base.QuestGiver, base.QuestGiver.CurrentSettlement, itemRoster);
				base.AddTrackedObject(this._questCaravanMobileParty);
				this._questCaravanMobileParty.SetPartyUsedByQuest(true);
				this._questCaravanMobileParty.Ai.SetDoNotMakeNewDecisions(true);
				this._questCaravanMobileParty.IgnoreByOtherPartiesTill(base.QuestDueTime);
				this._caravanWaitedInSettlementForHours = 4;
			}

			// Token: 0x06005698 RID: 22168 RVA: 0x00197348 File Offset: 0x00195548
			private bool ProperSettlementCondition(Settlement settlement)
			{
				return settlement != Settlement.CurrentSettlement && settlement.IsTown && !settlement.IsUnderSiege && !this._visitedSettlements.Contains(settlement);
			}

			// Token: 0x06005699 RID: 22169 RVA: 0x00197374 File Offset: 0x00195574
			private void InitializeCaravanOnCreation(MobileParty mobileParty, Hero owner, Settlement settlement, ItemRoster caravanItems)
			{
				mobileParty.Aggressiveness = 0f;
				PartyTemplateObject randomCaravanTemplate = CaravanHelper.GetRandomCaravanTemplate(owner.Culture, false, true);
				mobileParty.InitializeMobilePartyAtPosition(TroopRoster.CreateDummyTroopRoster(), TroopRoster.CreateDummyTroopRoster(), settlement.GatePosition, false);
				MobilePartyHelper.FillPartyManuallyAfterCreation(mobileParty, randomCaravanTemplate, this.CaravanPartyTroopCount);
				CharacterObject characterObject = CharacterObject.All.First<CharacterObject>((CharacterObject character) => character.Occupation == Occupation.CaravanGuard && character.IsInfantry && character.Level == 26 && character.Culture == mobileParty.Party.Owner.Culture);
				mobileParty.MemberRoster.AddToCounts(characterObject, 1, true, 0, 0, true, -1);
				mobileParty.Party.SetVisualAsDirty();
				mobileParty.InitializePartyTrade(Campaign.Current.Models.CaravanModel.GetInitialTradeGold(owner, false, false));
				if (caravanItems != null)
				{
					mobileParty.ItemRoster.Add(caravanItems);
					return;
				}
				float num = 10000f;
				ItemObject itemObject = null;
				foreach (ItemObject itemObject2 in Items.All)
				{
					if (itemObject2.ItemCategory == DefaultItemCategories.PackAnimal && !itemObject2.NotMerchandise && (float)itemObject2.Value < num)
					{
						itemObject = itemObject2;
						num = (float)itemObject2.Value;
					}
				}
				if (itemObject != null)
				{
					mobileParty.ItemRoster.Add(new ItemRosterElement(itemObject, (int)((float)mobileParty.MemberRoster.TotalManCount * 0.5f), null));
				}
			}

			// Token: 0x0600569A RID: 22170 RVA: 0x00197500 File Offset: 0x00195700
			private void GetAdditionalVisualsForParty(CultureObject culture, out string mountStringId, out string harnessStringId)
			{
				if (culture.StringId == "aserai" || culture.StringId == "khuzait")
				{
					mountStringId = "camel";
					harnessStringId = ((MBRandom.RandomFloat > 0.5f) ? "camel_saddle_a" : "camel_saddle_b");
					return;
				}
				mountStringId = "mule";
				harnessStringId = ((MBRandom.RandomFloat > 0.5f) ? "mule_load_a" : ((MBRandom.RandomFloat > 0.5f) ? "mule_load_b" : "mule_load_c"));
			}

			// Token: 0x0600569B RID: 22171 RVA: 0x00197588 File Offset: 0x00195788
			protected override void RegisterEvents()
			{
				CampaignEvents.SettlementEntered.AddNonSerializedListener(this, new Action<MobileParty, Settlement, Hero>(this.OnSettlementEntered));
				CampaignEvents.OnSettlementLeftEvent.AddNonSerializedListener(this, new Action<MobileParty, Settlement>(this.OnSettlementLeft));
				CampaignEvents.MapEventEnded.AddNonSerializedListener(this, new Action<MapEvent>(this.OnMapEventEnded));
				CampaignEvents.WarDeclared.AddNonSerializedListener(this, new Action<IFaction, IFaction, DeclareWarAction.DeclareWarDetail>(this.OnWarDeclared));
				CampaignEvents.OnClanChangedKingdomEvent.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, ChangeKingdomAction.ChangeKingdomActionDetail, bool>(this.OnClanChangedKingdom));
				CampaignEvents.HourlyTickPartyEvent.AddNonSerializedListener(this, new Action<MobileParty>(this.OnPartyHourlyTick));
				CampaignEvents.OnSettlementOwnerChangedEvent.AddNonSerializedListener(this, new Action<Settlement, bool, Hero, Hero, Hero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail>(this.OnSettlementOwnerChanged));
			}

			// Token: 0x0600569C RID: 22172 RVA: 0x00197636 File Offset: 0x00195836
			private void OnPartyHourlyTick(MobileParty mobileParty)
			{
				this.CheckPartyAndMakeItAttackTheCaravan(mobileParty);
				this.CheckEncounterForBanditParty(this._questBanditMobileParty);
				this.CheckEncounterForBanditParty(this._otherBanditParty);
				this.CheckOtherBanditPartyDistance();
			}

			// Token: 0x0600569D RID: 22173 RVA: 0x00197660 File Offset: 0x00195860
			private void CheckOtherBanditPartyDistance()
			{
				if (base.IsOngoing)
				{
					if (this._otherBanditParty != null && this._otherBanditParty.IsActive && this._otherBanditParty.TargetParty == this._questCaravanMobileParty && this._otherBanditPartyFollowDuration < 0)
					{
						if (base.IsTracked(this._otherBanditParty))
						{
							base.RemoveTrackedObject(this._otherBanditParty);
						}
						this._otherBanditParty.SetMoveModeHold();
						this._otherBanditParty.Ai.SetDoNotMakeNewDecisions(false);
						this._otherBanditParty = null;
					}
					if (this._questBanditMobileParty != null && this._questBanditMobileParty.IsActive && this._questBanditMobileParty.MapEvent == null && this._questBanditMobileParty.TargetParty == this._questCaravanMobileParty && this._questBanditPartyFollowDuration < 0 && !this._questBanditMobileParty.IsVisible)
					{
						if (base.IsTracked(this._questBanditMobileParty))
						{
							base.RemoveTrackedObject(this._questBanditMobileParty);
						}
						this._questBanditMobileParty.SetMoveModeHold();
						this._questBanditMobileParty.Ai.SetDoNotMakeNewDecisions(false);
					}
				}
			}

			// Token: 0x0600569E RID: 22174 RVA: 0x00197768 File Offset: 0x00195968
			private void CheckEncounterForBanditParty(MobileParty mobileParty)
			{
				if (base.IsOngoing && mobileParty != null && mobileParty.IsActive && mobileParty.MapEvent == null && this._questCaravanMobileParty.IsActive && this._questCaravanMobileParty.MapEvent == null && this._questCaravanMobileParty.CurrentSettlement == null && mobileParty.Position.DistanceSquared(this._questCaravanMobileParty.Position) <= 1f)
				{
					EncounterManager.StartPartyEncounter(mobileParty.Party, this._questCaravanMobileParty.Party);
					MBInformationManager.AddQuickInformation(new TextObject("{=o8uAzFaJ}The caravan you are protecting is ambushed by raiders!", null), 0, null, null, "");
					this._questCaravanMobileParty.MapEvent.IsInvulnerable = true;
				}
			}

			// Token: 0x0600569F RID: 22175 RVA: 0x00197824 File Offset: 0x00195A24
			private void CheckPartyAndMakeItAttackTheCaravan(MobileParty mobileParty)
			{
				if (this._otherBanditParty == null && mobileParty != this._questBanditMobileParty && mobileParty.IsBandit && !mobileParty.IsCurrentlyUsedByAQuest && mobileParty.MapEvent == null && mobileParty.NavigationCapability == MobileParty.NavigationType.Default && mobileParty.Party.NumberOfHealthyMembers > this._questCaravanMobileParty.Party.NumberOfHealthyMembers && (mobileParty.Speed > this._questCaravanMobileParty.Speed || mobileParty.Position.DistanceSquared(this._questCaravanMobileParty.Position) < 9f))
				{
					Settlement settlement = this._visitedSettlements.LastOrDefault<Settlement>() ?? this._questCaravanMobileParty.HomeSettlement;
					Settlement targetSettlement = this._questCaravanMobileParty.TargetSettlement;
					if (targetSettlement == null)
					{
						this.TryToFindAndSetTargetToNextSettlement();
						return;
					}
					float num;
					float num2;
					if (this._questCaravanMobileParty.CurrentSettlement != null)
					{
						num = Campaign.Current.Models.MapDistanceModel.GetDistance(this._questCaravanMobileParty.CurrentSettlement, targetSettlement, false, false, MobileParty.NavigationType.Default);
						num2 = Campaign.Current.Models.MapDistanceModel.GetDistance(this._questCaravanMobileParty.CurrentSettlement, settlement, false, false, MobileParty.NavigationType.Default);
					}
					else
					{
						float num3;
						num = Campaign.Current.Models.MapDistanceModel.GetDistance(this._questCaravanMobileParty, targetSettlement, false, MobileParty.NavigationType.Default, out num3);
						num2 = Campaign.Current.Models.MapDistanceModel.GetDistance(this._questCaravanMobileParty, settlement, false, MobileParty.NavigationType.Default, out num3);
					}
					float num4 = mobileParty.Position.DistanceSquared(this._questCaravanMobileParty.Position);
					if (num > 5f && num2 > 5f && num4 < this.BanditPartyAttackRadiusMin * this.BanditPartyAttackRadiusMin)
					{
						SetPartyAiAction.GetActionForEngagingParty(mobileParty, this._questCaravanMobileParty, MobileParty.NavigationType.Default, false);
						mobileParty.Ai.SetDoNotMakeNewDecisions(true);
						if (!base.IsTracked(mobileParty))
						{
							base.AddTrackedObject(mobileParty);
						}
						float num5 = mobileParty.Speed + this._questCaravanMobileParty.Speed;
						this._otherBanditPartyFollowDuration = (int)(num4 / num5) + 5;
						this._otherBanditParty = mobileParty;
					}
				}
			}

			// Token: 0x060056A0 RID: 22176 RVA: 0x00197A28 File Offset: 0x00195C28
			private void OnSettlementEntered(MobileParty party, Settlement settlement, Hero hero)
			{
				if (party == this._questCaravanMobileParty && settlement != this._questCaravanMobileParty.HomeSettlement && settlement.Position.NearlyEquals(MobileParty.MainParty.Position.ToVec2(), MobileParty.MainParty.SeeingRange + 2f) && settlement == this._questCaravanMobileParty.TargetSettlement)
				{
					this._visitedSettlements.Add(settlement);
					base.UpdateQuestTaskStage(this._playerStartsQuestLog, this._visitedSettlements.Count);
					TextObject textObject = new TextObject("{=0wj3HIbh}Caravan entered {SETTLEMENT_LINK}.", null);
					textObject.SetTextVariable("SETTLEMENT_LINK", settlement.EncyclopediaLinkWithName);
					base.AddLog(textObject, true);
					if (this._questBanditMobileParty != null && this._questBanditMobileParty.IsActive)
					{
						if (base.IsTracked(this._questBanditMobileParty))
						{
							base.RemoveTrackedObject(this._questBanditMobileParty);
						}
						this._questBanditMobileParty.Ai.SetDoNotMakeNewDecisions(false);
						this._questBanditMobileParty.IgnoreByOtherPartiesTill(CampaignTime.Now);
						if (this._questBanditMobileParty.MapEvent == null)
						{
							SetPartyAiAction.GetActionForPatrollingAroundSettlement(this._questBanditMobileParty, settlement, MobileParty.NavigationType.Default, false, false);
						}
					}
					if (this._otherBanditParty != null)
					{
						if (base.IsTracked(this._otherBanditParty))
						{
							base.RemoveTrackedObject(this._otherBanditParty);
						}
						this._otherBanditParty.SetMoveModeHold();
						this._otherBanditParty.Ai.SetDoNotMakeNewDecisions(false);
						this._otherBanditParty = null;
					}
					if (this._visitedSettlements.Count == this._requiredSettlementNumber)
					{
						this.SuccessConsequences(false);
						return;
					}
					int num = this.CaravanPartyTroopCount - this._questCaravanMobileParty.MemberRoster.TotalManCount;
					if (num > 0)
					{
						this._questCaravanMobileParty.AddElementToMemberRoster(this._questCaravanMobileParty.TargetSettlement.Culture.CaravanGuard, MBRandom.RandomInt(Math.Min(15, num)), false);
					}
				}
			}

			// Token: 0x060056A1 RID: 22177 RVA: 0x00197BF9 File Offset: 0x00195DF9
			protected override void DailyTick()
			{
				this._daysSpentForEscorting++;
			}

			// Token: 0x060056A2 RID: 22178 RVA: 0x00197C09 File Offset: 0x00195E09
			private void OnSettlementOwnerChanged(Settlement settlement, bool openToClaim, Hero newOwner, Hero oldOwner, Hero capturerHero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail detail)
			{
				this.CheckWarDeclaration();
			}

			// Token: 0x060056A3 RID: 22179 RVA: 0x00197C11 File Offset: 0x00195E11
			private void OnClanChangedKingdom(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail detail, bool showNotification = true)
			{
				this.CheckWarDeclaration();
			}

			// Token: 0x060056A4 RID: 22180 RVA: 0x00197C19 File Offset: 0x00195E19
			private void CheckWarDeclaration()
			{
				if (base.QuestGiver.CurrentSettlement.MapFaction.IsAtWarWith(Hero.MainHero.MapFaction))
				{
					base.CompleteQuestWithCancel(this.CancelByWarQuestLogText);
				}
			}

			// Token: 0x060056A5 RID: 22181 RVA: 0x00197C48 File Offset: 0x00195E48
			private void OnWarDeclared(IFaction faction1, IFaction faction2, DeclareWarAction.DeclareWarDetail detail)
			{
				if (detail == DeclareWarAction.DeclareWarDetail.CausedByPlayerHostility && (faction1 == this._questCaravanMobileParty.MapFaction || faction2 == this._questCaravanMobileParty.MapFaction) && PlayerEncounter.Current != null && PlayerEncounter.PlayerIsAttacker)
				{
					this.FailByPlayerHostileConsequences();
				}
				else
				{
					this.CheckWarDeclaration();
				}
				if (this._questCaravanMobileParty != null && (this._questCaravanMobileParty.TargetSettlement == null || this._questCaravanMobileParty.TargetSettlement.MapFaction.IsAtWarWith(this._questCaravanMobileParty.MapFaction)) && base.IsOngoing)
				{
					this.TryToFindAndSetTargetToNextSettlement();
				}
			}

			// Token: 0x060056A6 RID: 22182 RVA: 0x00197CD8 File Offset: 0x00195ED8
			protected override void HourlyTick()
			{
				if (base.IsOngoing && this._questCaravanMobileParty.TargetSettlement == null)
				{
					this.TryToFindAndSetTargetToNextSettlement();
				}
				if (base.IsOngoing)
				{
					if (this.CaravanIsInsideSettlement)
					{
						this.SimulateSettlementWaitForCaravan();
					}
					else if (this._questCaravanMobileParty.MapEvent == null)
					{
						this.AdjustCaravansSpeed();
					}
					this.NotifyPlayerOrCancelTheQuestIfCaravanIsFar();
					if (base.IsOngoing)
					{
						this.ThinkAboutSpawningBanditParty();
						this.CheckCaravanMapEvent();
						this._otherBanditPartyFollowDuration--;
						this._questBanditPartyFollowDuration--;
					}
				}
			}

			// Token: 0x060056A7 RID: 22183 RVA: 0x00197D64 File Offset: 0x00195F64
			private void CheckCaravanMapEvent()
			{
				if (this._questCaravanMobileParty.MapEvent != null && this._questCaravanMobileParty.MapEvent.IsInvulnerable && this._questCaravanMobileParty.MapEvent.BattleStartTime.ElapsedHoursUntilNow > 3f)
				{
					this._questCaravanMobileParty.MapEvent.IsInvulnerable = false;
				}
			}

			// Token: 0x060056A8 RID: 22184 RVA: 0x00197DC0 File Offset: 0x00195FC0
			private void AdjustCaravansSpeed()
			{
				if (!MobileParty.MainParty.IsActive)
				{
					return;
				}
				float num = MobileParty.MainParty.Speed;
				float num2 = this._questCaravanMobileParty.Speed;
				while (num < num2 || num - num2 > 1f)
				{
					if (num2 >= num)
					{
						this.CaravanCustomPartyComponent.SetBaseSpeed(this.CaravanCustomPartyComponent.BaseSpeed - 0.05f);
					}
					else if (num - num2 > 1f)
					{
						this.CaravanCustomPartyComponent.SetBaseSpeed(this.CaravanCustomPartyComponent.BaseSpeed + 0.05f);
					}
					num = MobileParty.MainParty.Speed;
					num2 = this._questCaravanMobileParty.Speed;
				}
			}

			// Token: 0x060056A9 RID: 22185 RVA: 0x00197E60 File Offset: 0x00196060
			private void ThinkAboutSpawningBanditParty()
			{
				if (!this._questBanditPartyAlreadyAttacked && this._questBanditMobileParty == null)
				{
					Settlement targetSettlement = this._questCaravanMobileParty.TargetSettlement;
					if (targetSettlement != null)
					{
						float num2;
						float num = ((this._questCaravanMobileParty.CurrentSettlement != null) ? Campaign.Current.Models.MapDistanceModel.GetDistance(this._questCaravanMobileParty.CurrentSettlement, targetSettlement, false, false, MobileParty.NavigationType.Default) : Campaign.Current.Models.MapDistanceModel.GetDistance(this._questCaravanMobileParty, targetSettlement, false, MobileParty.NavigationType.Default, out num2));
						if (num > 10f && num < this.QuestBanditPartySpawnDistance)
						{
							this.ActivateBanditParty();
							float num3 = this._questBanditMobileParty.Speed + this._questCaravanMobileParty.Speed;
							this._questBanditPartyFollowDuration = (int)(this.QuestBanditPartySpawnDistance / num3) + 5;
							this._questBanditPartyAlreadyAttacked = true;
						}
					}
				}
			}

			// Token: 0x060056AA RID: 22186 RVA: 0x00197F2E File Offset: 0x0019612E
			private void SimulateSettlementWaitForCaravan()
			{
				this._caravanWaitedInSettlementForHours++;
				if (this._caravanWaitedInSettlementForHours >= 5)
				{
					LeaveSettlementAction.ApplyForParty(this._questCaravanMobileParty);
					this._caravanWaitedInSettlementForHours = 0;
				}
			}

			// Token: 0x060056AB RID: 22187 RVA: 0x00197F5C File Offset: 0x0019615C
			private void NotifyPlayerOrCancelTheQuestIfCaravanIsFar()
			{
				if (this._questCaravanMobileParty.IsActive && !this._questCaravanMobileParty.IsVisible)
				{
					float num = this._questCaravanMobileParty.Position.Distance(MobileParty.MainParty.Position);
					if (!this._isPlayerNotifiedForDanger && num >= MobileParty.MainParty.SeeingRange + 3f)
					{
						MBInformationManager.AddQuickInformation(new TextObject("{=2y9DhzCR}You are about to lose sight of the caravan. Find the caravan before they are in danger!", null), 0, null, null, "");
						this._isPlayerNotifiedForDanger = true;
						return;
					}
					if (num >= MobileParty.MainParty.SeeingRange + 20f)
					{
						base.AddLog(this.CaravanLostTheTrackLogText, false);
						this.FailConsequences(false);
					}
				}
			}

			// Token: 0x060056AC RID: 22188 RVA: 0x0019800C File Offset: 0x0019620C
			private void OnSettlementLeft(MobileParty party, Settlement settlement)
			{
				if (party == this._questCaravanMobileParty)
				{
					this.AdjustCaravansSpeed();
					if (party.TargetSettlement == null || party.TargetSettlement == settlement)
					{
						this.TryToFindAndSetTargetToNextSettlement();
					}
					this._caravanWaitedInSettlementForHours = 0;
					this._questBanditPartyAlreadyAttacked = false;
					this._questCaravanMobileParty.Party.SetAsCameraFollowParty();
					if (base.IsTracked(settlement))
					{
						base.RemoveTrackedObject(settlement);
					}
				}
			}

			// Token: 0x060056AD RID: 22189 RVA: 0x00198070 File Offset: 0x00196270
			private void TryToFindAndSetTargetToNextSettlement()
			{
				int num = 0;
				int num2 = -1;
				do
				{
					num2 = SettlementHelper.FindNextSettlementAroundMobileParty(this._questCaravanMobileParty, MobileParty.NavigationType.Default, 150f, num2, null);
					if (num2 >= 0)
					{
						Settlement settlement = Settlement.All[num2];
						if (this.ProperSettlementCondition(settlement) && settlement != this._questCaravanMobileParty.HomeSettlement && (this._visitedSettlements.Count == 0 || settlement != this._visitedSettlements[this._visitedSettlements.Count - 1]) && !settlement.MapFaction.IsAtWarWith(this._questCaravanMobileParty.MapFaction))
						{
							num++;
						}
					}
				}
				while (num2 >= 0);
				if (num > 0)
				{
					int num3 = MBRandom.RandomInt(num);
					num2 = -1;
					Settlement settlement2;
					for (;;)
					{
						num2 = SettlementHelper.FindNextSettlementAroundMobileParty(this._questCaravanMobileParty, MobileParty.NavigationType.Default, 150f, num2, null);
						if (num2 >= 0)
						{
							settlement2 = Settlement.All[num2];
							if (this.ProperSettlementCondition(settlement2) && settlement2 != this._questCaravanMobileParty.HomeSettlement && (this._visitedSettlements.Count == 0 || settlement2 != this._visitedSettlements[this._visitedSettlements.Count - 1]) && !settlement2.MapFaction.IsAtWarWith(this._questCaravanMobileParty.MapFaction))
							{
								num3--;
								if (num3 < 0)
								{
									break;
								}
							}
						}
						if (num2 < 0)
						{
							return;
						}
					}
					Settlement settlement3 = settlement2;
					SetPartyAiAction.GetActionForVisitingSettlement(this._questCaravanMobileParty, settlement3, MobileParty.NavigationType.Default, false, false);
					this._questCaravanMobileParty.Ai.SetDoNotMakeNewDecisions(true);
					TextObject textObject = new TextObject("{=OjI8uGFa}We are traveling to {SETTLEMENT_NAME}.", null);
					textObject.SetTextVariable("SETTLEMENT_NAME", settlement3.Name);
					MBInformationManager.AddQuickInformation(textObject, 100, PartyBaseHelper.GetVisualPartyLeader(this._questCaravanMobileParty.Party), null, "");
					TextObject textObject2 = new TextObject("{=QDpfYm4c}The caravan is moving to {SETTLEMENT_NAME}.", null);
					textObject2.SetTextVariable("SETTLEMENT_NAME", settlement3.EncyclopediaLinkWithName);
					base.AddLog(textObject2, true);
					if (!base.IsTracked(settlement3))
					{
						base.AddTrackedObject(settlement3);
					}
					if (this._questBanditMobileParty == null || !this._questBanditMobileParty.IsActive)
					{
						return;
					}
					float num4 = DistanceHelper.FindClosestDistanceFromMobilePartyToMobileParty(this._questCaravanMobileParty, this._questBanditMobileParty, MobileParty.NavigationType.Default);
					if (this._questBanditMobileParty.Speed < this._questCaravanMobileParty.Speed || num4 > 10f)
					{
						this._questBanditMobileParty.SetMoveModeHold();
						this._questBanditMobileParty.Ai.SetDoNotMakeNewDecisions(false);
						this._questBanditMobileParty.IgnoreByOtherPartiesTill(CampaignTime.Now);
						if (base.IsTracked(this._questBanditMobileParty))
						{
							base.RemoveTrackedObject(this._questBanditMobileParty);
						}
						this._questBanditMobileParty = null;
						return;
					}
					return;
				}
				this.CaravanNoTargetQuestSuccess();
			}

			// Token: 0x060056AE RID: 22190 RVA: 0x001982FA File Offset: 0x001964FA
			private void CaravanNoTargetQuestSuccess()
			{
				this.SuccessConsequences(true);
			}

			// Token: 0x060056AF RID: 22191 RVA: 0x00198304 File Offset: 0x00196504
			private void OnMapEventEnded(MapEvent mapEvent)
			{
				if (this._questCaravanMobileParty != null && mapEvent.InvolvedParties.Contains(this._questCaravanMobileParty.Party))
				{
					if (mapEvent.HasWinner)
					{
						bool flag = this._questCaravanMobileParty.MapEventSide == MobileParty.MainParty.MapEventSide && mapEvent.IsPlayerMapEvent;
						bool flag2 = mapEvent.Winner == this._questCaravanMobileParty.MapEventSide;
						bool flag3 = mapEvent.InvolvedParties.Contains(PartyBase.MainParty);
						if (!flag2)
						{
							if (!flag3)
							{
								base.AddLog(this.CaravanDestroyedByBanditsLogText, false);
								this.FailConsequences(true);
								return;
							}
							if (flag)
							{
								base.AddLog(this.CaravanDestroyedQuestLogText, false);
								this.FailConsequences(true);
								return;
							}
							this.FailByPlayerHostileConsequences();
							return;
						}
						else
						{
							if (this._questBanditMobileParty != null && this._questBanditMobileParty.IsActive && mapEvent.InvolvedParties.Contains(this._questBanditMobileParty.Party))
							{
								DestroyPartyAction.Apply(MobileParty.MainParty.Party, this._questBanditMobileParty);
							}
							if (this._otherBanditParty != null && this._otherBanditParty.IsActive && mapEvent.InvolvedParties.Contains(this._otherBanditParty.Party))
							{
								DestroyPartyAction.Apply(MobileParty.MainParty.Party, this._otherBanditParty);
							}
							if (this._questCaravanMobileParty.MemberRoster.TotalManCount <= 0)
							{
								this.FailConsequences(true);
							}
							if (this._questCaravanMobileParty.Speed < 2f)
							{
								this._questCaravanMobileParty.ItemRoster.AddToCounts(MBObjectManager.Instance.GetObject<ItemObject>("sumpter_horse"), 5);
								return;
							}
						}
					}
					else if (this._questCaravanMobileParty.MemberRoster.TotalManCount <= 0)
					{
						this.FailConsequences(true);
					}
				}
			}

			// Token: 0x060056B0 RID: 22192 RVA: 0x001984B0 File Offset: 0x001966B0
			private void SuccessConsequences(bool isNoTargetLeftSuccess)
			{
				GiveGoldAction.ApplyBetweenCharacters(null, Hero.MainHero, this.TotalRewardGold, false);
				base.QuestGiver.AddPower(10f);
				this.RelationshipChangeWithQuestGiver = 5;
				TraitLevelingHelper.OnIssueSolvedThroughQuest(base.QuestGiver, new Tuple<TraitObject, int>[]
				{
					new Tuple<TraitObject, int>(DefaultTraits.Honor, 50)
				});
				base.QuestGiver.CurrentSettlement.Town.Prosperity += 10f;
				if (isNoTargetLeftSuccess)
				{
					base.AddLog(this.CaravanNoTargetLogText, false);
				}
				else
				{
					base.AddLog(this.SuccessQuestLogText, true);
				}
				MobileParty questBanditMobileParty = this._questBanditMobileParty;
				if (questBanditMobileParty != null)
				{
					questBanditMobileParty.Ai.SetDoNotMakeNewDecisions(false);
				}
				base.CompleteQuestWithSuccess();
			}

			// Token: 0x060056B1 RID: 22193 RVA: 0x00198568 File Offset: 0x00196768
			private void FailConsequences(bool banditsWon = false)
			{
				base.QuestGiver.AddPower(-10f);
				this.RelationshipChangeWithQuestGiver = -5;
				TraitLevelingHelper.OnIssueFailed(base.QuestGiver, new Tuple<TraitObject, int>[]
				{
					new Tuple<TraitObject, int>(DefaultTraits.Honor, -20)
				});
				base.QuestGiver.CurrentSettlement.Town.Prosperity -= 10f;
				if (this._questBanditMobileParty != null)
				{
					this._questBanditMobileParty.Ai.SetDoNotMakeNewDecisions(false);
					this._questBanditMobileParty.IgnoreByOtherPartiesTill(CampaignTime.Now);
					if (base.IsTracked(this._questBanditMobileParty))
					{
						base.RemoveTrackedObject(this._questBanditMobileParty);
					}
				}
				if (this._questCaravanMobileParty != null)
				{
					this._questCaravanMobileParty.Ai.SetDoNotMakeNewDecisions(false);
					this._questCaravanMobileParty.IgnoreByOtherPartiesTill(CampaignTime.Now);
				}
				if (this._questBanditMobileParty != null && !banditsWon)
				{
					if (base.IsTracked(this._questBanditMobileParty))
					{
						base.RemoveTrackedObject(this._questBanditMobileParty);
					}
					this._questBanditMobileParty.SetPartyUsedByQuest(false);
					this._questBanditMobileParty.IgnoreByOtherPartiesTill(CampaignTime.Now);
					if (this._questBanditMobileParty.IsActive && this._questBanditMobileParty.IsVisible)
					{
						DestroyPartyAction.Apply(null, this._questBanditMobileParty);
					}
				}
				base.CompleteQuestWithFail(null);
			}

			// Token: 0x060056B2 RID: 22194 RVA: 0x001986A8 File Offset: 0x001968A8
			private void FailByPlayerHostileConsequences()
			{
				base.QuestGiver.AddPower(-10f);
				this.RelationshipChangeWithQuestGiver = -10;
				TraitLevelingHelper.OnIssueFailed(base.QuestGiver, new Tuple<TraitObject, int>[]
				{
					new Tuple<TraitObject, int>(DefaultTraits.Honor, -80)
				});
				base.QuestGiver.CurrentSettlement.Town.Prosperity -= 20f;
				base.AddLog(this.CaravanDestroyedByPlayerQuestLogText, true);
				MobileParty questBanditMobileParty = this._questBanditMobileParty;
				if (questBanditMobileParty != null)
				{
					questBanditMobileParty.Ai.SetDoNotMakeNewDecisions(false);
				}
				base.CompleteQuestWithFail(null);
			}

			// Token: 0x060056B3 RID: 22195 RVA: 0x0019873A File Offset: 0x0019693A
			protected override void InitializeQuestOnGameLoad()
			{
				MobileParty questCaravanMobileParty = this._questCaravanMobileParty;
				if (questCaravanMobileParty != null && questCaravanMobileParty.IsCaravan)
				{
					base.CompleteQuestWithCancel(null);
				}
				this.SetDialogs();
			}

			// Token: 0x060056B4 RID: 22196 RVA: 0x00198760 File Offset: 0x00196960
			private void ActivateBanditParty()
			{
				Hideout closestHideout = SettlementHelper.FindNearestHideoutToMobileParty(this._questCaravanMobileParty, this._questCaravanMobileParty.NavigationCapability, (Settlement x) => x.IsActive);
				Clan clan = Clan.BanditFactions.FirstOrDefault<Clan>((Clan t) => t.Culture == closestHideout.Settlement.Culture);
				PartyTemplateObject partyTemplateObject = Campaign.Current.ObjectManager.GetObject<PartyTemplateObject>("kingdom_hero_party_caravan_ambushers") ?? clan.DefaultPartyTemplate;
				this._questBanditMobileParty = BanditPartyComponent.CreateBanditParty("escort_caravan_quest_" + base.StringId, clan, closestHideout.Settlement.Hideout, false, partyTemplateObject, this._questCaravanMobileParty.TargetSettlement.GatePosition);
				this._questBanditMobileParty.Party.SetCustomName(new TextObject("{=u1Pkt4HC}Raiders", null));
				Campaign.Current.MobilePartyLocator.UpdateLocator(this._questBanditMobileParty);
				this._questBanditMobileParty.ActualClan = clan;
				this._questBanditMobileParty.MemberRoster.Clear();
				for (int i = 0; i < this.BanditPartyTroopCount; i++)
				{
					List<ValueTuple<PartyTemplateStack, float>> list = new List<ValueTuple<PartyTemplateStack, float>>();
					foreach (PartyTemplateStack partyTemplateStack in partyTemplateObject.Stacks)
					{
						list.Add(new ValueTuple<PartyTemplateStack, float>(partyTemplateStack, (float)(64 - partyTemplateStack.Character.Level)));
					}
					PartyTemplateStack partyTemplateStack2 = MBRandom.ChooseWeighted<PartyTemplateStack>(list);
					this._questBanditMobileParty.MemberRoster.AddToCounts(partyTemplateStack2.Character, 1, false, 0, 0, true, -1);
				}
				this._questBanditMobileParty.ItemRoster.AddToCounts(DefaultItems.Grain, this.BanditPartyTroopCount);
				this._questBanditMobileParty.ItemRoster.AddToCounts(MBObjectManager.Instance.GetObject<ItemObject>("sumpter_horse"), this.BanditPartyTroopCount);
				this._questBanditMobileParty.IgnoreByOtherPartiesTill(base.QuestDueTime);
				SetPartyAiAction.GetActionForEngagingParty(this._questBanditMobileParty, this._questCaravanMobileParty, MobileParty.NavigationType.Default, false);
				this._questBanditMobileParty.Ai.SetDoNotMakeNewDecisions(true);
				base.AddTrackedObject(this._questBanditMobileParty);
			}

			// Token: 0x060056B5 RID: 22197 RVA: 0x00198998 File Offset: 0x00196B98
			protected override void OnFinalize()
			{
				if (this._questCaravanMobileParty != null && this._questCaravanMobileParty.IsActive && this._questCaravanMobileParty.IsCustomParty)
				{
					CaravanPartyComponent.ConvertPartyToCaravanParty(this._questCaravanMobileParty, base.QuestGiver, base.QuestGiver.CurrentSettlement, false, null, null, false);
					this._questCaravanMobileParty.Ai.SetDoNotMakeNewDecisions(false);
					this._questCaravanMobileParty.IgnoreByOtherPartiesTill(CampaignTime.Now);
				}
				if (this._questCaravanMobileParty != null)
				{
					base.RemoveTrackedObject(this._questCaravanMobileParty);
				}
				if (this._otherBanditParty != null && this._otherBanditParty.IsActive)
				{
					this._otherBanditParty.Ai.SetDoNotMakeNewDecisions(false);
					this._otherBanditParty.IgnoreByOtherPartiesTill(CampaignTime.Now);
				}
			}

			// Token: 0x060056B6 RID: 22198 RVA: 0x00198A54 File Offset: 0x00196C54
			protected override void OnTimedOut()
			{
				base.QuestGiver.AddPower(-5f);
				this.RelationshipChangeWithQuestGiver = -5;
				TraitLevelingHelper.OnIssueFailed(base.QuestGiver, new Tuple<TraitObject, int>[]
				{
					new Tuple<TraitObject, int>(DefaultTraits.Honor, -20)
				});
				base.QuestGiver.CurrentSettlement.Town.Prosperity -= 20f;
				base.AddLog(new TextObject("{=pUrSIed8}You have failed to escort the caravan to its destination.", null), false);
			}

			// Token: 0x04001C5E RID: 7262
			private const int BattleFakeSimulationDuration = 3;

			// Token: 0x04001C5F RID: 7263
			private const string CustomPartyComponentTalkId = "escort_caravan_talk";

			// Token: 0x04001C60 RID: 7264
			[SaveableField(2)]
			private readonly int _requiredSettlementNumber;

			// Token: 0x04001C61 RID: 7265
			[SaveableField(3)]
			private List<Settlement> _visitedSettlements;

			// Token: 0x04001C62 RID: 7266
			[SaveableField(4)]
			private MobileParty _questCaravanMobileParty;

			// Token: 0x04001C63 RID: 7267
			[SaveableField(5)]
			private MobileParty _questBanditMobileParty;

			// Token: 0x04001C64 RID: 7268
			[SaveableField(7)]
			private readonly float _difficultyMultiplier;

			// Token: 0x04001C65 RID: 7269
			[SaveableField(12)]
			private bool _isPlayerNotifiedForDanger;

			// Token: 0x04001C66 RID: 7270
			[SaveableField(26)]
			private MobileParty _otherBanditParty;

			// Token: 0x04001C67 RID: 7271
			[SaveableField(30)]
			private int _questBanditPartyFollowDuration;

			// Token: 0x04001C68 RID: 7272
			[SaveableField(31)]
			private int _otherBanditPartyFollowDuration;

			// Token: 0x04001C69 RID: 7273
			[SaveableField(11)]
			private int _daysSpentForEscorting = 1;

			// Token: 0x04001C6A RID: 7274
			private int _caravanWaitedInSettlementForHours;

			// Token: 0x04001C6B RID: 7275
			[SaveableField(23)]
			private bool _questBanditPartyAlreadyAttacked;

			// Token: 0x04001C6C RID: 7276
			private CustomPartyComponent _customPartyComponent;

			// Token: 0x04001C6D RID: 7277
			[SaveableField(1)]
			private JournalLog _playerStartsQuestLog;
		}

		// Token: 0x02000700 RID: 1792
		public class EscortMerchantCaravanIssueTypeDefiner : SaveableTypeDefiner
		{
			// Token: 0x060056B9 RID: 22201 RVA: 0x00198AEB File Offset: 0x00196CEB
			public EscortMerchantCaravanIssueTypeDefiner()
				: base(450000)
			{
			}

			// Token: 0x060056BA RID: 22202 RVA: 0x00198AF8 File Offset: 0x00196CF8
			protected override void DefineClassTypes()
			{
				base.AddClassDefinition(typeof(EscortMerchantCaravanIssueBehavior.EscortMerchantCaravanIssue), 1, null);
				base.AddClassDefinition(typeof(EscortMerchantCaravanIssueBehavior.EscortMerchantCaravanIssueQuest), 2, null);
			}
		}
	}
}
