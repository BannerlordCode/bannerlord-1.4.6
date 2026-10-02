using System;
using TaleWorlds.Core.ViewModelCollection.Selector;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting
{
	// Token: 0x020000FD RID: 253
	public class CraftingSecondaryUsageItemVM : SelectorItemVM
	{
		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x060016C5 RID: 5829 RVA: 0x000584A6 File Offset: 0x000566A6
		public int UsageIndex { get; }

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x060016C6 RID: 5830 RVA: 0x000584AE File Offset: 0x000566AE
		public int SelectorIndex { get; }

		// Token: 0x060016C7 RID: 5831 RVA: 0x000584B6 File Offset: 0x000566B6
		public CraftingSecondaryUsageItemVM(TextObject name, int index, int usageIndex, SelectorVM<CraftingSecondaryUsageItemVM> parentSelector)
			: base(name)
		{
			this._parentSelector = parentSelector;
			this.SelectorIndex = index;
			this.UsageIndex = usageIndex;
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x000584D5 File Offset: 0x000566D5
		public void ExecuteSelect()
		{
			this._parentSelector.SelectedIndex = this.SelectorIndex;
		}

		// Token: 0x04000A6D RID: 2669
		private SelectorVM<CraftingSecondaryUsageItemVM> _parentSelector;
	}
}
