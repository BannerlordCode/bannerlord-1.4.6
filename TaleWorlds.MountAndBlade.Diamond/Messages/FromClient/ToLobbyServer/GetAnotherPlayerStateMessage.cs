using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000093 RID: 147
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class GetAnotherPlayerStateMessage : Message
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002BF RID: 703 RVA: 0x00003E67 File Offset: 0x00002067
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x00003E6F File Offset: 0x0000206F
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x060002C1 RID: 705 RVA: 0x00003E78 File Offset: 0x00002078
		public GetAnotherPlayerStateMessage()
		{
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00003E80 File Offset: 0x00002080
		public GetAnotherPlayerStateMessage(PlayerId playerId)
		{
			this.PlayerId = playerId;
		}
	}
}
