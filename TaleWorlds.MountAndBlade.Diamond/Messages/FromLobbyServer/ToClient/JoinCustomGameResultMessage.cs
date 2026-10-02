using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200004A RID: 74
	[Serializable]
	public class JoinCustomGameResultMessage : Message
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600017B RID: 379 RVA: 0x000030F5 File Offset: 0x000012F5
		// (set) Token: 0x0600017C RID: 380 RVA: 0x000030FD File Offset: 0x000012FD
		[JsonProperty]
		public JoinGameData JoinGameData { get; private set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00003106 File Offset: 0x00001306
		// (set) Token: 0x0600017E RID: 382 RVA: 0x0000310E File Offset: 0x0000130E
		[JsonProperty]
		public bool Success { get; private set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00003117 File Offset: 0x00001317
		// (set) Token: 0x06000180 RID: 384 RVA: 0x0000311F File Offset: 0x0000131F
		[JsonProperty]
		public CustomGameJoinResponse Response { get; private set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00003128 File Offset: 0x00001328
		// (set) Token: 0x06000182 RID: 386 RVA: 0x00003130 File Offset: 0x00001330
		[JsonProperty]
		public string MatchId { get; private set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00003139 File Offset: 0x00001339
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00003141 File Offset: 0x00001341
		[JsonProperty]
		public bool IsAdmin { get; private set; }

		// Token: 0x06000185 RID: 389 RVA: 0x0000314A File Offset: 0x0000134A
		public JoinCustomGameResultMessage()
		{
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00003152 File Offset: 0x00001352
		private JoinCustomGameResultMessage(JoinGameData joinGameData, bool success, CustomGameJoinResponse response, string matchId, bool isAdmin)
		{
			this.JoinGameData = joinGameData;
			this.Success = success;
			this.Response = response;
			this.MatchId = matchId;
			this.IsAdmin = isAdmin;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000317F File Offset: 0x0000137F
		public static JoinCustomGameResultMessage CreateSuccess(JoinGameData joinGameData, string matchId, bool isAdmin)
		{
			return new JoinCustomGameResultMessage(joinGameData, true, CustomGameJoinResponse.Success, matchId, isAdmin);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000318B File Offset: 0x0000138B
		public static JoinCustomGameResultMessage CreateFailed(CustomGameJoinResponse response)
		{
			return new JoinCustomGameResultMessage(null, false, response, null, false);
		}
	}
}
