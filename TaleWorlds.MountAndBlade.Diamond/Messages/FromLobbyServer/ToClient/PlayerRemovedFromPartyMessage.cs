using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200005B RID: 91
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class PlayerRemovedFromPartyMessage : Message
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00003458 File Offset: 0x00001658
		// (set) Token: 0x060001CD RID: 461 RVA: 0x00003460 File Offset: 0x00001660
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00003469 File Offset: 0x00001669
		// (set) Token: 0x060001CF RID: 463 RVA: 0x00003471 File Offset: 0x00001671
		[JsonProperty]
		public PartyRemoveReason Reason { get; private set; }

		// Token: 0x060001D0 RID: 464 RVA: 0x0000347A File Offset: 0x0000167A
		public PlayerRemovedFromPartyMessage()
		{
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00003482 File Offset: 0x00001682
		public PlayerRemovedFromPartyMessage(PlayerId playerId, PartyRemoveReason reason)
		{
			this.PlayerId = playerId;
			this.Reason = reason;
		}
	}
}
