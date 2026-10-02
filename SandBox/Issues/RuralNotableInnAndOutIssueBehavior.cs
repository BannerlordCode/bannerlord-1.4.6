using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using SandBox.BoardGames.MissionLogics;
using SandBox.CampaignBehaviors;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.Issues;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.SaveSystem;

namespace SandBox.Issues
{
	// Token: 0x020000B9 RID: 185
	public class RuralNotableInnAndOutIssueBehavior : CampaignBehaviorBase
	{
		// Token: 0x060007AE RID: 1966 RVA: 0x0003404C File Offset: 0x0003224C
		public override void RegisterEvents()
		{
			CampaignEvents.OnCheckForIssueEvent.AddNonSerializedListener(this, new Action<Hero>(this.OnCheckForIssue));
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00034065 File Offset: 0x00032265
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00034068 File Offset: 0x00032268
		private bool ConditionsHold(Hero issueGiver)
		{
			return (issueGiver.IsRuralNotable || issueGiver.IsHeadman) && issueGiver.CurrentSettlement.Village != null && issueGiver.CurrentSettlement.Village.Bound.IsTown && issueGiver.GetTraitLevel(DefaultTraits.Mercy) + issueGiver.GetTraitLevel(DefaultTraits.Honor) < 0 && Campaign.Current.GetCampaignBehavior<BoardGameCampaignBehavior>() != null && issueGiver.CurrentSettlement.Village.Bound.Culture.BoardGame != CultureObject.BoardGameType.None;
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x000340F4 File Offset: 0x000322F4
		public void OnCheckForIssue(Hero hero)
		{
			if (this.ConditionsHold(hero))
			{
				Campaign.Current.IssueManager.AddPotentialIssueData(hero, new PotentialIssueData(new PotentialIssueData.StartIssueDelegate(this.OnSelected), typeof(RuralNotableInnAndOutIssueBehavior.RuralNotableInnAndOutIssue), IssueBase.IssueFrequency.Common, null));
				return;
			}
			Campaign.Current.IssueManager.AddPotentialIssueData(hero, new PotentialIssueData(typeof(RuralNotableInnAndOutIssueBehavior.RuralNotableInnAndOutIssue), IssueBase.IssueFrequency.Common));
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00034158 File Offset: 0x00032358
		private IssueBase OnSelected(in PotentialIssueData pid, Hero issueOwner)
		{
			return new RuralNotableInnAndOutIssueBehavior.RuralNotableInnAndOutIssue(issueOwner);
		}

		// Token: 0x0400041B RID: 1051
		private const IssueBase.IssueFrequency RuralNotableInnAndOutIssueFrequency = IssueBase.IssueFrequency.Common;

		// Token: 0x0400041C RID: 1052
		private const float IssueDuration = 30f;

		// Token: 0x0400041D RID: 1053
		private const float QuestDuration = 14f;

		// Token: 0x020001CF RID: 463
		public class RuralNotableInnAndOutIssueTypeDefiner : SaveableTypeDefiner
		{
			// Token: 0x060011DF RID: 4575 RVA: 0x00072AF5 File Offset: 0x00070CF5
			public RuralNotableInnAndOutIssueTypeDefiner()
				: base(585900)
			{
			}

			// Token: 0x060011E0 RID: 4576 RVA: 0x00072B02 File Offset: 0x00070D02
			protected override void DefineClassTypes()
			{
				base.AddClassDefinition(typeof(RuralNotableInnAndOutIssueBehavior.RuralNotableInnAndOutIssue), 1, null);
				base.AddClassDefinition(typeof(RuralNotableInnAndOutIssueBehavior.RuralNotableInnAndOutIssueQuest), 2, null);
			}
		}

		// Token: 0x020001D0 RID: 464
		public class RuralNotableInnAndOutIssue : IssueBase
		{
			// Token: 0x170001D5 RID: 469
			// (get) Token: 0x060011E1 RID: 4577 RVA: 0x00072B28 File Offset: 0x00070D28
			public override IssueBase.AlternativeSolutionScaleFlag AlternativeSolutionScaleFlags
			{
				get
				{
					return IssueBase.AlternativeSolutionScaleFlag.FailureRisk;
				}
			}

			// Token: 0x170001D6 RID: 470
			// (get) Token: 0x060011E2 RID: 4578 RVA: 0x00072B2B File Offset: 0x00070D2B
			protected override bool IssueQuestCanBeDuplicated
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170001D7 RID: 471
			// (get) Token: 0x060011E3 RID: 4579 RVA: 0x00072B2E File Offset: 0x00070D2E
			public override int AlternativeSolutionBaseNeededMenCount
			{
				get
				{
					return 1 + MathF.Ceiling(3f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x170001D8 RID: 472
			// (get) Token: 0x060011E4 RID: 4580 RVA: 0x00072B43 File Offset: 0x00070D43
			protected override int AlternativeSolutionBaseDurationInDaysInternal
			{
				get
				{
					return 1 + MathF.Ceiling(3f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x170001D9 RID: 473
			// (get) Token: 0x060011E5 RID: 4581 RVA: 0x00072B58 File Offset: 0x00070D58
			protected override int RewardGold
			{
				get
				{
					return 1000;
				}
			}

			// Token: 0x170001DA RID: 474
			// (get) Token: 0x060011E6 RID: 4582 RVA: 0x00072B5F File Offset: 0x00070D5F
			public override TextObject Title
			{
				get
				{
					return new TextObject("{=uUhtKnfA}Inn and Out", null);
				}
			}

			// Token: 0x170001DB RID: 475
			// (get) Token: 0x060011E7 RID: 4583 RVA: 0x00072B6C File Offset: 0x00070D6C
			public override TextObject Description
			{
				get
				{
					TextObject textObject = new TextObject("{=swamqBRq}{ISSUE_OWNER.NAME} wants you to beat the game host", null);
					StringHelpers.SetCharacterProperties("ISSUE_OWNER", base.IssueOwner.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x170001DC RID: 476
			// (get) Token: 0x060011E8 RID: 4584 RVA: 0x00072B9E File Offset: 0x00070D9E
			public override TextObject IssueBriefByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=T0zupcGB}Ah yes... It is a bit embarrassing to mention, [ib:nervous][if:convo_nervous]but... Well, when I am in town, I often have a drink at the inn and perhaps play a round of {GAME_TYPE} or two. Normally I play for low stakes but let's just say that last time the wine went to my head, and I lost something I couldn't afford to lose.", null);
					textObject.SetTextVariable("GAME_TYPE", GameTexts.FindText("str_boardgame_name", this._boardGameType.ToString()));
					return textObject;
				}
			}

			// Token: 0x170001DD RID: 477
			// (get) Token: 0x060011E9 RID: 4585 RVA: 0x00072BD2 File Offset: 0x00070DD2
			public override TextObject IssueAcceptByPlayer
			{
				get
				{
					return new TextObject("{=h2tMadtI}I've heard that story before. What did you lose?", null);
				}
			}

			// Token: 0x170001DE RID: 478
			// (get) Token: 0x060011EA RID: 4586 RVA: 0x00072BE0 File Offset: 0x00070DE0
			public override TextObject IssueQuestSolutionExplanationByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=LD4tGYCA}It's a deed to a plot of farmland. Not a big or valuable plot,[ib:normal][if:convo_disbelief] mind you, but I'd rather not have to explain to my men why they won't be sowing it this year. You can find the man who took it from me at the tavern in {TARGET_SETTLEMENT}. They call him the \"Game Host\". Just be straight about what you're doing. He's in no position to work the land. I don't imagine that he'll turn down a chance to make more money off of it. Bring it back and {REWARD}{GOLD_ICON} is yours.", null);
					textObject.SetTextVariable("REWARD", this.RewardGold);
					textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"6\">");
					textObject.SetTextVariable("TARGET_SETTLEMENT", this._targetSettlement.Name);
					return textObject;
				}
			}

			// Token: 0x170001DF RID: 479
			// (get) Token: 0x060011EB RID: 4587 RVA: 0x00072C32 File Offset: 0x00070E32
			public override TextObject IssueAlternativeSolutionExplanationByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=urCXu9Fc}Well, I could try and buy it from him, but I would not really prefer that.[if:convo_innocent_smile] I would be the joke of the tavern for months to come... If you choose to do that, I can only offer {REWARD}{GOLD_ICON} to compensate for your payment. If you have a man with a knack for such games he might do the trick.", null);
					textObject.SetTextVariable("REWARD", this.RewardGold);
					textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"6\">");
					return textObject;
				}
			}

			// Token: 0x170001E0 RID: 480
			// (get) Token: 0x060011EC RID: 4588 RVA: 0x00072C62 File Offset: 0x00070E62
			public override TextObject IssueQuestSolutionAcceptByPlayer
			{
				get
				{
					return new TextObject("{=KMThnMbt}I'll go to the tavern and win it back the same way you lost it.", null);
				}
			}

			// Token: 0x170001E1 RID: 481
			// (get) Token: 0x060011ED RID: 4589 RVA: 0x00072C70 File Offset: 0x00070E70
			public override TextObject IssueAlternativeSolutionAcceptByPlayer
			{
				get
				{
					TextObject textObject = new TextObject("{=QdKWaabR}Worry not {ISSUE_OWNER.NAME}, my men will be back with your deed in no time.", null);
					StringHelpers.SetCharacterProperties("ISSUE_OWNER", base.IssueOwner.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x170001E2 RID: 482
			// (get) Token: 0x060011EE RID: 4590 RVA: 0x00072CA2 File Offset: 0x00070EA2
			public override TextObject IssueDiscussAlternativeSolution
			{
				get
				{
					return new TextObject("{=1yEyUHJe}I really hope your men can get my deed back. [if:convo_excited]On my father's name, I will never gamble again.", null);
				}
			}

			// Token: 0x170001E3 RID: 483
			// (get) Token: 0x060011EF RID: 4591 RVA: 0x00072CB0 File Offset: 0x00070EB0
			public override TextObject IssueAlternativeSolutionResponseByIssueGiver
			{
				get
				{
					TextObject textObject = new TextObject("{=kiaN39yb}Thank you, {PLAYER.NAME}. I'm sure your companion will be persuasive.[if:convo_relaxed_happy]", null);
					StringHelpers.SetCharacterProperties("PLAYER", CharacterObject.PlayerCharacter, textObject, false);
					return textObject;
				}
			}

			// Token: 0x170001E4 RID: 484
			// (get) Token: 0x060011F0 RID: 4592 RVA: 0x00072CDC File Offset: 0x00070EDC
			public override bool IsThereAlternativeSolution
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170001E5 RID: 485
			// (get) Token: 0x060011F1 RID: 4593 RVA: 0x00072CDF File Offset: 0x00070EDF
			public override bool IsThereLordSolution
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170001E6 RID: 486
			// (get) Token: 0x060011F2 RID: 4594 RVA: 0x00072CE4 File Offset: 0x00070EE4
			protected override TextObject AlternativeSolutionStartLog
			{
				get
				{
					TextObject textObject = new TextObject("{=MIxzaqzi}{QUEST_GIVER.LINK} told you that he lost a land deed in a wager in {TARGET_CITY}. He needs to buy it back, and he wants your companions to intimidate the seller into offering a reasonable price. You asked {COMPANION.LINK} to take {TROOP_COUNT} of your men to go and take care of it. They should report back to you in {RETURN_DAYS} days.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.IssueOwner.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("COMPANION", base.AlternativeSolutionHero.CharacterObject, textObject, false);
					textObject.SetTextVariable("TARGET_CITY", this._targetSettlement.EncyclopediaLinkWithName);
					textObject.SetTextVariable("RETURN_DAYS", base.GetTotalAlternativeSolutionDurationInDays());
					textObject.SetTextVariable("TROOP_COUNT", this.AlternativeSolutionSentTroops.TotalManCount - 1);
					return textObject;
				}
			}

			// Token: 0x060011F3 RID: 4595 RVA: 0x00072D70 File Offset: 0x00070F70
			public RuralNotableInnAndOutIssue(Hero issueOwner)
				: base(issueOwner, CampaignTime.DaysFromNow(30f))
			{
				this.InitializeQuestVariables();
			}

			// Token: 0x060011F4 RID: 4596 RVA: 0x00072D89 File Offset: 0x00070F89
			protected override float GetIssueEffectAmountInternal(IssueEffect issueEffect)
			{
				if (issueEffect == DefaultIssueEffects.SettlementProsperity)
				{
					return -0.1f;
				}
				if (issueEffect == DefaultIssueEffects.IssueOwnerPower)
				{
					return -0.1f;
				}
				return 0f;
			}

			// Token: 0x060011F5 RID: 4597 RVA: 0x00072DAC File Offset: 0x00070FAC
			public override ValueTuple<SkillObject, int> GetAlternativeSolutionSkill(Hero hero)
			{
				return new ValueTuple<SkillObject, int>((hero.GetSkillValue(DefaultSkills.Charm) >= hero.GetSkillValue(DefaultSkills.Tactics)) ? DefaultSkills.Charm : DefaultSkills.Tactics, 120);
			}

			// Token: 0x060011F6 RID: 4598 RVA: 0x00072DD9 File Offset: 0x00070FD9
			public override bool AlternativeSolutionCondition(out TextObject explanation)
			{
				return QuestHelper.CheckRosterForAlternativeSolution(MobileParty.MainParty.MemberRoster, base.GetTotalAlternativeSolutionNeededMenCount(), out explanation, 0, false) && QuestHelper.CheckGoldForAlternativeSolution(1000, out explanation);
			}

			// Token: 0x170001E7 RID: 487
			// (get) Token: 0x060011F7 RID: 4599 RVA: 0x00072E02 File Offset: 0x00071002
			protected override int CompanionSkillRewardXP
			{
				get
				{
					return (int)(500f + 1000f * base.IssueDifficultyMultiplier);
				}
			}

			// Token: 0x060011F8 RID: 4600 RVA: 0x00072E18 File Offset: 0x00071018
			protected override void AlternativeSolutionEndWithSuccessConsequence()
			{
				this.RelationshipChangeWithIssueOwner = 5;
				GainRenownAction.Apply(Hero.MainHero, 5f, false);
				base.IssueOwner.CurrentSettlement.Village.Bound.Town.Loyalty += 5f;
			}

			// Token: 0x060011F9 RID: 4601 RVA: 0x00072E67 File Offset: 0x00071067
			protected override void AlternativeSolutionEndWithFailureConsequence()
			{
				this.RelationshipChangeWithIssueOwner -= 5;
				base.IssueOwner.CurrentSettlement.Village.Bound.Town.Loyalty -= 5f;
			}

			// Token: 0x060011FA RID: 4602 RVA: 0x00072EA2 File Offset: 0x000710A2
			public override bool DoTroopsSatisfyAlternativeSolution(TroopRoster troopRoster, out TextObject explanation)
			{
				return QuestHelper.CheckRosterForAlternativeSolution(troopRoster, base.GetTotalAlternativeSolutionNeededMenCount(), out explanation, 0, false);
			}

			// Token: 0x060011FB RID: 4603 RVA: 0x00072EB3 File Offset: 0x000710B3
			public override IssueBase.IssueFrequency GetFrequency()
			{
				return IssueBase.IssueFrequency.Common;
			}

			// Token: 0x060011FC RID: 4604 RVA: 0x00072EB8 File Offset: 0x000710B8
			public override bool IssueStayAliveConditions()
			{
				BoardGameCampaignBehavior campaignBehavior = Campaign.Current.GetCampaignBehavior<BoardGameCampaignBehavior>();
				return campaignBehavior != null && !campaignBehavior.WonBoardGamesInOneWeekInSettlement.Contains(this._targetSettlement) && !base.IssueOwner.CurrentSettlement.IsRaided && !base.IssueOwner.CurrentSettlement.IsUnderRaid;
			}

			// Token: 0x060011FD RID: 4605 RVA: 0x00072F0D File Offset: 0x0007110D
			protected override void CompleteIssueWithTimedOutConsequences()
			{
			}

			// Token: 0x060011FE RID: 4606 RVA: 0x00072F0F File Offset: 0x0007110F
			private void InitializeQuestVariables()
			{
				this._targetSettlement = base.IssueOwner.CurrentSettlement.Village.Bound;
				this._boardGameType = this._targetSettlement.Culture.BoardGame;
			}

			// Token: 0x060011FF RID: 4607 RVA: 0x00072F42 File Offset: 0x00071142
			protected override void OnGameLoad()
			{
				this.InitializeQuestVariables();
			}

			// Token: 0x06001200 RID: 4608 RVA: 0x00072F4A File Offset: 0x0007114A
			protected override void HourlyTick()
			{
			}

			// Token: 0x06001201 RID: 4609 RVA: 0x00072F4C File Offset: 0x0007114C
			protected override QuestBase GenerateIssueQuest(string questId)
			{
				return new RuralNotableInnAndOutIssueBehavior.RuralNotableInnAndOutIssueQuest(questId, base.IssueOwner, CampaignTime.DaysFromNow(14f), this.RewardGold);
			}

			// Token: 0x06001202 RID: 4610 RVA: 0x00072F6C File Offset: 0x0007116C
			protected override bool CanPlayerTakeQuestConditions(Hero issueGiver, out IssueBase.PreconditionFlags flag, out Hero relationHero, out SkillObject skill, out int requiredGold)
			{
				skill = null;
				relationHero = null;
				requiredGold = 0;
				flag = IssueBase.PreconditionFlags.None;
				if (issueGiver.GetRelationWithPlayer() < -10f)
				{
					flag |= IssueBase.PreconditionFlags.Relation;
					relationHero = issueGiver;
				}
				if (FactionManager.IsAtWarAgainstFaction(issueGiver.CurrentSettlement.MapFaction, Hero.MainHero.MapFaction))
				{
					flag |= IssueBase.PreconditionFlags.AtWar;
				}
				if (Hero.MainHero.Gold < 2000)
				{
					requiredGold = 2000;
					flag |= IssueBase.PreconditionFlags.Money;
				}
				return flag == IssueBase.PreconditionFlags.None;
			}

			// Token: 0x06001203 RID: 4611 RVA: 0x00072FE4 File Offset: 0x000711E4
			internal static void AutoGeneratedStaticCollectObjectsRuralNotableInnAndOutIssue(object o, List<object> collectedObjects)
			{
				((RuralNotableInnAndOutIssueBehavior.RuralNotableInnAndOutIssue)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x06001204 RID: 4612 RVA: 0x00072FF2 File Offset: 0x000711F2
			protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				base.AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x040008A5 RID: 2213
			private const int CompanionSkillLimit = 120;

			// Token: 0x040008A6 RID: 2214
			private const int QuestMoneyLimit = 2000;

			// Token: 0x040008A7 RID: 2215
			private const int AlternativeSolutionGoldCost = 1000;

			// Token: 0x040008A8 RID: 2216
			private CultureObject.BoardGameType _boardGameType;

			// Token: 0x040008A9 RID: 2217
			private Settlement _targetSettlement;
		}

		// Token: 0x020001D1 RID: 465
		public class RuralNotableInnAndOutIssueQuest : QuestBase
		{
			// Token: 0x170001E8 RID: 488
			// (get) Token: 0x06001205 RID: 4613 RVA: 0x00072FFC File Offset: 0x000711FC
			private TextObject QuestStartLog
			{
				get
				{
					TextObject textObject = new TextObject("{=tirG1BB2}{QUEST_GIVER.LINK} told you that he lost a land deed while playing games in a tavern in {TARGET_SETTLEMENT}. He wants you to go find the game host and win it back for him. You told him that you will take care of the situation yourself.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("TARGET_SETTLEMENT", this._targetSettlement.EncyclopediaLinkWithName);
					return textObject;
				}
			}

			// Token: 0x170001E9 RID: 489
			// (get) Token: 0x06001206 RID: 4614 RVA: 0x00073048 File Offset: 0x00071248
			private TextObject SuccessLog
			{
				get
				{
					TextObject textObject = new TextObject("{=bvhWLb4C}You defeated the Game Host and got the deed back. {QUEST_GIVER.LINK}.{newline}\"Thank you for resolving this issue so neatly. Please accept these {GOLD}{GOLD_ICON} denars with our gratitude.\"", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("GOLD", this.RewardGold);
					textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"6\">");
					return textObject;
				}
			}

			// Token: 0x170001EA RID: 490
			// (get) Token: 0x06001207 RID: 4615 RVA: 0x000730A0 File Offset: 0x000712A0
			private TextObject SuccessWithPayingLog
			{
				get
				{
					TextObject textObject = new TextObject("{=TIPxWsYW}You have bought the deed from the game host. {QUEST_GIVER.LINK}.{newline}\"I am happy that I got my land back. I'm not so happy that everyone knows I had to pay for it, but... Anyway, please accept these {GOLD}{GOLD_ICON} denars with my gratitude.\"", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("GOLD", 800);
					textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"6\">");
					return textObject;
				}
			}

			// Token: 0x170001EB RID: 491
			// (get) Token: 0x06001208 RID: 4616 RVA: 0x000730F4 File Offset: 0x000712F4
			private TextObject LostLog
			{
				get
				{
					TextObject textObject = new TextObject("{=ye4oqBFB}You lost the board game and failed to help {QUEST_GIVER.LINK}. \"Thank you for trying, {PLAYER.NAME}, but I guess I chose the wrong person for the job.\"", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					StringHelpers.SetCharacterProperties("PLAYER", CharacterObject.PlayerCharacter, textObject, false);
					return textObject;
				}
			}

			// Token: 0x170001EC RID: 492
			// (get) Token: 0x06001209 RID: 4617 RVA: 0x00073138 File Offset: 0x00071338
			private TextObject QuestCanceledTargetVillageRaided
			{
				get
				{
					TextObject textObject = new TextObject("{=DLesz9jI}{QUEST_GIVER.LINK}’s village is raided. {?QUEST_GIVER.GENDER}She{?}He{\\?} flees to the countryside, and your agreement with {?QUEST_GIVER.GENDER}her{?}him{\\?} is canceled.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("SETTLEMENT", this._targetSettlement.EncyclopediaLinkWithName);
					return textObject;
				}
			}

			// Token: 0x170001ED RID: 493
			// (get) Token: 0x0600120A RID: 4618 RVA: 0x00073181 File Offset: 0x00071381
			private TextObject QuestCanceledWarDeclared
			{
				get
				{
					TextObject textObject = new TextObject("{=cKz1cyuM}Your clan is now at war with {QUEST_GIVER_SETTLEMENT_FACTION}. Quest is canceled.", null);
					textObject.SetTextVariable("QUEST_GIVER_SETTLEMENT_FACTION", base.QuestGiver.CurrentSettlement.MapFaction.Name);
					return textObject;
				}
			}

			// Token: 0x170001EE RID: 494
			// (get) Token: 0x0600120B RID: 4619 RVA: 0x000731B0 File Offset: 0x000713B0
			private TextObject PlayerDeclaredWarQuestLogText
			{
				get
				{
					TextObject textObject = new TextObject("{=bqeWVVEE}Your actions have started a war with {QUEST_GIVER.LINK}'s faction. {?QUEST_GIVER.GENDER}She{?}He{\\?} cancels your agreement and the quest is a failure.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x170001EF RID: 495
			// (get) Token: 0x0600120C RID: 4620 RVA: 0x000731E4 File Offset: 0x000713E4
			private TextObject QuestCanceledSettlementIsUnderSiege
			{
				get
				{
					TextObject textObject = new TextObject("{=b5LdBYpF}{SETTLEMENT} is under siege. Your agreement with {QUEST_GIVER.LINK} is canceled.", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					textObject.SetTextVariable("SETTLEMENT", this._targetSettlement.EncyclopediaLinkWithName);
					return textObject;
				}
			}

			// Token: 0x170001F0 RID: 496
			// (get) Token: 0x0600120D RID: 4621 RVA: 0x00073230 File Offset: 0x00071430
			private TextObject TimeoutLog
			{
				get
				{
					TextObject textObject = new TextObject("{=XLy8anVr}You received a message from {QUEST_GIVER.LINK}. \"This may not have seemed like an important task, but I placed my trust in you. I guess I was wrong to do so.\"", null);
					StringHelpers.SetCharacterProperties("QUEST_GIVER", base.QuestGiver.CharacterObject, textObject, false);
					return textObject;
				}
			}

			// Token: 0x170001F1 RID: 497
			// (get) Token: 0x0600120E RID: 4622 RVA: 0x00073262 File Offset: 0x00071462
			public override TextObject Title
			{
				get
				{
					return new TextObject("{=uUhtKnfA}Inn and Out", null);
				}
			}

			// Token: 0x170001F2 RID: 498
			// (get) Token: 0x0600120F RID: 4623 RVA: 0x0007326F File Offset: 0x0007146F
			public override bool IsRemainingTimeHidden
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001210 RID: 4624 RVA: 0x00073272 File Offset: 0x00071472
			public RuralNotableInnAndOutIssueQuest(string questId, Hero giverHero, CampaignTime duration, int rewardGold)
				: base(questId, giverHero, duration, rewardGold)
			{
				this.InitializeQuestVariables();
				this.SetDialogs();
				base.InitializeQuestOnCreation();
			}

			// Token: 0x06001211 RID: 4625 RVA: 0x00073291 File Offset: 0x00071491
			private void InitializeQuestVariables()
			{
				this._targetSettlement = base.QuestGiver.CurrentSettlement.Village.Bound;
				this._boardGameType = this._targetSettlement.Culture.BoardGame;
			}

			// Token: 0x06001212 RID: 4626 RVA: 0x000732C4 File Offset: 0x000714C4
			private void QuestAcceptedConsequences()
			{
				base.StartQuest();
				base.AddLog(this.QuestStartLog, false);
				base.AddTrackedObject(this._targetSettlement);
			}

			// Token: 0x06001213 RID: 4627 RVA: 0x000732E6 File Offset: 0x000714E6
			protected override void InitializeQuestOnGameLoad()
			{
				this.InitializeQuestVariables();
				this.SetDialogs();
				if (Campaign.Current.GetCampaignBehavior<BoardGameCampaignBehavior>() == null)
				{
					base.CompleteQuestWithCancel(null);
				}
			}

			// Token: 0x06001214 RID: 4628 RVA: 0x00073307 File Offset: 0x00071507
			protected override void HourlyTick()
			{
			}

			// Token: 0x06001215 RID: 4629 RVA: 0x0007330C File Offset: 0x0007150C
			protected override void RegisterEvents()
			{
				CampaignEvents.OnPlayerBoardGameOverEvent.AddNonSerializedListener(this, new Action<Hero, BoardGameHelper.BoardGameState>(this.OnBoardGameEnd));
				CampaignEvents.WarDeclared.AddNonSerializedListener(this, new Action<IFaction, IFaction, DeclareWarAction.DeclareWarDetail>(this.OnWarDeclared));
				CampaignEvents.OnClanChangedKingdomEvent.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, ChangeKingdomAction.ChangeKingdomActionDetail, bool>(this.OnClanChangedKingdom));
				CampaignEvents.OnSiegeEventStartedEvent.AddNonSerializedListener(this, new Action<SiegeEvent>(this.OnSiegeStarted));
				CampaignEvents.MapEventStarted.AddNonSerializedListener(this, new Action<MapEvent, PartyBase, PartyBase>(this.OnMapEventStarted));
				CampaignEvents.VillageBeingRaided.AddNonSerializedListener(this, new Action<Village>(this.OnVillageBeingRaided));
				CampaignEvents.LocationCharactersSimulatedEvent.AddNonSerializedListener(this, new Action(this.OnLocationCharactersSimulated));
			}

			// Token: 0x06001216 RID: 4630 RVA: 0x000733BC File Offset: 0x000715BC
			private void OnLocationCharactersSimulated()
			{
				if (Settlement.CurrentSettlement != null && Settlement.CurrentSettlement == this._targetSettlement && Campaign.Current.GameMenuManager.MenuLocations.Count > 0 && Campaign.Current.GameMenuManager.MenuLocations[0].StringId == "tavern")
				{
					foreach (Agent agent in Mission.Current.Agents)
					{
						LocationCharacter locationCharacter = LocationComplex.Current.GetLocationWithId("tavern").GetLocationCharacter(agent.Origin);
						if (locationCharacter != null && locationCharacter.Character.Occupation == Occupation.TavernGameHost)
						{
							locationCharacter.IsVisualTracked = true;
						}
					}
				}
			}

			// Token: 0x06001217 RID: 4631 RVA: 0x0007349C File Offset: 0x0007169C
			private void OnMapEventStarted(MapEvent mapEvent, PartyBase attackerParty, PartyBase defenderParty)
			{
				if (QuestHelper.CheckMinorMajorCoercion(this, mapEvent, attackerParty))
				{
					QuestHelper.ApplyGenericMinorMajorCoercionConsequences(this, mapEvent);
				}
			}

			// Token: 0x06001218 RID: 4632 RVA: 0x000734AF File Offset: 0x000716AF
			private void OnVillageBeingRaided(Village village)
			{
				if (village == base.QuestGiver.CurrentSettlement.Village)
				{
					base.CompleteQuestWithCancel(this.QuestCanceledTargetVillageRaided);
				}
			}

			// Token: 0x06001219 RID: 4633 RVA: 0x000734D0 File Offset: 0x000716D0
			private void OnBoardGameEnd(Hero opposingHero, BoardGameHelper.BoardGameState state)
			{
				if (this._checkForBoardGameEnd)
				{
					this._playerWonTheGame = state == BoardGameHelper.BoardGameState.Win;
				}
			}

			// Token: 0x0600121A RID: 4634 RVA: 0x000734E4 File Offset: 0x000716E4
			private void OnSiegeStarted(SiegeEvent siegeEvent)
			{
				if (siegeEvent.BesiegedSettlement == this._targetSettlement)
				{
					base.CompleteQuestWithCancel(this.QuestCanceledSettlementIsUnderSiege);
				}
			}

			// Token: 0x0600121B RID: 4635 RVA: 0x00073500 File Offset: 0x00071700
			protected override void SetDialogs()
			{
				TextObject textObject = new TextObject("{=I6amLvVE}Good, good. That's the best way to do these things. [if:convo_normal]Go to {TARGET_SETTLEMENT}, find this game host and wipe the smirk off of his face.", null);
				textObject.SetTextVariable("TARGET_SETTLEMENT", this._targetSettlement.Name);
				this.OfferDialogFlow = DialogFlow.CreateDialogFlow("issue_classic_quest_start", 100).NpcLine(textObject, null, null, null, null).Condition(() => Hero.OneToOneConversationHero == base.QuestGiver)
					.Consequence(new ConversationSentence.OnConsequenceDelegate(this.QuestAcceptedConsequences))
					.CloseDialog();
				this.DiscussDialogFlow = DialogFlow.CreateDialogFlow("quest_discuss", 100).NpcLine(new TextObject("{=HGRWs0zE}Have you met the man who took my deed? Did you get it back?[if:convo_astonished]", null), null, null, null, null).Condition(() => Hero.OneToOneConversationHero == base.QuestGiver)
					.BeginPlayerOptions(null, false)
					.PlayerOption(new TextObject("{=uJPAYUU7}I will be on my way soon enough.", null), null, null, null)
					.NpcLine(new TextObject("{=MOmePlJQ}Could you hurry this along? I don't want him to find another buyer.[if:convo_pondering] Thank you.", null), null, null, null, null)
					.CloseDialog()
					.PlayerOption(new TextObject("{=azVhRGik}I am waiting for the right moment.", null), null, null, null)
					.NpcLine(new TextObject("{=bRMLn0jj}Well, if he wanders off to another town, or gets his throat slit,[if:convo_pondering] or loses the deed, that would be the wrong moment, now wouldn't it?", null), null, null, null, null)
					.CloseDialog()
					.EndPlayerOptions();
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetGameHostDialogFlow(), this);
				Campaign.Current.ConversationManager.AddDialogFlow(this.GetGameHostDialogueAfterFirstGame(), this);
			}

			// Token: 0x0600121C RID: 4636 RVA: 0x0007363C File Offset: 0x0007183C
			private DialogFlow GetGameHostDialogFlow()
			{
				return DialogFlow.CreateDialogFlow("start", 125).NpcLine("{=dzWioKRa}Hello there, are you looking for a friendly match? A wager perhaps?[if:convo_mocking_aristocratic]", null, null, null, null).Condition(() => this.TavernHostDialogCondition(true))
					.PlayerLine(new TextObject("{=eOle8pYT}You won a deed of land from my associate. I'm here to win it back.", null), null, null, null)
					.NpcLine("{=bEipgE5E}Ah, yes, these are the most interesting kinds of games, aren't they? [if:convo_excited]I won't deny myself the pleasure but clearly that deed is worth more to him than just the value of the land. I'll wager the deed, but you need to put up 1000 denars.", null, null, null, null)
					.BeginPlayerOptions(null, false)
					.PlayerOption("{=XvkSbY6N}I see your wager. Let's play.", null, null, null)
					.Condition(() => Hero.MainHero.Gold >= 1000)
					.Consequence(new ConversationSentence.OnConsequenceDelegate(this.StartBoardGame))
					.CloseDialog()
					.PlayerOption("{=89b5ao7P}As of now, I do not have 1000 denars to afford on gambling. I may get back to you once I get the required amount.", null, null, null)
					.Condition(() => Hero.MainHero.Gold < 1000)
					.NpcLine(new TextObject("{=ppi6eVos}As you wish.", null), null, null, null, null)
					.CloseDialog()
					.PlayerOption("{=WrnvRayQ}Let's just save ourselves some trouble, and I'll just pay you that amount.", null, null, null)
					.ClickableCondition(new ConversationSentence.OnClickableConditionDelegate(this.CheckPlayerHasEnoughDenarsClickableCondition))
					.NpcLine("{=pa3RY39w}Sure. I'm happy to turn paper into silver... 1000 denars it is.[if:convo_evil_smile]", null, null, null, null)
					.Consequence(new ConversationSentence.OnConsequenceDelegate(this.PlayerPaid1000QuestSuccess))
					.CloseDialog()
					.PlayerOption("{=BSeplVwe}That's too much. I will be back later.", null, null, null)
					.CloseDialog()
					.EndPlayerOptions()
					.CloseDialog();
			}

			// Token: 0x0600121D RID: 4637 RVA: 0x0007378C File Offset: 0x0007198C
			private DialogFlow GetGameHostDialogueAfterFirstGame()
			{
				return DialogFlow.CreateDialogFlow("start", 125).BeginNpcOptions(null, false).NpcOption(new TextObject("{=dyhZUHao}Well, I thought you were here to be sheared, [if:convo_shocked]but it looks like the sheep bites back. Very well, nicely played, here's your man's land back.", null), () => this._playerWonTheGame && this.TavernHostDialogCondition(false), null, null, null, null)
					.Consequence(new ConversationSentence.OnConsequenceDelegate(this.PlayerWonTheBoardGame))
					.CloseDialog()
					.NpcOption("{=TdnD29Ax}Ah! You almost had me! Maybe you just weren't paying attention. [if:convo_mocking_teasing]Care to put another 1000 denars on the table and have another go?", () => !this._playerWonTheGame && this._tryCount < 2 && this.TavernHostDialogCondition(false), null, null, null, null)
					.BeginPlayerOptions(null, false)
					.PlayerOption("{=fiMZ696A}Yes, I'll play again.", null, null, null)
					.ClickableCondition(new ConversationSentence.OnClickableConditionDelegate(this.CheckPlayerHasEnoughDenarsClickableCondition))
					.Consequence(new ConversationSentence.OnConsequenceDelegate(this.StartBoardGame))
					.CloseDialog()
					.PlayerOption("{=zlFSIvD5}No, no. I know a trap when I see one. You win. Good-bye.", null, null, null)
					.NpcLine(new TextObject("{=ppi6eVos}As you wish.", null), null, null, null, null)
					.Consequence(new ConversationSentence.OnConsequenceDelegate(this.PlayerFailAfterBoardGame))
					.CloseDialog()
					.EndPlayerOptions()
					.NpcOption("{=hkNrC5d3}That was fun, but I've learned not to inflict too great a humiliation on those who carry a sword.[if:convo_merry] I'll take my winnings and enjoy them now. Good-bye to you!", () => this._tryCount >= 2 && this.TavernHostDialogCondition(false), null, null, null, null)
					.Consequence(new ConversationSentence.OnConsequenceDelegate(this.PlayerFailAfterBoardGame))
					.CloseDialog()
					.EndNpcOptions();
			}

			// Token: 0x0600121E RID: 4638 RVA: 0x000738A8 File Offset: 0x00071AA8
			private bool CheckPlayerHasEnoughDenarsClickableCondition(out TextObject explanation)
			{
				if (Hero.MainHero.Gold >= 1000)
				{
					explanation = null;
					return true;
				}
				explanation = new TextObject("{=AMlaYbJv}You don't have 1000 denars.", null);
				return false;
			}

			// Token: 0x0600121F RID: 4639 RVA: 0x000738D0 File Offset: 0x00071AD0
			private bool TavernHostDialogCondition(bool isInitialDialogue = false)
			{
				if ((!this._checkForBoardGameEnd || !isInitialDialogue) && Settlement.CurrentSettlement == this._targetSettlement && CharacterObject.OneToOneConversationCharacter.Occupation == Occupation.TavernGameHost)
				{
					LocationComplex locationComplex = LocationComplex.Current;
					if (((locationComplex != null) ? locationComplex.GetLocationWithId("tavern") : null) != null)
					{
						Mission.Current.GetMissionBehavior<MissionBoardGameLogic>().DetectOpposingAgent();
						return Mission.Current.GetMissionBehavior<MissionBoardGameLogic>().CheckIfBothSidesAreSitting();
					}
				}
				return false;
			}

			// Token: 0x06001220 RID: 4640 RVA: 0x0007393B File Offset: 0x00071B3B
			private void PlayerPaid1000QuestSuccess()
			{
				base.AddLog(this.SuccessWithPayingLog, false);
				this._applyLesserReward = true;
				GiveGoldAction.ApplyBetweenCharacters(Hero.MainHero, null, 1000, false);
				base.CompleteQuestWithSuccess();
			}

			// Token: 0x06001221 RID: 4641 RVA: 0x0007396C File Offset: 0x00071B6C
			protected override void OnFinalize()
			{
				if (Mission.Current != null)
				{
					foreach (Agent agent in Mission.Current.Agents)
					{
						Location locationWithId = LocationComplex.Current.GetLocationWithId("tavern");
						if (locationWithId != null)
						{
							LocationCharacter locationCharacter = locationWithId.GetLocationCharacter(agent.Origin);
							if (locationCharacter != null && locationCharacter.Character.Occupation == Occupation.TavernGameHost)
							{
								locationCharacter.IsVisualTracked = false;
							}
						}
					}
				}
			}

			// Token: 0x06001222 RID: 4642 RVA: 0x000739FC File Offset: 0x00071BFC
			private void ApplySuccessRewards()
			{
				GiveGoldAction.ApplyBetweenCharacters(null, Hero.MainHero, this._applyLesserReward ? 800 : this.RewardGold, false);
				ChangeRelationAction.ApplyPlayerRelation(base.QuestGiver, 5, true, true);
				GainRenownAction.Apply(Hero.MainHero, 1f, false);
				base.QuestGiver.CurrentSettlement.Village.Bound.Town.Loyalty += 5f;
			}

			// Token: 0x06001223 RID: 4643 RVA: 0x00073A73 File Offset: 0x00071C73
			protected override void OnCompleteWithSuccess()
			{
				this.ApplySuccessRewards();
			}

			// Token: 0x06001224 RID: 4644 RVA: 0x00073A7C File Offset: 0x00071C7C
			private void StartBoardGame()
			{
				MissionBoardGameLogic missionBehavior = Mission.Current.GetMissionBehavior<MissionBoardGameLogic>();
				Campaign.Current.GetCampaignBehavior<BoardGameCampaignBehavior>().SetBetAmount(1000);
				missionBehavior.DetectOpposingAgent();
				missionBehavior.SetCurrentDifficulty(BoardGameHelper.AIDifficulty.Normal);
				missionBehavior.SetBoardGame(this._boardGameType);
				missionBehavior.StartBoardGame();
				this._checkForBoardGameEnd = true;
				this._tryCount++;
			}

			// Token: 0x06001225 RID: 4645 RVA: 0x00073ADA File Offset: 0x00071CDA
			private void PlayerWonTheBoardGame()
			{
				base.AddLog(this.SuccessLog, false);
				base.CompleteQuestWithSuccess();
			}

			// Token: 0x06001226 RID: 4646 RVA: 0x00073AF0 File Offset: 0x00071CF0
			private void PlayerFailAfterBoardGame()
			{
				base.AddLog(this.LostLog, false);
				this.RelationshipChangeWithQuestGiver = -5;
				base.QuestGiver.CurrentSettlement.Village.Bound.Town.Loyalty -= 5f;
				base.CompleteQuestWithFail(null);
			}

			// Token: 0x06001227 RID: 4647 RVA: 0x00073B45 File Offset: 0x00071D45
			private void OnClanChangedKingdom(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail detail, bool showNotification = true)
			{
				if (base.QuestGiver.CurrentSettlement.MapFaction.IsAtWarWith(Hero.MainHero.MapFaction))
				{
					base.CompleteQuestWithCancel(this.QuestCanceledWarDeclared);
				}
			}

			// Token: 0x06001228 RID: 4648 RVA: 0x00073B74 File Offset: 0x00071D74
			private void OnWarDeclared(IFaction faction1, IFaction faction2, DeclareWarAction.DeclareWarDetail detail)
			{
				QuestHelper.CheckWarDeclarationAndFailOrCancelTheQuest(this, faction1, faction2, detail, this.PlayerDeclaredWarQuestLogText, this.QuestCanceledWarDeclared, false);
			}

			// Token: 0x06001229 RID: 4649 RVA: 0x00073B8C File Offset: 0x00071D8C
			public override GameMenuOption.IssueQuestFlags IsLocationTrackedByQuest(Location location)
			{
				if (PlayerEncounter.LocationEncounter.Settlement == this._targetSettlement && location.StringId == "tavern")
				{
					return GameMenuOption.IssueQuestFlags.ActiveIssue;
				}
				return GameMenuOption.IssueQuestFlags.None;
			}

			// Token: 0x0600122A RID: 4650 RVA: 0x00073BB8 File Offset: 0x00071DB8
			protected override void OnTimedOut()
			{
				this.RelationshipChangeWithQuestGiver = -5;
				base.QuestGiver.CurrentSettlement.Village.Bound.Town.Loyalty -= 5f;
				base.AddLog(this.TimeoutLog, false);
			}

			// Token: 0x0600122B RID: 4651 RVA: 0x00073C06 File Offset: 0x00071E06
			internal static void AutoGeneratedStaticCollectObjectsRuralNotableInnAndOutIssueQuest(object o, List<object> collectedObjects)
			{
				((RuralNotableInnAndOutIssueBehavior.RuralNotableInnAndOutIssueQuest)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x0600122C RID: 4652 RVA: 0x00073C14 File Offset: 0x00071E14
			protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				base.AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x0600122D RID: 4653 RVA: 0x00073C1D File Offset: 0x00071E1D
			internal static object AutoGeneratedGetMemberValue_tryCount(object o)
			{
				return ((RuralNotableInnAndOutIssueBehavior.RuralNotableInnAndOutIssueQuest)o)._tryCount;
			}

			// Token: 0x040008AA RID: 2218
			public const int LesserReward = 800;

			// Token: 0x040008AB RID: 2219
			private CultureObject.BoardGameType _boardGameType;

			// Token: 0x040008AC RID: 2220
			private Settlement _targetSettlement;

			// Token: 0x040008AD RID: 2221
			private bool _checkForBoardGameEnd;

			// Token: 0x040008AE RID: 2222
			private bool _playerWonTheGame;

			// Token: 0x040008AF RID: 2223
			private bool _applyLesserReward;

			// Token: 0x040008B0 RID: 2224
			[SaveableField(1)]
			private int _tryCount;
		}
	}
}
