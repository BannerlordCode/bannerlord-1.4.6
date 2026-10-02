using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000063 RID: 99
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class RequestJoinPartyMessage : Message
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00003643 File Offset: 0x00001843
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x0000364B File Offset: 0x0000184B
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00003654 File Offset: 0x00001854
		// (set) Token: 0x060001FA RID: 506 RVA: 0x0000365C File Offset: 0x0000185C
		[JsonProperty]
		public string PlayerName { get; private set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00003665 File Offset: 0x00001865
		// (set) Token: 0x060001FC RID: 508 RVA: 0x0000366D File Offset: 0x0000186D
		[JsonProperty]
		public PlayerId ViaPlayerId { get; private set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00003676 File Offset: 0x00001876
		// (set) Token: 0x060001FE RID: 510 RVA: 0x0000367E File Offset: 0x0000187E
		[JsonProperty]
		public string ViaPlayerName { get; private set; }

		// Token: 0x060001FF RID: 511 RVA: 0x00003687 File Offset: 0x00001887
		public RequestJoinPartyMessage()
		{
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000368F File Offset: 0x0000188F
		public RequestJoinPartyMessage(PlayerId playerId, string playerName, PlayerId viaPlayerId, string viaPlayerName)
		{
			this.PlayerId = playerId;
			this.PlayerName = playerName;
			this.ViaPlayerId = viaPlayerId;
			this.ViaPlayerName = viaPlayerName;
		}
	}
}
