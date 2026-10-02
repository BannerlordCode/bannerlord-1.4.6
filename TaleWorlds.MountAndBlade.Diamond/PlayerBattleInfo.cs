using System;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200013D RID: 317
	[Serializable]
	public class PlayerBattleInfo
	{
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x0000C890 File Offset: 0x0000AA90
		// (set) Token: 0x0600087E RID: 2174 RVA: 0x0000C898 File Offset: 0x0000AA98
		public PlayerId PlayerId { get; set; }

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x0000C8A1 File Offset: 0x0000AAA1
		// (set) Token: 0x06000880 RID: 2176 RVA: 0x0000C8A9 File Offset: 0x0000AAA9
		public string Name { get; set; }

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x0000C8B2 File Offset: 0x0000AAB2
		// (set) Token: 0x06000882 RID: 2178 RVA: 0x0000C8BA File Offset: 0x0000AABA
		public int TeamNo { get; set; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x0000C8C3 File Offset: 0x0000AAC3
		public bool Fled
		{
			get
			{
				return this._state == PlayerBattleInfo.State.Fled;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x0000C8CE File Offset: 0x0000AACE
		public bool Disconnected
		{
			get
			{
				return this._state == PlayerBattleInfo.State.Disconnected;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x0000C8D9 File Offset: 0x0000AAD9
		// (set) Token: 0x06000886 RID: 2182 RVA: 0x0000C8E1 File Offset: 0x0000AAE1
		public BattleJoinType JoinType { get; set; }

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x0000C8EA File Offset: 0x0000AAEA
		// (set) Token: 0x06000888 RID: 2184 RVA: 0x0000C8F2 File Offset: 0x0000AAF2
		public int PeerIndex { get; set; }

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x0000C8FB File Offset: 0x0000AAFB
		public PlayerBattleInfo.State CurrentState
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0000C903 File Offset: 0x0000AB03
		public PlayerBattleInfo()
		{
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0000C90B File Offset: 0x0000AB0B
		public PlayerBattleInfo(PlayerId playerId, string name, int teamNo)
		{
			this.PlayerId = playerId;
			this.Name = name;
			this.TeamNo = teamNo;
			this.PeerIndex = -1;
			this._state = PlayerBattleInfo.State.AssignedToBattle;
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0000C936 File Offset: 0x0000AB36
		public PlayerBattleInfo(PlayerId playerId, string name, int teamNo, int peerIndex, PlayerBattleInfo.State state)
		{
			this.PlayerId = playerId;
			this.Name = name;
			this.TeamNo = teamNo;
			this.PeerIndex = peerIndex;
			this._state = state;
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0000C963 File Offset: 0x0000AB63
		public void Flee()
		{
			if (this._state != PlayerBattleInfo.State.Disconnected && this._state != PlayerBattleInfo.State.AtBattle)
			{
				throw new Exception("PlayerBattleInfo incorrect state, expected AtBattle or Disconnected; got " + this._state);
			}
			this._state = PlayerBattleInfo.State.Fled;
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0000C999 File Offset: 0x0000AB99
		public void Disconnect()
		{
			if (this._state != PlayerBattleInfo.State.AtBattle)
			{
				throw new Exception("PlayerBattleInfo incorrect state, expected AtBattle got " + this._state);
			}
			this._state = PlayerBattleInfo.State.Disconnected;
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0000C9C6 File Offset: 0x0000ABC6
		public void Initialize(int peerIndex)
		{
			if (this._state != PlayerBattleInfo.State.AssignedToBattle)
			{
				throw new Exception("PlayerBattleInfo incorrect state, expected AssignedToBattle got " + this._state);
			}
			this.PeerIndex = peerIndex;
			this._state = PlayerBattleInfo.State.AtBattle;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0000C9FA File Offset: 0x0000ABFA
		public void RejoinBattle(int teamNo)
		{
			if (this._state != PlayerBattleInfo.State.Disconnected)
			{
				throw new Exception("PlayerBattleInfo incorrect state, expected Disconnected got " + this._state);
			}
			this.TeamNo = teamNo;
			this.PeerIndex = -1;
			this._state = PlayerBattleInfo.State.AssignedToBattle;
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0000CA35 File Offset: 0x0000AC35
		public PlayerBattleInfo Clone()
		{
			return new PlayerBattleInfo(this.PlayerId, this.Name, this.TeamNo, this.PeerIndex, this._state);
		}

		// Token: 0x0400039A RID: 922
		private PlayerBattleInfo.State _state;

		// Token: 0x020001CA RID: 458
		public enum State
		{
			// Token: 0x040006A5 RID: 1701
			Created,
			// Token: 0x040006A6 RID: 1702
			AssignedToBattle,
			// Token: 0x040006A7 RID: 1703
			AtBattle,
			// Token: 0x040006A8 RID: 1704
			Disconnected,
			// Token: 0x040006A9 RID: 1705
			Fled
		}
	}
}
