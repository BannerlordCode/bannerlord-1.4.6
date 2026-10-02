using System;
using SandBox.Missions.AgentBehaviors;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace SandBox.AI
{
	// Token: 0x0200010B RID: 267
	public class AgentBehaviorManager : IAgentBehaviorManager
	{
		// Token: 0x06000D52 RID: 3410 RVA: 0x000610F0 File Offset: 0x0005F2F0
		public void AddQuestCharacterBehaviors(IAgent agent)
		{
			BehaviorSets.AddQuestCharacterBehaviors(agent);
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x000610F8 File Offset: 0x0005F2F8
		void IAgentBehaviorManager.AddWandererBehaviors(IAgent agent)
		{
			BehaviorSets.AddWandererBehaviors(agent);
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x00061100 File Offset: 0x0005F300
		void IAgentBehaviorManager.AddOutdoorWandererBehaviors(IAgent agent)
		{
			BehaviorSets.AddOutdoorWandererBehaviors(agent);
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00061108 File Offset: 0x0005F308
		void IAgentBehaviorManager.AddIndoorWandererBehaviors(IAgent agent)
		{
			BehaviorSets.AddIndoorWandererBehaviors(agent);
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x00061110 File Offset: 0x0005F310
		void IAgentBehaviorManager.AddFixedCharacterBehaviors(IAgent agent)
		{
			BehaviorSets.AddFixedCharacterBehaviors(agent);
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x00061118 File Offset: 0x0005F318
		void IAgentBehaviorManager.AddPatrollingThugBehaviors(IAgent agent)
		{
			BehaviorSets.AddPatrollingThugBehaviors(agent);
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x00061120 File Offset: 0x0005F320
		void IAgentBehaviorManager.AddStandGuardBehaviors(IAgent agent)
		{
			BehaviorSets.AddStandGuardBehaviors(agent);
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x00061128 File Offset: 0x0005F328
		void IAgentBehaviorManager.AddFixedGuardBehaviors(IAgent agent)
		{
			BehaviorSets.AddFixedGuardBehaviors(agent);
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x00061130 File Offset: 0x0005F330
		void IAgentBehaviorManager.AddStealthAgentBehaviors(IAgent agent)
		{
			BehaviorSets.StealthAgentBehaviors(agent);
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x00061138 File Offset: 0x0005F338
		void IAgentBehaviorManager.AddPatrollingGuardBehaviors(IAgent agent)
		{
			BehaviorSets.AddPatrollingGuardBehaviors(agent);
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x00061140 File Offset: 0x0005F340
		void IAgentBehaviorManager.AddCompanionBehaviors(IAgent agent)
		{
			BehaviorSets.AddCompanionBehaviors(agent);
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x00061148 File Offset: 0x0005F348
		void IAgentBehaviorManager.AddBodyguardBehaviors(IAgent agent)
		{
			BehaviorSets.AddBodyguardBehaviors(agent);
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x00061150 File Offset: 0x0005F350
		public void AddFirstCompanionBehavior(IAgent agent)
		{
			BehaviorSets.AddFirstCompanionBehavior(agent);
		}
	}
}
