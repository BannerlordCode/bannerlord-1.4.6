using System;
using System.Collections.Generic;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.View.MissionViews.Singleplayer
{
	// Token: 0x02000093 RID: 147
	public class MissionCustomBattlePreloadView : MissionView
	{
		// Token: 0x06000547 RID: 1351 RVA: 0x00026CF0 File Offset: 0x00024EF0
		public override void OnPreMissionTick(float dt)
		{
			if (!this._preloadDone)
			{
				MissionCombatantsLogic missionBehavior = base.Mission.GetMissionBehavior<MissionCombatantsLogic>();
				List<BasicCharacterObject> list = new List<BasicCharacterObject>();
				foreach (IBattleCombatant battleCombatant in missionBehavior.GetAllCombatants())
				{
					list.AddRange(((CustomBattleCombatant)battleCombatant).Characters);
				}
				this._helperInstance.PreloadCharacters(list);
				SiegeDeploymentMissionController missionBehavior2 = Mission.Current.GetMissionBehavior<SiegeDeploymentMissionController>();
				if (missionBehavior2 != null)
				{
					this._helperInstance.PreloadItems(missionBehavior2.GetSiegeMissiles());
				}
				this._preloadDone = true;
			}
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00026D94 File Offset: 0x00024F94
		public override void OnSceneRenderingStarted()
		{
			this._helperInstance.WaitForMeshesToBeLoaded();
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00026DA1 File Offset: 0x00024FA1
		public override void OnMissionStateDeactivated()
		{
			base.OnMissionStateDeactivated();
			this._helperInstance.Clear();
		}

		// Token: 0x040002F1 RID: 753
		private PreloadHelper _helperInstance = new PreloadHelper();

		// Token: 0x040002F2 RID: 754
		private bool _preloadDone;
	}
}
