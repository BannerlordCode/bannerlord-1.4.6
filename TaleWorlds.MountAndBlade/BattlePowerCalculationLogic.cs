using System;
using System.Collections.Generic;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200027C RID: 636
	public class BattlePowerCalculationLogic : MissionLogic, IBattlePowerCalculationLogic, IMissionBehavior
	{
		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06002363 RID: 9059 RVA: 0x0007DEE5 File Offset: 0x0007C0E5
		// (set) Token: 0x06002364 RID: 9060 RVA: 0x0007DEED File Offset: 0x0007C0ED
		public bool IsTeamPowersCalculated { get; private set; }

		// Token: 0x06002365 RID: 9061 RVA: 0x0007DEF8 File Offset: 0x0007C0F8
		public BattlePowerCalculationLogic()
		{
			this._sidePowerData = new Dictionary<Team, float>[2];
			for (int i = 0; i < 2; i++)
			{
				this._sidePowerData[i] = new Dictionary<Team, float>();
			}
			this.IsTeamPowersCalculated = false;
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x0007DF37 File Offset: 0x0007C137
		public float GetTotalTeamPower(Team team)
		{
			if (!this.IsTeamPowersCalculated)
			{
				this.CalculateTeamPowers();
			}
			return this._sidePowerData[(int)team.Side][team];
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x0007DF5C File Offset: 0x0007C15C
		private void CalculateTeamPowers()
		{
			Mission.TeamCollection teams = base.Mission.Teams;
			foreach (Team team in teams)
			{
				this._sidePowerData[(int)team.Side].Add(team, 0f);
			}
			IMissionAgentSpawnLogic missionBehavior = base.Mission.GetMissionBehavior<IMissionAgentSpawnLogic>();
			for (int i = 0; i < 2; i++)
			{
				BattleSideEnum battleSideEnum = (BattleSideEnum)i;
				IEnumerable<IAgentOriginBase> allTroopsForSide = missionBehavior.GetAllTroopsForSide(battleSideEnum);
				Dictionary<Team, float> dictionary = this._sidePowerData[i];
				bool flag = base.Mission.PlayerTeam != null && base.Mission.PlayerTeam.Side == battleSideEnum;
				foreach (IAgentOriginBase agentOriginBase in allTroopsForSide)
				{
					Team agentTeam = Mission.GetAgentTeam(agentOriginBase, flag);
					BasicCharacterObject troop = agentOriginBase.Troop;
					Dictionary<Team, float> dictionary2 = dictionary;
					Team team2 = agentTeam;
					dictionary2[team2] += troop.GetPower();
				}
			}
			foreach (Team team3 in teams)
			{
				team3.QuerySystem.Expire();
			}
			this.IsTeamPowersCalculated = true;
		}

		// Token: 0x04000D9C RID: 3484
		private Dictionary<Team, float>[] _sidePowerData;
	}
}
