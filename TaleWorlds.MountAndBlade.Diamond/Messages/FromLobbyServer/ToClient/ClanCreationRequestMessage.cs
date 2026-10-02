using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200001F RID: 31
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class ClanCreationRequestMessage : Message
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000028E3 File Offset: 0x00000AE3
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x000028EB File Offset: 0x00000AEB
		[JsonProperty]
		public string CreatorPlayerName { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000028F4 File Offset: 0x00000AF4
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x000028FC File Offset: 0x00000AFC
		[JsonProperty]
		public PlayerId CreatorPlayerId { get; private set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00002905 File Offset: 0x00000B05
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x0000290D File Offset: 0x00000B0D
		[JsonProperty]
		public string ClanName { get; private set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00002916 File Offset: 0x00000B16
		// (set) Token: 0x060000BB RID: 187 RVA: 0x0000291E File Offset: 0x00000B1E
		[JsonProperty]
		public string ClanTag { get; private set; }

		// Token: 0x060000BC RID: 188 RVA: 0x00002927 File Offset: 0x00000B27
		public ClanCreationRequestMessage()
		{
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000292F File Offset: 0x00000B2F
		public ClanCreationRequestMessage(PlayerId creatorPlayerId, string creatorPlayerName, string clanName, string clanTag)
		{
			this.CreatorPlayerId = creatorPlayerId;
			this.CreatorPlayerName = creatorPlayerName;
			this.ClanName = clanName;
			this.ClanTag = clanTag;
		}
	}
}
