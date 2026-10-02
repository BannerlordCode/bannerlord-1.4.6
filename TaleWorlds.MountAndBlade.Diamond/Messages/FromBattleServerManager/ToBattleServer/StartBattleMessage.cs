using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromBattleServerManager.ToBattleServer
{
	// Token: 0x020000E4 RID: 228
	[MessageDescription("BattleServerManager", "BattleServer", true)]
	[Serializable]
	public class StartBattleMessage : Message
	{
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x00004DD8 File Offset: 0x00002FD8
		// (set) Token: 0x0600042B RID: 1067 RVA: 0x00004DE0 File Offset: 0x00002FE0
		[JsonProperty]
		public string SceneName { get; private set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x00004DE9 File Offset: 0x00002FE9
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x00004DF1 File Offset: 0x00002FF1
		[JsonProperty]
		public string GameType { get; private set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x00004DFA File Offset: 0x00002FFA
		// (set) Token: 0x0600042F RID: 1071 RVA: 0x00004E02 File Offset: 0x00003002
		[JsonProperty]
		public Guid BattleId { get; private set; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x00004E0B File Offset: 0x0000300B
		// (set) Token: 0x06000431 RID: 1073 RVA: 0x00004E13 File Offset: 0x00003013
		[JsonProperty]
		public string Faction1 { get; private set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x00004E1C File Offset: 0x0000301C
		// (set) Token: 0x06000433 RID: 1075 RVA: 0x00004E24 File Offset: 0x00003024
		[JsonProperty]
		public string Faction2 { get; private set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x00004E2D File Offset: 0x0000302D
		// (set) Token: 0x06000435 RID: 1077 RVA: 0x00004E35 File Offset: 0x00003035
		[JsonProperty]
		public int MinRequiredPlayerCountToStartBattle { get; private set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x00004E3E File Offset: 0x0000303E
		// (set) Token: 0x06000437 RID: 1079 RVA: 0x00004E46 File Offset: 0x00003046
		[JsonProperty]
		public int BattleSize { get; private set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x00004E4F File Offset: 0x0000304F
		// (set) Token: 0x06000439 RID: 1081 RVA: 0x00004E57 File Offset: 0x00003057
		[JsonProperty]
		public int RoundThreshold { get; private set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x00004E60 File Offset: 0x00003060
		// (set) Token: 0x0600043B RID: 1083 RVA: 0x00004E68 File Offset: 0x00003068
		[JsonProperty]
		public float MoraleThreshold { get; private set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x00004E71 File Offset: 0x00003071
		// (set) Token: 0x0600043D RID: 1085 RVA: 0x00004E79 File Offset: 0x00003079
		[JsonProperty]
		public bool UseAnalytics { get; private set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x00004E82 File Offset: 0x00003082
		// (set) Token: 0x0600043F RID: 1087 RVA: 0x00004E8A File Offset: 0x0000308A
		[JsonProperty]
		public bool CaptureMovementData { get; private set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x00004E93 File Offset: 0x00003093
		// (set) Token: 0x06000441 RID: 1089 RVA: 0x00004E9B File Offset: 0x0000309B
		[JsonProperty]
		public string AnalyticsServiceAddress { get; private set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x00004EA4 File Offset: 0x000030A4
		// (set) Token: 0x06000443 RID: 1091 RVA: 0x00004EAC File Offset: 0x000030AC
		[JsonProperty]
		public int MaxFriendlyKillCount { get; private set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x00004EB5 File Offset: 0x000030B5
		// (set) Token: 0x06000445 RID: 1093 RVA: 0x00004EBD File Offset: 0x000030BD
		[JsonProperty]
		public float MaxFriendlyDamage { get; private set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x00004EC6 File Offset: 0x000030C6
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x00004ECE File Offset: 0x000030CE
		[JsonProperty]
		public float MaxFriendlyDamagePerSingleRound { get; private set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x00004ED7 File Offset: 0x000030D7
		// (set) Token: 0x06000449 RID: 1097 RVA: 0x00004EDF File Offset: 0x000030DF
		[JsonProperty]
		public float RoundFriendlyDamageLimit { get; private set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x00004EE8 File Offset: 0x000030E8
		// (set) Token: 0x0600044B RID: 1099 RVA: 0x00004EF0 File Offset: 0x000030F0
		[JsonProperty]
		public int MaxRoundsOverLimitCount { get; private set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x00004EF9 File Offset: 0x000030F9
		// (set) Token: 0x0600044D RID: 1101 RVA: 0x00004F01 File Offset: 0x00003101
		[JsonProperty]
		public bool IsPremadeGame { get; private set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x00004F0A File Offset: 0x0000310A
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x00004F12 File Offset: 0x00003112
		[JsonProperty]
		public string[] ProfanityList { get; private set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x00004F1B File Offset: 0x0000311B
		// (set) Token: 0x06000451 RID: 1105 RVA: 0x00004F23 File Offset: 0x00003123
		[JsonProperty]
		public PremadeGameType PremadeGameType { get; private set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x00004F2C File Offset: 0x0000312C
		// (set) Token: 0x06000453 RID: 1107 RVA: 0x00004F34 File Offset: 0x00003134
		[JsonProperty]
		public string[] AllowList { get; private set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x00004F3D File Offset: 0x0000313D
		// (set) Token: 0x06000455 RID: 1109 RVA: 0x00004F45 File Offset: 0x00003145
		[JsonProperty]
		public PlayerId[] AssignedPlayers { get; private set; }

		// Token: 0x06000456 RID: 1110 RVA: 0x00004F4E File Offset: 0x0000314E
		public StartBattleMessage()
		{
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00004F58 File Offset: 0x00003158
		public StartBattleMessage(Guid battleId, string sceneName, string gameType, string faction1, string faction2, int minRequiredPlayerCountToStartBattle, int battleSize, int roundThreshold, float moraleThreshold, bool useAnalytics, bool captureMovementData, string analyticsServiceAddress, int maxFriendlyKillCount, float maxFriendlyDamage, float maxFriendlyDamagePerSingleRound, float roundFriendlyDamageLimit, int maxRoundsOverLimitCount, bool isPremadeGame, PremadeGameType premadeGameType, string[] profanityList, string[] allowList, PlayerId[] assignedPlayers)
		{
			this.SceneName = sceneName;
			this.GameType = gameType;
			this.BattleId = battleId;
			this.Faction1 = faction1;
			this.Faction2 = faction2;
			this.MinRequiredPlayerCountToStartBattle = minRequiredPlayerCountToStartBattle;
			this.BattleSize = battleSize;
			this.UseAnalytics = useAnalytics;
			this.CaptureMovementData = captureMovementData;
			this.AnalyticsServiceAddress = analyticsServiceAddress;
			this.RoundThreshold = roundThreshold;
			this.MoraleThreshold = moraleThreshold;
			this.MaxFriendlyKillCount = maxFriendlyKillCount;
			this.MaxFriendlyDamage = maxFriendlyDamage;
			this.MaxFriendlyDamagePerSingleRound = maxFriendlyDamagePerSingleRound;
			this.RoundFriendlyDamageLimit = roundFriendlyDamageLimit;
			this.MaxRoundsOverLimitCount = maxRoundsOverLimitCount;
			this.IsPremadeGame = isPremadeGame;
			this.PremadeGameType = premadeGameType;
			this.ProfanityList = profanityList;
			this.AllowList = allowList;
			this.AssignedPlayers = assignedPlayers;
		}
	}
}
