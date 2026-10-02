using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000BF RID: 191
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class RequestJoinPlayerPartyMessage : Message
	{
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600036F RID: 879 RVA: 0x000045A6 File Offset: 0x000027A6
		// (set) Token: 0x06000370 RID: 880 RVA: 0x000045AE File Offset: 0x000027AE
		[JsonProperty]
		public PlayerId TargetPlayer { get; private set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000371 RID: 881 RVA: 0x000045B7 File Offset: 0x000027B7
		// (set) Token: 0x06000372 RID: 882 RVA: 0x000045BF File Offset: 0x000027BF
		[JsonProperty]
		public bool InviteRequest { get; private set; }

		// Token: 0x06000373 RID: 883 RVA: 0x000045C8 File Offset: 0x000027C8
		public RequestJoinPlayerPartyMessage()
		{
		}

		// Token: 0x06000374 RID: 884 RVA: 0x000045D0 File Offset: 0x000027D0
		public RequestJoinPlayerPartyMessage(PlayerId targetPlayer, bool inviteRequest)
		{
			this.TargetPlayer = targetPlayer;
			this.InviteRequest = inviteRequest;
		}
	}
}
