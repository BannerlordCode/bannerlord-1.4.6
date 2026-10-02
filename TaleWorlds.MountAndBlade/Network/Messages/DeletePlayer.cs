using System;

namespace TaleWorlds.MountAndBlade.Network.Messages
{
	// Token: 0x020003BE RID: 958
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class DeletePlayer : GameNetworkMessage
	{
		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x060035A1 RID: 13729 RVA: 0x000DD301 File Offset: 0x000DB501
		// (set) Token: 0x060035A2 RID: 13730 RVA: 0x000DD309 File Offset: 0x000DB509
		public int PlayerIndex { get; private set; }

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x060035A3 RID: 13731 RVA: 0x000DD312 File Offset: 0x000DB512
		// (set) Token: 0x060035A4 RID: 13732 RVA: 0x000DD31A File Offset: 0x000DB51A
		public bool AddToDisconnectList { get; private set; }

		// Token: 0x060035A5 RID: 13733 RVA: 0x000DD323 File Offset: 0x000DB523
		public DeletePlayer(int playerIndex, bool addToDisconnectList)
		{
			this.PlayerIndex = playerIndex;
			this.AddToDisconnectList = addToDisconnectList;
		}

		// Token: 0x060035A6 RID: 13734 RVA: 0x000DD339 File Offset: 0x000DB539
		public DeletePlayer()
		{
		}

		// Token: 0x060035A7 RID: 13735 RVA: 0x000DD341 File Offset: 0x000DB541
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket(this.PlayerIndex, CompressionBasic.PlayerCompressionInfo);
			GameNetworkMessage.WriteBoolToPacket(this.AddToDisconnectList);
		}

		// Token: 0x060035A8 RID: 13736 RVA: 0x000DD360 File Offset: 0x000DB560
		protected override bool OnRead()
		{
			bool flag = true;
			this.PlayerIndex = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.PlayerCompressionInfo, ref flag);
			this.AddToDisconnectList = GameNetworkMessage.ReadBoolFromPacket(ref flag);
			return flag;
		}

		// Token: 0x060035A9 RID: 13737 RVA: 0x000DD38F File Offset: 0x000DB58F
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Peers;
		}

		// Token: 0x060035AA RID: 13738 RVA: 0x000DD393 File Offset: 0x000DB593
		protected override string OnGetLogFormat()
		{
			return "Delete player with index" + this.PlayerIndex;
		}
	}
}
