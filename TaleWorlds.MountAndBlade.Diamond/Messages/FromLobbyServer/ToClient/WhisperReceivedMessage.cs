using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200006C RID: 108
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class WhisperReceivedMessage : Message
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00003811 File Offset: 0x00001A11
		// (set) Token: 0x06000224 RID: 548 RVA: 0x00003819 File Offset: 0x00001A19
		[JsonProperty]
		public string FromPlayer { get; private set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00003822 File Offset: 0x00001A22
		// (set) Token: 0x06000226 RID: 550 RVA: 0x0000382A File Offset: 0x00001A2A
		[JsonProperty]
		public string ToPlayer { get; private set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00003833 File Offset: 0x00001A33
		// (set) Token: 0x06000228 RID: 552 RVA: 0x0000383B File Offset: 0x00001A3B
		[JsonProperty]
		public string Message { get; private set; }

		// Token: 0x06000229 RID: 553 RVA: 0x00003844 File Offset: 0x00001A44
		public WhisperReceivedMessage()
		{
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000384C File Offset: 0x00001A4C
		public WhisperReceivedMessage(string fromPlayer, string toPlayer, string message)
		{
			this.FromPlayer = fromPlayer;
			this.ToPlayer = toPlayer;
			this.Message = message;
		}
	}
}
