using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000014 RID: 20
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class AdminMessage : Message
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000026D0 File Offset: 0x000008D0
		// (set) Token: 0x06000082 RID: 130 RVA: 0x000026D8 File Offset: 0x000008D8
		[JsonProperty]
		public string Message { get; private set; }

		// Token: 0x06000083 RID: 131 RVA: 0x000026E1 File Offset: 0x000008E1
		public AdminMessage()
		{
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000026E9 File Offset: 0x000008E9
		public AdminMessage(string message)
		{
			this.Message = message;
		}
	}
}
