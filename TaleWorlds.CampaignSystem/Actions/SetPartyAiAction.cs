using System;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x020004C7 RID: 1223
	public static class SetPartyAiAction
	{
		// Token: 0x06004AE7 RID: 19175 RVA: 0x0017B6A0 File Offset: 0x001798A0
		private static void ApplyInternal(MobileParty owner, Settlement settlement, MobileParty mobileParty, CampaignVec2 position, SetPartyAiAction.SetPartyAiActionDetail detail, MobileParty.NavigationType navigationType, bool isFromPort, bool isTargetingPort)
		{
			if (detail == SetPartyAiAction.SetPartyAiActionDetail.GoToSettlement)
			{
				if (owner.DefaultBehavior != AiBehavior.GoToSettlement || owner.TargetSettlement != settlement || navigationType != owner.DesiredAiNavigationType || owner.IsTargetingPort != isTargetingPort || owner.StartTransitionNextFrameToExitFromPort != isFromPort)
				{
					if (isFromPort && !owner.IsTransitionInProgress)
					{
						owner.StartTransitionNextFrameToExitFromPort = true;
					}
					owner.SetMoveGoToSettlement(settlement, navigationType, isTargetingPort);
				}
				if (owner.Army != null && owner.Army.LeaderParty == owner)
				{
					owner.Army.ArmyType = Army.ArmyTypes.Defender;
					owner.Army.AiBehaviorObject = settlement;
					return;
				}
			}
			else if (detail == SetPartyAiAction.SetPartyAiActionDetail.PatrolAroundSettlement)
			{
				if (owner.DefaultBehavior != AiBehavior.PatrolAroundPoint || owner.TargetSettlement != settlement || navigationType != owner.DesiredAiNavigationType || owner.IsTargetingPort != isTargetingPort || owner.StartTransitionNextFrameToExitFromPort != isFromPort)
				{
					if (isFromPort && !owner.IsTransitionInProgress)
					{
						owner.StartTransitionNextFrameToExitFromPort = true;
					}
					owner.SetMovePatrolAroundSettlement(settlement, navigationType, isTargetingPort);
				}
				if (owner.Army != null && owner.Army.LeaderParty == owner)
				{
					owner.Army.ArmyType = Army.ArmyTypes.Defender;
					owner.Army.AiBehaviorObject = settlement;
					return;
				}
			}
			else if (detail == SetPartyAiAction.SetPartyAiActionDetail.RaidSettlement)
			{
				if (owner.DefaultBehavior != AiBehavior.RaidSettlement || owner.TargetSettlement != settlement || navigationType != owner.DesiredAiNavigationType || owner.StartTransitionNextFrameToExitFromPort != isFromPort)
				{
					if (isFromPort && !owner.IsTransitionInProgress)
					{
						owner.StartTransitionNextFrameToExitFromPort = true;
					}
					owner.SetMoveRaidSettlement(settlement, navigationType, isTargetingPort);
					if (owner.Army != null && owner.Army.LeaderParty == owner)
					{
						owner.Army.ArmyType = Army.ArmyTypes.Raider;
						owner.Army.AiBehaviorObject = settlement;
						return;
					}
				}
			}
			else if (detail == SetPartyAiAction.SetPartyAiActionDetail.BesiegeSettlement)
			{
				if (owner.DefaultBehavior != AiBehavior.BesiegeSettlement || owner.TargetSettlement != settlement || navigationType != owner.DesiredAiNavigationType || owner.StartTransitionNextFrameToExitFromPort != isFromPort)
				{
					if (isFromPort && !owner.IsTransitionInProgress)
					{
						owner.StartTransitionNextFrameToExitFromPort = true;
					}
					owner.SetMoveBesiegeSettlement(settlement, navigationType);
					if (owner.Army != null && owner.Army.LeaderParty == owner)
					{
						owner.Army.ArmyType = Army.ArmyTypes.Besieger;
						owner.Army.AiBehaviorObject = settlement;
						return;
					}
				}
			}
			else if (detail == SetPartyAiAction.SetPartyAiActionDetail.GoAroundParty)
			{
				if (owner.DefaultBehavior != AiBehavior.GoAroundParty || owner != mobileParty || navigationType != owner.DesiredAiNavigationType || owner.StartTransitionNextFrameToExitFromPort != isFromPort)
				{
					if (isFromPort && !owner.IsTransitionInProgress)
					{
						owner.StartTransitionNextFrameToExitFromPort = true;
					}
					owner.SetMoveGoAroundParty(mobileParty, navigationType);
					return;
				}
			}
			else if (detail == SetPartyAiAction.SetPartyAiActionDetail.EngageParty)
			{
				if (owner.DefaultBehavior != AiBehavior.EngageParty || owner != mobileParty || navigationType != owner.DesiredAiNavigationType || owner.StartTransitionNextFrameToExitFromPort != isFromPort)
				{
					if (isFromPort && !owner.IsTransitionInProgress)
					{
						owner.StartTransitionNextFrameToExitFromPort = true;
					}
					owner.SetMoveEngageParty(mobileParty, navigationType);
					return;
				}
			}
			else if (detail == SetPartyAiAction.SetPartyAiActionDetail.DefendParty)
			{
				if (owner.DefaultBehavior != AiBehavior.DefendSettlement || owner != mobileParty || navigationType != owner.DesiredAiNavigationType || owner.StartTransitionNextFrameToExitFromPort != isFromPort || owner.IsTargetingPort != isTargetingPort)
				{
					if (isFromPort && !owner.IsTransitionInProgress)
					{
						owner.StartTransitionNextFrameToExitFromPort = true;
					}
					owner.SetMoveDefendSettlement(settlement, isTargetingPort, navigationType);
					if (owner.Army != null && owner.Army.LeaderParty == owner)
					{
						owner.Army.ArmyType = Army.ArmyTypes.Defender;
						owner.Army.AiBehaviorObject = settlement;
						return;
					}
				}
			}
			else if (detail == SetPartyAiAction.SetPartyAiActionDetail.EscortParty)
			{
				if (owner.DefaultBehavior != AiBehavior.EscortParty || owner.TargetParty != mobileParty || navigationType != owner.DesiredAiNavigationType || owner.StartTransitionNextFrameToExitFromPort != isFromPort || owner.IsTargetingPort != isTargetingPort)
				{
					if (isFromPort && !owner.IsTransitionInProgress)
					{
						owner.StartTransitionNextFrameToExitFromPort = true;
					}
					owner.SetMoveEscortParty(mobileParty, navigationType, isTargetingPort);
					return;
				}
			}
			else if (detail == SetPartyAiAction.SetPartyAiActionDetail.MoveToNearestLand)
			{
				if (owner.DefaultBehavior != AiBehavior.MoveToNearestLandOrPort)
				{
					owner.SetMoveToNearestLand(settlement);
					return;
				}
			}
			else if (detail == SetPartyAiAction.SetPartyAiActionDetail.PatrolAroundPoint && (owner.DefaultBehavior != AiBehavior.PatrolAroundPoint || navigationType != owner.DesiredAiNavigationType))
			{
				owner.SetMovePatrolAroundPoint(position, navigationType);
			}
		}

		// Token: 0x06004AE8 RID: 19176 RVA: 0x0017BA68 File Offset: 0x00179C68
		public static void GetActionForVisitingSettlement(MobileParty owner, Settlement settlement, MobileParty.NavigationType navigationType, bool isFromPort, bool isTargetingPort)
		{
			SetPartyAiAction.ApplyInternal(owner, settlement, null, CampaignVec2.Zero, SetPartyAiAction.SetPartyAiActionDetail.GoToSettlement, navigationType, isFromPort, isTargetingPort);
		}

		// Token: 0x06004AE9 RID: 19177 RVA: 0x0017BA7C File Offset: 0x00179C7C
		public static void GetActionForPatrollingAroundSettlement(MobileParty owner, Settlement settlement, MobileParty.NavigationType navigationType, bool isFromPort, bool isTargetingPort)
		{
			SetPartyAiAction.ApplyInternal(owner, settlement, null, CampaignVec2.Zero, SetPartyAiAction.SetPartyAiActionDetail.PatrolAroundSettlement, navigationType, isFromPort, isTargetingPort);
		}

		// Token: 0x06004AEA RID: 19178 RVA: 0x0017BA90 File Offset: 0x00179C90
		public static void GetActionForPatrollingAroundPoint(MobileParty owner, CampaignVec2 position, MobileParty.NavigationType navigationType, bool isFromPort)
		{
			SetPartyAiAction.ApplyInternal(owner, null, null, position, SetPartyAiAction.SetPartyAiActionDetail.PatrolAroundPoint, navigationType, isFromPort, false);
		}

		// Token: 0x06004AEB RID: 19179 RVA: 0x0017BA9F File Offset: 0x00179C9F
		public static void GetActionForRaidingSettlement(MobileParty owner, Settlement settlement, MobileParty.NavigationType navigationType, bool isFromPort, bool isTargetingPort)
		{
			SetPartyAiAction.ApplyInternal(owner, settlement, null, CampaignVec2.Zero, SetPartyAiAction.SetPartyAiActionDetail.RaidSettlement, navigationType, isFromPort, isTargetingPort);
		}

		// Token: 0x06004AEC RID: 19180 RVA: 0x0017BAB3 File Offset: 0x00179CB3
		public static void GetActionForBesiegingSettlement(MobileParty owner, Settlement settlement, MobileParty.NavigationType navigationType, bool isFromPort)
		{
			SetPartyAiAction.ApplyInternal(owner, settlement, null, CampaignVec2.Zero, SetPartyAiAction.SetPartyAiActionDetail.BesiegeSettlement, navigationType, isFromPort, false);
		}

		// Token: 0x06004AED RID: 19181 RVA: 0x0017BAC6 File Offset: 0x00179CC6
		public static void GetActionForEngagingParty(MobileParty owner, MobileParty mobileParty, MobileParty.NavigationType navigationType, bool isFromPort)
		{
			SetPartyAiAction.ApplyInternal(owner, null, mobileParty, CampaignVec2.Zero, SetPartyAiAction.SetPartyAiActionDetail.EngageParty, navigationType, isFromPort, false);
		}

		// Token: 0x06004AEE RID: 19182 RVA: 0x0017BAD9 File Offset: 0x00179CD9
		public static void GetActionForGoingAroundParty(MobileParty owner, MobileParty mobileParty, MobileParty.NavigationType navigationType, bool isFromPort)
		{
			SetPartyAiAction.ApplyInternal(owner, null, mobileParty, CampaignVec2.Zero, SetPartyAiAction.SetPartyAiActionDetail.GoAroundParty, navigationType, isFromPort, false);
		}

		// Token: 0x06004AEF RID: 19183 RVA: 0x0017BAEC File Offset: 0x00179CEC
		public static void GetActionForDefendingSettlement(MobileParty owner, Settlement settlement, MobileParty.NavigationType navigationType, bool isFromPort, bool isTargetingPort)
		{
			SetPartyAiAction.ApplyInternal(owner, settlement, null, CampaignVec2.Zero, SetPartyAiAction.SetPartyAiActionDetail.DefendParty, navigationType, isFromPort, isTargetingPort);
		}

		// Token: 0x06004AF0 RID: 19184 RVA: 0x0017BB00 File Offset: 0x00179D00
		public static void GetActionForEscortingParty(MobileParty owner, MobileParty mobileParty, MobileParty.NavigationType navigationType, bool isFromPort, bool isTargetingPort)
		{
			SetPartyAiAction.ApplyInternal(owner, null, mobileParty, CampaignVec2.Zero, SetPartyAiAction.SetPartyAiActionDetail.EscortParty, navigationType, isFromPort, isTargetingPort);
		}

		// Token: 0x06004AF1 RID: 19185 RVA: 0x0017BB14 File Offset: 0x00179D14
		public static void GetActionForMovingToNearestLand(MobileParty owner, Settlement settlement)
		{
			SetPartyAiAction.ApplyInternal(owner, settlement, null, CampaignVec2.Zero, SetPartyAiAction.SetPartyAiActionDetail.MoveToNearestLand, MobileParty.NavigationType.Naval, false, false);
		}

		// Token: 0x020008A2 RID: 2210
		private enum SetPartyAiActionDetail
		{
			// Token: 0x040024E5 RID: 9445
			GoToSettlement,
			// Token: 0x040024E6 RID: 9446
			PatrolAroundSettlement,
			// Token: 0x040024E7 RID: 9447
			PatrolAroundPoint,
			// Token: 0x040024E8 RID: 9448
			RaidSettlement,
			// Token: 0x040024E9 RID: 9449
			BesiegeSettlement,
			// Token: 0x040024EA RID: 9450
			EngageParty,
			// Token: 0x040024EB RID: 9451
			GoAroundParty,
			// Token: 0x040024EC RID: 9452
			DefendParty,
			// Token: 0x040024ED RID: 9453
			EscortParty,
			// Token: 0x040024EE RID: 9454
			MoveToNearestLand
		}
	}
}
