using System;

namespace TaleWorlds.Core.ImageIdentifiers
{
	// Token: 0x020000E4 RID: 228
	public class CharacterImageIdentifier : ImageIdentifier
	{
		// Token: 0x06000B8A RID: 2954 RVA: 0x000255AC File Offset: 0x000237AC
		public CharacterImageIdentifier(CharacterCode characterCode)
		{
			base.Id = ((characterCode != null) ? characterCode.Code : null) ?? "";
			base.AdditionalArgs = "";
			base.TextureProviderName = "CharacterImageTextureProvider";
		}
	}
}
