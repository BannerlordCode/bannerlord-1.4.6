using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000B2 RID: 178
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class PromotePlayerToPartyLeaderMessage : Message
	{
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00004281 File Offset: 0x00002481
		// (set) Token: 0x06000327 RID: 807 RVA: 0x00004289 File Offset: 0x00002489
		[JsonProperty]
		public PlayerId PromotedPlayerId { get; private set; }

		// Token: 0x06000328 RID: 808 RVA: 0x00004292 File Offset: 0x00002492
		public PromotePlayerToPartyLeaderMessage()
		{
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000429A File Offset: 0x0000249A
		public PromotePlayerToPartyLeaderMessage(PlayerId promotedPlayerId)
		{
			this.PromotedPlayerId = promotedPlayerId;
		}
	}
}
