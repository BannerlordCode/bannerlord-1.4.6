using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200001E RID: 30
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class ClanCreationRequestAnsweredMessage : Message
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000028A3 File Offset: 0x00000AA3
		// (set) Token: 0x060000AF RID: 175 RVA: 0x000028AB File Offset: 0x00000AAB
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000028B4 File Offset: 0x00000AB4
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x000028BC File Offset: 0x00000ABC
		[JsonProperty]
		public ClanCreationAnswer ClanCreationAnswer { get; private set; }

		// Token: 0x060000B2 RID: 178 RVA: 0x000028C5 File Offset: 0x00000AC5
		public ClanCreationRequestAnsweredMessage()
		{
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000028CD File Offset: 0x00000ACD
		public ClanCreationRequestAnsweredMessage(PlayerId playerId, ClanCreationAnswer clanCreationAnswer)
		{
			this.PlayerId = playerId;
			this.ClanCreationAnswer = clanCreationAnswer;
		}
	}
}
