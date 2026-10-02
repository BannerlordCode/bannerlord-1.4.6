using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000286 RID: 646
	public interface ICustomReinforcementSpawnTimer
	{
		// Token: 0x06002408 RID: 9224
		bool Check(BattleSideEnum side);

		// Token: 0x06002409 RID: 9225
		void ResetTimer(BattleSideEnum side);
	}
}
