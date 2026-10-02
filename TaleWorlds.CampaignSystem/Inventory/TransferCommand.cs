using System;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.Inventory
{
	// Token: 0x020000DA RID: 218
	public struct TransferCommand
	{
		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060014F6 RID: 5366 RVA: 0x00060DB4 File Offset: 0x0005EFB4
		public Equipment FromSideEquipment
		{
			get
			{
				switch (this.FromSide)
				{
				case InventoryLogic.InventorySide.CivilianEquipment:
				{
					CharacterObject character = this.Character;
					if (character == null)
					{
						return null;
					}
					return character.FirstCivilianEquipment;
				}
				case InventoryLogic.InventorySide.BattleEquipment:
				{
					CharacterObject character2 = this.Character;
					if (character2 == null)
					{
						return null;
					}
					return character2.FirstBattleEquipment;
				}
				case InventoryLogic.InventorySide.StealthEquipment:
				{
					CharacterObject character3 = this.Character;
					if (character3 == null)
					{
						return null;
					}
					return character3.FirstStealthEquipment;
				}
				default:
					return null;
				}
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x00060E18 File Offset: 0x0005F018
		public Equipment ToSideEquipment
		{
			get
			{
				switch (this.ToSide)
				{
				case InventoryLogic.InventorySide.CivilianEquipment:
				{
					CharacterObject character = this.Character;
					if (character == null)
					{
						return null;
					}
					return character.FirstCivilianEquipment;
				}
				case InventoryLogic.InventorySide.BattleEquipment:
				{
					CharacterObject character2 = this.Character;
					if (character2 == null)
					{
						return null;
					}
					return character2.FirstBattleEquipment;
				}
				case InventoryLogic.InventorySide.StealthEquipment:
				{
					CharacterObject character3 = this.Character;
					if (character3 == null)
					{
						return null;
					}
					return character3.FirstStealthEquipment;
				}
				default:
					return null;
				}
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x060014F8 RID: 5368 RVA: 0x00060E79 File Offset: 0x0005F079
		// (set) Token: 0x060014F9 RID: 5369 RVA: 0x00060E81 File Offset: 0x0005F081
		public InventoryLogic.InventorySide FromSide { get; private set; }

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x060014FA RID: 5370 RVA: 0x00060E8A File Offset: 0x0005F08A
		// (set) Token: 0x060014FB RID: 5371 RVA: 0x00060E92 File Offset: 0x0005F092
		public InventoryLogic.InventorySide ToSide { get; private set; }

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060014FC RID: 5372 RVA: 0x00060E9B File Offset: 0x0005F09B
		// (set) Token: 0x060014FD RID: 5373 RVA: 0x00060EA3 File Offset: 0x0005F0A3
		public EquipmentIndex FromEquipmentIndex { get; private set; }

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060014FE RID: 5374 RVA: 0x00060EAC File Offset: 0x0005F0AC
		// (set) Token: 0x060014FF RID: 5375 RVA: 0x00060EB4 File Offset: 0x0005F0B4
		public EquipmentIndex ToEquipmentIndex { get; private set; }

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001500 RID: 5376 RVA: 0x00060EBD File Offset: 0x0005F0BD
		// (set) Token: 0x06001501 RID: 5377 RVA: 0x00060EC5 File Offset: 0x0005F0C5
		public int Amount { get; private set; }

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x00060ECE File Offset: 0x0005F0CE
		// (set) Token: 0x06001503 RID: 5379 RVA: 0x00060ED6 File Offset: 0x0005F0D6
		public ItemRosterElement ElementToTransfer { get; private set; }

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x00060EDF File Offset: 0x0005F0DF
		// (set) Token: 0x06001505 RID: 5381 RVA: 0x00060EE7 File Offset: 0x0005F0E7
		public CharacterObject Character { get; private set; }

		// Token: 0x06001506 RID: 5382 RVA: 0x00060EF0 File Offset: 0x0005F0F0
		public static TransferCommand Transfer(int amount, InventoryLogic.InventorySide fromSide, InventoryLogic.InventorySide toSide, ItemRosterElement elementToTransfer, EquipmentIndex fromEquipmentIndex, EquipmentIndex toEquipmentIndex, CharacterObject character)
		{
			return new TransferCommand
			{
				FromSide = fromSide,
				ToSide = toSide,
				ElementToTransfer = elementToTransfer,
				FromEquipmentIndex = fromEquipmentIndex,
				ToEquipmentIndex = toEquipmentIndex,
				Character = character,
				Amount = amount
			};
		}
	}
}
