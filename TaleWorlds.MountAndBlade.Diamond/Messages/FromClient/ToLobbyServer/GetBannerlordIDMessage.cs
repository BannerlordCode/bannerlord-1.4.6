using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000096 RID: 150
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class GetBannerlordIDMessage : Message
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00003E9F File Offset: 0x0000209F
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x00003EA7 File Offset: 0x000020A7
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x060002C7 RID: 711 RVA: 0x00003EB0 File Offset: 0x000020B0
		public GetBannerlordIDMessage()
		{
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00003EB8 File Offset: 0x000020B8
		public GetBannerlordIDMessage(PlayerId playerId)
		{
			this.PlayerId = playerId;
		}
	}
}
