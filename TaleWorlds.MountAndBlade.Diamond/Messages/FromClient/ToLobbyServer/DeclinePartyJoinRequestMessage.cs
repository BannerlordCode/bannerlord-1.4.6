using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x0200008B RID: 139
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class DeclinePartyJoinRequestMessage : Message
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00003D2E File Offset: 0x00001F2E
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x00003D36 File Offset: 0x00001F36
		[JsonProperty]
		public PlayerId RequesterPlayerId { get; private set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00003D3F File Offset: 0x00001F3F
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x00003D47 File Offset: 0x00001F47
		[JsonProperty]
		public PartyJoinDeclineReason Reason { get; private set; }

		// Token: 0x060002A5 RID: 677 RVA: 0x00003D50 File Offset: 0x00001F50
		public DeclinePartyJoinRequestMessage()
		{
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00003D58 File Offset: 0x00001F58
		public DeclinePartyJoinRequestMessage(PlayerId requesterPlayerId, PartyJoinDeclineReason reason)
		{
			this.RequesterPlayerId = requesterPlayerId;
			this.Reason = reason;
		}
	}
}
