using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.Missions.BattleScore
{
	// Token: 0x020003EF RID: 1007
	public abstract class BattleScoreContext
	{
		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06003736 RID: 14134
		public abstract bool IsPowerComparisonRelevant { get; }

		// Token: 0x06003737 RID: 14135
		public abstract Banner GetAttackerBanner();

		// Token: 0x06003738 RID: 14136
		public abstract Banner GetDefenderBanner();
	}
}
