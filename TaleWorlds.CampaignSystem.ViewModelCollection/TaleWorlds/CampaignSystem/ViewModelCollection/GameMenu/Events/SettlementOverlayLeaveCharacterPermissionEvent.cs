using System;
using TaleWorlds.Library.EventSystem;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.Events
{
	// Token: 0x020000C1 RID: 193
	public class SettlementOverlayLeaveCharacterPermissionEvent : EventBase
	{
		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001324 RID: 4900 RVA: 0x0004DB5B File Offset: 0x0004BD5B
		// (set) Token: 0x06001325 RID: 4901 RVA: 0x0004DB63 File Offset: 0x0004BD63
		public Action<bool, TextObject> IsLeaveAvailable { get; private set; }

		// Token: 0x06001326 RID: 4902 RVA: 0x0004DB6C File Offset: 0x0004BD6C
		public SettlementOverlayLeaveCharacterPermissionEvent(Action<bool, TextObject> isLeaveAvailable)
		{
			this.IsLeaveAvailable = isLeaveAvailable;
		}
	}
}
