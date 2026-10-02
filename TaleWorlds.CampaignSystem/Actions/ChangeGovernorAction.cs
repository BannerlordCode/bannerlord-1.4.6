using System;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x0200049A RID: 1178
	public static class ChangeGovernorAction
	{
		// Token: 0x06004A20 RID: 18976 RVA: 0x001772C8 File Offset: 0x001754C8
		private static void ApplyInternal(Town fortification, Hero governor)
		{
			Hero governor2 = fortification.Governor;
			if (governor == null)
			{
				fortification.Governor = null;
			}
			else if (governor.CurrentSettlement == fortification.Settlement && !governor.IsPrisoner)
			{
				fortification.Governor = governor;
				TeleportHeroAction.ApplyImmediateTeleportToSettlement(governor, fortification.Settlement);
			}
			else
			{
				fortification.Governor = null;
				TeleportHeroAction.ApplyDelayedTeleportToSettlementAsGovernor(governor, fortification.Settlement);
			}
			if (governor2 != null)
			{
				governor2.GovernorOf = null;
			}
			CampaignEventDispatcher.Instance.OnGovernorChanged(fortification, governor2, governor);
			if (governor != null)
			{
				CampaignEventDispatcher.Instance.OnHeroGetsBusy(governor, HeroGetsBusyReasons.BecomeGovernor);
			}
		}

		// Token: 0x06004A21 RID: 18977 RVA: 0x00177350 File Offset: 0x00175550
		private static void ApplyGiveUpInternal(Hero governor)
		{
			Town governorOf = governor.GovernorOf;
			governorOf.Governor = null;
			governor.GovernorOf = null;
			CampaignEventDispatcher.Instance.OnGovernorChanged(governorOf, governor, null);
		}

		// Token: 0x06004A22 RID: 18978 RVA: 0x0017737F File Offset: 0x0017557F
		public static void Apply(Town fortification, Hero governor)
		{
			ChangeGovernorAction.ApplyInternal(fortification, governor);
		}

		// Token: 0x06004A23 RID: 18979 RVA: 0x00177388 File Offset: 0x00175588
		public static void RemoveGovernorOf(Hero governor)
		{
			ChangeGovernorAction.ApplyGiveUpInternal(governor);
		}

		// Token: 0x06004A24 RID: 18980 RVA: 0x00177390 File Offset: 0x00175590
		public static void RemoveGovernorOfIfExists(Town town)
		{
			if (town.Governor != null)
			{
				ChangeGovernorAction.ApplyGiveUpInternal(town.Governor);
			}
		}
	}
}
