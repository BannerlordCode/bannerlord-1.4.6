using System;
using System.Collections.Generic;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000132 RID: 306
	[Serializable]
	public class MatchmakingWaitTimeStats
	{
		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x0000C125 File Offset: 0x0000A325
		// (set) Token: 0x06000854 RID: 2132 RVA: 0x0000C12C File Offset: 0x0000A32C
		public static MatchmakingWaitTimeStats Empty { get; private set; } = new MatchmakingWaitTimeStats();

		// Token: 0x06000856 RID: 2134 RVA: 0x0000C140 File Offset: 0x0000A340
		public MatchmakingWaitTimeStats()
		{
			this._regionStats = new List<MatchmakingWaitTimeRegionStats>();
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0000C153 File Offset: 0x0000A353
		public void AddRegionStats(MatchmakingWaitTimeRegionStats regionStats)
		{
			this._regionStats.Add(regionStats);
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0000C164 File Offset: 0x0000A364
		public MatchmakingWaitTimeRegionStats GetRegionStats(string region)
		{
			foreach (MatchmakingWaitTimeRegionStats matchmakingWaitTimeRegionStats in this._regionStats)
			{
				if (matchmakingWaitTimeRegionStats.Region.ToLower() == region.ToLower())
				{
					return matchmakingWaitTimeRegionStats;
				}
			}
			return null;
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0000C1D0 File Offset: 0x0000A3D0
		public int GetWaitTime(string region, string gameType, WaitTimeStatType statType)
		{
			int num = 0;
			if (!string.IsNullOrEmpty(region) && !string.IsNullOrEmpty(gameType))
			{
				MatchmakingWaitTimeRegionStats regionStats = this.GetRegionStats(region);
				if (regionStats != null)
				{
					num = regionStats.GetWaitTime(gameType, statType);
				}
			}
			return num;
		}

		// Token: 0x04000356 RID: 854
		private List<MatchmakingWaitTimeRegionStats> _regionStats;
	}
}
