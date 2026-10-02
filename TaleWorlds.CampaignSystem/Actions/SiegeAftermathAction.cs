using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x020004C8 RID: 1224
	public static class SiegeAftermathAction
	{
		// Token: 0x06004AF2 RID: 19186 RVA: 0x0017BB28 File Offset: 0x00179D28
		private static void ApplyInternal(MobileParty attackerParty, Settlement settlement, SiegeAftermathAction.SiegeAftermath aftermathType, Clan previousSettlementOwner, Dictionary<MobileParty, float> partyContributions)
		{
			CampaignEventDispatcher.Instance.OnSiegeAftermathApplied(attackerParty, settlement, aftermathType, previousSettlementOwner, partyContributions);
		}

		// Token: 0x06004AF3 RID: 19187 RVA: 0x0017BB3A File Offset: 0x00179D3A
		public static void ApplyAftermath(MobileParty attackerParty, Settlement settlement, SiegeAftermathAction.SiegeAftermath aftermathType, Clan previousSettlementOwner, Dictionary<MobileParty, float> partyContributions)
		{
			SiegeAftermathAction.ApplyInternal(attackerParty, settlement, aftermathType, previousSettlementOwner, partyContributions);
		}

		// Token: 0x020008A3 RID: 2211
		public enum SiegeAftermath
		{
			// Token: 0x040024F0 RID: 9456
			Devastate,
			// Token: 0x040024F1 RID: 9457
			Pillage,
			// Token: 0x040024F2 RID: 9458
			ShowMercy
		}
	}
}
