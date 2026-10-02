using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x0200007D RID: 125
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class ChangeGameTypesMessage : Message
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00003AA1 File Offset: 0x00001CA1
		// (set) Token: 0x06000264 RID: 612 RVA: 0x00003AA9 File Offset: 0x00001CA9
		[JsonProperty]
		public string[] GameTypes { get; private set; }

		// Token: 0x06000265 RID: 613 RVA: 0x00003AB2 File Offset: 0x00001CB2
		public ChangeGameTypesMessage()
		{
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00003ABA File Offset: 0x00001CBA
		public ChangeGameTypesMessage(string[] gameTypes)
		{
			this.GameTypes = gameTypes;
		}
	}
}
