using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.BannerBuilder
{
	// Token: 0x02000088 RID: 136
	public class BannerBuilderLayerVM : ViewModel
	{
		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x00027655 File Offset: 0x00025855
		// (set) Token: 0x06000B01 RID: 2817 RVA: 0x0002765D File Offset: 0x0002585D
		public BannerData Data { get; private set; }

		// Token: 0x06000B02 RID: 2818 RVA: 0x00027668 File Offset: 0x00025868
		public BannerBuilderLayerVM(BannerData data, int layerIndex)
		{
			this.Data = data;
			this.LayerIndex = layerIndex;
			this._rotationValue = this.Data.Rotation;
			this._positionValue = this.Data.Position;
			this._sizeValue = this.Data.Size;
			this._isDrawStrokeActive = this.Data.DrawStroke;
			this._isMirrorActive = this.Data.Mirror;
			this.Refresh();
			this.IsLayerPattern = layerIndex == 0;
			this.CanDeleteLayer = !this.IsLayerPattern;
			this.TotalAreaSize = 1528;
			this.EditableAreaSize = 512;
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00027714 File Offset: 0x00025914
		public void Refresh()
		{
			this.IconID = this.Data.MeshId;
			this.IconIDAsString = this.IconID.ToString();
			uint color = BannerManager.Instance.ReadOnlyColorPalette[this.Data.ColorId].Color;
			this.Color1 = Color.FromUint(color);
			uint color2 = BannerManager.Instance.ReadOnlyColorPalette[this.Data.ColorId2].Color;
			this.Color2 = Color.FromUint(color2);
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x000277A4 File Offset: 0x000259A4
		public void ExecuteDelete()
		{
			Action<BannerBuilderLayerVM> onDeletion = BannerBuilderLayerVM._onDeletion;
			if (onDeletion == null)
			{
				return;
			}
			onDeletion(this);
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x000277B6 File Offset: 0x000259B6
		public void ExecuteSelection()
		{
			Action<BannerBuilderLayerVM> onSelection = BannerBuilderLayerVM._onSelection;
			if (onSelection == null)
			{
				return;
			}
			onSelection(this);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x000277C8 File Offset: 0x000259C8
		public void SetLayerIndex(int newIndex)
		{
			this.LayerIndex = newIndex;
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x000277D1 File Offset: 0x000259D1
		public void ExecuteSelectColor1()
		{
			Action<int, Action<BannerBuilderColorItemVM>> onColorSelection = BannerBuilderLayerVM._onColorSelection;
			if (onColorSelection == null)
			{
				return;
			}
			onColorSelection(this.Data.ColorId, new Action<BannerBuilderColorItemVM>(this.OnSelectColor1));
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x000277FC File Offset: 0x000259FC
		private void OnSelectColor1(BannerBuilderColorItemVM selectedColor)
		{
			this.Data.ColorId = selectedColor.ColorID;
			this.Color1 = Color.FromUint(selectedColor.BannerColor.Color);
			this.ExecuteUpdateBanner();
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00027839 File Offset: 0x00025A39
		public void ExecuteSelectColor2()
		{
			Action<int, Action<BannerBuilderColorItemVM>> onColorSelection = BannerBuilderLayerVM._onColorSelection;
			if (onColorSelection == null)
			{
				return;
			}
			onColorSelection(this.Data.ColorId2, new Action<BannerBuilderColorItemVM>(this.OnSelectColor2));
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00027864 File Offset: 0x00025A64
		private void OnSelectColor2(BannerBuilderColorItemVM selectedColor)
		{
			this.Data.ColorId2 = selectedColor.ColorID;
			this.Color2 = Color.FromUint(selectedColor.BannerColor.Color);
			this.ExecuteUpdateBanner();
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x000278A4 File Offset: 0x00025AA4
		public void ExecuteSwapColors()
		{
			int colorId = this.Data.ColorId2;
			this.Data.ColorId2 = this.Data.ColorId;
			this.Data.ColorId = colorId;
			Color color = this.Color2;
			Color color2 = this.Color1;
			this.Color1 = color;
			this.Color2 = color2;
			this.Refresh();
			this.ExecuteUpdateBanner();
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0002790B File Offset: 0x00025B0B
		public void ExecuteCenterSigil()
		{
			this.PositionValue = new Vec2((float)this.TotalAreaSize / 2f, (float)this.TotalAreaSize / 2f);
			this.ExecuteUpdateBanner();
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x00027938 File Offset: 0x00025B38
		public void ExecuteResetSize()
		{
			float num = (float)(this.IsLayerPattern ? this.TotalAreaSize : 483);
			this.SizeValue = new Vec2(num, num);
			this.ExecuteUpdateBanner();
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0002796F File Offset: 0x00025B6F
		public void ExecuteUpdateBanner()
		{
			Action refresh = BannerBuilderLayerVM._refresh;
			if (refresh == null)
			{
				return;
			}
			refresh();
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x00027980 File Offset: 0x00025B80
		// (set) Token: 0x06000B10 RID: 2832 RVA: 0x00027988 File Offset: 0x00025B88
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

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x000279A6 File Offset: 0x00025BA6
		// (set) Token: 0x06000B12 RID: 2834 RVA: 0x000279AE File Offset: 0x00025BAE
		[DataSourceProperty]
		public bool CanDeleteLayer
		{
			get
			{
				return this._canDeleteLayer;
			}
			set
			{
				if (value != this._canDeleteLayer)
				{
					this._canDeleteLayer = value;
					base.OnPropertyChangedWithValue(value, "CanDeleteLayer");
				}
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x000279CC File Offset: 0x00025BCC
		// (set) Token: 0x06000B14 RID: 2836 RVA: 0x000279D4 File Offset: 0x00025BD4
		[DataSourceProperty]
		public bool IsLayerPattern
		{
			get
			{
				return this._isLayerPattern;
			}
			set
			{
				if (value != this._isLayerPattern)
				{
					this._isLayerPattern = value;
					base.OnPropertyChangedWithValue(value, "IsLayerPattern");
				}
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x000279F2 File Offset: 0x00025BF2
		// (set) Token: 0x06000B16 RID: 2838 RVA: 0x000279FA File Offset: 0x00025BFA
		[DataSourceProperty]
		public bool IsDrawStrokeActive
		{
			get
			{
				return this._isDrawStrokeActive;
			}
			set
			{
				if (value != this._isDrawStrokeActive)
				{
					this._isDrawStrokeActive = value;
					base.OnPropertyChangedWithValue(value, "IsDrawStrokeActive");
					this.Data.DrawStroke = value;
					this.ExecuteUpdateBanner();
				}
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x00027A2A File Offset: 0x00025C2A
		// (set) Token: 0x06000B18 RID: 2840 RVA: 0x00027A32 File Offset: 0x00025C32
		[DataSourceProperty]
		public bool IsMirrorActive
		{
			get
			{
				return this._isMirrorActive;
			}
			set
			{
				if (value != this._isMirrorActive)
				{
					this._isMirrorActive = value;
					base.OnPropertyChangedWithValue(value, "IsMirrorActive");
					this.Data.Mirror = value;
					this.ExecuteUpdateBanner();
				}
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x00027A62 File Offset: 0x00025C62
		// (set) Token: 0x06000B1A RID: 2842 RVA: 0x00027A6A File Offset: 0x00025C6A
		[DataSourceProperty]
		public float RotationValue
		{
			get
			{
				return this._rotationValue;
			}
			set
			{
				if (value != this._rotationValue)
				{
					this._rotationValue = value;
					this.Data.RotationValue = value;
					base.OnPropertyChangedWithValue(value, "RotationValue");
					base.OnPropertyChanged("RotationValue360");
				}
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x00027A9F File Offset: 0x00025C9F
		// (set) Token: 0x06000B1C RID: 2844 RVA: 0x00027AAE File Offset: 0x00025CAE
		[DataSourceProperty]
		public int RotationValue360
		{
			get
			{
				return (int)(this._rotationValue * 360f);
			}
			set
			{
				if (value != (int)(this._rotationValue * 360f))
				{
					this.RotationValue = (float)value / 360f;
					base.OnPropertyChangedWithValue(value, "RotationValue360");
				}
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x00027ADA File Offset: 0x00025CDA
		// (set) Token: 0x06000B1E RID: 2846 RVA: 0x00027AE2 File Offset: 0x00025CE2
		[DataSourceProperty]
		public int IconID
		{
			get
			{
				return this._iconID;
			}
			set
			{
				if (value != this._iconID)
				{
					this._iconID = value;
					base.OnPropertyChangedWithValue(value, "IconID");
				}
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x00027B00 File Offset: 0x00025D00
		// (set) Token: 0x06000B20 RID: 2848 RVA: 0x00027B08 File Offset: 0x00025D08
		[DataSourceProperty]
		public int LayerIndex
		{
			get
			{
				return this._layerIndex;
			}
			set
			{
				if (value != this._layerIndex)
				{
					this._layerIndex = value;
					base.OnPropertyChangedWithValue(value, "LayerIndex");
				}
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x00027B26 File Offset: 0x00025D26
		// (set) Token: 0x06000B22 RID: 2850 RVA: 0x00027B2E File Offset: 0x00025D2E
		[DataSourceProperty]
		public int EditableAreaSize
		{
			get
			{
				return this._editableAreaSize;
			}
			set
			{
				if (value != this._editableAreaSize)
				{
					this._editableAreaSize = value;
					base.OnPropertyChangedWithValue(value, "EditableAreaSize");
				}
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x00027B4C File Offset: 0x00025D4C
		// (set) Token: 0x06000B24 RID: 2852 RVA: 0x00027B54 File Offset: 0x00025D54
		[DataSourceProperty]
		public int TotalAreaSize
		{
			get
			{
				return this._totalAreaSize;
			}
			set
			{
				if (value != this._totalAreaSize)
				{
					this._totalAreaSize = value;
					base.OnPropertyChangedWithValue(value, "TotalAreaSize");
				}
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x00027B72 File Offset: 0x00025D72
		// (set) Token: 0x06000B26 RID: 2854 RVA: 0x00027B7A File Offset: 0x00025D7A
		[DataSourceProperty]
		public string IconIDAsString
		{
			get
			{
				return this._iconIDAsString;
			}
			set
			{
				if (value != this._iconIDAsString)
				{
					this._iconIDAsString = value;
					base.OnPropertyChangedWithValue<string>(value, "IconIDAsString");
				}
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x00027B9D File Offset: 0x00025D9D
		// (set) Token: 0x06000B28 RID: 2856 RVA: 0x00027BA5 File Offset: 0x00025DA5
		[DataSourceProperty]
		public Color Color1
		{
			get
			{
				return this._color1;
			}
			set
			{
				if (value != this._color1)
				{
					this._color1 = value;
					base.OnPropertyChangedWithValue(value, "Color1");
					this.Color1AsStr = value.ToString();
				}
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x00027BDB File Offset: 0x00025DDB
		// (set) Token: 0x06000B2A RID: 2858 RVA: 0x00027BE3 File Offset: 0x00025DE3
		[DataSourceProperty]
		public Color Color2
		{
			get
			{
				return this._color2;
			}
			set
			{
				if (value != this._color2)
				{
					this._color2 = value;
					base.OnPropertyChangedWithValue(value, "Color2");
					this.Color2AsStr = value.ToString();
				}
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x00027C19 File Offset: 0x00025E19
		// (set) Token: 0x06000B2C RID: 2860 RVA: 0x00027C21 File Offset: 0x00025E21
		[DataSourceProperty]
		public string Color1AsStr
		{
			get
			{
				return this._color1AsStr;
			}
			set
			{
				if (value != this._color1AsStr)
				{
					this._color1AsStr = value;
					base.OnPropertyChangedWithValue<string>(value, "Color1AsStr");
				}
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x00027C44 File Offset: 0x00025E44
		// (set) Token: 0x06000B2E RID: 2862 RVA: 0x00027C4C File Offset: 0x00025E4C
		[DataSourceProperty]
		public string Color2AsStr
		{
			get
			{
				return this._color2AsStr;
			}
			set
			{
				if (value != this._color2AsStr)
				{
					this._color2AsStr = value;
					base.OnPropertyChangedWithValue<string>(value, "Color2AsStr");
				}
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x00027C6F File Offset: 0x00025E6F
		// (set) Token: 0x06000B30 RID: 2864 RVA: 0x00027C78 File Offset: 0x00025E78
		[DataSourceProperty]
		public Vec2 PositionValue
		{
			get
			{
				return this._positionValue;
			}
			set
			{
				if (this._positionValue != value)
				{
					this._positionValue = value;
					base.OnPropertyChangedWithValue(value, "PositionValue");
					base.OnPropertyChanged("PositionValueX");
					base.OnPropertyChanged("PositionValueY");
					this.Data.Position = value;
				}
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x00027CC8 File Offset: 0x00025EC8
		// (set) Token: 0x06000B32 RID: 2866 RVA: 0x00027CDC File Offset: 0x00025EDC
		[DataSourceProperty]
		public float PositionValueX
		{
			get
			{
				return (float)Math.Round((double)this._positionValue.X);
			}
			set
			{
				value = (float)Math.Round((double)value);
				if (value != this._positionValue.X)
				{
					this.PositionValue = new Vec2(value, this._positionValue.Y);
					this.Data.Position = this._positionValue;
					base.OnPropertyChangedWithValue(value, "PositionValueX");
				}
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x00027D35 File Offset: 0x00025F35
		// (set) Token: 0x06000B34 RID: 2868 RVA: 0x00027D4C File Offset: 0x00025F4C
		[DataSourceProperty]
		public float PositionValueY
		{
			get
			{
				return (float)Math.Round((double)this._positionValue.Y);
			}
			set
			{
				value = (float)Math.Round((double)value);
				if (value != this._positionValue.Y)
				{
					this.PositionValue = new Vec2(this._positionValue.X, value);
					this.Data.Position = this._positionValue;
					base.OnPropertyChangedWithValue(value, "PositionValueY");
				}
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x00027DA5 File Offset: 0x00025FA5
		// (set) Token: 0x06000B36 RID: 2870 RVA: 0x00027DB0 File Offset: 0x00025FB0
		[DataSourceProperty]
		public Vec2 SizeValue
		{
			get
			{
				return this._sizeValue;
			}
			set
			{
				if (this._sizeValue != value)
				{
					this._sizeValue = value;
					base.OnPropertyChangedWithValue(value, "SizeValue");
					base.OnPropertyChanged("SizeValueX");
					base.OnPropertyChanged("SizeValueY");
					this.Data.Size = value;
				}
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x00027E00 File Offset: 0x00026000
		// (set) Token: 0x06000B38 RID: 2872 RVA: 0x00027E14 File Offset: 0x00026014
		[DataSourceProperty]
		public float SizeValueX
		{
			get
			{
				return (float)Math.Round((double)this._sizeValue.X);
			}
			set
			{
				value = (float)Math.Round((double)value);
				if (value != this._sizeValue.X)
				{
					this.SizeValue = new Vec2(value, this._sizeValue.Y);
					this.Data.Size = this._sizeValue;
					base.OnPropertyChangedWithValue(value, "SizeValueX");
				}
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x00027E6D File Offset: 0x0002606D
		// (set) Token: 0x06000B3A RID: 2874 RVA: 0x00027E84 File Offset: 0x00026084
		[DataSourceProperty]
		public float SizeValueY
		{
			get
			{
				return (float)Math.Round((double)this._sizeValue.Y);
			}
			set
			{
				value = (float)Math.Round((double)value);
				if (value != this._sizeValue.Y)
				{
					this.SizeValue = new Vec2(this._sizeValue.X, value);
					this.Data.Size = this._sizeValue;
					base.OnPropertyChangedWithValue(value, "SizeValueY");
				}
			}
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00027EDD File Offset: 0x000260DD
		public static void SetLayerActions(Action refresh, Action<BannerBuilderLayerVM> onSelection, Action<BannerBuilderLayerVM> onDeletion, Action<int, Action<BannerBuilderColorItemVM>> onColorSelection)
		{
			BannerBuilderLayerVM._onSelection = onSelection;
			BannerBuilderLayerVM._onDeletion = onDeletion;
			BannerBuilderLayerVM._onColorSelection = onColorSelection;
			BannerBuilderLayerVM._refresh = refresh;
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00027EF7 File Offset: 0x000260F7
		public static void ResetLayerActions()
		{
			BannerBuilderLayerVM._onSelection = null;
			BannerBuilderLayerVM._onDeletion = null;
			BannerBuilderLayerVM._onColorSelection = null;
			BannerBuilderLayerVM._refresh = null;
		}

		// Token: 0x04000505 RID: 1285
		private static Action<BannerBuilderLayerVM> _onSelection;

		// Token: 0x04000506 RID: 1286
		private static Action<BannerBuilderLayerVM> _onDeletion;

		// Token: 0x04000507 RID: 1287
		private static Action<int, Action<BannerBuilderColorItemVM>> _onColorSelection;

		// Token: 0x04000508 RID: 1288
		private static Action _refresh;

		// Token: 0x04000509 RID: 1289
		private int _iconID;

		// Token: 0x0400050A RID: 1290
		private string _iconIDAsString;

		// Token: 0x0400050B RID: 1291
		private Color _color1;

		// Token: 0x0400050C RID: 1292
		private Color _color2;

		// Token: 0x0400050D RID: 1293
		private string _color1AsStr;

		// Token: 0x0400050E RID: 1294
		private string _color2AsStr;

		// Token: 0x0400050F RID: 1295
		private bool _isSelected;

		// Token: 0x04000510 RID: 1296
		private bool _canDeleteLayer;

		// Token: 0x04000511 RID: 1297
		private bool _isLayerPattern;

		// Token: 0x04000512 RID: 1298
		private bool _isDrawStrokeActive;

		// Token: 0x04000513 RID: 1299
		private bool _isMirrorActive;

		// Token: 0x04000514 RID: 1300
		private int _editableAreaSize;

		// Token: 0x04000515 RID: 1301
		private int _totalAreaSize;

		// Token: 0x04000516 RID: 1302
		private int _layerIndex;

		// Token: 0x04000517 RID: 1303
		private float _rotationValue;

		// Token: 0x04000518 RID: 1304
		private Vec2 _positionValue;

		// Token: 0x04000519 RID: 1305
		private Vec2 _sizeValue;
	}
}
