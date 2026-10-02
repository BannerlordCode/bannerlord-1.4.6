using System;
using TaleWorlds.CampaignSystem.Party;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x02000408 RID: 1032
	public interface IParleyCampaignBehavior
	{
		// Token: 0x060040EF RID: 16623
		PartyBase GetParleyedParty();

		// Token: 0x060040F0 RID: 16624
		void StartParley(PartyBase partyBase);
	}
}
