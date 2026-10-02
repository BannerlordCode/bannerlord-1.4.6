using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000C0 RID: 192
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class RequestToJoinPremadeGameMessage : Message
	{
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000375 RID: 885 RVA: 0x000045E6 File Offset: 0x000027E6
		// (set) Token: 0x06000376 RID: 886 RVA: 0x000045EE File Offset: 0x000027EE
		[JsonProperty]
		public Guid GameId { get; private set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000377 RID: 887 RVA: 0x000045F7 File Offset: 0x000027F7
		// (set) Token: 0x06000378 RID: 888 RVA: 0x000045FF File Offset: 0x000027FF
		[JsonProperty]
		public string Password { get; private set; }

		// Token: 0x06000379 RID: 889 RVA: 0x00004608 File Offset: 0x00002808
		public RequestToJoinPremadeGameMessage()
		{
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00004610 File Offset: 0x00002810
		public RequestToJoinPremadeGameMessage(Guid gameId, string password)
		{
			this.GameId = gameId;
			this.Password = password;
		}
	}
}
