using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Workshops;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x02000449 RID: 1097
	public class TradeRumorsCampaignBehavior : CampaignBehaviorBase, ITradeRumorCampaignBehavior, ICampaignBehavior
	{
		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x0600468D RID: 18061 RVA: 0x00160A9C File Offset: 0x0015EC9C
		public IEnumerable<TradeRumor> TradeRumors
		{
			get
			{
				foreach (TradeRumor tradeRumor in this._tradeRumors)
				{
					if (!tradeRumor.IsExpired())
					{
						yield return tradeRumor;
					}
				}
				List<TradeRumor>.Enumerator enumerator = default(List<TradeRumor>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x0600468E RID: 18062 RVA: 0x00160AAC File Offset: 0x0015ECAC
		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<Dictionary<Settlement, CampaignTime>>("_enteredSettlements", ref this._enteredSettlements);
			dataStore.SyncData<List<TradeRumor>>("_tradeRumors", ref this._tradeRumors);
		}

		// Token: 0x0600468F RID: 18063 RVA: 0x00160AD4 File Offset: 0x0015ECD4
		public override void RegisterEvents()
		{
			CampaignEvents.SettlementEntered.AddNonSerializedListener(this, new Action<MobileParty, Settlement, Hero>(this.OnSettlementEntered));
			CampaignEvents.DailyTickEvent.AddNonSerializedListener(this, new Action(this.DailyTick));
			CampaignEvents.OnNewGameCreatedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnNewGameCreated));
			CampaignEvents.OnTradeRumorIsTakenEvent.AddNonSerializedListener(this, new Action<List<TradeRumor>, Settlement>(this.OnTradeRumorIsTaken));
		}

		// Token: 0x06004690 RID: 18064 RVA: 0x00160B3D File Offset: 0x0015ED3D
		public void OnTradeRumorIsTaken(List<TradeRumor> newRumors, Settlement sourceSettlement = null)
		{
			this.AddTradeRumors(newRumors, sourceSettlement);
		}

		// Token: 0x06004691 RID: 18065 RVA: 0x00160B48 File Offset: 0x0015ED48
		public void AddTradeRumors(List<TradeRumor> newRumors, Settlement sourceSettlement = null)
		{
			bool flag = true;
			foreach (TradeRumor tradeRumor in newRumors)
			{
				foreach (TradeRumor tradeRumor2 in this.TradeRumors)
				{
					if (tradeRumor2.Settlement == tradeRumor.Settlement && tradeRumor2.ItemCategory == tradeRumor.ItemCategory)
					{
						flag = false;
					}
				}
				if (flag)
				{
					this._tradeRumors.Add(tradeRumor);
				}
			}
		}

		// Token: 0x06004692 RID: 18066 RVA: 0x00160BF8 File Offset: 0x0015EDF8
		private void OnNewGameCreated(CampaignGameStarter starter)
		{
		}

		// Token: 0x06004693 RID: 18067 RVA: 0x00160BFA File Offset: 0x0015EDFA
		public void DailyTick()
		{
			this.AddDailyTradeRumors(1);
			this.DeleteExpiredRumors();
			this.DeleteExpiredEnteredSettlements();
		}

		// Token: 0x06004694 RID: 18068 RVA: 0x00160C10 File Offset: 0x0015EE10
		private void DeleteExpiredEnteredSettlements()
		{
			List<Settlement> list = new List<Settlement>();
			foreach (KeyValuePair<Settlement, CampaignTime> keyValuePair in this._enteredSettlements)
			{
				if (CampaignTime.Now - keyValuePair.Value >= CampaignTime.Days(1f))
				{
					list.Add(keyValuePair.Key);
				}
			}
			foreach (Settlement settlement in list)
			{
				this._enteredSettlements.Remove(settlement);
			}
		}

		// Token: 0x06004695 RID: 18069 RVA: 0x00160CD8 File Offset: 0x0015EED8
		public void OnSettlementEntered(MobileParty mobileParty, Settlement settlement, Hero hero)
		{
			if (mobileParty == null || (!mobileParty.IsMainParty && (!mobileParty.IsCaravan || mobileParty.Party.Owner == null || mobileParty.Party.Owner.Clan != Clan.PlayerClan || !Hero.MainHero.GetPerkValue(DefaultPerks.Trade.TravelingRumors))) || !settlement.IsTown)
			{
				return;
			}
			Town town = settlement.Town;
			if (((town != null) ? town.MarketData : null) == null)
			{
				return;
			}
			if (!this._enteredSettlements.ContainsKey(settlement) || (this._enteredSettlements.ContainsKey(settlement) && CampaignTime.Now - this._enteredSettlements[settlement] >= CampaignTime.Days(1f)))
			{
				List<TradeRumor> list = new List<TradeRumor>();
				IEnumerable<TradeRumor> tradeRumors = this._tradeRumors;
				Func<TradeRumor, bool> <>9__0;
				Func<TradeRumor, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (TradeRumor x) => x.Settlement == settlement);
				}
				foreach (TradeRumor tradeRumor in tradeRumors.Where<TradeRumor>(func))
				{
					list.Add(tradeRumor);
				}
				foreach (TradeRumor tradeRumor2 in list)
				{
					this._tradeRumors.Remove(tradeRumor2);
				}
				List<TradeRumor> list2 = new List<TradeRumor>();
				foreach (ItemObject itemObject in Items.AllTradeGoods)
				{
					list2.Add(new TradeRumor(settlement, itemObject, settlement.Town.GetItemPrice(itemObject, null, false), settlement.Town.GetItemPrice(itemObject, null, true), 10));
				}
				this.AddTradeRumors(list2, settlement);
				if (!this._enteredSettlements.ContainsKey(settlement))
				{
					this._enteredSettlements.Add(settlement, CampaignTime.Now);
					return;
				}
				this._enteredSettlements[settlement] = CampaignTime.Now;
			}
		}

		// Token: 0x06004696 RID: 18070 RVA: 0x00160F44 File Offset: 0x0015F144
		public void DeleteExpiredRumors()
		{
			List<TradeRumor> list = new List<TradeRumor>();
			foreach (TradeRumor tradeRumor in this._tradeRumors.Where<TradeRumor>((TradeRumor x) => x.IsExpired()))
			{
				list.Add(tradeRumor);
			}
			foreach (TradeRumor tradeRumor2 in list)
			{
				this._tradeRumors.Remove(tradeRumor2);
			}
		}

		// Token: 0x06004697 RID: 18071 RVA: 0x00161000 File Offset: 0x0015F200
		public void AddDailyTradeRumors(int numberOfTradeRumors)
		{
			int num = 0;
			foreach (ItemObject itemObject in Items.All)
			{
				if (itemObject.Type == ItemObject.ItemTypeEnum.Goods || itemObject.Type == ItemObject.ItemTypeEnum.Horse)
				{
					num++;
				}
			}
			int count = Campaign.Current.AllTowns.Count;
			List<TradeRumor> list = new List<TradeRumor>();
			for (int i = 0; i < numberOfTradeRumors; i++)
			{
				int num2 = MBRandom.RandomInt(count);
				int num3 = MBRandom.RandomInt(num);
				foreach (Town town in Campaign.Current.AllTowns)
				{
					num2--;
					if (num2 < 0)
					{
						using (List<ItemObject>.Enumerator enumerator = Items.All.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								ItemObject itemObject2 = enumerator.Current;
								if (itemObject2.Type == ItemObject.ItemTypeEnum.Goods || itemObject2.Type == ItemObject.ItemTypeEnum.Horse)
								{
									num3--;
									if (num3 < 0)
									{
										list.Add(new TradeRumor(town.Settlement, itemObject2, town.GetItemPrice(itemObject2, null, false), town.GetItemPrice(itemObject2, null, true), 10 + MBRandom.RandomInt(10)));
										break;
									}
								}
							}
							break;
						}
					}
				}
				if (Hero.MainHero.GetPerkValue(DefaultPerks.Trade.Tollgates))
				{
					foreach (Workshop workshop in Hero.MainHero.OwnedWorkshops)
					{
						foreach (ItemObject itemObject3 in Items.AllTradeGoods)
						{
							list.Add(new TradeRumor(workshop.Settlement, itemObject3, workshop.Settlement.Town.GetItemPrice(itemObject3, null, false), workshop.Settlement.Town.GetItemPrice(itemObject3, null, true), 10));
						}
					}
				}
			}
			this.AddTradeRumors(list, null);
		}

		// Token: 0x040013C0 RID: 5056
		private List<TradeRumor> _tradeRumors = new List<TradeRumor>();

		// Token: 0x040013C1 RID: 5057
		private Dictionary<Settlement, CampaignTime> _enteredSettlements = new Dictionary<Settlement, CampaignTime>();
	}
}
