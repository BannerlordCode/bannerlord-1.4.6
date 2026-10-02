using System;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.GameComponents
{
	// Token: 0x0200013C RID: 316
	public class DefaultPartyTransitionModel : PartyTransitionModel
	{
		// Token: 0x06001982 RID: 6530 RVA: 0x0007FA92 File Offset: 0x0007DC92
		public override CampaignTime GetFleetTravelTimeToSettlement(MobileParty mobileParty, Settlement targetSettlement)
		{
			return CampaignTime.Never;
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x0007FA99 File Offset: 0x0007DC99
		public override CampaignTime GetTransitionTimeDisembarking(MobileParty mobileParty)
		{
			return CampaignTime.Never;
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x0007FAA0 File Offset: 0x0007DCA0
		public override CampaignTime GetTransitionTimeForEmbarking(MobileParty mobileParty)
		{
			return CampaignTime.Never;
		}
	}
}
