using System;
using Newtonsoft.Json;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000148 RID: 328
	[Serializable]
	public class PlayerStatsCaptain : PlayerStatsRanked
	{
		// Token: 0x170002DD RID: 733
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x0000D494 File Offset: 0x0000B694
		// (set) Token: 0x06000910 RID: 2320 RVA: 0x0000D49C File Offset: 0x0000B69C
		public int CaptainsKilled { get; set; }

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x0000D4A5 File Offset: 0x0000B6A5
		// (set) Token: 0x06000912 RID: 2322 RVA: 0x0000D4AD File Offset: 0x0000B6AD
		public int MVPs { get; set; }

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x0000D4B6 File Offset: 0x0000B6B6
		// (set) Token: 0x06000914 RID: 2324 RVA: 0x0000D4BE File Offset: 0x0000B6BE
		public int Score { get; set; }

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x0000D4C7 File Offset: 0x0000B6C7
		[JsonIgnore]
		public int AverageScore
		{
			get
			{
				if (this.Score / (base.WinCount + base.LoseCount) == 0)
				{
					return 1;
				}
				return base.WinCount + base.LoseCount;
			}
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0000D4EE File Offset: 0x0000B6EE
		public PlayerStatsCaptain()
		{
			base.GameType = "Captain";
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0000D504 File Offset: 0x0000B704
		public void FillWith(PlayerId playerId, int killCount, int deathCount, int assistCount, int winCount, int loseCount, int forfeitCount, int rating, int ratingDeviation, string rank, bool evaluating, int evaluationMatchesPlayedCount, int captainsKilled, int mvps, int score)
		{
			base.FillWith(playerId, killCount, deathCount, assistCount, winCount, loseCount, forfeitCount, rating, ratingDeviation, rank, evaluating, evaluationMatchesPlayedCount);
			this.CaptainsKilled = captainsKilled;
			this.MVPs = mvps;
			this.Score = score;
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0000D544 File Offset: 0x0000B744
		public void FillWithNewPlayer(PlayerId playerId, int defaultRating, int defaultRatingDeviation)
		{
			this.FillWith(playerId, 0, 0, 0, 0, 0, 0, defaultRating, defaultRatingDeviation, "", true, 0, 0, 0, 0);
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0000D56C File Offset: 0x0000B76C
		public void Update(BattlePlayerStatsCaptain stats, bool won)
		{
			base.Update(stats, won);
			this.CaptainsKilled += stats.CaptainsKilled;
			this.MVPs += stats.MVPs;
			this.Score += stats.Score;
		}
	}
}
