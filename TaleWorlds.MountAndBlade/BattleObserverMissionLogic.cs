using System;
using System.Collections.Generic;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200027B RID: 635
	public class BattleObserverMissionLogic : MissionLogic
	{
		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06002359 RID: 9049 RVA: 0x0007DBCB File Offset: 0x0007BDCB
		// (set) Token: 0x0600235A RID: 9050 RVA: 0x0007DBD3 File Offset: 0x0007BDD3
		public IBattleObserver BattleObserver { get; private set; }

		// Token: 0x0600235B RID: 9051 RVA: 0x0007DBDC File Offset: 0x0007BDDC
		public void SetObserver(IBattleObserver observer)
		{
			this.BattleObserver = observer;
			foreach (Agent agent in this._onAgentBuildCache)
			{
				this.BattleObserver.TroopNumberChanged(agent.Team.Side, agent.Origin.BattleCombatant, agent.Character, 1, 0, 0, 0, 0, 0);
				this._builtAgentCountForSides[(int)agent.Team.Side]++;
			}
			this._onAgentBuildCache.Clear();
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x0007DC84 File Offset: 0x0007BE84
		public override void EarlyStart()
		{
			base.EarlyStart();
			this._builtAgentCountForSides = new int[2];
			this._removedAgentCountForSides = new int[2];
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x0007DCA4 File Offset: 0x0007BEA4
		public override void OnAgentBuild(Agent agent, Banner banner)
		{
			if (agent.IsHuman)
			{
				if (this.BattleObserver != null && agent.Team != Team.Invalid)
				{
					BattleSideEnum side = agent.Team.Side;
					this.BattleObserver.TroopNumberChanged(side, agent.Origin.BattleCombatant, agent.Character, 1, 0, 0, 0, 0, 0);
					this._builtAgentCountForSides[(int)side]++;
					return;
				}
				this._onAgentBuildCache.Add(agent);
			}
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x0007DD1C File Offset: 0x0007BF1C
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow blow)
		{
			if (affectedAgent.IsHuman && affectedAgent.Team != Team.Invalid)
			{
				BattleSideEnum side = affectedAgent.Team.Side;
				switch (agentState)
				{
				case AgentState.Routed:
					this.BattleObserver.TroopNumberChanged(side, affectedAgent.Origin.BattleCombatant, affectedAgent.Character, -1, 0, 0, 1, 0, 0);
					break;
				case AgentState.Unconscious:
					this.BattleObserver.TroopNumberChanged(side, affectedAgent.Origin.BattleCombatant, affectedAgent.Character, -1, 0, 1, 0, 0, 0);
					break;
				case AgentState.Killed:
					this.BattleObserver.TroopNumberChanged(side, affectedAgent.Origin.BattleCombatant, affectedAgent.Character, -1, 1, 0, 0, 0, 0);
					break;
				default:
					throw new ArgumentOutOfRangeException("agentState", agentState, null);
				}
				this._removedAgentCountForSides[(int)side]++;
				if (affectorAgent != null && affectorAgent.IsHuman && (agentState == AgentState.Unconscious || agentState == AgentState.Killed))
				{
					this.BattleObserver.TroopNumberChanged(affectorAgent.Team.Side, affectorAgent.Origin.BattleCombatant, affectorAgent.Character, 0, 0, 0, 0, 1, 0);
				}
			}
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x0007DE38 File Offset: 0x0007C038
		public override void OnAgentTeamChanged(Team prevTeam, Team newTeam, Agent agent)
		{
			if (prevTeam == Team.Invalid && agent.IsHuman && newTeam != null && newTeam != Team.Invalid)
			{
				this.BattleObserver.TroopNumberChanged(agent.Team.Side, agent.Origin.BattleCombatant, agent.Character, 1, 0, 0, 0, 0, 0);
				this._builtAgentCountForSides[(int)agent.Team.Side]++;
			}
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x0007DEA8 File Offset: 0x0007C0A8
		public override void OnMissionResultReady(MissionResult missionResult)
		{
			if (missionResult.PlayerVictory)
			{
				this.BattleObserver.BattleResultsReady();
			}
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x0007DEBD File Offset: 0x0007C0BD
		public float GetDeathToBuiltAgentRatioForSide(BattleSideEnum side)
		{
			return (float)this._removedAgentCountForSides[(int)side] / (float)this._builtAgentCountForSides[(int)side];
		}

		// Token: 0x04000D98 RID: 3480
		private int[] _builtAgentCountForSides;

		// Token: 0x04000D99 RID: 3481
		private int[] _removedAgentCountForSides;

		// Token: 0x04000D9A RID: 3482
		private List<Agent> _onAgentBuildCache = new List<Agent>();
	}
}
