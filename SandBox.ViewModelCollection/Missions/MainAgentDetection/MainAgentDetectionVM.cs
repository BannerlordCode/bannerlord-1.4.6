using System;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace SandBox.ViewModelCollection.Missions.MainAgentDetection
{
	// Token: 0x02000041 RID: 65
	public class MainAgentDetectionVM : ViewModel
	{
		// Token: 0x06000436 RID: 1078 RVA: 0x0001139A File Offset: 0x0000F59A
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.SuspicionFullText = new TextObject("{=KgTFCWG8}You are suspicious", null).ToString();
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x000113B8 File Offset: 0x0000F5B8
		public void UpdateDetectionValues(float minDetectionLevel, float maxDetectionLevel, float currentDetectionLevel)
		{
			this.MinimumDetectionLevel = minDetectionLevel;
			this.MaximumDetectionLevel = maxDetectionLevel;
			this.CurrentDetectionLevel = currentDetectionLevel;
			this.CurrentDetectionLevelRatio = MBMath.InverseLerp(this.MinimumDetectionLevel, this.MaximumDetectionLevel, this.CurrentDetectionLevel);
			this.HasDetection = this.CurrentDetectionLevel > 0f;
			this.HasReachedSuspicionTreshold = this.CurrentDetectionLevel >= this.MaximumDetectionLevel;
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x00011421 File Offset: 0x0000F621
		// (set) Token: 0x06000439 RID: 1081 RVA: 0x00011429 File Offset: 0x0000F629
		[DataSourceProperty]
		public bool HasDetection
		{
			get
			{
				return this._hasDetection;
			}
			set
			{
				if (value != this._hasDetection)
				{
					this._hasDetection = value;
					base.OnPropertyChangedWithValue(value, "HasDetection");
				}
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x00011447 File Offset: 0x0000F647
		// (set) Token: 0x0600043B RID: 1083 RVA: 0x0001144F File Offset: 0x0000F64F
		[DataSourceProperty]
		public bool HasReachedSuspicionTreshold
		{
			get
			{
				return this._hasReachedSuspicionTreshold;
			}
			set
			{
				if (value != this._hasReachedSuspicionTreshold)
				{
					this._hasReachedSuspicionTreshold = value;
					base.OnPropertyChangedWithValue(value, "HasReachedSuspicionTreshold");
				}
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x0001146D File Offset: 0x0000F66D
		// (set) Token: 0x0600043D RID: 1085 RVA: 0x00011475 File Offset: 0x0000F675
		[DataSourceProperty]
		public float MinimumDetectionLevel
		{
			get
			{
				return this._minimumDetectionLevel;
			}
			set
			{
				if (value != this._minimumDetectionLevel)
				{
					this._minimumDetectionLevel = value;
					base.OnPropertyChangedWithValue(value, "MinimumDetectionLevel");
				}
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x00011493 File Offset: 0x0000F693
		// (set) Token: 0x0600043F RID: 1087 RVA: 0x0001149B File Offset: 0x0000F69B
		[DataSourceProperty]
		public float MaximumDetectionLevel
		{
			get
			{
				return this._maximumDetectionLevel;
			}
			set
			{
				if (value != this._maximumDetectionLevel)
				{
					this._maximumDetectionLevel = value;
					base.OnPropertyChangedWithValue(value, "MaximumDetectionLevel");
				}
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x000114B9 File Offset: 0x0000F6B9
		// (set) Token: 0x06000441 RID: 1089 RVA: 0x000114C1 File Offset: 0x0000F6C1
		[DataSourceProperty]
		public float CurrentDetectionLevel
		{
			get
			{
				return this._currentDetectionLevel;
			}
			set
			{
				if (value != this._currentDetectionLevel)
				{
					this._currentDetectionLevel = value;
					base.OnPropertyChangedWithValue(value, "CurrentDetectionLevel");
				}
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x000114DF File Offset: 0x0000F6DF
		// (set) Token: 0x06000443 RID: 1091 RVA: 0x000114E7 File Offset: 0x0000F6E7
		[DataSourceProperty]
		public float CurrentDetectionLevelRatio
		{
			get
			{
				return this._currentDetectionLevelRatio;
			}
			set
			{
				if (value != this._currentDetectionLevelRatio)
				{
					this._currentDetectionLevelRatio = value;
					base.OnPropertyChangedWithValue(value, "CurrentDetectionLevelRatio");
				}
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x00011505 File Offset: 0x0000F705
		// (set) Token: 0x06000445 RID: 1093 RVA: 0x0001150D File Offset: 0x0000F70D
		[DataSourceProperty]
		public string SuspicionFullText
		{
			get
			{
				return this._suspicionFullText;
			}
			set
			{
				if (value != this._suspicionFullText)
				{
					this._suspicionFullText = value;
					base.OnPropertyChangedWithValue<string>(value, "SuspicionFullText");
				}
			}
		}

		// Token: 0x04000224 RID: 548
		private bool _hasDetection;

		// Token: 0x04000225 RID: 549
		private bool _hasReachedSuspicionTreshold;

		// Token: 0x04000226 RID: 550
		private float _minimumDetectionLevel;

		// Token: 0x04000227 RID: 551
		private float _maximumDetectionLevel;

		// Token: 0x04000228 RID: 552
		private float _currentDetectionLevel;

		// Token: 0x04000229 RID: 553
		private float _currentDetectionLevelRatio;

		// Token: 0x0400022A RID: 554
		private string _suspicionFullText;
	}
}
