using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200015B RID: 347
	[Serializable]
	public class SupportedFeatures
	{
		// Token: 0x060009A6 RID: 2470 RVA: 0x0000EE4C File Offset: 0x0000D04C
		public SupportedFeatures()
		{
			this.Features = -1;
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0000EE5B File Offset: 0x0000D05B
		public SupportedFeatures(int features)
		{
			this.Features = features;
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0000EE6C File Offset: 0x0000D06C
		public bool SupportsFeatures(Features feature)
		{
			return (this.Features & (int)feature) == (int)feature;
		}

		// Token: 0x040004A7 RID: 1191
		public int Features;
	}
}
