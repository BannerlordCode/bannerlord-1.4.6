using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics
{
	// Token: 0x02000082 RID: 130
	public class SandBoxMissionHandler : MissionLogic
	{
		// Token: 0x0600052E RID: 1326 RVA: 0x00022D7D File Offset: 0x00020F7D
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow killingBlow)
		{
			if (affectedAgent != null && affectedAgent.Character != CharacterObject.PlayerCharacter)
			{
				return;
			}
			if (affectedAgent == affectorAgent || affectorAgent == null)
			{
				Campaign.Current.GameMenuManager.SetNextMenu("settlement_player_unconscious");
			}
		}
	}
}
