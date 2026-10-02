using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002A1 RID: 673
	public class MissionTimer
	{
		// Token: 0x0600253D RID: 9533 RVA: 0x000871D9 File Offset: 0x000853D9
		private MissionTimer()
		{
		}

		// Token: 0x0600253E RID: 9534 RVA: 0x000871E1 File Offset: 0x000853E1
		public MissionTimer(float duration)
		{
			this._startTime = MissionTime.Now;
			this._duration = duration;
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x000871FB File Offset: 0x000853FB
		public MissionTime GetStartTime()
		{
			return this._startTime;
		}

		// Token: 0x06002540 RID: 9536 RVA: 0x00087203 File Offset: 0x00085403
		public float GetTimerDuration()
		{
			return this._duration;
		}

		// Token: 0x06002541 RID: 9537 RVA: 0x0008720C File Offset: 0x0008540C
		public float GetRemainingTimeInSeconds(bool synched = false)
		{
			if (this._duration < 0f)
			{
				return 0f;
			}
			float num = this._duration - this._startTime.ElapsedSeconds;
			if (synched && GameNetwork.IsClientOrReplay)
			{
				num -= Mission.Current.MissionTimeTracker.GetLastSyncDifference();
			}
			if (num <= 0f)
			{
				return 0f;
			}
			return num;
		}

		// Token: 0x06002542 RID: 9538 RVA: 0x0008726A File Offset: 0x0008546A
		public bool Check(bool reset = false)
		{
			bool flag = this.GetRemainingTimeInSeconds(false) <= 0f;
			if (flag && reset)
			{
				this._startTime = MissionTime.Now;
			}
			return flag;
		}

		// Token: 0x06002543 RID: 9539 RVA: 0x0008728D File Offset: 0x0008548D
		public void Reset()
		{
			this._startTime = MissionTime.Now;
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x0008729A File Offset: 0x0008549A
		public void Set(float timeInSeconds)
		{
			this._startTime = new MissionTime(Mission.Current.MissionTimeTracker.NumberOfTicks + (long)(timeInSeconds * 10000000f));
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x000872BF File Offset: 0x000854BF
		public void SetDuration(float duration)
		{
			this._duration = duration;
		}

		// Token: 0x06002546 RID: 9542 RVA: 0x000872C8 File Offset: 0x000854C8
		public static MissionTimer CreateSynchedTimerClient(float startTimeInSeconds, float duration)
		{
			return new MissionTimer
			{
				_startTime = new MissionTime((long)(startTimeInSeconds * 10000000f)),
				_duration = duration
			};
		}

		// Token: 0x04000E5F RID: 3679
		private MissionTime _startTime;

		// Token: 0x04000E60 RID: 3680
		private float _duration;
	}
}
