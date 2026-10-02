using System;
using System.Collections.Generic;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.Encyclopedia
{
	// Token: 0x02000179 RID: 377
	public abstract class EncyclopediaListItemComparerBase : IComparer<EncyclopediaListItem>
	{
		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06001B7C RID: 7036 RVA: 0x0008E086 File Offset: 0x0008C286
		// (set) Token: 0x06001B7D RID: 7037 RVA: 0x0008E08E File Offset: 0x0008C28E
		public bool IsAscending { get; private set; }

		// Token: 0x06001B7E RID: 7038 RVA: 0x0008E097 File Offset: 0x0008C297
		public void SetSortOrder(bool isAscending)
		{
			this.IsAscending = isAscending;
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x0008E0A0 File Offset: 0x0008C2A0
		public void SwitchSortOrder()
		{
			this.IsAscending = !this.IsAscending;
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x0008E0B1 File Offset: 0x0008C2B1
		public void SetDefaultSortOrder()
		{
			this.IsAscending = false;
		}

		// Token: 0x06001B81 RID: 7041
		public abstract int Compare(EncyclopediaListItem x, EncyclopediaListItem y);

		// Token: 0x06001B82 RID: 7042
		public abstract string GetComparedValueText(EncyclopediaListItem item);

		// Token: 0x06001B83 RID: 7043 RVA: 0x0008E0BA File Offset: 0x0008C2BA
		protected int ResolveEquality(EncyclopediaListItem x, EncyclopediaListItem y)
		{
			return x.Name.CompareTo(y.Name);
		}

		// Token: 0x0400094B RID: 2379
		protected readonly TextObject _emptyValue = new TextObject("{=4NaOKslb}-", null);

		// Token: 0x0400094C RID: 2380
		protected readonly TextObject _missingValue = new TextObject("{=keqS2dGa}???", null);
	}
}
