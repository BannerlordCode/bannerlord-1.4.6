using System;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x0200040A RID: 1034
	public interface IRetrainOutlawPartyMembersCampaignBehavior : ICampaignBehavior
	{
		// Token: 0x060040F2 RID: 16626
		int GetRetrainedNumber(CharacterObject character);

		// Token: 0x060040F3 RID: 16627
		void SetRetrainedNumber(CharacterObject character, int number);
	}
}
