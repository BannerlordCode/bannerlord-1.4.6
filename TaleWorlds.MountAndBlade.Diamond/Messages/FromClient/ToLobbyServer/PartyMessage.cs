using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000B0 RID: 176
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class PartyMessage : Message
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600031E RID: 798 RVA: 0x00004231 File Offset: 0x00002431
		// (set) Token: 0x0600031F RID: 799 RVA: 0x00004239 File Offset: 0x00002439
		[JsonProperty]
		public string Message { get; private set; }

		// Token: 0x06000320 RID: 800 RVA: 0x00004242 File Offset: 0x00002442
		public PartyMessage()
		{
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000424A File Offset: 0x0000244A
		public PartyMessage(string message)
		{
			this.Message = message;
		}
	}
}
