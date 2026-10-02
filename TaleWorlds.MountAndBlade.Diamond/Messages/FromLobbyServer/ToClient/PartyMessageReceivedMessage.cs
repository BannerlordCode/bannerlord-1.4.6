using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000052 RID: 82
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class PartyMessageReceivedMessage : Message
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00003308 File Offset: 0x00001508
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00003310 File Offset: 0x00001510
		[JsonProperty]
		public string PlayerName { get; private set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00003319 File Offset: 0x00001519
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00003321 File Offset: 0x00001521
		[JsonProperty]
		public string Message { get; private set; }

		// Token: 0x060001AF RID: 431 RVA: 0x0000332A File Offset: 0x0000152A
		public PartyMessageReceivedMessage()
		{
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00003332 File Offset: 0x00001532
		public PartyMessageReceivedMessage(string playerName, string message)
		{
			this.PlayerName = playerName;
			this.Message = message;
		}
	}
}
