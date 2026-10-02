using System;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000EE RID: 238
	[Serializable]
	public class BattlePlayerEntry
	{
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x000054BC File Offset: 0x000036BC
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x000054C4 File Offset: 0x000036C4
		public PlayerId PlayerId { get; set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x000054CD File Offset: 0x000036CD
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x000054D5 File Offset: 0x000036D5
		public int TeamNo { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x000054DE File Offset: 0x000036DE
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x000054E6 File Offset: 0x000036E6
		public Guid Party { get; set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x000054EF File Offset: 0x000036EF
		// (set) Token: 0x060004A7 RID: 1191 RVA: 0x000054F7 File Offset: 0x000036F7
		public BattlePlayerStatsBase PlayerStats { get; set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x00005500 File Offset: 0x00003700
		// (set) Token: 0x060004A9 RID: 1193 RVA: 0x00005508 File Offset: 0x00003708
		public int PlayTime { get; set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x00005511 File Offset: 0x00003711
		// (set) Token: 0x060004AB RID: 1195 RVA: 0x00005519 File Offset: 0x00003719
		public DateTime LastJoinTime { get; set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x00005522 File Offset: 0x00003722
		// (set) Token: 0x060004AD RID: 1197 RVA: 0x0000552A File Offset: 0x0000372A
		public bool Disconnected { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00005533 File Offset: 0x00003733
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x0000553B File Offset: 0x0000373B
		public string GameType { get; set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00005544 File Offset: 0x00003744
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x0000554C File Offset: 0x0000374C
		public bool Won { get; set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00005555 File Offset: 0x00003755
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x0000555D File Offset: 0x0000375D
		public BattleJoinType BattleJoinType { get; set; }
	}
}
