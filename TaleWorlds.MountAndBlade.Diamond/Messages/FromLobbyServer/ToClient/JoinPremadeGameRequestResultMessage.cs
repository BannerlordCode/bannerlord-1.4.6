using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200004D RID: 77
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class JoinPremadeGameRequestResultMessage : Message
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00003280 File Offset: 0x00001480
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00003288 File Offset: 0x00001488
		[JsonProperty]
		public bool Successful { get; private set; }

		// Token: 0x0600019F RID: 415 RVA: 0x00003291 File Offset: 0x00001491
		public JoinPremadeGameRequestResultMessage()
		{
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00003299 File Offset: 0x00001499
		public JoinPremadeGameRequestResultMessage(bool successful)
		{
			this.Successful = successful;
		}
	}
}
