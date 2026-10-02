using System;
using Newtonsoft.Json;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200014C RID: 332
	[Serializable]
	public class PlayerStatsSkirmish : PlayerStatsRanked
	{
		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x0000D8F0 File Offset: 0x0000BAF0
		// (set) Token: 0x06000942 RID: 2370 RVA: 0x0000D8F8 File Offset: 0x0000BAF8
		public int MVPs { get; set; }

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x0000D901 File Offset: 0x0000BB01
		// (set) Token: 0x06000944 RID: 2372 RVA: 0x0000D909 File Offset: 0x0000BB09
		public int Score { get; set; }

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x0000D912 File Offset: 0x0000BB12
		[JsonIgnore]
		public int AverageScore
		{
			get
			{
				return this.Score / ((base.WinCount + base.LoseCount != 0) ? (base.WinCount + base.LoseCount) : 1);
			}
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0000D93A File Offset: 0x0000BB3A
		public PlayerStatsSkirmish()
		{
			base.GameType = "Skirmish";
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0000D950 File Offset: 0x0000BB50
		public void FillWith(PlayerId playerId, int killCount, int deathCount, int assistCount, int winCount, int loseCount, int forfeitCount, int rating, int ratingDeviation, string rank, bool evaluating, int evaluationMatchesPlayedCount, int mvps, int score)
		{
			base.FillWith(playerId, killCount, deathCount, assistCount, winCount, loseCount, forfeitCount, rating, ratingDeviation, rank, evaluating, evaluationMatchesPlayedCount);
			this.MVPs = mvps;
			this.Score = score;
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0000D988 File Offset: 0x0000BB88
		public void FillWithNewPlayer(PlayerId playerId, int defaultRating, int defaultRatingDeviation)
		{
			this.FillWith(playerId, 0, 0, 0, 0, 0, 0, defaultRating, defaultRatingDeviation, "", true, 0, 0, 0);
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0000D9AD File Offset: 0x0000BBAD
		public void Update(BattlePlayerStatsSkirmish stats, bool won)
		{
			base.Update(stats, won);
			this.MVPs += stats.MVPs;
			this.Score += stats.Score;
		}
	}
}
