using System;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200014A RID: 330
	[Serializable]
	public class PlayerStatsRanked : PlayerStatsBase
	{
		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x0000D6C5 File Offset: 0x0000B8C5
		// (set) Token: 0x06000927 RID: 2343 RVA: 0x0000D6CD File Offset: 0x0000B8CD
		public int Rating { get; set; }

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x0000D6D6 File Offset: 0x0000B8D6
		// (set) Token: 0x06000929 RID: 2345 RVA: 0x0000D6DE File Offset: 0x0000B8DE
		public string Rank { get; set; }

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x0000D6E7 File Offset: 0x0000B8E7
		// (set) Token: 0x0600092B RID: 2347 RVA: 0x0000D6EF File Offset: 0x0000B8EF
		public bool Evaluating { get; set; }

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x0000D6F8 File Offset: 0x0000B8F8
		// (set) Token: 0x0600092D RID: 2349 RVA: 0x0000D700 File Offset: 0x0000B900
		public int EvaluationMatchesPlayedCount { get; set; }

		// Token: 0x0600092E RID: 2350 RVA: 0x0000D709 File Offset: 0x0000B909
		public void FillWith(PlayerId playerId, int killCount, int deathCount, int assistCount, int winCount, int loseCount, int forfeitCount, int rating, int ratingDeviation, string rank, bool evaluating, int evaluationMatchesPlayedCount)
		{
			base.FillWith(playerId, killCount, deathCount, assistCount, winCount, loseCount, forfeitCount);
			this.Rating = rating;
			this.Rank = rank;
			this.Evaluating = evaluating;
			this.EvaluationMatchesPlayedCount = evaluationMatchesPlayedCount;
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0000D73C File Offset: 0x0000B93C
		public virtual void FillWithNewPlayer(PlayerId playerId, string gameType, int defaultRating, int defaultRatingDeviation)
		{
			this.FillWith(playerId, 0, 0, 0, 0, 0, 0, defaultRating, defaultRatingDeviation, "", true, 0);
		}
	}
}
