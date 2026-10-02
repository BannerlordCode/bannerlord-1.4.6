using System;

namespace TaleWorlds.CampaignSystem.GameState
{
	// Token: 0x02000396 RID: 918
	public interface IInventoryStateHandler
	{
		// Token: 0x0600351B RID: 13595
		void ExecuteLootingScript();

		// Token: 0x0600351C RID: 13596
		void ExecuteSellAllLoot();

		// Token: 0x0600351D RID: 13597
		void ExecuteBuyConsumableItem();
	}
}
