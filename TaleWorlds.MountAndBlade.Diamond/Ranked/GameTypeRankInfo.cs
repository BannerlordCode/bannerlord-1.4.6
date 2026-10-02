using System;

namespace TaleWorlds.MountAndBlade.Diamond.Ranked
{
	// Token: 0x0200015C RID: 348
	[Serializable]
	public class GameTypeRankInfo
	{
		// Token: 0x17000316 RID: 790
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x0000EE86 File Offset: 0x0000D086
		// (set) Token: 0x060009AA RID: 2474 RVA: 0x0000EE8E File Offset: 0x0000D08E
		public string GameType { get; private set; }

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x0000EE97 File Offset: 0x0000D097
		// (set) Token: 0x060009AC RID: 2476 RVA: 0x0000EE9F File Offset: 0x0000D09F
		public RankBarInfo RankBarInfo { get; private set; }

		// Token: 0x060009AD RID: 2477 RVA: 0x0000EEA8 File Offset: 0x0000D0A8
		public GameTypeRankInfo(string gameType, RankBarInfo rankBarInfo)
		{
			this.GameType = gameType;
			this.RankBarInfo = rankBarInfo;
		}
	}
}
