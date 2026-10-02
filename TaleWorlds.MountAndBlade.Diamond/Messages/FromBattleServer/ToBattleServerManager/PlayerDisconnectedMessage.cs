using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromBattleServer.ToBattleServerManager
{
	// Token: 0x020000D8 RID: 216
	[MessageDescription("BattleServer", "BattleServerManager", false)]
	[Serializable]
	public class PlayerDisconnectedMessage : Message
	{
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00004BB6 File Offset: 0x00002DB6
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x00004BBE File Offset: 0x00002DBE
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00004BC7 File Offset: 0x00002DC7
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x00004BCF File Offset: 0x00002DCF
		[JsonProperty]
		public DisconnectType Type { get; private set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x00004BD8 File Offset: 0x00002DD8
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x00004BE0 File Offset: 0x00002DE0
		[JsonProperty]
		public bool IsAllowedLeave { get; private set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x00004BE9 File Offset: 0x00002DE9
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x00004BF1 File Offset: 0x00002DF1
		[JsonProperty]
		public BattleResult BattleResult { get; private set; }

		// Token: 0x060003FE RID: 1022 RVA: 0x00004BFA File Offset: 0x00002DFA
		public PlayerDisconnectedMessage()
		{
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00004C02 File Offset: 0x00002E02
		public PlayerDisconnectedMessage(PlayerId playerId, DisconnectType type, bool isAllowedLeave, BattleResult battleResult)
		{
			this.PlayerId = playerId;
			this.Type = type;
			this.IsAllowedLeave = isAllowedLeave;
			this.BattleResult = battleResult;
		}
	}
}
