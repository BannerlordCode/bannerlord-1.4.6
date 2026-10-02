using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003F9 RID: 1017
	public interface IAlleyCampaignBehavior : ICampaignBehavior
	{
		// Token: 0x0600402C RID: 16428
		bool GetIsPlayerAlleyUnderAttack(Alley alley);

		// Token: 0x0600402D RID: 16429
		int GetPlayerOwnedAlleyTroopCount(Alley alley);

		// Token: 0x0600402E RID: 16430
		int GetResponseTimeLeftForAttackInDays(Alley alley);

		// Token: 0x0600402F RID: 16431
		void AbandonAlleyFromClanMenu(Alley alley);

		// Token: 0x06004030 RID: 16432
		Hero GetAssignedClanMemberOfAlley(Alley alley);

		// Token: 0x06004031 RID: 16433
		bool IsHeroAlleyLeaderOfAnyPlayerAlley(Hero hero);

		// Token: 0x06004032 RID: 16434
		List<Hero> GetAllAssignedClanMembersForOwnedAlleys();

		// Token: 0x06004033 RID: 16435
		void ChangeAlleyMember(Alley alley, Hero newAlleyLead);

		// Token: 0x06004034 RID: 16436
		void OnPlayerRetreatedFromMission();

		// Token: 0x06004035 RID: 16437
		void OnPlayerDiedInMission();
	}
}
