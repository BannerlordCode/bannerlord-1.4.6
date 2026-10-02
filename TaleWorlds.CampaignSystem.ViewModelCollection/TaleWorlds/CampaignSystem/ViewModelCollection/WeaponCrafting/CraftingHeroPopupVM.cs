using System;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting
{
	// Token: 0x020000F9 RID: 249
	public class CraftingHeroPopupVM : ViewModel
	{
		// Token: 0x06001687 RID: 5767 RVA: 0x00057E4C File Offset: 0x0005604C
		public CraftingHeroPopupVM(Func<MBBindingList<CraftingAvailableHeroItemVM>> getCraftingHeroes)
		{
			this.GetCraftingHeroes = getCraftingHeroes;
			this.SelectHeroText = new TextObject("{=xaeXEj8J}Select character for smithing", null).ToString();
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x00057E71 File Offset: 0x00056071
		public void ExecuteOpenPopup()
		{
			this.IsVisible = true;
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x00057E7A File Offset: 0x0005607A
		public void ExecuteClosePopup()
		{
			this.IsVisible = false;
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x00057E83 File Offset: 0x00056083
		public override void OnFinalize()
		{
			base.OnFinalize();
			InputKeyItemVM exitInputKey = this.ExitInputKey;
			if (exitInputKey == null)
			{
				return;
			}
			exitInputKey.OnFinalize();
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x0600168B RID: 5771 RVA: 0x00057E9B File Offset: 0x0005609B
		// (set) Token: 0x0600168C RID: 5772 RVA: 0x00057EA3 File Offset: 0x000560A3
		[DataSourceProperty]
		public bool IsVisible
		{
			get
			{
				return this._isVisible;
			}
			set
			{
				if (value != this._isVisible)
				{
					this._isVisible = value;
					base.OnPropertyChangedWithValue(value, "IsVisible");
				}
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x0600168D RID: 5773 RVA: 0x00057EC1 File Offset: 0x000560C1
		// (set) Token: 0x0600168E RID: 5774 RVA: 0x00057EC9 File Offset: 0x000560C9
		[DataSourceProperty]
		public string SelectHeroText
		{
			get
			{
				return this._selectHeroText;
			}
			set
			{
				if (value != this._selectHeroText)
				{
					this._selectHeroText = value;
					base.OnPropertyChangedWithValue<string>(value, "SelectHeroText");
				}
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x0600168F RID: 5775 RVA: 0x00057EEC File Offset: 0x000560EC
		[DataSourceProperty]
		public MBBindingList<CraftingAvailableHeroItemVM> CraftingHeroes
		{
			get
			{
				return this.GetCraftingHeroes();
			}
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x00057EF9 File Offset: 0x000560F9
		public void SetExitInputKey(HotKey hotKey)
		{
			this.ExitInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06001691 RID: 5777 RVA: 0x00057F08 File Offset: 0x00056108
		// (set) Token: 0x06001692 RID: 5778 RVA: 0x00057F10 File Offset: 0x00056110
		[DataSourceProperty]
		public InputKeyItemVM ExitInputKey
		{
			get
			{
				return this._exitInputKey;
			}
			set
			{
				if (value != this._exitInputKey)
				{
					this._exitInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "ExitInputKey");
				}
			}
		}

		// Token: 0x04000A4E RID: 2638
		private readonly Func<MBBindingList<CraftingAvailableHeroItemVM>> GetCraftingHeroes;

		// Token: 0x04000A4F RID: 2639
		private bool _isVisible;

		// Token: 0x04000A50 RID: 2640
		private string _selectHeroText;

		// Token: 0x04000A51 RID: 2641
		private InputKeyItemVM _exitInputKey;
	}
}
