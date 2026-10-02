using System;
using TaleWorlds.MountAndBlade.Diamond;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200031E RID: 798
	public static class NetworkMain
	{
		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06002D7D RID: 11645 RVA: 0x000AFEB6 File Offset: 0x000AE0B6
		// (set) Token: 0x06002D7E RID: 11646 RVA: 0x000AFEBD File Offset: 0x000AE0BD
		public static LobbyClient GameClient { get; private set; }

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06002D7F RID: 11647 RVA: 0x000AFEC5 File Offset: 0x000AE0C5
		// (set) Token: 0x06002D80 RID: 11648 RVA: 0x000AFECC File Offset: 0x000AE0CC
		public static CommunityClient CommunityClient { get; private set; }

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06002D81 RID: 11649 RVA: 0x000AFED4 File Offset: 0x000AE0D4
		// (set) Token: 0x06002D82 RID: 11650 RVA: 0x000AFEDB File Offset: 0x000AE0DB
		public static CustomBattleServer CustomBattleServer { get; private set; }

		// Token: 0x06002D83 RID: 11651 RVA: 0x000AFEE3 File Offset: 0x000AE0E3
		public static void SetPeers(LobbyClient gameClient, CommunityClient communityClient, CustomBattleServer customBattleServer)
		{
			NetworkMain.GameClient = gameClient;
			NetworkMain.CommunityClient = communityClient;
			NetworkMain.CustomBattleServer = customBattleServer;
		}
	}
}
