using System;
using System.Collections.Generic;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000242 RID: 578
	public interface IMissionSystemHandler
	{
		// Token: 0x06002162 RID: 8546
		void OnMissionAfterStarting(Mission mission);

		// Token: 0x06002163 RID: 8547
		void OnMissionLoadingFinished(Mission mission);

		// Token: 0x06002164 RID: 8548
		void BeforeMissionTick(Mission mission, float realDt);

		// Token: 0x06002165 RID: 8549
		void AfterMissionTick(Mission mission, float realDt);

		// Token: 0x06002166 RID: 8550
		void UpdateCamera(Mission mission, float realDt);

		// Token: 0x06002167 RID: 8551
		bool RenderIsReady();

		// Token: 0x06002168 RID: 8552
		IEnumerable<MissionBehavior> OnAddBehaviors(IEnumerable<MissionBehavior> behaviors, Mission mission, string missionName, bool addDefaultMissionBehaviors);
	}
}
