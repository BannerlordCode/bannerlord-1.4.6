using System;
using TaleWorlds.CampaignSystem.Party;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003DD RID: 989
	public class CharacterDevelopmentCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003CCF RID: 15567 RVA: 0x00103E8C File Offset: 0x0010208C
		public override void RegisterEvents()
		{
			CampaignEvents.DailyTickHeroEvent.AddNonSerializedListener(this, new Action<Hero>(this.DailyTickHero));
			CampaignEvents.OnCharacterCreationIsOverEvent.AddNonSerializedListener(this, new Action(this.OnCharacterCreationIsOver));
			CampaignEvents.OnHeroActivatedEvent.AddNonSerializedListener(this, new Action<Hero, Hero.CharacterStates>(this.OnHeroActivated));
		}

		// Token: 0x06003CD0 RID: 15568 RVA: 0x00103EDE File Offset: 0x001020DE
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06003CD1 RID: 15569 RVA: 0x00103EE0 File Offset: 0x001020E0
		private void DailyTickHero(Hero hero)
		{
			if (this.ShouldDevelopCharacterStats(hero))
			{
				hero.HeroDeveloper.DevelopCharacterStats();
			}
		}

		// Token: 0x06003CD2 RID: 15570 RVA: 0x00103EF8 File Offset: 0x001020F8
		private void OnCharacterCreationIsOver()
		{
			if (CampaignOptions.AutoAllocateClanMemberPerks)
			{
				foreach (Hero hero in Campaign.Current.AliveHeroes)
				{
					if (!hero.IsChild && hero.Clan == Clan.PlayerClan && hero != Hero.MainHero)
					{
						hero.HeroDeveloper.DevelopCharacterStats();
					}
				}
			}
		}

		// Token: 0x06003CD3 RID: 15571 RVA: 0x00103F78 File Offset: 0x00102178
		private void OnHeroActivated(Hero hero, Hero.CharacterStates previousState)
		{
			if (this.ShouldDevelopCharacterStats(hero))
			{
				hero.HeroDeveloper.DevelopCharacterStats();
			}
		}

		// Token: 0x06003CD4 RID: 15572 RVA: 0x00103F90 File Offset: 0x00102190
		private bool ShouldDevelopCharacterStats(Hero hero)
		{
			if (!hero.IsChild && hero.IsAlive && (hero.Clan != Clan.PlayerClan || (hero != Hero.MainHero && CampaignOptions.AutoAllocateClanMemberPerks)))
			{
				MobileParty partyBelongedTo = hero.PartyBelongedTo;
				return ((partyBelongedTo != null) ? partyBelongedTo.MapEvent : null) == null;
			}
			return false;
		}
	}
}
