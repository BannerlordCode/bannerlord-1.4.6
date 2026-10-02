using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000046 RID: 70
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class InvitationToClanMessage : Message
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00002FF4 File Offset: 0x000011F4
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00002FFC File Offset: 0x000011FC
		[JsonProperty]
		public PlayerId InviterId { get; private set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00003005 File Offset: 0x00001205
		// (set) Token: 0x06000166 RID: 358 RVA: 0x0000300D File Offset: 0x0000120D
		[JsonProperty]
		public string ClanName { get; private set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00003016 File Offset: 0x00001216
		// (set) Token: 0x06000168 RID: 360 RVA: 0x0000301E File Offset: 0x0000121E
		[JsonProperty]
		public string ClanTag { get; private set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00003027 File Offset: 0x00001227
		// (set) Token: 0x0600016A RID: 362 RVA: 0x0000302F File Offset: 0x0000122F
		[JsonProperty]
		public int ClanPlayerCount { get; private set; }

		// Token: 0x0600016B RID: 363 RVA: 0x00003038 File Offset: 0x00001238
		public InvitationToClanMessage()
		{
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00003040 File Offset: 0x00001240
		public InvitationToClanMessage(PlayerId inviterId, string clanName, string clanTag, int clanPlayerCount)
		{
			this.InviterId = inviterId;
			this.ClanName = clanName;
			this.ClanTag = clanTag;
			this.ClanPlayerCount = clanPlayerCount;
		}
	}
}
