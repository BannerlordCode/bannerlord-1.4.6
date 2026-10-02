using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000028 RID: 40
	[Serializable]
	public class CreatePremadeGameMessageResult : FunctionResult
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00002A6C File Offset: 0x00000C6C
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00002A74 File Offset: 0x00000C74
		[JsonProperty]
		public bool Successful { get; private set; }

		// Token: 0x060000DC RID: 220 RVA: 0x00002A7D File Offset: 0x00000C7D
		public CreatePremadeGameMessageResult()
		{
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00002A85 File Offset: 0x00000C85
		public CreatePremadeGameMessageResult(bool successful)
		{
			this.Successful = successful;
		}
	}
}
