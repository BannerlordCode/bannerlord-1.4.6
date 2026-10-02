using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000159 RID: 345
	[Serializable]
	public class ServerStatus
	{
		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x0000ED75 File Offset: 0x0000CF75
		// (set) Token: 0x06000996 RID: 2454 RVA: 0x0000ED7D File Offset: 0x0000CF7D
		public bool IsMatchmakingEnabled { get; set; }

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x0000ED86 File Offset: 0x0000CF86
		// (set) Token: 0x06000998 RID: 2456 RVA: 0x0000ED8E File Offset: 0x0000CF8E
		public bool IsCustomBattleEnabled { get; set; }

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x0000ED97 File Offset: 0x0000CF97
		// (set) Token: 0x0600099A RID: 2458 RVA: 0x0000ED9F File Offset: 0x0000CF9F
		public bool IsPlayerBasedCustomBattleEnabled { get; set; }

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x0000EDA8 File Offset: 0x0000CFA8
		// (set) Token: 0x0600099C RID: 2460 RVA: 0x0000EDB0 File Offset: 0x0000CFB0
		public bool IsPremadeGameEnabled { get; set; }

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x0000EDB9 File Offset: 0x0000CFB9
		// (set) Token: 0x0600099E RID: 2462 RVA: 0x0000EDC1 File Offset: 0x0000CFC1
		public bool IsTestRegionEnabled { get; set; }

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x0000EDCA File Offset: 0x0000CFCA
		// (set) Token: 0x060009A0 RID: 2464 RVA: 0x0000EDD2 File Offset: 0x0000CFD2
		public Announcement Announcement { get; set; }

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x0000EDDB File Offset: 0x0000CFDB
		public ServerNotification[] ServerNotifications { get; }

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x0000EDE3 File Offset: 0x0000CFE3
		// (set) Token: 0x060009A3 RID: 2467 RVA: 0x0000EDEB File Offset: 0x0000CFEB
		public int FriendListUpdatePeriod { get; set; }

		// Token: 0x060009A4 RID: 2468 RVA: 0x0000EDF4 File Offset: 0x0000CFF4
		public ServerStatus()
		{
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0000EDFC File Offset: 0x0000CFFC
		public ServerStatus(bool isMatchmakingEnabled, bool isCustomBattleEnabled, bool isPlayerBasedCustomBattleEnabled, bool isPremadeGameEnabled, bool isTestRegionEnabled, Announcement announcement, ServerNotification[] serverNotifications, int friendListUpdatePeriod)
		{
			this.IsMatchmakingEnabled = isMatchmakingEnabled;
			this.IsCustomBattleEnabled = isCustomBattleEnabled;
			this.IsPlayerBasedCustomBattleEnabled = isPlayerBasedCustomBattleEnabled;
			this.IsPremadeGameEnabled = isPremadeGameEnabled;
			this.IsTestRegionEnabled = isTestRegionEnabled;
			this.Announcement = announcement;
			this.ServerNotifications = serverNotifications;
			this.FriendListUpdatePeriod = friendListUpdatePeriod;
		}
	}
}
