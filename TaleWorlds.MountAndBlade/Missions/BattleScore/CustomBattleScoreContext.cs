using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.Missions.BattleScore
{
	// Token: 0x020003F0 RID: 1008
	public class CustomBattleScoreContext : BattleScoreContext
	{
		// Token: 0x0600373A RID: 14138 RVA: 0x000E48B7 File Offset: 0x000E2AB7
		public CustomBattleScoreContext(Mission mission)
		{
			this._mission = mission;
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x0600373B RID: 14139 RVA: 0x000E48C6 File Offset: 0x000E2AC6
		public override bool IsPowerComparisonRelevant
		{
			get
			{
				return this._mission.Mode != MissionMode.Deployment;
			}
		}

		// Token: 0x0600373C RID: 14140 RVA: 0x000E48D9 File Offset: 0x000E2AD9
		public override Banner GetAttackerBanner()
		{
			return this.GetSideBannerInfo(BattleSideEnum.Attacker);
		}

		// Token: 0x0600373D RID: 14141 RVA: 0x000E48E2 File Offset: 0x000E2AE2
		public override Banner GetDefenderBanner()
		{
			return this.GetSideBannerInfo(BattleSideEnum.Defender);
		}

		// Token: 0x0600373E RID: 14142 RVA: 0x000E48EC File Offset: 0x000E2AEC
		private Banner GetSideBannerInfo(BattleSideEnum sideEnum)
		{
			MissionCombatantsLogic missionBehavior = this._mission.GetMissionBehavior<MissionCombatantsLogic>();
			if (missionBehavior == null)
			{
				return null;
			}
			return missionBehavior.GetBannerForSide(sideEnum);
		}

		// Token: 0x040017BA RID: 6074
		private readonly Mission _mission;
	}
}
