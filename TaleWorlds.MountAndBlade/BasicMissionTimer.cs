using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001E5 RID: 485
	public class BasicMissionTimer
	{
		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001C6A RID: 7274 RVA: 0x000613FB File Offset: 0x0005F5FB
		public float ElapsedTime
		{
			get
			{
				return MBCommon.GetTotalMissionTime() - this._startTime;
			}
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x00061409 File Offset: 0x0005F609
		public BasicMissionTimer()
		{
			this._startTime = MBCommon.GetTotalMissionTime();
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x0006141C File Offset: 0x0005F61C
		public void Reset()
		{
			this._startTime = MBCommon.GetTotalMissionTime();
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x00061429 File Offset: 0x0005F629
		public void Set(float newElapsedTime)
		{
			this._startTime = MBCommon.GetTotalMissionTime() - newElapsedTime;
		}

		// Token: 0x04000989 RID: 2441
		private float _startTime;
	}
}
