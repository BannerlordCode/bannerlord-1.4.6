using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000135 RID: 309
	public enum MBLoginErrorCode
	{
		// Token: 0x0400035E RID: 862
		None,
		// Token: 0x0400035F RID: 863
		CouldNotLogin,
		// Token: 0x04000360 RID: 864
		VersionMismatch,
		// Token: 0x04000361 RID: 865
		IncorrectPassword,
		// Token: 0x04000362 RID: 866
		FamilyShareNotAllowed,
		// Token: 0x04000363 RID: 867
		BannedFromGame,
		// Token: 0x04000364 RID: 868
		NoAuthenticationToken,
		// Token: 0x04000365 RID: 869
		AuthTokenExpired,
		// Token: 0x04000366 RID: 870
		BannedFromHostingServers,
		// Token: 0x04000367 RID: 871
		CustomBattleServerIncompatibleVersion,
		// Token: 0x04000368 RID: 872
		ReachedMaxNumberofCustomBattleServers,
		// Token: 0x04000369 RID: 873
		CouldNotDestroyOldSession,
		// Token: 0x0400036A RID: 874
		LoggingInDisabled
	}
}
