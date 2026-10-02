using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000036 RID: 54
	[Serializable]
	public class GetClanLeaderboardResult : FunctionResult
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00002CE2 File Offset: 0x00000EE2
		// (set) Token: 0x06000118 RID: 280 RVA: 0x00002CEA File Offset: 0x00000EEA
		[JsonProperty]
		public ClanLeaderboardInfo ClanLeaderboardInfo { get; private set; }

		// Token: 0x06000119 RID: 281 RVA: 0x00002CF3 File Offset: 0x00000EF3
		public GetClanLeaderboardResult()
		{
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00002CFB File Offset: 0x00000EFB
		public GetClanLeaderboardResult(ClanLeaderboardInfo info)
		{
			this.ClanLeaderboardInfo = info;
		}
	}
}
