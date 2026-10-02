using System;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.Inventory
{
	// Token: 0x020000D9 RID: 217
	public class TransferCommandResult
	{
		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060014E7 RID: 5351 RVA: 0x00060CB0 File Offset: 0x0005EEB0
		public Equipment ResultSideEquipment
		{
			get
			{
				switch (this.ResultSide)
				{
				case InventoryLogic.InventorySide.CivilianEquipment:
				{
					CharacterObject transferCharacter = this.TransferCharacter;
					if (transferCharacter == null)
					{
						return null;
					}
					return transferCharacter.FirstCivilianEquipment;
				}
				case InventoryLogic.InventorySide.BattleEquipment:
				{
					CharacterObject transferCharacter2 = this.TransferCharacter;
					if (transferCharacter2 == null)
					{
						return null;
					}
					return transferCharacter2.FirstBattleEquipment;
				}
				case InventoryLogic.InventorySide.StealthEquipment:
				{
					CharacterObject transferCharacter3 = this.TransferCharacter;
					if (transferCharacter3 == null)
					{
						return null;
					}
					return transferCharacter3.FirstStealthEquipment;
				}
				default:
					return null;
				}
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060014E8 RID: 5352 RVA: 0x00060D11 File Offset: 0x0005EF11
		// (set) Token: 0x060014E9 RID: 5353 RVA: 0x00060D19 File Offset: 0x0005EF19
		public CharacterObject TransferCharacter { get; private set; }

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x00060D22 File Offset: 0x0005EF22
		// (set) Token: 0x060014EB RID: 5355 RVA: 0x00060D2A File Offset: 0x0005EF2A
		public InventoryLogic.InventorySide ResultSide { get; private set; }

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x060014EC RID: 5356 RVA: 0x00060D33 File Offset: 0x0005EF33
		// (set) Token: 0x060014ED RID: 5357 RVA: 0x00060D3B File Offset: 0x0005EF3B
		public ItemRosterElement EffectedItemRosterElement { get; private set; }

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060014EE RID: 5358 RVA: 0x00060D44 File Offset: 0x0005EF44
		// (set) Token: 0x060014EF RID: 5359 RVA: 0x00060D4C File Offset: 0x0005EF4C
		public int EffectedNumber { get; private set; }

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x060014F0 RID: 5360 RVA: 0x00060D55 File Offset: 0x0005EF55
		// (set) Token: 0x060014F1 RID: 5361 RVA: 0x00060D5D File Offset: 0x0005EF5D
		public int FinalNumber { get; private set; }

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060014F2 RID: 5362 RVA: 0x00060D66 File Offset: 0x0005EF66
		// (set) Token: 0x060014F3 RID: 5363 RVA: 0x00060D6E File Offset: 0x0005EF6E
		public EquipmentIndex EffectedEquipmentIndex { get; private set; }

		// Token: 0x060014F4 RID: 5364 RVA: 0x00060D77 File Offset: 0x0005EF77
		public TransferCommandResult()
		{
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x00060D7F File Offset: 0x0005EF7F
		public TransferCommandResult(InventoryLogic.InventorySide resultSide, ItemRosterElement effectedItemRosterElement, int effectedNumber, int finalNumber, EquipmentIndex effectedEquipmentIndex, CharacterObject transferCharacter)
		{
			this.ResultSide = resultSide;
			this.EffectedItemRosterElement = effectedItemRosterElement;
			this.EffectedNumber = effectedNumber;
			this.FinalNumber = finalNumber;
			this.EffectedEquipmentIndex = effectedEquipmentIndex;
			this.TransferCharacter = transferCharacter;
		}
	}
}
