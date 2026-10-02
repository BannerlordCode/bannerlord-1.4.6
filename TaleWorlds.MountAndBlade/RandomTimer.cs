using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000385 RID: 901
	public class RandomTimer : Timer
	{
		// Token: 0x060033D0 RID: 13264 RVA: 0x000D5E1F File Offset: 0x000D401F
		public RandomTimer(float gameTime, float durationMin, float durationMax)
			: base(gameTime, MBRandom.RandomFloatRanged(durationMin, durationMax), true)
		{
			this.durationMin = durationMin;
			this.durationMax = durationMax;
		}

		// Token: 0x060033D1 RID: 13265 RVA: 0x000D5E40 File Offset: 0x000D4040
		public override bool Check(float gameTime)
		{
			bool flag = false;
			bool flag2;
			do
			{
				flag2 = base.Check(gameTime);
				if (flag2)
				{
					this.RecomputeDuration();
					flag = true;
				}
			}
			while (flag2);
			return flag;
		}

		// Token: 0x060033D2 RID: 13266 RVA: 0x000D5E66 File Offset: 0x000D4066
		public void ChangeDuration(float min, float max)
		{
			this.durationMin = min;
			this.durationMax = max;
			this.RecomputeDuration();
		}

		// Token: 0x060033D3 RID: 13267 RVA: 0x000D5E7C File Offset: 0x000D407C
		public void RecomputeDuration()
		{
			base.Duration = MBRandom.RandomFloatRanged(this.durationMin, this.durationMax);
		}

		// Token: 0x040015E8 RID: 5608
		private float durationMin;

		// Token: 0x040015E9 RID: 5609
		private float durationMax;
	}
}
