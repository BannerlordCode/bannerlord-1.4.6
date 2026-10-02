using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.WeaponDesign
{
	// Token: 0x02000109 RID: 265
	public class WeaponClassVM : ViewModel
	{
		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x060017AA RID: 6058 RVA: 0x0005AD82 File Offset: 0x00058F82
		// (set) Token: 0x060017AB RID: 6059 RVA: 0x0005AD8A File Offset: 0x00058F8A
		public int NewlyUnlockedPieceCount { get; set; }

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x060017AC RID: 6060 RVA: 0x0005AD93 File Offset: 0x00058F93
		public CraftingTemplate Template { get; }

		// Token: 0x060017AD RID: 6061 RVA: 0x0005AD9C File Offset: 0x00058F9C
		public WeaponClassVM(int selectionIndex, CraftingTemplate template, Action<int> onSelect)
		{
			this._onSelect = onSelect;
			this.SelectionIndex = selectionIndex;
			this.Template = template;
			this._selectedPieces = new Dictionary<CraftingPiece.PieceTypes, string>
			{
				{
					CraftingPiece.PieceTypes.Blade,
					null
				},
				{
					CraftingPiece.PieceTypes.Guard,
					null
				},
				{
					CraftingPiece.PieceTypes.Handle,
					null
				},
				{
					CraftingPiece.PieceTypes.Pommel,
					null
				}
			};
			this.RefreshValues();
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x0005ADF8 File Offset: 0x00058FF8
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.TemplateName = this.Template.TemplateName.ToString();
			this.UnlockedPiecesLabelText = new TextObject("{=OGbskMfz}Unlocked Parts:", null).ToString();
			this.WeaponType = this.Template.StringId;
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x0005AE48 File Offset: 0x00059048
		public void RegisterSelectedPiece(CraftingPiece.PieceTypes type, string pieceID)
		{
			string text;
			if (this._selectedPieces.TryGetValue(type, out text) && text != pieceID)
			{
				this._selectedPieces[type] = pieceID;
			}
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x0005AE7C File Offset: 0x0005907C
		public string GetSelectedPieceData(CraftingPiece.PieceTypes type)
		{
			string text;
			if (this._selectedPieces.TryGetValue(type, out text))
			{
				return text;
			}
			return null;
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x0005AE9C File Offset: 0x0005909C
		public void ExecuteSelect()
		{
			Action<int> onSelect = this._onSelect;
			if (onSelect == null)
			{
				return;
			}
			onSelect(this.SelectionIndex);
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x060017B2 RID: 6066 RVA: 0x0005AEB4 File Offset: 0x000590B4
		// (set) Token: 0x060017B3 RID: 6067 RVA: 0x0005AEBC File Offset: 0x000590BC
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

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x060017B4 RID: 6068 RVA: 0x0005AEDA File Offset: 0x000590DA
		// (set) Token: 0x060017B5 RID: 6069 RVA: 0x0005AEE2 File Offset: 0x000590E2
		[DataSourceProperty]
		public string UnlockedPiecesLabelText
		{
			get
			{
				return this._unlockedPiecesLabelText;
			}
			set
			{
				if (value != this._unlockedPiecesLabelText)
				{
					this._unlockedPiecesLabelText = value;
					base.OnPropertyChangedWithValue<string>(value, "UnlockedPiecesLabelText");
				}
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x060017B6 RID: 6070 RVA: 0x0005AF05 File Offset: 0x00059105
		// (set) Token: 0x060017B7 RID: 6071 RVA: 0x0005AF0D File Offset: 0x0005910D
		[DataSourceProperty]
		public int UnlockedPiecesCount
		{
			get
			{
				return this._unlockedPiecesCount;
			}
			set
			{
				if (value != this._unlockedPiecesCount)
				{
					this._unlockedPiecesCount = value;
					base.OnPropertyChangedWithValue(value, "UnlockedPiecesCount");
				}
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x060017B8 RID: 6072 RVA: 0x0005AF2B File Offset: 0x0005912B
		// (set) Token: 0x060017B9 RID: 6073 RVA: 0x0005AF33 File Offset: 0x00059133
		[DataSourceProperty]
		public string TemplateName
		{
			get
			{
				return this._templateName;
			}
			set
			{
				if (value != this._templateName)
				{
					this._templateName = value;
					base.OnPropertyChangedWithValue<string>(value, "TemplateName");
				}
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x060017BA RID: 6074 RVA: 0x0005AF56 File Offset: 0x00059156
		// (set) Token: 0x060017BB RID: 6075 RVA: 0x0005AF5E File Offset: 0x0005915E
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

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x060017BC RID: 6076 RVA: 0x0005AF7C File Offset: 0x0005917C
		// (set) Token: 0x060017BD RID: 6077 RVA: 0x0005AF84 File Offset: 0x00059184
		[DataSourceProperty]
		public int SelectionIndex
		{
			get
			{
				return this._selectionIndex;
			}
			set
			{
				if (value != this._selectionIndex)
				{
					this._selectionIndex = value;
					base.OnPropertyChangedWithValue(value, "SelectionIndex");
				}
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x060017BE RID: 6078 RVA: 0x0005AFA2 File Offset: 0x000591A2
		// (set) Token: 0x060017BF RID: 6079 RVA: 0x0005AFAA File Offset: 0x000591AA
		[DataSourceProperty]
		public string WeaponType
		{
			get
			{
				return this._weaponType;
			}
			set
			{
				if (value != this._weaponType)
				{
					this._weaponType = value;
					base.OnPropertyChangedWithValue<string>(value, "WeaponType");
				}
			}
		}

		// Token: 0x04000AD7 RID: 2775
		private Action<int> _onSelect;

		// Token: 0x04000AD8 RID: 2776
		private Dictionary<CraftingPiece.PieceTypes, string> _selectedPieces;

		// Token: 0x04000AD9 RID: 2777
		private bool _hasNewlyUnlockedPieces;

		// Token: 0x04000ADA RID: 2778
		private string _unlockedPiecesLabelText;

		// Token: 0x04000ADB RID: 2779
		private int _unlockedPiecesCount;

		// Token: 0x04000ADC RID: 2780
		private string _templateName;

		// Token: 0x04000ADD RID: 2781
		private bool _isSelected;

		// Token: 0x04000ADE RID: 2782
		private int _selectionIndex;

		// Token: 0x04000ADF RID: 2783
		private string _weaponType;
	}
}
