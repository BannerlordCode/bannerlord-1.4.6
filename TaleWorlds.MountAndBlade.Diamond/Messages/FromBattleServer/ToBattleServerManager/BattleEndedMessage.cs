using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromBattleServer.ToBattleServerManager
{
	// Token: 0x020000CE RID: 206
	[MessageDescription("BattleServer", "BattleServerManager", true)]
	[Serializable]
	public class BattleEndedMessage : Message
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x00004886 File Offset: 0x00002A86
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x0000488E File Offset: 0x00002A8E
		[JsonProperty]
		public BattleResult BattleResult { get; set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x00004897 File Offset: 0x00002A97
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x0000489F File Offset: 0x00002A9F
		[JsonProperty]
		public GameLog[] GameLogs { get; set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060003BA RID: 954 RVA: 0x000048A8 File Offset: 0x00002AA8
		// (set) Token: 0x060003BB RID: 955 RVA: 0x000048B0 File Offset: 0x00002AB0
		[JsonProperty]
		public List<BadgeDataEntry> BadgeDataEntries { get; set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060003BC RID: 956 RVA: 0x000048B9 File Offset: 0x00002AB9
		// (set) Token: 0x060003BD RID: 957 RVA: 0x000048C1 File Offset: 0x00002AC1
		[JsonProperty]
		public Dictionary<int, int> TeamScores { get; set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060003BE RID: 958 RVA: 0x000048CA File Offset: 0x00002ACA
		// (set) Token: 0x060003BF RID: 959 RVA: 0x000048D2 File Offset: 0x00002AD2
		[JsonProperty]
		public Dictionary<string, int> PlayerScores { get; set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x000048DB File Offset: 0x00002ADB
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x000048E3 File Offset: 0x00002AE3
		[JsonProperty]
		public int GameTime { get; set; }

		// Token: 0x060003C2 RID: 962 RVA: 0x000048EC File Offset: 0x00002AEC
		public BattleEndedMessage()
		{
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x000048F4 File Offset: 0x00002AF4
		public BattleEndedMessage(BattleResult battleResult, GameLog[] gameLogs, Dictionary<ValueTuple<PlayerId, string, string>, int> badgeDataDictionary, int gameTime, Dictionary<int, int> teamScores, Dictionary<PlayerId, int> playerScores)
		{
			this.BattleResult = battleResult;
			this.GameLogs = gameLogs;
			this.BadgeDataEntries = BadgeDataEntry.ToList(badgeDataDictionary);
			this.TeamScores = teamScores;
			this.PlayerScores = playerScores.ToDictionary<KeyValuePair<PlayerId, int>, string, int>((KeyValuePair<PlayerId, int> kvp) => kvp.Key.ToString(), (KeyValuePair<PlayerId, int> kvp) => kvp.Value);
			this.GameTime = gameTime;
		}
	}
}
