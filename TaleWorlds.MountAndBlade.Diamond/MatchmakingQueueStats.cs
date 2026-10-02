using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200012F RID: 303
	[Serializable]
	public class MatchmakingQueueStats
	{
		// Token: 0x1700028B RID: 651
		// (get) Token: 0x0600082E RID: 2094 RVA: 0x0000BCB1 File Offset: 0x00009EB1
		// (set) Token: 0x0600082F RID: 2095 RVA: 0x0000BCB8 File Offset: 0x00009EB8
		public static MatchmakingQueueStats Empty { get; private set; } = new MatchmakingQueueStats();

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x0000BCC0 File Offset: 0x00009EC0
		[JsonIgnore]
		public int TotalCount
		{
			get
			{
				int num = 0;
				foreach (MatchmakingQueueRegionStats matchmakingQueueRegionStats in this.RegionStats)
				{
					num += matchmakingQueueRegionStats.TotalCount;
				}
				return num;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x0000BD18 File Offset: 0x00009F18
		[JsonIgnore]
		public int AverageWaitTime
		{
			get
			{
				int num = 0;
				int num2 = 0;
				if (this.RegionStats.Count > 0)
				{
					foreach (MatchmakingQueueRegionStats matchmakingQueueRegionStats in this.RegionStats)
					{
						num2 += matchmakingQueueRegionStats.AverageWaitTime;
					}
					num = num2 / this.RegionStats.Count;
				}
				return num;
			}
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0000BD9C File Offset: 0x00009F9C
		public MatchmakingQueueStats()
		{
			this.RegionStats = new List<MatchmakingQueueRegionStats>();
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0000BDAF File Offset: 0x00009FAF
		public void AddRegionStats(MatchmakingQueueRegionStats matchmakingQueueRegionStats)
		{
			this.RegionStats.Add(matchmakingQueueRegionStats);
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0000BDC0 File Offset: 0x00009FC0
		public MatchmakingQueueRegionStats GetRegionStats(string region)
		{
			foreach (MatchmakingQueueRegionStats matchmakingQueueRegionStats in this.RegionStats)
			{
				if (matchmakingQueueRegionStats.Region.ToLower() == region.ToLower())
				{
					return matchmakingQueueRegionStats;
				}
			}
			return null;
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0000BE2C File Offset: 0x0000A02C
		public int GetQueueCountOf(string region, string[] gameTypes)
		{
			int num = 0;
			if (!string.IsNullOrEmpty(region) && gameTypes != null)
			{
				MatchmakingQueueRegionStats regionStats = this.GetRegionStats(region);
				if (regionStats != null)
				{
					num = regionStats.GetQueueCountOf(gameTypes);
				}
			}
			return num;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0000BE5C File Offset: 0x0000A05C
		public string[] GetRegionNames()
		{
			string[] array = new string[this.RegionStats.Count];
			for (int i = 0; i < this.RegionStats.Count; i++)
			{
				array[i] = this.RegionStats[i].Region;
			}
			return array;
		}

		// Token: 0x0400034B RID: 843
		[JsonProperty]
		public List<MatchmakingQueueRegionStats> RegionStats;
	}
}
