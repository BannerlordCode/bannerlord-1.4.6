using System;
using TaleWorlds.CampaignSystem.Settlements.Workshops;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.TownManagement
{
	// Token: 0x020000AA RID: 170
	public class TownManagementShopItemVM : ViewModel
	{
		// Token: 0x0600105E RID: 4190 RVA: 0x00042D40 File Offset: 0x00040F40
		public TownManagementShopItemVM(Workshop workshop)
		{
			this._workshop = workshop;
			this.IsEmpty = this._workshop.WorkshopType == null;
			if (!this.IsEmpty)
			{
				this.ShopId = this._workshop.WorkshopType.StringId;
			}
			else
			{
				this.ShopId = "empty";
			}
			this.RefreshValues();
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x00042DA0 File Offset: 0x00040FA0
		public override void RefreshValues()
		{
			base.RefreshValues();
			if (!this.IsEmpty)
			{
				this.ShopName = this._workshop.WorkshopType.Name.ToString();
				return;
			}
			this.ShopName = GameTexts.FindText("str_empty", null).ToString();
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x00042DED File Offset: 0x00040FED
		public void ExecuteBeginHint()
		{
			if (this._workshop.WorkshopType != null)
			{
				InformationManager.ShowTooltip(typeof(Workshop), new object[] { this._workshop });
			}
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x00042E1A File Offset: 0x0004101A
		public void ExecuteEndHint()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x00042E21 File Offset: 0x00041021
		// (set) Token: 0x06001063 RID: 4195 RVA: 0x00042E29 File Offset: 0x00041029
		[DataSourceProperty]
		public bool IsEmpty
		{
			get
			{
				return this._isEmpty;
			}
			set
			{
				if (value != this._isEmpty)
				{
					this._isEmpty = value;
					base.OnPropertyChangedWithValue(value, "IsEmpty");
				}
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x00042E47 File Offset: 0x00041047
		// (set) Token: 0x06001065 RID: 4197 RVA: 0x00042E4F File Offset: 0x0004104F
		[DataSourceProperty]
		public string ShopName
		{
			get
			{
				return this._shopName;
			}
			set
			{
				if (value != this._shopName)
				{
					this._shopName = value;
					base.OnPropertyChangedWithValue<string>(value, "ShopName");
				}
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x00042E72 File Offset: 0x00041072
		// (set) Token: 0x06001067 RID: 4199 RVA: 0x00042E7A File Offset: 0x0004107A
		[DataSourceProperty]
		public string ShopId
		{
			get
			{
				return this._shopId;
			}
			set
			{
				if (value != this._shopId)
				{
					this._shopId = value;
					base.OnPropertyChangedWithValue<string>(value, "ShopId");
				}
			}
		}

		// Token: 0x0400077E RID: 1918
		private readonly Workshop _workshop;

		// Token: 0x0400077F RID: 1919
		private bool _isEmpty;

		// Token: 0x04000780 RID: 1920
		private string _shopName;

		// Token: 0x04000781 RID: 1921
		private string _shopId;
	}
}
