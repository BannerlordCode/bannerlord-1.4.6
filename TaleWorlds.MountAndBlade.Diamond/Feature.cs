using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000113 RID: 275
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	public class Feature : Attribute
	{
		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x00007DF8 File Offset: 0x00005FF8
		// (set) Token: 0x0600060C RID: 1548 RVA: 0x00007E00 File Offset: 0x00006000
		public Features FeatureFlag { get; private set; }

		// Token: 0x0600060D RID: 1549 RVA: 0x00007E09 File Offset: 0x00006009
		public Feature(Features flag)
		{
			this.FeatureFlag = flag;
		}
	}
}
