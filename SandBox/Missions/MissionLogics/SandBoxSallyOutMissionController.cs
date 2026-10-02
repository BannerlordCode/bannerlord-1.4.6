using System;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics
{
	// Token: 0x02000084 RID: 132
	public class SandBoxSallyOutMissionController : SallyOutMissionController
	{
		// Token: 0x06000533 RID: 1331 RVA: 0x00022E24 File Offset: 0x00021024
		public SandBoxSallyOutMissionController(bool isSallyOutAmbush)
			: base(isSallyOutAmbush)
		{
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00022E2D File Offset: 0x0002102D
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._mapEvent = MapEvent.PlayerMapEvent;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00022E40 File Offset: 0x00021040
		protected override void GetInitialTroopCounts(out int besiegedTotalTroopCount, out int besiegerTotalTroopCount)
		{
			besiegedTotalTroopCount = this._mapEvent.GetNumberOfInvolvedMen(BattleSideEnum.Defender);
			besiegerTotalTroopCount = this._mapEvent.GetNumberOfInvolvedMen(BattleSideEnum.Attacker);
		}

		// Token: 0x040002C6 RID: 710
		private MapEvent _mapEvent;
	}
}
