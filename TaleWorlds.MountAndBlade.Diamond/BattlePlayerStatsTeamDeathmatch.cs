using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000F7 RID: 247
	[Serializable]
	public class BattlePlayerStatsTeamDeathmatch : BattlePlayerStatsBase
	{
		// Token: 0x060004EA RID: 1258 RVA: 0x0000581C File Offset: 0x00003A1C
		public BattlePlayerStatsTeamDeathmatch()
		{
			base.GameType = "TeamDeathmatch";
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x0000582F File Offset: 0x00003A2F
		// (set) Token: 0x060004EC RID: 1260 RVA: 0x00005837 File Offset: 0x00003A37
		public int Score { get; set; }
	}
}
