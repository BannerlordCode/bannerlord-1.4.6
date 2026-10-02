using System;

namespace TaleWorlds.MountAndBlade.Objects.Siege
{
	// Token: 0x020003B1 RID: 945
	public class MultiplayerBallistaSpawner : BallistaSpawner
	{
		// Token: 0x06003545 RID: 13637 RVA: 0x000DB0D0 File Offset: 0x000D92D0
		protected internal override void OnPreInit()
		{
			this._spawnerMissionHelper = new SpawnerEntityMissionHelper(this, false);
		}
	}
}
