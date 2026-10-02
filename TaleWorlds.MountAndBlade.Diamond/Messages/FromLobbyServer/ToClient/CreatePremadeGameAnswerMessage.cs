using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000027 RID: 39
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class CreatePremadeGameAnswerMessage : Message
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00002A44 File Offset: 0x00000C44
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00002A4C File Offset: 0x00000C4C
		[JsonProperty]
		public bool Successful { get; private set; }

		// Token: 0x060000D8 RID: 216 RVA: 0x00002A55 File Offset: 0x00000C55
		public CreatePremadeGameAnswerMessage()
		{
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00002A5D File Offset: 0x00000C5D
		public CreatePremadeGameAnswerMessage(bool successful)
		{
			this.Successful = successful;
		}
	}
}
