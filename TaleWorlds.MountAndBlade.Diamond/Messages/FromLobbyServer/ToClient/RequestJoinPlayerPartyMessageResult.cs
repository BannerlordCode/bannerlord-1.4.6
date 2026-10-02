using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000064 RID: 100
	[Serializable]
	public class RequestJoinPlayerPartyMessageResult : FunctionResult
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000201 RID: 513 RVA: 0x000036B4 File Offset: 0x000018B4
		// (set) Token: 0x06000202 RID: 514 RVA: 0x000036BC File Offset: 0x000018BC
		[JsonProperty]
		public bool Success { get; private set; }

		// Token: 0x06000203 RID: 515 RVA: 0x000036C5 File Offset: 0x000018C5
		public RequestJoinPlayerPartyMessageResult()
		{
		}

		// Token: 0x06000204 RID: 516 RVA: 0x000036CD File Offset: 0x000018CD
		public RequestJoinPlayerPartyMessageResult(bool success)
		{
			this.Success = success;
		}
	}
}
