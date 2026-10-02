using System;
using TaleWorlds.CampaignSystem.ViewModelCollection.Inventory;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.WeaponDesign
{
	// Token: 0x02000100 RID: 256
	public class CraftingItemFlagVM : ItemFlagVM
	{
		// Token: 0x0600175A RID: 5978 RVA: 0x0005A388 File Offset: 0x00058588
		public CraftingItemFlagVM(string iconPath, TextObject hint, bool isDisplayed)
			: base(iconPath, hint)
		{
			this.IsDisplayed = isDisplayed;
			this.IconPath = "SPGeneral\\" + iconPath;
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x0600175B RID: 5979 RVA: 0x0005A3AA File Offset: 0x000585AA
		// (set) Token: 0x0600175C RID: 5980 RVA: 0x0005A3B2 File Offset: 0x000585B2
		[DataSourceProperty]
		public bool IsDisplayed
		{
			get
			{
				return this._isDisplayed;
			}
			set
			{
				if (value != this._isDisplayed)
				{
					this._isDisplayed = value;
					base.OnPropertyChangedWithValue(value, "IsDisplayed");
				}
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x0600175D RID: 5981 RVA: 0x0005A3D0 File Offset: 0x000585D0
		// (set) Token: 0x0600175E RID: 5982 RVA: 0x0005A3D8 File Offset: 0x000585D8
		[DataSourceProperty]
		public string IconPath
		{
			get
			{
				return this._iconPath;
			}
			set
			{
				if (value != this._iconPath)
				{
					this._iconPath = value;
					base.OnPropertyChangedWithValue<string>(value, "IconPath");
				}
			}
		}

		// Token: 0x04000AAD RID: 2733
		private bool _isDisplayed;

		// Token: 0x04000AAE RID: 2734
		private string _iconPath;
	}
}
