using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000114 RID: 276
	[Flags]
	public enum Features
	{
		// Token: 0x04000262 RID: 610
		None = 0,
		// Token: 0x04000263 RID: 611
		Matchmaking = 1,
		// Token: 0x04000264 RID: 612
		CustomGame = 2,
		// Token: 0x04000265 RID: 613
		Party = 4,
		// Token: 0x04000266 RID: 614
		Clan = 8,
		// Token: 0x04000267 RID: 615
		BannerlordFriendList = 16,
		// Token: 0x04000268 RID: 616
		TextChat = 32,
		// Token: 0x04000269 RID: 617
		All = -1
	}
}
