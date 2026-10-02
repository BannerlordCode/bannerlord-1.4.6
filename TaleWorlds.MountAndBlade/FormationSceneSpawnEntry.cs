using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000214 RID: 532
	public struct FormationSceneSpawnEntry
	{
		// Token: 0x06001EFB RID: 7931 RVA: 0x0006BB22 File Offset: 0x00069D22
		public FormationSceneSpawnEntry(FormationClass formationClass, GameEntity spawnEntity, GameEntity reinforcementSpawnEntity)
		{
			this.FormationClass = formationClass;
			this.SpawnEntity = spawnEntity;
			this.ReinforcementSpawnEntity = reinforcementSpawnEntity;
		}

		// Token: 0x04000A9C RID: 2716
		public readonly FormationClass FormationClass;

		// Token: 0x04000A9D RID: 2717
		public readonly GameEntity SpawnEntity;

		// Token: 0x04000A9E RID: 2718
		public readonly GameEntity ReinforcementSpawnEntity;
	}
}
