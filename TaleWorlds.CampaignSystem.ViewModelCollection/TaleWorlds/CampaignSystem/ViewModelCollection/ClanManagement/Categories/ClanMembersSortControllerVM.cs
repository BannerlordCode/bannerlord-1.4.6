using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement.Categories
{
	// Token: 0x0200013C RID: 316
	public class ClanMembersSortControllerVM : ViewModel
	{
		// Token: 0x06001DA9 RID: 7593 RVA: 0x0006DB48 File Offset: 0x0006BD48
		public ClanMembersSortControllerVM(MBBindingList<MBBindingList<ClanLordItemVM>> listsToControl)
		{
			this._listsToControl = listsToControl;
			this._nameComparer = new ClanMembersSortControllerVM.ItemNameComparer();
			this._locationComparer = new ClanMembersSortControllerVM.ItemLocationComparer();
		}

		// Token: 0x06001DAA RID: 7594 RVA: 0x0006DB6D File Offset: 0x0006BD6D
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.NameText = GameTexts.FindText("str_sort_by_name_label", null).ToString();
			this.LocationText = GameTexts.FindText("str_tooltip_label_location", null).ToString();
		}

		// Token: 0x06001DAB RID: 7595 RVA: 0x0006DBA4 File Offset: 0x0006BDA4
		public void ExecuteSortByName()
		{
			int nameState = this.NameState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.NameState = (nameState + 1) % 3;
			if (this.NameState == 0)
			{
				int nameState2 = this.NameState;
				this.NameState = nameState2 + 1;
			}
			this._nameComparer.SetSortMode(this.NameState == 1);
			foreach (MBBindingList<ClanLordItemVM> mbbindingList in this._listsToControl)
			{
				mbbindingList.Sort(this._nameComparer);
			}
			this.IsNameSelected = true;
		}

		// Token: 0x06001DAC RID: 7596 RVA: 0x0006DC40 File Offset: 0x0006BE40
		public void ExecuteSortByLocation()
		{
			int locationState = this.LocationState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.LocationState = (locationState + 1) % 3;
			if (this.LocationState == 0)
			{
				int locationState2 = this.LocationState;
				this.LocationState = locationState2 + 1;
			}
			this._locationComparer.SetSortMode(this.LocationState == 1);
			foreach (MBBindingList<ClanLordItemVM> mbbindingList in this._listsToControl)
			{
				mbbindingList.Sort(this._locationComparer);
			}
			this.IsLocationSelected = true;
		}

		// Token: 0x06001DAD RID: 7597 RVA: 0x0006DCDC File Offset: 0x0006BEDC
		private void SetAllStates(CampaignUIHelper.SortState state)
		{
			this.NameState = (int)state;
			this.LocationState = (int)state;
			this.IsNameSelected = false;
			this.IsLocationSelected = false;
		}

		// Token: 0x06001DAE RID: 7598 RVA: 0x0006DCFA File Offset: 0x0006BEFA
		public void ResetAllStates()
		{
			this.SetAllStates(CampaignUIHelper.SortState.Default);
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06001DAF RID: 7599 RVA: 0x0006DD03 File Offset: 0x0006BF03
		// (set) Token: 0x06001DB0 RID: 7600 RVA: 0x0006DD0B File Offset: 0x0006BF0B
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

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06001DB1 RID: 7601 RVA: 0x0006DD29 File Offset: 0x0006BF29
		// (set) Token: 0x06001DB2 RID: 7602 RVA: 0x0006DD31 File Offset: 0x0006BF31
		[DataSourceProperty]
		public int LocationState
		{
			get
			{
				return this._locationState;
			}
			set
			{
				if (value != this._locationState)
				{
					this._locationState = value;
					base.OnPropertyChangedWithValue(value, "LocationState");
				}
			}
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06001DB3 RID: 7603 RVA: 0x0006DD4F File Offset: 0x0006BF4F
		// (set) Token: 0x06001DB4 RID: 7604 RVA: 0x0006DD57 File Offset: 0x0006BF57
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

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06001DB5 RID: 7605 RVA: 0x0006DD75 File Offset: 0x0006BF75
		// (set) Token: 0x06001DB6 RID: 7606 RVA: 0x0006DD7D File Offset: 0x0006BF7D
		[DataSourceProperty]
		public bool IsLocationSelected
		{
			get
			{
				return this._isLocationSelected;
			}
			set
			{
				if (value != this._isLocationSelected)
				{
					this._isLocationSelected = value;
					base.OnPropertyChangedWithValue(value, "IsLocationSelected");
				}
			}
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06001DB7 RID: 7607 RVA: 0x0006DD9B File Offset: 0x0006BF9B
		// (set) Token: 0x06001DB8 RID: 7608 RVA: 0x0006DDA3 File Offset: 0x0006BFA3
		[DataSourceProperty]
		public string NameText
		{
			get
			{
				return this._nameText;
			}
			set
			{
				if (value != this._nameText)
				{
					this._nameText = value;
					base.OnPropertyChangedWithValue<string>(value, "NameText");
				}
			}
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06001DB9 RID: 7609 RVA: 0x0006DDC6 File Offset: 0x0006BFC6
		// (set) Token: 0x06001DBA RID: 7610 RVA: 0x0006DDCE File Offset: 0x0006BFCE
		[DataSourceProperty]
		public string LocationText
		{
			get
			{
				return this._locationText;
			}
			set
			{
				if (value != this._locationText)
				{
					this._locationText = value;
					base.OnPropertyChangedWithValue<string>(value, "LocationText");
				}
			}
		}

		// Token: 0x04000DDB RID: 3547
		private readonly MBBindingList<MBBindingList<ClanLordItemVM>> _listsToControl;

		// Token: 0x04000DDC RID: 3548
		private readonly ClanMembersSortControllerVM.ItemNameComparer _nameComparer;

		// Token: 0x04000DDD RID: 3549
		private readonly ClanMembersSortControllerVM.ItemLocationComparer _locationComparer;

		// Token: 0x04000DDE RID: 3550
		private int _nameState;

		// Token: 0x04000DDF RID: 3551
		private int _locationState;

		// Token: 0x04000DE0 RID: 3552
		private bool _isNameSelected;

		// Token: 0x04000DE1 RID: 3553
		private bool _isLocationSelected;

		// Token: 0x04000DE2 RID: 3554
		private string _nameText;

		// Token: 0x04000DE3 RID: 3555
		private string _locationText;

		// Token: 0x020002B7 RID: 695
		public abstract class ItemComparerBase : IComparer<ClanLordItemVM>
		{
			// Token: 0x0600268B RID: 9867 RVA: 0x00083D08 File Offset: 0x00081F08
			public void SetSortMode(bool isAcending)
			{
				this._isAcending = isAcending;
			}

			// Token: 0x0600268C RID: 9868
			public abstract int Compare(ClanLordItemVM x, ClanLordItemVM y);

			// Token: 0x04001360 RID: 4960
			protected bool _isAcending;
		}

		// Token: 0x020002B8 RID: 696
		public class ItemNameComparer : ClanMembersSortControllerVM.ItemComparerBase
		{
			// Token: 0x0600268E RID: 9870 RVA: 0x00083D19 File Offset: 0x00081F19
			public override int Compare(ClanLordItemVM x, ClanLordItemVM y)
			{
				if (this._isAcending)
				{
					return y.Name.CompareTo(x.Name) * -1;
				}
				return y.Name.CompareTo(x.Name);
			}
		}

		// Token: 0x020002B9 RID: 697
		public class ItemLocationComparer : ClanMembersSortControllerVM.ItemComparerBase
		{
			// Token: 0x06002690 RID: 9872 RVA: 0x00083D50 File Offset: 0x00081F50
			public override int Compare(ClanLordItemVM x, ClanLordItemVM y)
			{
				int num = this.GetDistanceToMainHero(y).CompareTo(this.GetDistanceToMainHero(x));
				if (this._isAcending)
				{
					return num * -1;
				}
				return num;
			}

			// Token: 0x06002691 RID: 9873 RVA: 0x00083D84 File Offset: 0x00081F84
			private float GetDistanceToMainHero(ClanLordItemVM item)
			{
				return item.GetHero().GetCampaignPosition().Distance(Hero.MainHero.GetCampaignPosition());
			}
		}
	}
}
