using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade.ComponentInterfaces;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001F8 RID: 504
	public class CustomBattleSpawnModel : BattleSpawnModel
	{
		// Token: 0x06001D99 RID: 7577 RVA: 0x0006573F File Offset: 0x0006393F
		public override void OnMissionStart()
		{
			MissionReinforcementsHelper.OnMissionStart();
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x00065746 File Offset: 0x00063946
		public override void OnMissionEnd()
		{
			MissionReinforcementsHelper.OnMissionEnd();
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x00065750 File Offset: 0x00063950
		[return: TupleElementNames(new string[] { "origin", "formationIndex" })]
		public override List<ValueTuple<IAgentOriginBase, int>> GetInitialSpawnAssignments(BattleSideEnum battleSide, List<IAgentOriginBase> troopOrigins)
		{
			List<ValueTuple<IAgentOriginBase, int>> list = new List<ValueTuple<IAgentOriginBase, int>>();
			foreach (IAgentOriginBase agentOriginBase in troopOrigins)
			{
				ValueTuple<IAgentOriginBase, int> valueTuple = new ValueTuple<IAgentOriginBase, int>(agentOriginBase, (int)Mission.Current.GetAgentTroopClass(battleSide, agentOriginBase.Troop));
				list.Add(valueTuple);
			}
			return list;
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x000657C0 File Offset: 0x000639C0
		[return: TupleElementNames(new string[] { "origin", "formationIndex" })]
		public override List<ValueTuple<IAgentOriginBase, int>> GetReinforcementAssignments(BattleSideEnum battleSide, List<IAgentOriginBase> troopOrigins)
		{
			return MissionReinforcementsHelper.GetReinforcementAssignments(battleSide, troopOrigins);
		}
	}
}
