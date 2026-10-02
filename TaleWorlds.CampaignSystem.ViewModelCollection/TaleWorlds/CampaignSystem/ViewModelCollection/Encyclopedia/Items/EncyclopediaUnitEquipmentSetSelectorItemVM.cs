using System;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Core.ViewModelCollection.Selector;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items
{
	// Token: 0x020000EE RID: 238
	public class EncyclopediaUnitEquipmentSetSelectorItemVM : SelectorItemVM
	{
		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x060015DC RID: 5596 RVA: 0x00055C45 File Offset: 0x00053E45
		// (set) Token: 0x060015DD RID: 5597 RVA: 0x00055C4D File Offset: 0x00053E4D
		public Equipment EquipmentSet { get; private set; }

		// Token: 0x060015DE RID: 5598 RVA: 0x00055C56 File Offset: 0x00053E56
		public EncyclopediaUnitEquipmentSetSelectorItemVM(Equipment equipmentSet, string name = "")
			: base(name)
		{
			this.EquipmentSet = equipmentSet;
			this.LeftEquipmentList = new MBBindingList<CharacterEquipmentItemVM>();
			this.RightEquipmentList = new MBBindingList<CharacterEquipmentItemVM>();
			this.RefreshEquipment();
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x00055C84 File Offset: 0x00053E84
		private void RefreshEquipment()
		{
			this.LeftEquipmentList.Clear();
			this.LeftEquipmentList.Add(new CharacterEquipmentItemVM(this.EquipmentSet[EquipmentIndex.NumAllWeaponSlots].Item));
			this.LeftEquipmentList.Add(new CharacterEquipmentItemVM(this.EquipmentSet[EquipmentIndex.Cape].Item));
			this.LeftEquipmentList.Add(new CharacterEquipmentItemVM(this.EquipmentSet[EquipmentIndex.Body].Item));
			this.LeftEquipmentList.Add(new CharacterEquipmentItemVM(this.EquipmentSet[EquipmentIndex.Gloves].Item));
			this.LeftEquipmentList.Add(new CharacterEquipmentItemVM(this.EquipmentSet[EquipmentIndex.Leg].Item));
			this.LeftEquipmentList.Add(new CharacterEquipmentItemVM(this.EquipmentSet[EquipmentIndex.ArmorItemEndSlot].Item));
			this.RightEquipmentList.Clear();
			this.RightEquipmentList.Add(new CharacterEquipmentItemVM(this.EquipmentSet[EquipmentIndex.WeaponItemBeginSlot].Item));
			this.RightEquipmentList.Add(new CharacterEquipmentItemVM(this.EquipmentSet[EquipmentIndex.Weapon1].Item));
			this.RightEquipmentList.Add(new CharacterEquipmentItemVM(this.EquipmentSet[EquipmentIndex.Weapon2].Item));
			this.RightEquipmentList.Add(new CharacterEquipmentItemVM(this.EquipmentSet[EquipmentIndex.Weapon3].Item));
			this.RightEquipmentList.Add(new CharacterEquipmentItemVM(this.EquipmentSet[EquipmentIndex.ExtraWeaponSlot].Item));
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x060015E0 RID: 5600 RVA: 0x00055E35 File Offset: 0x00054035
		// (set) Token: 0x060015E1 RID: 5601 RVA: 0x00055E3D File Offset: 0x0005403D
		[DataSourceProperty]
		public MBBindingList<CharacterEquipmentItemVM> LeftEquipmentList
		{
			get
			{
				return this._leftEquipmentList;
			}
			set
			{
				if (value != this._leftEquipmentList)
				{
					this._leftEquipmentList = value;
					base.OnPropertyChangedWithValue<MBBindingList<CharacterEquipmentItemVM>>(value, "LeftEquipmentList");
				}
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x060015E2 RID: 5602 RVA: 0x00055E5B File Offset: 0x0005405B
		// (set) Token: 0x060015E3 RID: 5603 RVA: 0x00055E63 File Offset: 0x00054063
		[DataSourceProperty]
		public MBBindingList<CharacterEquipmentItemVM> RightEquipmentList
		{
			get
			{
				return this._rightEquipmentList;
			}
			set
			{
				if (value != this._rightEquipmentList)
				{
					this._rightEquipmentList = value;
					base.OnPropertyChangedWithValue<MBBindingList<CharacterEquipmentItemVM>>(value, "RightEquipmentList");
				}
			}
		}

		// Token: 0x040009F2 RID: 2546
		private MBBindingList<CharacterEquipmentItemVM> _leftEquipmentList;

		// Token: 0x040009F3 RID: 2547
		private MBBindingList<CharacterEquipmentItemVM> _rightEquipmentList;
	}
}
