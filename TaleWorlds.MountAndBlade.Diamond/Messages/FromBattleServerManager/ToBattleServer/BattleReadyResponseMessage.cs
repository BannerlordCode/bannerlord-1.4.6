using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromBattleServerManager.ToBattleServer
{
	// Token: 0x020000DD RID: 221
	[MessageDescription("BattleServerManager", "BattleServer", true)]
	[Serializable]
	public class BattleReadyResponseMessage : FunctionResult
	{
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x00004C97 File Offset: 0x00002E97
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x00004C9F File Offset: 0x00002E9F
		[JsonProperty]
		public bool ShouldReportActivities { get; private set; }

		// Token: 0x0600040D RID: 1037 RVA: 0x00004CA8 File Offset: 0x00002EA8
		public BattleReadyResponseMessage()
		{
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00004CB0 File Offset: 0x00002EB0
		public BattleReadyResponseMessage(bool shouldReportActivities)
		{
			this.ShouldReportActivities = shouldReportActivities;
		}
	}
}
