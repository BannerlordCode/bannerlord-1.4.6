using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.AgentBehaviors
{
	// Token: 0x020000A4 RID: 164
	public class BehaviorSets
	{
		// Token: 0x060006E1 RID: 1761 RVA: 0x0002E944 File Offset: 0x0002CB44
		private static void AddBehaviorGroups(IAgent agent)
		{
			AgentNavigator agentNavigator = ((Agent)agent).GetComponent<CampaignAgentComponent>().AgentNavigator;
			agentNavigator.AddBehaviorGroup<DailyBehaviorGroup>();
			agentNavigator.AddBehaviorGroup<InterruptingBehaviorGroup>();
			agentNavigator.AddBehaviorGroup<AlarmedBehaviorGroup>();
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0002E96A File Offset: 0x0002CB6A
		public static void AddQuestCharacterBehaviors(IAgent agent)
		{
			BehaviorSets.AddBehaviorGroups(agent);
			AgentNavigator agentNavigator = ((Agent)agent).GetComponent<CampaignAgentComponent>().AgentNavigator;
			agentNavigator.GetBehaviorGroup<DailyBehaviorGroup>().AddBehavior<WalkingBehavior>();
			AlarmedBehaviorGroup behaviorGroup = agentNavigator.GetBehaviorGroup<AlarmedBehaviorGroup>();
			behaviorGroup.AddBehavior<FleeBehavior>();
			behaviorGroup.AddBehavior<FightBehavior>();
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0002E9A0 File Offset: 0x0002CBA0
		public static void AddWandererBehaviors(IAgent agent)
		{
			BehaviorSets.AddBehaviorGroups(agent);
			AgentNavigator agentNavigator = ((Agent)agent).GetComponent<CampaignAgentComponent>().AgentNavigator;
			agentNavigator.GetBehaviorGroup<DailyBehaviorGroup>().AddBehavior<WalkingBehavior>();
			AlarmedBehaviorGroup behaviorGroup = agentNavigator.GetBehaviorGroup<AlarmedBehaviorGroup>();
			behaviorGroup.AddBehavior<FleeBehavior>();
			behaviorGroup.AddBehavior<FightBehavior>();
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0002E9D8 File Offset: 0x0002CBD8
		public static void AddOutdoorWandererBehaviors(IAgent agent)
		{
			BehaviorSets.AddBehaviorGroups(agent);
			AgentNavigator agentNavigator = ((Agent)agent).GetComponent<CampaignAgentComponent>().AgentNavigator;
			DailyBehaviorGroup behaviorGroup = agentNavigator.GetBehaviorGroup<DailyBehaviorGroup>();
			behaviorGroup.AddBehavior<WalkingBehavior>().SetIndoorWandering(false);
			behaviorGroup.AddBehavior<ChangeLocationBehavior>();
			AlarmedBehaviorGroup behaviorGroup2 = agentNavigator.GetBehaviorGroup<AlarmedBehaviorGroup>();
			behaviorGroup2.AddBehavior<FleeBehavior>();
			behaviorGroup2.AddBehavior<FightBehavior>();
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0002EA25 File Offset: 0x0002CC25
		public static void AddIndoorWandererBehaviors(IAgent agent)
		{
			BehaviorSets.AddBehaviorGroups(agent);
			AgentNavigator agentNavigator = ((Agent)agent).GetComponent<CampaignAgentComponent>().AgentNavigator;
			agentNavigator.GetBehaviorGroup<DailyBehaviorGroup>().AddBehavior<WalkingBehavior>().SetOutdoorWandering(false);
			AlarmedBehaviorGroup behaviorGroup = agentNavigator.GetBehaviorGroup<AlarmedBehaviorGroup>();
			behaviorGroup.AddBehavior<FleeBehavior>();
			behaviorGroup.AddBehavior<FightBehavior>();
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0002EA60 File Offset: 0x0002CC60
		public static void AddFixedCharacterBehaviors(IAgent agent)
		{
			BehaviorSets.AddBehaviorGroups(agent);
			AgentNavigator agentNavigator = ((Agent)agent).GetComponent<CampaignAgentComponent>().AgentNavigator;
			WalkingBehavior walkingBehavior = agentNavigator.GetBehaviorGroup<DailyBehaviorGroup>().AddBehavior<WalkingBehavior>();
			walkingBehavior.SetIndoorWandering(false);
			walkingBehavior.SetOutdoorWandering(false);
			AlarmedBehaviorGroup behaviorGroup = agentNavigator.GetBehaviorGroup<AlarmedBehaviorGroup>();
			behaviorGroup.AddBehavior<FleeBehavior>();
			behaviorGroup.AddBehavior<FightBehavior>();
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0002EAAD File Offset: 0x0002CCAD
		public static void AddPatrollingThugBehaviors(IAgent agent)
		{
			BehaviorSets.AddBehaviorGroups(agent);
			AgentNavigator agentNavigator = ((Agent)agent).GetComponent<CampaignAgentComponent>().AgentNavigator;
			agentNavigator.GetBehaviorGroup<DailyBehaviorGroup>().AddBehavior<PatrolAgentBehavior>();
			AlarmedBehaviorGroup behaviorGroup = agentNavigator.GetBehaviorGroup<AlarmedBehaviorGroup>();
			behaviorGroup.AddBehavior<FleeBehavior>();
			behaviorGroup.AddBehavior<FightBehavior>();
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0002EAE3 File Offset: 0x0002CCE3
		public static void AddStandGuardBehaviors(IAgent agent)
		{
			BehaviorSets.AddBehaviorGroups(agent);
			AgentNavigator agentNavigator = ((Agent)agent).GetComponent<CampaignAgentComponent>().AgentNavigator;
			agentNavigator.GetBehaviorGroup<DailyBehaviorGroup>().AddBehavior<StandGuardBehavior>();
			AlarmedBehaviorGroup behaviorGroup = agentNavigator.GetBehaviorGroup<AlarmedBehaviorGroup>();
			behaviorGroup.AddBehavior<FightBehavior>();
			behaviorGroup.DisableCalmDown = true;
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0002EB19 File Offset: 0x0002CD19
		public static void AddFixedGuardBehaviors(IAgent agent)
		{
			BehaviorSets.AddBehaviorGroups(agent);
			((Agent)agent).GetComponent<CampaignAgentComponent>().AgentNavigator.GetBehaviorGroup<AlarmedBehaviorGroup>().AddBehavior<FightBehavior>();
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0002EB3C File Offset: 0x0002CD3C
		public static void StealthAgentBehaviors(IAgent agent)
		{
			BehaviorSets.AddBehaviorGroups(agent);
			AgentNavigator agentNavigator = ((Agent)agent).GetComponent<CampaignAgentComponent>().AgentNavigator;
			agentNavigator.AddBehaviorGroup<DailyBehaviorGroup>();
			agentNavigator.AddBehaviorGroup<AlarmedBehaviorGroup>();
			AlarmedBehaviorGroup behaviorGroup = agentNavigator.GetBehaviorGroup<AlarmedBehaviorGroup>();
			behaviorGroup.AddBehavior<CautiousBehavior>();
			behaviorGroup.AddBehavior<FightBehavior>();
			if (agent.Character.StringId == "disguise_officer_character")
			{
				behaviorGroup.SetCanMoveWhenCautious(false);
			}
			agentNavigator.GetBehaviorGroup<DailyBehaviorGroup>().AddBehavior<PatrolAgentBehavior>();
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0002EBAB File Offset: 0x0002CDAB
		public static void AddPatrollingGuardBehaviors(IAgent agent)
		{
			BehaviorSets.AddBehaviorGroups(agent);
			AgentNavigator agentNavigator = ((Agent)agent).GetComponent<CampaignAgentComponent>().AgentNavigator;
			agentNavigator.GetBehaviorGroup<DailyBehaviorGroup>().AddBehavior<PatrollingGuardBehavior>();
			AlarmedBehaviorGroup behaviorGroup = agentNavigator.GetBehaviorGroup<AlarmedBehaviorGroup>();
			behaviorGroup.AddBehavior<FightBehavior>();
			behaviorGroup.DisableCalmDown = true;
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0002EBE1 File Offset: 0x0002CDE1
		public static void AddCompanionBehaviors(IAgent agent)
		{
			BehaviorSets.AddBehaviorGroups(agent);
			AgentNavigator agentNavigator = ((Agent)agent).GetComponent<CampaignAgentComponent>().AgentNavigator;
			agentNavigator.GetBehaviorGroup<DailyBehaviorGroup>().AddBehavior<WalkingBehavior>().SetIndoorWandering(false);
			agentNavigator.GetBehaviorGroup<AlarmedBehaviorGroup>().AddBehavior<FightBehavior>();
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0002EC15 File Offset: 0x0002CE15
		public static void AddBodyguardBehaviors(IAgent agent)
		{
			BehaviorSets.AddBehaviorGroups(agent);
			AgentNavigator agentNavigator = ((Agent)agent).GetComponent<CampaignAgentComponent>().AgentNavigator;
			DailyBehaviorGroup behaviorGroup = agentNavigator.GetBehaviorGroup<DailyBehaviorGroup>();
			behaviorGroup.AddBehavior<WalkingBehavior>();
			behaviorGroup.AddBehavior<FollowAgentBehavior>().SetTargetAgent(Agent.Main);
			agentNavigator.GetBehaviorGroup<AlarmedBehaviorGroup>().AddBehavior<FightBehavior>();
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0002EC54 File Offset: 0x0002CE54
		public static void AddFirstCompanionBehavior(IAgent agent)
		{
			BehaviorSets.AddBehaviorGroups(agent);
			AgentNavigator agentNavigator = ((Agent)agent).GetComponent<CampaignAgentComponent>().AgentNavigator;
			agentNavigator.GetBehaviorGroup<DailyBehaviorGroup>();
			agentNavigator.GetBehaviorGroup<AlarmedBehaviorGroup>().AddBehavior<FightBehavior>();
		}
	}
}
