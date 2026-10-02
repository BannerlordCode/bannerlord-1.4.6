using System;
using System.Collections.Generic;
using Helpers;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Naval;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.AiBehaviors
{
	// Token: 0x02000473 RID: 1139
	public class AiPatrollingBehavior : CampaignBehaviorBase
	{
		// Token: 0x060048A3 RID: 18595 RVA: 0x00170024 File Offset: 0x0016E224
		public override void RegisterEvents()
		{
			CampaignEvents.AiHourlyTickEvent.AddNonSerializedListener(this, new Action<MobileParty, PartyThinkParams>(this.AiHourlyTick));
			CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnSessionLaunched));
			CampaignEvents.OnShipDestroyedEvent.AddNonSerializedListener(this, new Action<PartyBase, Ship, DestroyShipAction.ShipDestroyDetail>(this.OnShipDestroyed));
			CampaignEvents.OnBlockadeActivatedEvent.AddNonSerializedListener(this, new Action<SiegeEvent>(this.OnBlockadeActivated));
			CampaignEvents.OnShipOwnerChangedEvent.AddNonSerializedListener(this, new Action<Ship, PartyBase, ChangeShipOwnerAction.ShipOwnerChangeDetail>(this.OnShipOwnerChanged));
		}

		// Token: 0x060048A4 RID: 18596 RVA: 0x001700A4 File Offset: 0x0016E2A4
		private void OnBlockadeActivated(SiegeEvent siegeEvent)
		{
			foreach (MobileParty mobileParty in MobileParty.All)
			{
				if (mobileParty.DefaultBehavior == AiBehavior.GoToSettlement && mobileParty.TargetSettlement == siegeEvent.BesiegedSettlement && mobileParty.CurrentSettlement != siegeEvent.BesiegedSettlement)
				{
					mobileParty.SetMoveModeHold();
				}
			}
		}

		// Token: 0x060048A5 RID: 18597 RVA: 0x0017011C File Offset: 0x0016E31C
		private void OnShipOwnerChanged(Ship ship, PartyBase oldOwner, ChangeShipOwnerAction.ShipOwnerChangeDetail changeDetail)
		{
			this.CheckPartyIfNeeded(oldOwner);
		}

		// Token: 0x060048A6 RID: 18598 RVA: 0x00170125 File Offset: 0x0016E325
		private void OnShipDestroyed(PartyBase owner, Ship ship, DestroyShipAction.ShipDestroyDetail detail)
		{
			this.CheckPartyIfNeeded(owner);
		}

		// Token: 0x060048A7 RID: 18599 RVA: 0x00170130 File Offset: 0x0016E330
		private void CheckPartyIfNeeded(PartyBase party)
		{
			if (party != null && party.IsMobile && party.MobileParty.IsLordParty && party.MobileParty.DefaultBehavior == AiBehavior.PatrolAroundPoint && !party.MobileParty.TargetPosition.IsOnLand && !party.MobileParty.HasNavalNavigationCapability)
			{
				party.MobileParty.SetMoveModeHold();
			}
		}

		// Token: 0x060048A8 RID: 18600 RVA: 0x0017018E File Offset: 0x0016E38E
		private void OnSessionLaunched(CampaignGameStarter campaignGameStarter)
		{
			this._disbandPartyCampaignBehavior = Campaign.Current.GetCampaignBehavior<IDisbandPartyCampaignBehavior>();
		}

		// Token: 0x060048A9 RID: 18601 RVA: 0x001701A0 File Offset: 0x0016E3A0
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x060048AA RID: 18602 RVA: 0x001701A4 File Offset: 0x0016E3A4
		private void AiHourlyTick(MobileParty mobileParty, PartyThinkParams p)
		{
			if (mobileParty.IsMilitia || mobileParty.IsCaravan || mobileParty.IsVillager || mobileParty.IsBandit || mobileParty.IsPatrolParty || mobileParty.IsDisbanding || (!mobileParty.MapFaction.IsMinorFaction && !mobileParty.MapFaction.IsKingdomFaction && !mobileParty.MapFaction.Leader.IsLord))
			{
				return;
			}
			if (mobileParty.CurrentSettlement != null && mobileParty.CurrentSettlement.IsUnderSiege)
			{
				return;
			}
			if (mobileParty.Army != null)
			{
				return;
			}
			if (mobileParty.GetNumDaysForFoodToLast() <= 6)
			{
				return;
			}
			Settlement currentSettlement = mobileParty.CurrentSettlement;
			if (((currentSettlement != null) ? currentSettlement.SiegeEvent : null) != null)
			{
				return;
			}
			float num4;
			if (mobileParty.Army != null)
			{
				float num = 0f;
				foreach (MobileParty mobileParty2 in mobileParty.Army.Parties)
				{
					float num2 = PartyBaseHelper.FindPartySizeNormalLimit(mobileParty2);
					float num3 = mobileParty2.PartySizeRatio / num2;
					num += num3;
				}
				num4 = num / (float)mobileParty.Army.Parties.Count;
			}
			else
			{
				float num5 = PartyBaseHelper.FindPartySizeNormalLimit(mobileParty);
				num4 = mobileParty.PartySizeRatio / num5;
			}
			float num6 = MathF.Sqrt(MathF.Min(1f, num4));
			if (!mobileParty.IsDisbanding)
			{
				IDisbandPartyCampaignBehavior disbandPartyCampaignBehavior = this._disbandPartyCampaignBehavior;
				if (disbandPartyCampaignBehavior == null || !disbandPartyCampaignBehavior.IsPartyWaitingForDisband(mobileParty))
				{
					goto IL_0157;
				}
			}
			num6 *= 0.25f;
			IL_0157:
			this.CalculateDefensivePatrollingScores(mobileParty, p, num6);
			this.CalculateOffensiveNavalPatrollingScores(mobileParty, p, num6);
		}

		// Token: 0x060048AB RID: 18603 RVA: 0x0017032C File Offset: 0x0016E52C
		private void CalculateOffensiveNavalPatrollingScores(MobileParty mobileParty, PartyThinkParams p, float scoreAdjustment)
		{
			if (mobileParty.HasNavalNavigationCapability && mobileParty.MapFaction.IsKingdomFaction && mobileParty.MapFaction.Leader != mobileParty.LeaderHero)
			{
				foreach (IFaction faction in mobileParty.MapFaction.FactionsAtWarWith)
				{
					foreach (Settlement settlement in faction.Settlements)
					{
						if (settlement.HasPort)
						{
							float num;
							this.GetDistanceScoreForOffensiveNavalPatrolling(settlement, mobileParty, out num);
							if (num > 0.5f)
							{
								this.CalculateOffensiveNavalPatrollingScoreForSettlement(settlement, p, num);
							}
						}
					}
				}
			}
		}

		// Token: 0x060048AC RID: 18604 RVA: 0x0017040C File Offset: 0x0016E60C
		private void CalculateDefensivePatrollingScores(MobileParty mobileParty, PartyThinkParams p, float scoreAdjustment)
		{
			if (mobileParty.Party.MapFaction.Settlements.Count > 0)
			{
				float num;
				SettlementHelper.FindFurthestFortificationToSettlement(mobileParty.MapFaction.Fiefs, MobileParty.NavigationType.Default, mobileParty.MapFaction.FactionMidSettlement, out num);
				using (List<Settlement>.Enumerator enumerator = mobileParty.Party.MapFaction.Settlements.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Settlement settlement = enumerator.Current;
						if (settlement.IsTown || settlement.IsVillage)
						{
							float maxValue = float.MaxValue;
							if (settlement.HasPort && mobileParty.HasNavalNavigationCapability && (!mobileParty.MapFaction.IsKingdomFaction || mobileParty.MapFaction.Leader != mobileParty.LeaderHero))
							{
								this.GetDistanceScoreForDefensiveNavalPatrolling(settlement, mobileParty, out maxValue);
								if (maxValue > 0.2f)
								{
									this.CalculateDefensivePatrollingScoreForSettlement(settlement, p, maxValue, true);
								}
							}
							this.GetDistanceScoreForLandPatrolling(settlement, mobileParty, num, out maxValue);
							if (maxValue > 0.2f)
							{
								this.CalculateDefensivePatrollingScoreForSettlement(settlement, p, maxValue, false);
							}
						}
					}
					return;
				}
			}
			float num2 = Campaign.Current.GetAverageDistanceBetweenClosestTwoTownsWithNavigationType(mobileParty.NavigationCapability) * 4f / (Campaign.Current.EstimatedAverageLordPartySpeed * (float)CampaignTime.HoursInDay) * Campaign.Current.EstimatedAverageLordPartySpeed * (float)CampaignTime.HoursInDay;
			int num3 = -1;
			do
			{
				num3 = SettlementHelper.FindNextSettlementAroundMobileParty(mobileParty, mobileParty.NavigationCapability, num2, num3, (Settlement x) => x.IsTown);
				if (num3 >= 0)
				{
					Settlement settlement2 = Settlement.All[num3];
					float averageDistanceBetweenClosestTwoTownsWithNavigationType = Campaign.Current.GetAverageDistanceBetweenClosestTwoTownsWithNavigationType(MobileParty.NavigationType.Default);
					float num4 = Campaign.Current.Models.MapDistanceModel.GetDistance(mobileParty.HomeSettlement, settlement2, false, false, MobileParty.NavigationType.Default);
					if (num4 < averageDistanceBetweenClosestTwoTownsWithNavigationType)
					{
						num4 = averageDistanceBetweenClosestTwoTownsWithNavigationType;
					}
					float num5 = averageDistanceBetweenClosestTwoTownsWithNavigationType * 5f / num4;
					this.CalculateDefensivePatrollingScoreForSettlement(settlement2, p, scoreAdjustment * num5, false);
				}
			}
			while (num3 >= 0);
		}

		// Token: 0x060048AD RID: 18605 RVA: 0x00170604 File Offset: 0x0016E804
		private void GetDistanceScoreForDefensiveNavalPatrolling(Settlement targetSettlement, MobileParty mobileParty, out float bestDistanceScore)
		{
			bestDistanceScore = 0f;
			MobileParty.NavigationType navigationType;
			float num;
			bool flag;
			AiHelper.GetBestNavigationTypeAndAdjustedDistanceOfSettlementForMobileParty(mobileParty, targetSettlement, true, out navigationType, out num, out flag);
			if (navigationType != MobileParty.NavigationType.None)
			{
				float averageDistanceBetweenClosestTwoTownsWithNavigationType = Campaign.Current.GetAverageDistanceBetweenClosestTwoTownsWithNavigationType(MobileParty.NavigationType.Naval);
				if (num > averageDistanceBetweenClosestTwoTownsWithNavigationType)
				{
					bestDistanceScore = -1f;
					return;
				}
				bestDistanceScore = MBMath.Map(1f - num / averageDistanceBetweenClosestTwoTownsWithNavigationType, 0f, 1f, 0.2f, 1f);
			}
		}

		// Token: 0x060048AE RID: 18606 RVA: 0x00170668 File Offset: 0x0016E868
		private void GetDistanceScoreForOffensiveNavalPatrolling(Settlement targetSettlement, MobileParty mobileParty, out float bestDistanceScore)
		{
			bestDistanceScore = 0f;
			MobileParty.NavigationType navigationType;
			float num;
			bool flag;
			AiHelper.GetBestNavigationTypeAndAdjustedDistanceOfSettlementForMobileParty(mobileParty, targetSettlement, true, out navigationType, out num, out flag);
			if (navigationType != MobileParty.NavigationType.None)
			{
				float num2 = Campaign.Current.GetAverageDistanceBetweenClosestTwoTownsWithNavigationType(MobileParty.NavigationType.Naval) * 3f;
				if (num > num2)
				{
					bestDistanceScore = -1f;
					return;
				}
				bestDistanceScore = MBMath.Map(1f - num / num2, 0f, 1f, 0.5f, 1.5f);
			}
		}

		// Token: 0x060048AF RID: 18607 RVA: 0x001706D0 File Offset: 0x0016E8D0
		private void GetDistanceScoreForLandPatrolling(Settlement targetSettlement, MobileParty mobileParty, float distanceToFurthestAllySettlementToFactionMidSettlement, out float bestDistanceScore)
		{
			float distance = Campaign.Current.Models.MapDistanceModel.GetDistance(mobileParty.MapFaction.FactionMidSettlement, targetSettlement, false, false, mobileParty.NavigationCapability);
			float num;
			if (distanceToFurthestAllySettlementToFactionMidSettlement == 0f)
			{
				num = 0.5f;
			}
			else
			{
				num = distance / distanceToFurthestAllySettlementToFactionMidSettlement;
			}
			float num2 = MBMath.Map(num, 0f, 1f, 0.2f, 0.8f);
			if (mobileParty.PartySizeRatio >= num2)
			{
				bestDistanceScore = MBMath.Map(0.8f - (mobileParty.PartySizeRatio - num2), 0f, 0.8f, 0.2f, 1f);
				return;
			}
			bestDistanceScore = 0f;
		}

		// Token: 0x060048B0 RID: 18608 RVA: 0x00170778 File Offset: 0x0016E978
		private void CalculateDefensivePatrollingScoreForSettlement(Settlement settlement, PartyThinkParams p, float scoreAdjustment, bool isNavalPatrolling)
		{
			MobileParty mobilePartyOf = p.MobilePartyOf;
			MobileParty.NavigationType navigationType;
			float num;
			bool flag;
			AiHelper.GetBestNavigationTypeAndAdjustedDistanceOfSettlementForMobileParty(mobilePartyOf, settlement, isNavalPatrolling, out navigationType, out num, out flag);
			if (navigationType != MobileParty.NavigationType.None)
			{
				AIBehaviorData aibehaviorData = new AIBehaviorData(settlement, AiBehavior.PatrolAroundPoint, navigationType, false, flag, isNavalPatrolling);
				float num2 = Campaign.Current.Models.TargetScoreCalculatingModel.CalculateDefensivePatrollingScoreForSettlement(settlement, isNavalPatrolling, mobilePartyOf);
				num2 *= scoreAdjustment;
				if (num2 > 0f)
				{
					if (!mobilePartyOf.IsCurrentlyAtSea)
					{
					}
					ValueTuple<AIBehaviorData, float> valueTuple = new ValueTuple<AIBehaviorData, float>(aibehaviorData, 1.44f + num2);
					p.AddBehaviorScore(in valueTuple);
				}
			}
		}

		// Token: 0x060048B1 RID: 18609 RVA: 0x001707FC File Offset: 0x0016E9FC
		private void CalculateOffensiveNavalPatrollingScoreForSettlement(Settlement settlement, PartyThinkParams p, float scoreAdjustment)
		{
			MobileParty mobilePartyOf = p.MobilePartyOf;
			MobileParty.NavigationType navigationType;
			float num;
			bool flag;
			AiHelper.GetBestNavigationTypeAndAdjustedDistanceOfSettlementForMobileParty(mobilePartyOf, settlement, true, out navigationType, out num, out flag);
			if (navigationType != MobileParty.NavigationType.None)
			{
				AIBehaviorData aibehaviorData = new AIBehaviorData(settlement, AiBehavior.PatrolAroundPoint, navigationType, false, flag, true);
				float num2 = Campaign.Current.Models.TargetScoreCalculatingModel.CalculateOffensivePatrollingScoreForSettlement(settlement, true, mobilePartyOf);
				num2 *= scoreAdjustment;
				if (num2 > 0f)
				{
					ValueTuple<AIBehaviorData, float> valueTuple = new ValueTuple<AIBehaviorData, float>(aibehaviorData, 1.44f + num2);
					p.AddBehaviorScore(in valueTuple);
				}
			}
		}

		// Token: 0x04001411 RID: 5137
		private const float BasePatrolScore = 1.44f;

		// Token: 0x04001412 RID: 5138
		private const float MinimumDefensivePatrolDistanceScore = 0.2f;

		// Token: 0x04001413 RID: 5139
		private const float MaximumDefensivePatrolDistanceScore = 1f;

		// Token: 0x04001414 RID: 5140
		private const float MinimumOffensivePatrolDistanceScore = 0.5f;

		// Token: 0x04001415 RID: 5141
		private const float MaximumOffensivePatrolDistanceScore = 1.5f;

		// Token: 0x04001416 RID: 5142
		private IDisbandPartyCampaignBehavior _disbandPartyCampaignBehavior;
	}
}
