using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000F4 RID: 244
	[Serializable]
	public class BattlePlayerStatsDuel : BattlePlayerStatsBase
	{
		// Token: 0x060004D1 RID: 1233 RVA: 0x00005728 File Offset: 0x00003928
		public BattlePlayerStatsDuel()
		{
			base.GameType = "Duel";
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0000573B File Offset: 0x0000393B
		// (set) Token: 0x060004D3 RID: 1235 RVA: 0x00005743 File Offset: 0x00003943
		public int DuelsWon { get; set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x0000574C File Offset: 0x0000394C
		// (set) Token: 0x060004D5 RID: 1237 RVA: 0x00005754 File Offset: 0x00003954
		public int InfantryWins { get; set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x0000575D File Offset: 0x0000395D
		// (set) Token: 0x060004D7 RID: 1239 RVA: 0x00005765 File Offset: 0x00003965
		public int ArcherWins { get; set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x0000576E File Offset: 0x0000396E
		// (set) Token: 0x060004D9 RID: 1241 RVA: 0x00005776 File Offset: 0x00003976
		public int CavalryWins { get; set; }
	}
}
