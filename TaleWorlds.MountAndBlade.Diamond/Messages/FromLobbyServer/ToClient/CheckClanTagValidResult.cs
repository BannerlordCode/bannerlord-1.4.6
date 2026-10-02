using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200001C RID: 28
	public class CheckClanTagValidResult : FunctionResult
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00002873 File Offset: 0x00000A73
		// (set) Token: 0x060000AA RID: 170 RVA: 0x0000287B File Offset: 0x00000A7B
		[JsonProperty]
		public bool TagExists { get; private set; }

		// Token: 0x060000AB RID: 171 RVA: 0x00002884 File Offset: 0x00000A84
		public CheckClanTagValidResult()
		{
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000288C File Offset: 0x00000A8C
		public CheckClanTagValidResult(bool tagExists)
		{
			this.TagExists = tagExists;
		}
	}
}
