using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000047 RID: 71
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class InvitationToPartyMessage : Message
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00003065 File Offset: 0x00001265
		// (set) Token: 0x0600016E RID: 366 RVA: 0x0000306D File Offset: 0x0000126D
		[JsonProperty]
		public string InviterPlayerName { get; private set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00003076 File Offset: 0x00001276
		// (set) Token: 0x06000170 RID: 368 RVA: 0x0000307E File Offset: 0x0000127E
		[JsonProperty]
		public PlayerId InviterPlayerId { get; private set; }

		// Token: 0x06000171 RID: 369 RVA: 0x00003087 File Offset: 0x00001287
		public InvitationToPartyMessage()
		{
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000308F File Offset: 0x0000128F
		public InvitationToPartyMessage(string inviterPlayerName, PlayerId inviterPlayerId)
		{
			this.InviterPlayerName = inviterPlayerName;
			this.InviterPlayerId = inviterPlayerId;
		}
	}
}
