using System;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000149 RID: 329
	[Serializable]
	public class PlayerStatsDuel : PlayerStatsBase
	{
		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x0000D5BA File Offset: 0x0000B7BA
		// (set) Token: 0x0600091B RID: 2331 RVA: 0x0000D5C2 File Offset: 0x0000B7C2
		public int DuelsWon { get; set; }

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x0000D5CB File Offset: 0x0000B7CB
		// (set) Token: 0x0600091D RID: 2333 RVA: 0x0000D5D3 File Offset: 0x0000B7D3
		public int InfantryWins { get; set; }

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x0000D5DC File Offset: 0x0000B7DC
		// (set) Token: 0x0600091F RID: 2335 RVA: 0x0000D5E4 File Offset: 0x0000B7E4
		public int ArcherWins { get; set; }

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x0000D5ED File Offset: 0x0000B7ED
		// (set) Token: 0x06000921 RID: 2337 RVA: 0x0000D5F5 File Offset: 0x0000B7F5
		public int CavalryWins { get; set; }

		// Token: 0x06000922 RID: 2338 RVA: 0x0000D5FE File Offset: 0x0000B7FE
		public PlayerStatsDuel()
		{
			base.GameType = "Duel";
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0000D611 File Offset: 0x0000B811
		public void FillWith(PlayerId playerId, int killCount, int deathCount, int assistCount, int winCount, int loseCount, int forfeitCount, int duelsWon, int infantryWins, int archerWins, int cavalryWins)
		{
			base.FillWith(playerId, killCount, deathCount, assistCount, winCount, loseCount, forfeitCount);
			this.DuelsWon = duelsWon;
			this.InfantryWins = infantryWins;
			this.ArcherWins = archerWins;
			this.CavalryWins = cavalryWins;
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0000D644 File Offset: 0x0000B844
		public void FillWithNewPlayer(PlayerId playerId)
		{
			this.FillWith(playerId, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0000D664 File Offset: 0x0000B864
		public void Update(BattlePlayerStatsDuel stats, bool won)
		{
			base.Update(stats, won);
			this.DuelsWon += stats.DuelsWon;
			this.InfantryWins += stats.InfantryWins;
			this.ArcherWins += stats.ArcherWins;
			this.CavalryWins += stats.CavalryWins;
		}
	}
}
