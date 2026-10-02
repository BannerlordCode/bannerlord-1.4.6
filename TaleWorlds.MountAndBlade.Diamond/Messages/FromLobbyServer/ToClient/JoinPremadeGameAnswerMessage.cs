using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200004B RID: 75
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class JoinPremadeGameAnswerMessage : Message
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00003197 File Offset: 0x00001397
		// (set) Token: 0x0600018A RID: 394 RVA: 0x0000319F File Offset: 0x0000139F
		[JsonProperty]
		public bool Successful { get; private set; }

		// Token: 0x0600018B RID: 395 RVA: 0x000031A8 File Offset: 0x000013A8
		public JoinPremadeGameAnswerMessage()
		{
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000031B0 File Offset: 0x000013B0
		public JoinPremadeGameAnswerMessage(bool successful)
		{
			this.Successful = successful;
		}
	}
}
