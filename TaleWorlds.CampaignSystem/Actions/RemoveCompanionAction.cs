using System;
using TaleWorlds.CampaignSystem.Party;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x020004C2 RID: 1218
	public static class RemoveCompanionAction
	{
		// Token: 0x06004AD6 RID: 19158 RVA: 0x0017AD50 File Offset: 0x00178F50
		private static void ApplyInternal(Clan clan, Hero companion, RemoveCompanionAction.RemoveCompanionDetail detail)
		{
			MobileParty partyBelongedTo = companion.PartyBelongedTo;
			PartyBase partyBase = ((partyBelongedTo != null) ? partyBelongedTo.Party : null);
			companion.CompanionOf = null;
			if (partyBase != null && partyBase.IsMobile && detail != RemoveCompanionAction.RemoveCompanionDetail.ByTurningToLord)
			{
				bool flag = partyBase.LeaderHero == companion;
				partyBase.MemberRoster.AddToCounts(companion.CharacterObject, -1, false, 0, 0, true, -1);
				if (flag)
				{
					partyBase.MobileParty.SetMoveModeHold();
					partyBase.MobileParty.Ai.RethinkAtNextHourlyTick = true;
					if (partyBase.MemberRoster.Count == 0)
					{
						DestroyPartyAction.Apply(null, partyBase.MobileParty);
					}
					else
					{
						DisbandPartyAction.StartDisband(partyBase.MobileParty);
					}
				}
			}
			if (detail == RemoveCompanionAction.RemoveCompanionDetail.Fire)
			{
				if (companion.PartyBelongedToAsPrisoner != null)
				{
					EndCaptivityAction.ApplyByEscape(companion, null, true);
				}
				else
				{
					MakeHeroFugitiveAction.Apply(companion, false);
				}
				if (companion.IsWanderer)
				{
					companion.ResetEquipments();
				}
			}
			if (companion.GovernorOf != null)
			{
				ChangeGovernorAction.RemoveGovernorOf(companion);
			}
			CampaignEventDispatcher.Instance.OnCompanionRemoved(companion, detail);
		}

		// Token: 0x06004AD7 RID: 19159 RVA: 0x0017AE2F File Offset: 0x0017902F
		public static void ApplyByFire(Clan clan, Hero companion)
		{
			RemoveCompanionAction.ApplyInternal(clan, companion, RemoveCompanionAction.RemoveCompanionDetail.Fire);
		}

		// Token: 0x06004AD8 RID: 19160 RVA: 0x0017AE39 File Offset: 0x00179039
		public static void ApplyAfterQuest(Clan clan, Hero companion)
		{
			RemoveCompanionAction.ApplyInternal(clan, companion, RemoveCompanionAction.RemoveCompanionDetail.AfterQuest);
		}

		// Token: 0x06004AD9 RID: 19161 RVA: 0x0017AE43 File Offset: 0x00179043
		public static void ApplyByDeath(Clan clan, Hero companion)
		{
			RemoveCompanionAction.ApplyInternal(clan, companion, RemoveCompanionAction.RemoveCompanionDetail.Death);
		}

		// Token: 0x06004ADA RID: 19162 RVA: 0x0017AE4D File Offset: 0x0017904D
		public static void ApplyByByTurningToLord(Clan clan, Hero companion)
		{
			RemoveCompanionAction.ApplyInternal(clan, companion, RemoveCompanionAction.RemoveCompanionDetail.ByTurningToLord);
		}

		// Token: 0x020008A0 RID: 2208
		public enum RemoveCompanionDetail
		{
			// Token: 0x040024DD RID: 9437
			Fire,
			// Token: 0x040024DE RID: 9438
			Death,
			// Token: 0x040024DF RID: 9439
			AfterQuest,
			// Token: 0x040024E0 RID: 9440
			ByTurningToLord
		}
	}
}
