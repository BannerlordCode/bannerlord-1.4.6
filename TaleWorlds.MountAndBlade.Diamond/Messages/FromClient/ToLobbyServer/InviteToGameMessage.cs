using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000AC RID: 172
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class InviteToGameMessage : Message
	{
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600030C RID: 780 RVA: 0x00004179 File Offset: 0x00002379
		// (set) Token: 0x0600030D RID: 781 RVA: 0x00004181 File Offset: 0x00002381
		[JsonProperty]
		public PlayerId InvitedPlayerId { get; private set; }

		// Token: 0x0600030E RID: 782 RVA: 0x0000418A File Offset: 0x0000238A
		public InviteToGameMessage()
		{
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00004192 File Offset: 0x00002392
		public InviteToGameMessage(PlayerId invitedPlayerId)
		{
			this.InvitedPlayerId = invitedPlayerId;
		}
	}
}
