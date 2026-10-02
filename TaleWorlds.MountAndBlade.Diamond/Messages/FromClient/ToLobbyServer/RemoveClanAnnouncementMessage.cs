using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000B9 RID: 185
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class RemoveClanAnnouncementMessage : Message
	{
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00004444 File Offset: 0x00002644
		// (set) Token: 0x0600034F RID: 847 RVA: 0x0000444C File Offset: 0x0000264C
		[JsonProperty]
		public int AnnouncementId { get; private set; }

		// Token: 0x06000350 RID: 848 RVA: 0x00004455 File Offset: 0x00002655
		public RemoveClanAnnouncementMessage()
		{
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000445D File Offset: 0x0000265D
		public RemoveClanAnnouncementMessage(int announcementId)
		{
			this.AnnouncementId = announcementId;
		}
	}
}
