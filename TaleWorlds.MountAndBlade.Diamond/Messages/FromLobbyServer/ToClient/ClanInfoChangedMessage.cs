using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000022 RID: 34
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class ClanInfoChangedMessage : Message
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00002964 File Offset: 0x00000B64
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x0000296C File Offset: 0x00000B6C
		[JsonProperty]
		public ClanHomeInfo ClanHomeInfo { get; private set; }

		// Token: 0x060000C2 RID: 194 RVA: 0x00002975 File Offset: 0x00000B75
		public ClanInfoChangedMessage()
		{
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000297D File Offset: 0x00000B7D
		public ClanInfoChangedMessage(ClanHomeInfo clanHomeInfo)
		{
			this.ClanHomeInfo = clanHomeInfo;
		}
	}
}
