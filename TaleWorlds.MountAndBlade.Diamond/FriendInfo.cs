using System;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000116 RID: 278
	[Serializable]
	public class FriendInfo
	{
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x00007E18 File Offset: 0x00006018
		// (set) Token: 0x0600060F RID: 1551 RVA: 0x00007E20 File Offset: 0x00006020
		public PlayerId Id { get; set; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x00007E29 File Offset: 0x00006029
		// (set) Token: 0x06000611 RID: 1553 RVA: 0x00007E31 File Offset: 0x00006031
		public FriendStatus Status { get; set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x00007E3A File Offset: 0x0000603A
		// (set) Token: 0x06000613 RID: 1555 RVA: 0x00007E42 File Offset: 0x00006042
		public string Name { get; set; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x00007E4B File Offset: 0x0000604B
		// (set) Token: 0x06000615 RID: 1557 RVA: 0x00007E53 File Offset: 0x00006053
		public bool IsOnline { get; set; }
	}
}
