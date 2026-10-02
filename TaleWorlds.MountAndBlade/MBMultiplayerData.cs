using System;
using TaleWorlds.MountAndBlade.Diamond;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001D0 RID: 464
	public class MBMultiplayerData
	{
		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001BBB RID: 7099 RVA: 0x0006057C File Offset: 0x0005E77C
		// (set) Token: 0x06001BBC RID: 7100 RVA: 0x00060583 File Offset: 0x0005E783
		public static Guid ServerId { get; set; }

		// Token: 0x06001BBD RID: 7101 RVA: 0x0006058C File Offset: 0x0005E78C
		[MBCallback(null, false)]
		public static string GetServerId()
		{
			return MBMultiplayerData.ServerId.ToString();
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x000605AC File Offset: 0x0005E7AC
		[MBCallback(null, false)]
		public static string GetServerName()
		{
			return MBMultiplayerData.ServerName;
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x000605B3 File Offset: 0x0005E7B3
		[MBCallback(null, false)]
		public static string GetGameModule()
		{
			return MBMultiplayerData.GameModule;
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x000605BA File Offset: 0x0005E7BA
		[MBCallback(null, false)]
		public static string GetGameType()
		{
			return MBMultiplayerData.GameType;
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x000605C1 File Offset: 0x0005E7C1
		[MBCallback(null, false)]
		public static string GetMap()
		{
			return MBMultiplayerData.Map;
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x000605C8 File Offset: 0x0005E7C8
		[MBCallback(null, false)]
		public static int GetCurrentPlayerCount()
		{
			return GameNetwork.NetworkPeerCount;
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x000605CF File Offset: 0x0005E7CF
		[MBCallback(null, false)]
		public static int GetPlayerCountLimit()
		{
			return MBMultiplayerData.PlayerCountLimit;
		}

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06001BC4 RID: 7108 RVA: 0x000605D8 File Offset: 0x0005E7D8
		// (remove) Token: 0x06001BC5 RID: 7109 RVA: 0x0006060C File Offset: 0x0005E80C
		public static event MBMultiplayerData.GameServerInfoReceivedDelegate GameServerInfoReceived;

		// Token: 0x06001BC6 RID: 7110 RVA: 0x00060640 File Offset: 0x0005E840
		[MBCallback(null, false)]
		public static void UpdateGameServerInfo(string id, string gameServer, string gameModule, string gameType, string map, int currentPlayerCount, int maxPlayerCount, string address, int port)
		{
			if (MBMultiplayerData.GameServerInfoReceived != null)
			{
				MBMultiplayerData.GameServerInfoReceived(new CustomBattleId(Guid.Parse(id)), gameServer, gameModule, gameType, map, currentPlayerCount, maxPlayerCount, address, port);
			}
		}

		// Token: 0x04000919 RID: 2329
		public static string ServerName;

		// Token: 0x0400091A RID: 2330
		public static string GameModule;

		// Token: 0x0400091B RID: 2331
		public static string GameType;

		// Token: 0x0400091C RID: 2332
		public static string Map;

		// Token: 0x0400091D RID: 2333
		public static int PlayerCountLimit;

		// Token: 0x0200050A RID: 1290
		// (Invoke) Token: 0x06003BB3 RID: 15283
		public delegate void GameServerInfoReceivedDelegate(CustomBattleId id, string gameServer, string gameModule, string gameType, string map, int currentPlayerCount, int maxPlayerCount, string address, int port);
	}
}
