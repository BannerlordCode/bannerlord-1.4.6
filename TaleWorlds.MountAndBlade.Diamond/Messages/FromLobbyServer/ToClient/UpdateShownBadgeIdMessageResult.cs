using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200006A RID: 106
	[Serializable]
	public class UpdateShownBadgeIdMessageResult : FunctionResult
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600021B RID: 539 RVA: 0x000037C1 File Offset: 0x000019C1
		// (set) Token: 0x0600021C RID: 540 RVA: 0x000037C9 File Offset: 0x000019C9
		[JsonProperty]
		public bool Successful { get; private set; }

		// Token: 0x0600021D RID: 541 RVA: 0x000037D2 File Offset: 0x000019D2
		public UpdateShownBadgeIdMessageResult()
		{
		}

		// Token: 0x0600021E RID: 542 RVA: 0x000037DA File Offset: 0x000019DA
		public UpdateShownBadgeIdMessageResult(bool successful)
		{
			this.Successful = successful;
		}
	}
}
