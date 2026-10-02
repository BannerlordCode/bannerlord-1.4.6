using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000A1 RID: 161
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class GetPlayerGameTypeRankInfoMessage : Message
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00003F8F File Offset: 0x0000218F
		// (set) Token: 0x060002DF RID: 735 RVA: 0x00003F97 File Offset: 0x00002197
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x060002E0 RID: 736 RVA: 0x00003FA0 File Offset: 0x000021A0
		public GetPlayerGameTypeRankInfoMessage()
		{
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00003FA8 File Offset: 0x000021A8
		public GetPlayerGameTypeRankInfoMessage(PlayerId playerId)
		{
			this.PlayerId = playerId;
		}
	}
}
