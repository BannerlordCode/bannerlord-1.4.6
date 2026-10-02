using System;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000061 RID: 97
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class RejoinBattleRequestAnswerMessage : Message
	{
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x000035FB File Offset: 0x000017FB
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x00003603 File Offset: 0x00001803
		public bool IsRejoinAccepted { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000360C File Offset: 0x0000180C
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x00003614 File Offset: 0x00001814
		public bool IsSuccessful { get; set; }

		// Token: 0x060001F4 RID: 500 RVA: 0x0000361D File Offset: 0x0000181D
		public RejoinBattleRequestAnswerMessage()
		{
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00003625 File Offset: 0x00001825
		public RejoinBattleRequestAnswerMessage(bool isRejoinAccepted, bool isSuccessful)
		{
			this.IsRejoinAccepted = isRejoinAccepted;
			this.IsSuccessful = isSuccessful;
		}
	}
}
