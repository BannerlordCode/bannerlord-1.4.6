using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000069 RID: 105
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class UpdatePlayerDataMessage : Message
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00003799 File Offset: 0x00001999
		// (set) Token: 0x06000218 RID: 536 RVA: 0x000037A1 File Offset: 0x000019A1
		[JsonProperty]
		public PlayerData PlayerData { get; private set; }

		// Token: 0x06000219 RID: 537 RVA: 0x000037AA File Offset: 0x000019AA
		public UpdatePlayerDataMessage()
		{
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000037B2 File Offset: 0x000019B2
		public UpdatePlayerDataMessage(PlayerData playerData)
		{
			this.PlayerData = playerData;
		}
	}
}
