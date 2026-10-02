using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000055 RID: 85
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class PlayerAssignedPartyLeaderMessage : Message
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00003358 File Offset: 0x00001558
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x00003360 File Offset: 0x00001560
		[JsonProperty]
		public PlayerId PartyLeaderId { get; private set; }

		// Token: 0x060001B5 RID: 437 RVA: 0x00003369 File Offset: 0x00001569
		public PlayerAssignedPartyLeaderMessage()
		{
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00003371 File Offset: 0x00001571
		public PlayerAssignedPartyLeaderMessage(PlayerId partyLeaderId)
		{
			this.PartyLeaderId = partyLeaderId;
		}
	}
}
