using System;
using TaleWorlds.MountAndBlade.ComponentInterfaces;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001FE RID: 510
	public class DefaultMissionDifficultyModel : MissionDifficultyModel
	{
		// Token: 0x06001DB3 RID: 7603 RVA: 0x00066070 File Offset: 0x00064270
		public override float GetDamageMultiplierOfCombatDifficulty(Agent victimAgent, Agent attackerAgent = null)
		{
			float num = 1f;
			victimAgent = (victimAgent.IsMount ? victimAgent.RiderAgent : victimAgent);
			if (victimAgent != null)
			{
				if (victimAgent.IsMainAgent)
				{
					num = Mission.Current.DamageToPlayerMultiplier;
				}
				else
				{
					Mission mission = Mission.Current;
					Agent agent = ((mission != null) ? mission.MainAgent : null);
					if (agent != null && victimAgent.IsFriendOf(agent))
					{
						if (attackerAgent != null && attackerAgent == agent)
						{
							num = Mission.Current.DamageFromPlayerToFriendsMultiplier;
						}
						else
						{
							num = Mission.Current.DamageToFriendsMultiplier;
						}
					}
				}
			}
			return num;
		}
	}
}
