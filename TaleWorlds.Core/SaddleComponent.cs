using System;

namespace TaleWorlds.Core
{
	// Token: 0x020000C7 RID: 199
	public class SaddleComponent : ItemComponent
	{
		// Token: 0x06000ADC RID: 2780 RVA: 0x000230C8 File Offset: 0x000212C8
		public SaddleComponent(SaddleComponent saddleComponent)
		{
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x000230D0 File Offset: 0x000212D0
		public override ItemComponent GetCopy()
		{
			return new SaddleComponent(this);
		}
	}
}
