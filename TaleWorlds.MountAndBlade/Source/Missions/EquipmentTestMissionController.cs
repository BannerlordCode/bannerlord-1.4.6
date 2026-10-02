using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade.Source.Missions
{
	// Token: 0x020003D3 RID: 979
	public class EquipmentTestMissionController : MissionLogic
	{
		// Token: 0x0600365C RID: 13916 RVA: 0x000E10D0 File Offset: 0x000DF2D0
		public override void AfterStart()
		{
			base.AfterStart();
			WeakGameEntity weakGameEntity = base.Mission.Scene.FindWeakEntityWithTag("spawnpoint_player");
			base.Mission.SpawnAgent(new AgentBuildData(Game.Current.PlayerTroop).Team(base.Mission.AttackerTeam).InitialFrameFromSpawnPointEntity(weakGameEntity).CivilianEquipment(false)
				.Controller(AgentControllerType.Player), false);
		}
	}
}
