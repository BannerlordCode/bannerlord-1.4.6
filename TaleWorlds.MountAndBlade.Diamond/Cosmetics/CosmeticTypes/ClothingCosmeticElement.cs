using System;
using System.Collections.Generic;

namespace TaleWorlds.MountAndBlade.Diamond.Cosmetics.CosmeticTypes
{
	// Token: 0x02000179 RID: 377
	public class ClothingCosmeticElement : CosmeticElement
	{
		// Token: 0x06000A95 RID: 2709 RVA: 0x00011A6D File Offset: 0x0000FC6D
		public ClothingCosmeticElement(string id, CosmeticsManager.CosmeticRarity rarity, int cost, List<string> replaceItemsId, List<Tuple<string, string>> replaceItemless)
			: base(id, rarity, cost, CosmeticsManager.CosmeticType.Clothing)
		{
			this.ReplaceItemsId = replaceItemsId;
			this.ReplaceItemless = replaceItemless;
		}

		// Token: 0x0400051F RID: 1311
		public readonly List<string> ReplaceItemsId;

		// Token: 0x04000520 RID: 1312
		public readonly List<Tuple<string, string>> ReplaceItemless;
	}
}
