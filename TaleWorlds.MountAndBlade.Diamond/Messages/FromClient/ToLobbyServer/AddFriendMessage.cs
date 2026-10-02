using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000075 RID: 117
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class AddFriendMessage : Message
	{
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00003991 File Offset: 0x00001B91
		// (set) Token: 0x06000249 RID: 585 RVA: 0x00003999 File Offset: 0x00001B99
		[JsonProperty]
		public PlayerId FriendId { get; private set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600024A RID: 586 RVA: 0x000039A2 File Offset: 0x00001BA2
		// (set) Token: 0x0600024B RID: 587 RVA: 0x000039AA File Offset: 0x00001BAA
		[JsonProperty]
		public bool DontUseNameForUnknownPlayer { get; private set; }

		// Token: 0x0600024C RID: 588 RVA: 0x000039B3 File Offset: 0x00001BB3
		public AddFriendMessage()
		{
		}

		// Token: 0x0600024D RID: 589 RVA: 0x000039BB File Offset: 0x00001BBB
		public AddFriendMessage(PlayerId friendId, bool dontUseNameForUnknownPlayer)
		{
			this.FriendId = friendId;
			this.DontUseNameForUnknownPlayer = dontUseNameForUnknownPlayer;
		}
	}
}
