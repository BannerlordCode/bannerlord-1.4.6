using System;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.WeaponDesign
{
	// Token: 0x02000107 RID: 263
	public class CraftingOrderTabOpenedEvent : EventBase
	{
		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x060017A4 RID: 6052 RVA: 0x0005AD42 File Offset: 0x00058F42
		// (set) Token: 0x060017A5 RID: 6053 RVA: 0x0005AD4A File Offset: 0x00058F4A
		public bool IsOpen { get; private set; }

		// Token: 0x060017A6 RID: 6054 RVA: 0x0005AD53 File Offset: 0x00058F53
		public CraftingOrderTabOpenedEvent(bool isOpen)
		{
			this.IsOpen = isOpen;
		}
	}
}
