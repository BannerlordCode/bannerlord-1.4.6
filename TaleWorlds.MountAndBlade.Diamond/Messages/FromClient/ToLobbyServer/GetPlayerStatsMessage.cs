using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000A2 RID: 162
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class GetPlayerStatsMessage : Message
	{
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00003FB7 File Offset: 0x000021B7
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x00003FBF File Offset: 0x000021BF
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x060002E4 RID: 740 RVA: 0x00003FC8 File Offset: 0x000021C8
		public GetPlayerStatsMessage()
		{
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00003FD0 File Offset: 0x000021D0
		public GetPlayerStatsMessage(PlayerId playerId)
		{
			this.PlayerId = playerId;
		}
	}
}
