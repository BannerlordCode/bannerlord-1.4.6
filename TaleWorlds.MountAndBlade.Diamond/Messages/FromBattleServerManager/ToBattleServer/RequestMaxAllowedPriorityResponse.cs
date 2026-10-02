using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromBattleServerManager.ToBattleServer
{
	// Token: 0x020000E3 RID: 227
	[MessageDescription("BattleServerManager", "BattleServer", true)]
	[Serializable]
	public class RequestMaxAllowedPriorityResponse : FunctionResult
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x00004DB0 File Offset: 0x00002FB0
		// (set) Token: 0x06000427 RID: 1063 RVA: 0x00004DB8 File Offset: 0x00002FB8
		[JsonProperty]
		public sbyte Priority { get; private set; }

		// Token: 0x06000428 RID: 1064 RVA: 0x00004DC1 File Offset: 0x00002FC1
		public RequestMaxAllowedPriorityResponse()
		{
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00004DC9 File Offset: 0x00002FC9
		public RequestMaxAllowedPriorityResponse(sbyte priority)
		{
			this.Priority = priority;
		}
	}
}
