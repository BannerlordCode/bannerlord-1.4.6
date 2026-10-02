using System;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.Party
{
	// Token: 0x02000303 RID: 771
	public struct ShipTemplateStack
	{
		// Token: 0x06002D36 RID: 11574 RVA: 0x000BF23D File Offset: 0x000BD43D
		public ShipTemplateStack(ShipHull shipHull, int minValue, int maxValue)
		{
			this.ShipHull = shipHull;
			this.MinValue = minValue;
			this.MaxValue = maxValue;
		}

		// Token: 0x04000D43 RID: 3395
		public ShipHull ShipHull;

		// Token: 0x04000D44 RID: 3396
		public int MinValue;

		// Token: 0x04000D45 RID: 3397
		public int MaxValue;
	}
}
