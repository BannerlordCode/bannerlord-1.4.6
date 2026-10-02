using System;
using Helpers;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Issues;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Buildings;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.GameComponents
{
	// Token: 0x0200014D RID: 333
	public class DefaultSettlementLoyaltyModel : SettlementLoyaltyModel
	{
		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x060019F6 RID: 6646 RVA: 0x000836C9 File Offset: 0x000818C9
		public override float HighLoyaltyProsperityEffect
		{
			get
			{
				return 0.5f;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x060019F7 RID: 6647 RVA: 0x000836D0 File Offset: 0x000818D0
		public override int LowLoyaltyProsperityEffect
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x060019F8 RID: 6648 RVA: 0x000836D3 File Offset: 0x000818D3
		public override int ThresholdForTaxBoost
		{
			get
			{
				return 75;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x060019F9 RID: 6649 RVA: 0x000836D7 File Offset: 0x000818D7
		public override int ThresholdForTaxCorruption
		{
			get
			{
				return 50;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x060019FA RID: 6650 RVA: 0x000836DB File Offset: 0x000818DB
		public override int ThresholdForHigherTaxCorruption
		{
			get
			{
				return 25;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x060019FB RID: 6651 RVA: 0x000836DF File Offset: 0x000818DF
		public override int ThresholdForProsperityBoost
		{
			get
			{
				return 75;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x060019FC RID: 6652 RVA: 0x000836E3 File Offset: 0x000818E3
		public override int ThresholdForProsperityPenalty
		{
			get
			{
				return 25;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x060019FD RID: 6653 RVA: 0x000836E7 File Offset: 0x000818E7
		public override int AdditionalStarvationPenaltyStartDay
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x060019FE RID: 6654 RVA: 0x000836EB File Offset: 0x000818EB
		public override int AdditionalStarvationLoyaltyEffect
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x060019FF RID: 6655 RVA: 0x000836EE File Offset: 0x000818EE
		public override int RebellionStartLoyaltyThreshold
		{
			get
			{
				return 15;
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001A00 RID: 6656 RVA: 0x000836F2 File Offset: 0x000818F2
		public override int RebelliousStateStartLoyaltyThreshold
		{
			get
			{
				return 25;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001A01 RID: 6657 RVA: 0x000836F6 File Offset: 0x000818F6
		public override int LoyaltyBoostAfterRebellionStartValue
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001A02 RID: 6658 RVA: 0x000836F9 File Offset: 0x000818F9
		public override int MilitiaBoostPercentage
		{
			get
			{
				return 200;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001A03 RID: 6659 RVA: 0x00083700 File Offset: 0x00081900
		public override float ThresholdForNotableRelationBonus
		{
			get
			{
				return 75f;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001A04 RID: 6660 RVA: 0x00083707 File Offset: 0x00081907
		public override int DailyNotableRelationBonus
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001A05 RID: 6661 RVA: 0x0008370A File Offset: 0x0008190A
		public override int SettlementLoyaltyChangeDueToSecurityThreshold
		{
			get
			{
				return 50;
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06001A06 RID: 6662 RVA: 0x0008370E File Offset: 0x0008190E
		public override int MaximumLoyaltyInSettlement
		{
			get
			{
				return 100;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06001A07 RID: 6663 RVA: 0x00083712 File Offset: 0x00081912
		public override int LoyaltyDriftMedium
		{
			get
			{
				return 50;
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001A08 RID: 6664 RVA: 0x00083716 File Offset: 0x00081916
		public override float HighSecurityLoyaltyEffect
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001A09 RID: 6665 RVA: 0x0008371D File Offset: 0x0008191D
		public override float LowSecurityLoyaltyEffect
		{
			get
			{
				return -2f;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001A0A RID: 6666 RVA: 0x00083724 File Offset: 0x00081924
		public override float GovernorSameCultureLoyaltyEffect
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001A0B RID: 6667 RVA: 0x0008372B File Offset: 0x0008192B
		public override float GovernorDifferentCultureLoyaltyEffect
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001A0C RID: 6668 RVA: 0x00083732 File Offset: 0x00081932
		public override float SettlementOwnerDifferentCultureLoyaltyEffect
		{
			get
			{
				return -3f;
			}
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x00083739 File Offset: 0x00081939
		public override ExplainedNumber CalculateLoyaltyChange(Town town, bool includeDescriptions = false)
		{
			return this.CalculateLoyaltyChangeInternal(town, includeDescriptions);
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x00083744 File Offset: 0x00081944
		public override void CalculateGoldGainDueToHighLoyalty(Town town, ref ExplainedNumber explainedNumber)
		{
			float num = MBMath.Map(town.Loyalty, (float)this.ThresholdForTaxBoost, 100f, 0f, 0.2f);
			explainedNumber.AddFactor(num, this.LoyaltyText);
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x00083780 File Offset: 0x00081980
		public override void CalculateGoldCutDueToLowLoyalty(Town town, ref ExplainedNumber explainedNumber)
		{
			float num = MBMath.Map(town.Loyalty, (float)this.ThresholdForHigherTaxCorruption, (float)this.ThresholdForTaxCorruption, -0.5f, 0f);
			explainedNumber.AddFactor(num, this.CorruptionText);
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x000837C0 File Offset: 0x000819C0
		private ExplainedNumber CalculateLoyaltyChangeInternal(Town town, bool includeDescriptions = false)
		{
			ExplainedNumber explainedNumber = new ExplainedNumber(0f, includeDescriptions, null);
			this.GetSettlementLoyaltyChangeDueToFoodStocks(town, ref explainedNumber);
			this.GetSettlementLoyaltyChangeDueToGovernorCulture(town, ref explainedNumber);
			this.GetSettlementLoyaltyChangeDueToOwnerCulture(town, ref explainedNumber);
			this.GetSettlementLoyaltyChangeDueToPolicies(town, ref explainedNumber);
			this.GetSettlementLoyaltyChangeDueToProjects(town, ref explainedNumber);
			this.GetSettlementLoyaltyChangeDueToIssues(town, ref explainedNumber);
			this.GetSettlementLoyaltyChangeDueToSecurity(town, ref explainedNumber);
			this.GetSettlementLoyaltyChangeDueToNotableRelations(town, ref explainedNumber);
			this.GetSettlementLoyaltyChangeDueToGovernorPerks(town, ref explainedNumber);
			this.GetSettlementLoyaltyChangeDueToLoyaltyDrift(town, ref explainedNumber);
			return explainedNumber;
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x00083838 File Offset: 0x00081A38
		private void GetSettlementLoyaltyChangeDueToGovernorPerks(Town town, ref ExplainedNumber explainedNumber)
		{
			PerkHelper.AddPerkBonusForTown(DefaultPerks.Leadership.HeroicLeader, town, ref explainedNumber);
			PerkHelper.AddPerkBonusForTown(DefaultPerks.Medicine.PhysicianOfPeople, town, ref explainedNumber);
			PerkHelper.AddPerkBonusForTown(DefaultPerks.Athletics.Durable, town, ref explainedNumber);
			PerkHelper.AddPerkBonusForTown(DefaultPerks.Bow.Discipline, town, ref explainedNumber);
			PerkHelper.AddPerkBonusForTown(DefaultPerks.Riding.WellStraped, town, ref explainedNumber);
			float num = 0f;
			for (int i = 0; i < town.Settlement.Parties.Count; i++)
			{
				MobileParty mobileParty = town.Settlement.Parties[i];
				if (mobileParty.ActualClan == town.OwnerClan)
				{
					if (mobileParty.IsMainParty)
					{
						for (int j = 0; j < mobileParty.MemberRoster.Count; j++)
						{
							CharacterObject characterAtIndex = mobileParty.MemberRoster.GetCharacterAtIndex(j);
							if (characterAtIndex.IsHero && characterAtIndex.HeroObject.GetPerkValue(DefaultPerks.Charm.Parade))
							{
								num += DefaultPerks.Charm.Parade.PrimaryBonus;
							}
						}
					}
					else if (mobileParty.LeaderHero != null && mobileParty.LeaderHero.GetPerkValue(DefaultPerks.Charm.Parade))
					{
						num += DefaultPerks.Charm.Parade.PrimaryBonus;
					}
				}
			}
			foreach (Hero hero in town.Settlement.HeroesWithoutParty)
			{
				if (hero.Clan == town.OwnerClan && hero.GetPerkValue(DefaultPerks.Charm.Parade))
				{
					num += DefaultPerks.Charm.Parade.PrimaryBonus;
				}
			}
			if (num > 0f)
			{
				explainedNumber.Add(num, DefaultPerks.Charm.Parade.Name, null);
			}
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x000839D0 File Offset: 0x00081BD0
		private void GetSettlementLoyaltyChangeDueToNotableRelations(Town town, ref ExplainedNumber explainedNumber)
		{
			float num = 0f;
			foreach (Hero hero in town.Settlement.Notables)
			{
				if (hero.SupporterOf != null)
				{
					if (hero.SupporterOf == town.Settlement.OwnerClan)
					{
						num += 0.5f;
					}
					else if (town.MapFaction.IsAtWarWith(hero.SupporterOf.MapFaction))
					{
						num += -0.5f;
					}
				}
			}
			if (!num.ApproximatelyEqualsTo(0f, 1E-05f))
			{
				explainedNumber.Add(num, this.NotableText, null);
			}
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x00083A8C File Offset: 0x00081C8C
		private void GetSettlementLoyaltyChangeDueToOwnerCulture(Town town, ref ExplainedNumber explainedNumber)
		{
			if (town.Settlement.OwnerClan.Culture != town.Settlement.Culture)
			{
				explainedNumber.Add(this.SettlementOwnerDifferentCultureLoyaltyEffect, DefaultSettlementLoyaltyModel.CultureText, null);
			}
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x00083AC0 File Offset: 0x00081CC0
		private void GetSettlementLoyaltyChangeDueToPolicies(Town town, ref ExplainedNumber explainedNumber)
		{
			Kingdom kingdom = town.Owner.Settlement.OwnerClan.Kingdom;
			if (kingdom != null)
			{
				if (kingdom.ActivePolicies.Contains(DefaultPolicies.Citizenship))
				{
					if (town.Settlement.OwnerClan.Culture == town.Settlement.Culture)
					{
						explainedNumber.Add(0.5f, DefaultPolicies.Citizenship.Name, null);
					}
					else
					{
						explainedNumber.Add(-0.5f, DefaultPolicies.Citizenship.Name, null);
					}
				}
				if (kingdom.ActivePolicies.Contains(DefaultPolicies.HuntingRights))
				{
					explainedNumber.Add(-0.2f, DefaultPolicies.HuntingRights.Name, null);
				}
				if (kingdom.ActivePolicies.Contains(DefaultPolicies.GrazingRights))
				{
					explainedNumber.Add(0.5f, DefaultPolicies.GrazingRights.Name, null);
				}
				if (kingdom.ActivePolicies.Contains(DefaultPolicies.TrialByJury))
				{
					explainedNumber.Add(0.5f, DefaultPolicies.TrialByJury.Name, null);
				}
				if (town.IsTown && kingdom.ActivePolicies.Contains(DefaultPolicies.ImperialTowns))
				{
					if (kingdom.RulingClan == town.Settlement.OwnerClan)
					{
						explainedNumber.Add(1f, DefaultPolicies.ImperialTowns.Name, null);
					}
					else
					{
						explainedNumber.Add(-0.3f, DefaultPolicies.ImperialTowns.Name, null);
					}
				}
				if (kingdom.ActivePolicies.Contains(DefaultPolicies.ForgivenessOfDebts))
				{
					explainedNumber.Add(2f, DefaultPolicies.ForgivenessOfDebts.Name, null);
				}
				if (kingdom.ActivePolicies.Contains(DefaultPolicies.TribunesOfThePeople) && town.IsTown)
				{
					explainedNumber.Add(1f, DefaultPolicies.TribunesOfThePeople.Name, null);
				}
				if (kingdom.ActivePolicies.Contains(DefaultPolicies.DebasementOfTheCurrency))
				{
					explainedNumber.Add(-1f, DefaultPolicies.DebasementOfTheCurrency.Name, null);
				}
			}
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x00083C99 File Offset: 0x00081E99
		private void GetSettlementLoyaltyChangeDueToGovernorCulture(Town town, ref ExplainedNumber explainedNumber)
		{
			if (town.Governor != null)
			{
				explainedNumber.Add((town.Governor.Culture == town.Culture) ? this.GovernorSameCultureLoyaltyEffect : this.GovernorDifferentCultureLoyaltyEffect, DefaultSettlementLoyaltyModel.GovernorCultureText, null);
			}
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x00083CD0 File Offset: 0x00081ED0
		private void GetSettlementLoyaltyChangeDueToFoodStocks(Town town, ref ExplainedNumber explainedNumber)
		{
			if (town.Settlement.IsStarving)
			{
				float num = -1f;
				if (town.Settlement.Party.DaysStarving > 14f)
				{
					num += -1f;
				}
				explainedNumber.Add(num, this.StarvingText, null);
			}
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x00083D20 File Offset: 0x00081F20
		private void GetSettlementLoyaltyChangeDueToSecurity(Town town, ref ExplainedNumber explainedNumber)
		{
			float num = ((town.Security > (float)this.SettlementLoyaltyChangeDueToSecurityThreshold) ? MBMath.Map(town.Security, (float)this.SettlementLoyaltyChangeDueToSecurityThreshold, (float)this.MaximumLoyaltyInSettlement, 0f, this.HighSecurityLoyaltyEffect) : MBMath.Map(town.Security, 0f, (float)this.SettlementLoyaltyChangeDueToSecurityThreshold, this.LowSecurityLoyaltyEffect, 0f));
			explainedNumber.Add(num, this.SecurityText, null);
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x00083D93 File Offset: 0x00081F93
		private void GetSettlementLoyaltyChangeDueToProjects(Town town, ref ExplainedNumber explainedNumber)
		{
			town.AddEffectOfBuildings(BuildingEffectEnum.Loyalty, ref explainedNumber);
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x00083D9D File Offset: 0x00081F9D
		private void GetSettlementLoyaltyChangeDueToIssues(Town town, ref ExplainedNumber explainedNumber)
		{
			Campaign.Current.Models.IssueModel.GetIssueEffectsOfSettlement(DefaultIssueEffects.SettlementLoyalty, town.Settlement, ref explainedNumber);
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x00083DBF File Offset: 0x00081FBF
		private void GetSettlementLoyaltyChangeDueToLoyaltyDrift(Town town, ref ExplainedNumber explainedNumber)
		{
			explainedNumber.Add(-0.1f * (town.Loyalty - (float)this.LoyaltyDriftMedium), this.LoyaltyDriftText, null);
		}

		// Token: 0x040008A6 RID: 2214
		private const float StarvationLoyaltyEffect = -1f;

		// Token: 0x040008A7 RID: 2215
		private const int AdditionalStarvationLoyaltyEffectAfterDays = 14;

		// Token: 0x040008A8 RID: 2216
		private const float NotableSupportsOwnerLoyaltyEffect = 0.5f;

		// Token: 0x040008A9 RID: 2217
		private const float NotableSupportsEnemyLoyaltyEffect = -0.5f;

		// Token: 0x040008AA RID: 2218
		private readonly TextObject StarvingText = GameTexts.FindText("str_starving", null);

		// Token: 0x040008AB RID: 2219
		private static readonly TextObject CultureText = new TextObject("{=YjoXyFDX}Owner Culture", null);

		// Token: 0x040008AC RID: 2220
		private static readonly TextObject GovernorCultureText = new TextObject("{=5Vo8dJub}Governor's Culture", null);

		// Token: 0x040008AD RID: 2221
		private static readonly TextObject NoGovernorText = new TextObject("{=NH5N3kP5}No governor", null);

		// Token: 0x040008AE RID: 2222
		private readonly TextObject NotableText = GameTexts.FindText("str_notable_relations", null);

		// Token: 0x040008AF RID: 2223
		private readonly TextObject CrimeText = GameTexts.FindText("str_governor_criminal", null);

		// Token: 0x040008B0 RID: 2224
		private readonly TextObject GovernorText = GameTexts.FindText("str_notable_governor", null);

		// Token: 0x040008B1 RID: 2225
		private readonly TextObject SecurityText = GameTexts.FindText("str_security", null);

		// Token: 0x040008B2 RID: 2226
		private readonly TextObject LoyaltyText = GameTexts.FindText("str_loyalty", null);

		// Token: 0x040008B3 RID: 2227
		private readonly TextObject LoyaltyDriftText = GameTexts.FindText("str_loyalty_drift", null);

		// Token: 0x040008B4 RID: 2228
		private readonly TextObject CorruptionText = GameTexts.FindText("str_corruption", null);
	}
}
