using System;
using System.Collections.Generic;
using System.Linq;
using NetworkMessages.FromClient;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.PlatformService;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002F6 RID: 758
	public class ChatBox : GameHandler
	{
		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06002B69 RID: 11113 RVA: 0x000A70EC File Offset: 0x000A52EC
		// (set) Token: 0x06002B6A RID: 11114 RVA: 0x000A70F4 File Offset: 0x000A52F4
		public bool IsContentRestricted { get; private set; }

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06002B6B RID: 11115 RVA: 0x000A70FD File Offset: 0x000A52FD
		public bool NetworkReady
		{
			get
			{
				return GameNetwork.IsClient || GameNetwork.IsServer || (NetworkMain.GameClient != null && NetworkMain.GameClient.Connected);
			}
		}

		// Token: 0x06002B6C RID: 11116 RVA: 0x000A7122 File Offset: 0x000A5322
		protected override void OnGameStart()
		{
			ChatBox._chatBox = this;
		}

		// Token: 0x06002B6D RID: 11117 RVA: 0x000A712A File Offset: 0x000A532A
		public override void OnBeforeSave()
		{
		}

		// Token: 0x06002B6E RID: 11118 RVA: 0x000A712C File Offset: 0x000A532C
		public override void OnAfterSave()
		{
		}

		// Token: 0x06002B6F RID: 11119 RVA: 0x000A712E File Offset: 0x000A532E
		protected override void OnGameEnd()
		{
			ChatBox._chatBox = null;
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x000A7136 File Offset: 0x000A5336
		public void SendMessageToAll(string message)
		{
			this.SendMessageToAll(message, null);
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x000A7140 File Offset: 0x000A5340
		public void SendMessageToAll(string message, List<VirtualPlayer> receiverList)
		{
			if (GameNetwork.IsClient && !this.IsContentRestricted)
			{
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new NetworkMessages.FromClient.PlayerMessageAll(message));
				GameNetwork.EndModuleEventAsClient();
				return;
			}
			if (GameNetwork.IsServer)
			{
				this.ServerPrepareAndSendMessage(GameNetwork.MyPeer, false, message, receiverList);
			}
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x000A717D File Offset: 0x000A537D
		public void SendMessageToTeam(string message)
		{
			this.SendMessageToTeam(message, null);
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x000A7187 File Offset: 0x000A5387
		public void SendMessageToTeam(string message, List<VirtualPlayer> receiverList)
		{
			if (GameNetwork.IsClient && !this.IsContentRestricted)
			{
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new NetworkMessages.FromClient.PlayerMessageTeam(message));
				GameNetwork.EndModuleEventAsClient();
				return;
			}
			if (GameNetwork.IsServer)
			{
				this.ServerPrepareAndSendMessage(GameNetwork.MyPeer, true, message, receiverList);
			}
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x000A71C4 File Offset: 0x000A53C4
		public void SendMessageToWhisperTarget(string message, string platformName, string whisperTarget)
		{
			if (NetworkMain.GameClient != null && NetworkMain.GameClient.Connected)
			{
				NetworkMain.GameClient.SendWhisper(whisperTarget, message);
				if (this.WhisperMessageSent != null)
				{
					this.WhisperMessageSent(message, whisperTarget);
				}
			}
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x000A71FA File Offset: 0x000A53FA
		private void OnServerMessage(string message)
		{
			if (this.ServerMessage != null)
			{
				this.ServerMessage(message);
			}
		}

		// Token: 0x06002B76 RID: 11126 RVA: 0x000A7210 File Offset: 0x000A5410
		protected override void OnGameNetworkBegin()
		{
			ChatBox._queuedTeamMessages = new List<ChatBox.QueuedMessageInfo>();
			ChatBox._queuedEveryoneMessages = new List<ChatBox.QueuedMessageInfo>();
			this._isNetworkInitialized = true;
			this.AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Add);
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x000A7234 File Offset: 0x000A5434
		private void AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode mode)
		{
			GameNetwork.NetworkMessageHandlerRegisterer networkMessageHandlerRegisterer = new GameNetwork.NetworkMessageHandlerRegisterer(mode);
			if (GameNetwork.IsClient)
			{
				networkMessageHandlerRegisterer.Register<NetworkMessages.FromServer.PlayerMessageTeam>(new GameNetworkMessage.ServerMessageHandlerDelegate<NetworkMessages.FromServer.PlayerMessageTeam>(this.HandleServerEventPlayerMessageTeam));
				networkMessageHandlerRegisterer.Register<NetworkMessages.FromServer.PlayerMessageAll>(new GameNetworkMessage.ServerMessageHandlerDelegate<NetworkMessages.FromServer.PlayerMessageAll>(this.HandleServerEventPlayerMessageAll));
				networkMessageHandlerRegisterer.Register<ServerMessage>(new GameNetworkMessage.ServerMessageHandlerDelegate<ServerMessage>(this.HandleServerEventServerMessage));
				networkMessageHandlerRegisterer.Register<ServerAdminMessage>(new GameNetworkMessage.ServerMessageHandlerDelegate<ServerAdminMessage>(this.HandleServerEventServerAdminMessage));
				return;
			}
			if (GameNetwork.IsServer)
			{
				networkMessageHandlerRegisterer.Register<NetworkMessages.FromClient.PlayerMessageAll>(new GameNetworkMessage.ClientMessageHandlerDelegate<NetworkMessages.FromClient.PlayerMessageAll>(this.HandleClientEventPlayerMessageAll));
				networkMessageHandlerRegisterer.Register<NetworkMessages.FromClient.PlayerMessageTeam>(new GameNetworkMessage.ClientMessageHandlerDelegate<NetworkMessages.FromClient.PlayerMessageTeam>(this.HandleClientEventPlayerMessageTeam));
			}
		}

		// Token: 0x06002B78 RID: 11128 RVA: 0x000A72C3 File Offset: 0x000A54C3
		protected override void OnGameNetworkEnd()
		{
			base.OnGameNetworkEnd();
			this.AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Remove);
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x000A72D4 File Offset: 0x000A54D4
		private void HandleServerEventPlayerMessageAll(NetworkMessages.FromServer.PlayerMessageAll message)
		{
			if (!this.IsContentRestricted)
			{
				this.ShouldShowPlayersMessage(message.Player.VirtualPlayer.Id, delegate(bool result)
				{
					if (result)
					{
						this.OnPlayerMessageReceived(message.Player, message.Message, false);
					}
				});
			}
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x000A7324 File Offset: 0x000A5524
		private void HandleServerEventPlayerMessageTeam(NetworkMessages.FromServer.PlayerMessageTeam message)
		{
			if (!this.IsContentRestricted)
			{
				this.ShouldShowPlayersMessage(message.Player.VirtualPlayer.Id, delegate(bool result)
				{
					if (result)
					{
						this.OnPlayerMessageReceived(message.Player, message.Message, true);
					}
				});
			}
		}

		// Token: 0x06002B7B RID: 11131 RVA: 0x000A7374 File Offset: 0x000A5574
		private void HandleServerEventServerMessage(ServerMessage message)
		{
			this.OnServerMessage(message.IsMessageTextId ? GameTexts.FindText(message.Message, null).ToString() : message.Message);
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x000A73A0 File Offset: 0x000A55A0
		private void HandleServerEventServerAdminMessage(ServerAdminMessage message)
		{
			if (message.IsAdminBroadcast)
			{
				TextObject textObject = new TextObject("{=!}{ADMIN_TEXT}", null);
				textObject.SetTextVariable("ADMIN_TEXT", message.Message);
				MBInformationManager.AddQuickInformation(textObject, 5000, null, null, "");
				SoundEvent.PlaySound2D("event:/ui/notification/alert");
			}
			ServerAdminMessageDelegate serverAdminMessage = this.ServerAdminMessage;
			if (serverAdminMessage == null)
			{
				return;
			}
			serverAdminMessage(message.Message);
		}

		// Token: 0x06002B7D RID: 11133 RVA: 0x000A7404 File Offset: 0x000A5604
		private bool HandleClientEventPlayerMessageAll(NetworkCommunicator networkPeer, NetworkMessages.FromClient.PlayerMessageAll message)
		{
			return this.ServerPrepareAndSendMessage(networkPeer, false, message.Message, message.ReceiverList);
		}

		// Token: 0x06002B7E RID: 11134 RVA: 0x000A741A File Offset: 0x000A561A
		private bool HandleClientEventPlayerMessageTeam(NetworkCommunicator networkPeer, NetworkMessages.FromClient.PlayerMessageTeam message)
		{
			return this.ServerPrepareAndSendMessage(networkPeer, true, message.Message, message.ReceiverList);
		}

		// Token: 0x06002B7F RID: 11135 RVA: 0x000A7430 File Offset: 0x000A5630
		public static void ServerSendServerMessageToEveryone(string message)
		{
			ChatBox._chatBox.OnServerMessage(message);
			GameNetwork.BeginBroadcastModuleEvent();
			GameNetwork.WriteMessage(new ServerMessage(message, false, false));
			GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None, null);
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x000A7458 File Offset: 0x000A5658
		private bool ServerPrepareAndSendMessage(NetworkCommunicator fromPeer, bool toTeamOnly, string message, List<VirtualPlayer> receiverList)
		{
			if (GameNetwork.IsDedicatedServer)
			{
				Action<NetworkCommunicator, string> onMessageReceivedAtDedicatedServer = this.OnMessageReceivedAtDedicatedServer;
				if (onMessageReceivedAtDedicatedServer != null)
				{
					onMessageReceivedAtDedicatedServer(fromPeer, message);
				}
			}
			if (fromPeer.IsMuted || MultiplayerGlobalMutedPlayersManager.IsUserMuted(fromPeer.VirtualPlayer.Id))
			{
				GameNetwork.BeginModuleEventAsServer(fromPeer);
				GameNetwork.WriteMessage(new ServerMessage("str_multiplayer_muted_message", true, false));
				GameNetwork.EndModuleEventAsServer();
				return true;
			}
			if (this._profanityChecker != null)
			{
				message = this._profanityChecker.CensorText(message);
			}
			if (!GameNetwork.IsDedicatedServer && fromPeer != GameNetwork.MyPeer && !this._mutedPlayers.Contains(fromPeer.VirtualPlayer.Id) && !PermaMuteList.IsPlayerMuted(fromPeer.VirtualPlayer.Id))
			{
				MissionPeer component = GameNetwork.MyPeer.GetComponent<MissionPeer>();
				if (component == null)
				{
					return false;
				}
				bool flag;
				if (toTeamOnly)
				{
					if (component == null)
					{
						return false;
					}
					MissionPeer component2 = fromPeer.GetComponent<MissionPeer>();
					if (component2 == null)
					{
						return false;
					}
					flag = component.Team == component2.Team;
				}
				else
				{
					flag = true;
				}
				if (flag)
				{
					this.OnPlayerMessageReceived(fromPeer, message, toTeamOnly);
				}
			}
			if (toTeamOnly)
			{
				ChatBox.ServerSendMessageToTeam(fromPeer, message, receiverList);
			}
			else
			{
				ChatBox.ServerSendMessageToEveryone(fromPeer, message, receiverList);
			}
			return true;
		}

		// Token: 0x06002B81 RID: 11137 RVA: 0x000A7564 File Offset: 0x000A5764
		private static void ServerSendMessageToTeam(NetworkCommunicator networkPeer, string message, List<VirtualPlayer> receiverList)
		{
			if (!networkPeer.IsSynchronized)
			{
				ChatBox._queuedTeamMessages.Add(new ChatBox.QueuedMessageInfo(networkPeer, message, receiverList));
				return;
			}
			MissionPeer missionPeer = networkPeer.GetComponent<MissionPeer>();
			MissionPeer missionPeer2 = missionPeer;
			if (((missionPeer2 != null) ? missionPeer2.Team : null) != null)
			{
				using (IEnumerator<NetworkCommunicator> enumerator = GameNetwork.NetworkPeers.Where<NetworkCommunicator>((NetworkCommunicator x) => !x.IsServerPeer && x.IsSynchronized && x.GetComponent<MissionPeer>().Team == missionPeer.Team).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						NetworkCommunicator networkCommunicator = enumerator.Current;
						if (receiverList == null || receiverList.Contains(networkCommunicator.VirtualPlayer))
						{
							GameNetwork.BeginModuleEventAsServer(networkCommunicator);
							GameNetwork.WriteMessage(new NetworkMessages.FromServer.PlayerMessageTeam(networkPeer, message));
							GameNetwork.EndModuleEventAsServer();
						}
					}
					return;
				}
			}
			ChatBox.ServerSendMessageToEveryone(networkPeer, message, receiverList);
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x000A762C File Offset: 0x000A582C
		private static void ServerSendMessageToEveryone(NetworkCommunicator networkPeer, string message, List<VirtualPlayer> receiverList)
		{
			if (!networkPeer.IsSynchronized)
			{
				ChatBox._queuedEveryoneMessages.Add(new ChatBox.QueuedMessageInfo(networkPeer, message, receiverList));
				return;
			}
			foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeers.Where<NetworkCommunicator>((NetworkCommunicator x) => !x.IsServerPeer && x.IsSynchronized))
			{
				if (receiverList == null || receiverList.Contains(networkCommunicator.VirtualPlayer))
				{
					GameNetwork.BeginModuleEventAsServer(networkCommunicator);
					GameNetwork.WriteMessage(new NetworkMessages.FromServer.PlayerMessageAll(networkPeer, message));
					GameNetwork.EndModuleEventAsServer();
				}
			}
		}

		// Token: 0x06002B83 RID: 11139 RVA: 0x000A76D8 File Offset: 0x000A58D8
		public void ResetMuteList()
		{
			this._mutedPlayers.Clear();
		}

		// Token: 0x06002B84 RID: 11140 RVA: 0x000A76E5 File Offset: 0x000A58E5
		public static void AddWhisperMessage(string fromUserName, string messageBody)
		{
			ChatBox._chatBox.OnWhisperMessageReceived(fromUserName, messageBody);
		}

		// Token: 0x06002B85 RID: 11141 RVA: 0x000A76F3 File Offset: 0x000A58F3
		public static void AddErrorWhisperMessage(string toUserName)
		{
			ChatBox._chatBox.OnErrorWhisperMessageReceived(toUserName);
		}

		// Token: 0x06002B86 RID: 11142 RVA: 0x000A7700 File Offset: 0x000A5900
		private void OnWhisperMessageReceived(string fromUserName, string messageBody)
		{
			if (this.WhisperMessageReceived != null)
			{
				this.WhisperMessageReceived(fromUserName, messageBody);
			}
		}

		// Token: 0x06002B87 RID: 11143 RVA: 0x000A7717 File Offset: 0x000A5917
		private void OnErrorWhisperMessageReceived(string toUserName)
		{
			if (this.ErrorWhisperMessageReceived != null)
			{
				this.ErrorWhisperMessageReceived(toUserName);
			}
		}

		// Token: 0x06002B88 RID: 11144 RVA: 0x000A772D File Offset: 0x000A592D
		private void OnPlayerMessageReceived(NetworkCommunicator networkPeer, string message, bool toTeamOnly)
		{
			if (this.PlayerMessageReceived != null)
			{
				this.PlayerMessageReceived(networkPeer, message, toTeamOnly);
			}
		}

		// Token: 0x06002B89 RID: 11145 RVA: 0x000A7745 File Offset: 0x000A5945
		public void SetPlayerMuted(PlayerId playerID, bool isMuted)
		{
			if (isMuted)
			{
				this.OnPlayerMuted(playerID);
				return;
			}
			this.OnPlayerUnmuted(playerID);
		}

		// Token: 0x06002B8A RID: 11146 RVA: 0x000A7759 File Offset: 0x000A5959
		public void SetPlayerMutedFromPlatform(PlayerId playerID, bool isMuted)
		{
			if (isMuted && !this._platformMutedPlayers.Contains(playerID))
			{
				this._platformMutedPlayers.Add(playerID);
				return;
			}
			if (!isMuted && this._platformMutedPlayers.Contains(playerID))
			{
				this._platformMutedPlayers.Remove(playerID);
			}
		}

		// Token: 0x06002B8B RID: 11147 RVA: 0x000A7797 File Offset: 0x000A5997
		private void OnPlayerMuted(PlayerId mutedPlayer)
		{
			if (!this._mutedPlayers.Contains(mutedPlayer))
			{
				this._mutedPlayers.Add(mutedPlayer);
				PlayerMutedDelegate onPlayerMuteChanged = this.OnPlayerMuteChanged;
				if (onPlayerMuteChanged == null)
				{
					return;
				}
				onPlayerMuteChanged(mutedPlayer, true);
			}
		}

		// Token: 0x06002B8C RID: 11148 RVA: 0x000A77C5 File Offset: 0x000A59C5
		private void OnPlayerUnmuted(PlayerId unmutedPlayer)
		{
			if (this._mutedPlayers.Contains(unmutedPlayer))
			{
				this._mutedPlayers.Remove(unmutedPlayer);
				PlayerMutedDelegate onPlayerMuteChanged = this.OnPlayerMuteChanged;
				if (onPlayerMuteChanged == null)
				{
					return;
				}
				onPlayerMuteChanged(unmutedPlayer, false);
			}
		}

		// Token: 0x06002B8D RID: 11149 RVA: 0x000A77F4 File Offset: 0x000A59F4
		public bool IsPlayerMuted(PlayerId player)
		{
			return this.IsPlayerMutedFromGame(player) || this.IsPlayerMutedFromPlatform(player);
		}

		// Token: 0x06002B8E RID: 11150 RVA: 0x000A7808 File Offset: 0x000A5A08
		public bool IsPlayerMutedFromPlatform(PlayerId player)
		{
			return this._platformMutedPlayers.Contains(player);
		}

		// Token: 0x06002B8F RID: 11151 RVA: 0x000A7818 File Offset: 0x000A5A18
		public bool IsPlayerMutedFromGame(PlayerId player)
		{
			if (GameNetwork.IsDedicatedServer)
			{
				return this._mutedPlayers.Contains(player);
			}
			PlatformServices.Instance.CheckPrivilege(Privilege.Chat, false, delegate(bool result)
			{
				this.IsContentRestricted = !result;
			});
			return this._mutedPlayers.Contains(player) || PermaMuteList.IsPlayerMuted(player);
		}

		// Token: 0x06002B90 RID: 11152 RVA: 0x000A7868 File Offset: 0x000A5A68
		private void ShouldShowPlayersMessage(PlayerId player, Action<bool> result)
		{
			if (this.IsPlayerMuted(player) || !NetworkMain.GameClient.SupportedFeatures.SupportsFeatures(Features.TextChat))
			{
				result(false);
				return;
			}
			PlayerIdProvidedTypes providedType = player.ProvidedType;
			LobbyClient gameClient = NetworkMain.GameClient;
			PlayerIdProvidedTypes? playerIdProvidedTypes = ((gameClient != null) ? new PlayerIdProvidedTypes?(gameClient.PlayerID.ProvidedType) : null);
			if (!((providedType == playerIdProvidedTypes.GetValueOrDefault()) & (playerIdProvidedTypes != null)))
			{
				result(true);
				return;
			}
			PlatformServices.Instance.CheckPermissionWithUser(Permission.CommunicateUsingText, player, delegate(bool res)
			{
				result(res);
			});
		}

		// Token: 0x06002B91 RID: 11153 RVA: 0x000A7911 File Offset: 0x000A5B11
		public void SetChatFilterLists(string[] profanityList, string[] allowList)
		{
			this._profanityChecker = new ProfanityChecker(profanityList, allowList);
		}

		// Token: 0x06002B92 RID: 11154 RVA: 0x000A7920 File Offset: 0x000A5B20
		public void InitializeForMultiplayer()
		{
			PlatformServices.Instance.CheckPrivilege(Privilege.Chat, true, delegate(bool result)
			{
				this.IsContentRestricted = !result;
			});
		}

		// Token: 0x06002B93 RID: 11155 RVA: 0x000A793A File Offset: 0x000A5B3A
		public void InitializeForSinglePlayer()
		{
			this.IsContentRestricted = false;
		}

		// Token: 0x06002B94 RID: 11156 RVA: 0x000A7943 File Offset: 0x000A5B43
		public void OnLogin()
		{
			PlatformServices.Instance.CheckPrivilege(Privilege.Chat, false, delegate(bool chatPrivilegeResult)
			{
				this.IsContentRestricted = !chatPrivilegeResult;
			});
		}

		// Token: 0x14000086 RID: 134
		// (add) Token: 0x06002B95 RID: 11157 RVA: 0x000A7960 File Offset: 0x000A5B60
		// (remove) Token: 0x06002B96 RID: 11158 RVA: 0x000A7998 File Offset: 0x000A5B98
		public event PlayerMessageReceivedDelegate PlayerMessageReceived;

		// Token: 0x14000087 RID: 135
		// (add) Token: 0x06002B97 RID: 11159 RVA: 0x000A79D0 File Offset: 0x000A5BD0
		// (remove) Token: 0x06002B98 RID: 11160 RVA: 0x000A7A08 File Offset: 0x000A5C08
		public event WhisperMessageSentDelegate WhisperMessageSent;

		// Token: 0x14000088 RID: 136
		// (add) Token: 0x06002B99 RID: 11161 RVA: 0x000A7A40 File Offset: 0x000A5C40
		// (remove) Token: 0x06002B9A RID: 11162 RVA: 0x000A7A78 File Offset: 0x000A5C78
		public event WhisperMessageReceivedDelegate WhisperMessageReceived;

		// Token: 0x14000089 RID: 137
		// (add) Token: 0x06002B9B RID: 11163 RVA: 0x000A7AB0 File Offset: 0x000A5CB0
		// (remove) Token: 0x06002B9C RID: 11164 RVA: 0x000A7AE8 File Offset: 0x000A5CE8
		public event ErrorWhisperMessageReceivedDelegate ErrorWhisperMessageReceived;

		// Token: 0x1400008A RID: 138
		// (add) Token: 0x06002B9D RID: 11165 RVA: 0x000A7B20 File Offset: 0x000A5D20
		// (remove) Token: 0x06002B9E RID: 11166 RVA: 0x000A7B58 File Offset: 0x000A5D58
		public event ServerMessageDelegate ServerMessage;

		// Token: 0x1400008B RID: 139
		// (add) Token: 0x06002B9F RID: 11167 RVA: 0x000A7B90 File Offset: 0x000A5D90
		// (remove) Token: 0x06002BA0 RID: 11168 RVA: 0x000A7BC8 File Offset: 0x000A5DC8
		public event ServerAdminMessageDelegate ServerAdminMessage;

		// Token: 0x1400008C RID: 140
		// (add) Token: 0x06002BA1 RID: 11169 RVA: 0x000A7C00 File Offset: 0x000A5E00
		// (remove) Token: 0x06002BA2 RID: 11170 RVA: 0x000A7C38 File Offset: 0x000A5E38
		public event PlayerMutedDelegate OnPlayerMuteChanged;

		// Token: 0x06002BA3 RID: 11171 RVA: 0x000A7C70 File Offset: 0x000A5E70
		protected override void OnTick(float dt)
		{
			if (GameNetwork.IsServer && this._isNetworkInitialized)
			{
				for (int i = ChatBox._queuedTeamMessages.Count - 1; i >= 0; i--)
				{
					ChatBox.QueuedMessageInfo queuedMessageInfo = ChatBox._queuedTeamMessages[i];
					if (queuedMessageInfo.SourcePeer.IsSynchronized)
					{
						ChatBox.ServerSendMessageToTeam(queuedMessageInfo.SourcePeer, queuedMessageInfo.Message, queuedMessageInfo.ReceiverList);
						ChatBox._queuedTeamMessages.RemoveAt(i);
					}
					else if (queuedMessageInfo.IsExpired)
					{
						ChatBox._queuedTeamMessages.RemoveAt(i);
					}
				}
				for (int j = ChatBox._queuedEveryoneMessages.Count - 1; j >= 0; j--)
				{
					ChatBox.QueuedMessageInfo queuedMessageInfo2 = ChatBox._queuedEveryoneMessages[j];
					if (queuedMessageInfo2.SourcePeer.IsSynchronized)
					{
						ChatBox.ServerSendMessageToEveryone(queuedMessageInfo2.SourcePeer, queuedMessageInfo2.Message, queuedMessageInfo2.ReceiverList);
						ChatBox._queuedEveryoneMessages.RemoveAt(j);
					}
					else if (queuedMessageInfo2.IsExpired)
					{
						ChatBox._queuedEveryoneMessages.RemoveAt(j);
					}
				}
			}
		}

		// Token: 0x040010E4 RID: 4324
		private static ChatBox _chatBox;

		// Token: 0x040010E6 RID: 4326
		private bool _isNetworkInitialized;

		// Token: 0x040010E7 RID: 4327
		public const string AdminMessageSoundEvent = "event:/ui/notification/alert";

		// Token: 0x040010E8 RID: 4328
		private List<PlayerId> _mutedPlayers = new List<PlayerId>();

		// Token: 0x040010E9 RID: 4329
		private List<PlayerId> _platformMutedPlayers = new List<PlayerId>();

		// Token: 0x040010EA RID: 4330
		private ProfanityChecker _profanityChecker;

		// Token: 0x040010EB RID: 4331
		private static List<ChatBox.QueuedMessageInfo> _queuedTeamMessages;

		// Token: 0x040010EC RID: 4332
		private static List<ChatBox.QueuedMessageInfo> _queuedEveryoneMessages;

		// Token: 0x040010ED RID: 4333
		public Action<NetworkCommunicator, string> OnMessageReceivedAtDedicatedServer;

		// Token: 0x020005D4 RID: 1492
		private class QueuedMessageInfo
		{
			// Token: 0x17000A84 RID: 2692
			// (get) Token: 0x06003E8F RID: 16015 RVA: 0x000F5888 File Offset: 0x000F3A88
			public bool IsExpired
			{
				get
				{
					return (DateTime.Now - this._creationTime).TotalSeconds >= 3.0;
				}
			}

			// Token: 0x06003E90 RID: 16016 RVA: 0x000F58BB File Offset: 0x000F3ABB
			public QueuedMessageInfo(NetworkCommunicator sourcePeer, string message, List<VirtualPlayer> receiverList)
			{
				this.SourcePeer = sourcePeer;
				this.Message = message;
				this._creationTime = DateTime.Now;
				this.ReceiverList = receiverList;
			}

			// Token: 0x04001F84 RID: 8068
			public readonly NetworkCommunicator SourcePeer;

			// Token: 0x04001F85 RID: 8069
			public readonly string Message;

			// Token: 0x04001F86 RID: 8070
			public readonly List<VirtualPlayer> ReceiverList;

			// Token: 0x04001F87 RID: 8071
			private const float _timeOutDuration = 3f;

			// Token: 0x04001F88 RID: 8072
			private DateTime _creationTime;
		}
	}
}
