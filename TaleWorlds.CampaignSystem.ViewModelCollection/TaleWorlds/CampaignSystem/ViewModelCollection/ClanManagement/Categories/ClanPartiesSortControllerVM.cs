using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement.Categories
{
	// Token: 0x0200013E RID: 318
	public class ClanPartiesSortControllerVM : ViewModel
	{
		// Token: 0x06001DDE RID: 7646 RVA: 0x0006E773 File Offset: 0x0006C973
		public ClanPartiesSortControllerVM(MBBindingList<MBBindingList<ClanPartyItemVM>> listsToControl)
		{
			this._listsToControl = listsToControl;
			this._nameComparer = new ClanPartiesSortControllerVM.ItemNameComparer();
			this._locationComparer = new ClanPartiesSortControllerVM.ItemLocationComparer();
			this._sizeComparer = new ClanPartiesSortControllerVM.ItemSizeComparer();
			this._shipCountComparer = new ClanPartiesSortControllerVM.ItemShipCountComparer();
		}

		// Token: 0x06001DDF RID: 7647 RVA: 0x0006E7B0 File Offset: 0x0006C9B0
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.NameText = GameTexts.FindText("str_sort_by_name_label", null).ToString();
			this.LocationText = GameTexts.FindText("str_tooltip_label_location", null).ToString();
			this.SizeText = GameTexts.FindText("str_clan_party_size", null).ToString();
			this.ShipCountText = new TextObject("{=URbKirPS}Ship Count", null).ToString();
		}

		// Token: 0x06001DE0 RID: 7648 RVA: 0x0006E81C File Offset: 0x0006CA1C
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
			foreach (MBBindingList<ClanPartyItemVM> mbbindingList in this._listsToControl)
			{
				mbbindingList.Sort(this._nameComparer);
			}
			this.IsNameSelected = true;
		}

		// Token: 0x06001DE1 RID: 7649 RVA: 0x0006E8B8 File Offset: 0x0006CAB8
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
			foreach (MBBindingList<ClanPartyItemVM> mbbindingList in this._listsToControl)
			{
				mbbindingList.Sort(this._locationComparer);
			}
			this.IsLocationSelected = true;
		}

		// Token: 0x06001DE2 RID: 7650 RVA: 0x0006E954 File Offset: 0x0006CB54
		public void ExecuteSortBySize()
		{
			int sizeState = this.SizeState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.SizeState = (sizeState + 1) % 3;
			if (this.SizeState == 0)
			{
				int sizeState2 = this.SizeState;
				this.SizeState = sizeState2 + 1;
			}
			this._sizeComparer.SetSortMode(this.SizeState == 1);
			foreach (MBBindingList<ClanPartyItemVM> mbbindingList in this._listsToControl)
			{
				mbbindingList.Sort(this._sizeComparer);
			}
			this.IsSizeSelected = true;
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x0006E9F0 File Offset: 0x0006CBF0
		public void ExecuteSortByShipCount()
		{
			int shipCountState = this.ShipCountState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.ShipCountState = (shipCountState + 1) % 3;
			if (this.ShipCountState == 0)
			{
				int shipCountState2 = this.ShipCountState;
				this.ShipCountState = shipCountState2 + 1;
			}
			this._shipCountComparer.SetSortMode(this.ShipCountState == 1);
			foreach (MBBindingList<ClanPartyItemVM> mbbindingList in this._listsToControl)
			{
				mbbindingList.Sort(this._shipCountComparer);
			}
			this.IsShipCountSelected = true;
		}

		// Token: 0x06001DE4 RID: 7652 RVA: 0x0006EA8C File Offset: 0x0006CC8C
		private void SetAllStates(CampaignUIHelper.SortState state)
		{
			this.NameState = (int)state;
			this.LocationState = (int)state;
			this.SizeState = (int)state;
			this.ShipCountState = (int)state;
			this.IsNameSelected = false;
			this.IsLocationSelected = false;
			this.IsSizeSelected = false;
			this.IsShipCountSelected = false;
		}

		// Token: 0x06001DE5 RID: 7653 RVA: 0x0006EAC6 File Offset: 0x0006CCC6
		public void ResetAllStates()
		{
			this.SetAllStates(CampaignUIHelper.SortState.Default);
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06001DE6 RID: 7654 RVA: 0x0006EACF File Offset: 0x0006CCCF
		// (set) Token: 0x06001DE7 RID: 7655 RVA: 0x0006EAD7 File Offset: 0x0006CCD7
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

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06001DE8 RID: 7656 RVA: 0x0006EAF5 File Offset: 0x0006CCF5
		// (set) Token: 0x06001DE9 RID: 7657 RVA: 0x0006EAFD File Offset: 0x0006CCFD
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

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06001DEA RID: 7658 RVA: 0x0006EB1B File Offset: 0x0006CD1B
		// (set) Token: 0x06001DEB RID: 7659 RVA: 0x0006EB23 File Offset: 0x0006CD23
		[DataSourceProperty]
		public int SizeState
		{
			get
			{
				return this._sizeState;
			}
			set
			{
				if (value != this._sizeState)
				{
					this._sizeState = value;
					base.OnPropertyChangedWithValue(value, "SizeState");
				}
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06001DEC RID: 7660 RVA: 0x0006EB41 File Offset: 0x0006CD41
		// (set) Token: 0x06001DED RID: 7661 RVA: 0x0006EB49 File Offset: 0x0006CD49
		[DataSourceProperty]
		public int ShipCountState
		{
			get
			{
				return this._shipCountState;
			}
			set
			{
				if (value != this._shipCountState)
				{
					this._shipCountState = value;
					base.OnPropertyChangedWithValue(value, "ShipCountState");
				}
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06001DEE RID: 7662 RVA: 0x0006EB67 File Offset: 0x0006CD67
		// (set) Token: 0x06001DEF RID: 7663 RVA: 0x0006EB6F File Offset: 0x0006CD6F
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

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06001DF0 RID: 7664 RVA: 0x0006EB8D File Offset: 0x0006CD8D
		// (set) Token: 0x06001DF1 RID: 7665 RVA: 0x0006EB95 File Offset: 0x0006CD95
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

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06001DF2 RID: 7666 RVA: 0x0006EBB3 File Offset: 0x0006CDB3
		// (set) Token: 0x06001DF3 RID: 7667 RVA: 0x0006EBBB File Offset: 0x0006CDBB
		[DataSourceProperty]
		public bool IsSizeSelected
		{
			get
			{
				return this._isSizeSelected;
			}
			set
			{
				if (value != this._isSizeSelected)
				{
					this._isSizeSelected = value;
					base.OnPropertyChangedWithValue(value, "IsSizeSelected");
				}
			}
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06001DF4 RID: 7668 RVA: 0x0006EBD9 File Offset: 0x0006CDD9
		// (set) Token: 0x06001DF5 RID: 7669 RVA: 0x0006EBE1 File Offset: 0x0006CDE1
		[DataSourceProperty]
		public bool IsShipCountSelected
		{
			get
			{
				return this._isShipCountSelected;
			}
			set
			{
				if (value != this._isShipCountSelected)
				{
					this._isShipCountSelected = value;
					base.OnPropertyChangedWithValue(value, "IsShipCountSelected");
				}
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06001DF6 RID: 7670 RVA: 0x0006EBFF File Offset: 0x0006CDFF
		// (set) Token: 0x06001DF7 RID: 7671 RVA: 0x0006EC07 File Offset: 0x0006CE07
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

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06001DF8 RID: 7672 RVA: 0x0006EC2A File Offset: 0x0006CE2A
		// (set) Token: 0x06001DF9 RID: 7673 RVA: 0x0006EC32 File Offset: 0x0006CE32
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

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06001DFA RID: 7674 RVA: 0x0006EC55 File Offset: 0x0006CE55
		// (set) Token: 0x06001DFB RID: 7675 RVA: 0x0006EC5D File Offset: 0x0006CE5D
		[DataSourceProperty]
		public string SizeText
		{
			get
			{
				return this._sizeText;
			}
			set
			{
				if (value != this._sizeText)
				{
					this._sizeText = value;
					base.OnPropertyChangedWithValue<string>(value, "SizeText");
				}
			}
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06001DFC RID: 7676 RVA: 0x0006EC80 File Offset: 0x0006CE80
		// (set) Token: 0x06001DFD RID: 7677 RVA: 0x0006EC88 File Offset: 0x0006CE88
		[DataSourceProperty]
		public string ShipCountText
		{
			get
			{
				return this._shipCountText;
			}
			set
			{
				if (value != this._shipCountText)
				{
					this._shipCountText = value;
					base.OnPropertyChangedWithValue<string>(value, "ShipCountText");
				}
			}
		}

		// Token: 0x04000DF4 RID: 3572
		private readonly MBBindingList<MBBindingList<ClanPartyItemVM>> _listsToControl;

		// Token: 0x04000DF5 RID: 3573
		private readonly ClanPartiesSortControllerVM.ItemNameComparer _nameComparer;

		// Token: 0x04000DF6 RID: 3574
		private readonly ClanPartiesSortControllerVM.ItemLocationComparer _locationComparer;

		// Token: 0x04000DF7 RID: 3575
		private readonly ClanPartiesSortControllerVM.ItemSizeComparer _sizeComparer;

		// Token: 0x04000DF8 RID: 3576
		private readonly ClanPartiesSortControllerVM.ItemShipCountComparer _shipCountComparer;

		// Token: 0x04000DF9 RID: 3577
		private int _nameState;

		// Token: 0x04000DFA RID: 3578
		private int _locationState;

		// Token: 0x04000DFB RID: 3579
		private int _sizeState;

		// Token: 0x04000DFC RID: 3580
		private int _shipCountState;

		// Token: 0x04000DFD RID: 3581
		private bool _isNameSelected;

		// Token: 0x04000DFE RID: 3582
		private bool _isLocationSelected;

		// Token: 0x04000DFF RID: 3583
		private bool _isSizeSelected;

		// Token: 0x04000E00 RID: 3584
		private bool _isShipCountSelected;

		// Token: 0x04000E01 RID: 3585
		private string _nameText;

		// Token: 0x04000E02 RID: 3586
		private string _locationText;

		// Token: 0x04000E03 RID: 3587
		private string _sizeText;

		// Token: 0x04000E04 RID: 3588
		private string _shipCountText;

		// Token: 0x020002BB RID: 699
		public abstract class ItemComparerBase : IComparer<ClanPartyItemVM>
		{
			// Token: 0x0600269A RID: 9882 RVA: 0x00083DF2 File Offset: 0x00081FF2
			public void SetSortMode(bool isAcending)
			{
				this._isAcending = isAcending;
			}

			// Token: 0x0600269B RID: 9883
			public abstract int Compare(ClanPartyItemVM x, ClanPartyItemVM y);

			// Token: 0x04001367 RID: 4967
			protected bool _isAcending;
		}

		// Token: 0x020002BC RID: 700
		public class ItemNameComparer : ClanPartiesSortControllerVM.ItemComparerBase
		{
			// Token: 0x0600269D RID: 9885 RVA: 0x00083E03 File Offset: 0x00082003
			public override int Compare(ClanPartyItemVM x, ClanPartyItemVM y)
			{
				if (this._isAcending)
				{
					return y.Name.CompareTo(x.Name) * -1;
				}
				return y.Name.CompareTo(x.Name);
			}
		}

		// Token: 0x020002BD RID: 701
		public class ItemLocationComparer : ClanPartiesSortControllerVM.ItemComparerBase
		{
			// Token: 0x0600269F RID: 9887 RVA: 0x00083E3C File Offset: 0x0008203C
			public override int Compare(ClanPartyItemVM x, ClanPartyItemVM y)
			{
				int num = this.GetDistanceToMainParty(y).CompareTo(this.GetDistanceToMainParty(x));
				if (this._isAcending)
				{
					return num * -1;
				}
				return num;
			}

			// Token: 0x060026A0 RID: 9888 RVA: 0x00083E70 File Offset: 0x00082070
			private float GetDistanceToMainParty(ClanPartyItemVM item)
			{
				return item.Party.MobileParty.Position.Distance(Hero.MainHero.GetCampaignPosition());
			}
		}

		// Token: 0x020002BE RID: 702
		public class ItemSizeComparer : ClanPartiesSortControllerVM.ItemComparerBase
		{
			// Token: 0x060026A2 RID: 9890 RVA: 0x00083EA8 File Offset: 0x000820A8
			public override int Compare(ClanPartyItemVM x, ClanPartyItemVM y)
			{
				if (this._isAcending)
				{
					return y.Party.MobileParty.MemberRoster.TotalManCount.CompareTo(x.Party.MobileParty.MemberRoster.TotalManCount) * -1;
				}
				return y.Party.MobileParty.MemberRoster.TotalManCount.CompareTo(x.Party.MobileParty.MemberRoster.TotalManCount);
			}
		}

		// Token: 0x020002BF RID: 703
		public class ItemShipCountComparer : ClanPartiesSortControllerVM.ItemComparerBase
		{
			// Token: 0x060026A4 RID: 9892 RVA: 0x00083F2C File Offset: 0x0008212C
			public override int Compare(ClanPartyItemVM x, ClanPartyItemVM y)
			{
				if (this._isAcending)
				{
					return y.Party.Ships.Count.CompareTo(x.Party.Ships.Count) * -1;
				}
				return y.Party.Ships.Count.CompareTo(x.Party.Ships.Count);
			}
		}
	}
}
