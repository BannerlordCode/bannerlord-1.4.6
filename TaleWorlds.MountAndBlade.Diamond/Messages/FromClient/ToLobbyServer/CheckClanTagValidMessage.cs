using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000082 RID: 130
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class CheckClanTagValidMessage : Message
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000277 RID: 631 RVA: 0x00003B69 File Offset: 0x00001D69
		// (set) Token: 0x06000278 RID: 632 RVA: 0x00003B71 File Offset: 0x00001D71
		[JsonProperty]
		public string ClanTag { get; private set; }

		// Token: 0x06000279 RID: 633 RVA: 0x00003B7A File Offset: 0x00001D7A
		public CheckClanTagValidMessage()
		{
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00003B82 File Offset: 0x00001D82
		public CheckClanTagValidMessage(string clanTag)
		{
			this.ClanTag = clanTag;
		}
	}
}
