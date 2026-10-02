using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000130 RID: 304
	[Serializable]
	public class MatchmakingQueueRegionStats
	{
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x0000BEA5 File Offset: 0x0000A0A5
		// (set) Token: 0x06000839 RID: 2105 RVA: 0x0000BEAD File Offset: 0x0000A0AD
		[JsonProperty]
		public string Region { get; set; }

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x0000BEB8 File Offset: 0x0000A0B8
		[JsonIgnore]
		public int TotalCount
		{
			get
			{
				int num = 0;
				foreach (MatchmakingQueueGameTypeStats matchmakingQueueGameTypeStats in this.GameTypeStats)
				{
					num += matchmakingQueueGameTypeStats.Count;
				}
				return num;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x0000BF10 File Offset: 0x0000A110
		// (set) Token: 0x0600083C RID: 2108 RVA: 0x0000BF18 File Offset: 0x0000A118
		[JsonProperty]
		public int MaxWaitTime { get; set; }

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x0000BF21 File Offset: 0x0000A121
		// (set) Token: 0x0600083E RID: 2110 RVA: 0x0000BF29 File Offset: 0x0000A129
		[JsonProperty]
		public int MinWaitTime { get; set; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x0000BF32 File Offset: 0x0000A132
		// (set) Token: 0x06000840 RID: 2112 RVA: 0x0000BF3A File Offset: 0x0000A13A
		[JsonProperty]
		public int MedianWaitTime { get; set; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x0000BF43 File Offset: 0x0000A143
		// (set) Token: 0x06000842 RID: 2114 RVA: 0x0000BF4B File Offset: 0x0000A14B
		[JsonProperty]
		public int AverageWaitTime { get; set; }

		// Token: 0x06000843 RID: 2115 RVA: 0x0000BF54 File Offset: 0x0000A154
		public MatchmakingQueueRegionStats(string region)
		{
			this.Region = region;
			this.GameTypeStats = new List<MatchmakingQueueGameTypeStats>();
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0000BF70 File Offset: 0x0000A170
		public MatchmakingQueueGameTypeStats GetQueueCountObjectOf(string[] gameTypes)
		{
			if (gameTypes != null)
			{
				foreach (MatchmakingQueueGameTypeStats matchmakingQueueGameTypeStats in this.GameTypeStats)
				{
					if (matchmakingQueueGameTypeStats.EqualWith(gameTypes))
					{
						return matchmakingQueueGameTypeStats;
					}
				}
			}
			return null;
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0000BFD0 File Offset: 0x0000A1D0
		public void AddStats(MatchmakingQueueGameTypeStats matchmakingQueueGameTypeStats)
		{
			this.GameTypeStats.Add(matchmakingQueueGameTypeStats);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0000BFE0 File Offset: 0x0000A1E0
		public int GetQueueCountOf(string[] gameTypes)
		{
			int num = 0;
			if (gameTypes != null)
			{
				foreach (MatchmakingQueueGameTypeStats matchmakingQueueGameTypeStats in this.GameTypeStats)
				{
					if (matchmakingQueueGameTypeStats.HasAnyGameType(gameTypes))
					{
						num += matchmakingQueueGameTypeStats.Count;
					}
				}
			}
			return num;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0000C044 File Offset: 0x0000A244
		public void SetWaitTimeStats(int averageWaitTime, int maxWaitTime, int minWaitTime, int medianWaitTime)
		{
			this.AverageWaitTime = averageWaitTime;
			this.MaxWaitTime = maxWaitTime;
			this.MinWaitTime = minWaitTime;
			this.MedianWaitTime = medianWaitTime;
		}

		// Token: 0x0400034D RID: 845
		[JsonProperty]
		public List<MatchmakingQueueGameTypeStats> GameTypeStats;
	}
}
