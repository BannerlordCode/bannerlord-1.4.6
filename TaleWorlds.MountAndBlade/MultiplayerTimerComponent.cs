using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002C2 RID: 706
	public class MultiplayerTimerComponent : MissionNetwork
	{
		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x0600288E RID: 10382 RVA: 0x00099E2C File Offset: 0x0009802C
		// (set) Token: 0x0600288F RID: 10383 RVA: 0x00099E34 File Offset: 0x00098034
		public bool IsTimerRunning { get; private set; }

		// Token: 0x06002890 RID: 10384 RVA: 0x00099E3D File Offset: 0x0009803D
		public void StartTimerAsServer(float duration)
		{
			this._missionTimer = new MissionTimer(duration);
			this.IsTimerRunning = true;
		}

		// Token: 0x06002891 RID: 10385 RVA: 0x00099E52 File Offset: 0x00098052
		public void StartTimerAsClient(float startTime, float duration)
		{
			this._missionTimer = MissionTimer.CreateSynchedTimerClient(startTime, duration);
			this.IsTimerRunning = true;
		}

		// Token: 0x06002892 RID: 10386 RVA: 0x00099E68 File Offset: 0x00098068
		public float GetRemainingTime(bool isSynched)
		{
			if (!this.IsTimerRunning)
			{
				return 0f;
			}
			float remainingTimeInSeconds = this._missionTimer.GetRemainingTimeInSeconds(isSynched);
			if (isSynched)
			{
				return MathF.Min(remainingTimeInSeconds, this._missionTimer.GetTimerDuration());
			}
			return remainingTimeInSeconds;
		}

		// Token: 0x06002893 RID: 10387 RVA: 0x00099EA6 File Offset: 0x000980A6
		public bool CheckIfTimerPassed()
		{
			return this.IsTimerRunning && this._missionTimer.Check(false);
		}

		// Token: 0x06002894 RID: 10388 RVA: 0x00099EBE File Offset: 0x000980BE
		public MissionTime GetCurrentTimerStartTime()
		{
			return this._missionTimer.GetStartTime();
		}

		// Token: 0x04000F91 RID: 3985
		private MissionTimer _missionTimer;
	}
}
