using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromCustomBattleServerManager.ToCustomBattleServer
{
	// Token: 0x0200000E RID: 14
	[MessageDescription("CustomBattleServerManager", "CustomBattleServer", true)]
	[Serializable]
	public class ClientWantsToConnectCustomGameMessage : Message
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002608 File Offset: 0x00000808
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00002610 File Offset: 0x00000810
		[JsonProperty]
		public PlayerJoinGameData[] PlayerJoinGameData { get; private set; }

		// Token: 0x0600006F RID: 111 RVA: 0x00002619 File Offset: 0x00000819
		public ClientWantsToConnectCustomGameMessage()
		{
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002621 File Offset: 0x00000821
		public ClientWantsToConnectCustomGameMessage(PlayerJoinGameData[] playerJoinGameData)
		{
			this.PlayerJoinGameData = playerJoinGameData;
		}
	}
}
