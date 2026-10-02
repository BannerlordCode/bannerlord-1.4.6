using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200011D RID: 285
	[Flags]
	internal enum InventoryItemType
	{
		// Token: 0x04000299 RID: 665
		None = 0,
		// Token: 0x0400029A RID: 666
		Weapon = 1,
		// Token: 0x0400029B RID: 667
		Shield = 2,
		// Token: 0x0400029C RID: 668
		HeadArmor = 4,
		// Token: 0x0400029D RID: 669
		BodyArmor = 8,
		// Token: 0x0400029E RID: 670
		LegArmor = 16,
		// Token: 0x0400029F RID: 671
		HandArmor = 32,
		// Token: 0x040002A0 RID: 672
		Horse = 64,
		// Token: 0x040002A1 RID: 673
		HorseHarness = 128,
		// Token: 0x040002A2 RID: 674
		Goods = 256,
		// Token: 0x040002A3 RID: 675
		Book = 512,
		// Token: 0x040002A4 RID: 676
		Animal = 1024,
		// Token: 0x040002A5 RID: 677
		Cape = 2048,
		// Token: 0x040002A6 RID: 678
		HorseCategory = 192,
		// Token: 0x040002A7 RID: 679
		Armors = 2108,
		// Token: 0x040002A8 RID: 680
		Equipable = 2303,
		// Token: 0x040002A9 RID: 681
		All = 4095
	}
}
