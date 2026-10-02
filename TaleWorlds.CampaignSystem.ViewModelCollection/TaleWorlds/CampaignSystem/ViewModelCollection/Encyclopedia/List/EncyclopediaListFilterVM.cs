using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Encyclopedia;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.List
{
	// Token: 0x020000DC RID: 220
	public class EncyclopediaListFilterVM : ViewModel
	{
		// Token: 0x06001524 RID: 5412 RVA: 0x00054042 File Offset: 0x00052242
		public EncyclopediaListFilterVM(EncyclopediaFilterItem filter, Action<EncyclopediaListFilterVM> UpdateFilters)
		{
			this.Filter = filter;
			this._isSelected = this.Filter.IsActive;
			this._updateFilters = UpdateFilters;
			this.RefreshValues();
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x0005406F File Offset: 0x0005226F
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Name = this.Filter.Name.ToString();
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x0005408D File Offset: 0x0005228D
		public void CopyFilterFrom(Dictionary<EncyclopediaFilterItem, bool> filters)
		{
			if (filters.ContainsKey(this.Filter))
			{
				this.IsSelected = filters[this.Filter];
			}
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x000540AF File Offset: 0x000522AF
		public void ExecuteOnFilterActivated()
		{
			Game.Current.EventManager.TriggerEvent<OnEncyclopediaFilterActivatedEvent>(new OnEncyclopediaFilterActivatedEvent());
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001528 RID: 5416 RVA: 0x000540C5 File Offset: 0x000522C5
		// (set) Token: 0x06001529 RID: 5417 RVA: 0x000540CD File Offset: 0x000522CD
		[DataSourceProperty]
		public bool IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				if (value != this._isSelected)
				{
					this._isSelected = value;
					base.OnPropertyChangedWithValue(value, "IsSelected");
					this.Filter.IsActive = value;
					this._updateFilters(this);
				}
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x0600152A RID: 5418 RVA: 0x00054103 File Offset: 0x00052303
		// (set) Token: 0x0600152B RID: 5419 RVA: 0x0005410B File Offset: 0x0005230B
		[DataSourceProperty]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value != this._name)
				{
					this._name = value;
					base.OnPropertyChangedWithValue<string>(value, "Name");
				}
			}
		}

		// Token: 0x040009A4 RID: 2468
		public readonly EncyclopediaFilterItem Filter;

		// Token: 0x040009A5 RID: 2469
		private readonly Action<EncyclopediaListFilterVM> _updateFilters;

		// Token: 0x040009A6 RID: 2470
		private string _name;

		// Token: 0x040009A7 RID: 2471
		private bool _isSelected;
	}
}
