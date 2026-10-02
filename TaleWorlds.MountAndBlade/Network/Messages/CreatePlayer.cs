using System;

namespace TaleWorlds.MountAndBlade.Network.Messages
{
	// Token: 0x020003BD RID: 957
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class CreatePlayer : GameNetworkMessage
	{
		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06003591 RID: 13713 RVA: 0x000DD149 File Offset: 0x000DB349
		// (set) Token: 0x06003592 RID: 13714 RVA: 0x000DD151 File Offset: 0x000DB351
		public int PlayerIndex { get; private set; }

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06003593 RID: 13715 RVA: 0x000DD15A File Offset: 0x000DB35A
		// (set) Token: 0x06003594 RID: 13716 RVA: 0x000DD162 File Offset: 0x000DB362
		public string PlayerName { get; private set; }

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06003595 RID: 13717 RVA: 0x000DD16B File Offset: 0x000DB36B
		// (set) Token: 0x06003596 RID: 13718 RVA: 0x000DD173 File Offset: 0x000DB373
		public int DisconnectedPeerIndex { get; private set; }

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06003597 RID: 13719 RVA: 0x000DD17C File Offset: 0x000DB37C
		// (set) Token: 0x06003598 RID: 13720 RVA: 0x000DD184 File Offset: 0x000DB384
		public bool IsNonExistingDisconnectedPeer { get; private set; }

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06003599 RID: 13721 RVA: 0x000DD18D File Offset: 0x000DB38D
		// (set) Token: 0x0600359A RID: 13722 RVA: 0x000DD195 File Offset: 0x000DB395
		public bool IsReceiverPeer { get; private set; }

		// Token: 0x0600359B RID: 13723 RVA: 0x000DD19E File Offset: 0x000DB39E
		public CreatePlayer(int playerIndex, string playerName, int disconnectedPeerIndex, bool isNonExistingDisconnectedPeer = false, bool isReceiverPeer = false)
		{
			this.PlayerIndex = playerIndex;
			this.PlayerName = playerName;
			this.DisconnectedPeerIndex = disconnectedPeerIndex;
			this.IsNonExistingDisconnectedPeer = isNonExistingDisconnectedPeer;
			this.IsReceiverPeer = isReceiverPeer;
		}

		// Token: 0x0600359C RID: 13724 RVA: 0x000DD1CB File Offset: 0x000DB3CB
		public CreatePlayer()
		{
		}

		// Token: 0x0600359D RID: 13725 RVA: 0x000DD1D4 File Offset: 0x000DB3D4
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket(this.PlayerIndex, CompressionBasic.PlayerCompressionInfo);
			GameNetworkMessage.WriteStringToPacket(this.PlayerName);
			GameNetworkMessage.WriteIntToPacket(this.DisconnectedPeerIndex, CompressionBasic.PlayerCompressionInfo);
			GameNetworkMessage.WriteBoolToPacket(this.IsNonExistingDisconnectedPeer);
			GameNetworkMessage.WriteBoolToPacket(this.IsReceiverPeer);
		}

		// Token: 0x0600359E RID: 13726 RVA: 0x000DD224 File Offset: 0x000DB424
		protected override bool OnRead()
		{
			bool flag = true;
			this.PlayerIndex = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.PlayerCompressionInfo, ref flag);
			this.PlayerName = GameNetworkMessage.ReadStringFromPacket(ref flag);
			this.DisconnectedPeerIndex = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.PlayerCompressionInfo, ref flag);
			this.IsNonExistingDisconnectedPeer = GameNetworkMessage.ReadBoolFromPacket(ref flag);
			this.IsReceiverPeer = GameNetworkMessage.ReadBoolFromPacket(ref flag);
			return flag;
		}

		// Token: 0x0600359F RID: 13727 RVA: 0x000DD27F File Offset: 0x000DB47F
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Peers;
		}

		// Token: 0x060035A0 RID: 13728 RVA: 0x000DD284 File Offset: 0x000DB484
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Create a new player with name: ",
				this.PlayerName,
				" and index: ",
				this.PlayerIndex,
				" and dcedIndex: ",
				this.DisconnectedPeerIndex,
				" which is ",
				(!this.IsNonExistingDisconnectedPeer) ? "not" : "",
				" a NonExistingDisconnectedPeer"
			});
		}
	}
}
