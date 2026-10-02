using System;
using System.Collections.Generic;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000129 RID: 297
	[Serializable]
	public class GameServerProperties
	{
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x0000B8C5 File Offset: 0x00009AC5
		// (set) Token: 0x060007B2 RID: 1970 RVA: 0x0000B8CD File Offset: 0x00009ACD
		public string Name { get; set; }

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x0000B8D6 File Offset: 0x00009AD6
		// (set) Token: 0x060007B4 RID: 1972 RVA: 0x0000B8DE File Offset: 0x00009ADE
		public string Address { get; set; }

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x0000B8E7 File Offset: 0x00009AE7
		// (set) Token: 0x060007B6 RID: 1974 RVA: 0x0000B8EF File Offset: 0x00009AEF
		public int Port { get; set; }

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x0000B8F8 File Offset: 0x00009AF8
		// (set) Token: 0x060007B8 RID: 1976 RVA: 0x0000B900 File Offset: 0x00009B00
		public string Region { get; set; }

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x0000B909 File Offset: 0x00009B09
		// (set) Token: 0x060007BA RID: 1978 RVA: 0x0000B911 File Offset: 0x00009B11
		public string GameModule { get; set; }

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x0000B91A File Offset: 0x00009B1A
		// (set) Token: 0x060007BC RID: 1980 RVA: 0x0000B922 File Offset: 0x00009B22
		public string GameType { get; set; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x0000B92B File Offset: 0x00009B2B
		// (set) Token: 0x060007BE RID: 1982 RVA: 0x0000B933 File Offset: 0x00009B33
		public string Map { get; set; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x0000B93C File Offset: 0x00009B3C
		// (set) Token: 0x060007C0 RID: 1984 RVA: 0x0000B944 File Offset: 0x00009B44
		public string UniqueMapId { get; set; }

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x0000B94D File Offset: 0x00009B4D
		// (set) Token: 0x060007C2 RID: 1986 RVA: 0x0000B955 File Offset: 0x00009B55
		public string GamePassword { get; set; }

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x0000B95E File Offset: 0x00009B5E
		// (set) Token: 0x060007C4 RID: 1988 RVA: 0x0000B966 File Offset: 0x00009B66
		public string AdminPassword { get; set; }

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x0000B96F File Offset: 0x00009B6F
		// (set) Token: 0x060007C6 RID: 1990 RVA: 0x0000B977 File Offset: 0x00009B77
		public int MaxPlayerCount { get; set; }

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x0000B980 File Offset: 0x00009B80
		// (set) Token: 0x060007C8 RID: 1992 RVA: 0x0000B988 File Offset: 0x00009B88
		public bool PasswordProtected { get; set; }

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x0000B991 File Offset: 0x00009B91
		// (set) Token: 0x060007CA RID: 1994 RVA: 0x0000B999 File Offset: 0x00009B99
		public bool IsOfficial { get; set; }

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x0000B9A2 File Offset: 0x00009BA2
		// (set) Token: 0x060007CC RID: 1996 RVA: 0x0000B9AA File Offset: 0x00009BAA
		public bool ByOfficialProvider { get; set; }

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x0000B9B3 File Offset: 0x00009BB3
		// (set) Token: 0x060007CE RID: 1998 RVA: 0x0000B9BB File Offset: 0x00009BBB
		public bool CrossplayEnabled { get; set; }

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x0000B9C4 File Offset: 0x00009BC4
		// (set) Token: 0x060007D0 RID: 2000 RVA: 0x0000B9CC File Offset: 0x00009BCC
		public int Permission { get; set; }

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x0000B9D5 File Offset: 0x00009BD5
		// (set) Token: 0x060007D2 RID: 2002 RVA: 0x0000B9DD File Offset: 0x00009BDD
		public PlayerId HostId { get; set; }

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x0000B9E6 File Offset: 0x00009BE6
		// (set) Token: 0x060007D4 RID: 2004 RVA: 0x0000B9EE File Offset: 0x00009BEE
		public string HostName { get; set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x0000B9F7 File Offset: 0x00009BF7
		// (set) Token: 0x060007D6 RID: 2006 RVA: 0x0000B9FF File Offset: 0x00009BFF
		public List<ModuleInfoModel> LoadedModules { get; set; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x0000BA08 File Offset: 0x00009C08
		// (set) Token: 0x060007D8 RID: 2008 RVA: 0x0000BA10 File Offset: 0x00009C10
		public bool AllowsOptionalModules { get; set; }

		// Token: 0x060007D9 RID: 2009 RVA: 0x0000BA19 File Offset: 0x00009C19
		public GameServerProperties()
		{
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0000BA24 File Offset: 0x00009C24
		public GameServerProperties(string name, string address, int port, string region, string gameModule, string gameType, string map, string uniqueMapId, string gamePassword, string adminPassword, int maxPlayerCount, bool isOfficial, bool byOfficialProvider, bool crossplayEnabled, PlayerId hostId, string hostName, List<ModuleInfoModel> loadedModules, bool allowsOptionalModules, int permission)
		{
			this.Name = name;
			this.Address = address;
			this.Port = port;
			this.Region = region;
			this.GameModule = gameModule;
			this.GameType = gameType;
			this.Map = map;
			this.GamePassword = gamePassword;
			this.UniqueMapId = uniqueMapId;
			this.AdminPassword = adminPassword;
			this.MaxPlayerCount = maxPlayerCount;
			this.IsOfficial = isOfficial;
			this.ByOfficialProvider = byOfficialProvider;
			this.CrossplayEnabled = crossplayEnabled;
			this.HostId = hostId;
			this.HostName = hostName;
			this.LoadedModules = loadedModules;
			this.AllowsOptionalModules = allowsOptionalModules;
			this.PasswordProtected = gamePassword != null;
			this.Permission = permission;
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0000BAD8 File Offset: 0x00009CD8
		public void CheckAndReplaceProxyAddress(IReadOnlyDictionary<string, string> proxyAddressMap)
		{
			string text;
			if (proxyAddressMap != null && proxyAddressMap.TryGetValue(this.Address, out text))
			{
				this.Address = text;
			}
		}
	}
}
