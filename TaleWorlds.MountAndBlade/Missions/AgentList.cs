using System;
using System.Collections.Generic;

namespace TaleWorlds.MountAndBlade.Missions
{
	// Token: 0x020003DF RID: 991
	public class AgentList : AgentReadOnlyList
	{
		// Token: 0x060036B0 RID: 14000 RVA: 0x000E2C26 File Offset: 0x000E0E26
		public AgentList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x060036B1 RID: 14001 RVA: 0x000E2C2F File Offset: 0x000E0E2F
		public AgentList(IEnumerable<Agent> collection)
			: base(collection)
		{
		}

		// Token: 0x060036B2 RID: 14002 RVA: 0x000E2C38 File Offset: 0x000E0E38
		public AgentList(List<Agent> collection)
			: base(collection)
		{
		}
	}
}
