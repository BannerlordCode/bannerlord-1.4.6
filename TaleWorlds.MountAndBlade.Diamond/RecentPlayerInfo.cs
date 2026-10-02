using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000153 RID: 339
	public class RecentPlayerInfo
	{
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x0000E1B4 File Offset: 0x0000C3B4
		// (set) Token: 0x06000988 RID: 2440 RVA: 0x0000E1BC File Offset: 0x0000C3BC
		public string PlayerId { get; set; }

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x0000E1C5 File Offset: 0x0000C3C5
		// (set) Token: 0x0600098A RID: 2442 RVA: 0x0000E1CD File Offset: 0x0000C3CD
		public string PlayerName { get; set; }

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x0000E1D6 File Offset: 0x0000C3D6
		// (set) Token: 0x0600098C RID: 2444 RVA: 0x0000E1DE File Offset: 0x0000C3DE
		public int ImportanceScore { get; set; }

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x0000E1E7 File Offset: 0x0000C3E7
		// (set) Token: 0x0600098E RID: 2446 RVA: 0x0000E1EF File Offset: 0x0000C3EF
		public DateTime InteractionTime { get; set; }
	}
}
