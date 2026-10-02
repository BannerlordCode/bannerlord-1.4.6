using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000038 RID: 56
	[Serializable]
	public class GetOfficialServerProviderNameResult : FunctionResult
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00002D32 File Offset: 0x00000F32
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00002D3A File Offset: 0x00000F3A
		[JsonProperty]
		public string Name { get; private set; }

		// Token: 0x06000121 RID: 289 RVA: 0x00002D43 File Offset: 0x00000F43
		public GetOfficialServerProviderNameResult()
		{
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00002D4B File Offset: 0x00000F4B
		public GetOfficialServerProviderNameResult(string name)
		{
			this.Name = name;
		}
	}
}
