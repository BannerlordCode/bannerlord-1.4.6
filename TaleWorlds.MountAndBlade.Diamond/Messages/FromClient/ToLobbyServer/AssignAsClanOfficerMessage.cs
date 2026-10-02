using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000076 RID: 118
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class AssignAsClanOfficerMessage : Message
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600024E RID: 590 RVA: 0x000039D1 File Offset: 0x00001BD1
		// (set) Token: 0x0600024F RID: 591 RVA: 0x000039D9 File Offset: 0x00001BD9
		[JsonProperty]
		public PlayerId AssignedPlayerId { get; private set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000250 RID: 592 RVA: 0x000039E2 File Offset: 0x00001BE2
		// (set) Token: 0x06000251 RID: 593 RVA: 0x000039EA File Offset: 0x00001BEA
		[JsonProperty]
		public bool DontUseNameForUnknownPlayer { get; private set; }

		// Token: 0x06000252 RID: 594 RVA: 0x000039F3 File Offset: 0x00001BF3
		public AssignAsClanOfficerMessage()
		{
		}

		// Token: 0x06000253 RID: 595 RVA: 0x000039FB File Offset: 0x00001BFB
		public AssignAsClanOfficerMessage(PlayerId assignedPlayerId, bool dontUseNameForUnknownPlayer)
		{
			this.AssignedPlayerId = assignedPlayerId;
			this.DontUseNameForUnknownPlayer = dontUseNameForUnknownPlayer;
		}
	}
}
