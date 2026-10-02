using System;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.HUD
{
	// Token: 0x02000054 RID: 84
	public class MissionMainAgentControllerEquipDropVM : ViewModel
	{
		// Token: 0x060006E7 RID: 1767 RVA: 0x00019250 File Offset: 0x00017450
		public MissionMainAgentControllerEquipDropVM(Action<EquipmentIndex> toggleItem)
		{
			this._toggleItem = toggleItem;
			this.EquippedWeapons = new MBBindingList<ControllerEquippedItemVM>();
			this.RefreshValues();
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00019281 File Offset: 0x00017481
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.PressToEquipText = new TextObject("{=HEEZhL90}Press to Equip", null).ToString();
			this.HoldToDropText = this._dropTextObject.ToString();
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x000192B0 File Offset: 0x000174B0
		private bool IsMainAgentAvailable()
		{
			Agent main = Agent.Main;
			return main != null && main.IsActive() && !Agent.Main.IsUsingGameObject && !Agent.Main.IsInWater();
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x000192E0 File Offset: 0x000174E0
		public void InitializeMainAgentPropterties()
		{
			Mission.Current.OnMainAgentChanged += this.OnMainAgentChanged;
			this.OnMainAgentChanged(null);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00019300 File Offset: 0x00017500
		private void OnMainAgentChanged(Agent oldAgent)
		{
			if (oldAgent != null)
			{
				oldAgent.OnMainAgentWieldedItemChange = (Agent.OnMainAgentWieldedItemChangeDelegate)Delegate.Remove(oldAgent.OnMainAgentWieldedItemChange, new Agent.OnMainAgentWieldedItemChangeDelegate(this.OnMainAgentWeaponChange));
			}
			if (Agent.Main != null)
			{
				Agent main = Agent.Main;
				main.OnMainAgentWieldedItemChange = (Agent.OnMainAgentWieldedItemChangeDelegate)Delegate.Combine(main.OnMainAgentWieldedItemChange, new Agent.OnMainAgentWieldedItemChangeDelegate(this.OnMainAgentWeaponChange));
			}
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0001935F File Offset: 0x0001755F
		private void OnMainAgentWeaponChange()
		{
			this.UpdateItemsWieldStatus();
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00019368 File Offset: 0x00017568
		public void OnToggle(bool isEnabled)
		{
			this.EquippedWeapons.ApplyActionOnAllItems(delegate(ControllerEquippedItemVM o)
			{
				o.OnFinalize();
			});
			this.EquippedWeapons.Clear();
			this.EquippedExtraWeapon = null;
			this.HaveExtraWeapon = false;
			if (isEnabled)
			{
				this.PressToEquipText = (this.IsMainAgentAvailable() ? new TextObject("{=HEEZhL90}Press to Equip", null).ToString() : string.Empty);
				this.EquippedWeapons.Add(new ControllerEquippedItemVM(GameTexts.FindText("str_cancel", null).ToString(), null, "None", null, new Action<EquipmentActionItemVM>(this.OnItemSelected)));
				int num = 0;
				int totalNumberOfWeaponsOnMainAgent = this.GetTotalNumberOfWeaponsOnMainAgent();
				for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.ExtraWeaponSlot; equipmentIndex++)
				{
					MissionWeapon missionWeapon = Agent.Main.Equipment[equipmentIndex];
					if (!missionWeapon.IsEmpty)
					{
						string itemTypeAsString = MissionMainAgentEquipmentControllerVM.GetItemTypeAsString(missionWeapon.Item);
						string weaponName = this.GetWeaponName(missionWeapon);
						this.EquippedWeapons.Add(new ControllerEquippedItemVM(weaponName, itemTypeAsString, equipmentIndex, MissionMainAgentControllerEquipDropVM.GetWeaponHotKey(num, totalNumberOfWeaponsOnMainAgent), new Action<EquipmentActionItemVM>(this.OnItemSelected)));
						num++;
					}
				}
				MissionWeapon missionWeapon2 = Agent.Main.Equipment[EquipmentIndex.ExtraWeaponSlot];
				this.HaveExtraWeapon = !missionWeapon2.IsEmpty;
				if (this.HaveExtraWeapon)
				{
					string itemTypeAsString2 = MissionMainAgentEquipmentControllerVM.GetItemTypeAsString(missionWeapon2.Item);
					string weaponName2 = this.GetWeaponName(missionWeapon2);
					this.EquippedExtraWeapon = new ControllerEquippedItemVM(weaponName2, itemTypeAsString2, EquipmentIndex.ExtraWeaponSlot, MissionMainAgentControllerEquipDropVM.GetWeaponHotKey(4, totalNumberOfWeaponsOnMainAgent), new Action<EquipmentActionItemVM>(this.OnItemSelected));
					num++;
				}
				this.UpdateItemsWieldStatus();
			}
			else
			{
				if (this._lastSelectedItem != null && this._lastSelectedItem.Identifier is EquipmentIndex)
				{
					Action<EquipmentIndex> toggleItem = this._toggleItem;
					if (toggleItem != null)
					{
						toggleItem((EquipmentIndex)this._lastSelectedItem.Identifier);
					}
				}
				this._lastSelectedItem = null;
			}
			this.IsActive = isEnabled;
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0001954C File Offset: 0x0001774C
		private void OnItemSelected(EquipmentActionItemVM selectedItem)
		{
			if (this._lastSelectedItem != selectedItem)
			{
				this._lastSelectedItem = selectedItem;
			}
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0001955E File Offset: 0x0001775E
		public void OnCancelHoldController()
		{
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00019560 File Offset: 0x00017760
		public void OnWeaponDroppedAtIndex(int droppedWeaponIndex)
		{
			this.OnToggle(true);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00019569 File Offset: 0x00017769
		private bool IsWieldedWeaponAtIndex(EquipmentIndex index)
		{
			return index == Agent.Main.GetPrimaryWieldedItemIndex() || index == Agent.Main.GetOffhandWieldedItemIndex();
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00019587 File Offset: 0x00017787
		public void OnWeaponEquippedAtIndex(int equippedWeaponIndex)
		{
			this.UpdateItemsWieldStatus();
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00019590 File Offset: 0x00017790
		public void SetDropProgressForIndex(EquipmentIndex eqIndex, float progress)
		{
			int i = 0;
			while (i < this.EquippedWeapons.Count)
			{
				object obj;
				if (!((obj = this.EquippedWeapons[i].Identifier) is EquipmentIndex))
				{
					goto IL_0031;
				}
				EquipmentIndex equipmentIndex = (EquipmentIndex)obj;
				if (equipmentIndex != eqIndex || progress <= 0.2f)
				{
					goto IL_0031;
				}
				float num = progress;
				IL_0039:
				float num2 = num;
				this.EquippedWeapons[i].DropProgress = num2;
				i++;
				continue;
				IL_0031:
				num = 0f;
				goto IL_0039;
			}
			if (this.HaveExtraWeapon)
			{
				object obj;
				float num3;
				if ((obj = this.EquippedExtraWeapon.Identifier) is EquipmentIndex)
				{
					EquipmentIndex equipmentIndex2 = (EquipmentIndex)obj;
					if (equipmentIndex2 == eqIndex && progress > 0.2f)
					{
						num3 = progress;
						goto IL_0097;
					}
				}
				num3 = 0f;
				IL_0097:
				float num4 = num3;
				this.EquippedExtraWeapon.DropProgress = num4;
			}
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00019644 File Offset: 0x00017844
		private void UpdateItemsWieldStatus()
		{
			for (int i = 0; i < this.EquippedWeapons.Count; i++)
			{
				object identifier;
				if ((identifier = this.EquippedWeapons[i].Identifier) is EquipmentIndex)
				{
					EquipmentIndex equipmentIndex = (EquipmentIndex)identifier;
					this.EquippedWeapons[i].IsWielded = this.IsWieldedWeaponAtIndex(equipmentIndex);
				}
			}
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x000196A0 File Offset: 0x000178A0
		private string GetWeaponName(MissionWeapon weapon)
		{
			string text = weapon.Item.Name.ToString();
			WeaponComponentData currentUsageItem = weapon.CurrentUsageItem;
			if (currentUsageItem != null && currentUsageItem.IsShield)
			{
				text = string.Concat(new object[] { text, " (", weapon.HitPoints, " / ", weapon.ModifiedMaxHitPoints, ")" });
			}
			else
			{
				WeaponComponentData currentUsageItem2 = weapon.CurrentUsageItem;
				if (currentUsageItem2 != null && currentUsageItem2.IsConsumable && weapon.ModifiedMaxAmount > 1)
				{
					text = string.Concat(new object[] { text, " (", weapon.Amount, " / ", weapon.ModifiedMaxAmount, ")" });
				}
			}
			return text;
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00019784 File Offset: 0x00017984
		public override void OnFinalize()
		{
			base.OnFinalize();
			if (Agent.Main != null)
			{
				Agent main = Agent.Main;
				main.OnMainAgentWieldedItemChange = (Agent.OnMainAgentWieldedItemChangeDelegate)Delegate.Remove(main.OnMainAgentWieldedItemChange, new Agent.OnMainAgentWieldedItemChangeDelegate(this.OnMainAgentWeaponChange));
			}
			Mission.Current.OnMainAgentChanged -= this.OnMainAgentChanged;
			this.EquippedWeapons.ApplyActionOnAllItems(delegate(ControllerEquippedItemVM o)
			{
				o.OnFinalize();
			});
			this.EquippedWeapons.Clear();
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00019810 File Offset: 0x00017A10
		private static HotKey GetWeaponHotKey(int currentIndexOfWeapon, int totalNumOfWeapons)
		{
			if (currentIndexOfWeapon == 0)
			{
				if (totalNumOfWeapons == 1)
				{
					return HotKeyManager.GetCategory("CombatHotKeyCategory").GetHotKey("ControllerEquipDropWeapon4");
				}
				if (totalNumOfWeapons > 1)
				{
					return HotKeyManager.GetCategory("CombatHotKeyCategory").GetHotKey("ControllerEquipDropWeapon1");
				}
				Debug.FailedAssert("Wrong number of total weapons!", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.ViewModelCollection\\HUD\\MissionMainAgentControllerEquipDropVM.cs", "GetWeaponHotKey", 222);
			}
			else if (currentIndexOfWeapon == 1)
			{
				if (totalNumOfWeapons == 2)
				{
					return HotKeyManager.GetCategory("CombatHotKeyCategory").GetHotKey("ControllerEquipDropWeapon3");
				}
				if (totalNumOfWeapons > 2)
				{
					return HotKeyManager.GetCategory("CombatHotKeyCategory").GetHotKey("ControllerEquipDropWeapon4");
				}
			}
			else
			{
				if (currentIndexOfWeapon == 2)
				{
					return HotKeyManager.GetCategory("CombatHotKeyCategory").GetHotKey("ControllerEquipDropWeapon3");
				}
				if (currentIndexOfWeapon == 3)
				{
					return HotKeyManager.GetCategory("CombatHotKeyCategory").GetHotKey("ControllerEquipDropWeapon2");
				}
				if (currentIndexOfWeapon == 4)
				{
					return HotKeyManager.GetCategory("CombatHotKeyCategory").GetHotKey("ControllerEquipDropExtraWeapon");
				}
				Debug.FailedAssert("Wrong index of current weapon. Cannot be higher than 3", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.ViewModelCollection\\HUD\\MissionMainAgentControllerEquipDropVM.cs", "GetWeaponHotKey", 250);
			}
			return null;
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0001990B File Offset: 0x00017B0B
		public void OnGamepadActiveChanged(bool isActive)
		{
			this.HoldToDropText = (isActive ? this._dropTextObject.ToString() : string.Empty);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00019928 File Offset: 0x00017B28
		private int GetTotalNumberOfWeaponsOnMainAgent()
		{
			int num = 0;
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.ExtraWeaponSlot; equipmentIndex++)
			{
				if (!Agent.Main.Equipment[equipmentIndex].IsEmpty)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x00019962 File Offset: 0x00017B62
		// (set) Token: 0x060006FB RID: 1787 RVA: 0x0001996A File Offset: 0x00017B6A
		[DataSourceProperty]
		public MBBindingList<ControllerEquippedItemVM> EquippedWeapons
		{
			get
			{
				return this._equipActions;
			}
			set
			{
				if (value != this._equipActions)
				{
					this._equipActions = value;
					base.OnPropertyChangedWithValue<MBBindingList<ControllerEquippedItemVM>>(value, "EquippedWeapons");
				}
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x00019988 File Offset: 0x00017B88
		// (set) Token: 0x060006FD RID: 1789 RVA: 0x00019990 File Offset: 0x00017B90
		[DataSourceProperty]
		public ControllerEquippedItemVM EquippedExtraWeapon
		{
			get
			{
				return this._equippedExtraWeapon;
			}
			set
			{
				if (value != this._equippedExtraWeapon)
				{
					this._equippedExtraWeapon = value;
					base.OnPropertyChangedWithValue<ControllerEquippedItemVM>(value, "EquippedExtraWeapon");
				}
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x000199AE File Offset: 0x00017BAE
		// (set) Token: 0x060006FF RID: 1791 RVA: 0x000199B6 File Offset: 0x00017BB6
		[DataSourceProperty]
		public string HoldToDropText
		{
			get
			{
				return this._holdToDropText;
			}
			set
			{
				if (value != this._holdToDropText)
				{
					this._holdToDropText = value;
					base.OnPropertyChangedWithValue<string>(value, "HoldToDropText");
				}
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x000199D9 File Offset: 0x00017BD9
		// (set) Token: 0x06000701 RID: 1793 RVA: 0x000199E1 File Offset: 0x00017BE1
		[DataSourceProperty]
		public string PressToEquipText
		{
			get
			{
				return this._pressToEquipText;
			}
			set
			{
				if (value != this._pressToEquipText)
				{
					this._pressToEquipText = value;
					base.OnPropertyChangedWithValue<string>(value, "PressToEquipText");
				}
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x00019A04 File Offset: 0x00017C04
		// (set) Token: 0x06000703 RID: 1795 RVA: 0x00019A0C File Offset: 0x00017C0C
		[DataSourceProperty]
		public bool IsActive
		{
			get
			{
				return this._isActive;
			}
			set
			{
				if (value != this._isActive)
				{
					this._isActive = value;
					base.OnPropertyChangedWithValue(value, "IsActive");
				}
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x00019A2A File Offset: 0x00017C2A
		// (set) Token: 0x06000705 RID: 1797 RVA: 0x00019A32 File Offset: 0x00017C32
		[DataSourceProperty]
		public bool HaveExtraWeapon
		{
			get
			{
				return this._haveExtraWeapon;
			}
			set
			{
				if (value != this._haveExtraWeapon)
				{
					this._haveExtraWeapon = value;
					base.OnPropertyChangedWithValue(value, "HaveExtraWeapon");
				}
			}
		}

		// Token: 0x04000315 RID: 789
		private EquipmentActionItemVM _lastSelectedItem;

		// Token: 0x04000316 RID: 790
		private Action<EquipmentIndex> _toggleItem;

		// Token: 0x04000317 RID: 791
		private TextObject _dropTextObject = new TextObject("{=d1tCz15N}Hold to Drop", null);

		// Token: 0x04000318 RID: 792
		private MBBindingList<ControllerEquippedItemVM> _equipActions;

		// Token: 0x04000319 RID: 793
		private ControllerEquippedItemVM _equippedExtraWeapon;

		// Token: 0x0400031A RID: 794
		private bool _isActive;

		// Token: 0x0400031B RID: 795
		private bool _haveExtraWeapon;

		// Token: 0x0400031C RID: 796
		private string _holdToDropText;

		// Token: 0x0400031D RID: 797
		private string _pressToEquipText;
	}
}
