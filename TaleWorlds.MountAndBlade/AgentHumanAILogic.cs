using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200026E RID: 622
	public class AgentHumanAILogic : MissionLogic
	{
		// Token: 0x060022E0 RID: 8928 RVA: 0x0007B31C File Offset: 0x0007951C
		public override void OnAgentCreated(Agent agent)
		{
			base.OnAgentCreated(agent);
			if (agent.IsAIControlled && agent.IsHuman)
			{
				agent.AddComponent(new HumanAIComponent(agent));
			}
		}

		// Token: 0x060022E1 RID: 8929 RVA: 0x0007B344 File Offset: 0x00079544
		protected internal override void OnAgentControllerChanged(Agent agent, AgentControllerType oldController)
		{
			base.OnAgentControllerChanged(agent, oldController);
			if (agent.IsHuman)
			{
				if (agent.Controller == AgentControllerType.AI)
				{
					agent.AddComponent(new HumanAIComponent(agent));
					return;
				}
				if (oldController == AgentControllerType.AI && agent.HumanAIComponent != null)
				{
					agent.RemoveComponent(agent.HumanAIComponent);
				}
			}
		}

		// Token: 0x060022E2 RID: 8930 RVA: 0x0007B390 File Offset: 0x00079590
		public override void OnAgentMount(Agent agent)
		{
			base.OnAgentMount(agent);
			Mission.Current.UpdateMountReservationsAfterRiderMounts(agent, agent.MountAgent);
		}
	}
}
