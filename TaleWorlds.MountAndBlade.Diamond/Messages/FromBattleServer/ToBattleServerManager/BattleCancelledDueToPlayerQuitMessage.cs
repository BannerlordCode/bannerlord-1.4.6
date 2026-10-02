using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromBattleServer.ToBattleServerManager
{
	// Token: 0x020000CD RID: 205
	[MessageDescription("BattleServer", "BattleServerManager", true)]
	[Serializable]
	public class BattleCancelledDueToPlayerQuitMessage : Message
	{
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x00004846 File Offset: 0x00002A46
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x0000484E File Offset: 0x00002A4E
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x00004857 File Offset: 0x00002A57
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x0000485F File Offset: 0x00002A5F
		[JsonProperty]
		public string GameType { get; private set; }

		// Token: 0x060003B4 RID: 948 RVA: 0x00004868 File Offset: 0x00002A68
		public BattleCancelledDueToPlayerQuitMessage()
		{
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00004870 File Offset: 0x00002A70
		public BattleCancelledDueToPlayerQuitMessage(PlayerId playerId, string gameType)
		{
			this.PlayerId = playerId;
			this.GameType = gameType;
		}
	}
}
