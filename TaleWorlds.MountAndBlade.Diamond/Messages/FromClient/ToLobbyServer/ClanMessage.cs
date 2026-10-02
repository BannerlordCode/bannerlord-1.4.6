using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000083 RID: 131
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class ClanMessage : Message
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600027B RID: 635 RVA: 0x00003B91 File Offset: 0x00001D91
		// (set) Token: 0x0600027C RID: 636 RVA: 0x00003B99 File Offset: 0x00001D99
		[JsonProperty]
		public string Message { get; private set; }

		// Token: 0x0600027D RID: 637 RVA: 0x00003BA2 File Offset: 0x00001DA2
		public ClanMessage()
		{
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00003BAA File Offset: 0x00001DAA
		public ClanMessage(string message)
		{
			this.Message = message;
		}
	}
}
