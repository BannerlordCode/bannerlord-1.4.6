using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Issues;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Buildings;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.GameComponents
{
	// Token: 0x02000151 RID: 337
	public class DefaultSettlementSecurityModel : SettlementSecurityModel
	{
		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06001A34 RID: 6708 RVA: 0x00084DE5 File Offset: 0x00082FE5
		public override int MaximumSecurityInSettlement
		{
			get
			{
				return 100;
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06001A35 RID: 6709 RVA: 0x00084DE9 File Offset: 0x00082FE9
		public override int SecurityDriftMedium
		{
			get
			{
				return 50;
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001A36 RID: 6710 RVA: 0x00084DED File Offset: 0x00082FED
		public override float MapEventSecurityEffectRadius
		{
			get
			{
				return 50f;
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001A37 RID: 6711 RVA: 0x00084DF4 File Offset: 0x00082FF4
		public override float HideoutClearedSecurityEffectRadius
		{
			get
			{
				return 100f;
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001A38 RID: 6712 RVA: 0x00084DFB File Offset: 0x00082FFB
		public override int HideoutClearedSecurityGain
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001A39 RID: 6713 RVA: 0x00084DFE File Offset: 0x00082FFE
		public override int ThresholdForTaxCorruption
		{
			get
			{
				return 50;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001A3A RID: 6714 RVA: 0x00084E02 File Offset: 0x00083002
		public override int ThresholdForHigherTaxCorruption
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001A3B RID: 6715 RVA: 0x00084E05 File Offset: 0x00083005
		public override int ThresholdForTaxBoost
		{
			get
			{
				return 75;
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001A3C RID: 6716 RVA: 0x00084E09 File Offset: 0x00083009
		public override int SettlementTaxBoostPercentage
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001A3D RID: 6717 RVA: 0x00084E0C File Offset: 0x0008300C
		public override int SettlementTaxPenaltyPercentage
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06001A3E RID: 6718 RVA: 0x00084E10 File Offset: 0x00083010
		public override int ThresholdForNotableRelationBonus
		{
			get
			{
				return 75;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06001A3F RID: 6719 RVA: 0x00084E14 File Offset: 0x00083014
		public override int ThresholdForNotableRelationPenalty
		{
			get
			{
				return 50;
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001A40 RID: 6720 RVA: 0x00084E18 File Offset: 0x00083018
		public override int DailyNotableRelationBonus
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06001A41 RID: 6721 RVA: 0x00084E1B File Offset: 0x0008301B
		public override int DailyNotableRelationPenalty
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001A42 RID: 6722 RVA: 0x00084E1E File Offset: 0x0008301E
		public override int DailyNotablePowerBonus
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001A43 RID: 6723 RVA: 0x00084E21 File Offset: 0x00083021
		public override int DailyNotablePowerPenalty
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x00084E24 File Offset: 0x00083024
		public override ExplainedNumber CalculateSecurityChange(Town town, bool includeDescriptions = false)
		{
			ExplainedNumber explainedNumber = new ExplainedNumber(0f, includeDescriptions, null);
			this.CalculateInfestedHideoutEffectsOnSecurity(town, ref explainedNumber);
			this.CalculateRaidedVillageEffectsOnSecurity(town, ref explainedNumber);
			this.CalculateUnderSiegeEffectsOnSecurity(town, ref explainedNumber);
			this.CalculateProsperityEffectOnSecurity(town, ref explainedNumber);
			this.CalculateGarrisonEffectsOnSecurity(town, ref explainedNumber);
			this.CalculatePolicyEffectsOnSecurity(town, ref explainedNumber);
			this.CalculateGovernorEffectsOnSecurity(town, ref explainedNumber);
			this.CalculateProjectEffectsOnSecurity(town, ref explainedNumber);
			this.CalculateIssueEffectsOnSecurity(town, ref explainedNumber);
			this.CalculatePerkEffectsOnSecurity(town, ref explainedNumber);
			this.CalculateSecurityDrift(town, ref explainedNumber);
			this.CalculateSettlementProjectSecurityBonuses(town, ref explainedNumber);
			this.CalculateSettlementPatrolPartiesBonuses(town, ref explainedNumber);
			return explainedNumber;
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x00084EB8 File Offset: 0x000830B8
		private void CalculateSettlementPatrolPartiesBonuses(Town town, ref ExplainedNumber result)
		{
			if (town.Settlement.PatrolParty != null)
			{
				foreach (Building building in town.Buildings)
				{
					if (building.BuildingType == DefaultBuildingTypes.SettlementGuardHouse && building.CurrentLevel > 0)
					{
						result.Add((float)building.CurrentLevel * 0.5f + 0.5f, this.PatrolPartiesText, null);
						break;
					}
				}
			}
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x00084F4C File Offset: 0x0008314C
		private void CalculateSettlementProjectSecurityBonuses(Town town, ref ExplainedNumber result)
		{
			town.AddEffectOfBuildings(BuildingEffectEnum.SecurityPerDay, ref result);
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x00084F57 File Offset: 0x00083157
		private void CalculateProsperityEffectOnSecurity(Town town, ref ExplainedNumber explainedNumber)
		{
			explainedNumber.Add(MathF.Max(-5f, -0.0005f * town.Prosperity), this.ProsperityText, null);
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x00084F7C File Offset: 0x0008317C
		private void CalculateUnderSiegeEffectsOnSecurity(Town town, ref ExplainedNumber explainedNumber)
		{
			if (town.Settlement.IsUnderSiege)
			{
				explainedNumber.Add(-3f, this.UnderSiegeText, null);
			}
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x00084FA0 File Offset: 0x000831A0
		private void CalculateRaidedVillageEffectsOnSecurity(Town town, ref ExplainedNumber explainedNumber)
		{
			float num = 0f;
			using (List<Village>.Enumerator enumerator = town.Settlement.BoundVillages.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.VillageState == Village.VillageStates.Looted)
					{
						num += -2f;
						break;
					}
				}
			}
			explainedNumber.Add(num, this.LootedVillagesText, null);
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x00085018 File Offset: 0x00083218
		private void CalculateInfestedHideoutEffectsOnSecurity(Town town, ref ExplainedNumber explainedNumber)
		{
			float num = Campaign.Current.EstimatedAverageBanditPartySpeed * (float)CampaignTime.HoursInDay * 0.5f;
			foreach (Hideout hideout in Hideout.All)
			{
				if (hideout.IsInfested && Campaign.Current.Models.MapDistanceModel.GetDistance(town.Settlement, hideout.Settlement, false, false, MobileParty.NavigationType.Default) < num)
				{
					explainedNumber.Add(-2f, this.NearbyHideoutText, null);
					break;
				}
			}
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x000850C0 File Offset: 0x000832C0
		private void CalculateSecurityDrift(Town town, ref ExplainedNumber explainedNumber)
		{
			explainedNumber.Add(-1f * (town.Security - (float)this.SecurityDriftMedium) / 15f, this.SecurityDriftText, null);
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x000850EC File Offset: 0x000832EC
		private void CalculatePolicyEffectsOnSecurity(Town town, ref ExplainedNumber explainedNumber)
		{
			Kingdom kingdom = town.Settlement.OwnerClan.Kingdom;
			if (kingdom != null)
			{
				if (town.IsTown)
				{
					if (kingdom.ActivePolicies.Contains(DefaultPolicies.Bailiffs))
					{
						explainedNumber.Add(1f, DefaultPolicies.Bailiffs.Name, null);
					}
					if (kingdom.ActivePolicies.Contains(DefaultPolicies.Serfdom))
					{
						explainedNumber.Add(1f, DefaultPolicies.Serfdom.Name, null);
					}
					if (kingdom.ActivePolicies.Contains(DefaultPolicies.Magistrates))
					{
						explainedNumber.Add(1f, DefaultPolicies.Magistrates.Name, null);
					}
				}
				if (kingdom.ActivePolicies.Contains(DefaultPolicies.TrialByJury))
				{
					explainedNumber.Add(-0.2f, DefaultPolicies.TrialByJury.Name, null);
				}
			}
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x000851B8 File Offset: 0x000833B8
		private void CalculateGovernorEffectsOnSecurity(Town town, ref ExplainedNumber explainedNumber)
		{
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x000851BC File Offset: 0x000833BC
		private void CalculateGarrisonEffectsOnSecurity(Town town, ref ExplainedNumber result)
		{
			if (town.GarrisonParty != null && town.GarrisonParty.MemberRoster.Count != 0 && town.GarrisonParty.MemberRoster.TotalHealthyCount != 0)
			{
				ExplainedNumber explainedNumber = new ExplainedNumber(0.01f, false, null);
				PerkHelper.AddPerkBonusForTown(DefaultPerks.OneHanded.StandUnited, town, ref explainedNumber);
				float num;
				float num2;
				float num3;
				this.CalculateStrengthOfGarrisonParty(town.GarrisonParty.Party, out num, out num2, out num3);
				float num4 = num * explainedNumber.ResultNumber;
				result.Add(num4, this.GarrisonText, null);
				if (PerkHelper.GetPerkValueForTown(DefaultPerks.Leadership.Authority, town))
				{
					result.Add(num4 * DefaultPerks.Leadership.Authority.PrimaryBonus, DefaultPerks.Leadership.Authority.Name, null);
				}
				if (PerkHelper.GetPerkValueForTown(DefaultPerks.Riding.ReliefForce, town))
				{
					float num5 = num3 / num;
					result.Add(num4 * num5 * DefaultPerks.Riding.ReliefForce.SecondaryBonus, DefaultPerks.Riding.ReliefForce.Name, null);
				}
				float num6 = num2 / num;
				if (PerkHelper.GetPerkValueForTown(DefaultPerks.Bow.MountedArchery, town))
				{
					result.Add(num4 * num6 * DefaultPerks.Bow.MountedArchery.SecondaryBonus, DefaultPerks.Bow.MountedArchery.Name, null);
				}
				if (PerkHelper.GetPerkValueForTown(DefaultPerks.Bow.RangersSwiftness, town))
				{
					result.Add(num4 * num6 * DefaultPerks.Bow.RangersSwiftness.SecondaryBonus, DefaultPerks.Bow.RangersSwiftness.Name, null);
				}
				if (PerkHelper.GetPerkValueForTown(DefaultPerks.Crossbow.RenownMarksmen, town))
				{
					result.Add(num4 * num6 * DefaultPerks.Crossbow.RenownMarksmen.SecondaryBonus, DefaultPerks.Crossbow.RenownMarksmen.Name, null);
				}
			}
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x00085338 File Offset: 0x00083538
		private void CalculateStrengthOfGarrisonParty(PartyBase party, out float totalStrength, out float archerStrength, out float cavalryStrength)
		{
			totalStrength = 0f;
			archerStrength = 0f;
			cavalryStrength = 0f;
			float num = 0f;
			MapEvent.PowerCalculationContext powerCalculationContext = MapEvent.PowerCalculationContext.Siege;
			BattleSideEnum battleSideEnum = BattleSideEnum.Defender;
			if (party.MapEvent != null)
			{
				battleSideEnum = party.Side;
				Hero leaderHero = party.LeaderHero;
				num = ((leaderHero != null) ? leaderHero.PowerModifier : 0f);
				powerCalculationContext = party.MapEvent.SimulationContext;
			}
			for (int i = 0; i < party.MemberRoster.Count; i++)
			{
				TroopRosterElement elementCopyAtIndex = party.MemberRoster.GetElementCopyAtIndex(i);
				if (elementCopyAtIndex.Character != null)
				{
					float troopPower = Campaign.Current.Models.MilitaryPowerModel.GetTroopPower(elementCopyAtIndex.Character, battleSideEnum, powerCalculationContext, num);
					float num2 = (float)(elementCopyAtIndex.Number - elementCopyAtIndex.WoundedNumber) * troopPower;
					if (elementCopyAtIndex.Character.IsMounted)
					{
						cavalryStrength += num2;
					}
					if (elementCopyAtIndex.Character.IsRanged)
					{
						archerStrength += num2;
					}
					totalStrength += num2;
				}
			}
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x00085434 File Offset: 0x00083634
		private void CalculatePerkEffectsOnSecurity(Town town, ref ExplainedNumber result)
		{
			float num = (float)town.Settlement.Parties.Where<MobileParty>(delegate(MobileParty x)
			{
				Clan actualClan = x.ActualClan;
				if (actualClan != null && !actualClan.IsAtWarWith(town.MapFaction))
				{
					Hero leaderHero = x.LeaderHero;
					return leaderHero != null && leaderHero.GetPerkValue(DefaultPerks.Leadership.Presence);
				}
				return false;
			}).Count<MobileParty>() * DefaultPerks.Leadership.Presence.PrimaryBonus;
			if (num > 0f)
			{
				result.Add(num, DefaultPerks.Leadership.Presence.Name, null);
			}
			if (town.Governor != null && town.Governor.GetPerkValue(DefaultPerks.Roguery.KnowHow))
			{
				PerkHelper.AddPerkBonusForTown(DefaultPerks.Roguery.KnowHow, town, ref result);
			}
			PerkHelper.AddPerkBonusForTown(DefaultPerks.OneHanded.ToBeBlunt, town, ref result);
			PerkHelper.AddPerkBonusForTown(DefaultPerks.Throwing.Focus, town, ref result);
			PerkHelper.AddPerkBonusForTown(DefaultPerks.Polearm.Skewer, town, ref result);
			PerkHelper.AddPerkBonusForTown(DefaultPerks.Tactics.Gensdarmes, town, ref result);
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x00085514 File Offset: 0x00083714
		private void CalculateProjectEffectsOnSecurity(Town town, ref ExplainedNumber explainedNumber)
		{
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x00085516 File Offset: 0x00083716
		private void CalculateIssueEffectsOnSecurity(Town town, ref ExplainedNumber explainedNumber)
		{
			Campaign.Current.Models.IssueModel.GetIssueEffectsOfSettlement(DefaultIssueEffects.SettlementSecurity, town.Settlement, ref explainedNumber);
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x00085538 File Offset: 0x00083738
		public override float GetLootedNearbyPartySecurityEffect(Town town, float sumOfAttackedPartyStrengths)
		{
			return -1f * sumOfAttackedPartyStrengths * 0.005f;
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x00085547 File Offset: 0x00083747
		public override float GetNearbyBanditPartyDefeatedSecurityEffect(Town town, float sumOfAttackedPartyStrengths)
		{
			return sumOfAttackedPartyStrengths * 0.005f;
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x00085550 File Offset: 0x00083750
		public override void CalculateGoldGainDueToHighSecurity(Town town, ref ExplainedNumber explainedNumber)
		{
			float num = MBMath.Map(town.Security, (float)this.ThresholdForTaxBoost, (float)this.MaximumSecurityInSettlement, 0f, (float)this.SettlementTaxBoostPercentage);
			explainedNumber.AddFactor(num * 0.01f, this.Security);
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x00085598 File Offset: 0x00083798
		public override void CalculateGoldCutDueToLowSecurity(Town town, ref ExplainedNumber explainedNumber)
		{
			float num = MBMath.Map(town.Security, (float)this.ThresholdForHigherTaxCorruption, (float)this.ThresholdForTaxCorruption, (float)this.SettlementTaxPenaltyPercentage, 0f);
			explainedNumber.AddFactor(-1f * num * 0.01f, this.CorruptionText);
		}

		// Token: 0x040008C5 RID: 2245
		private const float GarrisonHighSecurityGain = 3f;

		// Token: 0x040008C6 RID: 2246
		private const float GarrisonLowSecurityPenalty = -3f;

		// Token: 0x040008C7 RID: 2247
		private const float NearbyHideoutPenalty = -2f;

		// Token: 0x040008C8 RID: 2248
		private const float VillageLootedSecurityEffect = -2f;

		// Token: 0x040008C9 RID: 2249
		private const float UnderSiegeSecurityEffect = -3f;

		// Token: 0x040008CA RID: 2250
		private const float MaxProsperityEffect = -5f;

		// Token: 0x040008CB RID: 2251
		private const float PerProsperityEffect = -0.0005f;

		// Token: 0x040008CC RID: 2252
		private readonly TextObject GarrisonText = GameTexts.FindText("str_garrison", null);

		// Token: 0x040008CD RID: 2253
		private readonly TextObject LootedVillagesText = GameTexts.FindText("str_looted_villages", null);

		// Token: 0x040008CE RID: 2254
		private readonly TextObject CorruptionText = GameTexts.FindText("str_corruption", null);

		// Token: 0x040008CF RID: 2255
		private readonly TextObject NearbyHideoutText = GameTexts.FindText("str_nearby_hideout", null);

		// Token: 0x040008D0 RID: 2256
		private readonly TextObject UnderSiegeText = GameTexts.FindText("str_under_siege", null);

		// Token: 0x040008D1 RID: 2257
		private readonly TextObject ProsperityText = GameTexts.FindText("str_prosperity", null);

		// Token: 0x040008D2 RID: 2258
		private readonly TextObject Security = GameTexts.FindText("str_security", null);

		// Token: 0x040008D3 RID: 2259
		private readonly TextObject SecurityDriftText = GameTexts.FindText("str_security_drift", null);

		// Token: 0x040008D4 RID: 2260
		private readonly TextObject PatrolPartiesText = GameTexts.FindText("str_patrol_parties", null);
	}
}
