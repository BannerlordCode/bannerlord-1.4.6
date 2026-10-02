using System;

namespace TaleWorlds.MountAndBlade.Diamond.MultiplayerBadges
{
	// Token: 0x02000169 RID: 361
	public abstract class GameBadgeTracker
	{
		// Token: 0x06000A05 RID: 2565 RVA: 0x0000FFB6 File Offset: 0x0000E1B6
		public virtual void OnPlayerJoin(PlayerData playerData)
		{
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0000FFB8 File Offset: 0x0000E1B8
		public virtual void OnKill(KillData killData)
		{
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0000FFBA File Offset: 0x0000E1BA
		public virtual void OnStartingNextBattle()
		{
		}
	}
}
