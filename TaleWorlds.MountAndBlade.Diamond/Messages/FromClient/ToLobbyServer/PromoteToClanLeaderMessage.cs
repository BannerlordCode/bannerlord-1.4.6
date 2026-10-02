using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000B3 RID: 179
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class PromoteToClanLeaderMessage : Message
	{
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600032A RID: 810 RVA: 0x000042A9 File Offset: 0x000024A9
		// (set) Token: 0x0600032B RID: 811 RVA: 0x000042B1 File Offset: 0x000024B1
		[JsonProperty]
		public PlayerId PromotedPlayerId { get; private set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600032C RID: 812 RVA: 0x000042BA File Offset: 0x000024BA
		// (set) Token: 0x0600032D RID: 813 RVA: 0x000042C2 File Offset: 0x000024C2
		[JsonProperty]
		public bool DontUseNameForUnknownPlayer { get; private set; }

		// Token: 0x0600032E RID: 814 RVA: 0x000042CB File Offset: 0x000024CB
		public PromoteToClanLeaderMessage()
		{
		}

		// Token: 0x0600032F RID: 815 RVA: 0x000042D3 File Offset: 0x000024D3
		public PromoteToClanLeaderMessage(PlayerId promotedPlayerId, bool dontUseNameForUnknownPlayer)
		{
			this.PromotedPlayerId = promotedPlayerId;
			this.DontUseNameForUnknownPlayer = dontUseNameForUnknownPlayer;
		}
	}
}
