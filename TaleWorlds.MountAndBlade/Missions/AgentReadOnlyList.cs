using System;
using System.Collections.Generic;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Missions
{
	// Token: 0x020003E0 RID: 992
	public class AgentReadOnlyList : MBReadOnlyList<Agent>
	{
		// Token: 0x060036B3 RID: 14003 RVA: 0x000E2C41 File Offset: 0x000E0E41
		public AgentReadOnlyList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x060036B4 RID: 14004 RVA: 0x000E2C4A File Offset: 0x000E0E4A
		public AgentReadOnlyList(IEnumerable<Agent> collection)
			: base(collection)
		{
		}

		// Token: 0x060036B5 RID: 14005 RVA: 0x000E2C53 File Offset: 0x000E0E53
		public AgentReadOnlyList(List<Agent> collection)
			: base(collection)
		{
		}
	}
}
