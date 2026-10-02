using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x020004A9 RID: 1193
	public static class DestroyPartyAction
	{
		// Token: 0x06004A6A RID: 19050 RVA: 0x00178A94 File Offset: 0x00176C94
		private static void ApplyInternal(PartyBase destroyerParty, MobileParty destroyedParty)
		{
			if (destroyedParty != MobileParty.MainParty)
			{
				if (!destroyedParty.IsActive)
				{
					Debug.Print("Trying to destroy an inactive party with id: " + destroyedParty.StringId, 0, Debug.DebugColor.White, 17592186044416UL);
					Debug.FailedAssert("destroyedParty.IsActive", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Actions\\DestroyPartyAction.cs", "ApplyInternal", 17);
				}
				if (destroyedParty.IsCaravan && destroyedParty.Party.Owner != null && destroyedParty.Party.Owner.GetPerkValue(DefaultPerks.Trade.InsurancePlans))
				{
					GiveGoldAction.ApplyBetweenCharacters(null, destroyedParty.Party.Owner, (int)DefaultPerks.Trade.InsurancePlans.PrimaryBonus, false);
				}
				CampaignEventDispatcher.Instance.OnMobilePartyDestroyed(destroyedParty, destroyerParty);
				CampaignEventDispatcher.Instance.OnMapInteractableDestroyed(destroyedParty.Party);
				destroyedParty.RemoveParty();
			}
		}

		// Token: 0x06004A6B RID: 19051 RVA: 0x00178B56 File Offset: 0x00176D56
		public static void Apply(PartyBase destroyerParty, MobileParty destroyedParty)
		{
			DestroyPartyAction.ApplyInternal(destroyerParty, destroyedParty);
		}

		// Token: 0x06004A6C RID: 19052 RVA: 0x00178B5F File Offset: 0x00176D5F
		public static void ApplyForDisbanding(MobileParty disbandedParty, Settlement relatedSettlement)
		{
			if (disbandedParty.CurrentSettlement != null)
			{
				LeaveSettlementAction.ApplyForParty(disbandedParty);
			}
			CampaignEventDispatcher.Instance.OnPartyDisbanded(disbandedParty, relatedSettlement);
			DestroyPartyAction.ApplyInternal(null, disbandedParty);
		}
	}
}
