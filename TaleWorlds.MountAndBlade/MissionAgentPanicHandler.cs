using System;
using System.Collections.Generic;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000287 RID: 647
	public class MissionAgentPanicHandler : MissionLogic
	{
		// Token: 0x0600240A RID: 9226 RVA: 0x00082462 File Offset: 0x00080662
		public MissionAgentPanicHandler()
		{
			this._panickedAgents = new List<Agent>(256);
			this._panickedFormations = new List<Formation>(24);
			this._panickedTeams = new List<Team>(2);
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x00082494 File Offset: 0x00080694
		public override void OnAgentPanicked(Agent agent)
		{
			this._panickedAgents.Add(agent);
			if (agent.Formation != null && agent.Team != null)
			{
				if (!this._panickedFormations.Contains(agent.Formation))
				{
					this._panickedFormations.Add(agent.Formation);
				}
				if (!this._panickedTeams.Contains(agent.Team))
				{
					this._panickedTeams.Add(agent.Team);
				}
			}
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x00082508 File Offset: 0x00080708
		public override void OnPreMissionTick(float dt)
		{
			if (this._panickedAgents.Count > 0)
			{
				foreach (Team team in this._panickedTeams)
				{
					team.UpdateCachedEnemyDataForFleeing();
				}
				foreach (Formation formation in this._panickedFormations)
				{
					formation.OnBatchUnitRemovalStart();
				}
				foreach (Agent agent in this._panickedAgents)
				{
					CommonAIComponent commonAIComponent = agent.CommonAIComponent;
					if (commonAIComponent != null)
					{
						commonAIComponent.Retreat(false);
					}
					Mission.Current.OnAgentFleeing(agent);
				}
				foreach (Formation formation2 in this._panickedFormations)
				{
					formation2.OnBatchUnitRemovalEnd();
				}
				this._panickedAgents.Clear();
				this._panickedFormations.Clear();
				this._panickedTeams.Clear();
			}
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x00082660 File Offset: 0x00080860
		public override void OnRemoveBehavior()
		{
			this._panickedAgents.Clear();
			this._panickedFormations.Clear();
			this._panickedTeams.Clear();
			base.OnRemoveBehavior();
		}

		// Token: 0x04000DE0 RID: 3552
		private readonly List<Agent> _panickedAgents;

		// Token: 0x04000DE1 RID: 3553
		private readonly List<Formation> _panickedFormations;

		// Token: 0x04000DE2 RID: 3554
		private readonly List<Team> _panickedTeams;
	}
}
