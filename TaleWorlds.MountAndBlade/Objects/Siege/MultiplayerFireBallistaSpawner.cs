using System;

namespace TaleWorlds.MountAndBlade.Objects.Siege
{
	// Token: 0x020003B3 RID: 947
	public class MultiplayerFireBallistaSpawner : BallistaSpawner
	{
		// Token: 0x06003549 RID: 13641 RVA: 0x000DB147 File Offset: 0x000D9347
		protected internal override void OnPreInit()
		{
			this._spawnerMissionHelperFire = new SpawnerEntityMissionHelper(this, true);
		}
	}
}
