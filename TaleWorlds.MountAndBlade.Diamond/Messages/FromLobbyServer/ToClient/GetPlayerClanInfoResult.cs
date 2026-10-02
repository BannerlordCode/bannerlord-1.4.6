using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200003C RID: 60
	[Serializable]
	public class GetPlayerClanInfoResult : FunctionResult
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00002DD2 File Offset: 0x00000FD2
		// (set) Token: 0x06000130 RID: 304 RVA: 0x00002DDA File Offset: 0x00000FDA
		[JsonProperty]
		public ClanInfo ClanInfo { get; private set; }

		// Token: 0x06000131 RID: 305 RVA: 0x00002DE3 File Offset: 0x00000FE3
		public GetPlayerClanInfoResult()
		{
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00002DEB File Offset: 0x00000FEB
		public GetPlayerClanInfoResult(ClanInfo clanInfo)
		{
			this.ClanInfo = clanInfo;
		}
	}
}
