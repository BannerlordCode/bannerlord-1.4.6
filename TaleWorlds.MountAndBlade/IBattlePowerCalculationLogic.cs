using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000264 RID: 612
	public interface IBattlePowerCalculationLogic : IMissionBehavior
	{
		// Token: 0x06002269 RID: 8809
		float GetTotalTeamPower(Team team);
	}
}
