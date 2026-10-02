using System;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x02000403 RID: 1027
	public interface IMarriageOfferCampaignBehavior : ICampaignBehavior
	{
		// Token: 0x06004067 RID: 16487
		void OnMarriageOfferedToPlayer(Hero suitor, Hero maiden);

		// Token: 0x06004068 RID: 16488
		void OnMarriageOfferCanceled(Hero suitor, Hero maiden);

		// Token: 0x06004069 RID: 16489
		MBBindingList<TextObject> GetMarriageAcceptedConsequences();

		// Token: 0x0600406A RID: 16490
		void OnMarriageOfferAcceptedOnPopUp();

		// Token: 0x0600406B RID: 16491
		void OnMarriageOfferDeclinedOnPopUp();

		// Token: 0x0600406C RID: 16492
		bool IsHeroEngaged(Hero hero);
	}
}
