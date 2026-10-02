using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000C2 RID: 194
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class SetClanInformationMessage : Message
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000464E File Offset: 0x0000284E
		// (set) Token: 0x06000380 RID: 896 RVA: 0x00004656 File Offset: 0x00002856
		[JsonProperty]
		public string Information { get; private set; }

		// Token: 0x06000381 RID: 897 RVA: 0x0000465F File Offset: 0x0000285F
		public SetClanInformationMessage()
		{
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00004667 File Offset: 0x00002867
		public SetClanInformationMessage(string information)
		{
			this.Information = information;
		}
	}
}
