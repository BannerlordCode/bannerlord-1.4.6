using System;
using System.Collections.Generic;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200011A RID: 282
	public interface IBadgeComponent
	{
		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000651 RID: 1617
		Dictionary<ValueTuple<PlayerId, string, string>, int> DataDictionary { get; }

		// Token: 0x06000652 RID: 1618
		void OnPlayerJoin(PlayerData playerData);

		// Token: 0x06000653 RID: 1619
		void OnStartingNextBattle();
	}
}
