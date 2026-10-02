using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000FB RID: 251
	[Serializable]
	public struct BattleServerInformationForClient
	{
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x00006D3D File Offset: 0x00004F3D
		// (set) Token: 0x06000549 RID: 1353 RVA: 0x00006D45 File Offset: 0x00004F45
		public string MatchId { get; set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x00006D4E File Offset: 0x00004F4E
		// (set) Token: 0x0600054B RID: 1355 RVA: 0x00006D56 File Offset: 0x00004F56
		public string ServerAddress { get; set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x00006D5F File Offset: 0x00004F5F
		// (set) Token: 0x0600054D RID: 1357 RVA: 0x00006D67 File Offset: 0x00004F67
		public ushort ServerPort { get; set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x00006D70 File Offset: 0x00004F70
		// (set) Token: 0x0600054F RID: 1359 RVA: 0x00006D78 File Offset: 0x00004F78
		public int PeerIndex { get; set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x00006D81 File Offset: 0x00004F81
		// (set) Token: 0x06000551 RID: 1361 RVA: 0x00006D89 File Offset: 0x00004F89
		public int TeamNo { get; set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x00006D92 File Offset: 0x00004F92
		// (set) Token: 0x06000553 RID: 1363 RVA: 0x00006D9A File Offset: 0x00004F9A
		public int SessionKey { get; set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x00006DA3 File Offset: 0x00004FA3
		// (set) Token: 0x06000555 RID: 1365 RVA: 0x00006DAB File Offset: 0x00004FAB
		public string SceneName { get; set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x00006DB4 File Offset: 0x00004FB4
		// (set) Token: 0x06000557 RID: 1367 RVA: 0x00006DBC File Offset: 0x00004FBC
		public string GameType { get; set; }
	}
}
