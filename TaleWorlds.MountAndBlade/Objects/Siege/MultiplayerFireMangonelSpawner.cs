using System;

namespace TaleWorlds.MountAndBlade.Objects.Siege
{
	// Token: 0x020003B4 RID: 948
	public class MultiplayerFireMangonelSpawner : MangonelSpawner
	{
		// Token: 0x0600354B RID: 13643 RVA: 0x000DB15E File Offset: 0x000D935E
		protected internal override void OnPreInit()
		{
			this._spawnerMissionHelperFire = new SpawnerEntityMissionHelper(this, true);
		}
	}
}
