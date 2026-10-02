using System;

namespace TaleWorlds.MountAndBlade.Diamond.Cosmetics.CosmeticTypes
{
	// Token: 0x0200017A RID: 378
	public class SigilCosmeticElement : CosmeticElement
	{
		// Token: 0x06000A96 RID: 2710 RVA: 0x00011A89 File Offset: 0x0000FC89
		public SigilCosmeticElement(string id, CosmeticsManager.CosmeticRarity rarity, int cost, string bannerCode)
			: base(id, rarity, cost, CosmeticsManager.CosmeticType.Sigil)
		{
			this.BannerCode = bannerCode;
		}

		// Token: 0x04000521 RID: 1313
		public string BannerCode;
	}
}
