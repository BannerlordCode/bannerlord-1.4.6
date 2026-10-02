using System;
using System.Linq;
using NetworkMessages.FromClient;
using NetworkMessages.FromServer;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002AB RID: 683
	public class MissionLobbyEquipmentNetworkComponent : MissionNetwork
	{
		// Token: 0x1400004B RID: 75
		// (add) Token: 0x060025AE RID: 9646 RVA: 0x00088D60 File Offset: 0x00086F60
		// (remove) Token: 0x060025AF RID: 9647 RVA: 0x00088D98 File Offset: 0x00086F98
		public event MissionLobbyEquipmentNetworkComponent.OnToggleLoadoutDelegate OnToggleLoadout;

		// Token: 0x1400004C RID: 76
		// (add) Token: 0x060025B0 RID: 9648 RVA: 0x00088DD0 File Offset: 0x00086FD0
		// (remove) Token: 0x060025B1 RID: 9649 RVA: 0x00088E08 File Offset: 0x00087008
		public event MissionLobbyEquipmentNetworkComponent.OnRefreshEquipmentEventDelegate OnEquipmentRefreshed;

		// Token: 0x060025B2 RID: 9650 RVA: 0x00088E40 File Offset: 0x00087040
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			if (!GameNetwork.IsDedicatedServer)
			{
				this._agentVisualSpawnComponent = Mission.Current.GetMissionBehavior<MultiplayerMissionAgentVisualSpawnComponent>();
				this._agentVisualSpawnComponent.OnMyAgentVisualSpawned += this.OpenLoadout;
				this._agentVisualSpawnComponent.OnMyAgentSpawnedFromVisual += this.CloseLoadout;
				this._agentVisualSpawnComponent.OnMyAgentVisualRemoved += this.CloseLoadout;
			}
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x00088EB0 File Offset: 0x000870B0
		protected override void OnEndMission()
		{
			if (!GameNetwork.IsDedicatedServer)
			{
				this._agentVisualSpawnComponent.OnMyAgentVisualSpawned -= this.OpenLoadout;
				this._agentVisualSpawnComponent.OnMyAgentSpawnedFromVisual -= this.CloseLoadout;
				this._agentVisualSpawnComponent.OnMyAgentVisualRemoved -= this.CloseLoadout;
				this._agentVisualSpawnComponent = null;
			}
			base.OnEndMission();
		}

		// Token: 0x060025B4 RID: 9652 RVA: 0x00088F18 File Offset: 0x00087118
		protected override void AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegistererContainer registerer)
		{
			if (GameNetwork.IsServer)
			{
				registerer.RegisterBaseHandler<RequestTroopIndexChange>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventLobbyEquipmentUpdated));
				registerer.RegisterBaseHandler<TeamInitialPerkInfoMessage>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventTeamInitialPerkInfoMessage));
				registerer.RegisterBaseHandler<RequestPerkChange>(new GameNetworkMessage.ClientMessageHandlerDelegate<GameNetworkMessage>(this.HandleClientEventRequestPerkChange));
				return;
			}
			if (GameNetwork.IsClientOrReplay)
			{
				registerer.RegisterBaseHandler<UpdateSelectedTroopIndex>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.HandleServerEventEquipmentIndexUpdated));
				registerer.RegisterBaseHandler<SyncPerksForCurrentlySelectedTroop>(new GameNetworkMessage.ServerMessageHandlerDelegate<GameNetworkMessage>(this.SyncPerksForCurrentlySelectedTroop));
			}
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x00088F90 File Offset: 0x00087190
		private void HandleServerEventEquipmentIndexUpdated(GameNetworkMessage baseMessage)
		{
			UpdateSelectedTroopIndex updateSelectedTroopIndex = (UpdateSelectedTroopIndex)baseMessage;
			updateSelectedTroopIndex.Peer.GetComponent<MissionPeer>().SelectedTroopIndex = updateSelectedTroopIndex.SelectedTroopIndex;
		}

		// Token: 0x060025B6 RID: 9654 RVA: 0x00088FBC File Offset: 0x000871BC
		private void SyncPerksForCurrentlySelectedTroop(GameNetworkMessage baseMessage)
		{
			SyncPerksForCurrentlySelectedTroop syncPerksForCurrentlySelectedTroop = (SyncPerksForCurrentlySelectedTroop)baseMessage;
			MissionPeer component = syncPerksForCurrentlySelectedTroop.Peer.GetComponent<MissionPeer>();
			for (int i = 0; i < 3; i++)
			{
				component.SelectPerk(i, syncPerksForCurrentlySelectedTroop.PerkIndices[i], component.SelectedTroopIndex);
			}
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x00089000 File Offset: 0x00087200
		private bool HandleClientEventLobbyEquipmentUpdated(NetworkCommunicator peer, GameNetworkMessage baseMessage)
		{
			RequestTroopIndexChange requestTroopIndexChange = (RequestTroopIndexChange)baseMessage;
			MissionPeer component = peer.GetComponent<MissionPeer>();
			if (component == null)
			{
				return false;
			}
			SpawnComponent missionBehavior = base.Mission.GetMissionBehavior<SpawnComponent>();
			if (missionBehavior == null)
			{
				return false;
			}
			if (missionBehavior.AreAgentsSpawning() && component.SelectedTroopIndex != requestTroopIndexChange.SelectedTroopIndex)
			{
				if (component.Culture == null || requestTroopIndexChange.SelectedTroopIndex < 0 || MultiplayerClassDivisions.GetMPHeroClasses(component.Culture).Count<MultiplayerClassDivisions.MPHeroClass>() <= requestTroopIndexChange.SelectedTroopIndex)
				{
					component.SelectedTroopIndex = 0;
				}
				else
				{
					component.SelectedTroopIndex = requestTroopIndexChange.SelectedTroopIndex;
				}
				GameNetwork.BeginBroadcastModuleEvent();
				GameNetwork.WriteMessage(new UpdateSelectedTroopIndex(peer, component.SelectedTroopIndex));
				GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.ExcludeOtherTeamPlayers, peer);
				if (this.OnEquipmentRefreshed != null)
				{
					this.OnEquipmentRefreshed(component);
				}
			}
			return true;
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x000890B8 File Offset: 0x000872B8
		private bool HandleClientEventTeamInitialPerkInfoMessage(NetworkCommunicator peer, GameNetworkMessage baseMessage)
		{
			TeamInitialPerkInfoMessage teamInitialPerkInfoMessage = (TeamInitialPerkInfoMessage)baseMessage;
			MissionPeer component = peer.GetComponent<MissionPeer>();
			if (component == null)
			{
				return false;
			}
			if (base.Mission.GetMissionBehavior<SpawnComponent>() == null)
			{
				return false;
			}
			component.OnTeamInitialPerkInfoReceived(teamInitialPerkInfoMessage.Perks);
			return true;
		}

		// Token: 0x060025B9 RID: 9657 RVA: 0x000890F4 File Offset: 0x000872F4
		private bool HandleClientEventRequestPerkChange(NetworkCommunicator peer, GameNetworkMessage baseMessage)
		{
			RequestPerkChange requestPerkChange = (RequestPerkChange)baseMessage;
			MissionPeer component = peer.GetComponent<MissionPeer>();
			if (component == null)
			{
				return false;
			}
			SpawnComponent missionBehavior = base.Mission.GetMissionBehavior<SpawnComponent>();
			if (missionBehavior == null)
			{
				return false;
			}
			if (component.SelectPerk(requestPerkChange.PerkListIndex, requestPerkChange.PerkIndex, -1) && missionBehavior.AreAgentsSpawning() && this.OnEquipmentRefreshed != null)
			{
				this.OnEquipmentRefreshed(component);
			}
			return true;
		}

		// Token: 0x060025BA RID: 9658 RVA: 0x00089158 File Offset: 0x00087358
		public void PerkUpdated(int perkList, int perkIndex)
		{
			if (GameNetwork.IsServer)
			{
				MissionPeer component = GameNetwork.MyPeer.GetComponent<MissionPeer>();
				if (this.OnEquipmentRefreshed != null)
				{
					this.OnEquipmentRefreshed(component);
					return;
				}
			}
			else
			{
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new RequestPerkChange(perkList, perkIndex));
				GameNetwork.EndModuleEventAsClient();
			}
		}

		// Token: 0x060025BB RID: 9659 RVA: 0x000891A4 File Offset: 0x000873A4
		public void EquipmentUpdated()
		{
			if (GameNetwork.IsServer)
			{
				MissionPeer component = GameNetwork.MyPeer.GetComponent<MissionPeer>();
				if (component.SelectedTroopIndex != component.NextSelectedTroopIndex)
				{
					component.SelectedTroopIndex = component.NextSelectedTroopIndex;
					GameNetwork.BeginBroadcastModuleEvent();
					GameNetwork.WriteMessage(new UpdateSelectedTroopIndex(GameNetwork.MyPeer, component.SelectedTroopIndex));
					GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.ExcludeOtherTeamPlayers, GameNetwork.MyPeer);
					if (this.OnEquipmentRefreshed != null)
					{
						this.OnEquipmentRefreshed(component);
						return;
					}
				}
			}
			else
			{
				MissionPeer component2 = GameNetwork.MyPeer.GetComponent<MissionPeer>();
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new RequestTroopIndexChange(component2.NextSelectedTroopIndex));
				GameNetwork.EndModuleEventAsClient();
			}
		}

		// Token: 0x060025BC RID: 9660 RVA: 0x0008923B File Offset: 0x0008743B
		public void ToggleLoadout(bool isActive)
		{
			if (this.OnToggleLoadout != null)
			{
				this.OnToggleLoadout(isActive);
			}
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x00089251 File Offset: 0x00087451
		private void OpenLoadout()
		{
			this.ToggleLoadout(true);
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x0008925A File Offset: 0x0008745A
		private void CloseLoadout()
		{
			this.ToggleLoadout(false);
		}

		// Token: 0x04000E85 RID: 3717
		private MultiplayerMissionAgentVisualSpawnComponent _agentVisualSpawnComponent;

		// Token: 0x02000579 RID: 1401
		// (Invoke) Token: 0x06003D3F RID: 15679
		public delegate void OnToggleLoadoutDelegate(bool isActive);

		// Token: 0x0200057A RID: 1402
		// (Invoke) Token: 0x06003D43 RID: 15683
		public delegate void OnRefreshEquipmentEventDelegate(MissionPeer lobbyPeer);
	}
}
