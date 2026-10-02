using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000068 RID: 104
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class SystemMessage : Message
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00003754 File Offset: 0x00001954
		// (set) Token: 0x06000212 RID: 530 RVA: 0x0000375C File Offset: 0x0000195C
		[JsonProperty]
		public ServerInfoMessage Message { get; private set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00003765 File Offset: 0x00001965
		// (set) Token: 0x06000214 RID: 532 RVA: 0x0000376D File Offset: 0x0000196D
		[JsonProperty]
		public List<string> Parameters { get; private set; }

		// Token: 0x06000215 RID: 533 RVA: 0x00003776 File Offset: 0x00001976
		public SystemMessage()
		{
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000377E File Offset: 0x0000197E
		public SystemMessage(ServerInfoMessage message, params string[] arguments)
		{
			this.Message = message;
			this.Parameters = new List<string>(arguments);
		}
	}
}
