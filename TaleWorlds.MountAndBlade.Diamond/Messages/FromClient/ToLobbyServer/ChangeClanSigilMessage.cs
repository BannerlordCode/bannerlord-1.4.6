using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x0200007C RID: 124
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class ChangeClanSigilMessage : Message
	{
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00003A79 File Offset: 0x00001C79
		// (set) Token: 0x06000260 RID: 608 RVA: 0x00003A81 File Offset: 0x00001C81
		[JsonProperty]
		public string NewSigil { get; private set; }

		// Token: 0x06000261 RID: 609 RVA: 0x00003A8A File Offset: 0x00001C8A
		public ChangeClanSigilMessage()
		{
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00003A92 File Offset: 0x00001C92
		public ChangeClanSigilMessage(string newSigil)
		{
			this.NewSigil = newSigil;
		}
	}
}
