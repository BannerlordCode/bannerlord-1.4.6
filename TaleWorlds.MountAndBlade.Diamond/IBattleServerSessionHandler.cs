using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200011B RID: 283
	public interface IBattleServerSessionHandler
	{
		// Token: 0x06000654 RID: 1620
		void OnConnected();

		// Token: 0x06000655 RID: 1621
		void OnCantConnect();

		// Token: 0x06000656 RID: 1622
		void OnDisconnected();

		// Token: 0x06000657 RID: 1623
		void OnNewPlayer(BattlePeer peer);

		// Token: 0x06000658 RID: 1624
		void OnStartGame(string sceneName, string gameType, string faction1, string faction2, int minRequiredPlayerCountToStartBattle, int battleSize, string[] profanityList, string[] allowList);

		// Token: 0x06000659 RID: 1625
		void OnPlayerFledBattle(BattlePeer peer, out BattleResult battleResult, bool isQuitFromBattle);

		// Token: 0x0600065A RID: 1626
		void OnEndMission();

		// Token: 0x0600065B RID: 1627
		void OnStopServer();
	}
}
