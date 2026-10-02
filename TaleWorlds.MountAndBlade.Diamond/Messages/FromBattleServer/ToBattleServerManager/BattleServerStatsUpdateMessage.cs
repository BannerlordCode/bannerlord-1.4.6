using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromBattleServer.ToBattleServerManager
{
	// Token: 0x020000D3 RID: 211
	[MessageDescription("BattleServer", "BattleServerManager", false)]
	[Serializable]
	public class BattleServerStatsUpdateMessage : Message
	{
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060003DC RID: 988 RVA: 0x00004A98 File Offset: 0x00002C98
		// (set) Token: 0x060003DD RID: 989 RVA: 0x00004AA0 File Offset: 0x00002CA0
		[JsonProperty]
		public BattleResult BattleResult { get; private set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060003DE RID: 990 RVA: 0x00004AA9 File Offset: 0x00002CA9
		// (set) Token: 0x060003DF RID: 991 RVA: 0x00004AB1 File Offset: 0x00002CB1
		[JsonProperty]
		public Dictionary<int, int> TeamScores { get; private set; }

		// Token: 0x060003E0 RID: 992 RVA: 0x00004ABA File Offset: 0x00002CBA
		public BattleServerStatsUpdateMessage()
		{
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00004AC2 File Offset: 0x00002CC2
		public BattleServerStatsUpdateMessage(BattleResult battleResult, Dictionary<int, int> teamScores)
		{
			this.BattleResult = battleResult;
			this.TeamScores = teamScores;
		}
	}
}
