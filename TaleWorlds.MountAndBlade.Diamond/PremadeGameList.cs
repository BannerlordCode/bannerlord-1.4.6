using System;
using Newtonsoft.Json;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200014E RID: 334
	[Serializable]
	public class PremadeGameList
	{
		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x0000DA80 File Offset: 0x0000BC80
		// (set) Token: 0x06000952 RID: 2386 RVA: 0x0000DA87 File Offset: 0x0000BC87
		public static PremadeGameList Empty { get; private set; } = new PremadeGameList(new PremadeGameEntry[0]);

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x0000DAA1 File Offset: 0x0000BCA1
		// (set) Token: 0x06000955 RID: 2389 RVA: 0x0000DAA9 File Offset: 0x0000BCA9
		[JsonProperty]
		public PremadeGameEntry[] PremadeGameEntries { get; private set; }

		// Token: 0x06000956 RID: 2390 RVA: 0x0000DAB2 File Offset: 0x0000BCB2
		public PremadeGameList(PremadeGameEntry[] entries)
		{
			this.PremadeGameEntries = entries;
		}
	}
}
