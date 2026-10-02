using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200004C RID: 76
	[MessageDescription("LobbyServer", "Client", true)]
	[Serializable]
	public class JoinPremadeGameRequestMessage : Message
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600018D RID: 397 RVA: 0x000031BF File Offset: 0x000013BF
		// (set) Token: 0x0600018E RID: 398 RVA: 0x000031C7 File Offset: 0x000013C7
		[JsonProperty]
		public Guid ChallengerPartyId { get; private set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600018F RID: 399 RVA: 0x000031D0 File Offset: 0x000013D0
		// (set) Token: 0x06000190 RID: 400 RVA: 0x000031D8 File Offset: 0x000013D8
		[JsonProperty]
		public string ClanName { get; private set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000191 RID: 401 RVA: 0x000031E1 File Offset: 0x000013E1
		// (set) Token: 0x06000192 RID: 402 RVA: 0x000031E9 File Offset: 0x000013E9
		[JsonProperty]
		public string Sigil { get; private set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000193 RID: 403 RVA: 0x000031F2 File Offset: 0x000013F2
		// (set) Token: 0x06000194 RID: 404 RVA: 0x000031FA File Offset: 0x000013FA
		[JsonProperty]
		public PlayerId[] ChallengerPlayers { get; private set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00003203 File Offset: 0x00001403
		// (set) Token: 0x06000196 RID: 406 RVA: 0x0000320B File Offset: 0x0000140B
		[JsonProperty]
		public PlayerId ChallengerPartyLeaderId { get; private set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00003214 File Offset: 0x00001414
		// (set) Token: 0x06000198 RID: 408 RVA: 0x0000321C File Offset: 0x0000141C
		[JsonProperty]
		public PremadeGameType PremadeGameType { get; private set; }

		// Token: 0x06000199 RID: 409 RVA: 0x00003225 File Offset: 0x00001425
		public JoinPremadeGameRequestMessage()
		{
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000322D File Offset: 0x0000142D
		public JoinPremadeGameRequestMessage(Guid challengerPartyId, string clanName, string sigil, PlayerId[] challengerPlayers, PlayerId challengerPartyLeaderId, PremadeGameType premadeGameType)
		{
			this.ChallengerPartyId = challengerPartyId;
			this.ClanName = clanName;
			this.Sigil = sigil;
			this.ChallengerPlayers = challengerPlayers;
			this.ChallengerPartyLeaderId = challengerPartyLeaderId;
			this.PremadeGameType = premadeGameType;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00003262 File Offset: 0x00001462
		public static JoinPremadeGameRequestMessage CreateClanGameRequest(Guid challengerPartyId, string clanName, string sigil, PlayerId[] challengerPlayers)
		{
			return new JoinPremadeGameRequestMessage(challengerPartyId, clanName, sigil, challengerPlayers, PlayerId.Empty, PremadeGameType.Clan);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00003273 File Offset: 0x00001473
		public static JoinPremadeGameRequestMessage CreatePracticeGameRequest(Guid challengerPartyId, PlayerId leaderId, PlayerId[] challengerPlayers)
		{
			return new JoinPremadeGameRequestMessage(challengerPartyId, null, null, challengerPlayers, leaderId, PremadeGameType.Practice);
		}
	}
}
