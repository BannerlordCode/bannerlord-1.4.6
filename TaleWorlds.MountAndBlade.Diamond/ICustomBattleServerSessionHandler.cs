using System;
using System.Threading.Tasks;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200011C RID: 284
	public interface ICustomBattleServerSessionHandler
	{
		// Token: 0x0600065C RID: 1628
		void OnConnected();

		// Token: 0x0600065D RID: 1629
		void OnCantConnect();

		// Token: 0x0600065E RID: 1630
		void OnDisconnected();

		// Token: 0x0600065F RID: 1631
		void OnStateChanged(CustomBattleServer.State state);

		// Token: 0x06000660 RID: 1632
		void OnSuccessfulGameRegister();

		// Token: 0x06000661 RID: 1633
		Task<PlayerJoinGameResponseDataFromHost[]> OnClientWantsToConnectCustomGame(PlayerJoinGameData[] playerJoinData);

		// Token: 0x06000662 RID: 1634
		void OnClientQuitFromCustomGame(PlayerId playerId);

		// Token: 0x06000663 RID: 1635
		void OnGameFinished();

		// Token: 0x06000664 RID: 1636
		void OnChatFilterListsReceived(string[] profanityList, string[] allowList);

		// Token: 0x06000665 RID: 1637
		void OnPlayerKickRequested(PlayerId playerID, bool isBanning);
	}
}
