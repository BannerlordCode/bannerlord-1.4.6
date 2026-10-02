using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200005D RID: 93
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class PlayerSuggestedToPartyMessage : Message
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00003512 File Offset: 0x00001712
		// (set) Token: 0x060001DB RID: 475 RVA: 0x0000351A File Offset: 0x0000171A
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00003523 File Offset: 0x00001723
		// (set) Token: 0x060001DD RID: 477 RVA: 0x0000352B File Offset: 0x0000172B
		[JsonProperty]
		public string PlayerName { get; private set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00003534 File Offset: 0x00001734
		// (set) Token: 0x060001DF RID: 479 RVA: 0x0000353C File Offset: 0x0000173C
		[JsonProperty]
		public PlayerId SuggestingPlayerId { get; private set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00003545 File Offset: 0x00001745
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x0000354D File Offset: 0x0000174D
		[JsonProperty]
		public string SuggestingPlayerName { get; private set; }

		// Token: 0x060001E2 RID: 482 RVA: 0x00003556 File Offset: 0x00001756
		public PlayerSuggestedToPartyMessage()
		{
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000355E File Offset: 0x0000175E
		public PlayerSuggestedToPartyMessage(PlayerId playerId, string playerName, PlayerId suggestingPlayerId, string suggestingPlayerName)
		{
			this.PlayerId = playerId;
			this.PlayerName = playerName;
			this.SuggestingPlayerId = suggestingPlayerId;
			this.SuggestingPlayerName = suggestingPlayerName;
		}
	}
}
