using System;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x02000441 RID: 1089
	public class SettlementVariablesBehavior : CampaignBehaviorBase
	{
		// Token: 0x060045F8 RID: 17912 RVA: 0x0015C0E8 File Offset: 0x0015A2E8
		public override void RegisterEvents()
		{
			CampaignEvents.HourlyTickSettlementEvent.AddNonSerializedListener(this, new Action<Settlement>(this.HourlyTickSettlement));
		}

		// Token: 0x060045F9 RID: 17913 RVA: 0x0015C101 File Offset: 0x0015A301
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x060045FA RID: 17914 RVA: 0x0015C104 File Offset: 0x0015A304
		private void HourlyTickSettlement(Settlement settlement)
		{
			if (settlement.LastAttackerParty != null && settlement.Party.MapEvent == null && settlement.Party.SiegeEvent == null && settlement.LastThreatTime.ElapsedDaysUntilNow > this._resetLastAttackerPartyAsDays)
			{
				settlement.LastAttackerParty = null;
			}
		}

		// Token: 0x040013A9 RID: 5033
		private float _resetLastAttackerPartyAsDays = 1f;
	}
}
