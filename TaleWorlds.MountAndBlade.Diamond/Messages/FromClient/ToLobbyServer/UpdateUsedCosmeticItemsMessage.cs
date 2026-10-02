using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000C7 RID: 199
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class UpdateUsedCosmeticItemsMessage : Message
	{
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0000475E File Offset: 0x0000295E
		// (set) Token: 0x0600039A RID: 922 RVA: 0x00004766 File Offset: 0x00002966
		[JsonProperty]
		public List<CosmeticItemInfo> UsedCosmetics { get; private set; }

		// Token: 0x0600039B RID: 923 RVA: 0x0000476F File Offset: 0x0000296F
		public UpdateUsedCosmeticItemsMessage()
		{
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00004777 File Offset: 0x00002977
		public UpdateUsedCosmeticItemsMessage(List<CosmeticItemInfo> usedCosmetics)
		{
			this.UsedCosmetics = usedCosmetics;
		}
	}
}
