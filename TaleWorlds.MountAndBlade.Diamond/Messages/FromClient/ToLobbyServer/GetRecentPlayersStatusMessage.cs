using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000A7 RID: 167
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class GetRecentPlayersStatusMessage : Message
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000406F File Offset: 0x0000226F
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x00004077 File Offset: 0x00002277
		[JsonProperty]
		public PlayerId[] RecentPlayers { get; private set; }

		// Token: 0x060002F6 RID: 758 RVA: 0x00004080 File Offset: 0x00002280
		public GetRecentPlayersStatusMessage()
		{
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00004088 File Offset: 0x00002288
		public GetRecentPlayersStatusMessage(PlayerId[] recentPlayers)
		{
			this.RecentPlayers = recentPlayers;
		}
	}
}
