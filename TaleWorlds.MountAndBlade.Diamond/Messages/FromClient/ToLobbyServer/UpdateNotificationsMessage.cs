using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000C5 RID: 197
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class UpdateNotificationsMessage : Message
	{
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000470E File Offset: 0x0000290E
		// (set) Token: 0x06000392 RID: 914 RVA: 0x00004716 File Offset: 0x00002916
		[JsonProperty]
		public int[] SeenNotificationIds { get; private set; }

		// Token: 0x06000393 RID: 915 RVA: 0x0000471F File Offset: 0x0000291F
		public UpdateNotificationsMessage()
		{
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00004727 File Offset: 0x00002927
		public UpdateNotificationsMessage(int[] seenNotificationIds)
		{
			this.SeenNotificationIds = seenNotificationIds;
		}
	}
}
