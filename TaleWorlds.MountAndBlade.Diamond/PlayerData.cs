using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade.Diamond.MultiplayerBadges;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000142 RID: 322
	[Serializable]
	public class PlayerData
	{
		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x0000CB6A File Offset: 0x0000AD6A
		// (set) Token: 0x060008A5 RID: 2213 RVA: 0x0000CB72 File Offset: 0x0000AD72
		public PlayerId PlayerId { get; set; }

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x0000CB7B File Offset: 0x0000AD7B
		// (set) Token: 0x060008A7 RID: 2215 RVA: 0x0000CB83 File Offset: 0x0000AD83
		public PlayerId OwnerPlayerId { get; set; }

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x0000CB8C File Offset: 0x0000AD8C
		// (set) Token: 0x060008A9 RID: 2217 RVA: 0x0000CB94 File Offset: 0x0000AD94
		public string Sigil { get; set; }

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x0000CB9D File Offset: 0x0000AD9D
		// (set) Token: 0x060008AB RID: 2219 RVA: 0x0000CBA5 File Offset: 0x0000ADA5
		public BodyProperties BodyProperties
		{
			get
			{
				return this._bodyProperties;
			}
			set
			{
				this.SetBodyProperties(value);
			}
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0000CBAE File Offset: 0x0000ADAE
		private void SetBodyProperties(BodyProperties bodyProperties)
		{
			this._bodyProperties = bodyProperties.ClampForMultiplayer();
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x0000CBBD File Offset: 0x0000ADBD
		[JsonIgnore]
		public int ShownBadgeIndex
		{
			get
			{
				Badge byId = BadgeManager.GetById(this.ShownBadgeId);
				if (byId == null)
				{
					return -1;
				}
				return byId.Index;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x0000CBD5 File Offset: 0x0000ADD5
		// (set) Token: 0x060008AF RID: 2223 RVA: 0x0000CBDD File Offset: 0x0000ADDD
		public PlayerStatsBase[] Stats { get; set; }

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x0000CBE6 File Offset: 0x0000ADE6
		// (set) Token: 0x060008B1 RID: 2225 RVA: 0x0000CBEE File Offset: 0x0000ADEE
		public int Race { get; set; }

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x0000CBF7 File Offset: 0x0000ADF7
		// (set) Token: 0x060008B3 RID: 2227 RVA: 0x0000CBFF File Offset: 0x0000ADFF
		public bool IsFemale { get; set; }

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x0000CC08 File Offset: 0x0000AE08
		[JsonIgnore]
		public int KillCount
		{
			get
			{
				int num = 0;
				if (this.Stats != null)
				{
					foreach (PlayerStatsBase playerStatsBase in this.Stats)
					{
						num += playerStatsBase.KillCount;
					}
				}
				return num;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x0000CC44 File Offset: 0x0000AE44
		[JsonIgnore]
		public int DeathCount
		{
			get
			{
				int num = 0;
				if (this.Stats != null)
				{
					foreach (PlayerStatsBase playerStatsBase in this.Stats)
					{
						num += playerStatsBase.DeathCount;
					}
				}
				return num;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x0000CC80 File Offset: 0x0000AE80
		[JsonIgnore]
		public int AssistCount
		{
			get
			{
				int num = 0;
				if (this.Stats != null)
				{
					foreach (PlayerStatsBase playerStatsBase in this.Stats)
					{
						num += playerStatsBase.AssistCount;
					}
				}
				return num;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x0000CCBC File Offset: 0x0000AEBC
		[JsonIgnore]
		public int WinCount
		{
			get
			{
				int num = 0;
				if (this.Stats != null)
				{
					foreach (PlayerStatsBase playerStatsBase in this.Stats)
					{
						num += playerStatsBase.WinCount;
					}
				}
				return num;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x0000CCF8 File Offset: 0x0000AEF8
		[JsonIgnore]
		public int LoseCount
		{
			get
			{
				int num = 0;
				if (this.Stats != null)
				{
					foreach (PlayerStatsBase playerStatsBase in this.Stats)
					{
						num += playerStatsBase.LoseCount;
					}
				}
				return num;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x0000CD32 File Offset: 0x0000AF32
		// (set) Token: 0x060008BA RID: 2234 RVA: 0x0000CD3A File Offset: 0x0000AF3A
		public int Experience { get; set; }

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x0000CD43 File Offset: 0x0000AF43
		// (set) Token: 0x060008BC RID: 2236 RVA: 0x0000CD4B File Offset: 0x0000AF4B
		public string LastPlayerName { get; set; }

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x0000CD54 File Offset: 0x0000AF54
		// (set) Token: 0x060008BE RID: 2238 RVA: 0x0000CD5C File Offset: 0x0000AF5C
		public string Username { get; set; }

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x0000CD65 File Offset: 0x0000AF65
		// (set) Token: 0x060008C0 RID: 2240 RVA: 0x0000CD6D File Offset: 0x0000AF6D
		public int UserId { get; set; }

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x0000CD76 File Offset: 0x0000AF76
		// (set) Token: 0x060008C2 RID: 2242 RVA: 0x0000CD7E File Offset: 0x0000AF7E
		public bool IsUsingClanSigil { get; set; }

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x0000CD87 File Offset: 0x0000AF87
		// (set) Token: 0x060008C4 RID: 2244 RVA: 0x0000CD8F File Offset: 0x0000AF8F
		public string LastRegion { get; set; }

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x0000CD98 File Offset: 0x0000AF98
		// (set) Token: 0x060008C6 RID: 2246 RVA: 0x0000CDA0 File Offset: 0x0000AFA0
		public string[] LastGameTypes { get; set; }

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x0000CDA9 File Offset: 0x0000AFA9
		// (set) Token: 0x060008C8 RID: 2248 RVA: 0x0000CDB1 File Offset: 0x0000AFB1
		public DateTime? LastLogin { get; set; }

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x0000CDBA File Offset: 0x0000AFBA
		// (set) Token: 0x060008CA RID: 2250 RVA: 0x0000CDC2 File Offset: 0x0000AFC2
		public int Playtime { get; set; }

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x0000CDCB File Offset: 0x0000AFCB
		// (set) Token: 0x060008CC RID: 2252 RVA: 0x0000CDD3 File Offset: 0x0000AFD3
		public string ShownBadgeId { get; set; }

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x0000CDDC File Offset: 0x0000AFDC
		// (set) Token: 0x060008CE RID: 2254 RVA: 0x0000CDE4 File Offset: 0x0000AFE4
		public int Gold { get; set; }

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x0000CDED File Offset: 0x0000AFED
		// (set) Token: 0x060008D0 RID: 2256 RVA: 0x0000CDF5 File Offset: 0x0000AFF5
		public bool IsMuted { get; set; }

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x0000CE00 File Offset: 0x0000B000
		[JsonIgnore]
		public int Level
		{
			get
			{
				return new PlayerDataExperience(this.Experience).Level;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x0000CE20 File Offset: 0x0000B020
		[JsonIgnore]
		public int ExperienceToNextLevel
		{
			get
			{
				return new PlayerDataExperience(this.Experience).ExperienceToNextLevel;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x0000CE40 File Offset: 0x0000B040
		[JsonIgnore]
		public int ExperienceInCurrentLevel
		{
			get
			{
				return new PlayerDataExperience(this.Experience).ExperienceInCurrentLevel;
			}
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0000CE68 File Offset: 0x0000B068
		public void FillWith(PlayerId playerId, PlayerId ownerPlayerId, BodyProperties bodyProperties, bool isFemale, string sigil, int experience, string lastPlayerName, string username, int userId, string lastRegion, string[] lastGameTypes, DateTime? lastLogin, int playtime, string shownBadgeId, int gold, PlayerStatsBase[] stats, bool shouldLog, bool isUsingClanSigil)
		{
			this.PlayerId = playerId;
			this.OwnerPlayerId = ownerPlayerId;
			this.BodyProperties = bodyProperties;
			this.IsFemale = isFemale;
			this.Sigil = sigil;
			this.IsUsingClanSigil = isUsingClanSigil;
			this.Experience = experience;
			this.LastPlayerName = lastPlayerName;
			this.Username = username;
			this.UserId = userId;
			this.LastRegion = lastRegion;
			this.LastGameTypes = lastGameTypes;
			this.LastLogin = lastLogin;
			this.Playtime = playtime;
			this.ShownBadgeId = shownBadgeId;
			this.Gold = gold;
			this.Stats = stats;
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0000CEFC File Offset: 0x0000B0FC
		public void FillWithNewPlayer(PlayerId playerId, PlayerId ownerPlayerId, string[] gameTypes)
		{
			this.Stats = new PlayerStatsBase[0];
			this.PlayerId = playerId;
			this.OwnerPlayerId = ownerPlayerId;
			this.Sigil = "11.8.1.4345.4345.770.774.1.0.0.158.7.5.512.512.770.769.1.0.0";
			this.IsUsingClanSigil = false;
			this.LastGameTypes = gameTypes;
			this.Username = null;
			this.UserId = -1;
			this.Gold = 0;
			BodyProperties bodyProperties;
			if (BodyProperties.FromString("<BodyProperties version='4' age='36.35' weight='0.1025' build='0.7'  key='001C380CC000234B88E68BBA1372B7578B7BB5D788BC567878966669835754B604F926450F67798C000000000000000000000000000000000000000000DC10C4' />", out bodyProperties))
			{
				this.BodyProperties = bodyProperties;
			}
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0000CF66 File Offset: 0x0000B166
		public bool HasGameStats(string gameType)
		{
			return this.GetGameStats(gameType) != null;
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0000CF74 File Offset: 0x0000B174
		public PlayerStatsBase GetGameStats(string gameType)
		{
			if (this.Stats != null)
			{
				foreach (PlayerStatsBase playerStatsBase in this.Stats)
				{
					if (playerStatsBase.GameType == gameType)
					{
						return playerStatsBase;
					}
				}
			}
			return null;
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0000CFB4 File Offset: 0x0000B1B4
		public void UpdateGameStats(PlayerStatsBase playerGameTypeStats)
		{
			bool flag = false;
			if (this.Stats != null)
			{
				for (int i = 0; i < this.Stats.Length; i++)
				{
					if (this.Stats[i].GameType == playerGameTypeStats.GameType)
					{
						this.Stats[i] = playerGameTypeStats;
						flag = true;
					}
				}
			}
			if (!flag)
			{
				List<PlayerStatsBase> list = new List<PlayerStatsBase>();
				if (this.Stats != null)
				{
					list.AddRange(this.Stats);
				}
				list.Add(playerGameTypeStats);
				this.Stats = list.ToArray();
			}
		}

		// Token: 0x040003A6 RID: 934
		private const string DefaultBodyProperties1 = "<BodyProperties version='4' age='36.35' weight='0.1025' build='0.7'  key='001C380CC000234B88E68BBA1372B7578B7BB5D788BC567878966669835754B604F926450F67798C000000000000000000000000000000000000000000DC10C4' />";

		// Token: 0x040003A7 RID: 935
		private const string DefaultBodyProperties2 = "<BodyProperties version='4' age='46.35' weight='0.1025' build='0.7'  key='001C380CC000234B88E68BBA1372B7578B7BB5D788BC567878966669835754B604F926450F67798C000000000000000000000000000000000000000000DC10C4' />";

		// Token: 0x040003A8 RID: 936
		public const string DefaultSigil = "11.8.1.4345.4345.770.774.1.0.0.158.7.5.512.512.770.769.1.0.0";

		// Token: 0x040003AC RID: 940
		private BodyProperties _bodyProperties;
	}
}
