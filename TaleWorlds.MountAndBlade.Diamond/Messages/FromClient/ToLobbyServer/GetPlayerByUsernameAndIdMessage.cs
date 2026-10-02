using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x0200009E RID: 158
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class GetPlayerByUsernameAndIdMessage : Message
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00003F1F File Offset: 0x0000211F
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x00003F27 File Offset: 0x00002127
		[JsonProperty]
		public string Username { get; private set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00003F30 File Offset: 0x00002130
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x00003F38 File Offset: 0x00002138
		[JsonProperty]
		public int UserId { get; private set; }

		// Token: 0x060002D7 RID: 727 RVA: 0x00003F41 File Offset: 0x00002141
		public GetPlayerByUsernameAndIdMessage()
		{
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00003F49 File Offset: 0x00002149
		public GetPlayerByUsernameAndIdMessage(string username, int userId)
		{
			this.Username = username;
			this.UserId = userId;
		}
	}
}
