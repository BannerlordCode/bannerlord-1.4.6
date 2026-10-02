using System;

namespace TaleWorlds.Core.ImageIdentifiers
{
	// Token: 0x020000E6 RID: 230
	public class EmptyImageIdentifier : ImageIdentifier
	{
		// Token: 0x06000B8C RID: 2956 RVA: 0x00025624 File Offset: 0x00023824
		public EmptyImageIdentifier()
		{
			base.Id = string.Empty;
			base.AdditionalArgs = string.Empty;
			base.TextureProviderName = string.Empty;
		}
	}
}
