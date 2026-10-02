using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x0200008E RID: 142
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class EditClanAnnouncementMessage : Message
	{
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x00003D7E File Offset: 0x00001F7E
		// (set) Token: 0x060002AA RID: 682 RVA: 0x00003D86 File Offset: 0x00001F86
		[JsonProperty]
		public int AnnouncementId { get; private set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002AB RID: 683 RVA: 0x00003D8F File Offset: 0x00001F8F
		// (set) Token: 0x060002AC RID: 684 RVA: 0x00003D97 File Offset: 0x00001F97
		[JsonProperty]
		public string Text { get; private set; }

		// Token: 0x060002AD RID: 685 RVA: 0x00003DA0 File Offset: 0x00001FA0
		public EditClanAnnouncementMessage()
		{
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00003DA8 File Offset: 0x00001FA8
		public EditClanAnnouncementMessage(int announcementId, string text)
		{
			this.AnnouncementId = announcementId;
			this.Text = text;
		}
	}
}
