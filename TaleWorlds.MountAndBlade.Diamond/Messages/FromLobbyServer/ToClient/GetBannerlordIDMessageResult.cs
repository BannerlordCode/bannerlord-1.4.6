using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000034 RID: 52
	[Serializable]
	public class GetBannerlordIDMessageResult : FunctionResult
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00002C92 File Offset: 0x00000E92
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00002C9A File Offset: 0x00000E9A
		[JsonProperty]
		public string BannerlordID { get; private set; }

		// Token: 0x06000111 RID: 273 RVA: 0x00002CA3 File Offset: 0x00000EA3
		public GetBannerlordIDMessageResult()
		{
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00002CAB File Offset: 0x00000EAB
		public GetBannerlordIDMessageResult(string bannerlordID)
		{
			this.BannerlordID = bannerlordID;
		}
	}
}
