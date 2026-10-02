using System;
using System.Collections.Generic;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000134 RID: 308
	[Serializable]
	public class MatchmakingWaitTimeRegionStats
	{
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x0000C204 File Offset: 0x0000A404
		// (set) Token: 0x0600085B RID: 2139 RVA: 0x0000C20C File Offset: 0x0000A40C
		public string Region { get; private set; }

		// Token: 0x0600085C RID: 2140 RVA: 0x0000C215 File Offset: 0x0000A415
		public MatchmakingWaitTimeRegionStats(string region)
		{
			this.Region = region;
			this._gameTypeAverageWaitTimes = new Dictionary<string, Dictionary<WaitTimeStatType, int>>();
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0000C230 File Offset: 0x0000A430
		public void SetGameTypeAverage(string gameType, WaitTimeStatType statType, int average)
		{
			Dictionary<WaitTimeStatType, int> dictionary;
			if (!this._gameTypeAverageWaitTimes.TryGetValue(gameType, out dictionary))
			{
				dictionary = new Dictionary<WaitTimeStatType, int>();
				this._gameTypeAverageWaitTimes.Add(gameType, dictionary);
			}
			this._gameTypeAverageWaitTimes[gameType][statType] = average;
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0000C273 File Offset: 0x0000A473
		public bool HasStatsForGameType(string gameType)
		{
			return gameType != null && this._gameTypeAverageWaitTimes.ContainsKey(gameType);
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0000C288 File Offset: 0x0000A488
		public int GetWaitTime(string gameType, WaitTimeStatType statType)
		{
			Dictionary<WaitTimeStatType, int> dictionary;
			int num;
			if (this._gameTypeAverageWaitTimes.TryGetValue(gameType, out dictionary) && dictionary.TryGetValue(statType, out num))
			{
				return num;
			}
			return int.MaxValue;
		}

		// Token: 0x0400035C RID: 860
		private Dictionary<string, Dictionary<WaitTimeStatType, int>> _gameTypeAverageWaitTimes;
	}
}
