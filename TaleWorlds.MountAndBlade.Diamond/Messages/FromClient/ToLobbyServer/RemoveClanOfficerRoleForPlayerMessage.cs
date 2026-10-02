using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000BA RID: 186
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class RemoveClanOfficerRoleForPlayerMessage : Message
	{
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000446C File Offset: 0x0000266C
		// (set) Token: 0x06000353 RID: 851 RVA: 0x00004474 File Offset: 0x00002674
		[JsonProperty]
		public PlayerId RemovedOfficerId { get; private set; }

		// Token: 0x06000354 RID: 852 RVA: 0x0000447D File Offset: 0x0000267D
		public RemoveClanOfficerRoleForPlayerMessage()
		{
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00004485 File Offset: 0x00002685
		public RemoveClanOfficerRoleForPlayerMessage(PlayerId removedOfficerId)
		{
			this.RemovedOfficerId = removedOfficerId;
		}
	}
}
