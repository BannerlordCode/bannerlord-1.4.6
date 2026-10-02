using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000AB RID: 171
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class InviteToClanMessage : Message
	{
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000306 RID: 774 RVA: 0x00004139 File Offset: 0x00002339
		// (set) Token: 0x06000307 RID: 775 RVA: 0x00004141 File Offset: 0x00002341
		[JsonProperty]
		public PlayerId InvitedPlayerId { get; private set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0000414A File Offset: 0x0000234A
		// (set) Token: 0x06000309 RID: 777 RVA: 0x00004152 File Offset: 0x00002352
		[JsonProperty]
		public bool DontUseNameForUnknownPlayer { get; private set; }

		// Token: 0x0600030A RID: 778 RVA: 0x0000415B File Offset: 0x0000235B
		public InviteToClanMessage()
		{
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00004163 File Offset: 0x00002363
		public InviteToClanMessage(PlayerId invitedPlayerId, bool dontUseNameForUnknownPlayer)
		{
			this.InvitedPlayerId = invitedPlayerId;
			this.DontUseNameForUnknownPlayer = dontUseNameForUnknownPlayer;
		}
	}
}
