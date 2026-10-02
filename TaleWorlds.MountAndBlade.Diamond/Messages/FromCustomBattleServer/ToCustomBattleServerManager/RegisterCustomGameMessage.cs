using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromCustomBattleServer.ToCustomBattleServerManager
{
	// Token: 0x02000009 RID: 9
	[MessageDescription("CustomBattleServer", "CustomBattleServerManager", true)]
	[Serializable]
	public class RegisterCustomGameMessage : Message
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002377 File Offset: 0x00000577
		// (set) Token: 0x06000036 RID: 54 RVA: 0x0000237F File Offset: 0x0000057F
		[JsonProperty]
		public int GameDefinitionId { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002388 File Offset: 0x00000588
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002390 File Offset: 0x00000590
		[JsonProperty]
		public string GameModule { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002399 File Offset: 0x00000599
		// (set) Token: 0x0600003A RID: 58 RVA: 0x000023A1 File Offset: 0x000005A1
		[JsonProperty]
		public string GameType { get; private set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000023AA File Offset: 0x000005AA
		// (set) Token: 0x0600003C RID: 60 RVA: 0x000023B2 File Offset: 0x000005B2
		[JsonProperty]
		public string ServerName { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000023BB File Offset: 0x000005BB
		// (set) Token: 0x0600003E RID: 62 RVA: 0x000023C3 File Offset: 0x000005C3
		[JsonProperty]
		public string ServerAddress { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000023CC File Offset: 0x000005CC
		// (set) Token: 0x06000040 RID: 64 RVA: 0x000023D4 File Offset: 0x000005D4
		[JsonProperty]
		public int MaxPlayerCount { get; private set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000023DD File Offset: 0x000005DD
		// (set) Token: 0x06000042 RID: 66 RVA: 0x000023E5 File Offset: 0x000005E5
		[JsonProperty]
		public string Map { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000023EE File Offset: 0x000005EE
		// (set) Token: 0x06000044 RID: 68 RVA: 0x000023F6 File Offset: 0x000005F6
		[JsonProperty]
		public string UniqueMapId { get; private set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000023FF File Offset: 0x000005FF
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002407 File Offset: 0x00000607
		[JsonProperty]
		public string GamePassword { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002410 File Offset: 0x00000610
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002418 File Offset: 0x00000618
		[JsonProperty]
		public string AdminPassword { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002421 File Offset: 0x00000621
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002429 File Offset: 0x00000629
		[JsonProperty]
		public int Port { get; private set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002432 File Offset: 0x00000632
		// (set) Token: 0x0600004C RID: 76 RVA: 0x0000243A File Offset: 0x0000063A
		[JsonProperty]
		public string Region { get; private set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002443 File Offset: 0x00000643
		// (set) Token: 0x0600004E RID: 78 RVA: 0x0000244B File Offset: 0x0000064B
		[JsonProperty]
		public int Permission { get; private set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002454 File Offset: 0x00000654
		// (set) Token: 0x06000050 RID: 80 RVA: 0x0000245C File Offset: 0x0000065C
		[JsonProperty]
		public bool IsOverridingIP { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002465 File Offset: 0x00000665
		// (set) Token: 0x06000052 RID: 82 RVA: 0x0000246D File Offset: 0x0000066D
		[JsonProperty]
		public bool CrossplayEnabled { get; private set; }

		// Token: 0x06000053 RID: 83 RVA: 0x00002476 File Offset: 0x00000676
		public RegisterCustomGameMessage()
		{
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002480 File Offset: 0x00000680
		public RegisterCustomGameMessage(int gameDefinitionId, string gameModule, string gameType, string serverName, string serverAddress, int maxPlayerCount, string map, string uniqueMapId, string gamePassword, string adminPassword, int port, string region, int permission, bool crossplayEnabled, bool isOverridingIP)
		{
			this.GameDefinitionId = gameDefinitionId;
			this.GameModule = gameModule;
			this.GameType = gameType;
			this.ServerName = serverName;
			this.ServerAddress = serverAddress;
			this.MaxPlayerCount = maxPlayerCount;
			this.Map = map;
			this.UniqueMapId = uniqueMapId;
			this.GamePassword = gamePassword;
			this.AdminPassword = adminPassword;
			this.Port = port;
			this.Region = region;
			this.Permission = permission;
			this.CrossplayEnabled = crossplayEnabled;
			this.IsOverridingIP = isOverridingIP;
		}
	}
}
