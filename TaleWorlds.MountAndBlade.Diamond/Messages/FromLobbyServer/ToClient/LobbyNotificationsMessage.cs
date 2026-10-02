using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200004F RID: 79
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class LobbyNotificationsMessage : Message
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x000032B0 File Offset: 0x000014B0
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x000032B8 File Offset: 0x000014B8
		[JsonProperty]
		public LobbyNotification[] Notifications { get; private set; }

		// Token: 0x060001A4 RID: 420 RVA: 0x000032C1 File Offset: 0x000014C1
		public LobbyNotificationsMessage()
		{
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000032C9 File Offset: 0x000014C9
		public LobbyNotificationsMessage(LobbyNotification[] notifications)
		{
			this.Notifications = notifications;
		}
	}
}
