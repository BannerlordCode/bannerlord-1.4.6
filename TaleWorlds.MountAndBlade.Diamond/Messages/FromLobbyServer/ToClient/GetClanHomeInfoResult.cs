using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000035 RID: 53
	[Serializable]
	public class GetClanHomeInfoResult : FunctionResult
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00002CBA File Offset: 0x00000EBA
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00002CC2 File Offset: 0x00000EC2
		[JsonProperty]
		public ClanHomeInfo ClanHomeInfo { get; private set; }

		// Token: 0x06000115 RID: 277 RVA: 0x00002CCB File Offset: 0x00000ECB
		public GetClanHomeInfoResult()
		{
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00002CD3 File Offset: 0x00000ED3
		public GetClanHomeInfoResult(ClanHomeInfo clanHomeInfo)
		{
			this.ClanHomeInfo = clanHomeInfo;
		}
	}
}
