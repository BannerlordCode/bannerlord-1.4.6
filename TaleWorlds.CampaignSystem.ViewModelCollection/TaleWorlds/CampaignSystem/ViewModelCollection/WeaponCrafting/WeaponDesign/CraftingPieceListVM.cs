using System;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.WeaponDesign
{
	// Token: 0x02000101 RID: 257
	public class CraftingPieceListVM : ViewModel
	{
		// Token: 0x0600175F RID: 5983 RVA: 0x0005A3FB File Offset: 0x000585FB
		public CraftingPieceListVM(MBBindingList<CraftingPieceVM> pieceList, CraftingPiece.PieceTypes pieceType, Action<CraftingPiece.PieceTypes, bool> onSelect)
		{
			this.Pieces = pieceList;
			this.PieceType = pieceType;
			this._onSelect = onSelect;
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x0005A418 File Offset: 0x00058618
		public void ExecuteSelect()
		{
			Action<CraftingPiece.PieceTypes, bool> onSelect = this._onSelect;
			if (onSelect != null)
			{
				onSelect(this.PieceType, true);
			}
			this.HasNewlyUnlockedPieces = false;
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x0005A439 File Offset: 0x00058639
		public void Refresh()
		{
			this.HasNewlyUnlockedPieces = this.Pieces.Any<CraftingPieceVM>((CraftingPieceVM x) => x.IsNewlyUnlocked);
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06001762 RID: 5986 RVA: 0x0005A46B File Offset: 0x0005866B
		// (set) Token: 0x06001763 RID: 5987 RVA: 0x0005A473 File Offset: 0x00058673
		[DataSourceProperty]
		public bool HasNewlyUnlockedPieces
		{
			get
			{
				return this._hasNewlyUnlockedPieces;
			}
			set
			{
				if (value != this._hasNewlyUnlockedPieces)
				{
					this._hasNewlyUnlockedPieces = value;
					base.OnPropertyChangedWithValue(value, "HasNewlyUnlockedPieces");
				}
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06001764 RID: 5988 RVA: 0x0005A491 File Offset: 0x00058691
		// (set) Token: 0x06001765 RID: 5989 RVA: 0x0005A499 File Offset: 0x00058699
		[DataSourceProperty]
		public MBBindingList<CraftingPieceVM> Pieces
		{
			get
			{
				return this._pieces;
			}
			set
			{
				if (value != this._pieces)
				{
					this._pieces = value;
					base.OnPropertyChangedWithValue<MBBindingList<CraftingPieceVM>>(value, "Pieces");
				}
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06001766 RID: 5990 RVA: 0x0005A4B7 File Offset: 0x000586B7
		// (set) Token: 0x06001767 RID: 5991 RVA: 0x0005A4BF File Offset: 0x000586BF
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
				}
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06001768 RID: 5992 RVA: 0x0005A4DD File Offset: 0x000586DD
		// (set) Token: 0x06001769 RID: 5993 RVA: 0x0005A4E5 File Offset: 0x000586E5
		[DataSourceProperty]
		public bool IsEnabled
		{
			get
			{
				return this._isEnabled;
			}
			set
			{
				if (value != this._isEnabled)
				{
					this._isEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsEnabled");
				}
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x0600176A RID: 5994 RVA: 0x0005A503 File Offset: 0x00058703
		// (set) Token: 0x0600176B RID: 5995 RVA: 0x0005A50B File Offset: 0x0005870B
		[DataSourceProperty]
		public CraftingPieceVM SelectedPiece
		{
			get
			{
				return this._selectedPiece;
			}
			set
			{
				if (value != this._selectedPiece)
				{
					this._selectedPiece = value;
					base.OnPropertyChangedWithValue<CraftingPieceVM>(value, "SelectedPiece");
				}
			}
		}

		// Token: 0x04000AAF RID: 2735
		public CraftingPiece.PieceTypes PieceType;

		// Token: 0x04000AB0 RID: 2736
		private Action<CraftingPiece.PieceTypes, bool> _onSelect;

		// Token: 0x04000AB1 RID: 2737
		private bool _hasNewlyUnlockedPieces;

		// Token: 0x04000AB2 RID: 2738
		private MBBindingList<CraftingPieceVM> _pieces;

		// Token: 0x04000AB3 RID: 2739
		private bool _isSelected;

		// Token: 0x04000AB4 RID: 2740
		private bool _isEnabled;

		// Token: 0x04000AB5 RID: 2741
		private CraftingPieceVM _selectedPiece;
	}
}
