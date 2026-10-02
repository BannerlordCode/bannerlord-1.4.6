using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000072 RID: 114
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class AcceptPartyJoinRequestMessage : Message
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000238 RID: 568 RVA: 0x000038E9 File Offset: 0x00001AE9
		// (set) Token: 0x06000239 RID: 569 RVA: 0x000038F1 File Offset: 0x00001AF1
		[JsonProperty]
		public PlayerId RequesterPlayerId { get; private set; }

		// Token: 0x0600023A RID: 570 RVA: 0x000038FA File Offset: 0x00001AFA
		public AcceptPartyJoinRequestMessage()
		{
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00003902 File Offset: 0x00001B02
		public AcceptPartyJoinRequestMessage(PlayerId requesterPlayerId)
		{
			this.RequesterPlayerId = requesterPlayerId;
		}
	}
}
