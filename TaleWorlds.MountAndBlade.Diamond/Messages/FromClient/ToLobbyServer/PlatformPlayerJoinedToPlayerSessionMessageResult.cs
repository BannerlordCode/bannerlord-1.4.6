using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000CB RID: 203
	[Serializable]
	public class PlatformPlayerJoinedToPlayerSessionMessageResult : FunctionResult
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x000047F6 File Offset: 0x000029F6
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x000047FE File Offset: 0x000029FE
		[JsonProperty]
		public bool Successful { get; private set; }

		// Token: 0x060003AA RID: 938 RVA: 0x00004807 File Offset: 0x00002A07
		public PlatformPlayerJoinedToPlayerSessionMessageResult()
		{
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000480F File Offset: 0x00002A0F
		public PlatformPlayerJoinedToPlayerSessionMessageResult(bool successful)
		{
			this.Successful = successful;
		}
	}
}
