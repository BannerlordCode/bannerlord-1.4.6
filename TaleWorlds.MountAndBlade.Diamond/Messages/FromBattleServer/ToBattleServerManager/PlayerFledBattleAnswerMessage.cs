using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromBattleServer.ToBattleServerManager
{
	// Token: 0x020000D9 RID: 217
	[MessageDescription("BattleServer", "BattleServerManager", false)]
	[Serializable]
	public class PlayerFledBattleAnswerMessage : Message
	{
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x00004C27 File Offset: 0x00002E27
		// (set) Token: 0x06000401 RID: 1025 RVA: 0x00004C2F File Offset: 0x00002E2F
		[JsonProperty]
		public BattleResult BattleResult { get; private set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x00004C38 File Offset: 0x00002E38
		// (set) Token: 0x06000403 RID: 1027 RVA: 0x00004C40 File Offset: 0x00002E40
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x00004C49 File Offset: 0x00002E49
		// (set) Token: 0x06000405 RID: 1029 RVA: 0x00004C51 File Offset: 0x00002E51
		[JsonProperty]
		public bool IsAllowedLeave { get; private set; }

		// Token: 0x06000406 RID: 1030 RVA: 0x00004C5A File Offset: 0x00002E5A
		public PlayerFledBattleAnswerMessage()
		{
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00004C62 File Offset: 0x00002E62
		public PlayerFledBattleAnswerMessage(PlayerId playerId, BattleResult battleResult, bool isAllowedLeave)
		{
			this.PlayerId = playerId;
			this.BattleResult = battleResult;
			this.IsAllowedLeave = isAllowedLeave;
		}
	}
}
