using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000040 RID: 64
	[Serializable]
	public class GetPremadeGameListResult : FunctionResult
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00002E72 File Offset: 0x00001072
		// (set) Token: 0x06000140 RID: 320 RVA: 0x00002E7A File Offset: 0x0000107A
		[JsonProperty]
		public PremadeGameList GameList { get; private set; }

		// Token: 0x06000141 RID: 321 RVA: 0x00002E83 File Offset: 0x00001083
		public GetPremadeGameListResult()
		{
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00002E8B File Offset: 0x0000108B
		public GetPremadeGameListResult(PremadeGameList gameList)
		{
			this.GameList = gameList;
		}
	}
}
