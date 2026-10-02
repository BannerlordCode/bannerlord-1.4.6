using System;

namespace TaleWorlds.MountAndBlade.Objects.Siege
{
	// Token: 0x020003B6 RID: 950
	public class MultiplayerMangonelSpawner : MangonelSpawner
	{
		// Token: 0x0600354F RID: 13647 RVA: 0x000DB18C File Offset: 0x000D938C
		protected internal override void OnPreInit()
		{
			this._spawnerMissionHelper = new SpawnerEntityMissionHelper(this, false);
		}
	}
}
