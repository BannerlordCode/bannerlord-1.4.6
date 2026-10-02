using System;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.Core.ImageIdentifiers
{
	// Token: 0x020000E9 RID: 233
	public class PlayerAvatarImageIdentifier : ImageIdentifier
	{
		// Token: 0x06000B96 RID: 2966 RVA: 0x000256FB File Offset: 0x000238FB
		public PlayerAvatarImageIdentifier(PlayerId playerId, int forcedAvatarIndex)
		{
			base.Id = playerId.ToString();
			base.AdditionalArgs = string.Format("{0}", forcedAvatarIndex);
			base.TextureProviderName = "PlayerAvatarImageTextureProvider";
		}
	}
}
