using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x0200009C RID: 156
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class GetOtherPlayersStateMessage : Message
	{
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002CE RID: 718 RVA: 0x00003EEF File Offset: 0x000020EF
		// (set) Token: 0x060002CF RID: 719 RVA: 0x00003EF7 File Offset: 0x000020F7
		[JsonProperty]
		public List<PlayerId> Players { get; private set; }

		// Token: 0x060002D0 RID: 720 RVA: 0x00003F00 File Offset: 0x00002100
		public GetOtherPlayersStateMessage()
		{
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00003F08 File Offset: 0x00002108
		public GetOtherPlayersStateMessage(List<PlayerId> players)
		{
			this.Players = players;
		}
	}
}
