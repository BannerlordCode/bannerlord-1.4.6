using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000AF RID: 175
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class KickPlayerFromPartyMessage : Message
	{
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600031A RID: 794 RVA: 0x00004209 File Offset: 0x00002409
		// (set) Token: 0x0600031B RID: 795 RVA: 0x00004211 File Offset: 0x00002411
		[JsonProperty]
		public PlayerId KickedPlayerId { get; private set; }

		// Token: 0x0600031C RID: 796 RVA: 0x0000421A File Offset: 0x0000241A
		public KickPlayerFromPartyMessage()
		{
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00004222 File Offset: 0x00002422
		public KickPlayerFromPartyMessage(PlayerId kickedPlayerId)
		{
			this.KickedPlayerId = kickedPlayerId;
		}
	}
}
