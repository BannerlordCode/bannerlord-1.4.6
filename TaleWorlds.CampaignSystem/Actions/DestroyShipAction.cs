using System;
using TaleWorlds.CampaignSystem.Naval;
using TaleWorlds.CampaignSystem.Party;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x020004AA RID: 1194
	public static class DestroyShipAction
	{
		// Token: 0x06004A6D RID: 19053 RVA: 0x00178B84 File Offset: 0x00176D84
		private static void ApplyInternal(Ship ship, DestroyShipAction.ShipDestroyDetail detail)
		{
			PartyBase owner = ship.Owner;
			if (owner != null)
			{
				MobileParty mobileParty = owner.MobileParty;
				if (mobileParty != null)
				{
					mobileParty.SetNavalVisualAsDirty();
				}
			}
			ship.Owner = null;
			CampaignEventDispatcher.Instance.OnShipDestroyed(owner, ship, detail);
		}

		// Token: 0x06004A6E RID: 19054 RVA: 0x00178BC0 File Offset: 0x00176DC0
		public static void Apply(Ship ship)
		{
			DestroyShipAction.ApplyInternal(ship, DestroyShipAction.ShipDestroyDetail.ApplyDefault);
		}

		// Token: 0x06004A6F RID: 19055 RVA: 0x00178BC9 File Offset: 0x00176DC9
		public static void ApplyByDiscard(Ship ship)
		{
			DestroyShipAction.ApplyInternal(ship, DestroyShipAction.ShipDestroyDetail.ApplyByDiscard);
		}

		// Token: 0x02000895 RID: 2197
		public enum ShipDestroyDetail
		{
			// Token: 0x040024AD RID: 9389
			ApplyDefault,
			// Token: 0x040024AE RID: 9390
			ApplyByDiscard
		}
	}
}
