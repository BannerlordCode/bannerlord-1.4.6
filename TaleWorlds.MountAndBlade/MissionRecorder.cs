using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200029E RID: 670
	public class MissionRecorder
	{
		// Token: 0x06002506 RID: 9478 RVA: 0x00086A68 File Offset: 0x00084C68
		public MissionRecorder(Mission mission)
		{
			this._mission = mission;
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x00086A77 File Offset: 0x00084C77
		public void RestartRecord()
		{
			MBAPI.IMBMission.RestartRecord(this._mission.Pointer);
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x00086A8E File Offset: 0x00084C8E
		public void ProcessRecordUntilTime(float time)
		{
			MBAPI.IMBMission.ProcessRecordUntilTime(this._mission.Pointer, time);
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x00086AA6 File Offset: 0x00084CA6
		public bool IsEndOfRecord()
		{
			return MBAPI.IMBMission.EndOfRecord(this._mission.Pointer);
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x00086ABD File Offset: 0x00084CBD
		public void StartRecording()
		{
			MBAPI.IMBMission.StartRecording();
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x00086AC9 File Offset: 0x00084CC9
		public void RecordCurrentState()
		{
			MBAPI.IMBMission.RecordCurrentState(this._mission.Pointer);
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x00086AE0 File Offset: 0x00084CE0
		public void BackupRecordToFile(string fileName, string gameType, string sceneLevels)
		{
			MBAPI.IMBMission.BackupRecordToFile(this._mission.Pointer, fileName, gameType, sceneLevels);
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x00086AFA File Offset: 0x00084CFA
		public void RestoreRecordFromFile(string fileName)
		{
			MBAPI.IMBMission.RestoreRecordFromFile(this._mission.Pointer, fileName);
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x00086B12 File Offset: 0x00084D12
		public void ClearRecordBuffers()
		{
			MBAPI.IMBMission.ClearRecordBuffers(this._mission.Pointer);
		}

		// Token: 0x0600250F RID: 9487 RVA: 0x00086B29 File Offset: 0x00084D29
		public static string GetSceneNameForReplay(PlatformFilePath fileName)
		{
			return MBAPI.IMBMission.GetSceneNameForReplay(fileName);
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x00086B36 File Offset: 0x00084D36
		public static string GetGameTypeForReplay(PlatformFilePath fileName)
		{
			return MBAPI.IMBMission.GetGameTypeForReplay(fileName);
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x00086B43 File Offset: 0x00084D43
		public static string GetSceneLevelsForReplay(PlatformFilePath fileName)
		{
			return MBAPI.IMBMission.GetSceneLevelsForReplay(fileName);
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x00086B50 File Offset: 0x00084D50
		public static string GetAtmosphereNameForReplay(PlatformFilePath fileName)
		{
			return MBAPI.IMBMission.GetAtmosphereNameForReplay(fileName);
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x00086B5D File Offset: 0x00084D5D
		public static int GetAtmosphereSeasonForReplay(PlatformFilePath fileName)
		{
			return MBAPI.IMBMission.GetAtmosphereSeasonForReplay(fileName);
		}

		// Token: 0x04000E54 RID: 3668
		private readonly Mission _mission;
	}
}
