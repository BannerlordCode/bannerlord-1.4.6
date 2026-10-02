using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000247 RID: 583
	[Flags]
	public enum GoldGainFlags : ushort
	{
		// Token: 0x04000CD8 RID: 3288
		FirstRangedKill = 1,
		// Token: 0x04000CD9 RID: 3289
		FirstMeleeKill = 2,
		// Token: 0x04000CDA RID: 3290
		FirstAssist = 4,
		// Token: 0x04000CDB RID: 3291
		SecondAssist = 8,
		// Token: 0x04000CDC RID: 3292
		ThirdAssist = 16,
		// Token: 0x04000CDD RID: 3293
		FifthKill = 32,
		// Token: 0x04000CDE RID: 3294
		TenthKill = 64,
		// Token: 0x04000CDF RID: 3295
		DefaultKill = 128,
		// Token: 0x04000CE0 RID: 3296
		DefaultAssist = 256,
		// Token: 0x04000CE1 RID: 3297
		ObjectiveCompleted = 512,
		// Token: 0x04000CE2 RID: 3298
		ObjectiveDestroyed = 1024,
		// Token: 0x04000CE3 RID: 3299
		PerkBonus = 2048
	}
}
