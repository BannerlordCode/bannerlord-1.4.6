using System;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002E6 RID: 742
	public class CommunityGameJoinData
	{
		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06002AD2 RID: 10962 RVA: 0x000A4943 File Offset: 0x000A2B43
		// (set) Token: 0x06002AD3 RID: 10963 RVA: 0x000A494B File Offset: 0x000A2B4B
		public string Name { get; set; }

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06002AD4 RID: 10964 RVA: 0x000A4954 File Offset: 0x000A2B54
		// (set) Token: 0x06002AD5 RID: 10965 RVA: 0x000A495C File Offset: 0x000A2B5C
		public PlayerId PlayerId { get; set; }
	}
}
