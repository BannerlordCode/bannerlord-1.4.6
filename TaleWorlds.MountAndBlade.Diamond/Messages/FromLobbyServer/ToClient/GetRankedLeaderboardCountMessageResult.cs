using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000042 RID: 66
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class GetRankedLeaderboardCountMessageResult : FunctionResult
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00002EC2 File Offset: 0x000010C2
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00002ECA File Offset: 0x000010CA
		[JsonProperty]
		public int Count { get; private set; }

		// Token: 0x06000149 RID: 329 RVA: 0x00002ED3 File Offset: 0x000010D3
		public GetRankedLeaderboardCountMessageResult()
		{
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00002EDB File Offset: 0x000010DB
		public GetRankedLeaderboardCountMessageResult(int count)
		{
			this.Count = count;
		}
	}
}
