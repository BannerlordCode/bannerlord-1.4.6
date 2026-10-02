using System;
using System.Collections.Generic;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002FE RID: 766
	public class LobbyNetworkComponent : UdpNetworkComponent
	{
		// Token: 0x06002BC5 RID: 11205 RVA: 0x000A7DAA File Offset: 0x000A5FAA
		protected override void AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegistererContainer registerer)
		{
			if (GameNetwork.IsClientOrReplay)
			{
				registerer.RegisterBaseHandler<InitializeLobbyPeer>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventInitializeLobbyPeer));
			}
		}

		// Token: 0x06002BC6 RID: 11206 RVA: 0x000A7DC8 File Offset: 0x000A5FC8
		private void HandleServerEventInitializeLobbyPeer(GameNetworkMessage baseMessage)
		{
			InitializeLobbyPeer initializeLobbyPeer = (InitializeLobbyPeer)baseMessage;
			NetworkCommunicator peer = initializeLobbyPeer.Peer;
			VirtualPlayer virtualPlayer = peer.VirtualPlayer;
			virtualPlayer.Id = initializeLobbyPeer.ProvidedId;
			virtualPlayer.IsFemale = initializeLobbyPeer.IsFemale;
			virtualPlayer.BannerCode = initializeLobbyPeer.BannerCode;
			virtualPlayer.BodyProperties = initializeLobbyPeer.BodyProperties;
			virtualPlayer.ChosenBadgeIndex = initializeLobbyPeer.ChosenBadgeIndex;
			peer.ForcedAvatarIndex = initializeLobbyPeer.ForcedAvatarIndex;
		}

		// Token: 0x06002BC7 RID: 11207 RVA: 0x000A7E30 File Offset: 0x000A6030
		public override void HandleEarlyNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
			PlayerData parameter = networkPeer.PlayerConnectionInfo.GetParameter<PlayerData>("PlayerData");
			Dictionary<int, List<int>> parameter2 = networkPeer.PlayerConnectionInfo.GetParameter<Dictionary<int, List<int>>>("UsedCosmetics");
			VirtualPlayer virtualPlayer = networkPeer.VirtualPlayer;
			virtualPlayer.Id = parameter.PlayerId;
			virtualPlayer.BannerCode = parameter.Sigil;
			virtualPlayer.BodyProperties = parameter.BodyProperties;
			virtualPlayer.IsFemale = parameter.IsFemale;
			virtualPlayer.ChosenBadgeIndex = parameter.ShownBadgeIndex;
			virtualPlayer.UsedCosmetics = parameter2;
			networkPeer.IsMuted = parameter.IsMuted;
		}

		// Token: 0x06002BC8 RID: 11208 RVA: 0x000A7EB4 File Offset: 0x000A60B4
		public override void HandleNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
			VirtualPlayer virtualPlayer = networkPeer.VirtualPlayer;
			GameNetwork.BeginBroadcastModuleEvent();
			GameNetwork.WriteMessage(new InitializeLobbyPeer(networkPeer, virtualPlayer, -1));
			GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord | GameNetwork.EventBroadcastFlags.DontSendToPeers, null);
			foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeersIncludingDisconnectedPeers)
			{
				if (networkCommunicator.IsSynchronized || networkCommunicator == networkPeer)
				{
					bool flag = GameNetwork.VirtualPlayers[networkCommunicator.VirtualPlayer.Index] != networkCommunicator.VirtualPlayer;
					if (networkCommunicator != networkPeer && !flag && networkCommunicator != GameNetwork.MyPeer)
					{
						GameNetwork.BeginModuleEventAsServer(networkCommunicator);
						GameNetwork.WriteMessage(new InitializeLobbyPeer(networkPeer, virtualPlayer, -1));
						GameNetwork.EndModuleEventAsServer();
					}
					if (!networkPeer.IsServerPeer)
					{
						GameNetwork.BeginModuleEventAsServer(networkPeer);
						GameNetwork.WriteMessage(new InitializeLobbyPeer(networkCommunicator, networkCommunicator.VirtualPlayer, -1));
						GameNetwork.EndModuleEventAsServer();
					}
				}
			}
		}

		// Token: 0x06002BC9 RID: 11209 RVA: 0x000A7F98 File Offset: 0x000A6198
		public override void HandleLateNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x000A7F9A File Offset: 0x000A619A
		public override void HandlePlayerDisconnect(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x06002BCB RID: 11211 RVA: 0x000A7F9C File Offset: 0x000A619C
		public override void OnUdpNetworkHandlerTick(float dt)
		{
		}

		// Token: 0x040010F5 RID: 4341
		public const int MaxForcedAvatarIndex = 100;
	}
}
