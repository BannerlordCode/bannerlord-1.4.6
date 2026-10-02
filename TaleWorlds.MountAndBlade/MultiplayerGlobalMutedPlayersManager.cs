using System;
using System.Collections.Generic;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200031C RID: 796
	public static class MultiplayerGlobalMutedPlayersManager
	{
		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06002D3F RID: 11583 RVA: 0x000AF824 File Offset: 0x000ADA24
		private static List<PlayerId> _mutedPlayers
		{
			get
			{
				if (MultiplayerGlobalMutedPlayersManager._mutedPlayersInternal == null)
				{
					MultiplayerGlobalMutedPlayersManager._mutedPlayersInternal = new List<PlayerId>();
				}
				return MultiplayerGlobalMutedPlayersManager._mutedPlayersInternal;
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06002D40 RID: 11584 RVA: 0x000AF83C File Offset: 0x000ADA3C
		public static List<PlayerId> MutedPlayers
		{
			get
			{
				return MultiplayerGlobalMutedPlayersManager._mutedPlayers;
			}
		}

		// Token: 0x06002D41 RID: 11585 RVA: 0x000AF843 File Offset: 0x000ADA43
		public static void MutePlayer(PlayerId playerId)
		{
			MultiplayerGlobalMutedPlayersManager._mutedPlayers.Add(playerId);
		}

		// Token: 0x06002D42 RID: 11586 RVA: 0x000AF850 File Offset: 0x000ADA50
		public static void UnmutePlayer(PlayerId playerId)
		{
			MultiplayerGlobalMutedPlayersManager._mutedPlayers.Remove(playerId);
		}

		// Token: 0x06002D43 RID: 11587 RVA: 0x000AF85E File Offset: 0x000ADA5E
		public static bool IsUserMuted(PlayerId playerId)
		{
			return MultiplayerGlobalMutedPlayersManager._mutedPlayers.Contains(playerId);
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x000AF86B File Offset: 0x000ADA6B
		public static void ClearMutedPlayers()
		{
			MultiplayerGlobalMutedPlayersManager._mutedPlayers.Clear();
		}

		// Token: 0x040011D6 RID: 4566
		private static List<PlayerId> _mutedPlayersInternal;
	}
}
