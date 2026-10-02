using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000024 RID: 36
	[Serializable]
	public class ClientQuitFromCustomGameMessage : Message
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000CA RID: 202 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060000CB RID: 203 RVA: 0x000029D4 File Offset: 0x00000BD4
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x060000CC RID: 204 RVA: 0x000029DD File Offset: 0x00000BDD
		public ClientQuitFromCustomGameMessage()
		{
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000029E5 File Offset: 0x00000BE5
		public ClientQuitFromCustomGameMessage(PlayerId playerId)
		{
			this.PlayerId = playerId;
		}
	}
}
