using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000BB RID: 187
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class RemoveFriendMessage : Message
	{
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00004494 File Offset: 0x00002694
		// (set) Token: 0x06000357 RID: 855 RVA: 0x0000449C File Offset: 0x0000269C
		[JsonProperty]
		public PlayerId FriendId { get; private set; }

		// Token: 0x06000358 RID: 856 RVA: 0x000044A5 File Offset: 0x000026A5
		public RemoveFriendMessage()
		{
		}

		// Token: 0x06000359 RID: 857 RVA: 0x000044AD File Offset: 0x000026AD
		public RemoveFriendMessage(PlayerId friendId)
		{
			this.FriendId = friendId;
		}
	}
}
