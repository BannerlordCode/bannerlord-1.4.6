using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000F5 RID: 245
	[Serializable]
	public class BattlePlayerStatsSiege : BattlePlayerStatsBase
	{
		// Token: 0x060004DA RID: 1242 RVA: 0x0000577F File Offset: 0x0000397F
		public BattlePlayerStatsSiege()
		{
			base.GameType = "Siege";
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00005792 File Offset: 0x00003992
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x0000579A File Offset: 0x0000399A
		public int WallsBreached { get; set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x000057A3 File Offset: 0x000039A3
		// (set) Token: 0x060004DE RID: 1246 RVA: 0x000057AB File Offset: 0x000039AB
		public int SiegeEngineKills { get; set; }

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x000057B4 File Offset: 0x000039B4
		// (set) Token: 0x060004E0 RID: 1248 RVA: 0x000057BC File Offset: 0x000039BC
		public int SiegeEnginesDestroyed { get; set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x000057C5 File Offset: 0x000039C5
		// (set) Token: 0x060004E2 RID: 1250 RVA: 0x000057CD File Offset: 0x000039CD
		public int ObjectiveGoldGained { get; set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x000057D6 File Offset: 0x000039D6
		// (set) Token: 0x060004E4 RID: 1252 RVA: 0x000057DE File Offset: 0x000039DE
		public int Score { get; set; }
	}
}
