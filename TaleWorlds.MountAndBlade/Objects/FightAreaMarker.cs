using System;
using System.Collections.Generic;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.Objects
{
	// Token: 0x0200039F RID: 927
	public class FightAreaMarker : AreaMarker
	{
		// Token: 0x060034D7 RID: 13527 RVA: 0x000D96B2 File Offset: 0x000D78B2
		public IEnumerable<Agent> GetAgentsInRange(Team team, bool humanOnly = true)
		{
			foreach (Agent agent in team.ActiveAgents)
			{
				if ((!humanOnly || agent.IsHuman) && base.IsPositionInRange(agent.Position))
				{
					yield return agent;
				}
			}
			List<Agent>.Enumerator enumerator = default(List<Agent>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060034D8 RID: 13528 RVA: 0x000D96D0 File Offset: 0x000D78D0
		public IEnumerable<Agent> GetAgentsInRange(BattleSideEnum side, bool humanOnly = true)
		{
			foreach (Team team in Mission.Current.Teams)
			{
				if (team.Side == side)
				{
					foreach (Agent agent in this.GetAgentsInRange(team, humanOnly))
					{
						yield return agent;
					}
					IEnumerator<Agent> enumerator2 = null;
				}
			}
			List<Team>.Enumerator enumerator = default(List<Team>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0400166B RID: 5739
		public int SubAreaIndex = 1;
	}
}
