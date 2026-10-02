using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200021E RID: 542
	public struct FactoredNumber
	{
		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001F67 RID: 8039 RVA: 0x0006CCA7 File Offset: 0x0006AEA7
		public float ResultNumber
		{
			get
			{
				return MathF.Clamp(this.BaseNumber + this.BaseNumber * this._sumOfFactors, this.LimitMinValue, this.LimitMaxValue);
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001F68 RID: 8040 RVA: 0x0006CCCE File Offset: 0x0006AECE
		// (set) Token: 0x06001F69 RID: 8041 RVA: 0x0006CCD6 File Offset: 0x0006AED6
		public float BaseNumber { get; private set; }

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001F6A RID: 8042 RVA: 0x0006CCDF File Offset: 0x0006AEDF
		public float LimitMinValue
		{
			get
			{
				return this._limitMinValue;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001F6B RID: 8043 RVA: 0x0006CCE8 File Offset: 0x0006AEE8
		public float LimitMaxValue
		{
			get
			{
				return this._limitMaxValue;
			}
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x0006CCF1 File Offset: 0x0006AEF1
		public FactoredNumber(float baseNumber = 0f)
		{
			this.BaseNumber = baseNumber;
			this._sumOfFactors = 0f;
			this._limitMinValue = float.MinValue;
			this._limitMaxValue = float.MaxValue;
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x0006CD1B File Offset: 0x0006AF1B
		public void Add(float value)
		{
			if (value.ApproximatelyEqualsTo(0f, 1E-05f))
			{
				return;
			}
			this.BaseNumber += value;
		}

		// Token: 0x06001F6E RID: 8046 RVA: 0x0006CD3E File Offset: 0x0006AF3E
		public void AddFactor(float value)
		{
			if (value.ApproximatelyEqualsTo(0f, 1E-05f))
			{
				return;
			}
			this._sumOfFactors += value;
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x0006CD61 File Offset: 0x0006AF61
		public void LimitMin(float minValue)
		{
			this._limitMinValue = minValue;
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x0006CD6A File Offset: 0x0006AF6A
		public void LimitMax(float maxValue)
		{
			this._limitMaxValue = maxValue;
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x0006CD73 File Offset: 0x0006AF73
		public void Clamp(float minValue, float maxValue)
		{
			this.LimitMin(minValue);
			this.LimitMax(maxValue);
		}

		// Token: 0x04000AD8 RID: 2776
		private float _limitMinValue;

		// Token: 0x04000AD9 RID: 2777
		private float _limitMaxValue;

		// Token: 0x04000ADA RID: 2778
		private float _sumOfFactors;
	}
}
