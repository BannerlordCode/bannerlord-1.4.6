using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Objects.Siege
{
	// Token: 0x020003BB RID: 955
	public class SpawnerBase : ScriptComponentBehavior
	{
		// Token: 0x0600356A RID: 13674 RVA: 0x000DBC1D File Offset: 0x000D9E1D
		protected internal override bool OnCheckForProblems()
		{
			return !this._spawnerEditorHelper.IsValid;
		}

		// Token: 0x0600356B RID: 13675 RVA: 0x000DBC2D File Offset: 0x000D9E2D
		public virtual void AssignParameters(SpawnerEntityMissionHelper _spawnerMissionHelper)
		{
			Debug.FailedAssert("Please override 'AssignParameters' function in the derived class.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Objects\\Siege\\SpawnerBase.cs", "AssignParameters", 40);
		}

		// Token: 0x040016E3 RID: 5859
		[EditorVisibleScriptComponentVariable(true)]
		public string ToBeSpawnedOverrideName = "";

		// Token: 0x040016E4 RID: 5860
		[EditorVisibleScriptComponentVariable(true)]
		public string ToBeSpawnedOverrideNameForFireVersion = "";

		// Token: 0x040016E5 RID: 5861
		protected SpawnerEntityEditorHelper _spawnerEditorHelper;

		// Token: 0x040016E6 RID: 5862
		protected SpawnerEntityMissionHelper _spawnerMissionHelper;

		// Token: 0x040016E7 RID: 5863
		protected SpawnerEntityMissionHelper _spawnerMissionHelperFire;

		// Token: 0x0200067F RID: 1663
		public class SpawnerPermissionField : EditorVisibleScriptComponentVariable
		{
			// Token: 0x0600413E RID: 16702 RVA: 0x000FB4A9 File Offset: 0x000F96A9
			public SpawnerPermissionField()
				: base(false)
			{
			}
		}
	}
}
