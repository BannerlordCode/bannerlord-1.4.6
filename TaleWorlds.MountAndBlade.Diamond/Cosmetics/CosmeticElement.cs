using System;

namespace TaleWorlds.MountAndBlade.Diamond.Cosmetics
{
	// Token: 0x02000177 RID: 375
	public class CosmeticElement
	{
		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x0001148F File Offset: 0x0000F68F
		public bool IsFree
		{
			get
			{
				return this.Cost <= 0;
			}
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0001149D File Offset: 0x0000F69D
		public CosmeticElement(string id, CosmeticsManager.CosmeticRarity rarity, int cost, CosmeticsManager.CosmeticType type)
		{
			this.UsageIndex = -1;
			this.Id = id;
			this.Rarity = rarity;
			this.Cost = cost;
			this.Type = type;
		}

		// Token: 0x04000518 RID: 1304
		public int UsageIndex;

		// Token: 0x04000519 RID: 1305
		public string Id;

		// Token: 0x0400051A RID: 1306
		public CosmeticsManager.CosmeticRarity Rarity;

		// Token: 0x0400051B RID: 1307
		public int Cost;

		// Token: 0x0400051C RID: 1308
		public CosmeticsManager.CosmeticType Type;
	}
}
