using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000078 RID: 120
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class BuyCosmeticMessage : Message
	{
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00003A19 File Offset: 0x00001C19
		// (set) Token: 0x06000256 RID: 598 RVA: 0x00003A21 File Offset: 0x00001C21
		[JsonProperty]
		public string CosmeticId { get; private set; }

		// Token: 0x06000257 RID: 599 RVA: 0x00003A2A File Offset: 0x00001C2A
		public BuyCosmeticMessage()
		{
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00003A32 File Offset: 0x00001C32
		public BuyCosmeticMessage(string cosmeticId)
		{
			this.CosmeticId = cosmeticId;
		}
	}
}
