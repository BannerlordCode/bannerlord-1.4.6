using System;
using System.Collections.Generic;

namespace TaleWorlds.MountAndBlade.Diamond.Lobby.LocalData
{
	// Token: 0x02000171 RID: 369
	public class MatchHistoryData : MultiplayerLocalData
	{
		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x000108C3 File Offset: 0x0000EAC3
		// (set) Token: 0x06000A44 RID: 2628 RVA: 0x000108CB File Offset: 0x0000EACB
		public string MatchId { get; set; }

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x000108D4 File Offset: 0x0000EAD4
		// (set) Token: 0x06000A46 RID: 2630 RVA: 0x000108DC File Offset: 0x0000EADC
		public string MatchType { get; set; }

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x000108E5 File Offset: 0x0000EAE5
		// (set) Token: 0x06000A48 RID: 2632 RVA: 0x000108ED File Offset: 0x0000EAED
		public string GameType { get; set; }

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x000108F6 File Offset: 0x0000EAF6
		// (set) Token: 0x06000A4A RID: 2634 RVA: 0x000108FE File Offset: 0x0000EAFE
		public string Map { get; set; }

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x00010907 File Offset: 0x0000EB07
		// (set) Token: 0x06000A4C RID: 2636 RVA: 0x0001090F File Offset: 0x0000EB0F
		public DateTime MatchDate { get; set; }

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x00010918 File Offset: 0x0000EB18
		// (set) Token: 0x06000A4E RID: 2638 RVA: 0x00010920 File Offset: 0x0000EB20
		public int WinnerTeam { get; set; }

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x00010929 File Offset: 0x0000EB29
		// (set) Token: 0x06000A50 RID: 2640 RVA: 0x00010931 File Offset: 0x0000EB31
		public string Faction1 { get; set; }

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x0001093A File Offset: 0x0000EB3A
		// (set) Token: 0x06000A52 RID: 2642 RVA: 0x00010942 File Offset: 0x0000EB42
		public string Faction2 { get; set; }

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x0001094B File Offset: 0x0000EB4B
		// (set) Token: 0x06000A54 RID: 2644 RVA: 0x00010953 File Offset: 0x0000EB53
		public int DefenderScore { get; set; }

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x0001095C File Offset: 0x0000EB5C
		// (set) Token: 0x06000A56 RID: 2646 RVA: 0x00010964 File Offset: 0x0000EB64
		public int AttackerScore { get; set; }

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x0001096D File Offset: 0x0000EB6D
		// (set) Token: 0x06000A58 RID: 2648 RVA: 0x00010975 File Offset: 0x0000EB75
		public List<PlayerInfo> Players { get; set; }

		// Token: 0x06000A59 RID: 2649 RVA: 0x0001097E File Offset: 0x0000EB7E
		public MatchHistoryData()
		{
			this.Players = new List<PlayerInfo>();
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00010994 File Offset: 0x0000EB94
		public override bool HasSameContentWith(MultiplayerLocalData other)
		{
			MatchHistoryData matchHistoryData;
			if ((matchHistoryData = other as MatchHistoryData) == null)
			{
				return false;
			}
			bool flag = this.MatchId == matchHistoryData.MatchId && this.MatchType == matchHistoryData.MatchType && this.GameType == matchHistoryData.GameType && this.Map == matchHistoryData.Map && this.MatchDate == matchHistoryData.MatchDate && this.WinnerTeam == matchHistoryData.WinnerTeam && this.Faction1 == matchHistoryData.Faction1 && this.Faction2 == matchHistoryData.Faction2 && this.DefenderScore == matchHistoryData.DefenderScore && this.AttackerScore == matchHistoryData.AttackerScore;
			if (!flag)
			{
				return false;
			}
			if (this.Players != null || matchHistoryData.Players != null)
			{
				List<PlayerInfo> players = this.Players;
				int? num = ((players != null) ? new int?(players.Count) : null);
				List<PlayerInfo> players2 = matchHistoryData.Players;
				int? num2 = ((players2 != null) ? new int?(players2.Count) : null);
				if (!((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null))))
				{
					return flag;
				}
			}
			for (int i = 0; i < this.Players.Count; i++)
			{
				PlayerInfo playerInfo = this.Players[i];
				PlayerInfo playerInfo2 = matchHistoryData.Players[i];
				if (!playerInfo.HasSameContentWith(playerInfo2))
				{
					return false;
				}
			}
			return flag;
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00010B28 File Offset: 0x0000ED28
		private PlayerInfo TryGetPlayer(string id)
		{
			foreach (PlayerInfo playerInfo in this.Players)
			{
				if (playerInfo.PlayerId == id)
				{
					return playerInfo;
				}
			}
			return null;
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00010B8C File Offset: 0x0000ED8C
		public void AddOrUpdatePlayer(string id, string username, int forcedIndex, int teamNo)
		{
			PlayerInfo playerInfo = this.TryGetPlayer(id);
			if (playerInfo == null)
			{
				this.Players.Add(new PlayerInfo
				{
					PlayerId = id,
					Username = username,
					ForcedIndex = forcedIndex,
					TeamNo = teamNo
				});
				return;
			}
			playerInfo.TeamNo = teamNo;
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x00010BDC File Offset: 0x0000EDDC
		public bool TryUpdatePlayerStats(string id, int kill, int death, int assist)
		{
			PlayerInfo playerInfo = this.TryGetPlayer(id);
			if (playerInfo != null)
			{
				playerInfo.Kill = kill;
				playerInfo.Death = death;
				playerInfo.Assist = assist;
				return true;
			}
			return false;
		}
	}
}
