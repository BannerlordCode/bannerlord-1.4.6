using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000111 RID: 273
	public enum DisconnectType
	{
		// Token: 0x04000253 RID: 595
		QuitFromGame,
		// Token: 0x04000254 RID: 596
		TimedOut,
		// Token: 0x04000255 RID: 597
		KickedByHost,
		// Token: 0x04000256 RID: 598
		KickedByPoll,
		// Token: 0x04000257 RID: 599
		BannedByPoll,
		// Token: 0x04000258 RID: 600
		Inactivity,
		// Token: 0x04000259 RID: 601
		DisconnectedFromLobby,
		// Token: 0x0400025A RID: 602
		GameEnded,
		// Token: 0x0400025B RID: 603
		ServerNotResponding,
		// Token: 0x0400025C RID: 604
		KickedDueToFriendlyDamage,
		// Token: 0x0400025D RID: 605
		PlayStateMismatch,
		// Token: 0x0400025E RID: 606
		Unknown
	}
}
