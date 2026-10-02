using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NetworkMessages.FromClient;
using TaleWorlds.Core;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002F1 RID: 753
	public static class GameNetwork
	{
		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06002AE9 RID: 10985 RVA: 0x000A50B9 File Offset: 0x000A32B9
		public static bool IsServer
		{
			get
			{
				return MBCommon.CurrentGameType == MBCommon.GameType.MultiServer || MBCommon.CurrentGameType == MBCommon.GameType.MultiClientServer;
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06002AEA RID: 10986 RVA: 0x000A50CD File Offset: 0x000A32CD
		public static bool IsServerOrRecorder
		{
			get
			{
				return GameNetwork.IsServer || MBCommon.CurrentGameType == MBCommon.GameType.SingleRecord;
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06002AEB RID: 10987 RVA: 0x000A50E0 File Offset: 0x000A32E0
		public static bool IsClient
		{
			get
			{
				return MBCommon.CurrentGameType == MBCommon.GameType.MultiClient;
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06002AEC RID: 10988 RVA: 0x000A50EA File Offset: 0x000A32EA
		public static bool IsReplay
		{
			get
			{
				return MBCommon.CurrentGameType == MBCommon.GameType.SingleReplay;
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06002AED RID: 10989 RVA: 0x000A50F4 File Offset: 0x000A32F4
		public static bool IsClientOrReplay
		{
			get
			{
				return GameNetwork.IsClient || GameNetwork.IsReplay;
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06002AEE RID: 10990 RVA: 0x000A5104 File Offset: 0x000A3304
		public static bool IsDedicatedServer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06002AEF RID: 10991 RVA: 0x000A5107 File Offset: 0x000A3307
		public static bool MultiplayerDisabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06002AF0 RID: 10992 RVA: 0x000A510A File Offset: 0x000A330A
		public static bool IsMultiplayer
		{
			get
			{
				return GameNetwork.IsServer || GameNetwork.IsClient;
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06002AF1 RID: 10993 RVA: 0x000A511A File Offset: 0x000A331A
		public static bool IsMultiplayerOrReplay
		{
			get
			{
				return GameNetwork.IsMultiplayer || GameNetwork.IsReplay;
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06002AF2 RID: 10994 RVA: 0x000A512A File Offset: 0x000A332A
		public static bool IsSessionActive
		{
			get
			{
				return GameNetwork.IsServerOrRecorder || GameNetwork.IsClientOrReplay;
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06002AF3 RID: 10995 RVA: 0x000A513A File Offset: 0x000A333A
		public static IEnumerable<NetworkCommunicator> NetworkPeersIncludingDisconnectedPeers
		{
			get
			{
				foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeers)
				{
					yield return networkCommunicator;
				}
				List<NetworkCommunicator>.Enumerator enumerator = default(List<NetworkCommunicator>.Enumerator);
				int num;
				for (int i = 0; i < GameNetwork.DisconnectedNetworkPeers.Count; i = num + 1)
				{
					yield return GameNetwork.DisconnectedNetworkPeers[i];
					num = i;
				}
				yield break;
				yield break;
			}
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06002AF4 RID: 10996 RVA: 0x000A5143 File Offset: 0x000A3343
		// (set) Token: 0x06002AF5 RID: 10997 RVA: 0x000A514A File Offset: 0x000A334A
		public static VirtualPlayer[] VirtualPlayers { get; private set; }

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06002AF6 RID: 10998 RVA: 0x000A5152 File Offset: 0x000A3352
		// (set) Token: 0x06002AF7 RID: 10999 RVA: 0x000A5159 File Offset: 0x000A3359
		public static List<NetworkCommunicator> NetworkPeers { get; private set; }

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06002AF8 RID: 11000 RVA: 0x000A5161 File Offset: 0x000A3361
		// (set) Token: 0x06002AF9 RID: 11001 RVA: 0x000A5168 File Offset: 0x000A3368
		public static List<NetworkCommunicator> DisconnectedNetworkPeers { get; private set; }

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06002AFA RID: 11002 RVA: 0x000A5170 File Offset: 0x000A3370
		public static int NetworkPeerCount
		{
			get
			{
				return GameNetwork.NetworkPeers.Count;
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06002AFB RID: 11003 RVA: 0x000A517C File Offset: 0x000A337C
		public static bool NetworkPeersValid
		{
			get
			{
				return GameNetwork.NetworkPeers != null;
			}
		}

		// Token: 0x06002AFC RID: 11004 RVA: 0x000A5186 File Offset: 0x000A3386
		private static void AddNetworkPeer(NetworkCommunicator networkPeer)
		{
			GameNetwork.NetworkPeers.Add(networkPeer);
			Debug.Print("AddNetworkPeer: " + networkPeer.UserName, 0, Debug.DebugColor.White, 17179869184UL);
		}

		// Token: 0x06002AFD RID: 11005 RVA: 0x000A51B4 File Offset: 0x000A33B4
		private static void RemoveNetworkPeer(NetworkCommunicator networkPeer)
		{
			Debug.Print("RemoveNetworkPeer: " + networkPeer.UserName, 0, Debug.DebugColor.White, 17179869184UL);
			GameNetwork.NetworkPeers.Remove(networkPeer);
		}

		// Token: 0x06002AFE RID: 11006 RVA: 0x000A51E3 File Offset: 0x000A33E3
		private static void AddToDisconnectedPeers(NetworkCommunicator networkPeer)
		{
			Debug.Print("AddToDisconnectedPeers: " + networkPeer.UserName, 0, Debug.DebugColor.White, 17179869184UL);
			GameNetwork.DisconnectedNetworkPeers.Add(networkPeer);
		}

		// Token: 0x06002AFF RID: 11007 RVA: 0x000A5214 File Offset: 0x000A3414
		public static void ClearAllPeers()
		{
			if (GameNetwork.VirtualPlayers != null)
			{
				for (int i = 0; i < GameNetwork.VirtualPlayers.Length; i++)
				{
					GameNetwork.VirtualPlayers[i] = null;
				}
				GameNetwork.NetworkPeers.Clear();
				GameNetwork.DisconnectedNetworkPeers.Clear();
			}
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x000A5258 File Offset: 0x000A3458
		public static NetworkCommunicator FindNetworkPeer(int index)
		{
			foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeers)
			{
				if (networkCommunicator.Index == index)
				{
					return networkCommunicator;
				}
			}
			return null;
		}

		// Token: 0x06002B01 RID: 11009 RVA: 0x000A52B4 File Offset: 0x000A34B4
		public static void Initialize(IGameNetworkHandler handler)
		{
			GameNetwork._handler = handler;
			GameNetwork.VirtualPlayers = new VirtualPlayer[1023];
			GameNetwork.NetworkPeers = new List<NetworkCommunicator>();
			GameNetwork.DisconnectedNetworkPeers = new List<NetworkCommunicator>();
			MBNetwork.Initialize(new NetworkCommunication());
			GameNetwork.NetworkComponents = new List<UdpNetworkComponent>();
			GameNetwork.NetworkHandlers = new List<IUdpNetworkHandler>();
			GameNetwork._handler.OnInitialize();
		}

		// Token: 0x06002B02 RID: 11010 RVA: 0x000A5314 File Offset: 0x000A3514
		internal static void Tick(float dt)
		{
			int i = 0;
			try
			{
				for (i = 0; i < GameNetwork.NetworkHandlers.Count; i++)
				{
					GameNetwork.NetworkHandlers[i].OnUdpNetworkHandlerTick(dt);
				}
			}
			catch (Exception ex)
			{
				if (GameNetwork.NetworkHandlers.Count > 0 && i < GameNetwork.NetworkHandlers.Count && GameNetwork.NetworkHandlers[i] != null)
				{
					string text = GameNetwork.NetworkHandlers[i].ToString();
					Debug.Print("Exception On Network Component: " + text, 0, Debug.DebugColor.White, 17592186044416UL);
				}
				Debug.Print(ex.StackTrace, 0, Debug.DebugColor.White, 17592186044416UL);
				Debug.Print(ex.Message, 0, Debug.DebugColor.White, 17592186044416UL);
			}
		}

		// Token: 0x06002B03 RID: 11011 RVA: 0x000A53E4 File Offset: 0x000A35E4
		private static void StartMultiplayer()
		{
			VirtualPlayer.Reset();
			GameNetwork._handler.OnStartMultiplayer();
		}

		// Token: 0x06002B04 RID: 11012 RVA: 0x000A53F8 File Offset: 0x000A35F8
		public static void EndMultiplayer()
		{
			GameNetwork._handler.OnEndMultiplayer();
			for (int i = GameNetwork.NetworkComponents.Count - 1; i >= 0; i--)
			{
				GameNetwork.DestroyComponent(GameNetwork.NetworkComponents[i]);
			}
			for (int j = GameNetwork.NetworkHandlers.Count - 1; j >= 0; j--)
			{
				GameNetwork.RemoveNetworkHandler(GameNetwork.NetworkHandlers[j]);
			}
			if (GameNetwork.IsServer)
			{
				GameNetwork.TerminateServerSide();
			}
			if (GameNetwork.IsClientOrReplay)
			{
				GameNetwork.AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Remove);
			}
			if (GameNetwork.IsClient)
			{
				GameNetwork.TerminateClientSide();
			}
			Debug.Print("Clearing peers list with count " + GameNetwork.NetworkPeerCount, 0, Debug.DebugColor.White, 17592186044416UL);
			GameNetwork.ClearAllPeers();
			VirtualPlayer.Reset();
			GameNetwork.MyPeer = null;
			Debug.Print("NetworkManager::HandleMultiplayerEnd", 0, Debug.DebugColor.White, 17592186044416UL);
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x000A54D0 File Offset: 0x000A36D0
		[MBCallback(null, false)]
		internal static void HandleRemovePlayer(MBNetworkPeer peer, bool isTimedOut)
		{
			DisconnectInfo disconnectInfo;
			if ((disconnectInfo = peer.NetworkPeer.PlayerConnectionInfo.GetParameter<DisconnectInfo>("DisconnectInfo")) == null)
			{
				(disconnectInfo = new DisconnectInfo()).Type = DisconnectType.QuitFromGame;
			}
			DisconnectInfo disconnectInfo2 = disconnectInfo;
			disconnectInfo2.Type = (isTimedOut ? DisconnectType.TimedOut : disconnectInfo2.Type);
			peer.NetworkPeer.PlayerConnectionInfo.AddParameter("DisconnectInfo", disconnectInfo2);
			GameNetwork.HandleRemovePlayerInternal(peer.NetworkPeer, peer.NetworkPeer.IsSynchronized && MultiplayerIntermissionVotingManager.Instance.CurrentVoteState == MultiplayerIntermissionState.Idle);
		}

		// Token: 0x06002B06 RID: 11014 RVA: 0x000A5554 File Offset: 0x000A3754
		internal static void HandleRemovePlayerInternal(NetworkCommunicator networkPeer, bool isDisconnected)
		{
			if (GameNetwork.IsClient && networkPeer.IsMine)
			{
				GameNetwork.HandleDisconnect();
				return;
			}
			GameNetwork._handler.OnPlayerDisconnectedFromServer(networkPeer);
			if (GameNetwork.IsServer)
			{
				foreach (IUdpNetworkHandler udpNetworkHandler in GameNetwork.NetworkHandlers)
				{
					udpNetworkHandler.HandleEarlyPlayerDisconnect(networkPeer);
				}
				foreach (IUdpNetworkHandler udpNetworkHandler2 in GameNetwork.NetworkHandlers)
				{
					udpNetworkHandler2.HandlePlayerDisconnect(networkPeer);
				}
			}
			foreach (IUdpNetworkHandler udpNetworkHandler3 in GameNetwork.NetworkHandlers)
			{
				udpNetworkHandler3.OnPlayerDisconnectedFromServer(networkPeer);
			}
			GameNetwork.RemoveNetworkPeer(networkPeer);
			if (isDisconnected)
			{
				GameNetwork.AddToDisconnectedPeers(networkPeer);
			}
			GameNetwork.VirtualPlayers[networkPeer.VirtualPlayer.Index] = null;
			if (GameNetwork.IsServer)
			{
				foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeers)
				{
					if (!networkCommunicator.IsServerPeer)
					{
						GameNetwork.BeginModuleEventAsServer(networkCommunicator);
						GameNetwork.WriteMessage(new DeletePlayer(networkPeer.Index, isDisconnected));
						GameNetwork.EndModuleEventAsServer();
					}
				}
			}
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x000A56D0 File Offset: 0x000A38D0
		[MBCallback(null, false)]
		internal static void HandleDisconnect()
		{
			GameNetwork._handler.OnDisconnectedFromServer();
			foreach (IUdpNetworkHandler udpNetworkHandler in GameNetwork.NetworkHandlers)
			{
				udpNetworkHandler.OnDisconnectedFromServer();
			}
			GameNetwork.MyPeer = null;
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x000A5730 File Offset: 0x000A3930
		public static void StartReplay()
		{
			GameNetwork._handler.OnStartReplay();
		}

		// Token: 0x06002B09 RID: 11017 RVA: 0x000A573C File Offset: 0x000A393C
		public static void EndReplay()
		{
			GameNetwork._handler.OnEndReplay();
		}

		// Token: 0x06002B0A RID: 11018 RVA: 0x000A5748 File Offset: 0x000A3948
		public static void PreStartMultiplayerOnServer()
		{
			MBCommon.CurrentGameType = (GameNetwork.IsDedicatedServer ? MBCommon.GameType.MultiServer : MBCommon.GameType.MultiClientServer);
			GameNetwork.ClientPeerIndex = -1;
		}

		// Token: 0x06002B0B RID: 11019 RVA: 0x000A5760 File Offset: 0x000A3960
		public static void StartMultiplayerOnServer(int port)
		{
			Debug.Print("StartMultiplayerOnServer", 0, Debug.DebugColor.White, 17592186044416UL);
			GameNetwork.PreStartMultiplayerOnServer();
			GameNetwork.InitializeServerSide(port);
			GameNetwork.StartMultiplayer();
		}

		// Token: 0x06002B0C RID: 11020 RVA: 0x000A5788 File Offset: 0x000A3988
		[MBCallback(null, false)]
		internal static bool HandleNetworkPacketAsServer(MBNetworkPeer networkPeer)
		{
			return GameNetwork.HandleNetworkPacketAsServer(networkPeer.NetworkPeer);
		}

		// Token: 0x06002B0D RID: 11021 RVA: 0x000A5798 File Offset: 0x000A3998
		internal static bool HandleNetworkPacketAsServer(NetworkCommunicator networkPeer)
		{
			if (networkPeer == null)
			{
				Debug.Print("networkPeer == null", 0, Debug.DebugColor.White, 17592186044416UL);
				return false;
			}
			bool flag = true;
			try
			{
				int num = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.NetworkComponentEventTypeFromClientCompressionInfo, ref flag);
				if (flag)
				{
					if (num >= 0 && num < GameNetwork._gameNetworkMessageIdsFromClient.Count)
					{
						GameNetworkMessage gameNetworkMessage = Activator.CreateInstance(GameNetwork._gameNetworkMessageIdsFromClient[num]) as GameNetworkMessage;
						gameNetworkMessage.MessageId = num;
						flag = gameNetworkMessage.Read();
						if (flag)
						{
							bool flag2 = false;
							bool flag3 = true;
							List<GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>> list;
							if (GameNetwork._fromClientBaseMessageHandlers.TryGetValue(num, out list))
							{
								foreach (GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage> clientMessageHandlerDelegate in list)
								{
									flag = flag && clientMessageHandlerDelegate(networkPeer, gameNetworkMessage);
									if (!flag)
									{
										break;
									}
								}
								flag3 = false;
								flag2 = list.Count != 0;
							}
							List<object> list2;
							if (GameNetwork._fromClientMessageHandlers.TryGetValue(num, out list2))
							{
								foreach (object obj in list2)
								{
									Delegate @delegate = obj as Delegate;
									flag = flag && (bool)@delegate.DynamicInvokeWithLog(new object[] { networkPeer, gameNetworkMessage });
									if (!flag)
									{
										break;
									}
								}
								flag3 = false;
								flag2 = flag2 || list2.Count != 0;
							}
							if (flag3)
							{
								Debug.FailedAssert("Unknown network messageId " + gameNetworkMessage, "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Network\\GameNetwork.cs", "HandleNetworkPacketAsServer", 760);
								flag = false;
							}
							else if (!flag2)
							{
								Debug.Print("Handler not found for network message " + gameNetworkMessage, 0, Debug.DebugColor.White, 17179869184UL);
							}
						}
					}
					else
					{
						Debug.Print("Handler not found for network message " + num.ToString(), 0, Debug.DebugColor.White, 17179869184UL);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.Print("error " + ex.Message, 0, Debug.DebugColor.White, 17592186044416UL);
				return false;
			}
			return flag;
		}

		// Token: 0x06002B0E RID: 11022 RVA: 0x000A59D8 File Offset: 0x000A3BD8
		[MBCallback(null, false)]
		public static void HandleConsoleCommand(string command)
		{
			if (GameNetwork._handler != null)
			{
				GameNetwork._handler.OnHandleConsoleCommand(command);
			}
		}

		// Token: 0x06002B0F RID: 11023 RVA: 0x000A59EC File Offset: 0x000A3BEC
		private static void InitializeServerSide(int port)
		{
			MBAPI.IMBNetwork.InitializeServerSide(port);
		}

		// Token: 0x06002B10 RID: 11024 RVA: 0x000A59F9 File Offset: 0x000A3BF9
		private static void TerminateServerSide()
		{
			MBAPI.IMBNetwork.TerminateServerSide();
			if (!GameNetwork.IsDedicatedServer)
			{
				MBCommon.CurrentGameType = MBCommon.GameType.Single;
			}
		}

		// Token: 0x06002B11 RID: 11025 RVA: 0x000A5A12 File Offset: 0x000A3C12
		private static void PrepareNewUdpSession(int peerIndex, int sessionKey)
		{
			MBAPI.IMBNetwork.PrepareNewUdpSession(peerIndex, sessionKey);
		}

		// Token: 0x06002B12 RID: 11026 RVA: 0x000A5A20 File Offset: 0x000A3C20
		public static string GetActiveUdpSessionsIpAddress()
		{
			return MBAPI.IMBNetwork.GetActiveUdpSessionsIpAddress();
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x000A5A2C File Offset: 0x000A3C2C
		public static ICommunicator AddNewPlayerOnServer(PlayerConnectionInfo playerConnectionInfo, bool serverPeer, bool isAdmin)
		{
			bool flag = playerConnectionInfo == null;
			int num = (flag ? MBAPI.IMBNetwork.AddNewBotOnServer() : MBAPI.IMBNetwork.AddNewPlayerOnServer(serverPeer));
			Debug.Print(string.Concat(new object[] { "AddNewPlayerOnServer: ", playerConnectionInfo.Name, " index: ", num }), 0, Debug.DebugColor.White, 17179869184UL);
			if (num >= 0)
			{
				int num2 = 0;
				if (!serverPeer)
				{
					num2 = GameNetwork.GetSessionKeyForPlayer();
				}
				int num3 = -1;
				ICommunicator communicator = null;
				if (flag)
				{
					communicator = DummyCommunicator.CreateAsServer(num, "");
				}
				else
				{
					for (int i = 0; i < GameNetwork.DisconnectedNetworkPeers.Count; i++)
					{
						PlayerData parameter = playerConnectionInfo.GetParameter<PlayerData>("PlayerData");
						if (parameter != null && GameNetwork.DisconnectedNetworkPeers[i].VirtualPlayer.Id == parameter.PlayerId)
						{
							num3 = i;
							communicator = GameNetwork.DisconnectedNetworkPeers[i];
							NetworkCommunicator networkCommunicator = communicator as NetworkCommunicator;
							networkCommunicator.UpdateIndexForReconnectingPlayer(num);
							networkCommunicator.UpdateConnectionInfoForReconnect(playerConnectionInfo, isAdmin);
							MBAPI.IMBPeer.SetUserData(num, new MBNetworkPeer(networkCommunicator));
							Debug.Print("RemoveFromDisconnectedPeers: " + networkCommunicator.UserName, 0, Debug.DebugColor.White, 17179869184UL);
							GameNetwork.DisconnectedNetworkPeers.RemoveAt(i);
							break;
						}
					}
					if (communicator == null)
					{
						communicator = NetworkCommunicator.CreateAsServer(playerConnectionInfo, num, isAdmin);
					}
				}
				GameNetwork.VirtualPlayers[communicator.VirtualPlayer.Index] = communicator.VirtualPlayer;
				if (!flag)
				{
					NetworkCommunicator networkCommunicator2 = communicator as NetworkCommunicator;
					if (serverPeer && GameNetwork.IsServer)
					{
						GameNetwork.ClientPeerIndex = num;
						GameNetwork.MyPeer = networkCommunicator2;
					}
					networkCommunicator2.SessionKey = num2;
					networkCommunicator2.SetServerPeer(serverPeer);
					GameNetwork.AddNetworkPeer(networkCommunicator2);
					playerConnectionInfo.NetworkPeer = networkCommunicator2;
					if (!serverPeer)
					{
						GameNetwork.PrepareNewUdpSession(num, num2);
					}
					if (num3 < 0)
					{
						GameNetwork.BeginBroadcastModuleEvent();
						GameNetwork.WriteMessage(new CreatePlayer(networkCommunicator2.Index, playerConnectionInfo.Name, num3, false, false));
						GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord | GameNetwork.EventBroadcastFlags.DontSendToPeers, null);
					}
					foreach (NetworkCommunicator networkCommunicator3 in GameNetwork.NetworkPeers)
					{
						if (networkCommunicator3 != networkCommunicator2 && networkCommunicator3 != GameNetwork.MyPeer)
						{
							GameNetwork.BeginModuleEventAsServer(networkCommunicator3);
							GameNetwork.WriteMessage(new CreatePlayer(networkCommunicator2.Index, playerConnectionInfo.Name, num3, false, false));
							GameNetwork.EndModuleEventAsServer();
						}
						if (!serverPeer)
						{
							bool flag2 = networkCommunicator3 == networkCommunicator2;
							GameNetwork.BeginModuleEventAsServer(networkCommunicator2);
							GameNetwork.WriteMessage(new CreatePlayer(networkCommunicator3.Index, networkCommunicator3.UserName, -1, false, flag2));
							GameNetwork.EndModuleEventAsServer();
						}
					}
					for (int j = 0; j < GameNetwork.DisconnectedNetworkPeers.Count; j++)
					{
						NetworkCommunicator networkCommunicator4 = GameNetwork.DisconnectedNetworkPeers[j];
						GameNetwork.BeginModuleEventAsServer(networkCommunicator2);
						GameNetwork.WriteMessage(new CreatePlayer(networkCommunicator4.Index, networkCommunicator4.UserName, j, true, false));
						GameNetwork.EndModuleEventAsServer();
					}
					foreach (IUdpNetworkHandler udpNetworkHandler in GameNetwork.NetworkHandlers)
					{
						udpNetworkHandler.HandleNewClientConnect(playerConnectionInfo);
					}
					GameNetwork._handler.OnPlayerConnectedToServer(networkCommunicator2);
				}
				return communicator;
			}
			return null;
		}

		// Token: 0x06002B14 RID: 11028 RVA: 0x000A5D74 File Offset: 0x000A3F74
		public static GameNetwork.AddPlayersResult AddNewPlayersOnServer(PlayerConnectionInfo[] playerConnectionInfos, bool serverPeer)
		{
			bool flag = MBAPI.IMBNetwork.CanAddNewPlayersOnServer(playerConnectionInfos.Length);
			NetworkCommunicator[] array = new NetworkCommunicator[playerConnectionInfos.Length];
			if (flag)
			{
				for (int i = 0; i < array.Length; i++)
				{
					object parameter = playerConnectionInfos[i].GetParameter<object>("IsAdmin");
					bool flag2 = parameter != null && (bool)parameter;
					ICommunicator communicator = GameNetwork.AddNewPlayerOnServer(playerConnectionInfos[i], serverPeer, flag2);
					array[i] = communicator as NetworkCommunicator;
				}
			}
			return new GameNetwork.AddPlayersResult
			{
				NetworkPeers = array,
				Success = flag
			};
		}

		// Token: 0x06002B15 RID: 11029 RVA: 0x000A5DF8 File Offset: 0x000A3FF8
		public static void ClientFinishedLoading(NetworkCommunicator networkPeer)
		{
			foreach (IUdpNetworkHandler udpNetworkHandler in GameNetwork.NetworkHandlers)
			{
				udpNetworkHandler.HandleEarlyNewClientAfterLoadingFinished(networkPeer);
			}
			foreach (IUdpNetworkHandler udpNetworkHandler2 in GameNetwork.NetworkHandlers)
			{
				udpNetworkHandler2.HandleNewClientAfterLoadingFinished(networkPeer);
			}
			foreach (IUdpNetworkHandler udpNetworkHandler3 in GameNetwork.NetworkHandlers)
			{
				udpNetworkHandler3.HandleLateNewClientAfterLoadingFinished(networkPeer);
			}
			networkPeer.IsSynchronized = true;
			foreach (IUdpNetworkHandler udpNetworkHandler4 in GameNetwork.NetworkHandlers)
			{
				udpNetworkHandler4.HandleNewClientAfterSynchronized(networkPeer);
			}
			foreach (IUdpNetworkHandler udpNetworkHandler5 in GameNetwork.NetworkHandlers)
			{
				udpNetworkHandler5.HandleLateNewClientAfterSynchronized(networkPeer);
			}
		}

		// Token: 0x06002B16 RID: 11030 RVA: 0x000A5F4C File Offset: 0x000A414C
		public static void BeginModuleEventAsClient()
		{
			MBAPI.IMBNetwork.BeginModuleEventAsClient(true);
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x000A5F59 File Offset: 0x000A4159
		public static void EndModuleEventAsClient()
		{
			MBAPI.IMBNetwork.EndModuleEventAsClient(true);
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x000A5F66 File Offset: 0x000A4166
		public static void BeginModuleEventAsClientUnreliable()
		{
			MBAPI.IMBNetwork.BeginModuleEventAsClient(false);
		}

		// Token: 0x06002B19 RID: 11033 RVA: 0x000A5F73 File Offset: 0x000A4173
		public static void EndModuleEventAsClientUnreliable()
		{
			MBAPI.IMBNetwork.EndModuleEventAsClient(false);
		}

		// Token: 0x06002B1A RID: 11034 RVA: 0x000A5F80 File Offset: 0x000A4180
		public static void BeginModuleEventAsServer(NetworkCommunicator communicator)
		{
			GameNetwork.BeginModuleEventAsServer(communicator.VirtualPlayer);
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x000A5F8D File Offset: 0x000A418D
		public static void BeginModuleEventAsServerUnreliable(NetworkCommunicator communicator)
		{
			GameNetwork.BeginModuleEventAsServerUnreliable(communicator.VirtualPlayer);
		}

		// Token: 0x06002B1C RID: 11036 RVA: 0x000A5F9A File Offset: 0x000A419A
		public static void BeginModuleEventAsServer(VirtualPlayer peer)
		{
			MBAPI.IMBPeer.BeginModuleEvent(peer.Index, true);
		}

		// Token: 0x06002B1D RID: 11037 RVA: 0x000A5FAD File Offset: 0x000A41AD
		public static void EndModuleEventAsServer()
		{
			MBAPI.IMBPeer.EndModuleEvent(true);
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x000A5FBA File Offset: 0x000A41BA
		public static void BeginModuleEventAsServerUnreliable(VirtualPlayer peer)
		{
			MBAPI.IMBPeer.BeginModuleEvent(peer.Index, false);
		}

		// Token: 0x06002B1F RID: 11039 RVA: 0x000A5FCD File Offset: 0x000A41CD
		public static void EndModuleEventAsServerUnreliable()
		{
			MBAPI.IMBPeer.EndModuleEvent(false);
		}

		// Token: 0x06002B20 RID: 11040 RVA: 0x000A5FDA File Offset: 0x000A41DA
		public static void BeginBroadcastModuleEvent()
		{
			MBAPI.IMBNetwork.BeginBroadcastModuleEvent();
		}

		// Token: 0x06002B21 RID: 11041 RVA: 0x000A5FE8 File Offset: 0x000A41E8
		public static void EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags broadcastFlags, NetworkCommunicator targetPlayer = null)
		{
			int num = ((targetPlayer != null) ? targetPlayer.Index : (-1));
			MBAPI.IMBNetwork.EndBroadcastModuleEvent((int)broadcastFlags, num, true);
		}

		// Token: 0x06002B22 RID: 11042 RVA: 0x000A600F File Offset: 0x000A420F
		public static double ElapsedTimeSinceLastUdpPacketArrived()
		{
			return MBAPI.IMBNetwork.ElapsedTimeSinceLastUdpPacketArrived();
		}

		// Token: 0x06002B23 RID: 11043 RVA: 0x000A601C File Offset: 0x000A421C
		public static void EndBroadcastModuleEventUnreliable(GameNetwork.EventBroadcastFlags broadcastFlags, NetworkCommunicator targetPlayer = null)
		{
			int num = ((targetPlayer != null) ? targetPlayer.Index : (-1));
			MBAPI.IMBNetwork.EndBroadcastModuleEvent((int)broadcastFlags, num, false);
		}

		// Token: 0x06002B24 RID: 11044 RVA: 0x000A6044 File Offset: 0x000A4244
		public static void UnSynchronizeEveryone()
		{
			Debug.Print("UnSynchronizeEveryone is called!", 0, Debug.DebugColor.White, 17179869184UL);
			foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeers)
			{
				networkCommunicator.IsSynchronized = false;
			}
			foreach (IUdpNetworkHandler udpNetworkHandler in GameNetwork.NetworkHandlers)
			{
				udpNetworkHandler.OnEveryoneUnSynchronized();
			}
		}

		// Token: 0x06002B25 RID: 11045 RVA: 0x000A60E8 File Offset: 0x000A42E8
		public static void AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode mode)
		{
			GameNetwork.NetworkMessageHandlerRegisterer networkMessageHandlerRegisterer = new GameNetwork.NetworkMessageHandlerRegisterer(mode);
			networkMessageHandlerRegisterer.Register<CreatePlayer>(new GameNetworkMessage.ServerMessageHandlerDelegate<CreatePlayer>(GameNetwork.HandleServerEventCreatePlayer));
			networkMessageHandlerRegisterer.Register<DeletePlayer>(new GameNetworkMessage.ServerMessageHandlerDelegate<DeletePlayer>(GameNetwork.HandleServerEventDeletePlayer));
		}

		// Token: 0x06002B26 RID: 11046 RVA: 0x000A6113 File Offset: 0x000A4313
		public static void StartMultiplayerOnClient(string serverAddress, int port, int sessionKey, int playerIndex)
		{
			Debug.Print("StartMultiplayerOnClient", 0, Debug.DebugColor.White, 17592186044416UL);
			MBCommon.CurrentGameType = MBCommon.GameType.MultiClient;
			GameNetwork.ClientPeerIndex = playerIndex;
			GameNetwork.InitializeClientSide(serverAddress, port, sessionKey, playerIndex);
			GameNetwork.StartMultiplayer();
			GameNetwork.AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Add);
		}

		// Token: 0x06002B27 RID: 11047 RVA: 0x000A614C File Offset: 0x000A434C
		[MBCallback(null, false)]
		internal static bool HandleNetworkPacketAsClient()
		{
			if (!TWParallel.IsMainThread())
			{
				Debug.FailedAssert("Network messages should be handled from main thread", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Network\\GameNetwork.cs", "HandleNetworkPacketAsClient", 1204);
			}
			bool flag = true;
			int num = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.NetworkComponentEventTypeFromServerCompressionInfo, ref flag);
			if (flag && num >= 0 && num < GameNetwork._gameNetworkMessageIdsFromServer.Count)
			{
				GameNetworkMessage gameNetworkMessage = Activator.CreateInstance(GameNetwork._gameNetworkMessageIdsFromServer[num]) as GameNetworkMessage;
				gameNetworkMessage.MessageId = num;
				Debug.Print("Reading message: " + gameNetworkMessage.GetType().Name, 0, Debug.DebugColor.White, 17179869184UL);
				flag = gameNetworkMessage.Read();
				if (flag)
				{
					if (!NetworkMain.GameClient.IsInGame && !GameNetwork.IsReplay && !NetworkMain.CommunityClient.IsInGame)
					{
						Debug.Print("ignoring post mission message: " + gameNetworkMessage.GetType().Name, 0, Debug.DebugColor.White, 17179869184UL);
					}
					else
					{
						bool flag2 = false;
						bool flag3 = true;
						if ((gameNetworkMessage.GetLogFilter() & GameNetwork.MultiplayerLogging) != MultiplayerMessageFilter.None)
						{
							if (GameNetworkMessage.IsClientMissionOver)
							{
								Debug.Print("WARNING: Entering message processing while client mission is over", 0, Debug.DebugColor.White, 17592186044416UL);
							}
							Debug.Print("Processing message: " + gameNetworkMessage.GetType().Name + ": " + gameNetworkMessage.GetLogFormat(), 0, Debug.DebugColor.White, 17179869184UL);
						}
						List<GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>> list;
						if (GameNetwork._fromServerBaseMessageHandlers.TryGetValue(num, out list))
						{
							foreach (GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage> serverMessageHandlerDelegate in list)
							{
								try
								{
									serverMessageHandlerDelegate(gameNetworkMessage);
								}
								catch
								{
									Debug.Print("Exception in handler of " + num.ToString(), 0, Debug.DebugColor.White, 17179869184UL);
									Debug.Print("Exception in handler of " + gameNetworkMessage.GetType().Name, 0, Debug.DebugColor.Red, 17179869184UL);
								}
							}
							flag3 = false;
							flag2 = list.Count != 0;
						}
						List<object> list2;
						if (GameNetwork._fromServerMessageHandlers.TryGetValue(num, out list2))
						{
							foreach (object obj in list2)
							{
								(obj as Delegate).DynamicInvokeWithLog(new object[] { gameNetworkMessage });
							}
							flag3 = false;
							flag2 = flag2 || list2.Count != 0;
						}
						if (flag3)
						{
							Debug.Print("Invalid messageId " + num.ToString(), 0, Debug.DebugColor.White, 17179869184UL);
							Debug.Print("Invalid messageId " + gameNetworkMessage.GetType().Name, 0, Debug.DebugColor.White, 17179869184UL);
						}
						else if (!flag2)
						{
							Debug.Print("No message handler found for " + gameNetworkMessage.GetType().Name, 0, Debug.DebugColor.Red, 17179869184UL);
						}
					}
				}
				else
				{
					Debug.Print("Invalid message read for: " + gameNetworkMessage.GetType().Name, 0, Debug.DebugColor.White, 17179869184UL);
				}
			}
			else
			{
				Debug.Print("Invalid message id read: " + num, 0, Debug.DebugColor.White, 17179869184UL);
			}
			return flag;
		}

		// Token: 0x06002B28 RID: 11048 RVA: 0x000A6490 File Offset: 0x000A4690
		private static int GetSessionKeyForPlayer()
		{
			return new Random(DateTime.Now.Millisecond).Next(1, 4001);
		}

		// Token: 0x06002B29 RID: 11049 RVA: 0x000A64BC File Offset: 0x000A46BC
		public static NetworkCommunicator HandleNewClientConnect(PlayerConnectionInfo playerConnectionInfo, bool isAdmin)
		{
			NetworkCommunicator networkCommunicator = GameNetwork.AddNewPlayerOnServer(playerConnectionInfo, false, isAdmin) as NetworkCommunicator;
			GameNetwork._handler.OnNewPlayerConnect(playerConnectionInfo, networkCommunicator);
			return networkCommunicator;
		}

		// Token: 0x06002B2A RID: 11050 RVA: 0x000A64E4 File Offset: 0x000A46E4
		public static GameNetwork.AddPlayersResult HandleNewClientsConnect(PlayerConnectionInfo[] playerConnectionInfos, bool isAdmin)
		{
			GameNetwork.AddPlayersResult addPlayersResult = GameNetwork.AddNewPlayersOnServer(playerConnectionInfos, isAdmin);
			if (addPlayersResult.Success)
			{
				for (int i = 0; i < playerConnectionInfos.Length; i++)
				{
					GameNetwork._handler.OnNewPlayerConnect(playerConnectionInfos[i], addPlayersResult.NetworkPeers[i]);
				}
			}
			return addPlayersResult;
		}

		// Token: 0x06002B2B RID: 11051 RVA: 0x000A6528 File Offset: 0x000A4728
		public static void AddNetworkPeerToDisconnectAsServer(NetworkCommunicator networkPeer)
		{
			Debug.Print("adding peer to disconnect index:" + networkPeer.Index, 0, Debug.DebugColor.White, 17179869184UL);
			GameNetwork.AddPeerToDisconnect(networkPeer);
			GameNetwork.BeginModuleEventAsServer(networkPeer);
			GameNetwork.WriteMessage(new DeletePlayer(networkPeer.Index, false));
			GameNetwork.EndModuleEventAsServer();
		}

		// Token: 0x06002B2C RID: 11052 RVA: 0x000A6580 File Offset: 0x000A4780
		private static void HandleServerEventCreatePlayer(CreatePlayer message)
		{
			int playerIndex = message.PlayerIndex;
			string playerName = message.PlayerName;
			bool isReceiverPeer = message.IsReceiverPeer;
			NetworkCommunicator networkCommunicator;
			if (isReceiverPeer || message.IsNonExistingDisconnectedPeer || message.DisconnectedPeerIndex < 0)
			{
				networkCommunicator = NetworkCommunicator.CreateAsClient(playerName, playerIndex);
			}
			else
			{
				networkCommunicator = GameNetwork.DisconnectedNetworkPeers[message.DisconnectedPeerIndex];
				networkCommunicator.UpdateIndexForReconnectingPlayer(message.PlayerIndex);
				Debug.Print("RemoveFromDisconnectedPeers: " + networkCommunicator.UserName, 0, Debug.DebugColor.White, 17179869184UL);
				GameNetwork.DisconnectedNetworkPeers.RemoveAt(message.DisconnectedPeerIndex);
			}
			if (isReceiverPeer)
			{
				GameNetwork.MyPeer = networkCommunicator;
			}
			if (message.IsNonExistingDisconnectedPeer)
			{
				GameNetwork.AddToDisconnectedPeers(networkCommunicator);
			}
			else
			{
				GameNetwork.VirtualPlayers[networkCommunicator.VirtualPlayer.Index] = networkCommunicator.VirtualPlayer;
				GameNetwork.AddNetworkPeer(networkCommunicator);
			}
			GameNetwork._handler.OnPlayerConnectedToServer(networkCommunicator);
		}

		// Token: 0x06002B2D RID: 11053 RVA: 0x000A6650 File Offset: 0x000A4850
		private static void HandleServerEventDeletePlayer(DeletePlayer message)
		{
			NetworkCommunicator networkCommunicator = GameNetwork.NetworkPeers.FirstOrDefault<NetworkCommunicator>((NetworkCommunicator networkPeer) => networkPeer.Index == message.PlayerIndex);
			if (networkCommunicator != null)
			{
				GameNetwork.HandleRemovePlayerInternal(networkCommunicator, message.AddToDisconnectList);
			}
		}

		// Token: 0x06002B2E RID: 11054 RVA: 0x000A6695 File Offset: 0x000A4895
		public static void InitializeClientSide(string serverAddress, int port, int sessionKey, int playerIndex)
		{
			MBAPI.IMBNetwork.InitializeClientSide(serverAddress, port, sessionKey, playerIndex);
		}

		// Token: 0x06002B2F RID: 11055 RVA: 0x000A66A5 File Offset: 0x000A48A5
		public static void TerminateClientSide()
		{
			MBAPI.IMBNetwork.TerminateClientSide();
			MBCommon.CurrentGameType = MBCommon.GameType.Single;
		}

		// Token: 0x06002B30 RID: 11056 RVA: 0x000A66B7 File Offset: 0x000A48B7
		public static Type GetSynchedMissionObjectReadableRecordTypeFromIndex(int typeIndex)
		{
			return GameNetwork._synchedMissionObjectClassTypes[typeIndex];
		}

		// Token: 0x06002B31 RID: 11057 RVA: 0x000A66C4 File Offset: 0x000A48C4
		public static int GetSynchedMissionObjectReadableRecordIndexFromType(Type type)
		{
			for (int i = 0; i < GameNetwork._synchedMissionObjectClassTypes.Count; i++)
			{
				Type type2 = GameNetwork._synchedMissionObjectClassTypes[i];
				DefineSynchedMissionObjectType customAttribute = type2.GetCustomAttribute<DefineSynchedMissionObjectType>();
				DefineSynchedMissionObjectTypeForMod customAttribute2 = type2.GetCustomAttribute<DefineSynchedMissionObjectTypeForMod>();
				Type type3 = ((customAttribute != null) ? customAttribute.Type : null) ?? ((customAttribute2 != null) ? customAttribute2.Type : null);
				Type type4 = type;
				while (type4 != null)
				{
					if (type4 == type3)
					{
						return i;
					}
					type4 = type4.BaseType;
				}
			}
			return -1;
		}

		// Token: 0x06002B32 RID: 11058 RVA: 0x000A6740 File Offset: 0x000A4940
		public static void DestroyComponent(UdpNetworkComponent udpNetworkComponent)
		{
			GameNetwork.RemoveNetworkHandler(udpNetworkComponent);
			GameNetwork.NetworkComponents.Remove(udpNetworkComponent);
		}

		// Token: 0x06002B33 RID: 11059 RVA: 0x000A6754 File Offset: 0x000A4954
		public static T AddNetworkComponent<T>() where T : UdpNetworkComponent
		{
			T t = (T)((object)Activator.CreateInstance(typeof(T), new object[0]));
			GameNetwork.NetworkComponents.Add(t);
			GameNetwork.NetworkHandlers.Add(t);
			return t;
		}

		// Token: 0x06002B34 RID: 11060 RVA: 0x000A679D File Offset: 0x000A499D
		public static void AddNetworkHandler(IUdpNetworkHandler handler)
		{
			GameNetwork.NetworkHandlers.Add(handler);
		}

		// Token: 0x06002B35 RID: 11061 RVA: 0x000A67AA File Offset: 0x000A49AA
		public static void RemoveNetworkHandler(IUdpNetworkHandler handler)
		{
			handler.OnUdpNetworkHandlerClose();
			GameNetwork.NetworkHandlers.Remove(handler);
		}

		// Token: 0x06002B36 RID: 11062 RVA: 0x000A67C0 File Offset: 0x000A49C0
		public static T GetNetworkComponent<T>() where T : UdpNetworkComponent
		{
			using (List<UdpNetworkComponent>.Enumerator enumerator = GameNetwork.NetworkComponents.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					T t;
					if ((t = enumerator.Current as T) != null)
					{
						return t;
					}
				}
			}
			return default(T);
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06002B37 RID: 11063 RVA: 0x000A682C File Offset: 0x000A4A2C
		// (set) Token: 0x06002B38 RID: 11064 RVA: 0x000A6833 File Offset: 0x000A4A33
		public static List<UdpNetworkComponent> NetworkComponents { get; private set; }

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06002B39 RID: 11065 RVA: 0x000A683B File Offset: 0x000A4A3B
		// (set) Token: 0x06002B3A RID: 11066 RVA: 0x000A6842 File Offset: 0x000A4A42
		public static List<IUdpNetworkHandler> NetworkHandlers { get; private set; }

		// Token: 0x06002B3B RID: 11067 RVA: 0x000A684C File Offset: 0x000A4A4C
		public static void WriteMessage(GameNetworkMessage message)
		{
			if ((message.GetLogFilter() & GameNetwork.MultiplayerLogging) != MultiplayerMessageFilter.None)
			{
				Debug.Print("Writing message: " + message.GetLogFormat(), 0, Debug.DebugColor.White, 17179869184UL);
			}
			Type type = message.GetType();
			message.MessageId = GameNetwork._gameNetworkMessageTypesAll[type];
			message.Write();
		}

		// Token: 0x06002B3C RID: 11068 RVA: 0x000A68A8 File Offset: 0x000A4AA8
		private static void AddServerMessageHandler<T>(GameNetworkMessage.ServerMessageHandlerDelegate<T> handler) where T : GameNetworkMessage
		{
			int num = GameNetwork._gameNetworkMessageTypesFromServer[typeof(T)];
			GameNetwork._fromServerMessageHandlers[num].Add(handler);
		}

		// Token: 0x06002B3D RID: 11069 RVA: 0x000A68DC File Offset: 0x000A4ADC
		private static void AddServerBaseMessageHandler(GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage> handler, Type messageType)
		{
			int num = GameNetwork._gameNetworkMessageTypesFromServer[messageType];
			GameNetwork._fromServerBaseMessageHandlers[num].Add(handler);
		}

		// Token: 0x06002B3E RID: 11070 RVA: 0x000A6908 File Offset: 0x000A4B08
		private static void AddClientMessageHandler<T>(GameNetworkMessage.ClientMessageHandlerDelegate<T> handler) where T : GameNetworkMessage
		{
			int num = GameNetwork._gameNetworkMessageTypesFromClient[typeof(T)];
			GameNetwork._fromClientMessageHandlers[num].Add(handler);
		}

		// Token: 0x06002B3F RID: 11071 RVA: 0x000A693C File Offset: 0x000A4B3C
		private static void AddClientBaseMessageHandler(GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage> handler, Type messageType)
		{
			int num = GameNetwork._gameNetworkMessageTypesFromClient[messageType];
			GameNetwork._fromClientBaseMessageHandlers[num].Add(handler);
		}

		// Token: 0x06002B40 RID: 11072 RVA: 0x000A6968 File Offset: 0x000A4B68
		private static void RemoveServerMessageHandler<T>(GameNetworkMessage.ServerMessageHandlerDelegate<T> handler) where T : GameNetworkMessage
		{
			int num = GameNetwork._gameNetworkMessageTypesFromServer[typeof(T)];
			GameNetwork._fromServerMessageHandlers[num].Remove(handler);
		}

		// Token: 0x06002B41 RID: 11073 RVA: 0x000A699C File Offset: 0x000A4B9C
		private static void RemoveServerBaseMessageHandler(GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage> handler, Type messageType)
		{
			int num = GameNetwork._gameNetworkMessageTypesFromServer[messageType];
			GameNetwork._fromServerBaseMessageHandlers[num].Remove(handler);
		}

		// Token: 0x06002B42 RID: 11074 RVA: 0x000A69C8 File Offset: 0x000A4BC8
		private static void RemoveClientMessageHandler<T>(GameNetworkMessage.ClientMessageHandlerDelegate<T> handler) where T : GameNetworkMessage
		{
			int num = GameNetwork._gameNetworkMessageTypesFromClient[typeof(T)];
			GameNetwork._fromClientMessageHandlers[num].Remove(handler);
		}

		// Token: 0x06002B43 RID: 11075 RVA: 0x000A69FC File Offset: 0x000A4BFC
		internal static void FindGameNetworkMessages()
		{
			Debug.Print("Searching Game NetworkMessages Methods", 0, Debug.DebugColor.White, 17179869184UL);
			GameNetwork._fromClientMessageHandlers = new Dictionary<int, List<object>>();
			GameNetwork._fromServerMessageHandlers = new Dictionary<int, List<object>>();
			GameNetwork._fromClientBaseMessageHandlers = new Dictionary<int, List<GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>>>();
			GameNetwork._fromServerBaseMessageHandlers = new Dictionary<int, List<GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>>>();
			GameNetwork._gameNetworkMessageTypesAll = new Dictionary<Type, int>();
			GameNetwork._gameNetworkMessageTypesFromClient = new Dictionary<Type, int>();
			GameNetwork._gameNetworkMessageTypesFromServer = new Dictionary<Type, int>();
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			List<Type> list = new List<Type>();
			List<Type> list2 = new List<Type>();
			foreach (Assembly assembly in assemblies)
			{
				if (GameNetwork.CheckAssemblyForNetworkMessage(assembly))
				{
					GameNetwork.CollectGameNetworkMessagesFromAssembly(assembly, list, list2);
				}
			}
			list.Sort((Type s1, Type s2) => s1.FullName.CompareTo(s2.FullName));
			list2.Sort((Type s1, Type s2) => s1.FullName.CompareTo(s2.FullName));
			GameNetwork._gameNetworkMessageIdsFromClient = new List<Type>(list.Count);
			for (int j = 0; j < list.Count; j++)
			{
				Type type = list[j];
				GameNetwork._gameNetworkMessageIdsFromClient.Add(type);
				GameNetwork._gameNetworkMessageTypesFromClient.Add(type, j);
				GameNetwork._gameNetworkMessageTypesAll.Add(type, j);
				GameNetwork._fromClientMessageHandlers.Add(j, new List<object>());
				GameNetwork._fromClientBaseMessageHandlers.Add(j, new List<GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>>());
			}
			GameNetwork._gameNetworkMessageIdsFromServer = new List<Type>(list2.Count);
			for (int k = 0; k < list2.Count; k++)
			{
				Type type2 = list2[k];
				GameNetwork._gameNetworkMessageIdsFromServer.Add(type2);
				GameNetwork._gameNetworkMessageTypesFromServer.Add(type2, k);
				GameNetwork._gameNetworkMessageTypesAll.Add(type2, k);
				GameNetwork._fromServerMessageHandlers.Add(k, new List<object>());
				GameNetwork._fromServerBaseMessageHandlers.Add(k, new List<GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>>());
			}
			CompressionBasic.NetworkComponentEventTypeFromClientCompressionInfo = new CompressionInfo.Integer(0, list.Count - 1, true);
			CompressionBasic.NetworkComponentEventTypeFromServerCompressionInfo = new CompressionInfo.Integer(0, list2.Count - 1, true);
			Debug.Print("Found " + list.Count + " Client Game Network Messages", 0, Debug.DebugColor.White, 17179869184UL);
			Debug.Print("Found " + list2.Count + " Server Game Network Messages", 0, Debug.DebugColor.White, 17179869184UL);
		}

		// Token: 0x06002B44 RID: 11076 RVA: 0x000A6C5C File Offset: 0x000A4E5C
		internal static void FindSynchedMissionObjectTypes()
		{
			Debug.Print("Searching Game SynchedMissionObjects", 0, Debug.DebugColor.White, 17179869184UL);
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			GameNetwork._synchedMissionObjectClassTypes = new List<Type>();
			foreach (Assembly assembly in assemblies)
			{
				if (GameNetwork.CheckAssemblyForNetworkMessage(assembly))
				{
					GameNetwork.CollectSynchedMissionObjectTypesFromAssembly(assembly, GameNetwork._synchedMissionObjectClassTypes);
				}
			}
			GameNetwork._synchedMissionObjectClassTypes.Sort((Type s1, Type s2) => s1.FullName.CompareTo(s2.FullName));
		}

		// Token: 0x06002B45 RID: 11077 RVA: 0x000A6CE4 File Offset: 0x000A4EE4
		private static void RemoveClientBaseMessageHandler(GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage> handler, Type messageType)
		{
			int num = GameNetwork._gameNetworkMessageTypesFromClient[messageType];
			GameNetwork._fromClientBaseMessageHandlers[num].Remove(handler);
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x000A6D10 File Offset: 0x000A4F10
		private static bool CheckAssemblyForNetworkMessage(Assembly assembly)
		{
			Assembly assembly2 = Assembly.GetAssembly(typeof(GameNetworkMessage));
			if (assembly == assembly2)
			{
				return true;
			}
			AssemblyName[] referencedAssemblies = assembly.GetReferencedAssemblies();
			for (int i = 0; i < referencedAssemblies.Length; i++)
			{
				if (referencedAssemblies[i].FullName == assembly2.FullName)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002B47 RID: 11079 RVA: 0x000A6D65 File Offset: 0x000A4F65
		public static void SetServerBandwidthLimitInMbps(double value)
		{
			MBAPI.IMBNetwork.SetServerBandwidthLimitInMbps(value);
		}

		// Token: 0x06002B48 RID: 11080 RVA: 0x000A6D72 File Offset: 0x000A4F72
		public static void SetServerTickRate(double value)
		{
			MBAPI.IMBNetwork.SetServerTickRate(value);
		}

		// Token: 0x06002B49 RID: 11081 RVA: 0x000A6D7F File Offset: 0x000A4F7F
		public static void SetServerFrameRate(double value)
		{
			MBAPI.IMBNetwork.SetServerFrameRate(value);
		}

		// Token: 0x06002B4A RID: 11082 RVA: 0x000A6D8C File Offset: 0x000A4F8C
		public static void ResetDebugVariables()
		{
			MBAPI.IMBNetwork.ResetDebugVariables();
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x000A6D98 File Offset: 0x000A4F98
		public static void PrintDebugStats()
		{
			MBAPI.IMBNetwork.PrintDebugStats();
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x000A6DA4 File Offset: 0x000A4FA4
		public static float GetAveragePacketLossRatio()
		{
			return MBAPI.IMBNetwork.GetAveragePacketLossRatio();
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x000A6DB0 File Offset: 0x000A4FB0
		public static void GetDebugUploadsInBits(ref GameNetwork.DebugNetworkPacketStatisticsStruct networkStatisticsStruct, ref GameNetwork.DebugNetworkPositionCompressionStatisticsStruct posStatisticsStruct)
		{
			MBAPI.IMBNetwork.GetDebugUploadsInBits(ref networkStatisticsStruct, ref posStatisticsStruct);
		}

		// Token: 0x06002B4E RID: 11086 RVA: 0x000A6DBE File Offset: 0x000A4FBE
		public static void PrintReplicationTableStatistics()
		{
			MBAPI.IMBNetwork.PrintReplicationTableStatistics();
		}

		// Token: 0x06002B4F RID: 11087 RVA: 0x000A6DCA File Offset: 0x000A4FCA
		public static void ClearReplicationTableStatistics()
		{
			MBAPI.IMBNetwork.ClearReplicationTableStatistics();
		}

		// Token: 0x06002B50 RID: 11088 RVA: 0x000A6DD6 File Offset: 0x000A4FD6
		public static void ResetDebugUploads()
		{
			MBAPI.IMBNetwork.ResetDebugUploads();
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x000A6DE2 File Offset: 0x000A4FE2
		public static void ResetMissionData()
		{
			MBAPI.IMBNetwork.ResetMissionData();
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x000A6DEE File Offset: 0x000A4FEE
		private static void AddPeerToDisconnect(NetworkCommunicator networkPeer)
		{
			MBAPI.IMBNetwork.AddPeerToDisconnect(networkPeer.Index);
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x000A6E00 File Offset: 0x000A5000
		public static void InitializeCompressionInfos()
		{
			CompressionBasic.ActionCodeCompressionInfo = new CompressionInfo.Integer(ActionIndexCache.act_none.Index, MBAnimation.GetNumActionCodes() - 1, true);
			CompressionBasic.AnimationIndexCompressionInfo = new CompressionInfo.Integer(0, MBAnimation.GetNumAnimations() - 1, true);
			CompressionBasic.CultureIndexCompressionInfo = new CompressionInfo.Integer(-1, MBObjectManager.Instance.GetObjectTypeList<BasicCultureObject>().Count - 1, true);
			CompressionBasic.SoundEventsCompressionInfo = new CompressionInfo.Integer(0, SoundEvent.GetTotalEventCount() - 1, true);
			CompressionMission.ActionSetCompressionInfo = new CompressionInfo.Integer(0, MBActionSet.GetNumberOfActionSets() - 1, true);
			CompressionMission.MonsterUsageSetCompressionInfo = new CompressionInfo.Integer(0, MBActionSet.GetNumberOfMonsterUsageSets() - 1, true);
		}

		// Token: 0x06002B54 RID: 11092 RVA: 0x000A6E92 File Offset: 0x000A5092
		[MBCallback(null, false)]
		internal static void SyncRelevantGameOptionsToServer()
		{
			SyncRelevantGameOptionsToServer syncRelevantGameOptionsToServer = new SyncRelevantGameOptionsToServer();
			syncRelevantGameOptionsToServer.InitializeOptions();
			GameNetwork.BeginModuleEventAsClient();
			GameNetwork.WriteMessage(syncRelevantGameOptionsToServer);
			GameNetwork.EndModuleEventAsClient();
		}

		// Token: 0x06002B55 RID: 11093 RVA: 0x000A6EB0 File Offset: 0x000A50B0
		private static void CollectGameNetworkMessagesFromAssembly(Assembly assembly, List<Type> gameNetworkMessagesFromClient, List<Type> gameNetworkMessagesFromServer)
		{
			Type typeFromHandle = typeof(GameNetworkMessage);
			bool? flag = null;
			List<Type> typesSafe = assembly.GetTypesSafe(null);
			for (int i = 0; i < typesSafe.Count; i++)
			{
				Type type = typesSafe[i];
				if (typeFromHandle.IsAssignableFrom(type) && type != typeFromHandle && type.IsSealed && !(type.GetConstructor(Type.EmptyTypes) == null))
				{
					DefineGameNetworkMessageType customAttribute = type.GetCustomAttribute<DefineGameNetworkMessageType>();
					if (customAttribute != null)
					{
						if (flag == null || !flag.Value)
						{
							flag = new bool?(false);
							GameNetworkMessageSendType sendType = customAttribute.SendType;
							if (sendType != GameNetworkMessageSendType.FromClient)
							{
								if (sendType - GameNetworkMessageSendType.FromServer <= 1)
								{
									gameNetworkMessagesFromServer.Add(type);
								}
							}
							else
							{
								gameNetworkMessagesFromClient.Add(type);
							}
						}
					}
					else
					{
						DefineGameNetworkMessageTypeForMod customAttribute2 = type.GetCustomAttribute<DefineGameNetworkMessageTypeForMod>();
						if (customAttribute2 != null && (flag == null || flag.Value))
						{
							flag = new bool?(true);
							GameNetworkMessageSendType sendType2 = customAttribute2.SendType;
							if (sendType2 != GameNetworkMessageSendType.FromClient)
							{
								if (sendType2 - GameNetworkMessageSendType.FromServer <= 1)
								{
									gameNetworkMessagesFromServer.Add(type);
								}
							}
							else
							{
								gameNetworkMessagesFromClient.Add(type);
							}
						}
					}
				}
			}
		}

		// Token: 0x06002B56 RID: 11094 RVA: 0x000A6FE0 File Offset: 0x000A51E0
		private static void CollectSynchedMissionObjectTypesFromAssembly(Assembly assembly, List<Type> synchedMissionObjectClassTypes)
		{
			Type typeFromHandle = typeof(ISynchedMissionObjectReadableRecord);
			bool? flag = null;
			List<Type> typesSafe = assembly.GetTypesSafe(null);
			for (int i = 0; i < typesSafe.Count; i++)
			{
				Type type = typesSafe[i];
				if (typeFromHandle.IsAssignableFrom(type) && type != typeFromHandle)
				{
					if (type.GetCustomAttribute<DefineSynchedMissionObjectType>() != null)
					{
						if (flag == null || !flag.Value)
						{
							flag = new bool?(false);
							synchedMissionObjectClassTypes.Add(type);
						}
					}
					else if (type.GetCustomAttribute<DefineSynchedMissionObjectTypeForMod>() != null && (flag == null || flag.Value))
					{
						flag = new bool?(true);
						synchedMissionObjectClassTypes.Add(type);
					}
				}
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06002B57 RID: 11095 RVA: 0x000A709D File Offset: 0x000A529D
		// (set) Token: 0x06002B58 RID: 11096 RVA: 0x000A70A4 File Offset: 0x000A52A4
		public static NetworkCommunicator MyPeer { get; private set; }

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06002B59 RID: 11097 RVA: 0x000A70AC File Offset: 0x000A52AC
		public static bool IsMyPeerReady
		{
			get
			{
				return GameNetwork.MyPeer != null && GameNetwork.MyPeer.IsSynchronized;
			}
		}

		// Token: 0x040010C1 RID: 4289
		public const int MaxAutomatedBattleIndex = 10;

		// Token: 0x040010C2 RID: 4290
		public const int MaxPlayerCount = 1023;

		// Token: 0x040010C3 RID: 4291
		private static IGameNetworkHandler _handler;

		// Token: 0x040010C7 RID: 4295
		public static int ClientPeerIndex;

		// Token: 0x040010C8 RID: 4296
		private static MultiplayerMessageFilter MultiplayerLogging = (MultiplayerMessageFilter)(-1);

		// Token: 0x040010CB RID: 4299
		private static Dictionary<Type, int> _gameNetworkMessageTypesAll;

		// Token: 0x040010CC RID: 4300
		private static Dictionary<Type, int> _gameNetworkMessageTypesFromClient;

		// Token: 0x040010CD RID: 4301
		private static List<Type> _gameNetworkMessageIdsFromClient;

		// Token: 0x040010CE RID: 4302
		private static Dictionary<Type, int> _gameNetworkMessageTypesFromServer;

		// Token: 0x040010CF RID: 4303
		private static List<Type> _gameNetworkMessageIdsFromServer;

		// Token: 0x040010D0 RID: 4304
		private static Dictionary<int, List<object>> _fromClientMessageHandlers;

		// Token: 0x040010D1 RID: 4305
		private static Dictionary<int, List<object>> _fromServerMessageHandlers;

		// Token: 0x040010D2 RID: 4306
		private static Dictionary<int, List<GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>>> _fromClientBaseMessageHandlers;

		// Token: 0x040010D3 RID: 4307
		private static Dictionary<int, List<GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>>> _fromServerBaseMessageHandlers;

		// Token: 0x040010D4 RID: 4308
		private static List<Type> _synchedMissionObjectClassTypes;

		// Token: 0x020005CB RID: 1483
		public class NetworkMessageHandlerRegisterer
		{
			// Token: 0x06003E73 RID: 15987 RVA: 0x000F517A File Offset: 0x000F337A
			public NetworkMessageHandlerRegisterer(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode definitionMode)
			{
				this._registerMode = definitionMode;
			}

			// Token: 0x06003E74 RID: 15988 RVA: 0x000F5189 File Offset: 0x000F3389
			public void Register<T>(GameNetworkMessage.ServerMessageHandlerDelegate<T> handler) where T : GameNetworkMessage
			{
				if (this._registerMode == GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Add)
				{
					GameNetwork.AddServerMessageHandler<T>(handler);
					return;
				}
				GameNetwork.RemoveServerMessageHandler<T>(handler);
			}

			// Token: 0x06003E75 RID: 15989 RVA: 0x000F51A0 File Offset: 0x000F33A0
			public void RegisterBaseHandler<T>(GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage> handler) where T : GameNetworkMessage
			{
				if (this._registerMode == GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Add)
				{
					GameNetwork.AddServerBaseMessageHandler(handler, typeof(T));
					return;
				}
				GameNetwork.RemoveServerBaseMessageHandler(handler, typeof(T));
			}

			// Token: 0x06003E76 RID: 15990 RVA: 0x000F51CB File Offset: 0x000F33CB
			public void Register<T>(GameNetworkMessage.ClientMessageHandlerDelegate<T> handler) where T : GameNetworkMessage
			{
				if (this._registerMode == GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Add)
				{
					GameNetwork.AddClientMessageHandler<T>(handler);
					return;
				}
				GameNetwork.RemoveClientMessageHandler<T>(handler);
			}

			// Token: 0x06003E77 RID: 15991 RVA: 0x000F51E2 File Offset: 0x000F33E2
			public void RegisterBaseHandler<T>(GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage> handler) where T : GameNetworkMessage
			{
				if (this._registerMode == GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Add)
				{
					GameNetwork.AddClientBaseMessageHandler(handler, typeof(T));
					return;
				}
				GameNetwork.RemoveClientBaseMessageHandler(handler, typeof(T));
			}

			// Token: 0x04001F46 RID: 8006
			private readonly GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode _registerMode;

			// Token: 0x020006C1 RID: 1729
			public enum RegisterMode
			{
				// Token: 0x04002348 RID: 9032
				Add,
				// Token: 0x04002349 RID: 9033
				Remove
			}
		}

		// Token: 0x020005CC RID: 1484
		public class NetworkMessageHandlerRegistererContainer
		{
			// Token: 0x06003E78 RID: 15992 RVA: 0x000F520D File Offset: 0x000F340D
			public NetworkMessageHandlerRegistererContainer()
			{
				this._fromClientHandlers = new List<Delegate>();
				this._fromServerHandlers = new List<Delegate>();
				this._fromServerBaseHandlers = new List<Tuple<GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>, Type>>();
				this._fromClientBaseHandlers = new List<Tuple<GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>, Type>>();
			}

			// Token: 0x06003E79 RID: 15993 RVA: 0x000F5241 File Offset: 0x000F3441
			public void RegisterBaseHandler<T>(GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage> handler) where T : GameNetworkMessage
			{
				this._fromServerBaseHandlers.Add(new Tuple<GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>, Type>(handler, typeof(T)));
			}

			// Token: 0x06003E7A RID: 15994 RVA: 0x000F525E File Offset: 0x000F345E
			public void Register<T>(GameNetworkMessage.ServerMessageHandlerDelegate<T> handler) where T : GameNetworkMessage
			{
				this._fromServerHandlers.Add(handler);
			}

			// Token: 0x06003E7B RID: 15995 RVA: 0x000F526C File Offset: 0x000F346C
			public void RegisterBaseHandler<T>(GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage> handler)
			{
				this._fromClientBaseHandlers.Add(new Tuple<GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>, Type>(handler, typeof(T)));
			}

			// Token: 0x06003E7C RID: 15996 RVA: 0x000F5289 File Offset: 0x000F3489
			public void Register<T>(GameNetworkMessage.ClientMessageHandlerDelegate<T> handler) where T : GameNetworkMessage
			{
				this._fromClientHandlers.Add(handler);
			}

			// Token: 0x06003E7D RID: 15997 RVA: 0x000F5298 File Offset: 0x000F3498
			public void RegisterMessages()
			{
				if (this._fromServerHandlers.Count > 0 || this._fromServerBaseHandlers.Count > 0)
				{
					foreach (Delegate @delegate in this._fromServerHandlers)
					{
						Type type = @delegate.GetType().GenericTypeArguments[0];
						int num = GameNetwork._gameNetworkMessageTypesFromServer[type];
						GameNetwork._fromServerMessageHandlers[num].Add(@delegate);
					}
					using (List<Tuple<GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>, Type>>.Enumerator enumerator2 = this._fromServerBaseHandlers.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							Tuple<GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>, Type> tuple = enumerator2.Current;
							int num2 = GameNetwork._gameNetworkMessageTypesFromServer[tuple.Item2];
							GameNetwork._fromServerBaseMessageHandlers[num2].Add(tuple.Item1);
						}
						return;
					}
				}
				foreach (Delegate delegate2 in this._fromClientHandlers)
				{
					Type type2 = delegate2.GetType().GenericTypeArguments[0];
					int num3 = GameNetwork._gameNetworkMessageTypesFromClient[type2];
					GameNetwork._fromClientMessageHandlers[num3].Add(delegate2);
				}
				foreach (Tuple<GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>, Type> tuple2 in this._fromClientBaseHandlers)
				{
					int num4 = GameNetwork._gameNetworkMessageTypesFromClient[tuple2.Item2];
					GameNetwork._fromClientBaseMessageHandlers[num4].Add(tuple2.Item1);
				}
			}

			// Token: 0x06003E7E RID: 15998 RVA: 0x000F5470 File Offset: 0x000F3670
			public void UnregisterMessages()
			{
				if (this._fromServerHandlers.Count > 0 || this._fromServerBaseHandlers.Count > 0)
				{
					foreach (Delegate @delegate in this._fromServerHandlers)
					{
						Type type = @delegate.GetType().GenericTypeArguments[0];
						int num = GameNetwork._gameNetworkMessageTypesFromServer[type];
						GameNetwork._fromServerMessageHandlers[num].Remove(@delegate);
					}
					using (List<Tuple<GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>, Type>>.Enumerator enumerator2 = this._fromServerBaseHandlers.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							Tuple<GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>, Type> tuple = enumerator2.Current;
							int num2 = GameNetwork._gameNetworkMessageTypesFromServer[tuple.Item2];
							GameNetwork._fromServerBaseMessageHandlers[num2].Remove(tuple.Item1);
						}
						return;
					}
				}
				foreach (Delegate delegate2 in this._fromClientHandlers)
				{
					Type type2 = delegate2.GetType().GenericTypeArguments[0];
					int num3 = GameNetwork._gameNetworkMessageTypesFromClient[type2];
					GameNetwork._fromClientMessageHandlers[num3].Remove(delegate2);
				}
				foreach (Tuple<GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>, Type> tuple2 in this._fromClientBaseHandlers)
				{
					int num4 = GameNetwork._gameNetworkMessageTypesFromClient[tuple2.Item2];
					GameNetwork._fromClientBaseMessageHandlers[num4].Remove(tuple2.Item1);
				}
			}

			// Token: 0x04001F47 RID: 8007
			private List<Delegate> _fromClientHandlers;

			// Token: 0x04001F48 RID: 8008
			private List<Delegate> _fromServerHandlers;

			// Token: 0x04001F49 RID: 8009
			private List<Tuple<GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>, Type>> _fromServerBaseHandlers;

			// Token: 0x04001F4A RID: 8010
			private List<Tuple<GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>, Type>> _fromClientBaseHandlers;
		}

		// Token: 0x020005CD RID: 1485
		[Flags]
		public enum EventBroadcastFlags
		{
			// Token: 0x04001F4C RID: 8012
			None = 0,
			// Token: 0x04001F4D RID: 8013
			ExcludeTargetPlayer = 1,
			// Token: 0x04001F4E RID: 8014
			ExcludeNoBloodStainsOption = 2,
			// Token: 0x04001F4F RID: 8015
			ExcludeNoParticlesOption = 4,
			// Token: 0x04001F50 RID: 8016
			ExcludeNoSoundOption = 8,
			// Token: 0x04001F51 RID: 8017
			AddToMissionRecord = 16,
			// Token: 0x04001F52 RID: 8018
			IncludeUnsynchronizedClients = 32,
			// Token: 0x04001F53 RID: 8019
			ExcludeOtherTeamPlayers = 64,
			// Token: 0x04001F54 RID: 8020
			ExcludePeerTeamPlayers = 128,
			// Token: 0x04001F55 RID: 8021
			DontSendToPeers = 256
		}

		// Token: 0x020005CE RID: 1486
		[EngineStruct("Debug_network_position_compression_statistics_struct", false, null)]
		public struct DebugNetworkPositionCompressionStatisticsStruct
		{
			// Token: 0x04001F56 RID: 8022
			public int totalPositionUpload;

			// Token: 0x04001F57 RID: 8023
			public int totalPositionPrecisionBitCount;

			// Token: 0x04001F58 RID: 8024
			public int totalPositionCoarseBitCountX;

			// Token: 0x04001F59 RID: 8025
			public int totalPositionCoarseBitCountY;

			// Token: 0x04001F5A RID: 8026
			public int totalPositionCoarseBitCountZ;
		}

		// Token: 0x020005CF RID: 1487
		[EngineStruct("Debug_network_packet_statistics_struct", false, null)]
		public struct DebugNetworkPacketStatisticsStruct
		{
			// Token: 0x04001F5B RID: 8027
			public int TotalPackets;

			// Token: 0x04001F5C RID: 8028
			public int TotalUpload;

			// Token: 0x04001F5D RID: 8029
			public int TotalConstantsUpload;

			// Token: 0x04001F5E RID: 8030
			public int TotalReliableEventUpload;

			// Token: 0x04001F5F RID: 8031
			public int TotalReplicationUpload;

			// Token: 0x04001F60 RID: 8032
			public int TotalUnreliableEventUpload;

			// Token: 0x04001F61 RID: 8033
			public int TotalReplicationTableAdderCount;

			// Token: 0x04001F62 RID: 8034
			public int TotalReplicationTableAdderBitCount;

			// Token: 0x04001F63 RID: 8035
			public int TotalReplicationTableAdder;

			// Token: 0x04001F64 RID: 8036
			public double TotalCellPriority;

			// Token: 0x04001F65 RID: 8037
			public double TotalCellAgentPriority;

			// Token: 0x04001F66 RID: 8038
			public double TotalCellCellPriority;

			// Token: 0x04001F67 RID: 8039
			public int TotalCellPriorityChecks;

			// Token: 0x04001F68 RID: 8040
			public int TotalSentCellCount;

			// Token: 0x04001F69 RID: 8041
			public int TotalNotSentCellCount;

			// Token: 0x04001F6A RID: 8042
			public int TotalReplicationWriteCount;

			// Token: 0x04001F6B RID: 8043
			public int CurMaxPacketSizeInBytes;

			// Token: 0x04001F6C RID: 8044
			public double AveragePingTime;

			// Token: 0x04001F6D RID: 8045
			public double AverageDtToSendPacket;

			// Token: 0x04001F6E RID: 8046
			public double TimeOutPeriod;

			// Token: 0x04001F6F RID: 8047
			public double PacingRate;

			// Token: 0x04001F70 RID: 8048
			public double DeliveryRate;

			// Token: 0x04001F71 RID: 8049
			public double RoundTripTime;

			// Token: 0x04001F72 RID: 8050
			public int InflightBitCount;

			// Token: 0x04001F73 RID: 8051
			public int IsCongested;

			// Token: 0x04001F74 RID: 8052
			public int ProbeBwPhaseIndex;

			// Token: 0x04001F75 RID: 8053
			public double LostPercent;

			// Token: 0x04001F76 RID: 8054
			public int LostCount;

			// Token: 0x04001F77 RID: 8055
			public int TotalCountOnLostCheck;
		}

		// Token: 0x020005D0 RID: 1488
		public struct AddPlayersResult
		{
			// Token: 0x04001F78 RID: 8056
			public bool Success;

			// Token: 0x04001F79 RID: 8057
			public NetworkCommunicator[] NetworkPeers;
		}
	}
}
