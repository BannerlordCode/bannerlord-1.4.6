using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromCustomBattleServerManager.ToCustomBattleServer
{
	// Token: 0x02000011 RID: 17
	[MessageDescription("CustomBattleServerManager", "CustomBattleServer", true)]
	[Serializable]
	public class RegisterCustomGameMessageResponseMessage : FunctionResult
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002660 File Offset: 0x00000860
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00002668 File Offset: 0x00000868
		[JsonProperty]
		public bool ShouldReportActivities { get; private set; }

		// Token: 0x06000078 RID: 120 RVA: 0x00002671 File Offset: 0x00000871
		public RegisterCustomGameMessageResponseMessage()
		{
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002679 File Offset: 0x00000879
		public RegisterCustomGameMessageResponseMessage(bool shouldReportActivities)
		{
			this.ShouldReportActivities = shouldReportActivities;
		}
	}
}
