using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000081 RID: 129
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class CheckClanNameValidMessage : Message
	{
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00003B41 File Offset: 0x00001D41
		// (set) Token: 0x06000274 RID: 628 RVA: 0x00003B49 File Offset: 0x00001D49
		[JsonProperty]
		public string ClanName { get; private set; }

		// Token: 0x06000275 RID: 629 RVA: 0x00003B52 File Offset: 0x00001D52
		public CheckClanNameValidMessage()
		{
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00003B5A File Offset: 0x00001D5A
		public CheckClanNameValidMessage(string clanName)
		{
			this.ClanName = clanName;
		}
	}
}
