using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001E0 RID: 480
	public class MBTestRun
	{
		// Token: 0x06001C49 RID: 7241 RVA: 0x00061108 File Offset: 0x0005F308
		public static bool EnterEditMode()
		{
			return MBAPI.IMBTestRun.EnterEditMode();
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x00061114 File Offset: 0x0005F314
		public static bool NewScene()
		{
			return MBAPI.IMBTestRun.NewScene();
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x00061120 File Offset: 0x0005F320
		public static bool LeaveEditMode()
		{
			return MBAPI.IMBTestRun.LeaveEditMode();
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x0006112C File Offset: 0x0005F32C
		public static bool OpenScene(string sceneName)
		{
			return MBAPI.IMBTestRun.OpenScene(sceneName);
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x00061139 File Offset: 0x0005F339
		public static bool CloseScene()
		{
			return MBAPI.IMBTestRun.CloseScene();
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x00061145 File Offset: 0x0005F345
		public static bool SaveScene()
		{
			return false;
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x00061148 File Offset: 0x0005F348
		public static bool OpenDefaultScene()
		{
			return false;
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x0006114B File Offset: 0x0005F34B
		public static int GetFPS()
		{
			return MBAPI.IMBTestRun.GetFPS();
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x00061157 File Offset: 0x0005F357
		public static void StartMission()
		{
			MBAPI.IMBTestRun.StartMission();
		}
	}
}
