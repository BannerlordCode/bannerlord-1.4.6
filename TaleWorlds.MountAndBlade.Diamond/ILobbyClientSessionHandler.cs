using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Diamond.Ranked;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200012C RID: 300
	public interface ILobbyClientSessionHandler
	{
		// Token: 0x060007E4 RID: 2020
		void OnConnected();

		// Token: 0x060007E5 RID: 2021
		void OnCantConnect();

		// Token: 0x060007E6 RID: 2022
		void OnDisconnected(TextObject feedback);

		// Token: 0x060007E7 RID: 2023
		void OnPlayerDataReceived(PlayerData playerData);

		// Token: 0x060007E8 RID: 2024
		void OnPendingRejoin();

		// Token: 0x060007E9 RID: 2025
		void OnBattleResultReceived();

		// Token: 0x060007EA RID: 2026
		void OnBattleServerInformationReceived(BattleServerInformationForClient battleServerInformation);

		// Token: 0x060007EB RID: 2027
		void OnBattleServerLost();

		// Token: 0x060007EC RID: 2028
		void OnCancelJoiningBattle();

		// Token: 0x060007ED RID: 2029
		void OnRejoinRequestRejected();

		// Token: 0x060007EE RID: 2030
		void OnFindGameAnswer(bool successful, string[] selectedAndDisabledGameTypes, bool isRejoin);

		// Token: 0x060007EF RID: 2031
		void OnEnterBattleWithPartyAnswer(string[] selectedGameTypes);

		// Token: 0x060007F0 RID: 2032
		void OnWhisperMessageReceived(string fromPlayer, string toPlayer, string message);

		// Token: 0x060007F1 RID: 2033
		void OnClanMessageReceived(string playerName, string message);

		// Token: 0x060007F2 RID: 2034
		void OnPartyMessageReceived(string playerName, string message);

		// Token: 0x060007F3 RID: 2035
		void OnSystemMessageReceived(string message);

		// Token: 0x060007F4 RID: 2036
		void OnAdminMessageReceived(string message);

		// Token: 0x060007F5 RID: 2037
		void OnGameClientStateChange(LobbyClient.State oldState);

		// Token: 0x060007F6 RID: 2038
		void OnCustomGameServerListReceived(AvailableCustomGames customGameServerList);

		// Token: 0x060007F7 RID: 2039
		void OnPartyInvitationReceived(string inviterPlayerName, PlayerId inviterPlayerId);

		// Token: 0x060007F8 RID: 2040
		void OnPartyJoinRequestReceived(PlayerId playerId, PlayerId viaPlayerId, string viaFriendName);

		// Token: 0x060007F9 RID: 2041
		void OnPartyInvitationInvalidated();

		// Token: 0x060007FA RID: 2042
		void OnPlayerInvitedToParty(PlayerId playerId);

		// Token: 0x060007FB RID: 2043
		void OnPlayersAddedToParty([TupleElementNames(new string[] { "PlayerId", "PlayerName", "IsPartyLeader" })] List<ValueTuple<PlayerId, string, bool>> addedPlayers, [TupleElementNames(new string[] { "PlayerId", "PlayerName" })] List<ValueTuple<PlayerId, string>> invitedPlayers);

		// Token: 0x060007FC RID: 2044
		void OnPlayerRemovedFromParty(PlayerId playerId, PartyRemoveReason reason);

		// Token: 0x060007FD RID: 2045
		void OnPlayerAssignedPartyLeader(PlayerId partyLeaderId);

		// Token: 0x060007FE RID: 2046
		void OnPlayerSuggestedToParty(PlayerId playerId, string playerName, PlayerId suggestingPlayerId, string suggestingPlayerName);

		// Token: 0x060007FF RID: 2047
		void OnServerStatusReceived(ServerStatus serverStatus);

		// Token: 0x06000800 RID: 2048
		void OnSigilChanged();

		// Token: 0x06000801 RID: 2049
		void OnFriendListReceived(FriendInfo[] friends);

		// Token: 0x06000802 RID: 2050
		void OnRecentPlayerStatusesReceived(FriendInfo[] friends);

		// Token: 0x06000803 RID: 2051
		void OnNotificationsReceived(LobbyNotification[] notifications);

		// Token: 0x06000804 RID: 2052
		void OnClanInvitationReceived(string clanName, string clanTag, bool isCreation);

		// Token: 0x06000805 RID: 2053
		void OnClanInvitationAnswered(PlayerId playerId, ClanCreationAnswer answer);

		// Token: 0x06000806 RID: 2054
		void OnClanCreationSuccessful();

		// Token: 0x06000807 RID: 2055
		void OnClanCreationFailed();

		// Token: 0x06000808 RID: 2056
		void OnClanCreationStarted();

		// Token: 0x06000809 RID: 2057
		void OnClanInfoChanged();

		// Token: 0x0600080A RID: 2058
		void OnPremadeGameEligibilityStatusReceived(bool isEligible);

		// Token: 0x0600080B RID: 2059
		void OnPremadeGameCreated();

		// Token: 0x0600080C RID: 2060
		void OnPremadeGameListReceived();

		// Token: 0x0600080D RID: 2061
		void OnPremadeGameCreationCancelled();

		// Token: 0x0600080E RID: 2062
		void OnJoinPremadeGameRequested(string clanName, string clanSigilCode, Guid partyId, PlayerId[] challengerPlayerIDs, PlayerId challengerPartyLeaderID, PremadeGameType premadeGameType);

		// Token: 0x0600080F RID: 2063
		void OnJoinPremadeGameRequestSuccessful();

		// Token: 0x06000810 RID: 2064
		void OnQuitFromMatchmakerGame();

		// Token: 0x06000811 RID: 2065
		void OnMatchmakerGameOver(int oldExperience, int newExperience, List<string> badgesEarned, int lootGained, RankBarInfo oldRankBarInfo, RankBarInfo newRankBarInfo, BattleCancelReason battleCancelReason);

		// Token: 0x06000812 RID: 2066
		void OnRemovedFromMatchmakerGame(DisconnectType disconnectType);

		// Token: 0x06000813 RID: 2067
		void OnRejoinBattleRequestAnswered(bool isSuccessful);

		// Token: 0x06000814 RID: 2068
		void OnRegisterCustomGameServerResponse();

		// Token: 0x06000815 RID: 2069
		void OnCustomGameEnd();

		// Token: 0x06000816 RID: 2070
		PlayerJoinGameResponseDataFromHost[] OnClientWantsToConnectCustomGame(PlayerJoinGameData[] playerJoinData);

		// Token: 0x06000817 RID: 2071
		void OnClientQuitFromCustomGame(PlayerId playerId);

		// Token: 0x06000818 RID: 2072
		void OnJoinCustomGameResponse(bool success, JoinGameData joinGameData, CustomGameJoinResponse failureReason, bool isAdmin);

		// Token: 0x06000819 RID: 2073
		void OnJoinCustomGameFailureResponse(CustomGameJoinResponse response);

		// Token: 0x0600081A RID: 2074
		void OnQuitFromCustomGame();

		// Token: 0x0600081B RID: 2075
		void OnRemovedFromCustomGame(DisconnectType disconnectType);

		// Token: 0x0600081C RID: 2076
		void OnAnnouncementReceived(Announcement announcement);

		// Token: 0x0600081D RID: 2077
		Task<bool> OnInviteToPlatformSession(PlayerId playerId);

		// Token: 0x0600081E RID: 2078
		void OnEnterCustomBattleWithPartyAnswer();
	}
}
