using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000C1 RID: 193
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class ResponseCustomGameClientConnectionMessage : Message
	{
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600037B RID: 891 RVA: 0x00004626 File Offset: 0x00002826
		// (set) Token: 0x0600037C RID: 892 RVA: 0x0000462E File Offset: 0x0000282E
		[JsonProperty]
		public PlayerJoinGameResponseDataFromHost[] PlayerJoinData { get; private set; }

		// Token: 0x0600037D RID: 893 RVA: 0x00004637 File Offset: 0x00002837
		public ResponseCustomGameClientConnectionMessage()
		{
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000463F File Offset: 0x0000283F
		public ResponseCustomGameClientConnectionMessage(PlayerJoinGameResponseDataFromHost[] playerJoinData)
		{
			this.PlayerJoinData = playerJoinData;
		}
	}
}
