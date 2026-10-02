using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000CC RID: 204
	[Serializable]
	public class PSPlayerJoinedToPlayerSessionMessageResult : FunctionResult
	{
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0000481E File Offset: 0x00002A1E
		// (set) Token: 0x060003AD RID: 941 RVA: 0x00004826 File Offset: 0x00002A26
		[JsonProperty]
		public bool Successful { get; private set; }

		// Token: 0x060003AE RID: 942 RVA: 0x0000482F File Offset: 0x00002A2F
		public PSPlayerJoinedToPlayerSessionMessageResult()
		{
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00004837 File Offset: 0x00002A37
		public PSPlayerJoinedToPlayerSessionMessageResult(bool successful)
		{
			this.Successful = successful;
		}
	}
}
