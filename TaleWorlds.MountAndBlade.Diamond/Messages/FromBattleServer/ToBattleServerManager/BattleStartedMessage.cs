using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromBattleServer.ToBattleServerManager
{
	// Token: 0x020000D4 RID: 212
	[MessageDescription("BattleServer", "BattleServerManager", true)]
	[Serializable]
	public class BattleStartedMessage : Message
	{
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x00004AD8 File Offset: 0x00002CD8
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x00004AE0 File Offset: 0x00002CE0
		[JsonProperty]
		public bool Report { get; private set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x00004AE9 File Offset: 0x00002CE9
		// (set) Token: 0x060003E5 RID: 997 RVA: 0x00004AF1 File Offset: 0x00002CF1
		[JsonProperty]
		public Dictionary<string, int> PlayerTeams { get; private set; }

		// Token: 0x060003E6 RID: 998 RVA: 0x00004AFA File Offset: 0x00002CFA
		public BattleStartedMessage()
		{
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00004B02 File Offset: 0x00002D02
		public BattleStartedMessage(bool report)
		{
			this.Report = report;
			this.PlayerTeams = null;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00004B18 File Offset: 0x00002D18
		public BattleStartedMessage(bool report, Dictionary<string, int> playerTeams)
		{
			this.Report = report;
			this.PlayerTeams = playerTeams;
		}
	}
}
