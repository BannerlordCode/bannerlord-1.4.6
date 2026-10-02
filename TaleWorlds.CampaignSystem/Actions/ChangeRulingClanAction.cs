using System;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x020004A2 RID: 1186
	public class ChangeRulingClanAction
	{
		// Token: 0x06004A48 RID: 19016 RVA: 0x00178218 File Offset: 0x00176418
		private static void ApplyInternal(Kingdom kingdom, Clan newRulerClan)
		{
			Clan rulingClan = kingdom.RulingClan;
			kingdom.RulingClan = newRulerClan;
			CampaignEventDispatcher.Instance.OnRulingClanChanged(kingdom, rulingClan);
		}

		// Token: 0x06004A49 RID: 19017 RVA: 0x0017823F File Offset: 0x0017643F
		public static void Apply(Kingdom kingdom, Clan clan)
		{
			ChangeRulingClanAction.ApplyInternal(kingdom, clan);
		}
	}
}
