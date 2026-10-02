using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200026D RID: 621
	public class AgentCommonAILogic : MissionLogic
	{
		// Token: 0x060022DD RID: 8925 RVA: 0x0007B2A8 File Offset: 0x000794A8
		public override void OnAgentCreated(Agent agent)
		{
			base.OnAgentCreated(agent);
			if (agent.IsAIControlled)
			{
				agent.AddComponent(new CommonAIComponent(agent));
			}
		}

		// Token: 0x060022DE RID: 8926 RVA: 0x0007B2C8 File Offset: 0x000794C8
		protected internal override void OnAgentControllerChanged(Agent agent, AgentControllerType oldController)
		{
			base.OnAgentControllerChanged(agent, oldController);
			if (agent.IsActive())
			{
				if (agent.Controller == AgentControllerType.AI)
				{
					agent.AddComponent(new CommonAIComponent(agent));
					return;
				}
				if (oldController == AgentControllerType.AI && agent.CommonAIComponent != null)
				{
					agent.RemoveComponent(agent.CommonAIComponent);
				}
			}
		}
	}
}
