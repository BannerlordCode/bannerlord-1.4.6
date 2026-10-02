using System;

namespace TaleWorlds.MountAndBlade.Objects.Siege
{
	// Token: 0x020003B2 RID: 946
	public class MultiplayerBatteringRamSpawner : BatteringRamSpawner
	{
		// Token: 0x06003547 RID: 13639 RVA: 0x000DB0E8 File Offset: 0x000D92E8
		public override void AssignParameters(SpawnerEntityMissionHelper _spawnerMissionHelper)
		{
			base.AssignParameters(_spawnerMissionHelper);
			_spawnerMissionHelper.SpawnedEntity.GetFirstScriptOfType<DestructableComponent>().MaxHitPoint = 12000f;
			BatteringRam firstScriptOfType = _spawnerMissionHelper.SpawnedEntity.GetFirstScriptOfType<BatteringRam>();
			firstScriptOfType.MaxSpeed *= 1f;
			firstScriptOfType.MinSpeed *= 1f;
		}

		// Token: 0x040016B7 RID: 5815
		private const float MaxHitPoint = 12000f;

		// Token: 0x040016B8 RID: 5816
		private const float SpeedMultiplier = 1f;
	}
}
