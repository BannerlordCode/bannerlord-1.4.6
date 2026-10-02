using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.BannerBuilder
{
	// Token: 0x02000087 RID: 135
	public class BannerBuilderItemVM : ViewModel
	{
		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x0002755E File Offset: 0x0002575E
		// (set) Token: 0x06000AF4 RID: 2804 RVA: 0x00027566 File Offset: 0x00025766
		public BannerIconData IconData { get; private set; }

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x0002756F File Offset: 0x0002576F
		// (set) Token: 0x06000AF6 RID: 2806 RVA: 0x00027577 File Offset: 0x00025777
		public string BackgroundTextureID { get; private set; }

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00027580 File Offset: 0x00025780
		public BannerBuilderItemVM(int key, BannerIconData iconData, Action<BannerBuilderItemVM> onItemSelection)
		{
			this.MeshID = key;
			this.IconData = iconData;
			this._onItemSelection = onItemSelection;
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0002759D File Offset: 0x0002579D
		public BannerBuilderItemVM(int key, string backgroundTextureID, Action<BannerBuilderItemVM> onItemSelection)
		{
			this.MeshID = key;
			this.BackgroundTextureID = backgroundTextureID;
			this._onItemSelection = onItemSelection;
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x000275BA File Offset: 0x000257BA
		public void ExecuteSelection()
		{
			Action<BannerBuilderItemVM> onItemSelection = this._onItemSelection;
			if (onItemSelection == null)
			{
				return;
			}
			onItemSelection(this);
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x000275CD File Offset: 0x000257CD
		// (set) Token: 0x06000AFB RID: 2811 RVA: 0x000275D5 File Offset: 0x000257D5
		[DataSourceProperty]
		public int MeshID
		{
			get
			{
				return this._meshID;
			}
			set
			{
				if (value != this._meshID)
				{
					this._meshID = value;
					base.OnPropertyChangedWithValue(value, "MeshID");
					this.MeshIDAsString = this._meshID.ToString();
				}
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x00027604 File Offset: 0x00025804
		// (set) Token: 0x06000AFD RID: 2813 RVA: 0x0002760C File Offset: 0x0002580C
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

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x0002762A File Offset: 0x0002582A
		// (set) Token: 0x06000AFF RID: 2815 RVA: 0x00027632 File Offset: 0x00025832
		[DataSourceProperty]
		public string MeshIDAsString
		{
			get
			{
				return this._meshIDAsString;
			}
			set
			{
				if (value != this._meshIDAsString)
				{
					this._meshIDAsString = value;
					base.OnPropertyChangedWithValue<string>(value, "MeshIDAsString");
				}
			}
		}

		// Token: 0x04000500 RID: 1280
		private readonly Action<BannerBuilderItemVM> _onItemSelection;

		// Token: 0x04000501 RID: 1281
		public int _meshID;

		// Token: 0x04000502 RID: 1282
		public string _meshIDAsString;

		// Token: 0x04000503 RID: 1283
		public bool _isSelected;
	}
}
