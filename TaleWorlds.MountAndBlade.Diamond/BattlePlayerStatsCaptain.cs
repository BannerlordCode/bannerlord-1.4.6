using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000F3 RID: 243
	[Serializable]
	public class BattlePlayerStatsCaptain : BattlePlayerStatsBase
	{
		// Token: 0x060004CA RID: 1226 RVA: 0x000056E2 File Offset: 0x000038E2
		public BattlePlayerStatsCaptain()
		{
			base.GameType = "Captain";
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x000056F5 File Offset: 0x000038F5
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x000056FD File Offset: 0x000038FD
		public int CaptainsKilled { get; set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x00005706 File Offset: 0x00003906
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x0000570E File Offset: 0x0000390E
		public int MVPs { get; set; }

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x00005717 File Offset: 0x00003917
		// (set) Token: 0x060004D0 RID: 1232 RVA: 0x0000571F File Offset: 0x0000391F
		public int Score { get; set; }
	}
}
