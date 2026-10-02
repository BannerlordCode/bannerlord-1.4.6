using System;
using Helpers;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.GameComponents
{
	// Token: 0x02000165 RID: 357
	public class DefaultVillageTradeModel : VillageTradeModel
	{
		// Token: 0x06001B12 RID: 6930 RVA: 0x0008C591 File Offset: 0x0008A791
		public override float TradeBoundDistanceLimitAsDays(MobileParty.NavigationType navigationType)
		{
			return Campaign.Current.GetAverageDistanceBetweenClosestTwoTownsWithNavigationType(navigationType) * 3f / (Campaign.Current.EstimatedAverageVillagerPartySpeed * (float)CampaignTime.HoursInDay);
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x0008C5B8 File Offset: 0x0008A7B8
		public override Settlement GetTradeBoundToAssignForVillage(Village village)
		{
			MobileParty.NavigationType navigationType = MobileParty.NavigationType.Default;
			Settlement settlement = SettlementHelper.FindNearestSettlementToSettlement(village.Settlement, navigationType, (Settlement x) => x.IsTown && x.Town.MapFaction == village.Settlement.MapFaction);
			float distanceLimit = Campaign.Current.Models.VillageTradeModel.TradeBoundDistanceLimitAsDays(navigationType) * Campaign.Current.EstimatedAverageVillagerPartySpeed * (float)CampaignTime.HoursInDay;
			if (settlement != null && Campaign.Current.Models.MapDistanceModel.GetDistance(settlement, village.Settlement, false, false, navigationType) < distanceLimit)
			{
				return settlement;
			}
			Settlement settlement2 = SettlementHelper.FindNearestSettlementToSettlement(village.Settlement, navigationType, (Settlement x) => x.IsTown && x.Town.MapFaction != village.Settlement.MapFaction && !x.Town.MapFaction.IsAtWarWith(village.Settlement.MapFaction) && Campaign.Current.Models.MapDistanceModel.GetDistance(x, village.Settlement, false, false, navigationType) <= distanceLimit);
			if (settlement2 != null && Campaign.Current.Models.MapDistanceModel.GetDistance(settlement2, village.Settlement, false, false, navigationType) < distanceLimit)
			{
				return settlement2;
			}
			return null;
		}
	}
}
