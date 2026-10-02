using System;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x020004BB RID: 1211
	public static class LiftSiegeAction
	{
		// Token: 0x06004AC3 RID: 19139 RVA: 0x0017A67B File Offset: 0x0017887B
		private static void ApplyInternal(MobileParty side1Party, Settlement settlement)
		{
			settlement.SiegeEvent.BesiegerCamp.RemoveAllSiegeParties();
		}

		// Token: 0x06004AC4 RID: 19140 RVA: 0x0017A68D File Offset: 0x0017888D
		public static void GetGameAction(MobileParty side1Party)
		{
			LiftSiegeAction.ApplyInternal(side1Party, side1Party.BesiegedSettlement);
		}
	}
}
