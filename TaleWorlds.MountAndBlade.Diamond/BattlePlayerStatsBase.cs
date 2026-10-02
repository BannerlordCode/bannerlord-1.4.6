using System;
using Newtonsoft.Json;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000F0 RID: 240
	[JsonConverter(typeof(BattlePlayerStatsBaseJsonConverter))]
	[Serializable]
	public class BattlePlayerStatsBase
	{
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0000556E File Offset: 0x0000376E
		// (set) Token: 0x060004B6 RID: 1206 RVA: 0x00005576 File Offset: 0x00003776
		public string GameType { get; set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0000557F File Offset: 0x0000377F
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x00005587 File Offset: 0x00003787
		public int Kills { get; set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x00005590 File Offset: 0x00003790
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x00005598 File Offset: 0x00003798
		public int Assists { get; set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x000055A1 File Offset: 0x000037A1
		// (set) Token: 0x060004BC RID: 1212 RVA: 0x000055A9 File Offset: 0x000037A9
		public int Deaths { get; set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x000055B2 File Offset: 0x000037B2
		// (set) Token: 0x060004BE RID: 1214 RVA: 0x000055BA File Offset: 0x000037BA
		public int PlayTime { get; set; }
	}
}
