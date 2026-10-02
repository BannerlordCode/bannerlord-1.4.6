using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000050 RID: 80
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class MatchmakerDisabledMessage : Message
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x000032D8 File Offset: 0x000014D8
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x000032E0 File Offset: 0x000014E0
		[JsonProperty]
		public int RemainingTime { get; private set; }

		// Token: 0x060001A8 RID: 424 RVA: 0x000032E9 File Offset: 0x000014E9
		public MatchmakerDisabledMessage()
		{
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000032F1 File Offset: 0x000014F1
		public MatchmakerDisabledMessage(int remainingTime)
		{
			this.RemainingTime = remainingTime;
		}
	}
}
