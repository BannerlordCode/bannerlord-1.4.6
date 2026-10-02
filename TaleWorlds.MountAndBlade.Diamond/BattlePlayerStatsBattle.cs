using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000F2 RID: 242
	[Serializable]
	public class BattlePlayerStatsBattle : BattlePlayerStatsBase
	{
		// Token: 0x060004C5 RID: 1221 RVA: 0x000056AD File Offset: 0x000038AD
		public BattlePlayerStatsBattle()
		{
			base.GameType = "Battle";
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x000056C0 File Offset: 0x000038C0
		// (set) Token: 0x060004C7 RID: 1223 RVA: 0x000056C8 File Offset: 0x000038C8
		public int RoundsWon { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x000056D1 File Offset: 0x000038D1
		// (set) Token: 0x060004C9 RID: 1225 RVA: 0x000056D9 File Offset: 0x000038D9
		public int RoundsLost { get; set; }
	}
}
