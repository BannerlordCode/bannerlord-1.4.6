using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000089 RID: 137
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class DeclineJoinPremadeGameRequestMessage : Message
	{
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00003CFE File Offset: 0x00001EFE
		// (set) Token: 0x0600029D RID: 669 RVA: 0x00003D06 File Offset: 0x00001F06
		[JsonProperty]
		public Guid PartyId { get; private set; }

		// Token: 0x0600029E RID: 670 RVA: 0x00003D0F File Offset: 0x00001F0F
		public DeclineJoinPremadeGameRequestMessage()
		{
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00003D17 File Offset: 0x00001F17
		public DeclineJoinPremadeGameRequestMessage(Guid partyId)
		{
			this.PartyId = partyId;
		}
	}
}
