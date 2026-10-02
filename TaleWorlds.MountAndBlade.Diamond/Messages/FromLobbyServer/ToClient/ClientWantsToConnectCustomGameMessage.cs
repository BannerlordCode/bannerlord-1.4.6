using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000025 RID: 37
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class ClientWantsToConnectCustomGameMessage : Message
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000CE RID: 206 RVA: 0x000029F4 File Offset: 0x00000BF4
		// (set) Token: 0x060000CF RID: 207 RVA: 0x000029FC File Offset: 0x00000BFC
		[JsonProperty]
		public PlayerJoinGameData[] PlayerJoinGameData { get; private set; }

		// Token: 0x060000D0 RID: 208 RVA: 0x00002A05 File Offset: 0x00000C05
		public ClientWantsToConnectCustomGameMessage()
		{
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00002A0D File Offset: 0x00000C0D
		public ClientWantsToConnectCustomGameMessage(PlayerJoinGameData[] playerJoinGameData)
		{
			this.PlayerJoinGameData = playerJoinGameData;
		}
	}
}
