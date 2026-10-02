using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002A2 RID: 674
	public class MissionTimeTracker
	{
		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06002547 RID: 9543 RVA: 0x000872E9 File Offset: 0x000854E9
		// (set) Token: 0x06002548 RID: 9544 RVA: 0x000872F1 File Offset: 0x000854F1
		public long NumberOfTicks { get; private set; }

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06002549 RID: 9545 RVA: 0x000872FA File Offset: 0x000854FA
		// (set) Token: 0x0600254A RID: 9546 RVA: 0x00087302 File Offset: 0x00085502
		public long DeltaTimeInTicks { get; private set; }

		// Token: 0x0600254B RID: 9547 RVA: 0x0008730B File Offset: 0x0008550B
		public MissionTimeTracker(MissionTime initialMapTime)
		{
			this.NumberOfTicks = initialMapTime.NumberOfTicks;
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x00087320 File Offset: 0x00085520
		public MissionTimeTracker()
		{
			this.NumberOfTicks = 0L;
		}

		// Token: 0x0600254D RID: 9549 RVA: 0x00087330 File Offset: 0x00085530
		public void Tick(float seconds)
		{
			this.DeltaTimeInTicks = (long)(seconds * 10000000f);
			this.NumberOfTicks += this.DeltaTimeInTicks;
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x00087354 File Offset: 0x00085554
		public void UpdateSync(float newValue)
		{
			long num = (long)(newValue * 10000000f);
			this._lastSyncDifference = num - this.NumberOfTicks;
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x00087378 File Offset: 0x00085578
		public float GetLastSyncDifference()
		{
			return (float)this._lastSyncDifference / 10000000f;
		}

		// Token: 0x04000E63 RID: 3683
		private long _lastSyncDifference;
	}
}
