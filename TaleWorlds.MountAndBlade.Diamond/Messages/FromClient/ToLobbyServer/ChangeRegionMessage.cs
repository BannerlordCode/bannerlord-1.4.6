using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x0200007F RID: 127
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class ChangeRegionMessage : Message
	{
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600026B RID: 619 RVA: 0x00003AF1 File Offset: 0x00001CF1
		// (set) Token: 0x0600026C RID: 620 RVA: 0x00003AF9 File Offset: 0x00001CF9
		[JsonProperty]
		public string Region { get; private set; }

		// Token: 0x0600026D RID: 621 RVA: 0x00003B02 File Offset: 0x00001D02
		public ChangeRegionMessage()
		{
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00003B0A File Offset: 0x00001D0A
		public ChangeRegionMessage(string region)
		{
			this.Region = region;
		}
	}
}
