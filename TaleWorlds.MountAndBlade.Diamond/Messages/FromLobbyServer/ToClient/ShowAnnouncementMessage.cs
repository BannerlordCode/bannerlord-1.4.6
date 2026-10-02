using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000066 RID: 102
	[Serializable]
	public class ShowAnnouncementMessage : Message
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00003704 File Offset: 0x00001904
		// (set) Token: 0x0600020A RID: 522 RVA: 0x0000370C File Offset: 0x0000190C
		[JsonProperty]
		public Announcement Announcement { get; private set; }

		// Token: 0x0600020B RID: 523 RVA: 0x00003715 File Offset: 0x00001915
		public ShowAnnouncementMessage()
		{
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000371D File Offset: 0x0000191D
		public ShowAnnouncementMessage(Announcement announcement)
		{
			this.Announcement = announcement;
		}
	}
}
