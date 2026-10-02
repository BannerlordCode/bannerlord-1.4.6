using System;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.Diamond.Cosmetics.CosmeticTypes
{
	// Token: 0x0200017B RID: 379
	public class TauntCosmeticElement : CosmeticElement
	{
		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x00011A9D File Offset: 0x0000FC9D
		public static int MaxNumberOfTaunts
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x00011AA0 File Offset: 0x0000FCA0
		public TextObject Name { get; }

		// Token: 0x06000A99 RID: 2713 RVA: 0x00011AA8 File Offset: 0x0000FCA8
		public TauntCosmeticElement(int index, string id, CosmeticsManager.CosmeticRarity rarity, int cost, string name)
			: base(id, rarity, cost, CosmeticsManager.CosmeticType.Taunt)
		{
			this.UsageIndex = index;
			this.Name = new TextObject(name, null);
		}
	}
}
