using System;
using Newtonsoft.Json;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000109 RID: 265
	[Serializable]
	public class ClanLeaderboardInfo
	{
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x00007209 File Offset: 0x00005409
		// (set) Token: 0x060005AB RID: 1451 RVA: 0x00007210 File Offset: 0x00005410
		public static ClanLeaderboardInfo Empty { get; private set; } = new ClanLeaderboardInfo(new ClanLeaderboardEntry[0]);

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x0000722A File Offset: 0x0000542A
		// (set) Token: 0x060005AE RID: 1454 RVA: 0x00007232 File Offset: 0x00005432
		[JsonProperty]
		public ClanLeaderboardEntry[] ClanEntries { get; private set; }

		// Token: 0x060005AF RID: 1455 RVA: 0x0000723B File Offset: 0x0000543B
		public ClanLeaderboardInfo(ClanLeaderboardEntry[] entries)
		{
			this.ClanEntries = entries;
		}
	}
}
