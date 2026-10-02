using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000324 RID: 804
	public abstract class UdpNetworkComponent : IUdpNetworkHandler
	{
		// Token: 0x06002D9D RID: 11677 RVA: 0x000B00A0 File Offset: 0x000AE2A0
		protected UdpNetworkComponent()
		{
			this._missionNetworkMessageHandlerRegisterer = new GameNetwork.NetworkMessageHandlerRegistererContainer();
			this.AddRemoveMessageHandlers(this._missionNetworkMessageHandlerRegisterer);
			this._missionNetworkMessageHandlerRegisterer.RegisterMessages();
		}

		// Token: 0x06002D9E RID: 11678 RVA: 0x000B00CA File Offset: 0x000AE2CA
		protected virtual void AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegistererContainer registerer)
		{
		}

		// Token: 0x06002D9F RID: 11679 RVA: 0x000B00CC File Offset: 0x000AE2CC
		public virtual void OnUdpNetworkHandlerClose()
		{
			GameNetwork.NetworkMessageHandlerRegistererContainer missionNetworkMessageHandlerRegisterer = this._missionNetworkMessageHandlerRegisterer;
			if (missionNetworkMessageHandlerRegisterer != null)
			{
				missionNetworkMessageHandlerRegisterer.UnregisterMessages();
			}
			GameNetwork.NetworkComponents.Remove(this);
		}

		// Token: 0x06002DA0 RID: 11680 RVA: 0x000B00EB File Offset: 0x000AE2EB
		public virtual void OnUdpNetworkHandlerTick(float dt)
		{
		}

		// Token: 0x06002DA1 RID: 11681 RVA: 0x000B00ED File Offset: 0x000AE2ED
		public virtual void HandleNewClientConnect(PlayerConnectionInfo clientConnectionInfo)
		{
		}

		// Token: 0x06002DA2 RID: 11682 RVA: 0x000B00EF File Offset: 0x000AE2EF
		public virtual void HandleEarlyNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x06002DA3 RID: 11683 RVA: 0x000B00F1 File Offset: 0x000AE2F1
		public virtual void HandleNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x000B00F3 File Offset: 0x000AE2F3
		public virtual void HandleLateNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x06002DA5 RID: 11685 RVA: 0x000B00F5 File Offset: 0x000AE2F5
		public virtual void HandleNewClientAfterSynchronized(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x06002DA6 RID: 11686 RVA: 0x000B00F7 File Offset: 0x000AE2F7
		public virtual void HandleLateNewClientAfterSynchronized(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x06002DA7 RID: 11687 RVA: 0x000B00F9 File Offset: 0x000AE2F9
		public virtual void OnEveryoneUnSynchronized()
		{
		}

		// Token: 0x06002DA8 RID: 11688 RVA: 0x000B00FB File Offset: 0x000AE2FB
		public void HandleEarlyPlayerDisconnect(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x06002DA9 RID: 11689 RVA: 0x000B00FD File Offset: 0x000AE2FD
		public virtual void HandlePlayerDisconnect(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x000B00FF File Offset: 0x000AE2FF
		public virtual void OnPlayerDisconnectedFromServer(NetworkCommunicator networkPeer)
		{
		}

		// Token: 0x06002DAB RID: 11691 RVA: 0x000B0101 File Offset: 0x000AE301
		public virtual void OnDisconnectedFromServer()
		{
		}

		// Token: 0x040011FD RID: 4605
		private GameNetwork.NetworkMessageHandlerRegistererContainer _missionNetworkMessageHandlerRegisterer;
	}
}
