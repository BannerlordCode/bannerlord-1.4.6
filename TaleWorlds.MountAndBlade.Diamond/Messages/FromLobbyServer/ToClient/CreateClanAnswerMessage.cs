using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000026 RID: 38
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class CreateClanAnswerMessage : Message
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00002A1C File Offset: 0x00000C1C
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00002A24 File Offset: 0x00000C24
		[JsonProperty]
		public bool Successful { get; private set; }

		// Token: 0x060000D4 RID: 212 RVA: 0x00002A2D File Offset: 0x00000C2D
		public CreateClanAnswerMessage()
		{
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00002A35 File Offset: 0x00000C35
		public CreateClanAnswerMessage(bool successful)
		{
			this.Successful = successful;
		}
	}
}
