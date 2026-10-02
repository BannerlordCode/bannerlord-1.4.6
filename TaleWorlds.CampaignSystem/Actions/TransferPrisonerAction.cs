using System;
using TaleWorlds.CampaignSystem.Party;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x020004CD RID: 1229
	public static class TransferPrisonerAction
	{
		// Token: 0x06004B07 RID: 19207 RVA: 0x0017C396 File Offset: 0x0017A596
		private static void ApplyInternal(CharacterObject prisonerTroop, PartyBase prisonerOwnerParty, PartyBase newParty)
		{
			if (prisonerTroop.HeroObject == Hero.MainHero)
			{
				PlayerCaptivity.CaptorParty = newParty;
				return;
			}
			prisonerOwnerParty.PrisonRoster.AddToCounts(prisonerTroop, -1, false, 0, 0, true, -1);
			newParty.AddPrisoner(prisonerTroop, 1);
		}

		// Token: 0x06004B08 RID: 19208 RVA: 0x0017C3C8 File Offset: 0x0017A5C8
		public static void Apply(CharacterObject prisonerTroop, PartyBase prisonerOwnerParty, PartyBase newParty)
		{
			TransferPrisonerAction.ApplyInternal(prisonerTroop, prisonerOwnerParty, newParty);
		}
	}
}
