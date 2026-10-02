using System;

namespace TaleWorlds.MountAndBlade.MissionSpawnHandlers
{
	// Token: 0x020003C8 RID: 968
	public class CustomMissionSpawnHandler : MissionLogic
	{
		// Token: 0x06003621 RID: 13857 RVA: 0x000DF6D5 File Offset: 0x000DD8D5
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._missionAgentSpawnLogic = base.Mission.GetMissionBehavior<DefaultBattleMissionAgentSpawnLogic>();
		}

		// Token: 0x06003622 RID: 13858 RVA: 0x000DF6F0 File Offset: 0x000DD8F0
		protected static MissionSpawnSettings CreateCustomBattleWaveSpawnSettings()
		{
			return new MissionSpawnSettings(MissionSpawnSettings.InitialSpawnMethod.BattleSizeAllocating, MissionSpawnSettings.ReinforcementTimingMethod.GlobalTimer, MissionSpawnSettings.ReinforcementSpawnMethod.Wave, 3f, 0f, 0f, 0.5f, 0, 0f, 0f, 1f, 0.75f);
		}

		// Token: 0x04001735 RID: 5941
		protected DefaultBattleMissionAgentSpawnLogic _missionAgentSpawnLogic;
	}
}
