using System;

namespace TaleWorlds.Core.ImageIdentifiers
{
	// Token: 0x020000E8 RID: 232
	public class ItemImageIdentifier : ImageIdentifier
	{
		// Token: 0x06000B95 RID: 2965 RVA: 0x000256C6 File Offset: 0x000238C6
		public ItemImageIdentifier(ItemObject item, string bannerCode = "")
		{
			base.Id = ((item != null) ? item.StringId : null) ?? "";
			base.AdditionalArgs = bannerCode;
			base.TextureProviderName = "ItemImageTextureProvider";
		}
	}
}
