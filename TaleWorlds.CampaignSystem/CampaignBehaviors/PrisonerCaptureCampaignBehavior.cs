using System;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x02000436 RID: 1078
	public class PrisonerCaptureCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x0600451D RID: 17693 RVA: 0x00152FCC File Offset: 0x001511CC
		public override void RegisterEvents()
		{
			CampaignEvents.OnSettlementOwnerChangedEvent.AddNonSerializedListener(this, new Action<Settlement, bool, Hero, Hero, Hero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail>(this.OnSettlementOwnerChanged));
			CampaignEvents.WarDeclared.AddNonSerializedListener(this, new Action<IFaction, IFaction, DeclareWarAction.DeclareWarDetail>(this.OnWarDeclared));
			CampaignEvents.OnClanChangedKingdomEvent.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, ChangeKingdomAction.ChangeKingdomActionDetail, bool>(this.OnClanChangedKingdom));
		}

		// Token: 0x0600451E RID: 17694 RVA: 0x0015301E File Offset: 0x0015121E
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x0600451F RID: 17695 RVA: 0x00153020 File Offset: 0x00151220
		private void OnClanChangedKingdom(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail detail, bool showNotification)
		{
			for (int i = 0; i < clan.Settlements.Count; i++)
			{
				Settlement settlement = clan.Settlements[i];
				if (settlement.IsFortification)
				{
					this.HandleSettlementHeroes(settlement);
				}
			}
		}

		// Token: 0x06004520 RID: 17696 RVA: 0x00153060 File Offset: 0x00151260
		private void OnWarDeclared(IFaction faction1, IFaction faction2, DeclareWarAction.DeclareWarDetail detail)
		{
			for (int i = 0; i < faction1.Settlements.Count; i++)
			{
				Settlement settlement = faction1.Settlements[i];
				if (settlement.IsFortification)
				{
					this.HandleSettlementHeroes(settlement);
				}
			}
			for (int j = 0; j < faction2.Settlements.Count; j++)
			{
				Settlement settlement2 = faction2.Settlements[j];
				if (settlement2.IsFortification)
				{
					this.HandleSettlementHeroes(settlement2);
				}
			}
		}

		// Token: 0x06004521 RID: 17697 RVA: 0x001530D1 File Offset: 0x001512D1
		private void OnSettlementOwnerChanged(Settlement settlement, bool openToClaim, Hero newOwner, Hero oldOwner, Hero capturerHero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail detail)
		{
			if (settlement.IsFortification)
			{
				this.HandleSettlementHeroes(settlement);
			}
		}

		// Token: 0x06004522 RID: 17698 RVA: 0x001530E4 File Offset: 0x001512E4
		private void HandleSettlementHeroes(Settlement settlement)
		{
			for (int i = settlement.HeroesWithoutParty.Count - 1; i >= 0; i--)
			{
				Hero hero = settlement.HeroesWithoutParty[i];
				if (this.SettlementHeroCaptureCommonCondition(hero))
				{
					TakePrisonerAction.Apply(hero.CurrentSettlement.Party, hero);
				}
			}
			for (int j = settlement.Parties.Count - 1; j >= 0; j--)
			{
				MobileParty mobileParty = settlement.Parties[j];
				if (mobileParty.IsLordParty && (mobileParty.Army == null || (mobileParty.Army != null && mobileParty.Army.LeaderParty == mobileParty && !mobileParty.Army.Parties.Contains(MobileParty.MainParty))) && mobileParty.MapEvent == null && this.SettlementHeroCaptureCommonCondition(mobileParty.LeaderHero))
				{
					LeaveSettlementAction.ApplyForParty(mobileParty);
				}
			}
		}

		// Token: 0x06004523 RID: 17699 RVA: 0x001531B0 File Offset: 0x001513B0
		private bool SettlementHeroCaptureCommonCondition(Hero hero)
		{
			return hero != null && hero != Hero.MainHero && !hero.IsWanderer && !hero.IsNotable && hero.HeroState != Hero.CharacterStates.Prisoner && hero.HeroState != Hero.CharacterStates.Dead && hero.MapFaction != null && hero.CurrentSettlement != null && hero.MapFaction.IsAtWarWith(hero.CurrentSettlement.MapFaction);
		}
	}
}
