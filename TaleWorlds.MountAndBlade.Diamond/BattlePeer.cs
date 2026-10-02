using System;
using System.Collections.Generic;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000ED RID: 237
	public class BattlePeer
	{
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0000536C File Offset: 0x0000356C
		// (set) Token: 0x06000488 RID: 1160 RVA: 0x00005374 File Offset: 0x00003574
		public int Index { get; private set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x0000537D File Offset: 0x0000357D
		// (set) Token: 0x0600048A RID: 1162 RVA: 0x00005385 File Offset: 0x00003585
		public string Name { get; private set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0000538E File Offset: 0x0000358E
		public PlayerId PlayerId
		{
			get
			{
				return this.PlayerData.PlayerId;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x0000539B File Offset: 0x0000359B
		// (set) Token: 0x0600048D RID: 1165 RVA: 0x000053A3 File Offset: 0x000035A3
		public int TeamNo { get; private set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x000053AC File Offset: 0x000035AC
		// (set) Token: 0x0600048F RID: 1167 RVA: 0x000053B4 File Offset: 0x000035B4
		public BattleJoinType BattleJoinType { get; private set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x000053BD File Offset: 0x000035BD
		public bool Quit
		{
			get
			{
				return this.QuitType > BattlePeerQuitType.None;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x000053C8 File Offset: 0x000035C8
		// (set) Token: 0x06000492 RID: 1170 RVA: 0x000053D0 File Offset: 0x000035D0
		public PlayerData PlayerData { get; private set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x000053D9 File Offset: 0x000035D9
		// (set) Token: 0x06000494 RID: 1172 RVA: 0x000053E1 File Offset: 0x000035E1
		public Dictionary<string, List<string>> UsedCosmetics { get; private set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x000053EA File Offset: 0x000035EA
		// (set) Token: 0x06000496 RID: 1174 RVA: 0x000053F2 File Offset: 0x000035F2
		public int SessionKey { get; private set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x000053FB File Offset: 0x000035FB
		// (set) Token: 0x06000498 RID: 1176 RVA: 0x00005403 File Offset: 0x00003603
		public BattlePeerQuitType QuitType { get; private set; }

		// Token: 0x06000499 RID: 1177 RVA: 0x0000540C File Offset: 0x0000360C
		public BattlePeer(string name, PlayerData playerData, Dictionary<string, List<string>> usedCosmetics, int teamNo, BattleJoinType battleJoinType)
		{
			this.Index = -1;
			this.Name = name;
			this.PlayerData = playerData;
			this.UsedCosmetics = usedCosmetics;
			this.TeamNo = teamNo;
			this.BattleJoinType = battleJoinType;
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00005440 File Offset: 0x00003640
		internal void Flee()
		{
			this.QuitType = BattlePeerQuitType.Fled;
			this.Index = -1;
			this.SessionKey = 0;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00005457 File Offset: 0x00003657
		internal void SetPlayerDisconnectdFromLobby()
		{
			this.QuitType = BattlePeerQuitType.DisconnectedFromLobby;
			this.Index = -1;
			this.SessionKey = 0;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0000546E File Offset: 0x0000366E
		internal void SetPlayerDisconnectdFromGameSession()
		{
			this.QuitType = BattlePeerQuitType.DisconnectedFromGameSession;
			this.Index = -1;
			this.SessionKey = 0;
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00005485 File Offset: 0x00003685
		public void Rejoin(int teamNo)
		{
			this.QuitType = BattlePeerQuitType.None;
			this.TeamNo = teamNo;
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00005495 File Offset: 0x00003695
		public void InitializeSession(int index, int sessionKey)
		{
			this.Index = index;
			this.SessionKey = sessionKey;
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x000054A5 File Offset: 0x000036A5
		internal void SetPlayerKickedDueToFriendlyDamage()
		{
			this.QuitType = BattlePeerQuitType.KickedDueToFriendlyDamage;
			this.Index = -1;
			this.SessionKey = 0;
		}
	}
}
