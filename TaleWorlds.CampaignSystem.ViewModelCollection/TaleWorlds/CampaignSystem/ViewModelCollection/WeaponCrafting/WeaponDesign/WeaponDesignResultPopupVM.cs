using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.CraftingSystem;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.CampaignSystem.ViewModelCollection.Inventory;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Core.ViewModelCollection.Selector;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.WeaponDesign
{
	// Token: 0x0200010A RID: 266
	public class WeaponDesignResultPopupVM : ViewModel
	{
		// Token: 0x060017C0 RID: 6080 RVA: 0x0005AFD0 File Offset: 0x000591D0
		public WeaponDesignResultPopupVM(ItemObject craftedItem, TextObject itemName, Action onFinalize, Crafting crafting, CraftingOrder completedOrder, ItemCollectionElementViewModel itemVisualModel, MBBindingList<ItemFlagVM> weaponFlagIconsList, Func<CraftingSecondaryUsageItemVM, MBBindingList<WeaponDesignResultPropertyItemVM>> onGetPropertyList, Action<CraftingSecondaryUsageItemVM> onUsageSelected)
		{
			this._craftedItem = craftedItem;
			this._onFinalize = onFinalize;
			this._crafting = crafting;
			this._completedOrder = completedOrder;
			this._craftingBehavior = Campaign.Current.GetCampaignBehavior<ICraftingCampaignBehavior>();
			this._onUsageSelected = onUsageSelected;
			this.SecondaryUsageSelector = new SelectorVM<CraftingSecondaryUsageItemVM>(new List<string>(), -1, new Action<SelectorVM<CraftingSecondaryUsageItemVM>>(this.OnUsageSelected));
			this.WeaponFlagIconsList = weaponFlagIconsList;
			this._onGetPropertyList = onGetPropertyList;
			ItemModifier currentItemModifier = this._craftingBehavior.GetCurrentItemModifier();
			if (currentItemModifier != null)
			{
				TextObject textObject = currentItemModifier.Name.CopyTextObject();
				textObject.SetTextVariable("ITEMNAME", itemName.ToString());
				this.ItemName = textObject.ToString();
			}
			else
			{
				this.ItemName = itemName.ToString();
			}
			this.ItemName = this.ItemName.Trim();
			this.ItemVisualModel = itemVisualModel;
			Game game = Game.Current;
			if (game != null)
			{
				game.EventManager.TriggerEvent<CraftingWeaponResultPopupToggledEvent>(new CraftingWeaponResultPopupToggledEvent(true));
			}
			this.RefreshValues();
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x0005B0C8 File Offset: 0x000592C8
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.IsInOrderMode = this._completedOrder != null;
			this.WeaponCraftedText = new TextObject("{=0mqdFC2x}Weapon Crafted!", null).ToString();
			this.DoneLbl = GameTexts.FindText("str_done", null).ToString();
			this.RefreshUsages();
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x0005B11C File Offset: 0x0005931C
		public override void OnFinalize()
		{
			base.OnFinalize();
			this.DoneInputKey.OnFinalize();
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x0005B130 File Offset: 0x00059330
		private void RefreshUsages()
		{
			this.SecondaryUsageSelector.ItemList.Clear();
			MBReadOnlyList<WeaponComponentData> weapons = this._crafting.GetCurrentCraftedItemObject(false, null).Weapons;
			int num = this.SecondaryUsageSelector.SelectedIndex;
			int num2 = 0;
			for (int i = 0; i < weapons.Count; i++)
			{
				if (CampaignUIHelper.IsItemUsageApplicable(weapons[i]))
				{
					TextObject textObject = GameTexts.FindText("str_weapon_usage", weapons[i].WeaponDescriptionId);
					this.SecondaryUsageSelector.AddItem(new CraftingSecondaryUsageItemVM(textObject, num2, i, this.SecondaryUsageSelector));
					if (this.IsInOrderMode)
					{
						WeaponComponentData orderWeapon = this._completedOrder.GetStatWeapon();
						num = this._crafting.GetCurrentCraftedItemObject(false, null).Weapons.FindIndex((WeaponComponentData x) => x.WeaponDescriptionId == orderWeapon.WeaponDescriptionId);
					}
					else
					{
						CraftingOrder completedOrder = this._completedOrder;
						if (((completedOrder != null) ? completedOrder.GetStatWeapon().WeaponDescriptionId : null) == weapons[i].WeaponDescriptionId)
						{
							num = num2;
						}
					}
					num2++;
				}
			}
			this.SecondaryUsageSelector.SelectedIndex = ((num >= 0) ? num : 0);
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x0005B254 File Offset: 0x00059454
		private void OnUsageSelected(SelectorVM<CraftingSecondaryUsageItemVM> selector)
		{
			Func<CraftingSecondaryUsageItemVM, MBBindingList<WeaponDesignResultPropertyItemVM>> onGetPropertyList = this._onGetPropertyList;
			this.DesignResultPropertyList = ((onGetPropertyList != null) ? onGetPropertyList(selector.SelectedItem) : null);
			if (this._isInOrderMode)
			{
				bool flag;
				TextObject textObject;
				TextObject textObject2;
				int num;
				this._craftingBehavior.GetOrderResult(this._completedOrder, this._craftedItem, out flag, out textObject, out textObject2, out num);
				this.CraftedWeaponInitialWorth = this._completedOrder.BaseGoldReward;
				this.CraftedWeaponFinalWorth = num;
				this.IsOrderSuccessful = flag;
				this.CraftedWeaponWorthText = new TextObject("{=ZIn8W5ZG}Worth", null).ToString();
				this.DesignResultPropertyList.Add(new WeaponDesignResultPropertyItemVM(new TextObject("{=QmfZjCo1}Worth: ", null), (float)this.CraftedWeaponInitialWorth, (float)this.CraftedWeaponInitialWorth, (float)(this.CraftedWeaponFinalWorth - this.CraftedWeaponInitialWorth), false, true, false));
				this.OrderOwnerRemarkText = textObject.ToString();
				this.OrderResultText = textObject2.ToString();
			}
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x0005B330 File Offset: 0x00059530
		private void UpdateConfirmAvailability()
		{
			if (this.IsInOrderMode)
			{
				this.CanConfirm = true;
				this.ConfirmDisabledReasonHint = new HintViewModel();
				return;
			}
			Tuple<bool, TextObject> tuple = CampaignUIHelper.IsStringApplicableForItemName(this.ItemName);
			this.CanConfirm = tuple.Item1;
			this.ConfirmDisabledReasonHint = new HintViewModel(tuple.Item2, null);
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x0005B384 File Offset: 0x00059584
		public void ExecuteFinalizeCrafting()
		{
			TextObject textObject = new TextObject("{=!}" + this.ItemName, null);
			this._crafting.SetCraftedWeaponName(textObject);
			this._craftingBehavior.SetCraftedWeaponName(this._craftedItem, textObject);
			Action onFinalize = this._onFinalize;
			if (onFinalize != null)
			{
				onFinalize();
			}
			Game game = Game.Current;
			if (game != null)
			{
				game.EventManager.TriggerEvent<CraftingWeaponResultPopupToggledEvent>(new CraftingWeaponResultPopupToggledEvent(false));
			}
			if (!this._isInOrderMode)
			{
				TextObject textObject2 = GameTexts.FindText("crafting_added_to_inventory", null);
				textObject2.SetCharacterProperties("PLAYER", Hero.MainHero.CharacterObject, false);
				textObject2.SetTextVariable("ITEM_NAME", this.ItemName);
				MBInformationManager.AddQuickInformation(textObject2, 0, null, null, "");
			}
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x0005B43A File Offset: 0x0005963A
		public void ExecuteRandomCraftName()
		{
			this.ItemName = this._crafting.GetRandomCraftName().ToString();
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x060017C8 RID: 6088 RVA: 0x0005B452 File Offset: 0x00059652
		// (set) Token: 0x060017C9 RID: 6089 RVA: 0x0005B45A File Offset: 0x0005965A
		[DataSourceProperty]
		public MBBindingList<ItemFlagVM> WeaponFlagIconsList
		{
			get
			{
				return this._weaponFlagIconsList;
			}
			set
			{
				if (value != this._weaponFlagIconsList)
				{
					this._weaponFlagIconsList = value;
					base.OnPropertyChangedWithValue<MBBindingList<ItemFlagVM>>(value, "WeaponFlagIconsList");
				}
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x060017CA RID: 6090 RVA: 0x0005B478 File Offset: 0x00059678
		// (set) Token: 0x060017CB RID: 6091 RVA: 0x0005B480 File Offset: 0x00059680
		[DataSourceProperty]
		public bool IsInOrderMode
		{
			get
			{
				return this._isInOrderMode;
			}
			set
			{
				if (value != this._isInOrderMode)
				{
					this._isInOrderMode = value;
					base.OnPropertyChangedWithValue(value, "IsInOrderMode");
				}
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x060017CC RID: 6092 RVA: 0x0005B49E File Offset: 0x0005969E
		// (set) Token: 0x060017CD RID: 6093 RVA: 0x0005B4A6 File Offset: 0x000596A6
		[DataSourceProperty]
		public int CraftedWeaponFinalWorth
		{
			get
			{
				return this._craftedWeaponFinalWorth;
			}
			set
			{
				if (value != this._craftedWeaponFinalWorth)
				{
					this._craftedWeaponFinalWorth = value;
					base.OnPropertyChangedWithValue(value, "CraftedWeaponFinalWorth");
				}
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x060017CE RID: 6094 RVA: 0x0005B4C4 File Offset: 0x000596C4
		// (set) Token: 0x060017CF RID: 6095 RVA: 0x0005B4CC File Offset: 0x000596CC
		[DataSourceProperty]
		public int CraftedWeaponPriceDifference
		{
			get
			{
				return this._craftedWeaponPriceDifference;
			}
			set
			{
				if (value != this._craftedWeaponPriceDifference)
				{
					this._craftedWeaponPriceDifference = value;
					base.OnPropertyChangedWithValue(value, "CraftedWeaponPriceDifference");
				}
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x060017D0 RID: 6096 RVA: 0x0005B4EA File Offset: 0x000596EA
		// (set) Token: 0x060017D1 RID: 6097 RVA: 0x0005B4F2 File Offset: 0x000596F2
		[DataSourceProperty]
		public int CraftedWeaponInitialWorth
		{
			get
			{
				return this._craftedWeaponInitialWorth;
			}
			set
			{
				if (value != this._craftedWeaponInitialWorth)
				{
					this._craftedWeaponInitialWorth = value;
					base.OnPropertyChangedWithValue(value, "CraftedWeaponInitialWorth");
				}
			}
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x060017D2 RID: 6098 RVA: 0x0005B510 File Offset: 0x00059710
		// (set) Token: 0x060017D3 RID: 6099 RVA: 0x0005B518 File Offset: 0x00059718
		[DataSourceProperty]
		public string CraftedWeaponWorthText
		{
			get
			{
				return this._craftedWeaponWorthText;
			}
			set
			{
				if (value != this._craftedWeaponWorthText)
				{
					this._craftedWeaponWorthText = value;
					base.OnPropertyChangedWithValue<string>(value, "CraftedWeaponWorthText");
				}
			}
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x060017D4 RID: 6100 RVA: 0x0005B53B File Offset: 0x0005973B
		// (set) Token: 0x060017D5 RID: 6101 RVA: 0x0005B543 File Offset: 0x00059743
		[DataSourceProperty]
		public bool IsOrderSuccessful
		{
			get
			{
				return this._isOrderSuccessful;
			}
			set
			{
				if (value != this._isOrderSuccessful)
				{
					this._isOrderSuccessful = value;
					base.OnPropertyChangedWithValue(value, "IsOrderSuccessful");
				}
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x060017D6 RID: 6102 RVA: 0x0005B561 File Offset: 0x00059761
		// (set) Token: 0x060017D7 RID: 6103 RVA: 0x0005B569 File Offset: 0x00059769
		[DataSourceProperty]
		public bool CanConfirm
		{
			get
			{
				return this._canConfirm;
			}
			set
			{
				if (value != this._canConfirm)
				{
					this._canConfirm = value;
					base.OnPropertyChangedWithValue(value, "CanConfirm");
				}
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x060017D8 RID: 6104 RVA: 0x0005B587 File Offset: 0x00059787
		// (set) Token: 0x060017D9 RID: 6105 RVA: 0x0005B58F File Offset: 0x0005978F
		[DataSourceProperty]
		public string OrderResultText
		{
			get
			{
				return this._orderResultText;
			}
			set
			{
				if (value != this._orderResultText)
				{
					this._orderResultText = value;
					base.OnPropertyChangedWithValue<string>(value, "OrderResultText");
				}
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x060017DA RID: 6106 RVA: 0x0005B5B2 File Offset: 0x000597B2
		// (set) Token: 0x060017DB RID: 6107 RVA: 0x0005B5BA File Offset: 0x000597BA
		[DataSourceProperty]
		public string OrderOwnerRemarkText
		{
			get
			{
				return this._orderOwnerRemarkText;
			}
			set
			{
				if (value != this._orderOwnerRemarkText)
				{
					this._orderOwnerRemarkText = value;
					base.OnPropertyChangedWithValue<string>(value, "OrderOwnerRemarkText");
				}
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x060017DC RID: 6108 RVA: 0x0005B5DD File Offset: 0x000597DD
		// (set) Token: 0x060017DD RID: 6109 RVA: 0x0005B5E5 File Offset: 0x000597E5
		[DataSourceProperty]
		public string WeaponCraftedText
		{
			get
			{
				return this._weaponCraftedText;
			}
			set
			{
				if (value != this._weaponCraftedText)
				{
					this._weaponCraftedText = value;
					base.OnPropertyChangedWithValue<string>(value, "WeaponCraftedText");
				}
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x060017DE RID: 6110 RVA: 0x0005B608 File Offset: 0x00059808
		// (set) Token: 0x060017DF RID: 6111 RVA: 0x0005B610 File Offset: 0x00059810
		[DataSourceProperty]
		public string DoneLbl
		{
			get
			{
				return this._doneLbl;
			}
			set
			{
				if (value != this._doneLbl)
				{
					this._doneLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "DoneLbl");
				}
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x060017E0 RID: 6112 RVA: 0x0005B633 File Offset: 0x00059833
		// (set) Token: 0x060017E1 RID: 6113 RVA: 0x0005B63B File Offset: 0x0005983B
		[DataSourceProperty]
		public MBBindingList<WeaponDesignResultPropertyItemVM> DesignResultPropertyList
		{
			get
			{
				return this._designResultPropertyList;
			}
			set
			{
				if (value != this._designResultPropertyList)
				{
					this._designResultPropertyList = value;
					base.OnPropertyChangedWithValue<MBBindingList<WeaponDesignResultPropertyItemVM>>(value, "DesignResultPropertyList");
				}
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x060017E2 RID: 6114 RVA: 0x0005B659 File Offset: 0x00059859
		// (set) Token: 0x060017E3 RID: 6115 RVA: 0x0005B661 File Offset: 0x00059861
		[DataSourceProperty]
		public string ItemName
		{
			get
			{
				return this._itemName;
			}
			set
			{
				if (value != this._itemName)
				{
					this._itemName = value;
					this.UpdateConfirmAvailability();
					base.OnPropertyChangedWithValue<string>(value, "ItemName");
				}
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x060017E4 RID: 6116 RVA: 0x0005B68A File Offset: 0x0005988A
		// (set) Token: 0x060017E5 RID: 6117 RVA: 0x0005B692 File Offset: 0x00059892
		[DataSourceProperty]
		public ItemCollectionElementViewModel ItemVisualModel
		{
			get
			{
				return this._itemVisualModel;
			}
			set
			{
				if (value != this._itemVisualModel)
				{
					this._itemVisualModel = value;
					base.OnPropertyChangedWithValue<ItemCollectionElementViewModel>(value, "ItemVisualModel");
				}
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x060017E6 RID: 6118 RVA: 0x0005B6B0 File Offset: 0x000598B0
		// (set) Token: 0x060017E7 RID: 6119 RVA: 0x0005B6B8 File Offset: 0x000598B8
		[DataSourceProperty]
		public HintViewModel ConfirmDisabledReasonHint
		{
			get
			{
				return this._confirmDisabledReasonHint;
			}
			set
			{
				if (value != this._confirmDisabledReasonHint)
				{
					this._confirmDisabledReasonHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ConfirmDisabledReasonHint");
				}
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x060017E8 RID: 6120 RVA: 0x0005B6D6 File Offset: 0x000598D6
		// (set) Token: 0x060017E9 RID: 6121 RVA: 0x0005B6DE File Offset: 0x000598DE
		[DataSourceProperty]
		public SelectorVM<CraftingSecondaryUsageItemVM> SecondaryUsageSelector
		{
			get
			{
				return this._secondaryUsageSelector;
			}
			set
			{
				if (value != this._secondaryUsageSelector)
				{
					this._secondaryUsageSelector = value;
					base.OnPropertyChangedWithValue<SelectorVM<CraftingSecondaryUsageItemVM>>(value, "SecondaryUsageSelector");
				}
			}
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x0005B6FC File Offset: 0x000598FC
		public void SetDoneInputKey(HotKey hotkey)
		{
			this.DoneInputKey = InputKeyItemVM.CreateFromHotKey(hotkey, true);
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x060017EB RID: 6123 RVA: 0x0005B70B File Offset: 0x0005990B
		// (set) Token: 0x060017EC RID: 6124 RVA: 0x0005B713 File Offset: 0x00059913
		[DataSourceProperty]
		public InputKeyItemVM DoneInputKey
		{
			get
			{
				return this._doneInputKey;
			}
			set
			{
				if (value != this._doneInputKey)
				{
					this._doneInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "DoneInputKey");
				}
			}
		}

		// Token: 0x04000AE0 RID: 2784
		private readonly Action<CraftingSecondaryUsageItemVM> _onUsageSelected;

		// Token: 0x04000AE1 RID: 2785
		private readonly Func<CraftingSecondaryUsageItemVM, MBBindingList<WeaponDesignResultPropertyItemVM>> _onGetPropertyList;

		// Token: 0x04000AE2 RID: 2786
		private readonly Action _onFinalize;

		// Token: 0x04000AE3 RID: 2787
		private readonly Crafting _crafting;

		// Token: 0x04000AE4 RID: 2788
		private readonly CraftingOrder _completedOrder;

		// Token: 0x04000AE5 RID: 2789
		private readonly ItemObject _craftedItem;

		// Token: 0x04000AE6 RID: 2790
		private readonly ICraftingCampaignBehavior _craftingBehavior;

		// Token: 0x04000AE7 RID: 2791
		private MBBindingList<ItemFlagVM> _weaponFlagIconsList;

		// Token: 0x04000AE8 RID: 2792
		private bool _isInOrderMode;

		// Token: 0x04000AE9 RID: 2793
		private string _orderResultText;

		// Token: 0x04000AEA RID: 2794
		private string _orderOwnerRemarkText;

		// Token: 0x04000AEB RID: 2795
		private bool _isOrderSuccessful;

		// Token: 0x04000AEC RID: 2796
		private bool _canConfirm;

		// Token: 0x04000AED RID: 2797
		private string _craftedWeaponWorthText;

		// Token: 0x04000AEE RID: 2798
		private int _craftedWeaponInitialWorth;

		// Token: 0x04000AEF RID: 2799
		private int _craftedWeaponPriceDifference;

		// Token: 0x04000AF0 RID: 2800
		private int _craftedWeaponFinalWorth;

		// Token: 0x04000AF1 RID: 2801
		private string _weaponCraftedText;

		// Token: 0x04000AF2 RID: 2802
		private string _doneLbl;

		// Token: 0x04000AF3 RID: 2803
		private MBBindingList<WeaponDesignResultPropertyItemVM> _designResultPropertyList;

		// Token: 0x04000AF4 RID: 2804
		private string _itemName;

		// Token: 0x04000AF5 RID: 2805
		private ItemCollectionElementViewModel _itemVisualModel;

		// Token: 0x04000AF6 RID: 2806
		private HintViewModel _confirmDisabledReasonHint;

		// Token: 0x04000AF7 RID: 2807
		private SelectorVM<CraftingSecondaryUsageItemVM> _secondaryUsageSelector;

		// Token: 0x04000AF8 RID: 2808
		private InputKeyItemVM _doneInputKey;
	}
}
