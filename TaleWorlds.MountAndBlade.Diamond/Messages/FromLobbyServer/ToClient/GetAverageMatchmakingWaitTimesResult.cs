using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000033 RID: 51
	[Serializable]
	public class GetAverageMatchmakingWaitTimesResult : FunctionResult
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00002C6A File Offset: 0x00000E6A
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00002C72 File Offset: 0x00000E72
		[JsonProperty]
		public MatchmakingWaitTimeStats MatchmakingWaitTimeStats { get; private set; }

		// Token: 0x0600010D RID: 269 RVA: 0x00002C7B File Offset: 0x00000E7B
		public GetAverageMatchmakingWaitTimesResult()
		{
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00002C83 File Offset: 0x00000E83
		public GetAverageMatchmakingWaitTimesResult(MatchmakingWaitTimeStats matchmakingWaitTimeStats)
		{
			this.MatchmakingWaitTimeStats = matchmakingWaitTimeStats;
		}
	}
}
