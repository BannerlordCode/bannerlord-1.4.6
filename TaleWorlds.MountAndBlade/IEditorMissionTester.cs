using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002D8 RID: 728
	public interface IEditorMissionTester
	{
		// Token: 0x06002A88 RID: 10888
		void StartMissionForEditor(string missionName, string sceneName, string levels);

		// Token: 0x06002A89 RID: 10889
		void StartMissionForReplayEditor(string missionName, string sceneName, string levels, string fileName, bool record, float startTime, float endTime);
	}
}
