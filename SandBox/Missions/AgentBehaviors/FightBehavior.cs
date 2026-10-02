using System;
using SandBox.Missions.MissionLogics;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.AgentBehaviors
{
	// Token: 0x020000A9 RID: 169
	public class FightBehavior : AgentBehavior
	{
		// Token: 0x06000718 RID: 1816 RVA: 0x0003004F File Offset: 0x0002E24F
		public FightBehavior(AgentBehaviorGroup behaviorGroup)
			: base(behaviorGroup)
		{
			if (base.OwnerAgent.HumanAIComponent == null)
			{
				base.OwnerAgent.AddComponent(new HumanAIComponent(base.OwnerAgent));
			}
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0003007B File Offset: 0x0002E27B
		public override float GetAvailability(bool isSimulation)
		{
			if (!MissionFightHandler.IsAgentAggressive(base.OwnerAgent))
			{
				return 0.1f;
			}
			return 1f;
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00030098 File Offset: 0x0002E298
		protected override void OnActivate()
		{
			TextObject textObject = new TextObject("{=!}{p0} {p1} activate alarmed behavior group.", null);
			textObject.SetTextVariable("p0", base.OwnerAgent.Name.ToString());
			textObject.SetTextVariable("p1", base.OwnerAgent.Index.ToString());
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x000300EC File Offset: 0x0002E2EC
		protected override void OnDeactivate()
		{
			TextObject textObject = new TextObject("{=!}{p0} {p1} deactivate fight behavior.", null);
			textObject.SetTextVariable("p0", base.OwnerAgent.Name.ToString());
			textObject.SetTextVariable("p1", base.OwnerAgent.Index.ToString());
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0003013E File Offset: 0x0002E33E
		public override string GetDebugInfo()
		{
			return "Fight";
		}
	}
}
