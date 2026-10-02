using System;
using Newtonsoft.Json;
using TaleWorlds.Core;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000C3 RID: 195
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class UpdateCharacterMessage : Message
	{
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000383 RID: 899 RVA: 0x00004676 File Offset: 0x00002876
		// (set) Token: 0x06000384 RID: 900 RVA: 0x0000467E File Offset: 0x0000287E
		[JsonProperty]
		public BodyProperties BodyProperties { get; private set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000385 RID: 901 RVA: 0x00004687 File Offset: 0x00002887
		// (set) Token: 0x06000386 RID: 902 RVA: 0x0000468F File Offset: 0x0000288F
		[JsonProperty]
		public bool IsFemale { get; private set; }

		// Token: 0x06000387 RID: 903 RVA: 0x00004698 File Offset: 0x00002898
		public UpdateCharacterMessage()
		{
		}

		// Token: 0x06000388 RID: 904 RVA: 0x000046A0 File Offset: 0x000028A0
		public UpdateCharacterMessage(BodyProperties bodyProperties, bool isFemale)
		{
			this.BodyProperties = bodyProperties;
			this.IsFemale = isFemale;
		}
	}
}
