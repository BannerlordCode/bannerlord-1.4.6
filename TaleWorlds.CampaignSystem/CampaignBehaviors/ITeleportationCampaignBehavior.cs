using System;
using TaleWorlds.CampaignSystem.Map;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x0200040D RID: 1037
	public interface ITeleportationCampaignBehavior : ICampaignBehavior
	{
		// Token: 0x06004162 RID: 16738
		bool GetTargetOfTeleportingHero(Hero teleportingHero, out bool isGovernor, out bool isPartyLeader, out IMapPoint target);

		// Token: 0x06004163 RID: 16739
		CampaignTime GetHeroArrivalTimeToDestination(Hero teleportingHero);
	}
}
