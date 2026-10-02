using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000A6 RID: 166
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class GetRankedLeaderboardMessage : Message
	{
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00004017 File Offset: 0x00002217
		// (set) Token: 0x060002ED RID: 749 RVA: 0x0000401F File Offset: 0x0000221F
		[JsonProperty]
		public string GameType { get; private set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00004028 File Offset: 0x00002228
		// (set) Token: 0x060002EF RID: 751 RVA: 0x00004030 File Offset: 0x00002230
		[JsonProperty]
		public int StartIndex { get; private set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x00004039 File Offset: 0x00002239
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x00004041 File Offset: 0x00002241
		[JsonProperty]
		public int Count { get; private set; }

		// Token: 0x060002F2 RID: 754 RVA: 0x0000404A File Offset: 0x0000224A
		public GetRankedLeaderboardMessage()
		{
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00004052 File Offset: 0x00002252
		public GetRankedLeaderboardMessage(string gameType, int startIndex, int count)
		{
			this.GameType = gameType;
			this.StartIndex = startIndex;
			this.Count = count;
		}
	}
}
