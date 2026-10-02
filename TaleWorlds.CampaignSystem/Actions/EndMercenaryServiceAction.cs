using System;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x020004B0 RID: 1200
	public static class EndMercenaryServiceAction
	{
		// Token: 0x06004A8A RID: 19082 RVA: 0x0017907A File Offset: 0x0017727A
		private static void Apply(Clan clan, EndMercenaryServiceAction.EndMercenaryServiceActionDetails details)
		{
			clan.EndMercenaryService(details == EndMercenaryServiceAction.EndMercenaryServiceActionDetails.ApplyByLeavingKingdom);
			CampaignEventDispatcher.Instance.OnMercenaryServiceEnded(clan, details);
		}

		// Token: 0x06004A8B RID: 19083 RVA: 0x00179092 File Offset: 0x00177292
		public static void EndByDefault(Clan clan)
		{
			EndMercenaryServiceAction.Apply(clan, EndMercenaryServiceAction.EndMercenaryServiceActionDetails.ApplyByDefault);
		}

		// Token: 0x06004A8C RID: 19084 RVA: 0x0017909B File Offset: 0x0017729B
		public static void EndByLeavingKingdom(Clan clan)
		{
			EndMercenaryServiceAction.Apply(clan, EndMercenaryServiceAction.EndMercenaryServiceActionDetails.ApplyByLeavingKingdom);
		}

		// Token: 0x06004A8D RID: 19085 RVA: 0x001790A4 File Offset: 0x001772A4
		public static void EndByBecomingVassal(Clan clan)
		{
			EndMercenaryServiceAction.Apply(clan, EndMercenaryServiceAction.EndMercenaryServiceActionDetails.ApplyByBecomingVassal);
		}

		// Token: 0x02000896 RID: 2198
		public enum EndMercenaryServiceActionDetails
		{
			// Token: 0x040024B0 RID: 9392
			ApplyByDefault,
			// Token: 0x040024B1 RID: 9393
			ApplyByLeavingKingdom,
			// Token: 0x040024B2 RID: 9394
			ApplyByBecomingVassal
		}
	}
}
