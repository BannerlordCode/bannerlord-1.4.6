using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromCustomBattleServerManager.ToCustomBattleServer
{
	// Token: 0x02000010 RID: 16
	[MessageDescription("CustomBattleServerManager", "CustomBattleServer", true)]
	[Serializable]
	public class PlayerDisconnectedFromLobbyMessage : Message
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002638 File Offset: 0x00000838
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00002640 File Offset: 0x00000840
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x06000074 RID: 116 RVA: 0x00002649 File Offset: 0x00000849
		public PlayerDisconnectedFromLobbyMessage()
		{
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002651 File Offset: 0x00000851
		public PlayerDisconnectedFromLobbyMessage(PlayerId playerId)
		{
			this.PlayerId = playerId;
		}
	}
}
