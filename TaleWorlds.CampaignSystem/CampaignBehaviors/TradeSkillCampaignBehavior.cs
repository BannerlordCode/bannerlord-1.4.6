using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Inventory;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x0200044A RID: 1098
	public class TradeSkillCampaignBehavior : CampaignBehaviorBase, IPlayerTradeBehavior
	{
		// Token: 0x06004699 RID: 18073 RVA: 0x0016127E File Offset: 0x0015F47E
		public override void RegisterEvents()
		{
			CampaignEvents.PlayerInventoryExchangeEvent.AddNonSerializedListener(this, new Action<List<ValueTuple<ItemRosterElement, int>>, List<ValueTuple<ItemRosterElement, int>>, bool>(this.PlayerInventoryUpdated));
		}

		// Token: 0x0600469A RID: 18074 RVA: 0x00161298 File Offset: 0x0015F498
		private void RecordPurchases(ItemRosterElement itemRosterElement, int totalPrice)
		{
			TradeSkillCampaignBehavior.ItemTradeData itemTradeData;
			if (!this.ItemsTradeData.TryGetValue(itemRosterElement.EquipmentElement.Item, out itemTradeData))
			{
				itemTradeData = default(TradeSkillCampaignBehavior.ItemTradeData);
			}
			int num = itemTradeData.NumItemsPurchased + itemRosterElement.Amount;
			float num2 = (itemTradeData.AveragePrice * (float)itemTradeData.NumItemsPurchased + (float)totalPrice) / MathF.Max(0.0001f, (float)num);
			this.ItemsTradeData[itemRosterElement.EquipmentElement.Item] = new TradeSkillCampaignBehavior.ItemTradeData(num2, num);
		}

		// Token: 0x0600469B RID: 18075 RVA: 0x0016131C File Offset: 0x0015F51C
		private int RecordSales(ItemRosterElement itemRosterElement, int totalPrice, bool isTrading)
		{
			int num = 0;
			TradeSkillCampaignBehavior.ItemTradeData itemTradeData;
			if (this.ItemsTradeData.TryGetValue(itemRosterElement.EquipmentElement.Item, out itemTradeData))
			{
				if (isTrading)
				{
					int num2 = MathF.Min(itemTradeData.NumItemsPurchased, itemRosterElement.Amount);
					int num3 = itemTradeData.NumItemsPurchased - num2;
					float num4 = (float)num2 * itemTradeData.AveragePrice;
					float num5 = (float)totalPrice / MathF.Max(0.001f, (float)itemRosterElement.Amount);
					int num6 = MathF.Round((float)num2 * num5);
					num = MathF.Max(0, num6 - MathF.Floor(num4));
					if (num3 == 0)
					{
						this.ItemsTradeData.Remove(itemRosterElement.EquipmentElement.Item);
					}
					else
					{
						this.ItemsTradeData[itemRosterElement.EquipmentElement.Item] = new TradeSkillCampaignBehavior.ItemTradeData(itemTradeData.AveragePrice, num3);
					}
				}
				else
				{
					int num7 = MobileParty.MainParty.ItemRoster.FindIndexOfElement(itemRosterElement.EquipmentElement);
					if (num7 == -1)
					{
						this.ItemsTradeData.Remove(itemRosterElement.EquipmentElement.Item);
					}
					else
					{
						int amount = MobileParty.MainParty.ItemRoster.GetElementCopyAtIndex(num7).Amount;
						if (itemTradeData.NumItemsPurchased > amount)
						{
							this.ItemsTradeData[itemRosterElement.EquipmentElement.Item] = new TradeSkillCampaignBehavior.ItemTradeData(itemTradeData.AveragePrice, amount);
						}
					}
				}
			}
			return num;
		}

		// Token: 0x0600469C RID: 18076 RVA: 0x00161488 File Offset: 0x0015F688
		private int GetAveragePriceForItem(ItemRosterElement itemRosterElement)
		{
			TradeSkillCampaignBehavior.ItemTradeData itemTradeData;
			if (!this.ItemsTradeData.TryGetValue(itemRosterElement.EquipmentElement.Item, out itemTradeData))
			{
				return 0;
			}
			return MathF.Round(itemTradeData.AveragePrice);
		}

		// Token: 0x0600469D RID: 18077 RVA: 0x001614C0 File Offset: 0x0015F6C0
		private void PlayerInventoryUpdated(List<ValueTuple<ItemRosterElement, int>> purchasedItems, List<ValueTuple<ItemRosterElement, int>> soldItems, bool isTrading)
		{
			int num = 0;
			if (isTrading)
			{
				foreach (ValueTuple<ItemRosterElement, int> valueTuple in purchasedItems)
				{
					this.ProcessPurchases(valueTuple.Item1, valueTuple.Item2);
				}
			}
			foreach (ValueTuple<ItemRosterElement, int> valueTuple2 in soldItems)
			{
				num += this.ProcessSales(valueTuple2.Item1, valueTuple2.Item2, isTrading);
			}
			if (isTrading)
			{
				SkillLevelingManager.OnTradeProfitMade(PartyBase.MainParty, num);
				CampaignEventDispatcher.Instance.OnPlayerTradeProfit(num);
			}
		}

		// Token: 0x0600469E RID: 18078 RVA: 0x00161584 File Offset: 0x0015F784
		private int ProcessSales(ItemRosterElement itemRosterElement, int totalPrice, bool isTrading)
		{
			if (itemRosterElement.EquipmentElement.ItemModifier == null)
			{
				return this.RecordSales(itemRosterElement, totalPrice, isTrading);
			}
			return 0;
		}

		// Token: 0x0600469F RID: 18079 RVA: 0x001615B0 File Offset: 0x0015F7B0
		private void ProcessPurchases(ItemRosterElement itemRosterElement, int totalPrice)
		{
			if (itemRosterElement.EquipmentElement.ItemModifier == null)
			{
				this.RecordPurchases(itemRosterElement, totalPrice);
			}
		}

		// Token: 0x060046A0 RID: 18080 RVA: 0x001615D6 File Offset: 0x0015F7D6
		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<Dictionary<ItemObject, TradeSkillCampaignBehavior.ItemTradeData>>("ItemsTradeData", ref this.ItemsTradeData);
		}

		// Token: 0x060046A1 RID: 18081 RVA: 0x001615EC File Offset: 0x0015F7EC
		public int GetProjectedProfit(ItemRosterElement itemRosterElement, int itemCost)
		{
			if (itemRosterElement.EquipmentElement.ItemModifier != null)
			{
				return 0;
			}
			int averagePriceForItem = this.GetAveragePriceForItem(itemRosterElement);
			return itemCost - averagePriceForItem;
		}

		// Token: 0x040013C2 RID: 5058
		private Dictionary<ItemObject, TradeSkillCampaignBehavior.ItemTradeData> ItemsTradeData = new Dictionary<ItemObject, TradeSkillCampaignBehavior.ItemTradeData>();

		// Token: 0x02000864 RID: 2148
		public class TradeSkillCampaignBehaviorTypeDefiner : SaveableTypeDefiner
		{
			// Token: 0x06006846 RID: 26694 RVA: 0x001CA2D3 File Offset: 0x001C84D3
			public TradeSkillCampaignBehaviorTypeDefiner()
				: base(150794)
			{
			}

			// Token: 0x06006847 RID: 26695 RVA: 0x001CA2E0 File Offset: 0x001C84E0
			protected override void DefineStructTypes()
			{
				base.AddStructDefinition(typeof(TradeSkillCampaignBehavior.ItemTradeData), 10, null);
			}

			// Token: 0x06006848 RID: 26696 RVA: 0x001CA2F5 File Offset: 0x001C84F5
			protected override void DefineContainerDefinitions()
			{
				base.ConstructContainerDefinition(typeof(Dictionary<ItemObject, TradeSkillCampaignBehavior.ItemTradeData>));
			}
		}

		// Token: 0x02000865 RID: 2149
		internal struct ItemTradeData
		{
			// Token: 0x06006849 RID: 26697 RVA: 0x001CA307 File Offset: 0x001C8507
			public ItemTradeData(float averagePrice, int numItemsPurchased)
			{
				this.AveragePrice = averagePrice;
				this.NumItemsPurchased = numItemsPurchased;
			}

			// Token: 0x0600684A RID: 26698 RVA: 0x001CA318 File Offset: 0x001C8518
			public static void AutoGeneratedStaticCollectObjectsItemTradeData(object o, List<object> collectedObjects)
			{
				((TradeSkillCampaignBehavior.ItemTradeData)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x0600684B RID: 26699 RVA: 0x001CA334 File Offset: 0x001C8534
			private void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
			}

			// Token: 0x0600684C RID: 26700 RVA: 0x001CA336 File Offset: 0x001C8536
			internal static object AutoGeneratedGetMemberValueAveragePrice(object o)
			{
				return ((TradeSkillCampaignBehavior.ItemTradeData)o).AveragePrice;
			}

			// Token: 0x0600684D RID: 26701 RVA: 0x001CA348 File Offset: 0x001C8548
			internal static object AutoGeneratedGetMemberValueNumItemsPurchased(object o)
			{
				return ((TradeSkillCampaignBehavior.ItemTradeData)o).NumItemsPurchased;
			}

			// Token: 0x04002421 RID: 9249
			[SaveableField(10)]
			public readonly float AveragePrice;

			// Token: 0x04002422 RID: 9250
			[SaveableField(20)]
			public readonly int NumItemsPurchased;
		}
	}
}
