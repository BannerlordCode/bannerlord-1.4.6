using System;
using System.Collections.Generic;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.TournamentLeaderboard
{
	// Token: 0x020000AE RID: 174
	public class TournamentLeaderboardSortControllerVM : ViewModel
	{
		// Token: 0x060010D8 RID: 4312 RVA: 0x000442CC File Offset: 0x000424CC
		public TournamentLeaderboardSortControllerVM(ref MBBindingList<TournamentLeaderboardEntryItemVM> listToControl)
		{
			this._listToControl = listToControl;
			this._prizeComparer = new TournamentLeaderboardSortControllerVM.ItemPrizeComparer();
			this._nameComparer = new TournamentLeaderboardSortControllerVM.ItemNameComparer();
			this._placementComparer = new TournamentLeaderboardSortControllerVM.ItemPlacementComparer();
			this._victoriesComparer = new TournamentLeaderboardSortControllerVM.ItemVictoriesComparer();
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x00044308 File Offset: 0x00042508
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
			this._listToControl.Sort(this._nameComparer);
			this.IsNameSelected = true;
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x00044374 File Offset: 0x00042574
		public void ExecuteSortByPrize()
		{
			int prizeState = this.PrizeState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.PrizeState = (prizeState + 1) % 3;
			if (this.PrizeState == 0)
			{
				int prizeState2 = this.PrizeState;
				this.PrizeState = prizeState2 + 1;
			}
			this._prizeComparer.SetSortMode(this.PrizeState == 1);
			this._listToControl.Sort(this._prizeComparer);
			this.IsPrizeSelected = true;
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x000443E0 File Offset: 0x000425E0
		public void ExecuteSortByPlacement()
		{
			int placementState = this.PlacementState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.PlacementState = (placementState + 1) % 3;
			if (this.PlacementState == 0)
			{
				int placementState2 = this.PlacementState;
				this.PlacementState = placementState2 + 1;
			}
			this._placementComparer.SetSortMode(this.PlacementState == 1);
			this._listToControl.Sort(this._placementComparer);
			this.IsPlacementSelected = true;
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x0004444C File Offset: 0x0004264C
		public void ExecuteSortByVictories()
		{
			int victoriesState = this.VictoriesState;
			this.SetAllStates(CampaignUIHelper.SortState.Default);
			this.VictoriesState = (victoriesState + 1) % 3;
			if (this.VictoriesState == 0)
			{
				int victoriesState2 = this.VictoriesState;
				this.VictoriesState = victoriesState2 + 1;
			}
			this._victoriesComparer.SetSortMode(this.VictoriesState == 1);
			this._listToControl.Sort(this._victoriesComparer);
			this.IsVictoriesSelected = true;
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x000444B6 File Offset: 0x000426B6
		private void SetAllStates(CampaignUIHelper.SortState state)
		{
			this.NameState = (int)state;
			this.PrizeState = (int)state;
			this.PlacementState = (int)state;
			this.VictoriesState = (int)state;
			this.IsNameSelected = false;
			this.IsVictoriesSelected = false;
			this.IsPrizeSelected = false;
			this.IsPlacementSelected = false;
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x060010DE RID: 4318 RVA: 0x000444F0 File Offset: 0x000426F0
		// (set) Token: 0x060010DF RID: 4319 RVA: 0x000444F8 File Offset: 0x000426F8
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

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x060010E0 RID: 4320 RVA: 0x00044516 File Offset: 0x00042716
		// (set) Token: 0x060010E1 RID: 4321 RVA: 0x0004451E File Offset: 0x0004271E
		[DataSourceProperty]
		public int VictoriesState
		{
			get
			{
				return this._victoriesState;
			}
			set
			{
				if (value != this._victoriesState)
				{
					this._victoriesState = value;
					base.OnPropertyChangedWithValue(value, "VictoriesState");
				}
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x060010E2 RID: 4322 RVA: 0x0004453C File Offset: 0x0004273C
		// (set) Token: 0x060010E3 RID: 4323 RVA: 0x00044544 File Offset: 0x00042744
		[DataSourceProperty]
		public int PrizeState
		{
			get
			{
				return this._prizeState;
			}
			set
			{
				if (value != this._prizeState)
				{
					this._prizeState = value;
					base.OnPropertyChangedWithValue(value, "PrizeState");
				}
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x060010E4 RID: 4324 RVA: 0x00044562 File Offset: 0x00042762
		// (set) Token: 0x060010E5 RID: 4325 RVA: 0x0004456A File Offset: 0x0004276A
		[DataSourceProperty]
		public int PlacementState
		{
			get
			{
				return this._placementState;
			}
			set
			{
				if (value != this._placementState)
				{
					this._placementState = value;
					base.OnPropertyChangedWithValue(value, "PlacementState");
				}
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x060010E6 RID: 4326 RVA: 0x00044588 File Offset: 0x00042788
		// (set) Token: 0x060010E7 RID: 4327 RVA: 0x00044590 File Offset: 0x00042790
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

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x060010E8 RID: 4328 RVA: 0x000445AE File Offset: 0x000427AE
		// (set) Token: 0x060010E9 RID: 4329 RVA: 0x000445B6 File Offset: 0x000427B6
		[DataSourceProperty]
		public bool IsPrizeSelected
		{
			get
			{
				return this._isPrizeSelected;
			}
			set
			{
				if (value != this._isPrizeSelected)
				{
					this._isPrizeSelected = value;
					base.OnPropertyChangedWithValue(value, "IsPrizeSelected");
				}
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x060010EA RID: 4330 RVA: 0x000445D4 File Offset: 0x000427D4
		// (set) Token: 0x060010EB RID: 4331 RVA: 0x000445DC File Offset: 0x000427DC
		[DataSourceProperty]
		public bool IsPlacementSelected
		{
			get
			{
				return this._isPlacementSelected;
			}
			set
			{
				if (value != this._isPlacementSelected)
				{
					this._isPlacementSelected = value;
					base.OnPropertyChangedWithValue(value, "IsPlacementSelected");
				}
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x060010EC RID: 4332 RVA: 0x000445FA File Offset: 0x000427FA
		// (set) Token: 0x060010ED RID: 4333 RVA: 0x00044602 File Offset: 0x00042802
		[DataSourceProperty]
		public bool IsVictoriesSelected
		{
			get
			{
				return this._isVictoriesSelected;
			}
			set
			{
				if (value != this._isVictoriesSelected)
				{
					this._isVictoriesSelected = value;
					base.OnPropertyChangedWithValue(value, "IsVictoriesSelected");
				}
			}
		}

		// Token: 0x040007B1 RID: 1969
		private readonly MBBindingList<TournamentLeaderboardEntryItemVM> _listToControl;

		// Token: 0x040007B2 RID: 1970
		private readonly TournamentLeaderboardSortControllerVM.ItemNameComparer _nameComparer;

		// Token: 0x040007B3 RID: 1971
		private readonly TournamentLeaderboardSortControllerVM.ItemPrizeComparer _prizeComparer;

		// Token: 0x040007B4 RID: 1972
		private readonly TournamentLeaderboardSortControllerVM.ItemPlacementComparer _placementComparer;

		// Token: 0x040007B5 RID: 1973
		private readonly TournamentLeaderboardSortControllerVM.ItemVictoriesComparer _victoriesComparer;

		// Token: 0x040007B6 RID: 1974
		private int _nameState;

		// Token: 0x040007B7 RID: 1975
		private int _prizeState;

		// Token: 0x040007B8 RID: 1976
		private int _placementState;

		// Token: 0x040007B9 RID: 1977
		private int _victoriesState;

		// Token: 0x040007BA RID: 1978
		private bool _isNameSelected;

		// Token: 0x040007BB RID: 1979
		private bool _isPrizeSelected;

		// Token: 0x040007BC RID: 1980
		private bool _isPlacementSelected;

		// Token: 0x040007BD RID: 1981
		private bool _isVictoriesSelected;

		// Token: 0x02000220 RID: 544
		public abstract class ItemComparerBase : IComparer<TournamentLeaderboardEntryItemVM>
		{
			// Token: 0x06002496 RID: 9366 RVA: 0x0008097B File Offset: 0x0007EB7B
			public void SetSortMode(bool isAcending)
			{
				this._isAcending = isAcending;
			}

			// Token: 0x06002497 RID: 9367
			public abstract int Compare(TournamentLeaderboardEntryItemVM x, TournamentLeaderboardEntryItemVM y);

			// Token: 0x040011FA RID: 4602
			protected bool _isAcending;
		}

		// Token: 0x02000221 RID: 545
		public class ItemNameComparer : TournamentLeaderboardSortControllerVM.ItemComparerBase
		{
			// Token: 0x06002499 RID: 9369 RVA: 0x0008098C File Offset: 0x0007EB8C
			public override int Compare(TournamentLeaderboardEntryItemVM x, TournamentLeaderboardEntryItemVM y)
			{
				if (this._isAcending)
				{
					return y.Name.CompareTo(x.Name) * -1;
				}
				return y.Name.CompareTo(x.Name);
			}
		}

		// Token: 0x02000222 RID: 546
		public class ItemPrizeComparer : TournamentLeaderboardSortControllerVM.ItemComparerBase
		{
			// Token: 0x0600249B RID: 9371 RVA: 0x000809C4 File Offset: 0x0007EBC4
			public override int Compare(TournamentLeaderboardEntryItemVM x, TournamentLeaderboardEntryItemVM y)
			{
				if (this._isAcending)
				{
					return y.PrizeValue.CompareTo(x.PrizeValue) * -1;
				}
				return y.PrizeValue.CompareTo(x.PrizeValue);
			}
		}

		// Token: 0x02000223 RID: 547
		public class ItemPlacementComparer : TournamentLeaderboardSortControllerVM.ItemComparerBase
		{
			// Token: 0x0600249D RID: 9373 RVA: 0x00080A0C File Offset: 0x0007EC0C
			public override int Compare(TournamentLeaderboardEntryItemVM x, TournamentLeaderboardEntryItemVM y)
			{
				if (this._isAcending)
				{
					return y.PlacementOnLeaderboard.CompareTo(x.PlacementOnLeaderboard) * -1;
				}
				return y.PlacementOnLeaderboard.CompareTo(x.PlacementOnLeaderboard);
			}
		}

		// Token: 0x02000224 RID: 548
		public class ItemVictoriesComparer : TournamentLeaderboardSortControllerVM.ItemComparerBase
		{
			// Token: 0x0600249F RID: 9375 RVA: 0x00080A54 File Offset: 0x0007EC54
			public override int Compare(TournamentLeaderboardEntryItemVM x, TournamentLeaderboardEntryItemVM y)
			{
				if (this._isAcending)
				{
					return y.Victories.CompareTo(x.Victories) * -1;
				}
				return y.Victories.CompareTo(x.Victories);
			}
		}
	}
}
