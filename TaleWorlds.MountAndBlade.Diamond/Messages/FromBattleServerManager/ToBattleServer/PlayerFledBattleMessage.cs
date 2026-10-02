using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromBattleServerManager.ToBattleServer
{
	// Token: 0x020000E2 RID: 226
	[MessageDescription("BattleServerManager", "BattleServer", true)]
	[Serializable]
	public class PlayerFledBattleMessage : Message
	{
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x00004D88 File Offset: 0x00002F88
		// (set) Token: 0x06000423 RID: 1059 RVA: 0x00004D90 File Offset: 0x00002F90
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x06000424 RID: 1060 RVA: 0x00004D99 File Offset: 0x00002F99
		public PlayerFledBattleMessage()
		{
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00004DA1 File Offset: 0x00002FA1
		public PlayerFledBattleMessage(PlayerId playerId)
		{
			this.PlayerId = playerId;
		}
	}
}
