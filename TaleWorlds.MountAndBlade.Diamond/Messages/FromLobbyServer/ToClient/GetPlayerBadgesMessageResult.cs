using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200003A RID: 58
	[Serializable]
	public class GetPlayerBadgesMessageResult : FunctionResult
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00002D82 File Offset: 0x00000F82
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00002D8A File Offset: 0x00000F8A
		[JsonProperty]
		public string[] Badges { get; private set; }

		// Token: 0x06000129 RID: 297 RVA: 0x00002D93 File Offset: 0x00000F93
		public GetPlayerBadgesMessageResult()
		{
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00002D9B File Offset: 0x00000F9B
		public GetPlayerBadgesMessageResult(string[] badges)
		{
			this.Badges = badges;
		}
	}
}
