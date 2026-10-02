using System;
using System.Linq;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Election;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x02000440 RID: 1088
	public class SettlementClaimantCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x060045F3 RID: 17907 RVA: 0x0015BE44 File Offset: 0x0015A044
		public override void RegisterEvents()
		{
			CampaignEvents.OnSettlementOwnerChangedEvent.AddNonSerializedListener(this, new Action<Settlement, bool, Hero, Hero, Hero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail>(this.OnSettlementOwnerChanged));
			CampaignEvents.DailyTickSettlementEvent.AddNonSerializedListener(this, new Action<Settlement>(this.DailyTickSettlement));
		}

		// Token: 0x060045F4 RID: 17908 RVA: 0x0015BE74 File Offset: 0x0015A074
		private void DailyTickSettlement(Settlement settlement)
		{
			if (settlement.Town != null && settlement.Town.IsOwnerUnassigned && settlement.OwnerClan != null && settlement.OwnerClan.Kingdom != null)
			{
				Kingdom kingdom = settlement.OwnerClan.Kingdom;
				if (kingdom.UnresolvedDecisions.FirstOrDefault<KingdomDecision>((KingdomDecision x) => x is SettlementClaimantDecision) == null)
				{
					kingdom.AddDecision(new SettlementClaimantDecision(kingdom.RulingClan, settlement, null, null), true);
				}
			}
		}

		// Token: 0x060045F5 RID: 17909 RVA: 0x0015BEF8 File Offset: 0x0015A0F8
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x060045F6 RID: 17910 RVA: 0x0015BEFC File Offset: 0x0015A0FC
		public void OnSettlementOwnerChanged(Settlement settlement, bool openToClaim, Hero newOwner, Hero oldOwner, Hero capturerHero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail detail)
		{
			if (settlement.IsVillage && settlement.Party.MapEvent != null && !FactionManager.IsAtWarAgainstFaction(settlement.Party.MapEvent.AttackerSide.LeaderParty.MapFaction, newOwner.MapFaction))
			{
				settlement.Party.MapEvent.FinalizeEvent();
			}
			if (openToClaim && newOwner.MapFaction.IsKingdomFaction && (newOwner.MapFaction as Kingdom).Clans.Count > 1 && settlement.Town != null)
			{
				settlement.Town.IsOwnerUnassigned = true;
			}
			foreach (Kingdom kingdom in Kingdom.All)
			{
				foreach (KingdomDecision kingdomDecision in kingdom.UnresolvedDecisions.ToList<KingdomDecision>())
				{
					SettlementClaimantDecision settlementClaimantDecision;
					SettlementClaimantPreliminaryDecision settlementClaimantPreliminaryDecision;
					if ((settlementClaimantDecision = kingdomDecision as SettlementClaimantDecision) != null)
					{
						if (settlementClaimantDecision.Settlement == settlement)
						{
							kingdom.RemoveDecision(kingdomDecision);
						}
					}
					else if ((settlementClaimantPreliminaryDecision = kingdomDecision as SettlementClaimantPreliminaryDecision) != null && settlementClaimantPreliminaryDecision.Settlement == settlement && settlementClaimantPreliminaryDecision.Settlement == settlement)
					{
						kingdom.RemoveDecision(kingdomDecision);
					}
				}
			}
			if (oldOwner.Clan == Clan.PlayerClan && (newOwner == null || newOwner.Clan != Clan.PlayerClan))
			{
				foreach (ItemRosterElement itemRosterElement in settlement.Stash)
				{
					settlement.ItemRoster.AddToCounts(itemRosterElement.EquipmentElement.Item, itemRosterElement.Amount);
				}
				settlement.Stash.Clear();
			}
		}
	}
}
