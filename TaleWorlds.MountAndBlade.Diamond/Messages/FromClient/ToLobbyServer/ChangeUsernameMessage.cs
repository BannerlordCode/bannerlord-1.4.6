using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000080 RID: 128
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class ChangeUsernameMessage : Message
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00003B19 File Offset: 0x00001D19
		// (set) Token: 0x06000270 RID: 624 RVA: 0x00003B21 File Offset: 0x00001D21
		[JsonProperty]
		public string Username { get; private set; }

		// Token: 0x06000271 RID: 625 RVA: 0x00003B2A File Offset: 0x00001D2A
		public ChangeUsernameMessage()
		{
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00003B32 File Offset: 0x00001D32
		public ChangeUsernameMessage(string username)
		{
			this.Username = username;
		}
	}
}
