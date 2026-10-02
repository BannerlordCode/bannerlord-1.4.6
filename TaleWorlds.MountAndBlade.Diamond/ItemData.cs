using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200011F RID: 287
	public class ItemData
	{
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x000081B4 File Offset: 0x000063B4
		// (set) Token: 0x06000667 RID: 1639 RVA: 0x000081BC File Offset: 0x000063BC
		public string TypeId { get; set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x000081C5 File Offset: 0x000063C5
		// (set) Token: 0x06000669 RID: 1641 RVA: 0x000081CD File Offset: 0x000063CD
		public string ModifierId { get; set; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x000081D6 File Offset: 0x000063D6
		// (set) Token: 0x0600066B RID: 1643 RVA: 0x000081DE File Offset: 0x000063DE
		public int? Index { get; set; }

		// Token: 0x0600066C RID: 1644 RVA: 0x000081E7 File Offset: 0x000063E7
		public void CopyItemData(ItemData itemdata)
		{
			this.TypeId = itemdata.TypeId;
			this.ModifierId = itemdata.ModifierId;
			this.Index = itemdata.Index;
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x0000820D File Offset: 0x0000640D
		private ItemType ItemType
		{
			get
			{
				return ItemList.GetItemTypeOf(this.TypeId);
			}
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0000821C File Offset: 0x0000641C
		private static int GetInventoryItemTypeOfItem(ItemType itemType)
		{
			switch (itemType)
			{
			case ItemType.Horse:
				return 64;
			case ItemType.OneHandedWeapon:
				return 1;
			case ItemType.TwoHandedWeapon:
				return 1;
			case ItemType.Polearm:
				return 1;
			case ItemType.Arrows:
				return 1;
			case ItemType.Bolts:
				return 1;
			case ItemType.Shield:
				return 2;
			case ItemType.Bow:
				return 1;
			case ItemType.Crossbow:
				return 1;
			case ItemType.Thrown:
				return 1;
			case ItemType.Goods:
				return 256;
			case ItemType.HeadArmor:
				return 4;
			case ItemType.BodyArmor:
				return 8;
			case ItemType.LegArmor:
				return 16;
			case ItemType.HandArmor:
				return 32;
			case ItemType.Pistol:
				return 1;
			case ItemType.Musket:
				return 1;
			case ItemType.Bullets:
				return 1;
			case ItemType.Animal:
				return 1024;
			case ItemType.Book:
				return 512;
			case ItemType.Cape:
				return 2048;
			case ItemType.HorseHarness:
				return 128;
			}
			return 0;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x000082D3 File Offset: 0x000064D3
		public bool CanItemToEquipmentDragPossible(int equipmentIndex)
		{
			return ItemData.CanItemToEquipmentDragPossible(this.TypeId, equipmentIndex);
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x000082E4 File Offset: 0x000064E4
		public static bool CanItemToEquipmentDragPossible(string itemTypeId, int equipmentIndex)
		{
			InventoryItemType inventoryItemTypeOfItem = (InventoryItemType)ItemData.GetInventoryItemTypeOfItem(ItemList.GetItemTypeOf(itemTypeId));
			bool flag = false;
			if (equipmentIndex == 0 || equipmentIndex == 1 || equipmentIndex == 2 || equipmentIndex == 3)
			{
				flag = inventoryItemTypeOfItem == InventoryItemType.Weapon || inventoryItemTypeOfItem == InventoryItemType.Shield;
			}
			else if (equipmentIndex == 5)
			{
				flag = inventoryItemTypeOfItem == InventoryItemType.HeadArmor;
			}
			else if (equipmentIndex == 6)
			{
				flag = inventoryItemTypeOfItem == InventoryItemType.BodyArmor;
			}
			else if (equipmentIndex == 7)
			{
				flag = inventoryItemTypeOfItem == InventoryItemType.LegArmor;
			}
			else if (equipmentIndex == 8)
			{
				flag = inventoryItemTypeOfItem == InventoryItemType.HandArmor;
			}
			else if (equipmentIndex == 9)
			{
				flag = inventoryItemTypeOfItem == InventoryItemType.Cape;
			}
			else if (equipmentIndex == 10)
			{
				flag = inventoryItemTypeOfItem == InventoryItemType.Horse;
			}
			else if (equipmentIndex == 11)
			{
				flag = inventoryItemTypeOfItem == InventoryItemType.HorseHarness;
			}
			return flag;
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x00008378 File Offset: 0x00006578
		public int Price
		{
			get
			{
				return ItemData.GetPriceOf(this.TypeId, this.ModifierId);
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x0000838B File Offset: 0x0000658B
		public bool IsValid
		{
			get
			{
				return ItemData.IsItemValid(this.TypeId, this.ModifierId);
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x0000839E File Offset: 0x0000659E
		public string ItemKey
		{
			get
			{
				return this.TypeId + "|" + this.ModifierId;
			}
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x000083B6 File Offset: 0x000065B6
		public static int GetPriceOf(string itemId, string modifierId)
		{
			return ItemList.GetPriceOf(itemId, modifierId);
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x000083BF File Offset: 0x000065BF
		public static bool IsItemValid(string itemId, string modifierId)
		{
			return ItemList.IsItemValid(itemId, modifierId);
		}
	}
}
