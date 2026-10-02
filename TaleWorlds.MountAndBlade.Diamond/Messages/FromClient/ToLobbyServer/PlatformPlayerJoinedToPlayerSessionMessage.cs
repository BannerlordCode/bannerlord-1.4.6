using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000B1 RID: 177
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class PlatformPlayerJoinedToPlayerSessionMessage : Message
	{
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000322 RID: 802 RVA: 0x00004259 File Offset: 0x00002459
		// (set) Token: 0x06000323 RID: 803 RVA: 0x00004261 File Offset: 0x00002461
		[JsonProperty]
		public PlayerId InviterPlayerId { get; private set; }

		// Token: 0x06000324 RID: 804 RVA: 0x0000426A File Offset: 0x0000246A
		public PlatformPlayerJoinedToPlayerSessionMessage()
		{
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00004272 File Offset: 0x00002472
		public PlatformPlayerJoinedToPlayerSessionMessage(PlayerId inviterPlayerId)
		{
			this.InviterPlayerId = inviterPlayerId;
		}
	}
}
