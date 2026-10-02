using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000085 RID: 133
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class CreateClanMessage : Message
	{
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000280 RID: 640 RVA: 0x00003BC1 File Offset: 0x00001DC1
		// (set) Token: 0x06000281 RID: 641 RVA: 0x00003BC9 File Offset: 0x00001DC9
		[JsonProperty]
		public string ClanName { get; private set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000282 RID: 642 RVA: 0x00003BD2 File Offset: 0x00001DD2
		// (set) Token: 0x06000283 RID: 643 RVA: 0x00003BDA File Offset: 0x00001DDA
		[JsonProperty]
		public string ClanTag { get; private set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000284 RID: 644 RVA: 0x00003BE3 File Offset: 0x00001DE3
		// (set) Token: 0x06000285 RID: 645 RVA: 0x00003BEB File Offset: 0x00001DEB
		[JsonProperty]
		public string ClanFaction { get; private set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000286 RID: 646 RVA: 0x00003BF4 File Offset: 0x00001DF4
		// (set) Token: 0x06000287 RID: 647 RVA: 0x00003BFC File Offset: 0x00001DFC
		[JsonProperty]
		public string ClanSigil { get; private set; }

		// Token: 0x06000288 RID: 648 RVA: 0x00003C05 File Offset: 0x00001E05
		public CreateClanMessage()
		{
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00003C0D File Offset: 0x00001E0D
		public CreateClanMessage(string clanName, string clanTag, string clanFaction, string clanSigil)
		{
			this.ClanName = clanName;
			this.ClanTag = clanTag;
			this.ClanFaction = clanFaction;
			this.ClanSigil = clanSigil;
		}
	}
}
