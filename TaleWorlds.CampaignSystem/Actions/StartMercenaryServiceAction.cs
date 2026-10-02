using System;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x020004CA RID: 1226
	public static class StartMercenaryServiceAction
	{
		// Token: 0x06004AFA RID: 19194 RVA: 0x0017BF54 File Offset: 0x0017A154
		private static void ApplyStart(Clan clan, Kingdom kingdom, int awardMultiplier, StartMercenaryServiceAction.StartMercenaryServiceActionDetails details)
		{
			if (clan.IsUnderMercenaryService)
			{
				EndMercenaryServiceAction.EndByLeavingKingdom(clan);
			}
			clan.MercenaryAwardMultiplier = awardMultiplier;
			clan.Kingdom = kingdom;
			clan.StartMercenaryService();
			if (clan == Clan.PlayerClan)
			{
				Campaign.Current.KingdomManager.PlayerMercenaryServiceNextRenewalDay = Campaign.CurrentTime + 30f * (float)CampaignTime.HoursInDay;
			}
			CampaignEventDispatcher.Instance.OnMercenaryServiceStarted(clan, details);
		}

		// Token: 0x06004AFB RID: 19195 RVA: 0x0017BFB8 File Offset: 0x0017A1B8
		public static void ApplyByDefault(Clan clan, Kingdom kingdom, int awardMultiplier)
		{
			StartMercenaryServiceAction.ApplyStart(clan, kingdom, awardMultiplier, StartMercenaryServiceAction.StartMercenaryServiceActionDetails.ApplyByDefault);
		}

		// Token: 0x020008A5 RID: 2213
		public enum StartMercenaryServiceActionDetails
		{
			// Token: 0x040024F6 RID: 9462
			ApplyByDefault
		}
	}
}
