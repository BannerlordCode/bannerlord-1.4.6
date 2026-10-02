using System;

namespace TaleWorlds.MountAndBlade.Diamond.Ranked
{
	// Token: 0x0200015D RID: 349
	[Serializable]
	public class RankBarInfo
	{
		// Token: 0x17000318 RID: 792
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x0000EEBE File Offset: 0x0000D0BE
		// (set) Token: 0x060009AF RID: 2479 RVA: 0x0000EEC6 File Offset: 0x0000D0C6
		public string RankId { get; set; }

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x0000EECF File Offset: 0x0000D0CF
		// (set) Token: 0x060009B1 RID: 2481 RVA: 0x0000EED7 File Offset: 0x0000D0D7
		public string PreviousRankId { get; set; }

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x0000EEE0 File Offset: 0x0000D0E0
		// (set) Token: 0x060009B3 RID: 2483 RVA: 0x0000EEE8 File Offset: 0x0000D0E8
		public string NextRankId { get; set; }

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060009B4 RID: 2484 RVA: 0x0000EEF1 File Offset: 0x0000D0F1
		// (set) Token: 0x060009B5 RID: 2485 RVA: 0x0000EEF9 File Offset: 0x0000D0F9
		public float ProgressPercentage { get; set; }

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x0000EF02 File Offset: 0x0000D102
		// (set) Token: 0x060009B7 RID: 2487 RVA: 0x0000EF0A File Offset: 0x0000D10A
		public int Rating { get; set; }

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x0000EF13 File Offset: 0x0000D113
		// (set) Token: 0x060009B9 RID: 2489 RVA: 0x0000EF1B File Offset: 0x0000D11B
		public int RatingToNextRank { get; set; }

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x0000EF24 File Offset: 0x0000D124
		// (set) Token: 0x060009BB RID: 2491 RVA: 0x0000EF2C File Offset: 0x0000D12C
		public bool IsEvaluating { get; set; }

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x0000EF35 File Offset: 0x0000D135
		// (set) Token: 0x060009BD RID: 2493 RVA: 0x0000EF3D File Offset: 0x0000D13D
		public int EvaluationMatchesPlayed { get; set; }

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x0000EF46 File Offset: 0x0000D146
		// (set) Token: 0x060009BF RID: 2495 RVA: 0x0000EF4E File Offset: 0x0000D14E
		public int TotalEvaluationMatchesRequired { get; set; }

		// Token: 0x060009C0 RID: 2496 RVA: 0x0000EF57 File Offset: 0x0000D157
		public RankBarInfo()
		{
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0000EF60 File Offset: 0x0000D160
		public RankBarInfo(string rankId, string previousRankId, string nextRankId, float progressPercentage, int rating, int ratingToNextRank, bool isEvaluating, int evaluationMatchesPlayed, int totalEvaluationMatchesRequired)
		{
			this.RankId = rankId;
			this.PreviousRankId = previousRankId;
			this.NextRankId = nextRankId;
			this.ProgressPercentage = progressPercentage;
			this.Rating = rating;
			this.RatingToNextRank = ratingToNextRank;
			this.IsEvaluating = isEvaluating;
			this.EvaluationMatchesPlayed = evaluationMatchesPlayed;
			this.TotalEvaluationMatchesRequired = totalEvaluationMatchesRequired;
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0000EFB8 File Offset: 0x0000D1B8
		public static RankBarInfo CreateBarInfo(string rankId, string previousRankId, string nextRankId, float progressPercentage, int rating, int ratingToNextRank)
		{
			return new RankBarInfo(rankId, previousRankId, nextRankId, progressPercentage, rating, ratingToNextRank, false, 0, 0);
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0000EFD8 File Offset: 0x0000D1D8
		public static RankBarInfo CreateUnrankedInfo(int matchesPlayed, int totalMatchesRequired)
		{
			return new RankBarInfo("", "", "", 0f, 0, 0, true, matchesPlayed, totalMatchesRequired);
		}
	}
}
