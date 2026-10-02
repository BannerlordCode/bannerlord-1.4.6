using System;
using TaleWorlds.CampaignSystem.Encyclopedia;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.List
{
	// Token: 0x020000DD RID: 221
	public class EncyclopediaListItemVM : ViewModel
	{
		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x0600152C RID: 5420 RVA: 0x0005412E File Offset: 0x0005232E
		// (set) Token: 0x0600152D RID: 5421 RVA: 0x00054136 File Offset: 0x00052336
		public object Object { get; private set; }

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x0005413F File Offset: 0x0005233F
		public EncyclopediaListItem ListItem { get; }

		// Token: 0x0600152F RID: 5423 RVA: 0x00054148 File Offset: 0x00052348
		public EncyclopediaListItemVM(EncyclopediaListItem listItem)
		{
			this.Object = listItem.Object;
			this.Id = listItem.Id;
			this._type = listItem.TypeName;
			this.ListItem = listItem;
			this.PlayerCanSeeValues = listItem.PlayerCanSeeValues;
			this._onShowTooltip = listItem.OnShowTooltip;
			this.RefreshValues();
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x000541A4 File Offset: 0x000523A4
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Name = this.ListItem.Name;
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x000541BD File Offset: 0x000523BD
		public void Execute()
		{
			Campaign.Current.EncyclopediaManager.GoToLink(this._type, this.Id);
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x000541DA File Offset: 0x000523DA
		public void SetComparedValue(EncyclopediaListItemComparerBase comparer)
		{
			this.ComparedValue = comparer.GetComparedValueText(this.ListItem);
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x000541EE File Offset: 0x000523EE
		public void ExecuteBeginTooltip()
		{
			Action onShowTooltip = this._onShowTooltip;
			if (onShowTooltip == null)
			{
				return;
			}
			onShowTooltip();
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x00054200 File Offset: 0x00052400
		public void ExecuteEndTooltip()
		{
			if (this._onShowTooltip != null)
			{
				MBInformationManager.HideInformations();
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06001535 RID: 5429 RVA: 0x0005420F File Offset: 0x0005240F
		// (set) Token: 0x06001536 RID: 5430 RVA: 0x00054217 File Offset: 0x00052417
		[DataSourceProperty]
		public bool IsFiltered
		{
			get
			{
				return this._isFiltered;
			}
			set
			{
				if (value != this._isFiltered)
				{
					this._isFiltered = value;
					base.OnPropertyChangedWithValue(value, "IsFiltered");
				}
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x00054235 File Offset: 0x00052435
		// (set) Token: 0x06001538 RID: 5432 RVA: 0x0005423D File Offset: 0x0005243D
		[DataSourceProperty]
		public bool PlayerCanSeeValues
		{
			get
			{
				return this._playerCanSeeValues;
			}
			set
			{
				if (value != this._playerCanSeeValues)
				{
					this._playerCanSeeValues = value;
					base.OnPropertyChangedWithValue(value, "PlayerCanSeeValues");
				}
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06001539 RID: 5433 RVA: 0x0005425B File Offset: 0x0005245B
		// (set) Token: 0x0600153A RID: 5434 RVA: 0x00054263 File Offset: 0x00052463
		[DataSourceProperty]
		public string Id
		{
			get
			{
				return this._id;
			}
			set
			{
				if (value != this._id)
				{
					this._id = value;
					base.OnPropertyChangedWithValue<string>(value, "Id");
				}
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x0600153B RID: 5435 RVA: 0x00054286 File Offset: 0x00052486
		// (set) Token: 0x0600153C RID: 5436 RVA: 0x0005428E File Offset: 0x0005248E
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

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x0600153D RID: 5437 RVA: 0x000542B1 File Offset: 0x000524B1
		// (set) Token: 0x0600153E RID: 5438 RVA: 0x000542B9 File Offset: 0x000524B9
		[DataSourceProperty]
		public string ComparedValue
		{
			get
			{
				return this._comparedValue;
			}
			set
			{
				if (value != this._comparedValue)
				{
					this._comparedValue = value;
					base.OnPropertyChangedWithValue<string>(value, "ComparedValue");
				}
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x0600153F RID: 5439 RVA: 0x000542DC File Offset: 0x000524DC
		// (set) Token: 0x06001540 RID: 5440 RVA: 0x000542E4 File Offset: 0x000524E4
		[DataSourceProperty]
		public bool IsBookmarked
		{
			get
			{
				return this._isBookmarked;
			}
			set
			{
				if (value != this._isBookmarked)
				{
					this._isBookmarked = value;
					base.OnPropertyChangedWithValue(value, "IsBookmarked");
				}
			}
		}

		// Token: 0x040009AA RID: 2474
		private readonly string _type;

		// Token: 0x040009AB RID: 2475
		private readonly Action _onShowTooltip;

		// Token: 0x040009AC RID: 2476
		private string _id;

		// Token: 0x040009AD RID: 2477
		private string _name;

		// Token: 0x040009AE RID: 2478
		private string _comparedValue;

		// Token: 0x040009AF RID: 2479
		private bool _isFiltered;

		// Token: 0x040009B0 RID: 2480
		private bool _isBookmarked;

		// Token: 0x040009B1 RID: 2481
		private bool _playerCanSeeValues;
	}
}
