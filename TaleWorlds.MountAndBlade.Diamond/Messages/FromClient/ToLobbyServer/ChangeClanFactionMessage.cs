using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x0200007B RID: 123
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class ChangeClanFactionMessage : Message
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00003A51 File Offset: 0x00001C51
		// (set) Token: 0x0600025C RID: 604 RVA: 0x00003A59 File Offset: 0x00001C59
		[JsonProperty]
		public string NewFaction { get; private set; }

		// Token: 0x0600025D RID: 605 RVA: 0x00003A62 File Offset: 0x00001C62
		public ChangeClanFactionMessage()
		{
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00003A6A File Offset: 0x00001C6A
		public ChangeClanFactionMessage(string newFaction)
		{
			this.NewFaction = newFaction;
		}
	}
}
