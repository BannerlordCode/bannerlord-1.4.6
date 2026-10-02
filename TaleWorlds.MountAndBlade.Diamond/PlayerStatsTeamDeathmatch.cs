using System;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200014D RID: 333
	[Serializable]
	public class PlayerStatsTeamDeathmatch : PlayerStatsBase
	{
		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x0000D9DD File Offset: 0x0000BBDD
		// (set) Token: 0x0600094B RID: 2379 RVA: 0x0000D9E5 File Offset: 0x0000BBE5
		public int Score { get; set; }

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x0000D9EE File Offset: 0x0000BBEE
		public float AverageScore
		{
			get
			{
				return (float)this.Score / (float)((base.WinCount + base.LoseCount != 0) ? (base.WinCount + base.LoseCount) : 1);
			}
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0000DA18 File Offset: 0x0000BC18
		public PlayerStatsTeamDeathmatch()
		{
			base.GameType = "TeamDeathmatch";
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0000DA2B File Offset: 0x0000BC2B
		public void FillWith(PlayerId playerId, int killCount, int deathCount, int assistCount, int winCount, int loseCount, int forfeitCount, int score)
		{
			base.FillWith(playerId, killCount, deathCount, assistCount, winCount, loseCount, forfeitCount);
			this.Score = score;
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0000DA48 File Offset: 0x0000BC48
		public void FillWithNewPlayer(PlayerId playerId)
		{
			this.FillWith(playerId, 0, 0, 0, 0, 0, 0, 0);
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0000DA63 File Offset: 0x0000BC63
		public void Update(BattlePlayerStatsTeamDeathmatch stats, bool won)
		{
			base.Update(stats, won);
			this.Score += stats.Score;
		}
	}
}
