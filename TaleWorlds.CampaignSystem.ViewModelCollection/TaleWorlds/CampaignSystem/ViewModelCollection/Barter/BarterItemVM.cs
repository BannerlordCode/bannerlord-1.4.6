using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.BarterSystem.Barterables;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia;
using TaleWorlds.Core.ImageIdentifiers;
using TaleWorlds.Core.ViewModelCollection.ImageIdentifiers;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Barter
{
	// Token: 0x02000159 RID: 345
	public class BarterItemVM : EncyclopediaLinkVM
	{
		// Token: 0x06002081 RID: 8321 RVA: 0x00076980 File Offset: 0x00074B80
		public BarterItemVM(Barterable barterable, BarterItemVM.BarterTransferEventDelegate OnTransfer, Action onAmountChange, bool isFixed = false)
		{
			this.Barterable = barterable;
			base.ActiveLink = barterable.GetEncyclopediaLink();
			this._onTransfer = OnTransfer;
			this._onAmountChange = onAmountChange;
			this._isFixed = isFixed;
			this.IsItemTransferrable = !isFixed;
			this.BarterableType = this.Barterable.StringID;
			ImageIdentifier visualIdentifier = this.Barterable.GetVisualIdentifier();
			this.HasVisualIdentifier = visualIdentifier != null;
			if (visualIdentifier != null)
			{
				this.VisualIdentifier = new GenericImageIdentifierVM(visualIdentifier);
			}
			else
			{
				this.VisualIdentifier = null;
				FiefBarterable fiefBarterable;
				if ((fiefBarterable = this.Barterable as FiefBarterable) != null)
				{
					this.FiefFileName = fiefBarterable.TargetSettlement.SettlementComponent.BackgroundMeshName;
				}
			}
			this.TotalItemCount = this.Barterable.MaxAmount;
			this.CurrentOfferedAmount = 1;
			this.IsMultiple = this.TotalItemCount > 1;
			this.IsOffered = this.Barterable.IsOffered;
			this.RefreshValues();
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x00076A86 File Offset: 0x00074C86
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.ItemLbl = this.Barterable.Name.ToString();
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x00076AA4 File Offset: 0x00074CA4
		public void RefreshCompabilityWithItem(BarterItemVM item, bool isItemGotOffered)
		{
			if (isItemGotOffered && !item.Barterable.IsCompatible(this.Barterable))
			{
				this._incompatibleItems.Add(item.Barterable);
			}
			else if (!isItemGotOffered && this._incompatibleItems.Contains(item.Barterable))
			{
				this._incompatibleItems.Remove(item.Barterable);
			}
			this.IsItemTransferrable = this._incompatibleItems.Count <= 0;
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x00076B1C File Offset: 0x00074D1C
		public void ExecuteAddOffered()
		{
			int num = (BarterItemVM.IsEntireStackModifierActive ? this.TotalItemCount : (this.CurrentOfferedAmount + (BarterItemVM.IsFiveStackModifierActive ? 5 : 1)));
			this.CurrentOfferedAmount = ((num < this.TotalItemCount) ? num : this.TotalItemCount);
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x00076B64 File Offset: 0x00074D64
		public void ExecuteRemoveOffered()
		{
			int num = (BarterItemVM.IsEntireStackModifierActive ? 1 : (this.CurrentOfferedAmount - (BarterItemVM.IsFiveStackModifierActive ? 5 : 1)));
			this.CurrentOfferedAmount = ((num > 1) ? num : 1);
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x00076B9C File Offset: 0x00074D9C
		public void ExecuteAction()
		{
			if (this.IsItemTransferrable)
			{
				this._onTransfer(this, false);
			}
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x06002087 RID: 8327 RVA: 0x00076BB3 File Offset: 0x00074DB3
		// (set) Token: 0x06002088 RID: 8328 RVA: 0x00076BBB File Offset: 0x00074DBB
		[DataSourceProperty]
		public int TotalItemCount
		{
			get
			{
				return this._totalItemCount;
			}
			set
			{
				if (this._totalItemCount != value)
				{
					this._totalItemCount = value;
					base.OnPropertyChangedWithValue(value, "TotalItemCount");
					this.TotalItemCountText = CampaignUIHelper.GetAbbreviatedValueTextFromValue(value);
				}
			}
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x06002089 RID: 8329 RVA: 0x00076BE5 File Offset: 0x00074DE5
		// (set) Token: 0x0600208A RID: 8330 RVA: 0x00076BED File Offset: 0x00074DED
		[DataSourceProperty]
		public string TotalItemCountText
		{
			get
			{
				return this._totalItemCountText;
			}
			set
			{
				if (this._totalItemCountText != value)
				{
					this._totalItemCountText = value;
					base.OnPropertyChangedWithValue<string>(value, "TotalItemCountText");
				}
			}
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x0600208B RID: 8331 RVA: 0x00076C10 File Offset: 0x00074E10
		// (set) Token: 0x0600208C RID: 8332 RVA: 0x00076C18 File Offset: 0x00074E18
		[DataSourceProperty]
		public int CurrentOfferedAmount
		{
			get
			{
				return this._currentOfferedAmount;
			}
			set
			{
				if (this._currentOfferedAmount != value)
				{
					this.Barterable.CurrentAmount = value;
					Action onAmountChange = this._onAmountChange;
					if (onAmountChange != null)
					{
						onAmountChange();
					}
					this._currentOfferedAmount = value;
					base.OnPropertyChangedWithValue(value, "CurrentOfferedAmount");
					this.CurrentOfferedAmountText = CampaignUIHelper.GetAbbreviatedValueTextFromValue(value);
				}
			}
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x0600208D RID: 8333 RVA: 0x00076C6A File Offset: 0x00074E6A
		// (set) Token: 0x0600208E RID: 8334 RVA: 0x00076C72 File Offset: 0x00074E72
		[DataSourceProperty]
		public string CurrentOfferedAmountText
		{
			get
			{
				return this._currentOfferedAmountText;
			}
			set
			{
				if (this._currentOfferedAmountText != value)
				{
					this._currentOfferedAmountText = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentOfferedAmountText");
				}
			}
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x0600208F RID: 8335 RVA: 0x00076C95 File Offset: 0x00074E95
		// (set) Token: 0x06002090 RID: 8336 RVA: 0x00076C9D File Offset: 0x00074E9D
		[DataSourceProperty]
		public string BarterableType
		{
			get
			{
				return this._barterableType;
			}
			set
			{
				if (this._barterableType != value)
				{
					this._barterableType = value;
					base.OnPropertyChangedWithValue<string>(value, "BarterableType");
				}
			}
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x06002091 RID: 8337 RVA: 0x00076CC0 File Offset: 0x00074EC0
		// (set) Token: 0x06002092 RID: 8338 RVA: 0x00076CC8 File Offset: 0x00074EC8
		[DataSourceProperty]
		public bool HasVisualIdentifier
		{
			get
			{
				return this._hasVisualIdentifier;
			}
			set
			{
				if (this._hasVisualIdentifier != value)
				{
					this._hasVisualIdentifier = value;
					base.OnPropertyChangedWithValue(value, "HasVisualIdentifier");
				}
			}
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06002093 RID: 8339 RVA: 0x00076CE6 File Offset: 0x00074EE6
		// (set) Token: 0x06002094 RID: 8340 RVA: 0x00076CEE File Offset: 0x00074EEE
		[DataSourceProperty]
		public bool IsMultiple
		{
			get
			{
				return this._isMultiple;
			}
			set
			{
				if (this._isMultiple != value)
				{
					this._isMultiple = value;
					base.OnPropertyChangedWithValue(value, "IsMultiple");
				}
			}
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06002095 RID: 8341 RVA: 0x00076D0C File Offset: 0x00074F0C
		// (set) Token: 0x06002096 RID: 8342 RVA: 0x00076D14 File Offset: 0x00074F14
		[DataSourceProperty]
		public bool IsSelectorActive
		{
			get
			{
				return this._isSelectorActive;
			}
			set
			{
				if (this._isSelectorActive != value)
				{
					this._isSelectorActive = value;
					base.OnPropertyChangedWithValue(value, "IsSelectorActive");
				}
			}
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x06002097 RID: 8343 RVA: 0x00076D32 File Offset: 0x00074F32
		// (set) Token: 0x06002098 RID: 8344 RVA: 0x00076D3A File Offset: 0x00074F3A
		[DataSourceProperty]
		public ImageIdentifierVM VisualIdentifier
		{
			get
			{
				return this._visualIdentifier;
			}
			set
			{
				if (this._visualIdentifier != value)
				{
					this._visualIdentifier = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "VisualIdentifier");
				}
			}
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x06002099 RID: 8345 RVA: 0x00076D58 File Offset: 0x00074F58
		// (set) Token: 0x0600209A RID: 8346 RVA: 0x00076D60 File Offset: 0x00074F60
		[DataSourceProperty]
		public string ItemLbl
		{
			get
			{
				return this._itemLbl;
			}
			set
			{
				this._itemLbl = value;
				base.OnPropertyChangedWithValue<string>(value, "ItemLbl");
			}
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x0600209B RID: 8347 RVA: 0x00076D75 File Offset: 0x00074F75
		// (set) Token: 0x0600209C RID: 8348 RVA: 0x00076D7D File Offset: 0x00074F7D
		[DataSourceProperty]
		public string FiefFileName
		{
			get
			{
				return this._fiefFileName;
			}
			set
			{
				this._fiefFileName = value;
				base.OnPropertyChangedWithValue<string>(value, "FiefFileName");
			}
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x0600209D RID: 8349 RVA: 0x00076D92 File Offset: 0x00074F92
		// (set) Token: 0x0600209E RID: 8350 RVA: 0x00076D9A File Offset: 0x00074F9A
		[DataSourceProperty]
		public bool IsItemTransferrable
		{
			get
			{
				return this._isItemTransferrable;
			}
			set
			{
				if (this._isFixed)
				{
					value = false;
				}
				if (this._isItemTransferrable != value)
				{
					this._isItemTransferrable = value;
					base.OnPropertyChangedWithValue(value, "IsItemTransferrable");
				}
			}
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x0600209F RID: 8351 RVA: 0x00076DC3 File Offset: 0x00074FC3
		// (set) Token: 0x060020A0 RID: 8352 RVA: 0x00076DCB File Offset: 0x00074FCB
		[DataSourceProperty]
		public bool IsOffered
		{
			get
			{
				return this._isOffered;
			}
			set
			{
				if (value != this._isOffered)
				{
					this._isOffered = value;
					base.OnPropertyChangedWithValue(value, "IsOffered");
				}
			}
		}

		// Token: 0x04000F1C RID: 3868
		public static bool IsEntireStackModifierActive;

		// Token: 0x04000F1D RID: 3869
		public static bool IsFiveStackModifierActive;

		// Token: 0x04000F1E RID: 3870
		private readonly BarterItemVM.BarterTransferEventDelegate _onTransfer;

		// Token: 0x04000F1F RID: 3871
		private readonly Action _onAmountChange;

		// Token: 0x04000F20 RID: 3872
		private bool _isFixed;

		// Token: 0x04000F21 RID: 3873
		private List<Barterable> _incompatibleItems = new List<Barterable>();

		// Token: 0x04000F22 RID: 3874
		public Barterable Barterable;

		// Token: 0x04000F23 RID: 3875
		public bool _isOffered;

		// Token: 0x04000F24 RID: 3876
		private bool _isItemTransferrable = true;

		// Token: 0x04000F25 RID: 3877
		private string _itemLbl;

		// Token: 0x04000F26 RID: 3878
		private string _fiefFileName;

		// Token: 0x04000F27 RID: 3879
		private string _barterableType = "NULL";

		// Token: 0x04000F28 RID: 3880
		private string _currentOfferedAmountText;

		// Token: 0x04000F29 RID: 3881
		private ImageIdentifierVM _visualIdentifier;

		// Token: 0x04000F2A RID: 3882
		private bool _isSelectorActive;

		// Token: 0x04000F2B RID: 3883
		private bool _hasVisualIdentifier;

		// Token: 0x04000F2C RID: 3884
		private bool _isMultiple;

		// Token: 0x04000F2D RID: 3885
		private int _totalItemCount;

		// Token: 0x04000F2E RID: 3886
		private string _totalItemCountText;

		// Token: 0x04000F2F RID: 3887
		private int _currentOfferedAmount;

		// Token: 0x020002E7 RID: 743
		// (Invoke) Token: 0x0600273D RID: 10045
		public delegate void BarterTransferEventDelegate(BarterItemVM itemVM, bool transferAll);
	}
}
