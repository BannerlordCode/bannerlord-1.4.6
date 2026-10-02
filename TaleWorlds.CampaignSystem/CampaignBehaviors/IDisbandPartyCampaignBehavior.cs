using System;
using TaleWorlds.CampaignSystem.Party;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003FD RID: 1021
	public interface IDisbandPartyCampaignBehavior : ICampaignBehavior
	{
		// Token: 0x0600405B RID: 16475
		bool IsPartyWaitingForDisband(MobileParty party);
	}
}
