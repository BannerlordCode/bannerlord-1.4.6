using System;
using TaleWorlds.CampaignSystem.Encyclopedia;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.List
{
	// Token: 0x020000E3 RID: 227
	public class ListTypeVM : ViewModel
	{
		// Token: 0x0600157A RID: 5498 RVA: 0x00055034 File Offset: 0x00053234
		public ListTypeVM(EncyclopediaPage encyclopediaPage)
		{
			this.EncyclopediaPage = encyclopediaPage;
			this.ID = encyclopediaPage.GetIdentifierNames()[0];
			this.ImageID = encyclopediaPage.GetStringID();
			this.Order = encyclopediaPage.HomePageOrderIndex;
			this.RefreshValues();
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x0005506F File Offset: 0x0005326F
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Name = this.EncyclopediaPage.GetName().ToString();
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x0005508D File Offset: 0x0005328D
		public void Execute()
		{
			Campaign.Current.EncyclopediaManager.GoToLink("ListPage", this.ID);
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x0600157D RID: 5501 RVA: 0x000550A9 File Offset: 0x000532A9
		// (set) Token: 0x0600157E RID: 5502 RVA: 0x000550B1 File Offset: 0x000532B1
		[DataSourceProperty]
		public string ID
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
					base.OnPropertyChangedWithValue<string>(value, "ID");
				}
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x0600157F RID: 5503 RVA: 0x000550D4 File Offset: 0x000532D4
		// (set) Token: 0x06001580 RID: 5504 RVA: 0x000550DC File Offset: 0x000532DC
		[DataSourceProperty]
		public int Order
		{
			get
			{
				return this._order;
			}
			set
			{
				if (value != this._order)
				{
					this._order = value;
					base.OnPropertyChangedWithValue(value, "Order");
				}
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06001581 RID: 5505 RVA: 0x000550FA File Offset: 0x000532FA
		// (set) Token: 0x06001582 RID: 5506 RVA: 0x00055102 File Offset: 0x00053302
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

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06001583 RID: 5507 RVA: 0x00055125 File Offset: 0x00053325
		// (set) Token: 0x06001584 RID: 5508 RVA: 0x0005512D File Offset: 0x0005332D
		[DataSourceProperty]
		public string ImageID
		{
			get
			{
				return this._imageId;
			}
			set
			{
				if (value != this._imageId)
				{
					this._imageId = value;
					base.OnPropertyChangedWithValue<string>(value, "ImageID");
				}
			}
		}

		// Token: 0x040009C7 RID: 2503
		public readonly EncyclopediaPage EncyclopediaPage;

		// Token: 0x040009C8 RID: 2504
		private string _name;

		// Token: 0x040009C9 RID: 2505
		private string _id;

		// Token: 0x040009CA RID: 2506
		private string _imageId;

		// Token: 0x040009CB RID: 2507
		private int _order;
	}
}
