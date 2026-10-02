using System;
using System.Collections.Generic;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000359 RID: 857
	public class StandingPointWithAgentLimit : StandingPoint
	{
		// Token: 0x06003136 RID: 12598 RVA: 0x000C7E6B File Offset: 0x000C606B
		public void AddValidAgent(Agent agent)
		{
			if (agent != null)
			{
				this._validAgents.Add(agent);
			}
		}

		// Token: 0x06003137 RID: 12599 RVA: 0x000C7E7C File Offset: 0x000C607C
		public void ClearValidAgents()
		{
			this._validAgents.Clear();
		}

		// Token: 0x06003138 RID: 12600 RVA: 0x000C7E89 File Offset: 0x000C6089
		public override bool IsDisabledForAgent(Agent agent)
		{
			return !this._validAgents.Contains(agent) || base.IsDisabledForAgent(agent);
		}

		// Token: 0x040014A4 RID: 5284
		private readonly List<Agent> _validAgents = new List<Agent>();
	}
}
