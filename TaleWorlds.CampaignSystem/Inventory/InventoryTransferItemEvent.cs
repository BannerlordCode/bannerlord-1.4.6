using System;
using TaleWorlds.Core;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.CampaignSystem.Inventory
{
	// Token: 0x020000DB RID: 219
	public class InventoryTransferItemEvent : EventBase
	{
		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001507 RID: 5383 RVA: 0x00060F41 File Offset: 0x0005F141
		// (set) Token: 0x06001508 RID: 5384 RVA: 0x00060F49 File Offset: 0x0005F149
		public ItemObject Item { get; private set; }

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001509 RID: 5385 RVA: 0x00060F52 File Offset: 0x0005F152
		// (set) Token: 0x0600150A RID: 5386 RVA: 0x00060F5A File Offset: 0x0005F15A
		public bool IsBuyForPlayer { get; private set; }

		// Token: 0x0600150B RID: 5387 RVA: 0x00060F63 File Offset: 0x0005F163
		public InventoryTransferItemEvent(ItemObject item, bool isBuyForPlayer)
		{
			this.Item = item;
			this.IsBuyForPlayer = isBuyForPlayer;
		}
	}
}
