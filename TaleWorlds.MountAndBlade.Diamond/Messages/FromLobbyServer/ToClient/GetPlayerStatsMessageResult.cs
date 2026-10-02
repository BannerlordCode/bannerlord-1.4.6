using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200003F RID: 63
	[Serializable]
	public class GetPlayerStatsMessageResult : FunctionResult
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00002E4A File Offset: 0x0000104A
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00002E52 File Offset: 0x00001052
		[JsonProperty]
		public PlayerStatsBase[] PlayerStats { get; private set; }

		// Token: 0x0600013D RID: 317 RVA: 0x00002E5B File Offset: 0x0000105B
		public GetPlayerStatsMessageResult()
		{
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00002E63 File Offset: 0x00001063
		public GetPlayerStatsMessageResult(PlayerStatsBase[] playerStats)
		{
			this.PlayerStats = playerStats;
		}
	}
}
