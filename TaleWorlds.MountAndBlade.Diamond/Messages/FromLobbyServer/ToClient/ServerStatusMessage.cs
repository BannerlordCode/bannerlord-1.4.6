using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000065 RID: 101
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class ServerStatusMessage : Message
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000205 RID: 517 RVA: 0x000036DC File Offset: 0x000018DC
		// (set) Token: 0x06000206 RID: 518 RVA: 0x000036E4 File Offset: 0x000018E4
		[JsonProperty]
		public ServerStatus ServerStatus { get; private set; }

		// Token: 0x06000207 RID: 519 RVA: 0x000036ED File Offset: 0x000018ED
		public ServerStatusMessage()
		{
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000036F5 File Offset: 0x000018F5
		public ServerStatusMessage(ServerStatus serverStatus)
		{
			this.ServerStatus = serverStatus;
		}
	}
}
