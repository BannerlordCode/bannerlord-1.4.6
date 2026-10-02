using System;
using System.Linq;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x020004A8 RID: 1192
	public static class DestroyKingdomAction
	{
		// Token: 0x06004A67 RID: 19047 RVA: 0x001789EC File Offset: 0x00176BEC
		private static void ApplyInternal(Kingdom destroyedKingdom, bool isKingdomLeaderDeath = false)
		{
			destroyedKingdom.DeactivateKingdom();
			foreach (Clan clan in destroyedKingdom.Clans.ToList<Clan>())
			{
				if (!clan.IsEliminated)
				{
					if (isKingdomLeaderDeath)
					{
						DestroyClanAction.ApplyByClanLeaderDeath(clan);
					}
					else
					{
						DestroyClanAction.Apply(clan);
					}
					destroyedKingdom.RemoveClanInternal(clan);
				}
			}
			Campaign.Current.FactionManager.RemoveFactionsFromCampaignWars(destroyedKingdom);
			CampaignEventDispatcher.Instance.OnKingdomDestroyed(destroyedKingdom);
		}

		// Token: 0x06004A68 RID: 19048 RVA: 0x00178A80 File Offset: 0x00176C80
		public static void Apply(Kingdom destroyedKingdom)
		{
			DestroyKingdomAction.ApplyInternal(destroyedKingdom, false);
		}

		// Token: 0x06004A69 RID: 19049 RVA: 0x00178A89 File Offset: 0x00176C89
		public static void ApplyByKingdomLeaderDeath(Kingdom destroyedKingdom)
		{
			DestroyKingdomAction.ApplyInternal(destroyedKingdom, true);
		}
	}
}
