using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Encyclopedia;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.List
{
	// Token: 0x020000DB RID: 219
	public class EncyclopediaFilterGroupVM : ViewModel
	{
		// Token: 0x0600151D RID: 5405 RVA: 0x00053EF0 File Offset: 0x000520F0
		public EncyclopediaFilterGroupVM(EncyclopediaFilterGroup filterGroup, Action<EncyclopediaListFilterVM> UpdateFilters)
		{
			this.FilterGroup = filterGroup;
			this.Filters = new MBBindingList<EncyclopediaListFilterVM>();
			foreach (EncyclopediaFilterItem encyclopediaFilterItem in filterGroup.Filters)
			{
				this.Filters.Add(new EncyclopediaListFilterVM(encyclopediaFilterItem, UpdateFilters));
			}
			this.RefreshValues();
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x00053F6C File Offset: 0x0005216C
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Filters.ApplyActionOnAllItems(delegate(EncyclopediaListFilterVM x)
			{
				x.RefreshValues();
			});
			this.FilterName = this.FilterGroup.Name.ToString();
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x00053FC0 File Offset: 0x000521C0
		public void CopyFiltersFrom(Dictionary<EncyclopediaFilterItem, bool> filters)
		{
			this.Filters.ApplyActionOnAllItems(delegate(EncyclopediaListFilterVM x)
			{
				x.CopyFilterFrom(filters);
			});
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001520 RID: 5408 RVA: 0x00053FF1 File Offset: 0x000521F1
		// (set) Token: 0x06001521 RID: 5409 RVA: 0x00053FF9 File Offset: 0x000521F9
		[DataSourceProperty]
		public string FilterName
		{
			get
			{
				return this._filterName;
			}
			set
			{
				if (value != this._filterName)
				{
					this._filterName = value;
					base.OnPropertyChangedWithValue<string>(value, "FilterName");
				}
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001522 RID: 5410 RVA: 0x0005401C File Offset: 0x0005221C
		// (set) Token: 0x06001523 RID: 5411 RVA: 0x00054024 File Offset: 0x00052224
		[DataSourceProperty]
		public MBBindingList<EncyclopediaListFilterVM> Filters
		{
			get
			{
				return this._filters;
			}
			set
			{
				if (value != this._filters)
				{
					this._filters = value;
					base.OnPropertyChangedWithValue<MBBindingList<EncyclopediaListFilterVM>>(value, "Filters");
				}
			}
		}

		// Token: 0x040009A1 RID: 2465
		public readonly EncyclopediaFilterGroup FilterGroup;

		// Token: 0x040009A2 RID: 2466
		private MBBindingList<EncyclopediaListFilterVM> _filters;

		// Token: 0x040009A3 RID: 2467
		private string _filterName;
	}
}
