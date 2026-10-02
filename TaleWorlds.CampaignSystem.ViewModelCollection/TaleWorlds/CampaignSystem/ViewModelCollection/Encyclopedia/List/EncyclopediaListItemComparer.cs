using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Encyclopedia;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.List
{
	// Token: 0x020000DF RID: 223
	public class EncyclopediaListItemComparer : IComparer<EncyclopediaListItemVM>
	{
		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x0600155B RID: 5467 RVA: 0x0005474F File Offset: 0x0005294F
		public EncyclopediaSortController SortController { get; }

		// Token: 0x0600155C RID: 5468 RVA: 0x00054757 File Offset: 0x00052957
		public EncyclopediaListItemComparer(EncyclopediaSortController sortController)
		{
			this.SortController = sortController;
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x00054768 File Offset: 0x00052968
		private int GetBookmarkComparison(EncyclopediaListItemVM x, EncyclopediaListItemVM y)
		{
			return -x.IsBookmarked.CompareTo(y.IsBookmarked);
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x0005478C File Offset: 0x0005298C
		public int Compare(EncyclopediaListItemVM x, EncyclopediaListItemVM y)
		{
			int bookmarkComparison = this.GetBookmarkComparison(x, y);
			if (bookmarkComparison != 0)
			{
				return bookmarkComparison;
			}
			return this.SortController.Comparer.Compare(x.ListItem, y.ListItem);
		}
	}
}
