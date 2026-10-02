using System;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia
{
	// Token: 0x020000C8 RID: 200
	public class PlayerToggleTrackSettlementFromEncyclopediaEvent : EventBase
	{
		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06001330 RID: 4912 RVA: 0x0004DBD3 File Offset: 0x0004BDD3
		// (set) Token: 0x06001331 RID: 4913 RVA: 0x0004DBDB File Offset: 0x0004BDDB
		public bool IsCurrentlyTracked { get; private set; }

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001332 RID: 4914 RVA: 0x0004DBE4 File Offset: 0x0004BDE4
		// (set) Token: 0x06001333 RID: 4915 RVA: 0x0004DBEC File Offset: 0x0004BDEC
		public Settlement ToggledTrackedSettlement { get; private set; }

		// Token: 0x06001334 RID: 4916 RVA: 0x0004DBF5 File Offset: 0x0004BDF5
		public PlayerToggleTrackSettlementFromEncyclopediaEvent(Settlement toggleTrackedSettlement, bool isCurrentlyTracked)
		{
			this.ToggledTrackedSettlement = toggleTrackedSettlement;
			this.IsCurrentlyTracked = isCurrentlyTracked;
		}
	}
}
