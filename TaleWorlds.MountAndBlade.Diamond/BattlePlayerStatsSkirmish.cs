using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000F6 RID: 246
	[Serializable]
	public class BattlePlayerStatsSkirmish : BattlePlayerStatsBase
	{
		// Token: 0x060004E5 RID: 1253 RVA: 0x000057E7 File Offset: 0x000039E7
		public BattlePlayerStatsSkirmish()
		{
			base.GameType = "Skirmish";
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x000057FA File Offset: 0x000039FA
		// (set) Token: 0x060004E7 RID: 1255 RVA: 0x00005802 File Offset: 0x00003A02
		public int MVPs { get; set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x0000580B File Offset: 0x00003A0B
		// (set) Token: 0x060004E9 RID: 1257 RVA: 0x00005813 File Offset: 0x00003A13
		public int Score { get; set; }
	}
}
