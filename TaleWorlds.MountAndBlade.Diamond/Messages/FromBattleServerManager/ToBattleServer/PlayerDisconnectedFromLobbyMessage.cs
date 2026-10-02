using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromBattleServerManager.ToBattleServer
{
	// Token: 0x020000E1 RID: 225
	[MessageDescription("BattleServerManager", "BattleServer", true)]
	[Serializable]
	public class PlayerDisconnectedFromLobbyMessage : Message
	{
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x00004D60 File Offset: 0x00002F60
		// (set) Token: 0x0600041F RID: 1055 RVA: 0x00004D68 File Offset: 0x00002F68
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x06000420 RID: 1056 RVA: 0x00004D71 File Offset: 0x00002F71
		public PlayerDisconnectedFromLobbyMessage()
		{
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00004D79 File Offset: 0x00002F79
		public PlayerDisconnectedFromLobbyMessage(PlayerId playerId)
		{
			this.PlayerId = playerId;
		}
	}
}
