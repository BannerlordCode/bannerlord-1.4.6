using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000A5 RID: 165
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class GetRankedLeaderboardCountMessage : Message
	{
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00003FEF File Offset: 0x000021EF
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x00003FF7 File Offset: 0x000021F7
		[JsonProperty]
		public string GameType { get; private set; }

		// Token: 0x060002EA RID: 746 RVA: 0x00004000 File Offset: 0x00002200
		public GetRankedLeaderboardCountMessage()
		{
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00004008 File Offset: 0x00002208
		public GetRankedLeaderboardCountMessage(string gameType)
		{
			this.GameType = gameType;
		}
	}
}
