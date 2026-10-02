using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.HUD
{
	// Token: 0x02000059 RID: 89
	public class ReloadPhaseItemVM : ViewModel
	{
		// Token: 0x06000753 RID: 1875 RVA: 0x0001A8E9 File Offset: 0x00018AE9
		public ReloadPhaseItemVM(float progress, float relativeDurationToMaxDuration)
		{
			this.Update(progress, relativeDurationToMaxDuration);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0001A8F9 File Offset: 0x00018AF9
		public void Update(float progress, float relativeDurationToMaxDuration)
		{
			this.Progress = progress;
			this.RelativeDurationToMaxDuration = relativeDurationToMaxDuration;
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x0001A909 File Offset: 0x00018B09
		// (set) Token: 0x06000756 RID: 1878 RVA: 0x0001A911 File Offset: 0x00018B11
		[DataSourceProperty]
		public float Progress
		{
			get
			{
				return this._progress;
			}
			set
			{
				if (value != this._progress)
				{
					this._progress = value;
					base.OnPropertyChangedWithValue(value, "Progress");
				}
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x0001A92F File Offset: 0x00018B2F
		// (set) Token: 0x06000758 RID: 1880 RVA: 0x0001A937 File Offset: 0x00018B37
		[DataSourceProperty]
		public float RelativeDurationToMaxDuration
		{
			get
			{
				return this._relativeDurationToMaxDuration;
			}
			set
			{
				if (value != this._relativeDurationToMaxDuration)
				{
					this._relativeDurationToMaxDuration = value;
					base.OnPropertyChangedWithValue(value, "RelativeDurationToMaxDuration");
				}
			}
		}

		// Token: 0x04000342 RID: 834
		private float _progress;

		// Token: 0x04000343 RID: 835
		private float _relativeDurationToMaxDuration;
	}
}
