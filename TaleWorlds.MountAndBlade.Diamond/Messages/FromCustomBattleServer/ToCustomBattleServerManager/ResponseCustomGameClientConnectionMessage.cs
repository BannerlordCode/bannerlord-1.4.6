using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromCustomBattleServer.ToCustomBattleServerManager
{
	// Token: 0x0200000A RID: 10
	[MessageDescription("CustomBattleServer", "CustomBattleServerManager", false)]
	[Serializable]
	public class ResponseCustomGameClientConnectionMessage : Message
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002508 File Offset: 0x00000708
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002510 File Offset: 0x00000710
		[JsonProperty]
		public PlayerJoinGameResponseDataFromHost[] PlayerJoinData { get; private set; }

		// Token: 0x06000057 RID: 87 RVA: 0x00002519 File Offset: 0x00000719
		public ResponseCustomGameClientConnectionMessage()
		{
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002521 File Offset: 0x00000721
		public ResponseCustomGameClientConnectionMessage(PlayerJoinGameResponseDataFromHost[] playerJoinData)
		{
			this.PlayerJoinData = playerJoinData;
		}
	}
}
