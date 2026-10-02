using System;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000147 RID: 327
	[Serializable]
	public class PlayerStatsBattle : PlayerStatsBase
	{
		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x0000D3ED File Offset: 0x0000B5ED
		// (set) Token: 0x06000908 RID: 2312 RVA: 0x0000D3F5 File Offset: 0x0000B5F5
		public int RoundsWon { get; private set; }

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x0000D3FE File Offset: 0x0000B5FE
		// (set) Token: 0x0600090A RID: 2314 RVA: 0x0000D406 File Offset: 0x0000B606
		public int RoundsLost { get; private set; }

		// Token: 0x0600090B RID: 2315 RVA: 0x0000D40F File Offset: 0x0000B60F
		public PlayerStatsBattle()
		{
			base.GameType = "Battle";
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0000D422 File Offset: 0x0000B622
		public void FillWith(PlayerId playerId, int killCount, int deathCount, int assistCount, int winCount, int loseCount, int forfeitCount, int roundsWon, int roundsLost)
		{
			base.FillWith(playerId, killCount, deathCount, assistCount, winCount, loseCount, forfeitCount);
			this.RoundsWon = roundsWon;
			this.RoundsLost = roundsLost;
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0000D448 File Offset: 0x0000B648
		public void FillWithNewPlayer(PlayerId playerId)
		{
			this.FillWith(playerId, 0, 0, 0, 0, 0, 0, 0, 0);
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0000D464 File Offset: 0x0000B664
		public void Update(BattlePlayerStatsBattle stats, bool won)
		{
			base.Update(stats, won);
			this.RoundsWon += stats.RoundsWon;
			this.RoundsLost += stats.RoundsLost;
		}
	}
}
