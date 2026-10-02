using System;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.WeaponDesign
{
	// Token: 0x02000106 RID: 262
	public class CraftingWeaponClassSelectionOpenedEvent : EventBase
	{
		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x060017A1 RID: 6049 RVA: 0x0005AD22 File Offset: 0x00058F22
		// (set) Token: 0x060017A2 RID: 6050 RVA: 0x0005AD2A File Offset: 0x00058F2A
		public bool IsOpen { get; private set; }

		// Token: 0x060017A3 RID: 6051 RVA: 0x0005AD33 File Offset: 0x00058F33
		public CraftingWeaponClassSelectionOpenedEvent(bool isOpen)
		{
			this.IsOpen = isOpen;
		}
	}
}
