using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.Source.Missions.Handlers
{
	// Token: 0x020003DB RID: 987
	public class MissionFacialAnimationHandler : MissionLogic
	{
		// Token: 0x060036A0 RID: 13984 RVA: 0x000E23A1 File Offset: 0x000E05A1
		public override void EarlyStart()
		{
			this._animRefreshTimer = new Timer(base.Mission.CurrentTime, 5f, true);
		}

		// Token: 0x060036A1 RID: 13985 RVA: 0x000E23BF File Offset: 0x000E05BF
		public override void AfterStart()
		{
		}

		// Token: 0x060036A2 RID: 13986 RVA: 0x000E23C1 File Offset: 0x000E05C1
		public override void OnMissionTick(float dt)
		{
		}

		// Token: 0x060036A3 RID: 13987 RVA: 0x000E23C4 File Offset: 0x000E05C4
		private void SetDefaultFacialAnimationsForAllAgents()
		{
			foreach (Agent agent in base.Mission.Agents)
			{
				if (agent.IsActive() && agent.IsHuman)
				{
					agent.SetAgentFacialAnimation(Agent.FacialAnimChannel.Low, "idle_tired", true);
				}
			}
		}

		// Token: 0x04001781 RID: 6017
		private Timer _animRefreshTimer;
	}
}
