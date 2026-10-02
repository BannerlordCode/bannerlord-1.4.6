using System;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200014B RID: 331
	[Serializable]
	public class PlayerStatsSiege : PlayerStatsBase
	{
		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x0000D768 File Offset: 0x0000B968
		// (set) Token: 0x06000932 RID: 2354 RVA: 0x0000D770 File Offset: 0x0000B970
		public int WallsBreached { get; set; }

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x0000D779 File Offset: 0x0000B979
		// (set) Token: 0x06000934 RID: 2356 RVA: 0x0000D781 File Offset: 0x0000B981
		public int SiegeEngineKills { get; set; }

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x0000D78A File Offset: 0x0000B98A
		// (set) Token: 0x06000936 RID: 2358 RVA: 0x0000D792 File Offset: 0x0000B992
		public int SiegeEnginesDestroyed { get; set; }

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x0000D79B File Offset: 0x0000B99B
		// (set) Token: 0x06000938 RID: 2360 RVA: 0x0000D7A3 File Offset: 0x0000B9A3
		public int ObjectiveGoldGained { get; set; }

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x0000D7AC File Offset: 0x0000B9AC
		// (set) Token: 0x0600093A RID: 2362 RVA: 0x0000D7B4 File Offset: 0x0000B9B4
		public int Score { get; set; }

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x0000D7BD File Offset: 0x0000B9BD
		public int AverageScore
		{
			get
			{
				return this.Score / ((base.WinCount + base.LoseCount != 0) ? (base.WinCount + base.LoseCount) : 1);
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x0000D7E5 File Offset: 0x0000B9E5
		public int AverageKillCount
		{
			get
			{
				return base.KillCount / ((base.WinCount + base.LoseCount != 0) ? (base.WinCount + base.LoseCount) : 1);
			}
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0000D80D File Offset: 0x0000BA0D
		public PlayerStatsSiege()
		{
			base.GameType = "Siege";
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0000D820 File Offset: 0x0000BA20
		public void FillWith(PlayerId playerId, int killCount, int deathCount, int assistCount, int winCount, int loseCount, int forfeitCount, int wallsBreached, int siegeEngineKills, int siegeEnginesDestroyed, int objectiveGoldGained, int score)
		{
			base.FillWith(playerId, killCount, deathCount, assistCount, winCount, loseCount, forfeitCount);
			this.WallsBreached = wallsBreached;
			this.SiegeEngineKills = siegeEngineKills;
			this.SiegeEnginesDestroyed = siegeEnginesDestroyed;
			this.ObjectiveGoldGained = objectiveGoldGained;
			this.Score = score;
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0000D85C File Offset: 0x0000BA5C
		public void FillWithNewPlayer(PlayerId playerId)
		{
			this.FillWith(playerId, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0000D87C File Offset: 0x0000BA7C
		public void Update(BattlePlayerStatsSiege stats, bool won)
		{
			base.Update(stats, won);
			this.WallsBreached += stats.WallsBreached;
			this.SiegeEngineKills += stats.SiegeEngineKills;
			this.SiegeEnginesDestroyed += stats.SiegeEnginesDestroyed;
			this.ObjectiveGoldGained += stats.ObjectiveGoldGained;
			this.Score += stats.Score;
		}
	}
}
