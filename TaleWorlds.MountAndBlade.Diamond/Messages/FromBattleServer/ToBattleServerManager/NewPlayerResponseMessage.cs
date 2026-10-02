using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromBattleServer.ToBattleServerManager
{
	// Token: 0x020000D7 RID: 215
	[MessageDescription("BattleServer", "BattleServerManager", false)]
	[Serializable]
	public class NewPlayerResponseMessage : Message
	{
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00004B76 File Offset: 0x00002D76
		// (set) Token: 0x060003F1 RID: 1009 RVA: 0x00004B7E File Offset: 0x00002D7E
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00004B87 File Offset: 0x00002D87
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x00004B8F File Offset: 0x00002D8F
		[JsonProperty]
		public PlayerBattleServerInformation PlayerBattleInformation { get; private set; }

		// Token: 0x060003F4 RID: 1012 RVA: 0x00004B98 File Offset: 0x00002D98
		public NewPlayerResponseMessage()
		{
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00004BA0 File Offset: 0x00002DA0
		public NewPlayerResponseMessage(PlayerId playerId, PlayerBattleServerInformation playerBattleInformation)
		{
			this.PlayerId = playerId;
			this.PlayerBattleInformation = playerBattleInformation;
		}
	}
}
