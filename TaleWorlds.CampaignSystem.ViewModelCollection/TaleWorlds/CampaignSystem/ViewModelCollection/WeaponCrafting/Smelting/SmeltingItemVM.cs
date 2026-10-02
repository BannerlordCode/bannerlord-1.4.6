using System;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.ImageIdentifiers;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.Smelting
{
	// Token: 0x02000111 RID: 273
	public class SmeltingItemVM : ViewModel
	{
		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x0600190B RID: 6411 RVA: 0x0005FA8A File Offset: 0x0005DC8A
		// (set) Token: 0x0600190A RID: 6410 RVA: 0x0005FA81 File Offset: 0x0005DC81
		public EquipmentElement EquipmentElement { get; private set; }

		// Token: 0x0600190C RID: 6412 RVA: 0x0005FA94 File Offset: 0x0005DC94
		public SmeltingItemVM(EquipmentElement equipmentElement, Action<SmeltingItemVM> onSelection, Action<SmeltingItemVM, bool> onItemLockedStateChange, bool isLocked, int numOfItems)
		{
			this._onSelection = onSelection;
			this._onItemLockedStateChange = onItemLockedStateChange;
			this.EquipmentElement = equipmentElement;
			this.Yield = new MBBindingList<CraftingResourceItemVM>();
			this.InputMaterials = new MBBindingList<CraftingResourceItemVM>();
			this.LockHint = new HintViewModel(GameTexts.FindText("str_lock_in_inventory", null).SetTextVariable("TRANSFERABLE", GameTexts.FindText("str_items", null).ToString()), null);
			int[] smeltingOutputForItem = Campaign.Current.Models.SmithingModel.GetSmeltingOutputForItem(equipmentElement.Item);
			for (int i = 0; i < smeltingOutputForItem.Length; i++)
			{
				if (smeltingOutputForItem[i] > 0)
				{
					this.Yield.Add(new CraftingResourceItemVM((CraftingMaterials)i, smeltingOutputForItem[i], 0));
				}
				else if (smeltingOutputForItem[i] < 0)
				{
					this.InputMaterials.Add(new CraftingResourceItemVM((CraftingMaterials)i, -smeltingOutputForItem[i], 0));
				}
			}
			this.IsLocked = isLocked;
			this.Visual = new ItemImageIdentifierVM(equipmentElement.Item, "");
			this.NumOfItems = numOfItems;
			this.HasMoreThanOneItem = this.NumOfItems > 1;
			this.RefreshValues();
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x0005FBA4 File Offset: 0x0005DDA4
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Name = this.EquipmentElement.Item.Name.ToString();
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x0005FBD5 File Offset: 0x0005DDD5
		public void ExecuteSelection()
		{
			this._onSelection(this);
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x0005FBE3 File Offset: 0x0005DDE3
		public void ExecuteShowItemTooltip()
		{
			InformationManager.ShowTooltip(typeof(ItemObject), new object[] { this.EquipmentElement });
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x0005FC08 File Offset: 0x0005DE08
		public void ExecuteHideItemTooltip()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06001911 RID: 6417 RVA: 0x0005FC0F File Offset: 0x0005DE0F
		// (set) Token: 0x06001912 RID: 6418 RVA: 0x0005FC17 File Offset: 0x0005DE17
		[DataSourceProperty]
		public ItemImageIdentifierVM Visual
		{
			get
			{
				return this._visual;
			}
			set
			{
				if (value != this._visual)
				{
					this._visual = value;
					base.OnPropertyChangedWithValue<ItemImageIdentifierVM>(value, "Visual");
				}
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06001913 RID: 6419 RVA: 0x0005FC35 File Offset: 0x0005DE35
		// (set) Token: 0x06001914 RID: 6420 RVA: 0x0005FC3D File Offset: 0x0005DE3D
		[DataSourceProperty]
		public MBBindingList<CraftingResourceItemVM> Yield
		{
			get
			{
				return this._yield;
			}
			set
			{
				if (value != this._yield)
				{
					this._yield = value;
					base.OnPropertyChangedWithValue<MBBindingList<CraftingResourceItemVM>>(value, "Yield");
				}
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06001915 RID: 6421 RVA: 0x0005FC5B File Offset: 0x0005DE5B
		// (set) Token: 0x06001916 RID: 6422 RVA: 0x0005FC63 File Offset: 0x0005DE63
		[DataSourceProperty]
		public MBBindingList<CraftingResourceItemVM> InputMaterials
		{
			get
			{
				return this._inputMaterials;
			}
			set
			{
				if (value != this._inputMaterials)
				{
					this._inputMaterials = value;
					base.OnPropertyChangedWithValue<MBBindingList<CraftingResourceItemVM>>(value, "InputMaterials");
				}
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06001917 RID: 6423 RVA: 0x0005FC81 File Offset: 0x0005DE81
		// (set) Token: 0x06001918 RID: 6424 RVA: 0x0005FC89 File Offset: 0x0005DE89
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

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06001919 RID: 6425 RVA: 0x0005FCAC File Offset: 0x0005DEAC
		// (set) Token: 0x0600191A RID: 6426 RVA: 0x0005FCB4 File Offset: 0x0005DEB4
		[DataSourceProperty]
		public int NumOfItems
		{
			get
			{
				return this._numOfItems;
			}
			set
			{
				if (value != this._numOfItems)
				{
					this._numOfItems = value;
					base.OnPropertyChangedWithValue(value, "NumOfItems");
				}
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x0600191B RID: 6427 RVA: 0x0005FCD2 File Offset: 0x0005DED2
		// (set) Token: 0x0600191C RID: 6428 RVA: 0x0005FCDA File Offset: 0x0005DEDA
		[DataSourceProperty]
		public bool HasMoreThanOneItem
		{
			get
			{
				return this._hasMoreThanOneItem;
			}
			set
			{
				if (value != this._hasMoreThanOneItem)
				{
					this._hasMoreThanOneItem = value;
					base.OnPropertyChangedWithValue(value, "HasMoreThanOneItem");
				}
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x0600191D RID: 6429 RVA: 0x0005FCF8 File Offset: 0x0005DEF8
		// (set) Token: 0x0600191E RID: 6430 RVA: 0x0005FD00 File Offset: 0x0005DF00
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

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x0600191F RID: 6431 RVA: 0x0005FD1E File Offset: 0x0005DF1E
		// (set) Token: 0x06001920 RID: 6432 RVA: 0x0005FD26 File Offset: 0x0005DF26
		[DataSourceProperty]
		public HintViewModel LockHint
		{
			get
			{
				return this._lockHint;
			}
			set
			{
				if (value != this._lockHint)
				{
					this._lockHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "LockHint");
				}
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06001921 RID: 6433 RVA: 0x0005FD44 File Offset: 0x0005DF44
		// (set) Token: 0x06001922 RID: 6434 RVA: 0x0005FD4C File Offset: 0x0005DF4C
		[DataSourceProperty]
		public bool IsLocked
		{
			get
			{
				return this._isLocked;
			}
			set
			{
				if (value != this._isLocked)
				{
					this._isLocked = value;
					base.OnPropertyChangedWithValue(value, "IsLocked");
					this._onItemLockedStateChange(this, value);
				}
			}
		}

		// Token: 0x04000B82 RID: 2946
		private readonly Action<SmeltingItemVM> _onSelection;

		// Token: 0x04000B83 RID: 2947
		private readonly Action<SmeltingItemVM, bool> _onItemLockedStateChange;

		// Token: 0x04000B84 RID: 2948
		private ItemImageIdentifierVM _visual;

		// Token: 0x04000B85 RID: 2949
		private string _name;

		// Token: 0x04000B86 RID: 2950
		private int _numOfItems;

		// Token: 0x04000B87 RID: 2951
		private MBBindingList<CraftingResourceItemVM> _inputMaterials;

		// Token: 0x04000B88 RID: 2952
		private MBBindingList<CraftingResourceItemVM> _yield;

		// Token: 0x04000B89 RID: 2953
		private HintViewModel _lockHint;

		// Token: 0x04000B8A RID: 2954
		private bool _isSelected;

		// Token: 0x04000B8B RID: 2955
		private bool _isLocked;

		// Token: 0x04000B8C RID: 2956
		private bool _hasMoreThanOneItem;
	}
}
