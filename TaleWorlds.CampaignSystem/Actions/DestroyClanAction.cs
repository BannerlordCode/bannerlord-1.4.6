using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Party.PartyComponents;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x020004A7 RID: 1191
	public static class DestroyClanAction
	{
		// Token: 0x06004A63 RID: 19043 RVA: 0x001787AC File Offset: 0x001769AC
		private static void ApplyInternal(Clan destroyedClan, DestroyClanAction.DestroyClanActionDetails details)
		{
			CampaignEventDispatcher.Instance.OnClanDestroyed(destroyedClan);
			destroyedClan.DeactivateClan();
			foreach (WarPartyComponent warPartyComponent in destroyedClan.WarPartyComponents.ToList<WarPartyComponent>())
			{
				PartyBase partyBase = null;
				if (warPartyComponent.MobileParty.MapEvent != null)
				{
					partyBase = warPartyComponent.MobileParty.MapEventSide.OtherSide.LeaderParty;
					if (warPartyComponent.MobileParty.MapEvent != MobileParty.MainParty.MapEvent)
					{
						warPartyComponent.MobileParty.MapEventSide = null;
					}
				}
				DestroyPartyAction.Apply(partyBase, warPartyComponent.MobileParty);
			}
			List<Hero> list = destroyedClan.Heroes.Where<Hero>((Hero x) => x.IsAlive).ToList<Hero>();
			for (int i = 0; i < list.Count; i++)
			{
				Hero hero = list[i];
				if (details != DestroyClanAction.DestroyClanActionDetails.ClanLeaderDeath || hero != destroyedClan.Leader)
				{
					KillCharacterAction.ApplyByRemove(hero, false, true);
				}
			}
			if (details != DestroyClanAction.DestroyClanActionDetails.ClanLeaderDeath && destroyedClan.Leader != null && destroyedClan.Leader.IsAlive && destroyedClan.Leader.DeathMark == KillCharacterAction.KillCharacterActionDetail.None)
			{
				KillCharacterAction.ApplyByRemove(destroyedClan.Leader, false, true);
			}
			if (!destroyedClan.Settlements.IsEmpty<Settlement>())
			{
				Clan clan = FactionHelper.ChooseHeirClanForFiefs(destroyedClan);
				foreach (Settlement settlement in destroyedClan.Settlements.ToList<Settlement>())
				{
					if (settlement.IsTown || settlement.IsCastle)
					{
						Hero randomElementWithPredicate = clan.AliveLords.GetRandomElementWithPredicate<Hero>((Hero x) => !x.IsChild);
						ChangeOwnerOfSettlementAction.ApplyByDestroyClan(settlement, randomElementWithPredicate);
					}
				}
			}
			FactionManager.Instance.RemoveFactionsFromCampaignWars(destroyedClan);
			if (destroyedClan.Kingdom != null)
			{
				ChangeKingdomAction.ApplyByLeaveKingdomByClanDestruction(destroyedClan, true);
			}
			if (destroyedClan.IsRebelClan)
			{
				Campaign.Current.CampaignObjectManager.RemoveClan(destroyedClan);
			}
		}

		// Token: 0x06004A64 RID: 19044 RVA: 0x001789D0 File Offset: 0x00176BD0
		public static void Apply(Clan destroyedClan)
		{
			DestroyClanAction.ApplyInternal(destroyedClan, DestroyClanAction.DestroyClanActionDetails.Default);
		}

		// Token: 0x06004A65 RID: 19045 RVA: 0x001789D9 File Offset: 0x00176BD9
		public static void ApplyByFailedRebellion(Clan failedRebellionClan)
		{
			DestroyClanAction.ApplyInternal(failedRebellionClan, DestroyClanAction.DestroyClanActionDetails.RebellionFailure);
		}

		// Token: 0x06004A66 RID: 19046 RVA: 0x001789E2 File Offset: 0x00176BE2
		public static void ApplyByClanLeaderDeath(Clan destroyedClan)
		{
			DestroyClanAction.ApplyInternal(destroyedClan, DestroyClanAction.DestroyClanActionDetails.ClanLeaderDeath);
		}

		// Token: 0x02000893 RID: 2195
		private enum DestroyClanActionDetails
		{
			// Token: 0x040024A6 RID: 9382
			Default,
			// Token: 0x040024A7 RID: 9383
			RebellionFailure,
			// Token: 0x040024A8 RID: 9384
			ClanLeaderDeath
		}
	}
}
