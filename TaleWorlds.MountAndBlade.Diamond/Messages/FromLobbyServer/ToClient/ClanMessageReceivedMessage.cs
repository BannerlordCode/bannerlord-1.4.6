using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000023 RID: 35
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class ClanMessageReceivedMessage : Message
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x0000298C File Offset: 0x00000B8C
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00002994 File Offset: 0x00000B94
		[JsonProperty]
		public string PlayerName { get; private set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x0000299D File Offset: 0x00000B9D
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x000029A5 File Offset: 0x00000BA5
		[JsonProperty]
		public string Message { get; private set; }

		// Token: 0x060000C8 RID: 200 RVA: 0x000029AE File Offset: 0x00000BAE
		public ClanMessageReceivedMessage()
		{
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000029B6 File Offset: 0x00000BB6
		public ClanMessageReceivedMessage(string playerName, string message)
		{
			this.PlayerName = playerName;
			this.Message = message;
		}
	}
}
