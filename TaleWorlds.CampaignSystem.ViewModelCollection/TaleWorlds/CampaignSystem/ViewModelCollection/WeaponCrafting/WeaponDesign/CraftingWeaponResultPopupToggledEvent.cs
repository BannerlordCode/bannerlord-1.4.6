using System;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.WeaponDesign
{
	// Token: 0x0200010B RID: 267
	public class CraftingWeaponResultPopupToggledEvent : EventBase
	{
		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x060017ED RID: 6125 RVA: 0x0005B731 File Offset: 0x00059931
		public bool IsOpen { get; }

		// Token: 0x060017EE RID: 6126 RVA: 0x0005B739 File Offset: 0x00059939
		public CraftingWeaponResultPopupToggledEvent(bool isOpen)
		{
			this.IsOpen = isOpen;
		}
	}
}
