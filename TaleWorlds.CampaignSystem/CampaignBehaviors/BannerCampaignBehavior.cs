using System;
using System.Collections.Generic;
using Helpers;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003D3 RID: 979
	public class BannerCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003AD1 RID: 15057 RVA: 0x000F3FB4 File Offset: 0x000F21B4
		public override void RegisterEvents()
		{
			CampaignEvents.OnNewGameCreatedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnNewGameCreated));
			CampaignEvents.OnGameLoadFinishedEvent.AddNonSerializedListener(this, new Action(this.GiveBannersToHeroes));
			CampaignEvents.DailyTickHeroEvent.AddNonSerializedListener(this, new Action<Hero>(this.DailyTickHero));
			CampaignEvents.OnCollectLootsItemsEvent.AddNonSerializedListener(this, new Action<PartyBase, ItemRoster>(this.OnCollectLootItems));
			CampaignEvents.HeroComesOfAgeEvent.AddNonSerializedListener(this, new Action<Hero>(this.OnHeroComesOfAge));
			CampaignEvents.HeroCreated.AddNonSerializedListener(this, new Action<Hero, bool>(this.OnHeroCreated));
			CampaignEvents.OnClanCreatedEvent.AddNonSerializedListener(this, new Action<Clan, bool>(this.OnClanCreated));
		}

		// Token: 0x06003AD2 RID: 15058 RVA: 0x000F4062 File Offset: 0x000F2262
		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<Dictionary<Hero, CampaignTime>>("_heroNextBannerLootTime", ref this._heroNextBannerLootTime);
		}

		// Token: 0x06003AD3 RID: 15059 RVA: 0x000F4076 File Offset: 0x000F2276
		private void OnNewGameCreated(CampaignGameStarter campaignGameStarter)
		{
			this.GiveBannersToHeroes();
		}

		// Token: 0x06003AD4 RID: 15060 RVA: 0x000F4080 File Offset: 0x000F2280
		private void GiveBannersToHeroes()
		{
			foreach (Hero hero in Hero.AllAliveHeroes)
			{
				if (this.CanBannerBeGivenToHero(hero))
				{
					ItemObject randomBannerItemForHero = BannerHelper.GetRandomBannerItemForHero(hero);
					if (randomBannerItemForHero != null)
					{
						hero.BannerItem = new EquipmentElement(randomBannerItemForHero, null, null, false);
					}
				}
			}
		}

		// Token: 0x06003AD5 RID: 15061 RVA: 0x000F40F0 File Offset: 0x000F22F0
		private void DailyTickHero(Hero hero)
		{
			if (hero.Clan != Clan.PlayerClan)
			{
				EquipmentElement bannerItem = hero.BannerItem;
				BannerItemModel bannerItemModel = Campaign.Current.Models.BannerItemModel;
				if (!bannerItem.IsInvalid() && bannerItemModel.CanBannerBeUpdated(bannerItem.Item) && MBRandom.RandomFloat < 0.1f)
				{
					int bannerLevel = ((BannerComponent)bannerItem.Item.ItemComponent).BannerLevel;
					int bannerItemLevelForHero = bannerItemModel.GetBannerItemLevelForHero(hero);
					if (bannerLevel != bannerItemLevelForHero)
					{
						ItemObject upgradeBannerForHero = this.GetUpgradeBannerForHero(hero, bannerItemLevelForHero);
						if (upgradeBannerForHero != null)
						{
							hero.BannerItem = new EquipmentElement(upgradeBannerForHero, null, null, false);
							return;
						}
					}
				}
				else if (bannerItem.IsInvalid() && this.CanBannerBeGivenToHero(hero) && MBRandom.RandomFloat < 0.25f && !hero.IsPrisoner)
				{
					ItemObject randomBannerItemForHero = BannerHelper.GetRandomBannerItemForHero(hero);
					if (randomBannerItemForHero != null)
					{
						hero.BannerItem = new EquipmentElement(randomBannerItemForHero, null, null, false);
					}
				}
			}
		}

		// Token: 0x06003AD6 RID: 15062 RVA: 0x000F41C8 File Offset: 0x000F23C8
		private ItemObject GetUpgradeBannerForHero(Hero hero, int upgradeBannerLevel)
		{
			ItemObject item = hero.BannerItem.Item;
			foreach (ItemObject itemObject in Campaign.Current.Models.BannerItemModel.GetPossibleRewardBannerItems())
			{
				BannerComponent bannerComponent = (BannerComponent)itemObject.ItemComponent;
				if (itemObject.Culture == item.Culture && bannerComponent.BannerLevel == upgradeBannerLevel && bannerComponent.BannerEffect == ((BannerComponent)item.ItemComponent).BannerEffect)
				{
					return itemObject;
				}
			}
			return BannerHelper.GetRandomBannerItemForHero(hero);
		}

		// Token: 0x06003AD7 RID: 15063 RVA: 0x000F4278 File Offset: 0x000F2478
		private void OnCollectLootItems(PartyBase winnerParty, ItemRoster gainedLoots)
		{
			if (winnerParty == PartyBase.MainParty)
			{
				MapEvent mapEvent = MobileParty.MainParty.MapEvent;
				ItemObject bannerRewardForWinningMapEvent = Campaign.Current.Models.BattleRewardModel.GetBannerRewardForWinningMapEvent(mapEvent);
				if (bannerRewardForWinningMapEvent != null)
				{
					gainedLoots.AddToCounts(bannerRewardForWinningMapEvent, 1);
				}
				Hero hero = null;
				MBReadOnlyList<MapEventParty> mbreadOnlyList = mapEvent.PartiesOnSide(mapEvent.DefeatedSide);
				if (mbreadOnlyList.Exists((MapEventParty x) => x.Party.IsMobile && x.Party.MobileParty.Army != null))
				{
					foreach (MapEventParty mapEventParty in mbreadOnlyList)
					{
						if (mapEventParty.Party.IsMobile && mapEventParty.Party.MobileParty.Army != null && !mapEventParty.Party.MobileParty.Army.ArmyOwner.BannerItem.IsInvalid() && this.CanBannerBeLootedFromHero(mapEventParty.Party.MobileParty.Army.ArmyOwner))
						{
							hero = mapEventParty.Party.MobileParty.Army.ArmyOwner;
							break;
						}
					}
				}
				if (hero == null)
				{
					MapEventParty randomElementWithPredicate = mbreadOnlyList.GetRandomElementWithPredicate<MapEventParty>((MapEventParty x) => x.Party.LeaderHero != null && !x.Party.LeaderHero.BannerItem.IsInvalid() && this.CanBannerBeLootedFromHero(x.Party.LeaderHero));
					hero = ((randomElementWithPredicate != null) ? randomElementWithPredicate.Party.LeaderHero : null);
				}
				if (hero != null)
				{
					float bannerLootChanceFromDefeatedHero = Campaign.Current.Models.BattleRewardModel.GetBannerLootChanceFromDefeatedHero(hero);
					if (MBRandom.RandomFloat <= bannerLootChanceFromDefeatedHero)
					{
						this.LogBannerLootForHero(hero, ((BannerComponent)hero.BannerItem.Item.ItemComponent).BannerLevel);
						gainedLoots.AddToCounts(hero.BannerItem.Item, 1);
						hero.BannerItem = new EquipmentElement(null, null, null, false);
					}
				}
			}
		}

		// Token: 0x06003AD8 RID: 15064 RVA: 0x000F4450 File Offset: 0x000F2650
		private void OnHeroComesOfAge(Hero hero)
		{
			if (this.CanBannerBeGivenToHero(hero))
			{
				ItemObject randomBannerItemForHero = BannerHelper.GetRandomBannerItemForHero(hero);
				if (randomBannerItemForHero != null)
				{
					hero.BannerItem = new EquipmentElement(randomBannerItemForHero, null, null, false);
				}
			}
		}

		// Token: 0x06003AD9 RID: 15065 RVA: 0x000F4480 File Offset: 0x000F2680
		private void OnHeroCreated(Hero hero, bool isBornNaturally = false)
		{
			if (this.CanBannerBeGivenToHero(hero))
			{
				ItemObject randomBannerItemForHero = BannerHelper.GetRandomBannerItemForHero(hero);
				if (randomBannerItemForHero != null)
				{
					hero.BannerItem = new EquipmentElement(randomBannerItemForHero, null, null, false);
				}
			}
		}

		// Token: 0x06003ADA RID: 15066 RVA: 0x000F44B0 File Offset: 0x000F26B0
		private void OnClanCreated(Clan clan, bool isCompanion)
		{
			if (isCompanion)
			{
				Hero leader = clan.Leader;
				if (leader.BannerItem.IsInvalid())
				{
					ItemObject randomBannerItemForHero = BannerHelper.GetRandomBannerItemForHero(leader);
					if (randomBannerItemForHero != null)
					{
						leader.BannerItem = new EquipmentElement(randomBannerItemForHero, null, null, false);
					}
				}
			}
		}

		// Token: 0x06003ADB RID: 15067 RVA: 0x000F44F0 File Offset: 0x000F26F0
		private bool CanBannerBeLootedFromHero(Hero hero)
		{
			return !this._heroNextBannerLootTime.ContainsKey(hero) || this._heroNextBannerLootTime[hero].IsPast;
		}

		// Token: 0x06003ADC RID: 15068 RVA: 0x000F4521 File Offset: 0x000F2721
		private int GetCooldownDays(int bannerLevel)
		{
			if (bannerLevel == 1)
			{
				return 4;
			}
			if (bannerLevel == 1)
			{
				return 8;
			}
			return 12;
		}

		// Token: 0x06003ADD RID: 15069 RVA: 0x000F4534 File Offset: 0x000F2734
		private void LogBannerLootForHero(Hero hero, int bannerLevel)
		{
			CampaignTime campaignTime = CampaignTime.DaysFromNow((float)this.GetCooldownDays(bannerLevel));
			if (!this._heroNextBannerLootTime.ContainsKey(hero))
			{
				this._heroNextBannerLootTime.Add(hero, campaignTime);
				return;
			}
			this._heroNextBannerLootTime[hero] = campaignTime;
		}

		// Token: 0x06003ADE RID: 15070 RVA: 0x000F4578 File Offset: 0x000F2778
		private bool CanBannerBeGivenToHero(Hero hero)
		{
			int heroComesOfAge = Campaign.Current.Models.AgeModel.HeroComesOfAge;
			return hero.Occupation == Occupation.Lord && hero.Age >= (float)heroComesOfAge && hero.BannerItem.IsInvalid() && hero.Clan != Clan.PlayerClan;
		}

		// Token: 0x04001245 RID: 4677
		private const int BannerLevel1CooldownDays = 4;

		// Token: 0x04001246 RID: 4678
		private const int BannerLevel2CooldownDays = 8;

		// Token: 0x04001247 RID: 4679
		private const int BannerLevel3CooldownDays = 12;

		// Token: 0x04001248 RID: 4680
		private const float BannerItemUpdateChance = 0.1f;

		// Token: 0x04001249 RID: 4681
		private const float GiveBannerItemChance = 0.25f;

		// Token: 0x0400124A RID: 4682
		private Dictionary<Hero, CampaignTime> _heroNextBannerLootTime = new Dictionary<Hero, CampaignTime>();
	}
}
