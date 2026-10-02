using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000237 RID: 567
	public class GameStartupInfo
	{
		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x060020E6 RID: 8422 RVA: 0x000747BC File Offset: 0x000729BC
		// (set) Token: 0x060020E7 RID: 8423 RVA: 0x000747C4 File Offset: 0x000729C4
		public GameStartupType StartupType { get; internal set; }

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x060020E8 RID: 8424 RVA: 0x000747CD File Offset: 0x000729CD
		// (set) Token: 0x060020E9 RID: 8425 RVA: 0x000747D5 File Offset: 0x000729D5
		public DedicatedServerType DedicatedServerType { get; internal set; }

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x060020EA RID: 8426 RVA: 0x000747DE File Offset: 0x000729DE
		// (set) Token: 0x060020EB RID: 8427 RVA: 0x000747E6 File Offset: 0x000729E6
		public bool PlayerHostedDedicatedServer { get; internal set; }

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x060020EC RID: 8428 RVA: 0x000747EF File Offset: 0x000729EF
		// (set) Token: 0x060020ED RID: 8429 RVA: 0x000747F7 File Offset: 0x000729F7
		public bool IsSinglePlatformServer { get; internal set; }

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x060020EE RID: 8430 RVA: 0x00074800 File Offset: 0x00072A00
		// (set) Token: 0x060020EF RID: 8431 RVA: 0x00074808 File Offset: 0x00072A08
		public string CustomServerHostIP { get; internal set; } = string.Empty;

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x060020F0 RID: 8432 RVA: 0x00074811 File Offset: 0x00072A11
		// (set) Token: 0x060020F1 RID: 8433 RVA: 0x00074819 File Offset: 0x00072A19
		public int ServerPort { get; internal set; }

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x060020F2 RID: 8434 RVA: 0x00074822 File Offset: 0x00072A22
		// (set) Token: 0x060020F3 RID: 8435 RVA: 0x0007482A File Offset: 0x00072A2A
		public string ServerRegion { get; internal set; }

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x060020F4 RID: 8436 RVA: 0x00074833 File Offset: 0x00072A33
		// (set) Token: 0x060020F5 RID: 8437 RVA: 0x0007483B File Offset: 0x00072A3B
		public sbyte ServerPriority { get; internal set; }

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x060020F6 RID: 8438 RVA: 0x00074844 File Offset: 0x00072A44
		// (set) Token: 0x060020F7 RID: 8439 RVA: 0x0007484C File Offset: 0x00072A4C
		public string ServerGameMode { get; internal set; }

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x060020F8 RID: 8440 RVA: 0x00074855 File Offset: 0x00072A55
		// (set) Token: 0x060020F9 RID: 8441 RVA: 0x0007485D File Offset: 0x00072A5D
		public string CustomGameServerConfigFile { get; internal set; }

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x060020FA RID: 8442 RVA: 0x00074866 File Offset: 0x00072A66
		// (set) Token: 0x060020FB RID: 8443 RVA: 0x0007486E File Offset: 0x00072A6E
		public string CustomGameServerNameOverride { get; internal set; }

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x060020FC RID: 8444 RVA: 0x00074877 File Offset: 0x00072A77
		// (set) Token: 0x060020FD RID: 8445 RVA: 0x0007487F File Offset: 0x00072A7F
		public string CustomGameServerPasswordOverride { get; internal set; }

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x060020FE RID: 8446 RVA: 0x00074888 File Offset: 0x00072A88
		// (set) Token: 0x060020FF RID: 8447 RVA: 0x00074890 File Offset: 0x00072A90
		public string CustomGameServerAuthToken { get; internal set; }

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06002100 RID: 8448 RVA: 0x00074899 File Offset: 0x00072A99
		// (set) Token: 0x06002101 RID: 8449 RVA: 0x000748A1 File Offset: 0x00072AA1
		public bool CustomGameServerAllowsOptionalModules { get; internal set; } = true;

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06002102 RID: 8450 RVA: 0x000748AA File Offset: 0x00072AAA
		// (set) Token: 0x06002103 RID: 8451 RVA: 0x000748B2 File Offset: 0x00072AB2
		public string OverridenUserName { get; internal set; }

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06002104 RID: 8452 RVA: 0x000748BB File Offset: 0x00072ABB
		// (set) Token: 0x06002105 RID: 8453 RVA: 0x000748C3 File Offset: 0x00072AC3
		public string PremadeGameType { get; internal set; }

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06002106 RID: 8454 RVA: 0x000748CC File Offset: 0x00072ACC
		// (set) Token: 0x06002107 RID: 8455 RVA: 0x000748D4 File Offset: 0x00072AD4
		public int Permission { get; internal set; }

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06002108 RID: 8456 RVA: 0x000748DD File Offset: 0x00072ADD
		// (set) Token: 0x06002109 RID: 8457 RVA: 0x000748E5 File Offset: 0x00072AE5
		public string PlatformInterface { get; internal set; }

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x0600210A RID: 8458 RVA: 0x000748EE File Offset: 0x00072AEE
		// (set) Token: 0x0600210B RID: 8459 RVA: 0x000748F6 File Offset: 0x00072AF6
		public string EpicUserId { get; internal set; }

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x0600210C RID: 8460 RVA: 0x000748FF File Offset: 0x00072AFF
		// (set) Token: 0x0600210D RID: 8461 RVA: 0x00074907 File Offset: 0x00072B07
		public string EpicUserName { get; internal set; }

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x0600210E RID: 8462 RVA: 0x00074910 File Offset: 0x00072B10
		// (set) Token: 0x0600210F RID: 8463 RVA: 0x00074918 File Offset: 0x00072B18
		public bool IsContinueGame { get; internal set; }

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06002110 RID: 8464 RVA: 0x00074921 File Offset: 0x00072B21
		// (set) Token: 0x06002111 RID: 8465 RVA: 0x00074929 File Offset: 0x00072B29
		public double ServerBandwidthLimitInMbps { get; internal set; }

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06002112 RID: 8466 RVA: 0x00074932 File Offset: 0x00072B32
		// (set) Token: 0x06002113 RID: 8467 RVA: 0x0007493A File Offset: 0x00072B3A
		public int ServerTickRate { get; internal set; }
	}
}
