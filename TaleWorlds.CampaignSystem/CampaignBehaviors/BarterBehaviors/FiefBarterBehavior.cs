using System;
using TaleWorlds.CampaignSystem.BarterSystem;
using TaleWorlds.CampaignSystem.BarterSystem.Barterables;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.BarterBehaviors
{
	// Token: 0x02000467 RID: 1127
	public class FiefBarterBehavior : CampaignBehaviorBase
	{
		// Token: 0x06004859 RID: 18521 RVA: 0x0016C73B File Offset: 0x0016A93B
		public override void RegisterEvents()
		{
			CampaignEvents.BarterablesRequested.AddNonSerializedListener(this, new Action<BarterData>(this.CheckForBarters));
		}

		// Token: 0x0600485A RID: 18522 RVA: 0x0016C754 File Offset: 0x0016A954
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x0600485B RID: 18523 RVA: 0x0016C758 File Offset: 0x0016A958
		public void CheckForBarters(BarterData args)
		{
			if (args.OffererHero != null && args.OtherHero != null && args.OffererHero.GetPerkValue(DefaultPerks.Trade.EverythingHasAPrice) && (!args.OtherHero.Clan.IsMinorFaction || args.OtherHero.Clan == Clan.PlayerClan) && !args.OtherHero.Clan.IsUnderMercenaryService && !args.OffererHero.Clan.IsUnderMercenaryService)
			{
				foreach (Town town in Town.AllFiefs)
				{
					Clan ownerClan = town.OwnerClan;
					if (((ownerClan != null) ? ownerClan.Leader : null) == args.OffererHero)
					{
						Barterable barterable = new FiefBarterable(town.Settlement, args.OffererHero, args.OtherHero);
						args.AddBarterable<FiefBarterGroup>(barterable, false);
					}
					else
					{
						Clan ownerClan2 = town.OwnerClan;
						if (((ownerClan2 != null) ? ownerClan2.Leader : null) == args.OtherHero)
						{
							Barterable barterable2 = new FiefBarterable(town.Settlement, args.OtherHero, args.OffererHero);
							args.AddBarterable<FiefBarterGroup>(barterable2, false);
						}
					}
				}
			}
		}
	}
}
