using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Diamond;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002E3 RID: 739
	public static class BannerlordNetwork
	{
		// Token: 0x06002AC4 RID: 10948 RVA: 0x000A47B0 File Offset: 0x000A29B0
		private static PlayerConnectionInfo CreateServerPeerConnectionInfo()
		{
			LobbyClient gameClient = NetworkMain.GameClient;
			PlayerConnectionInfo playerConnectionInfo = new PlayerConnectionInfo(gameClient.PlayerID);
			PlayerData playerData = gameClient.PlayerData;
			playerConnectionInfo.AddParameter("PlayerData", playerData);
			playerConnectionInfo.AddParameter("UsedCosmetics", gameClient.UsedCosmetics);
			playerConnectionInfo.Name = gameClient.Name;
			return playerConnectionInfo;
		}

		// Token: 0x06002AC5 RID: 10949 RVA: 0x000A47FE File Offset: 0x000A29FE
		public static void CreateServerPeer()
		{
			if (MBCommon.CurrentGameType == MBCommon.GameType.MultiClientServer)
			{
				GameNetwork.AddNewPlayerOnServer(BannerlordNetwork.CreateServerPeerConnectionInfo(), true, true);
			}
		}

		// Token: 0x06002AC6 RID: 10950 RVA: 0x000A4815 File Offset: 0x000A2A15
		public static void StartMultiplayerLobbyMission(LobbyMissionType lobbyMissionType)
		{
			BannerlordNetwork.LobbyMissionType = lobbyMissionType;
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x000A4820 File Offset: 0x000A2A20
		public static void EndMultiplayerLobbyMission()
		{
			MissionState missionState = Game.Current.GameStateManager.ActiveState as MissionState;
			if (missionState != null && missionState.CurrentMission != null && !missionState.CurrentMission.MissionEnded)
			{
				if (missionState.CurrentMission.CurrentState != Mission.State.Continuing)
				{
					Debug.Print("Remove From Game: Begin delayed disconnect from server.".ToUpper(), 0, Debug.DebugColor.White, 17179869184UL);
					missionState.BeginDelayedDisconnectFromMission();
				}
				else
				{
					Debug.Print("Remove From Game: Begin instant disconnect from server.".ToUpper(), 0, Debug.DebugColor.White, 17179869184UL);
					missionState.CurrentMission.EndMission();
				}
				MBDebug.Print("Starting to clean up the current mission now.", 0, Debug.DebugColor.White, 17179869184UL);
			}
			ChatBox gameHandler = Game.Current.GetGameHandler<ChatBox>();
			if (gameHandler != null)
			{
				gameHandler.ResetMuteList();
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06002AC8 RID: 10952 RVA: 0x000A48DE File Offset: 0x000A2ADE
		// (set) Token: 0x06002AC9 RID: 10953 RVA: 0x000A48E5 File Offset: 0x000A2AE5
		public static LobbyMissionType LobbyMissionType { get; private set; }

		// Token: 0x04001043 RID: 4163
		public const int DefaultPort = 9999;
	}
}
