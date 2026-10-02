using System;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x020004B7 RID: 1207
	public static class IncreaseSettlementHealthAction
	{
		// Token: 0x06004AAF RID: 19119 RVA: 0x001799A8 File Offset: 0x00177BA8
		private static void ApplyInternal(Settlement settlement, float percentage)
		{
			settlement.SettlementHitPoints += percentage;
			settlement.SettlementHitPoints = ((settlement.SettlementHitPoints > 1f) ? 1f : settlement.SettlementHitPoints);
			if (settlement.SettlementHitPoints >= 1f && settlement.IsVillage && settlement.Village.VillageState != Village.VillageStates.Normal)
			{
				ChangeVillageStateAction.ApplyBySettingToNormal(settlement);
				settlement.Militia += 20f;
			}
		}

		// Token: 0x06004AB0 RID: 19120 RVA: 0x00179A1D File Offset: 0x00177C1D
		public static void Apply(Settlement settlement, float percentage)
		{
			IncreaseSettlementHealthAction.ApplyInternal(settlement, percentage);
		}
	}
}
