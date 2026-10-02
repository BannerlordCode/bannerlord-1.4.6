using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000043 RID: 67
	[Serializable]
	public class GetRankedLeaderboardMessageResult : FunctionResult
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00002EEA File Offset: 0x000010EA
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00002EF2 File Offset: 0x000010F2
		[JsonProperty]
		public PlayerLeaderboardData[] LeaderboardPlayers { get; private set; }

		// Token: 0x0600014D RID: 333 RVA: 0x00002EFB File Offset: 0x000010FB
		public GetRankedLeaderboardMessageResult()
		{
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00002F03 File Offset: 0x00001103
		public GetRankedLeaderboardMessageResult(PlayerLeaderboardData[] leaderboardPlayers)
		{
			this.LeaderboardPlayers = leaderboardPlayers;
		}
	}
}
