using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace TaleWorlds.MountAndBlade.Diamond.Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200016B RID: 363
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class DisconnectedFromChatRoomMessage : Message
	{
		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x00010111 File Offset: 0x0000E311
		// (set) Token: 0x06000A0C RID: 2572 RVA: 0x00010119 File Offset: 0x0000E319
		[JsonProperty]
		public Guid RoomId { get; private set; }

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x00010122 File Offset: 0x0000E322
		// (set) Token: 0x06000A0E RID: 2574 RVA: 0x0001012A File Offset: 0x0000E32A
		[JsonProperty]
		public string RoomName { get; private set; }

		// Token: 0x06000A0F RID: 2575 RVA: 0x00010133 File Offset: 0x0000E333
		public DisconnectedFromChatRoomMessage()
		{
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0001013B File Offset: 0x0000E33B
		public DisconnectedFromChatRoomMessage(Guid roomId, string roomName)
		{
			this.RoomId = roomId;
			this.RoomName = roomName;
		}
	}
}
