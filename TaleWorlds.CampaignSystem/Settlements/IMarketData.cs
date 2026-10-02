using System;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.Settlements
{
	// Token: 0x020003BF RID: 959
	public interface IMarketData
	{
		// Token: 0x0600391E RID: 14622
		int GetPrice(ItemObject item, MobileParty tradingParty, bool isSelling, PartyBase merchantParty);

		// Token: 0x0600391F RID: 14623
		int GetPrice(EquipmentElement itemRosterElement, MobileParty tradingParty, bool isSelling, PartyBase merchantParty);
	}
}
