using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade.Objects.Siege
{
	// Token: 0x020003AC RID: 940
	public class BallistaSpawner : SpawnerBase
	{
		// Token: 0x06003533 RID: 13619 RVA: 0x000DAB0F File Offset: 0x000D8D0F
		protected internal override void OnPreInit()
		{
			base.OnPreInit();
			this._spawnerMissionHelper = new SpawnerEntityMissionHelper(this, false);
			this._spawnerMissionHelperFire = new SpawnerEntityMissionHelper(this, true);
		}

		// Token: 0x06003534 RID: 13620 RVA: 0x000DAB34 File Offset: 0x000D8D34
		public override void AssignParameters(SpawnerEntityMissionHelper _spawnerMissionHelper)
		{
			_spawnerMissionHelper.SpawnedEntity.GetFirstScriptOfType<Ballista>().AddOnDeployTag = this.AddOnDeployTag;
			_spawnerMissionHelper.SpawnedEntity.GetFirstScriptOfType<Ballista>().RemoveOnDeployTag = this.RemoveOnDeployTag;
			_spawnerMissionHelper.SpawnedEntity.GetFirstScriptOfType<Ballista>().HorizontalDirectionRestriction = this.DirectionRestrictionDegree * 0.017453292f;
		}

		// Token: 0x04001699 RID: 5785
		[EditorVisibleScriptComponentVariable(true)]
		public string AddOnDeployTag = "";

		// Token: 0x0400169A RID: 5786
		[EditorVisibleScriptComponentVariable(true)]
		public string RemoveOnDeployTag = "";

		// Token: 0x0400169B RID: 5787
		[EditorVisibleScriptComponentVariable(true)]
		public float DirectionRestrictionDegree = 90f;
	}
}
