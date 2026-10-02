using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000BE RID: 190
	[MessageDescription("Client", "LobbyServer", false)]
	[Serializable]
	public class RequestJoinCustomGameMessage : Message
	{
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000454E File Offset: 0x0000274E
		// (set) Token: 0x06000368 RID: 872 RVA: 0x00004556 File Offset: 0x00002756
		[JsonProperty]
		public CustomBattleId CustomBattleId { get; private set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000455F File Offset: 0x0000275F
		// (set) Token: 0x0600036A RID: 874 RVA: 0x00004567 File Offset: 0x00002767
		[JsonProperty]
		public string Password { get; private set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600036B RID: 875 RVA: 0x00004570 File Offset: 0x00002770
		// (set) Token: 0x0600036C RID: 876 RVA: 0x00004578 File Offset: 0x00002778
		[JsonProperty]
		public bool IsJoinAsAdminOnly { get; private set; }

		// Token: 0x0600036D RID: 877 RVA: 0x00004581 File Offset: 0x00002781
		public RequestJoinCustomGameMessage()
		{
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00004589 File Offset: 0x00002789
		public RequestJoinCustomGameMessage(CustomBattleId customBattleId, string password = "", bool isJoinAsAdminOnly = false)
		{
			this.CustomBattleId = customBattleId;
			this.Password = password;
			this.IsJoinAsAdminOnly = isJoinAsAdminOnly;
		}
	}
}
