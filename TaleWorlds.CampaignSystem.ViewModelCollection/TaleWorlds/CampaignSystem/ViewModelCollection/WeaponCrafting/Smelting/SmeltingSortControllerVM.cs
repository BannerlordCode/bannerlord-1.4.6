using System;
using System.Collections.Generic;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.Smelting
{
	// Token: 0x02000112 RID: 274
	public class SmeltingSortControllerVM : ViewModel
	{
		// Token: 0x06001923 RID: 6435 RVA: 0x0005FD77 File Offset: 0x0005DF77
		public SmeltingSortControllerVM()
		{
			this._yieldComparer = new SmeltingSortControllerVM.ItemYieldComparer();
			this._typeComparer = new SmeltingSortControllerVM.ItemTypeComparer();
			this._nameComparer = new SmeltingSortControllerVM.ItemNameComparer();
			this.RefreshValues();
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x0005FDA8 File Offset: 0x0005DFA8
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.SortNameText = new TextObject("{=PDdh1sBj}Name", null).ToString();
			this.SortTypeText = new TextObject("{=zMMqgxb1}Type", null).ToString();
			this.SortYieldText = new TextObject("{=v3OF6vBg}Yield", null).ToString();
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x0005FDFD File Offset: 0x0005DFFD
		public void SetListToControl(MBBindingList<SmeltingItemVM> listToControl)
		{
			this._listToControl = listToControl;
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x0005FE08 File Offset: 0x0005E008
		public void SortByCurrentState()
		{
			if (this.IsNameSelected)
			{
				this._listToControl.Sort(this._nameComparer);
				return;
			}
			if (this.IsYieldSelected)
			{
				this._listToControl.Sort(this._yieldComparer);
				return;
			}
			if (this.IsTypeSelected)
			{
				this._listToControl.Sort(this._typeComparer);
			}
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x0005FE64 File Offset: 0x0005E064
		public void ExecuteSortByName()
		{
			int nameState = this.NameState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.NameState = (nameState + 1) % 3;
			if (this.NameState == 0)
			{
				this.NameState++;
			}
			this._nameComparer.SetSortMode(this.NameState == 1);
			this._listToControl.Sort(this._nameComparer);
			this.IsNameSelected = true;
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x0005FECC File Offset: 0x0005E0CC
		public void ExecuteSortByYield()
		{
			int yieldState = this.YieldState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.YieldState = (yieldState + 1) % 3;
			if (this.YieldState == 0)
			{
				this.YieldState++;
			}
			this._yieldComparer.SetSortMode(this.YieldState == 1);
			this._listToControl.Sort(this._yieldComparer);
			this.IsYieldSelected = true;
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x0005FF34 File Offset: 0x0005E134
		public void ExecuteSortByType()
		{
			int typeState = this.TypeState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.TypeState = (typeState + 1) % 3;
			if (this.TypeState == 0)
			{
				this.TypeState++;
			}
			this._typeComparer.SetSortMode(this.TypeState == 1);
			this._listToControl.Sort(this._typeComparer);
			this.IsTypeSelected = true;
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x0005FF9C File Offset: 0x0005E19C
		private void SetAllStates(CampaignUIHelper.SortState state)
		{
			this.NameState = (int)state;
			this.TypeState = (int)state;
			this.YieldState = (int)state;
			this.IsNameSelected = false;
			this.IsTypeSelected = false;
			this.IsYieldSelected = false;
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x0600192B RID: 6443 RVA: 0x0005FFC8 File Offset: 0x0005E1C8
		// (set) Token: 0x0600192C RID: 6444 RVA: 0x0005FFD0 File Offset: 0x0005E1D0
		[DataSourceProperty]
		public int NameState
		{
			get
			{
				return this._nameState;
			}
			set
			{
				if (value != this._nameState)
				{
					this._nameState = value;
					base.OnPropertyChangedWithValue(value, "NameState");
				}
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x0600192D RID: 6445 RVA: 0x0005FFEE File Offset: 0x0005E1EE
		// (set) Token: 0x0600192E RID: 6446 RVA: 0x0005FFF6 File Offset: 0x0005E1F6
		[DataSourceProperty]
		public int TypeState
		{
			get
			{
				return this._typeState;
			}
			set
			{
				if (value != this._typeState)
				{
					this._typeState = value;
					base.OnPropertyChangedWithValue(value, "TypeState");
				}
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x0600192F RID: 6447 RVA: 0x00060014 File Offset: 0x0005E214
		// (set) Token: 0x06001930 RID: 6448 RVA: 0x0006001C File Offset: 0x0005E21C
		[DataSourceProperty]
		public int YieldState
		{
			get
			{
				return this._yieldState;
			}
			set
			{
				if (value != this._yieldState)
				{
					this._yieldState = value;
					base.OnPropertyChangedWithValue(value, "YieldState");
				}
			}
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06001931 RID: 6449 RVA: 0x0006003A File Offset: 0x0005E23A
		// (set) Token: 0x06001932 RID: 6450 RVA: 0x00060042 File Offset: 0x0005E242
		[DataSourceProperty]
		public bool IsNameSelected
		{
			get
			{
				return this._isNameSelected;
			}
			set
			{
				if (value != this._isNameSelected)
				{
					this._isNameSelected = value;
					base.OnPropertyChangedWithValue(value, "IsNameSelected");
				}
			}
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06001933 RID: 6451 RVA: 0x00060060 File Offset: 0x0005E260
		// (set) Token: 0x06001934 RID: 6452 RVA: 0x00060068 File Offset: 0x0005E268
		[DataSourceProperty]
		public bool IsTypeSelected
		{
			get
			{
				return this._isTypeSelected;
			}
			set
			{
				if (value != this._isTypeSelected)
				{
					this._isTypeSelected = value;
					base.OnPropertyChangedWithValue(value, "IsTypeSelected");
				}
			}
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06001935 RID: 6453 RVA: 0x00060086 File Offset: 0x0005E286
		// (set) Token: 0x06001936 RID: 6454 RVA: 0x0006008E File Offset: 0x0005E28E
		[DataSourceProperty]
		public bool IsYieldSelected
		{
			get
			{
				return this._isYieldSelected;
			}
			set
			{
				if (value != this._isYieldSelected)
				{
					this._isYieldSelected = value;
					base.OnPropertyChangedWithValue(value, "IsYieldSelected");
				}
			}
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06001937 RID: 6455 RVA: 0x000600AC File Offset: 0x0005E2AC
		// (set) Token: 0x06001938 RID: 6456 RVA: 0x000600B4 File Offset: 0x0005E2B4
		[DataSourceProperty]
		public string SortTypeText
		{
			get
			{
				return this._sortTypeText;
			}
			set
			{
				if (value != this._sortTypeText)
				{
					this._sortTypeText = value;
					base.OnPropertyChangedWithValue<string>(value, "SortTypeText");
				}
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06001939 RID: 6457 RVA: 0x000600D7 File Offset: 0x0005E2D7
		// (set) Token: 0x0600193A RID: 6458 RVA: 0x000600DF File Offset: 0x0005E2DF
		[DataSourceProperty]
		public string SortNameText
		{
			get
			{
				return this._sortNameText;
			}
			set
			{
				if (value != this._sortNameText)
				{
					this._sortNameText = value;
					base.OnPropertyChangedWithValue<string>(value, "SortNameText");
				}
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x0600193B RID: 6459 RVA: 0x00060102 File Offset: 0x0005E302
		// (set) Token: 0x0600193C RID: 6460 RVA: 0x0006010A File Offset: 0x0005E30A
		[DataSourceProperty]
		public string SortYieldText
		{
			get
			{
				return this._sortYieldText;
			}
			set
			{
				if (value != this._sortYieldText)
				{
					this._sortYieldText = value;
					base.OnPropertyChangedWithValue<string>(value, "SortYieldText");
				}
			}
		}

		// Token: 0x04000B8D RID: 2957
		private MBBindingList<SmeltingItemVM> _listToControl;

		// Token: 0x04000B8E RID: 2958
		private readonly SmeltingSortControllerVM.ItemNameComparer _nameComparer;

		// Token: 0x04000B8F RID: 2959
		private readonly SmeltingSortControllerVM.ItemYieldComparer _yieldComparer;

		// Token: 0x04000B90 RID: 2960
		private readonly SmeltingSortControllerVM.ItemTypeComparer _typeComparer;

		// Token: 0x04000B91 RID: 2961
		private int _nameState;

		// Token: 0x04000B92 RID: 2962
		private int _yieldState;

		// Token: 0x04000B93 RID: 2963
		private int _typeState;

		// Token: 0x04000B94 RID: 2964
		private bool _isNameSelected;

		// Token: 0x04000B95 RID: 2965
		private bool _isYieldSelected;

		// Token: 0x04000B96 RID: 2966
		private bool _isTypeSelected;

		// Token: 0x04000B97 RID: 2967
		private string _sortTypeText;

		// Token: 0x04000B98 RID: 2968
		private string _sortNameText;

		// Token: 0x04000B99 RID: 2969
		private string _sortYieldText;

		// Token: 0x02000279 RID: 633
		public abstract class ItemComparerBase : IComparer<SmeltingItemVM>
		{
			// Token: 0x060025AC RID: 9644 RVA: 0x00081F39 File Offset: 0x00080139
			public void SetSortMode(bool isAscending)
			{
				this._isAscending = isAscending;
			}

			// Token: 0x060025AD RID: 9645
			public abstract int Compare(SmeltingItemVM x, SmeltingItemVM y);

			// Token: 0x060025AE RID: 9646 RVA: 0x00081F42 File Offset: 0x00080142
			protected int ResolveEquality(SmeltingItemVM x, SmeltingItemVM y)
			{
				return x.Name.CompareTo(y.Name);
			}

			// Token: 0x040012CF RID: 4815
			protected bool _isAscending;
		}

		// Token: 0x0200027A RID: 634
		public class ItemNameComparer : SmeltingSortControllerVM.ItemComparerBase
		{
			// Token: 0x060025B0 RID: 9648 RVA: 0x00081F5D File Offset: 0x0008015D
			public override int Compare(SmeltingItemVM x, SmeltingItemVM y)
			{
				if (this._isAscending)
				{
					return y.Name.CompareTo(x.Name) * -1;
				}
				return y.Name.CompareTo(x.Name);
			}
		}

		// Token: 0x0200027B RID: 635
		public class ItemYieldComparer : SmeltingSortControllerVM.ItemComparerBase
		{
			// Token: 0x060025B2 RID: 9650 RVA: 0x00081F94 File Offset: 0x00080194
			public override int Compare(SmeltingItemVM x, SmeltingItemVM y)
			{
				int num = y.Yield.Count.CompareTo(x.Yield.Count);
				if (num != 0)
				{
					return num * (this._isAscending ? (-1) : 1);
				}
				return base.ResolveEquality(x, y);
			}
		}

		// Token: 0x0200027C RID: 636
		public class ItemTypeComparer : SmeltingSortControllerVM.ItemComparerBase
		{
			// Token: 0x060025B4 RID: 9652 RVA: 0x00081FE4 File Offset: 0x000801E4
			public override int Compare(SmeltingItemVM x, SmeltingItemVM y)
			{
				int itemObjectTypeSortIndex = CampaignUIHelper.GetItemObjectTypeSortIndex(x.EquipmentElement.Item);
				int num = CampaignUIHelper.GetItemObjectTypeSortIndex(y.EquipmentElement.Item).CompareTo(itemObjectTypeSortIndex);
				if (num != 0)
				{
					return num * (this._isAscending ? (-1) : 1);
				}
				return base.ResolveEquality(x, y);
			}
		}
	}
}
