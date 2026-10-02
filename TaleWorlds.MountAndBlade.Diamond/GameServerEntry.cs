using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.Library;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000119 RID: 281
	[Serializable]
	public class GameServerEntry
	{
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x00007F24 File Offset: 0x00006124
		// (set) Token: 0x06000625 RID: 1573 RVA: 0x00007F2C File Offset: 0x0000612C
		[JsonProperty]
		public CustomBattleId Id { get; private set; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x00007F35 File Offset: 0x00006135
		// (set) Token: 0x06000627 RID: 1575 RVA: 0x00007F3D File Offset: 0x0000613D
		[JsonProperty]
		public string Address { get; private set; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x00007F46 File Offset: 0x00006146
		// (set) Token: 0x06000629 RID: 1577 RVA: 0x00007F4E File Offset: 0x0000614E
		[JsonProperty]
		public int Port { get; private set; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x00007F57 File Offset: 0x00006157
		// (set) Token: 0x0600062B RID: 1579 RVA: 0x00007F5F File Offset: 0x0000615F
		[JsonProperty]
		public string Region { get; private set; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x00007F68 File Offset: 0x00006168
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x00007F70 File Offset: 0x00006170
		[JsonProperty]
		public int PlayerCount { get; private set; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x00007F79 File Offset: 0x00006179
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x00007F81 File Offset: 0x00006181
		[JsonProperty]
		public int MaxPlayerCount { get; private set; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x00007F8A File Offset: 0x0000618A
		// (set) Token: 0x06000631 RID: 1585 RVA: 0x00007F92 File Offset: 0x00006192
		[JsonProperty]
		public string ServerName { get; private set; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x00007F9B File Offset: 0x0000619B
		// (set) Token: 0x06000633 RID: 1587 RVA: 0x00007FA3 File Offset: 0x000061A3
		[JsonProperty]
		public string GameModule { get; private set; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x00007FAC File Offset: 0x000061AC
		// (set) Token: 0x06000635 RID: 1589 RVA: 0x00007FB4 File Offset: 0x000061B4
		[JsonProperty]
		public string GameType { get; private set; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x00007FBD File Offset: 0x000061BD
		// (set) Token: 0x06000637 RID: 1591 RVA: 0x00007FC5 File Offset: 0x000061C5
		[JsonProperty]
		public string Map { get; private set; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x00007FCE File Offset: 0x000061CE
		// (set) Token: 0x06000639 RID: 1593 RVA: 0x00007FD6 File Offset: 0x000061D6
		[JsonProperty]
		public string UniqueMapId { get; private set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x00007FDF File Offset: 0x000061DF
		// (set) Token: 0x0600063B RID: 1595 RVA: 0x00007FE7 File Offset: 0x000061E7
		[JsonProperty]
		public int Ping { get; private set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x00007FF0 File Offset: 0x000061F0
		// (set) Token: 0x0600063D RID: 1597 RVA: 0x00007FF8 File Offset: 0x000061F8
		[JsonProperty]
		public bool IsOfficial { get; private set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x00008001 File Offset: 0x00006201
		// (set) Token: 0x0600063F RID: 1599 RVA: 0x00008009 File Offset: 0x00006209
		[JsonProperty]
		public bool ByOfficialProvider { get; private set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x00008012 File Offset: 0x00006212
		// (set) Token: 0x06000641 RID: 1601 RVA: 0x0000801A File Offset: 0x0000621A
		[JsonProperty]
		public bool PasswordProtected { get; private set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x00008023 File Offset: 0x00006223
		// (set) Token: 0x06000643 RID: 1603 RVA: 0x0000802B File Offset: 0x0000622B
		[JsonProperty]
		public int Permission { get; private set; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x00008034 File Offset: 0x00006234
		// (set) Token: 0x06000645 RID: 1605 RVA: 0x0000803C File Offset: 0x0000623C
		[JsonProperty]
		public bool CrossplayEnabled { get; private set; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x00008045 File Offset: 0x00006245
		// (set) Token: 0x06000647 RID: 1607 RVA: 0x0000804D File Offset: 0x0000624D
		[JsonProperty]
		public PlayerId HostId { get; private set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x00008056 File Offset: 0x00006256
		// (set) Token: 0x06000649 RID: 1609 RVA: 0x0000805E File Offset: 0x0000625E
		[JsonProperty]
		public string HostName { get; private set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x00008067 File Offset: 0x00006267
		// (set) Token: 0x0600064B RID: 1611 RVA: 0x0000806F File Offset: 0x0000626F
		[JsonProperty]
		public List<ModuleInfoModel> LoadedModules { get; private set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x00008078 File Offset: 0x00006278
		// (set) Token: 0x0600064D RID: 1613 RVA: 0x00008080 File Offset: 0x00006280
		[JsonProperty]
		public bool AllowsOptionalModules { get; private set; }

		// Token: 0x0600064E RID: 1614 RVA: 0x00008089 File Offset: 0x00006289
		public GameServerEntry()
		{
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00008094 File Offset: 0x00006294
		public GameServerEntry(CustomBattleId id, string serverName, string address, int port, string region, string gameModule, string gameType, string map, string uniqueMapId, int playerCount, int maxPlayerCount, bool isOfficial, bool byOfficialProvider, bool crossplayEnabled, PlayerId hostId, string hostName, List<ModuleInfoModel> loadedModules, bool allowsOptionalModules, bool passwordProtected = false, int permission = 0)
		{
			this.Id = id;
			this.ServerName = serverName;
			this.Address = address;
			this.GameModule = gameModule;
			this.GameType = gameType;
			this.Map = map;
			this.UniqueMapId = uniqueMapId;
			this.PlayerCount = playerCount;
			this.MaxPlayerCount = maxPlayerCount;
			this.Port = port;
			this.Region = region;
			this.IsOfficial = isOfficial;
			this.ByOfficialProvider = byOfficialProvider;
			this.CrossplayEnabled = crossplayEnabled;
			this.HostId = hostId;
			this.HostName = hostName;
			this.LoadedModules = loadedModules;
			this.AllowsOptionalModules = allowsOptionalModules;
			this.PasswordProtected = passwordProtected;
			this.Permission = permission;
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00008144 File Offset: 0x00006344
		public static void FilterGameServerEntriesBasedOnCrossplay(ref List<GameServerEntry> serverList, bool hasCrossplayPrivilege)
		{
			bool flag = ApplicationPlatform.CurrentPlatform == Platform.GDKDesktop;
			if (flag && !hasCrossplayPrivilege)
			{
				serverList.RemoveAll((GameServerEntry s) => s.CrossplayEnabled);
				return;
			}
			if (!flag)
			{
				serverList.RemoveAll((GameServerEntry s) => !s.CrossplayEnabled);
			}
		}
	}
}
