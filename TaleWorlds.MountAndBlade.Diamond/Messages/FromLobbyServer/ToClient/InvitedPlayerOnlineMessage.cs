using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000048 RID: 72
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class InvitedPlayerOnlineMessage : Message
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000173 RID: 371 RVA: 0x000030A5 File Offset: 0x000012A5
		// (set) Token: 0x06000174 RID: 372 RVA: 0x000030AD File Offset: 0x000012AD
		[JsonProperty]
		public PlayerId PlayerId { get; set; }

		// Token: 0x06000175 RID: 373 RVA: 0x000030B6 File Offset: 0x000012B6
		public InvitedPlayerOnlineMessage()
		{
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000030BE File Offset: 0x000012BE
		public InvitedPlayerOnlineMessage(PlayerId playerId)
		{
			this.PlayerId = playerId;
		}
	}
}
