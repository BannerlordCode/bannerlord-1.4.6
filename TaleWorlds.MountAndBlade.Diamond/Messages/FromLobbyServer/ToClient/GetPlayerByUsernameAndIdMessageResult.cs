using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200003B RID: 59
	[Serializable]
	public class GetPlayerByUsernameAndIdMessageResult : FunctionResult
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00002DAA File Offset: 0x00000FAA
		// (set) Token: 0x0600012C RID: 300 RVA: 0x00002DB2 File Offset: 0x00000FB2
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x0600012D RID: 301 RVA: 0x00002DBB File Offset: 0x00000FBB
		public GetPlayerByUsernameAndIdMessageResult()
		{
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00002DC3 File Offset: 0x00000FC3
		public GetPlayerByUsernameAndIdMessageResult(PlayerId playerId)
		{
			this.PlayerId = playerId;
		}
	}
}
