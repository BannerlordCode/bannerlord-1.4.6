using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000073 RID: 115
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class AddClanAnnouncementMessage : Message
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00003911 File Offset: 0x00001B11
		// (set) Token: 0x0600023D RID: 573 RVA: 0x00003919 File Offset: 0x00001B19
		[JsonProperty]
		public string Announcement { get; private set; }

		// Token: 0x0600023E RID: 574 RVA: 0x00003922 File Offset: 0x00001B22
		public AddClanAnnouncementMessage()
		{
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000392A File Offset: 0x00001B2A
		public AddClanAnnouncementMessage(string announcement)
		{
			this.Announcement = announcement;
		}
	}
}
