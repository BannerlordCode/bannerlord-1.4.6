using System;
using System.Collections.Generic;

namespace TaleWorlds.Core
{
	// Token: 0x020000BB RID: 187
	public interface IMissionTroopSupplier
	{
		// Token: 0x060009F3 RID: 2547
		IEnumerable<IAgentOriginBase> SupplyTroops(int numberToAllocate);

		// Token: 0x060009F4 RID: 2548
		IAgentOriginBase SupplyOneTroop();

		// Token: 0x060009F5 RID: 2549
		IEnumerable<IAgentOriginBase> GetAllTroops();

		// Token: 0x060009F6 RID: 2550
		BasicCharacterObject GetGeneralCharacter();

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060009F7 RID: 2551
		int NumRemovedTroops { get; }

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x060009F8 RID: 2552
		int NumTroopsNotSupplied { get; }

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060009F9 RID: 2553
		bool AnyTroopRemainsToBeSupplied { get; }

		// Token: 0x060009FA RID: 2554
		int GetNumberOfPlayerControllableTroops();
	}
}
