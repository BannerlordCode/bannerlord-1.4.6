using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200005F RID: 95
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class RecentPlayerStatusesMessage : Message
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x000035AB File Offset: 0x000017AB
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x000035B3 File Offset: 0x000017B3
		[JsonProperty]
		public FriendInfo[] Friends { get; private set; }

		// Token: 0x060001EA RID: 490 RVA: 0x000035BC File Offset: 0x000017BC
		public RecentPlayerStatusesMessage()
		{
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000035C4 File Offset: 0x000017C4
		public RecentPlayerStatusesMessage(FriendInfo[] friends)
		{
			this.Friends = friends;
		}
	}
}
