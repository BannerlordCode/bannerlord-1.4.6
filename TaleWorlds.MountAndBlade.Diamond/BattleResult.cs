using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.Library;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000F8 RID: 248
	[Serializable]
	public class BattleResult
	{
		// Token: 0x060004ED RID: 1261 RVA: 0x00005840 File Offset: 0x00003A40
		public BattleResult()
		{
			this.PlayerEntries = new Dictionary<string, BattlePlayerEntry>();
			this.IsCancelled = false;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0000585C File Offset: 0x00003A5C
		public void AddOrUpdatePlayerEntry(PlayerId playerId, int teamNo, string gameMode, Guid party, int overriddenInitialPlayTime = -1)
		{
			BattlePlayerEntry battlePlayerEntry;
			if (this.PlayerEntries.TryGetValue(playerId.ToString(), out battlePlayerEntry))
			{
				battlePlayerEntry.TeamNo = teamNo;
				battlePlayerEntry.Party = party;
				battlePlayerEntry.GameType = gameMode;
				if (battlePlayerEntry.Disconnected)
				{
					battlePlayerEntry.Disconnected = false;
					battlePlayerEntry.LastJoinTime = DateTime.Now;
					return;
				}
			}
			else
			{
				BattlePlayerStatsBase battlePlayerStatsBase = this.CreatePlayerBattleStats(gameMode);
				battlePlayerEntry = new BattlePlayerEntry();
				battlePlayerEntry.PlayerId = playerId;
				battlePlayerEntry.TeamNo = teamNo;
				battlePlayerEntry.Party = party;
				battlePlayerEntry.GameType = gameMode;
				battlePlayerEntry.PlayerStats = battlePlayerStatsBase;
				battlePlayerEntry.LastJoinTime = DateTime.Now;
				battlePlayerEntry.PlayTime = ((overriddenInitialPlayTime != -1) ? overriddenInitialPlayTime : 0);
				battlePlayerEntry.Disconnected = false;
				this.PlayerEntries.Add(playerId.ToString(), battlePlayerEntry);
			}
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00005926 File Offset: 0x00003B26
		public bool TryGetPlayerEntry(PlayerId playerId, out BattlePlayerEntry battlePlayerEntry)
		{
			return this.PlayerEntries.TryGetValue(playerId.ToString(), out battlePlayerEntry);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00005944 File Offset: 0x00003B44
		public void HandlePlayerDisconnect(PlayerId playerId)
		{
			BattlePlayerEntry battlePlayerEntry;
			if (this.PlayerEntries.TryGetValue(playerId.ToString(), out battlePlayerEntry))
			{
				battlePlayerEntry.Disconnected = true;
				battlePlayerEntry.PlayTime += (int)(DateTime.Now - battlePlayerEntry.LastJoinTime).TotalSeconds;
			}
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0000599C File Offset: 0x00003B9C
		public void DebugPrint()
		{
			Debug.Print("-----PRINTING BATTLE RESULT-----", 0, Debug.DebugColor.White, 17592186044416UL);
			foreach (BattlePlayerEntry battlePlayerEntry in this.PlayerEntries.Values)
			{
				Debug.Print("Player: " + battlePlayerEntry.PlayerId + "[DEBUG] ", 0, Debug.DebugColor.White, 17592186044416UL);
				Debug.Print("Kill: " + battlePlayerEntry.PlayerStats.Kills + "[DEBUG] ", 0, Debug.DebugColor.White, 17592186044416UL);
				Debug.Print("Death: " + battlePlayerEntry.PlayerStats.Deaths + "[DEBUG] ", 0, Debug.DebugColor.White, 17592186044416UL);
				Debug.Print("----", 0, Debug.DebugColor.White, 17592186044416UL);
			}
			Debug.Print("-----PRINTING OVER-----", 0, Debug.DebugColor.White, 17592186044416UL);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00005AC0 File Offset: 0x00003CC0
		public void SetBattleFinished(int winnerTeamNo, bool isPremadeGame, PremadeGameType premadeGameType)
		{
			this.WinnerTeamNo = winnerTeamNo;
			this.IsPremadeGame = isPremadeGame;
			this.PremadeGameType = premadeGameType;
			foreach (BattlePlayerEntry battlePlayerEntry in this.PlayerEntries.Values)
			{
				battlePlayerEntry.Won = battlePlayerEntry.TeamNo == winnerTeamNo;
				if (!battlePlayerEntry.Disconnected)
				{
					battlePlayerEntry.PlayTime += (int)(DateTime.Now - battlePlayerEntry.LastJoinTime).TotalSeconds;
				}
			}
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00005B64 File Offset: 0x00003D64
		public void SetBattleCancelled()
		{
			this.IsCancelled = true;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00005B70 File Offset: 0x00003D70
		private BattlePlayerStatsBase CreatePlayerBattleStats(string gameType)
		{
			if (gameType == "Skirmish")
			{
				return new BattlePlayerStatsSkirmish();
			}
			if (gameType == "Captain")
			{
				return new BattlePlayerStatsCaptain();
			}
			if (gameType == "Siege")
			{
				return new BattlePlayerStatsSiege();
			}
			if (gameType == "TeamDeathmatch")
			{
				return new BattlePlayerStatsTeamDeathmatch();
			}
			if (gameType == "Duel")
			{
				return new BattlePlayerStatsDuel();
			}
			if (gameType == "Battle")
			{
				return new BattlePlayerStatsBattle();
			}
			return new BattlePlayerStatsBase();
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00005BF4 File Offset: 0x00003DF4
		// (set) Token: 0x060004F6 RID: 1270 RVA: 0x00005BFC File Offset: 0x00003DFC
		[JsonProperty]
		public bool IsCancelled { get; private set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x00005C05 File Offset: 0x00003E05
		// (set) Token: 0x060004F8 RID: 1272 RVA: 0x00005C0D File Offset: 0x00003E0D
		[JsonProperty]
		public int WinnerTeamNo { get; private set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x00005C16 File Offset: 0x00003E16
		// (set) Token: 0x060004FA RID: 1274 RVA: 0x00005C1E File Offset: 0x00003E1E
		[JsonProperty]
		public bool IsPremadeGame { get; private set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x00005C27 File Offset: 0x00003E27
		// (set) Token: 0x060004FC RID: 1276 RVA: 0x00005C2F File Offset: 0x00003E2F
		[JsonProperty]
		public PremadeGameType PremadeGameType { get; private set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x00005C38 File Offset: 0x00003E38
		// (set) Token: 0x060004FE RID: 1278 RVA: 0x00005C40 File Offset: 0x00003E40
		[JsonProperty]
		public Dictionary<string, BattlePlayerEntry> PlayerEntries { get; private set; }
	}
}
