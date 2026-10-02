using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond.Ranked;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200003E RID: 62
	[Serializable]
	public class GetPlayerGameTypeRankInfoMessageResult : FunctionResult
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00002E22 File Offset: 0x00001022
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00002E2A File Offset: 0x0000102A
		[JsonProperty]
		public GameTypeRankInfo[] GameTypeRankInfo { get; private set; }

		// Token: 0x06000139 RID: 313 RVA: 0x00002E33 File Offset: 0x00001033
		public GetPlayerGameTypeRankInfoMessageResult()
		{
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00002E3B File Offset: 0x0000103B
		public GetPlayerGameTypeRankInfoMessageResult(GameTypeRankInfo[] gameTypeRankInfo)
		{
			this.GameTypeRankInfo = gameTypeRankInfo;
		}
	}
}
