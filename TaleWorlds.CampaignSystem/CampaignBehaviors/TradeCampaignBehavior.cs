using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x02000448 RID: 1096
	public class TradeCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x06004683 RID: 18051 RVA: 0x00160786 File Offset: 0x0015E986
		public void OnNewGameCreated(CampaignGameStarter campaignGameStarter)
		{
			this.InitializeMarkets();
		}

		// Token: 0x06004684 RID: 18052 RVA: 0x00160790 File Offset: 0x0015E990
		public override void RegisterEvents()
		{
			CampaignEvents.DailyTickTownEvent.AddNonSerializedListener(this, new Action<Town>(this.DailyTickTown));
			CampaignEvents.OnNewGameCreatedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnNewGameCreated));
			CampaignEvents.OnNewGameCreatedPartialFollowUpEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter, int>(this.OnNewGameCreatedPartialFollowUp));
			CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnSessionLaunched));
		}

		// Token: 0x06004685 RID: 18053 RVA: 0x001607FC File Offset: 0x0015E9FC
		private void OnNewGameCreatedPartialFollowUp(CampaignGameStarter campaignGameStarter, int i)
		{
			if (i == 2)
			{
				this.InitializeTrade();
			}
			if (i % 10 == 0)
			{
				foreach (Town town in Campaign.Current.AllTowns)
				{
					this.UpdateMarketStores(town);
				}
			}
		}

		// Token: 0x06004686 RID: 18054 RVA: 0x00160864 File Offset: 0x0015EA64
		private void OnSessionLaunched(CampaignGameStarter campaignGameStarter)
		{
			foreach (Town town in Town.AllTowns)
			{
				this.UpdateMarketStores(town);
			}
		}

		// Token: 0x06004687 RID: 18055 RVA: 0x001608B8 File Offset: 0x0015EAB8
		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<Dictionary<ItemCategory, float>>("_numberOfTotalItemsAtGameWorld", ref this._numberOfTotalItemsAtGameWorld);
		}

		// Token: 0x06004688 RID: 18056 RVA: 0x001608CC File Offset: 0x0015EACC
		private void InitializeTrade()
		{
			this._numberOfTotalItemsAtGameWorld = new Dictionary<ItemCategory, float>();
			Campaign.Current.Settlements.Where<Settlement>((Settlement settlement) => settlement.IsTown).ToList<Settlement>();
			foreach (Hero hero in Hero.AllAliveHeroes)
			{
				if (hero.CharacterObject.Occupation == Occupation.Lord && hero.Clan != Clan.PlayerClan)
				{
					Clan clan = hero.Clan;
					int num;
					if (((clan != null) ? clan.Leader : null) == hero)
					{
						num = 50000 + 10000 * hero.Clan.Tier + ((hero == hero.MapFaction.Leader) ? 50000 : 0);
					}
					else
					{
						num = 10000;
					}
					GiveGoldAction.ApplyBetweenCharacters(null, hero, num, false);
				}
			}
		}

		// Token: 0x06004689 RID: 18057 RVA: 0x001609CC File Offset: 0x0015EBCC
		public void DailyTickTown(Town town)
		{
			this.UpdateMarketStores(town);
		}

		// Token: 0x0600468A RID: 18058 RVA: 0x001609D5 File Offset: 0x0015EBD5
		private void UpdateMarketStores(Town town)
		{
			town.MarketData.UpdateStores();
		}

		// Token: 0x0600468B RID: 18059 RVA: 0x001609E4 File Offset: 0x0015EBE4
		private void InitializeMarkets()
		{
			foreach (Town town in Town.AllTowns)
			{
				foreach (ItemCategory itemCategory in ItemCategories.All)
				{
					if (itemCategory.IsValid)
					{
						town.MarketData.AddDemand(itemCategory, 3f);
						town.MarketData.AddSupply(itemCategory, 2f);
					}
				}
			}
		}

		// Token: 0x040013BD RID: 5053
		private Dictionary<ItemCategory, float> _numberOfTotalItemsAtGameWorld;

		// Token: 0x040013BE RID: 5054
		public const float MaximumTaxRatioForVillages = 1f;

		// Token: 0x040013BF RID: 5055
		public const float MaximumTaxRatioForTowns = 0.5f;

		// Token: 0x0200085F RID: 2143
		public enum TradeGoodType
		{
			// Token: 0x040023F4 RID: 9204
			Grain,
			// Token: 0x040023F5 RID: 9205
			Wood,
			// Token: 0x040023F6 RID: 9206
			Meat,
			// Token: 0x040023F7 RID: 9207
			Wool,
			// Token: 0x040023F8 RID: 9208
			Cheese,
			// Token: 0x040023F9 RID: 9209
			Iron,
			// Token: 0x040023FA RID: 9210
			Salt,
			// Token: 0x040023FB RID: 9211
			Spice,
			// Token: 0x040023FC RID: 9212
			Raw_Silk,
			// Token: 0x040023FD RID: 9213
			Fish,
			// Token: 0x040023FE RID: 9214
			Flax,
			// Token: 0x040023FF RID: 9215
			Grape,
			// Token: 0x04002400 RID: 9216
			Hides,
			// Token: 0x04002401 RID: 9217
			Clay,
			// Token: 0x04002402 RID: 9218
			Date_Fruit,
			// Token: 0x04002403 RID: 9219
			Bread,
			// Token: 0x04002404 RID: 9220
			Beer,
			// Token: 0x04002405 RID: 9221
			Wine,
			// Token: 0x04002406 RID: 9222
			Tools,
			// Token: 0x04002407 RID: 9223
			Pottery,
			// Token: 0x04002408 RID: 9224
			Cloth,
			// Token: 0x04002409 RID: 9225
			Linen,
			// Token: 0x0400240A RID: 9226
			Leather,
			// Token: 0x0400240B RID: 9227
			Velvet,
			// Token: 0x0400240C RID: 9228
			Saddle_Horse,
			// Token: 0x0400240D RID: 9229
			Steppe_Horse,
			// Token: 0x0400240E RID: 9230
			Hunter,
			// Token: 0x0400240F RID: 9231
			Desert_Horse,
			// Token: 0x04002410 RID: 9232
			Charger,
			// Token: 0x04002411 RID: 9233
			War_Horse,
			// Token: 0x04002412 RID: 9234
			Steppe_Charger,
			// Token: 0x04002413 RID: 9235
			Desert_War_Horse,
			// Token: 0x04002414 RID: 9236
			Unknown,
			// Token: 0x04002415 RID: 9237
			NumberOfTradeItems
		}
	}
}
