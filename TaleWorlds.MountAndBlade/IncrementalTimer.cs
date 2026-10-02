using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000249 RID: 585
	public class IncrementalTimer
	{
		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06002193 RID: 8595 RVA: 0x000756E6 File Offset: 0x000738E6
		// (set) Token: 0x06002194 RID: 8596 RVA: 0x000756EE File Offset: 0x000738EE
		public float TimerCounter { get; private set; }

		// Token: 0x06002195 RID: 8597 RVA: 0x000756F8 File Offset: 0x000738F8
		public IncrementalTimer(float totalDuration, float tickInterval)
		{
			this._tickInterval = MathF.Max(tickInterval, 0.01f);
			this._totalDuration = MathF.Max(totalDuration, 0.01f);
			this.TimerCounter = 0f;
			this._timer = new Timer(MBCommon.GetTotalMissionTime(), this._tickInterval, true);
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x0007574F File Offset: 0x0007394F
		public bool Check()
		{
			if (this._timer.Check(MBCommon.GetTotalMissionTime()))
			{
				this.TimerCounter += this._tickInterval / this._totalDuration;
				return true;
			}
			return false;
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x00075780 File Offset: 0x00073980
		public bool HasEnded()
		{
			return this.TimerCounter >= 1f;
		}

		// Token: 0x04000CE5 RID: 3301
		private readonly float _totalDuration;

		// Token: 0x04000CE6 RID: 3302
		private readonly float _tickInterval;

		// Token: 0x04000CE7 RID: 3303
		private readonly Timer _timer;
	}
}
