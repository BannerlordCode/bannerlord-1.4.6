using System;

namespace TaleWorlds.CampaignSystem.GameState
{
	// Token: 0x0200039E RID: 926
	public interface IPartyScreenPrisonHandler
	{
		// Token: 0x06003582 RID: 13698
		void ExecuteTakeAllPrisonersScript();

		// Token: 0x06003583 RID: 13699
		void ExecuteDoneScript();

		// Token: 0x06003584 RID: 13700
		void ExecuteResetScript();

		// Token: 0x06003585 RID: 13701
		void ExecuteSellAllPrisoners();
	}
}
