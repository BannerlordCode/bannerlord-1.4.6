using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200010E RID: 270
	[Serializable]
	public class CosmeticItemInfo
	{
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x00007386 File Offset: 0x00005586
		// (set) Token: 0x060005CC RID: 1484 RVA: 0x0000738E File Offset: 0x0000558E
		public string TroopId { get; set; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x00007397 File Offset: 0x00005597
		// (set) Token: 0x060005CE RID: 1486 RVA: 0x0000739F File Offset: 0x0000559F
		public string CosmeticIndex { get; set; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x000073A8 File Offset: 0x000055A8
		// (set) Token: 0x060005D0 RID: 1488 RVA: 0x000073B0 File Offset: 0x000055B0
		public bool IsEquipped { get; set; }

		// Token: 0x060005D1 RID: 1489 RVA: 0x000073B9 File Offset: 0x000055B9
		public CosmeticItemInfo()
		{
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x000073C1 File Offset: 0x000055C1
		public CosmeticItemInfo(string troopId, string cosmeticIndex, bool isEquipped)
		{
			this.TroopId = troopId;
			this.CosmeticIndex = cosmeticIndex;
			this.IsEquipped = isEquipped;
		}
	}
}
