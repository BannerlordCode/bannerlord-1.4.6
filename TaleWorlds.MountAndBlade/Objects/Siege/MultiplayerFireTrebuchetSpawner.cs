using System;

namespace TaleWorlds.MountAndBlade.Objects.Siege
{
	// Token: 0x020003B5 RID: 949
	public class MultiplayerFireTrebuchetSpawner : TrebuchetSpawner
	{
		// Token: 0x0600354D RID: 13645 RVA: 0x000DB175 File Offset: 0x000D9375
		protected internal override void OnPreInit()
		{
			this._spawnerMissionHelperFire = new SpawnerEntityMissionHelper(this, true);
		}
	}
}
