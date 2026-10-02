using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000C9 RID: 201
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class WhisperMessage : Message
	{
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x000047AE File Offset: 0x000029AE
		// (set) Token: 0x060003A2 RID: 930 RVA: 0x000047B6 File Offset: 0x000029B6
		[JsonProperty]
		public string TargetPlayerName { get; private set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x000047BF File Offset: 0x000029BF
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x000047C7 File Offset: 0x000029C7
		[JsonProperty]
		public string Message { get; private set; }

		// Token: 0x060003A5 RID: 933 RVA: 0x000047D0 File Offset: 0x000029D0
		public WhisperMessage()
		{
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x000047D8 File Offset: 0x000029D8
		public WhisperMessage(string targetPlayerName, string message)
		{
			this.TargetPlayerName = targetPlayerName;
			this.Message = message;
		}
	}
}
