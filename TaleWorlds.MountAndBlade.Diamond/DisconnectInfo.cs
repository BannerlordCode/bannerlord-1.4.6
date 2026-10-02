using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000112 RID: 274
	public class DisconnectInfo
	{
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x00007DD7 File Offset: 0x00005FD7
		// (set) Token: 0x06000609 RID: 1545 RVA: 0x00007DDF File Offset: 0x00005FDF
		public DisconnectType Type { get; set; }

		// Token: 0x0600060A RID: 1546 RVA: 0x00007DE8 File Offset: 0x00005FE8
		public DisconnectInfo()
		{
			this.Type = DisconnectType.Unknown;
		}
	}
}
