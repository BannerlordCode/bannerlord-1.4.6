using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000BC RID: 188
	[MessageDescription("Client", "LobbyServer", true)]
	[Serializable]
	public class ReportPlayerMessage : Message
	{
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600035A RID: 858 RVA: 0x000044BC File Offset: 0x000026BC
		// (set) Token: 0x0600035B RID: 859 RVA: 0x000044C4 File Offset: 0x000026C4
		[JsonProperty]
		public Guid GameId { get; private set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600035C RID: 860 RVA: 0x000044CD File Offset: 0x000026CD
		// (set) Token: 0x0600035D RID: 861 RVA: 0x000044D5 File Offset: 0x000026D5
		[JsonProperty]
		public PlayerId ReportedPlayerId { get; private set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600035E RID: 862 RVA: 0x000044DE File Offset: 0x000026DE
		// (set) Token: 0x0600035F RID: 863 RVA: 0x000044E6 File Offset: 0x000026E6
		[JsonProperty]
		public string ReportedPlayerName { get; private set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000360 RID: 864 RVA: 0x000044EF File Offset: 0x000026EF
		// (set) Token: 0x06000361 RID: 865 RVA: 0x000044F7 File Offset: 0x000026F7
		[JsonProperty]
		public PlayerReportType Type { get; private set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000362 RID: 866 RVA: 0x00004500 File Offset: 0x00002700
		// (set) Token: 0x06000363 RID: 867 RVA: 0x00004508 File Offset: 0x00002708
		[JsonProperty]
		public string Message { get; private set; }

		// Token: 0x06000364 RID: 868 RVA: 0x00004511 File Offset: 0x00002711
		public ReportPlayerMessage()
		{
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00004519 File Offset: 0x00002719
		public ReportPlayerMessage(Guid gameId, PlayerId reportedPlayerId, string reportedPlayerName, PlayerReportType type, string message)
		{
			this.GameId = gameId;
			this.ReportedPlayerId = reportedPlayerId;
			this.ReportedPlayerName = reportedPlayerName;
			this.Type = type;
			this.Message = message;
		}
	}
}
