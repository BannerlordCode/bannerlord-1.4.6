using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000B8 RID: 184
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class RejoinBattleRequestMessage : Message
	{
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0000441C File Offset: 0x0000261C
		// (set) Token: 0x0600034B RID: 843 RVA: 0x00004424 File Offset: 0x00002624
		[JsonProperty]
		public bool IsRejoinAccepted { get; private set; }

		// Token: 0x0600034C RID: 844 RVA: 0x0000442D File Offset: 0x0000262D
		public RejoinBattleRequestMessage()
		{
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00004435 File Offset: 0x00002635
		public RejoinBattleRequestMessage(bool isRejoinAccepted)
		{
			this.IsRejoinAccepted = isRejoinAccepted;
		}
	}
}
