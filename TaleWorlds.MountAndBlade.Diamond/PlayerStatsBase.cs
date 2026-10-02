using System;
using Newtonsoft.Json;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000145 RID: 325
	[JsonConverter(typeof(PlayerStatsBaseJsonConverter))]
	[Serializable]
	public class PlayerStatsBase
	{
		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x0000D1B8 File Offset: 0x0000B3B8
		// (set) Token: 0x060008EF RID: 2287 RVA: 0x0000D1C0 File Offset: 0x0000B3C0
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060008F0 RID: 2288 RVA: 0x0000D1C9 File Offset: 0x0000B3C9
		// (set) Token: 0x060008F1 RID: 2289 RVA: 0x0000D1D1 File Offset: 0x0000B3D1
		[JsonProperty]
		public int KillCount { get; set; }

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060008F2 RID: 2290 RVA: 0x0000D1DA File Offset: 0x0000B3DA
		// (set) Token: 0x060008F3 RID: 2291 RVA: 0x0000D1E2 File Offset: 0x0000B3E2
		[JsonProperty]
		public int DeathCount { get; set; }

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060008F4 RID: 2292 RVA: 0x0000D1EB File Offset: 0x0000B3EB
		// (set) Token: 0x060008F5 RID: 2293 RVA: 0x0000D1F3 File Offset: 0x0000B3F3
		[JsonProperty]
		public int AssistCount { get; set; }

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x0000D1FC File Offset: 0x0000B3FC
		// (set) Token: 0x060008F7 RID: 2295 RVA: 0x0000D204 File Offset: 0x0000B404
		[JsonProperty]
		public int WinCount { get; set; }

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x0000D20D File Offset: 0x0000B40D
		// (set) Token: 0x060008F9 RID: 2297 RVA: 0x0000D215 File Offset: 0x0000B415
		[JsonProperty]
		public int LoseCount { get; set; }

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x0000D21E File Offset: 0x0000B41E
		// (set) Token: 0x060008FB RID: 2299 RVA: 0x0000D226 File Offset: 0x0000B426
		[JsonProperty]
		public int ForfeitCount { get; set; }

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x0000D22F File Offset: 0x0000B42F
		[JsonIgnore]
		public float AverageKillPerDeath
		{
			get
			{
				return (float)this.KillCount / (float)((this.DeathCount != 0) ? this.DeathCount : 1);
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x0000D24B File Offset: 0x0000B44B
		// (set) Token: 0x060008FE RID: 2302 RVA: 0x0000D253 File Offset: 0x0000B453
		[JsonProperty]
		public string GameType { get; set; }

		// Token: 0x06000900 RID: 2304 RVA: 0x0000D264 File Offset: 0x0000B464
		public void FillWith(PlayerId playerId, int killCount, int deathCount, int assistCount, int winCount, int loseCount, int forfeitCount)
		{
			this.PlayerId = playerId;
			this.KillCount = killCount;
			this.DeathCount = deathCount;
			this.AssistCount = assistCount;
			this.WinCount = winCount;
			this.LoseCount = loseCount;
			this.ForfeitCount = forfeitCount;
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0000D29C File Offset: 0x0000B49C
		public virtual void Update(BattlePlayerStatsBase battleStats, bool won)
		{
			this.KillCount += battleStats.Kills;
			this.DeathCount += battleStats.Deaths;
			this.AssistCount += battleStats.Assists;
			int num;
			if (won)
			{
				num = this.WinCount;
				this.WinCount = num + 1;
				return;
			}
			num = this.LoseCount;
			this.LoseCount = num + 1;
		}
	}
}
