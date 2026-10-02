using System;
using System.Linq;
using Newtonsoft.Json;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000131 RID: 305
	[Serializable]
	public class MatchmakingQueueGameTypeStats
	{
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x0000C063 File Offset: 0x0000A263
		// (set) Token: 0x06000849 RID: 2121 RVA: 0x0000C06B File Offset: 0x0000A26B
		[JsonProperty]
		public string[] GameTypes { get; set; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x0000C074 File Offset: 0x0000A274
		// (set) Token: 0x0600084B RID: 2123 RVA: 0x0000C07C File Offset: 0x0000A27C
		[JsonProperty]
		public int Count { get; set; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x0000C085 File Offset: 0x0000A285
		// (set) Token: 0x0600084D RID: 2125 RVA: 0x0000C08D File Offset: 0x0000A28D
		[JsonProperty]
		public int TotalWaitTime { get; set; }

		// Token: 0x0600084E RID: 2126 RVA: 0x0000C096 File Offset: 0x0000A296
		public MatchmakingQueueGameTypeStats()
		{
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0000C09E File Offset: 0x0000A29E
		public MatchmakingQueueGameTypeStats(string[] gameTypes)
		{
			this.GameTypes = gameTypes;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0000C0AD File Offset: 0x0000A2AD
		public bool HasGameType(string gameType)
		{
			return this.GameTypes.Contains(gameType);
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0000C0BC File Offset: 0x0000A2BC
		public bool EqualWith(string[] gameTypes)
		{
			if (this.GameTypes.Length == gameTypes.Length)
			{
				foreach (string text in gameTypes)
				{
					if (!this.HasGameType(text))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0000C0F8 File Offset: 0x0000A2F8
		internal bool HasAnyGameType(string[] gameTypes)
		{
			foreach (string text in gameTypes)
			{
				if (this.HasGameType(text))
				{
					return true;
				}
			}
			return false;
		}
	}
}
