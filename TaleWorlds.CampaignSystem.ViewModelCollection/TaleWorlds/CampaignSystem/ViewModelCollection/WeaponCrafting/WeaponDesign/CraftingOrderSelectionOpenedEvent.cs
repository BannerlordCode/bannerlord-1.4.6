using System;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.WeaponDesign
{
	// Token: 0x02000108 RID: 264
	public class CraftingOrderSelectionOpenedEvent : EventBase
	{
		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x060017A7 RID: 6055 RVA: 0x0005AD62 File Offset: 0x00058F62
		// (set) Token: 0x060017A8 RID: 6056 RVA: 0x0005AD6A File Offset: 0x00058F6A
		public bool IsOpen { get; private set; }

		// Token: 0x060017A9 RID: 6057 RVA: 0x0005AD73 File Offset: 0x00058F73
		public CraftingOrderSelectionOpenedEvent(bool isOpen)
		{
			this.IsOpen = isOpen;
		}
	}
}
