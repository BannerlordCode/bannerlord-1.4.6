using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200010D RID: 269
	[Serializable]
	public class ClanStats
	{
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0000734E File Offset: 0x0000554E
		// (set) Token: 0x060005C7 RID: 1479 RVA: 0x00007356 File Offset: 0x00005556
		public int WinCount { get; private set; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x0000735F File Offset: 0x0000555F
		// (set) Token: 0x060005C9 RID: 1481 RVA: 0x00007367 File Offset: 0x00005567
		public int LossCount { get; private set; }

		// Token: 0x060005CA RID: 1482 RVA: 0x00007370 File Offset: 0x00005570
		public ClanStats(int winCount, int lossCount)
		{
			this.WinCount = winCount;
			this.LossCount = lossCount;
		}
	}
}
