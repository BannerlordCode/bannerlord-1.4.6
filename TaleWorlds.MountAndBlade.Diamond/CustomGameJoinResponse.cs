using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200012B RID: 299
	public enum CustomGameJoinResponse
	{
		// Token: 0x0400032E RID: 814
		Success,
		// Token: 0x0400032F RID: 815
		IncorrectPlayerState,
		// Token: 0x04000330 RID: 816
		ServerCapacityIsFull,
		// Token: 0x04000331 RID: 817
		ErrorOnGameServer,
		// Token: 0x04000332 RID: 818
		GameServerAccessError,
		// Token: 0x04000333 RID: 819
		CustomGameServerNotAvailable,
		// Token: 0x04000334 RID: 820
		CustomGameServerFinishing,
		// Token: 0x04000335 RID: 821
		IncorrectPassword,
		// Token: 0x04000336 RID: 822
		PlayerBanned,
		// Token: 0x04000337 RID: 823
		HostReplyTimedOut,
		// Token: 0x04000338 RID: 824
		NoPlayerDataFound,
		// Token: 0x04000339 RID: 825
		UnspecifiedError,
		// Token: 0x0400033A RID: 826
		NoPlayersCanJoin,
		// Token: 0x0400033B RID: 827
		AlreadyRequestedWaitingForServerResponse,
		// Token: 0x0400033C RID: 828
		RequesterIsNotPartyLeader,
		// Token: 0x0400033D RID: 829
		NotAllPlayersReady,
		// Token: 0x0400033E RID: 830
		NotAllPlayersModulesMatchWithServer
	}
}
