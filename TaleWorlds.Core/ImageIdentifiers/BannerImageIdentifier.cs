using System;

namespace TaleWorlds.Core.ImageIdentifiers
{
	// Token: 0x020000E3 RID: 227
	public class BannerImageIdentifier : ImageIdentifier
	{
		// Token: 0x06000B89 RID: 2953 RVA: 0x0002556E File Offset: 0x0002376E
		public BannerImageIdentifier(Banner banner, bool nineGrid = false)
		{
			base.Id = ((banner != null) ? banner.BannerCode : "");
			base.AdditionalArgs = (nineGrid ? "ninegrid" : "");
			base.TextureProviderName = "BannerImageTextureProvider";
		}
	}
}
