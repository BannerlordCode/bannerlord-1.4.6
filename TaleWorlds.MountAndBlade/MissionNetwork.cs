using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200029D RID: 669
	public abstract class MissionNetwork : MissionLogic, IUdpNetworkHandler
	{
		// Token: 0x060024E8 RID: 9448 RVA: 0x00086998 File Offset: 0x00084B98
		public override void OnAfterMissionCreated()
		{
			this._missionNetworkMessageHandlerRegisterer = new GameNetwork.NetworkMessageHandlerRegistererContainer();
			this.AddRemoveMessageHandlers(this._missionNetworkMessageHandlerRegisterer);
			this._missionNetworkMessageHandlerRegisterer.RegisterMessages();
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x000869BC File Offset: 0x00084BBC
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			GameNetwork.AddNetworkHandler(this);
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x000869CA File Offset: 0x00084BCA
		public override void OnRemoveBehavior()
		{
			GameNetwork.RemoveNetworkHandler(this);
			base.OnRemoveBehavior();
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x000869D8 File Offset: 0x00084BD8
		protected virtual void AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegistererContainer registerer)
		{
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x000869DA File Offset: 0x00084BDA
		public virtual void OnPlayerConnectedToServer(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x000869DC File Offset: 0x00084BDC
		public virtual void OnPlayerDisconnectedFromServer(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x000869DE File Offset: 0x00084BDE
		void IUdpNetworkHandler.OnUdpNetworkHandlerTick(float dt)
		{
			this.OnUdpNetworkHandlerTick();
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x000869E6 File Offset: 0x00084BE6
		void IUdpNetworkHandler.OnUdpNetworkHandlerClose()
		{
			this.OnUdpNetworkHandlerClose();
			GameNetwork.NetworkMessageHandlerRegistererContainer missionNetworkMessageHandlerRegisterer = this._missionNetworkMessageHandlerRegisterer;
			if (missionNetworkMessageHandlerRegisterer == null)
			{
				return;
			}
			missionNetworkMessageHandlerRegisterer.UnregisterMessages();
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x000869FE File Offset: 0x00084BFE
		void IUdpNetworkHandler.HandleNewClientConnect(PlayerConnectionInfo clientConnectionInfo)
		{
			this.HandleNewClientConnect(clientConnectionInfo);
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x00086A07 File Offset: 0x00084C07
		void IUdpNetworkHandler.HandleEarlyNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
			this.HandleEarlyNewClientAfterLoadingFinished(networkPeer);
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x00086A10 File Offset: 0x00084C10
		void IUdpNetworkHandler.HandleNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
			this.HandleNewClientAfterLoadingFinished(networkPeer);
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x00086A19 File Offset: 0x00084C19
		void IUdpNetworkHandler.HandleLateNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
			this.HandleLateNewClientAfterLoadingFinished(networkPeer);
		}

		// Token: 0x060024F4 RID: 9460 RVA: 0x00086A22 File Offset: 0x00084C22
		void IUdpNetworkHandler.HandleNewClientAfterSynchronized(NetworkCommunicator networkPeer)
		{
			this.HandleNewClientAfterSynchronized(networkPeer);
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x00086A2B File Offset: 0x00084C2B
		void IUdpNetworkHandler.HandleLateNewClientAfterSynchronized(NetworkCommunicator networkPeer)
		{
			this.HandleLateNewClientAfterSynchronized(networkPeer);
		}

		// Token: 0x060024F6 RID: 9462 RVA: 0x00086A34 File Offset: 0x00084C34
		void IUdpNetworkHandler.HandleEarlyPlayerDisconnect(NetworkCommunicator networkPeer)
		{
			this.HandleEarlyPlayerDisconnect(networkPeer);
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x00086A3D File Offset: 0x00084C3D
		void IUdpNetworkHandler.HandlePlayerDisconnect(NetworkCommunicator networkPeer)
		{
			this.HandlePlayerDisconnect(networkPeer);
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x00086A46 File Offset: 0x00084C46
		void IUdpNetworkHandler.OnEveryoneUnSynchronized()
		{
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x00086A48 File Offset: 0x00084C48
		void IUdpNetworkHandler.OnPlayerDisconnectedFromServer(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x00086A4A File Offset: 0x00084C4A
		void IUdpNetworkHandler.OnDisconnectedFromServer()
		{
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x00086A4C File Offset: 0x00084C4C
		protected virtual void OnUdpNetworkHandlerTick()
		{
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x00086A4E File Offset: 0x00084C4E
		protected virtual void OnUdpNetworkHandlerClose()
		{
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x00086A50 File Offset: 0x00084C50
		protected virtual void HandleNewClientConnect(PlayerConnectionInfo clientConnectionInfo)
		{
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x00086A52 File Offset: 0x00084C52
		protected virtual void HandleEarlyNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x00086A54 File Offset: 0x00084C54
		protected virtual void HandleNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x00086A56 File Offset: 0x00084C56
		protected virtual void HandleLateNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x00086A58 File Offset: 0x00084C58
		protected virtual void HandleNewClientAfterSynchronized(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x00086A5A File Offset: 0x00084C5A
		protected virtual void HandleLateNewClientAfterSynchronized(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x00086A5C File Offset: 0x00084C5C
		protected virtual void HandleEarlyPlayerDisconnect(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x00086A5E File Offset: 0x00084C5E
		protected virtual void HandlePlayerDisconnect(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x04000E53 RID: 3667
		private GameNetwork.NetworkMessageHandlerRegistererContainer _missionNetworkMessageHandlerRegisterer;
	}
}
