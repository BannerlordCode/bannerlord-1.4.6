using System;
using TaleWorlds.Library;

namespace SandBox.ViewModelCollection.Missions
{
	// Token: 0x0200002E RID: 46
	public class MissionQuestBarVM : ViewModel
	{
		// Token: 0x060003B4 RID: 948 RVA: 0x0000FCC0 File Offset: 0x0000DEC0
		public void UpdateQuestValues(float minDetectionLevel, float maxDetectionLevel, float currentDetectionLevel)
		{
			this.MinimumQuestLevel = minDetectionLevel;
			this.MaximumQuestLevel = maxDetectionLevel;
			this.CurrentQuestLevel = currentDetectionLevel;
			this.CurrentQuestLevelRatio = MBMath.InverseLerp(this.MinimumQuestLevel, this.MaximumQuestLevel, this.CurrentQuestLevel);
			this.HasQuestLevel = this.CurrentQuestLevel > 0f;
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000FD12 File Offset: 0x0000DF12
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x0000FD1A File Offset: 0x0000DF1A
		[DataSourceProperty]
		public bool HasQuestLevel
		{
			get
			{
				return this._hasQuestLevel;
			}
			set
			{
				if (value != this._hasQuestLevel)
				{
					this._hasQuestLevel = value;
					base.OnPropertyChangedWithValue(value, "HasQuestLevel");
				}
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x0000FD38 File Offset: 0x0000DF38
		// (set) Token: 0x060003B8 RID: 952 RVA: 0x0000FD40 File Offset: 0x0000DF40
		[DataSourceProperty]
		public float MinimumQuestLevel
		{
			get
			{
				return this._minimumQuestLevel;
			}
			set
			{
				if (value != this._minimumQuestLevel)
				{
					this._minimumQuestLevel = value;
					base.OnPropertyChangedWithValue(value, "MinimumQuestLevel");
				}
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0000FD5E File Offset: 0x0000DF5E
		// (set) Token: 0x060003BA RID: 954 RVA: 0x0000FD66 File Offset: 0x0000DF66
		[DataSourceProperty]
		public float MaximumQuestLevel
		{
			get
			{
				return this._maximumQuestLevel;
			}
			set
			{
				if (value != this._maximumQuestLevel)
				{
					this._maximumQuestLevel = value;
					base.OnPropertyChangedWithValue(value, "MaximumQuestLevel");
				}
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0000FD84 File Offset: 0x0000DF84
		// (set) Token: 0x060003BC RID: 956 RVA: 0x0000FD8C File Offset: 0x0000DF8C
		[DataSourceProperty]
		public float CurrentQuestLevel
		{
			get
			{
				return this._currentQuestLevel;
			}
			set
			{
				if (value != this._currentQuestLevel)
				{
					this._currentQuestLevel = value;
					base.OnPropertyChangedWithValue(value, "CurrentQuestLevel");
				}
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0000FDAA File Offset: 0x0000DFAA
		// (set) Token: 0x060003BE RID: 958 RVA: 0x0000FDB2 File Offset: 0x0000DFB2
		[DataSourceProperty]
		public float CurrentQuestLevelRatio
		{
			get
			{
				return this._currentQuestLevelRatio;
			}
			set
			{
				if (value != this._currentQuestLevelRatio)
				{
					this._currentQuestLevelRatio = value;
					base.OnPropertyChangedWithValue(value, "CurrentQuestLevelRatio");
				}
			}
		}

		// Token: 0x040001E2 RID: 482
		private bool _hasQuestLevel;

		// Token: 0x040001E3 RID: 483
		private float _minimumQuestLevel;

		// Token: 0x040001E4 RID: 484
		private float _maximumQuestLevel;

		// Token: 0x040001E5 RID: 485
		private float _currentQuestLevel;

		// Token: 0x040001E6 RID: 486
		private float _currentQuestLevelRatio;
	}
}
