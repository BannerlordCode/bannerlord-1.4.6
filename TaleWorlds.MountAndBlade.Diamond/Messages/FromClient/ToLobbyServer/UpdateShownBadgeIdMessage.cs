using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000C6 RID: 198
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class UpdateShownBadgeIdMessage : Message
	{
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000395 RID: 917 RVA: 0x00004736 File Offset: 0x00002936
		// (set) Token: 0x06000396 RID: 918 RVA: 0x0000473E File Offset: 0x0000293E
		[JsonProperty]
		public string ShownBadgeId { get; private set; }

		// Token: 0x06000397 RID: 919 RVA: 0x00004747 File Offset: 0x00002947
		public UpdateShownBadgeIdMessage()
		{
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000474F File Offset: 0x0000294F
		public UpdateShownBadgeIdMessage(string shownBadgeId)
		{
			this.ShownBadgeId = shownBadgeId;
		}
	}
}
