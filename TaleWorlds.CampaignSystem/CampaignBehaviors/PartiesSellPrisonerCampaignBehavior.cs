using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x02000426 RID: 1062
	public class PartiesSellPrisonerCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x060043A9 RID: 17321 RVA: 0x0014899A File Offset: 0x00146B9A
		public override void RegisterEvents()
		{
			CampaignEvents.SettlementEntered.AddNonSerializedListener(this, new Action<MobileParty, Settlement, Hero>(this.OnSettlementEntered));
			CampaignEvents.DailyTickSettlementEvent.AddNonSerializedListener(this, new Action<Settlement>(this.DailyTickSettlement));
		}

		// Token: 0x060043AA RID: 17322 RVA: 0x001489CA File Offset: 0x00146BCA
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x060043AB RID: 17323 RVA: 0x001489CC File Offset: 0x00146BCC
		private void OnSettlementEntered(MobileParty mobileParty, Settlement settlement, Hero hero)
		{
			if (mobileParty != null && !mobileParty.IsMainParty && settlement.IsFortification && mobileParty.MapFaction != null && !mobileParty.IsDisbanding && !mobileParty.MapFaction.IsAtWarWith(settlement.MapFaction) && (mobileParty.PrisonRoster.TotalRegulars > 0 || (mobileParty.PrisonRoster.TotalHeroes > 0 && mobileParty.PrisonRoster.GetTroopRoster().Exists((TroopRosterElement x) => x.Character != CharacterObject.PlayerCharacter && x.Character.HeroObject.MapFaction.IsAtWarWith(settlement.MapFaction)))))
			{
				TroopRoster troopRoster = TroopRoster.CreateDummyTroopRoster();
				foreach (TroopRosterElement troopRosterElement in mobileParty.PrisonRoster.GetTroopRoster())
				{
					if (!troopRosterElement.Character.IsHero || (!troopRosterElement.Character.IsPlayerCharacter && troopRosterElement.Character.HeroObject.MapFaction.IsAtWarWith(settlement.MapFaction)))
					{
						troopRoster.Add(troopRosterElement);
					}
				}
				SellPrisonersAction.ApplyForSelectedPrisoners(mobileParty.Party, settlement.Party, troopRoster);
			}
		}

		// Token: 0x060043AC RID: 17324 RVA: 0x00148B1C File Offset: 0x00146D1C
		private void DailyTickSettlement(Settlement settlement)
		{
			if (settlement.IsFortification)
			{
				TroopRoster prisonRoster = settlement.Party.PrisonRoster;
				if (prisonRoster.TotalRegulars > 0)
				{
					int num = ((settlement.Owner == Hero.MainHero) ? (prisonRoster.TotalManCount - settlement.Party.PrisonerSizeLimit) : MBRandom.RoundRandomized((float)prisonRoster.TotalRegulars * 0.1f));
					if (num > 0)
					{
						TroopRoster troopRoster = TroopRoster.CreateDummyTroopRoster();
						IEnumerable<TroopRosterElement> enumerable;
						if (settlement.Owner != Hero.MainHero)
						{
							enumerable = prisonRoster.GetTroopRoster().AsEnumerable<TroopRosterElement>();
						}
						else
						{
							IEnumerable<TroopRosterElement> enumerable2 = from t in prisonRoster.GetTroopRoster()
								orderby t.Character.Tier
								select t;
							enumerable = enumerable2;
						}
						foreach (TroopRosterElement troopRosterElement in enumerable)
						{
							if (!troopRosterElement.Character.IsHero)
							{
								int num2 = Math.Min(num, troopRosterElement.Number);
								num -= num2;
								troopRoster.AddToCounts(troopRosterElement.Character, num2, false, 0, 0, true, -1);
								if (num <= 0)
								{
									break;
								}
							}
						}
						if (troopRoster.TotalManCount > 0)
						{
							SellPrisonersAction.ApplyForSelectedPrisoners(settlement.Party, null, troopRoster);
						}
					}
				}
			}
		}
	}
}
