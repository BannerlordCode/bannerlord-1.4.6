using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000056 RID: 86
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class PlayerInvitedToPartyMessage : Message
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00003380 File Offset: 0x00001580
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x00003388 File Offset: 0x00001588
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00003391 File Offset: 0x00001591
		// (set) Token: 0x060001BA RID: 442 RVA: 0x00003399 File Offset: 0x00001599
		[JsonProperty]
		public string PlayerName { get; private set; }

		// Token: 0x060001BB RID: 443 RVA: 0x000033A2 File Offset: 0x000015A2
		public PlayerInvitedToPartyMessage()
		{
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000033AA File Offset: 0x000015AA
		public PlayerInvitedToPartyMessage(PlayerId playerId, string playerName)
		{
			this.PlayerId = playerId;
			this.PlayerName = playerName;
		}
	}
}
