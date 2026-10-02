using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000074 RID: 116
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class AddFriendByUsernameAndIdMessage : Message
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00003939 File Offset: 0x00001B39
		// (set) Token: 0x06000241 RID: 577 RVA: 0x00003941 File Offset: 0x00001B41
		[JsonProperty]
		public string Username { get; private set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000394A File Offset: 0x00001B4A
		// (set) Token: 0x06000243 RID: 579 RVA: 0x00003952 File Offset: 0x00001B52
		[JsonProperty]
		public int UserId { get; private set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000395B File Offset: 0x00001B5B
		// (set) Token: 0x06000245 RID: 581 RVA: 0x00003963 File Offset: 0x00001B63
		[JsonProperty]
		public bool DontUseNameForUnknownPlayer { get; private set; }

		// Token: 0x06000246 RID: 582 RVA: 0x0000396C File Offset: 0x00001B6C
		public AddFriendByUsernameAndIdMessage()
		{
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00003974 File Offset: 0x00001B74
		public AddFriendByUsernameAndIdMessage(string username, int userId, bool dontUseNameForUnknownPlayer)
		{
			this.Username = username;
			this.UserId = userId;
			this.DontUseNameForUnknownPlayer = dontUseNameForUnknownPlayer;
		}
	}
}
