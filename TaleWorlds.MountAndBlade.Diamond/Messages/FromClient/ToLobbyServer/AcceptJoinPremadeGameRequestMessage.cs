using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000070 RID: 112
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class AcceptJoinPremadeGameRequestMessage : Message
	{
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000233 RID: 563 RVA: 0x000038B9 File Offset: 0x00001AB9
		// (set) Token: 0x06000234 RID: 564 RVA: 0x000038C1 File Offset: 0x00001AC1
		[JsonProperty]
		public Guid PartyId { get; private set; }

		// Token: 0x06000235 RID: 565 RVA: 0x000038CA File Offset: 0x00001ACA
		public AcceptJoinPremadeGameRequestMessage()
		{
		}

		// Token: 0x06000236 RID: 566 RVA: 0x000038D2 File Offset: 0x00001AD2
		public AcceptJoinPremadeGameRequestMessage(Guid partyId)
		{
			this.PartyId = partyId;
		}
	}
}
