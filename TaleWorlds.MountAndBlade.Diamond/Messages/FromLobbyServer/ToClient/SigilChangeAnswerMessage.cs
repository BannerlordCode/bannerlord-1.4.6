using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000067 RID: 103
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class SigilChangeAnswerMessage : Message
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000372C File Offset: 0x0000192C
		// (set) Token: 0x0600020E RID: 526 RVA: 0x00003734 File Offset: 0x00001934
		[JsonProperty]
		public bool Successful { get; private set; }

		// Token: 0x0600020F RID: 527 RVA: 0x0000373D File Offset: 0x0000193D
		public SigilChangeAnswerMessage()
		{
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00003745 File Offset: 0x00001945
		public SigilChangeAnswerMessage(bool answer)
		{
			this.Successful = answer;
		}
	}
}
