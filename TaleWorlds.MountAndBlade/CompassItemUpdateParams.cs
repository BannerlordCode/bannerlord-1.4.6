using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000253 RID: 595
	public struct CompassItemUpdateParams
	{
		// Token: 0x060021C4 RID: 8644 RVA: 0x00076754 File Offset: 0x00074954
		public CompassItemUpdateParams(object item, TargetIconType targetType, Vec3 worldPosition, uint color, uint color2)
		{
			this = default(CompassItemUpdateParams);
			this.Item = item;
			this.TargetType = targetType;
			this.WorldPosition = worldPosition;
			this.Color = color;
			this.Color2 = color2;
			this.IsAttacker = false;
			this.IsAlly = false;
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x00076790 File Offset: 0x00074990
		public CompassItemUpdateParams(object item, TargetIconType targetType, Vec3 worldPosition, Banner banner, bool isAttacker, bool isAlly)
		{
			this = default(CompassItemUpdateParams);
			this.Item = item;
			this.TargetType = targetType;
			this.WorldPosition = worldPosition;
			this.Banner = banner;
			this.IsAttacker = isAttacker;
			this.IsAlly = isAlly;
		}

		// Token: 0x04000D07 RID: 3335
		public readonly object Item;

		// Token: 0x04000D08 RID: 3336
		public readonly TargetIconType TargetType;

		// Token: 0x04000D09 RID: 3337
		public readonly Vec3 WorldPosition;

		// Token: 0x04000D0A RID: 3338
		public readonly uint Color;

		// Token: 0x04000D0B RID: 3339
		public readonly uint Color2;

		// Token: 0x04000D0C RID: 3340
		public readonly Banner Banner;

		// Token: 0x04000D0D RID: 3341
		public readonly bool IsAttacker;

		// Token: 0x04000D0E RID: 3342
		public readonly bool IsAlly;
	}
}
