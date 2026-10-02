using System;
using TaleWorlds.Core.ViewModelCollection.Selector;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.List
{
	// Token: 0x020000E1 RID: 225
	public class EncyclopediaListSelectorItemVM : SelectorItemVM
	{
		// Token: 0x06001561 RID: 5473 RVA: 0x000547E6 File Offset: 0x000529E6
		public EncyclopediaListSelectorItemVM(EncyclopediaListItemComparer comparer)
			: base(comparer.SortController.Name.ToString())
		{
			this.Comparer = comparer;
		}

		// Token: 0x040009BE RID: 2494
		public EncyclopediaListItemComparer Comparer;
	}
}
