using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200003D RID: 61
	[Serializable]
	public class GetPlayerCountInQueueResult : FunctionResult
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00002DFA File Offset: 0x00000FFA
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00002E02 File Offset: 0x00001002
		[JsonProperty]
		public MatchmakingQueueStats MatchmakingQueueStats { get; private set; }

		// Token: 0x06000135 RID: 309 RVA: 0x00002E0B File Offset: 0x0000100B
		public GetPlayerCountInQueueResult()
		{
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00002E13 File Offset: 0x00001013
		public GetPlayerCountInQueueResult(MatchmakingQueueStats matchmakingQueueStats)
		{
			this.MatchmakingQueueStats = matchmakingQueueStats;
		}
	}
}
