using System;

namespace TaleWorlds.MountAndBlade.Objects.Siege
{
	// Token: 0x020003B8 RID: 952
	public class MultiplayerTrebuchetSpawner : TrebuchetSpawner
	{
		// Token: 0x06003553 RID: 13651 RVA: 0x000DB1E9 File Offset: 0x000D93E9
		protected internal override void OnPreInit()
		{
			this._spawnerMissionHelper = new SpawnerEntityMissionHelper(this, false);
		}
	}
}
