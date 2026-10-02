using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000037 RID: 55
	[Serializable]
	public class GetDedicatedCustomServerAuthTokenMessageResult : FunctionResult
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00002D0A File Offset: 0x00000F0A
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00002D12 File Offset: 0x00000F12
		[JsonProperty]
		public string AuthToken { get; private set; }

		// Token: 0x0600011D RID: 285 RVA: 0x00002D1B File Offset: 0x00000F1B
		public GetDedicatedCustomServerAuthTokenMessageResult()
		{
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00002D23 File Offset: 0x00000F23
		public GetDedicatedCustomServerAuthTokenMessageResult(string authToken)
		{
			this.AuthToken = authToken;
		}
	}
}
