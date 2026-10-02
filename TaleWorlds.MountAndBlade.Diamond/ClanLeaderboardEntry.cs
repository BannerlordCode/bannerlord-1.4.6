using System;
using Newtonsoft.Json;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200010A RID: 266
	[Serializable]
	public class ClanLeaderboardEntry
	{
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0000724A File Offset: 0x0000544A
		// (set) Token: 0x060005B1 RID: 1457 RVA: 0x00007252 File Offset: 0x00005452
		public Guid ClanId { get; private set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0000725B File Offset: 0x0000545B
		// (set) Token: 0x060005B3 RID: 1459 RVA: 0x00007263 File Offset: 0x00005463
		public string Name { get; private set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0000726C File Offset: 0x0000546C
		// (set) Token: 0x060005B5 RID: 1461 RVA: 0x00007274 File Offset: 0x00005474
		public string Tag { get; private set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x0000727D File Offset: 0x0000547D
		// (set) Token: 0x060005B7 RID: 1463 RVA: 0x00007285 File Offset: 0x00005485
		public string Sigil { get; private set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x0000728E File Offset: 0x0000548E
		// (set) Token: 0x060005B9 RID: 1465 RVA: 0x00007296 File Offset: 0x00005496
		public int WinCount { get; private set; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x0000729F File Offset: 0x0000549F
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x000072A7 File Offset: 0x000054A7
		public int LossCount { get; private set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x000072B0 File Offset: 0x000054B0
		// (set) Token: 0x060005BD RID: 1469 RVA: 0x000072B8 File Offset: 0x000054B8
		public float Score { get; private set; }

		// Token: 0x060005BE RID: 1470 RVA: 0x000072C1 File Offset: 0x000054C1
		[JsonConstructor]
		public ClanLeaderboardEntry(Guid clanId, string name, string tag, string sigil, int winCount, int lossCount, float score)
		{
			this.ClanId = clanId;
			this.Name = name;
			this.Tag = tag;
			this.Sigil = sigil;
			this.WinCount = winCount;
			this.LossCount = lossCount;
			this.Score = score;
		}
	}
}
