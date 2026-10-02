using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x0200009F RID: 159
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class GetPlayerClanInfo : Message
	{
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x00003F5F File Offset: 0x0000215F
		// (set) Token: 0x060002DA RID: 730 RVA: 0x00003F67 File Offset: 0x00002167
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x060002DB RID: 731 RVA: 0x00003F70 File Offset: 0x00002170
		public GetPlayerClanInfo()
		{
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00003F78 File Offset: 0x00002178
		public GetPlayerClanInfo(PlayerId playerId)
		{
			this.PlayerId = playerId;
		}
	}
}
