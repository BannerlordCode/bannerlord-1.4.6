using System;
using System.Collections.Generic;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002E9 RID: 745
	public static class CustomGameBannedPlayerManager
	{
		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06002AD9 RID: 10969 RVA: 0x000A4EA1 File Offset: 0x000A30A1
		private static Dictionary<PlayerId, CustomGameBannedPlayerManager.BannedPlayer> _bannedPlayers
		{
			get
			{
				if (CustomGameBannedPlayerManager._bannedPlayersInternal == null)
				{
					CustomGameBannedPlayerManager._bannedPlayersInternal = new Dictionary<PlayerId, CustomGameBannedPlayerManager.BannedPlayer>();
				}
				return CustomGameBannedPlayerManager._bannedPlayersInternal;
			}
		}

		// Token: 0x06002ADA RID: 10970 RVA: 0x000A4EBC File Offset: 0x000A30BC
		public static void AddBannedPlayer(PlayerId playerId, int banDueTime)
		{
			CustomGameBannedPlayerManager._bannedPlayers[playerId] = new CustomGameBannedPlayerManager.BannedPlayer
			{
				PlayerId = playerId,
				BanDueTime = banDueTime
			};
		}

		// Token: 0x06002ADB RID: 10971 RVA: 0x000A4EF0 File Offset: 0x000A30F0
		public static bool IsUserBanned(PlayerId playerId)
		{
			return CustomGameBannedPlayerManager._bannedPlayers.ContainsKey(playerId) && CustomGameBannedPlayerManager._bannedPlayers[playerId].BanDueTime > Environment.TickCount;
		}

		// Token: 0x04001099 RID: 4249
		private static Dictionary<PlayerId, CustomGameBannedPlayerManager.BannedPlayer> _bannedPlayersInternal;

		// Token: 0x020005CA RID: 1482
		private struct BannedPlayer
		{
			// Token: 0x17000A80 RID: 2688
			// (get) Token: 0x06003E6F RID: 15983 RVA: 0x000F5158 File Offset: 0x000F3358
			// (set) Token: 0x06003E70 RID: 15984 RVA: 0x000F5160 File Offset: 0x000F3360
			public PlayerId PlayerId { get; set; }

			// Token: 0x17000A81 RID: 2689
			// (get) Token: 0x06003E71 RID: 15985 RVA: 0x000F5169 File Offset: 0x000F3369
			// (set) Token: 0x06003E72 RID: 15986 RVA: 0x000F5171 File Offset: 0x000F3371
			public int BanDueTime { get; set; }
		}
	}
}
