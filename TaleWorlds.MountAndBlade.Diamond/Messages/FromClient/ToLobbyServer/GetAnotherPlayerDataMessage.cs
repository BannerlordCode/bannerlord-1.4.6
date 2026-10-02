using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000092 RID: 146
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class GetAnotherPlayerDataMessage : Message
	{
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002BB RID: 699 RVA: 0x00003E3F File Offset: 0x0000203F
		// (set) Token: 0x060002BC RID: 700 RVA: 0x00003E47 File Offset: 0x00002047
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x060002BD RID: 701 RVA: 0x00003E50 File Offset: 0x00002050
		public GetAnotherPlayerDataMessage()
		{
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00003E58 File Offset: 0x00002058
		public GetAnotherPlayerDataMessage(PlayerId playerId)
		{
			this.PlayerId = playerId;
		}
	}
}
