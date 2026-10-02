using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.Inventory;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Workshops;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;
using TaleWorlds.Localization;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x02000454 RID: 1108
	public class WorkshopsCampaignBehavior : CampaignBehaviorBase, IWorkshopWarehouseCampaignBehavior
	{
		// Token: 0x060047C2 RID: 18370 RVA: 0x00168BC8 File Offset: 0x00166DC8
		public override void RegisterEvents()
		{
			CampaignEvents.OnNewGameCreatedPartialFollowUpEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter, int>(this.OnNewGameCreatedPartialFollowUp));
			CampaignEvents.OnAfterSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnAfterSessionLaunched));
			CampaignEvents.OnGameLoadedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnGameLoaded));
			CampaignEvents.DailyTickTownEvent.AddNonSerializedListener(this, new Action<Town>(this.DailyTickTown));
			CampaignEvents.OnSettlementOwnerChangedEvent.AddNonSerializedListener(this, new Action<Settlement, bool, Hero, Hero, Hero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail>(this.OnSettlementOwnerChanged));
			CampaignEvents.HeroKilledEvent.AddNonSerializedListener(this, new Action<Hero, Hero, KillCharacterAction.KillCharacterActionDetail, bool>(this.OnHeroKilled));
			CampaignEvents.WarDeclared.AddNonSerializedListener(this, new Action<IFaction, IFaction, DeclareWarAction.DeclareWarDetail>(this.OnWarDeclared));
			CampaignEvents.OnClanChangedKingdomEvent.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, ChangeKingdomAction.ChangeKingdomActionDetail, bool>(this.OnClanChangedKingdom));
			CampaignEvents.WorkshopOwnerChangedEvent.AddNonSerializedListener(this, new Action<Workshop, Hero>(this.OnWorkshopOwnerChanged));
			CampaignEvents.WorkshopTypeChangedEvent.AddNonSerializedListener(this, new Action<Workshop>(this.OnWorkshopTypeChanged));
		}

		// Token: 0x060047C3 RID: 18371 RVA: 0x00168CBB File Offset: 0x00166EBB
		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<KeyValuePair<Settlement, ItemRoster>[]>("_warehouseRosterPerSettlement", ref this._warehouseRosterPerSettlement);
			dataStore.SyncData<WorkshopsCampaignBehavior.WorkshopData[]>("_workshopData", ref this._workshopData);
		}

		// Token: 0x060047C4 RID: 18372 RVA: 0x00168CE1 File Offset: 0x00166EE1
		private void OnNewGameCreatedPartialFollowUp(CampaignGameStarter starter, int i)
		{
			if (i >= 10)
			{
				if (i == 10)
				{
					this.InitializeBehaviorData();
					this.FillItemsInAllCategories();
					this.InitializeWorkshops();
					this.BuildWorkshopsAtGameStart();
				}
				if (i % 20 == 0)
				{
					this.RunTownShopsAtGameStart();
				}
			}
		}

		// Token: 0x060047C5 RID: 18373 RVA: 0x00168D14 File Offset: 0x00166F14
		private void InitializeBehaviorData()
		{
			if (this._workshopData == null)
			{
				this._workshopData = new WorkshopsCampaignBehavior.WorkshopData[Campaign.Current.Models.WorkshopModel.MaximumWorkshopsPlayerCanHave];
			}
			if (this._warehouseRosterPerSettlement == null)
			{
				this._warehouseRosterPerSettlement = new KeyValuePair<Settlement, ItemRoster>[Campaign.Current.Models.WorkshopModel.MaximumWorkshopsPlayerCanHave];
			}
		}

		// Token: 0x060047C6 RID: 18374 RVA: 0x00168D70 File Offset: 0x00166F70
		private void OnGameLoaded(CampaignGameStarter campaignGameStarter)
		{
			this.InitializeBehaviorData();
			if (MBSaveLoad.IsUpdatingGameVersion && MBSaveLoad.LastLoadedGameVersion < ApplicationVersion.FromString("v1.2.0", 0))
			{
				foreach (Workshop workshop in Hero.MainHero.OwnedWorkshops)
				{
					this.AddNewWorkshopData(workshop);
					this.AddNewWarehouseDataIfNeeded(workshop.Settlement);
				}
			}
			if (MBSaveLoad.LastLoadedGameVersion.IsOlderThan(ApplicationVersion.FromString("v1.2.9.35637", 0)))
			{
				for (int i = 0; i < this._workshopData.Length; i++)
				{
					if (this._workshopData[i] != null && this._workshopData[i].Workshop.Owner != Hero.MainHero)
					{
						this._workshopData[i] = null;
					}
				}
			}
			this.EnsureBehaviorDataSize();
			this.FillItemsInAllCategories();
			if (MBSaveLoad.IsUpdatingGameVersion && MBSaveLoad.LastLoadedGameVersion < ApplicationVersion.FromString("v1.3.0", 0))
			{
				this.RemoveDeadOwnersFromWorkshops();
			}
		}

		// Token: 0x060047C7 RID: 18375 RVA: 0x00168E80 File Offset: 0x00167080
		private void RemoveDeadOwnersFromWorkshops()
		{
			foreach (Settlement settlement in Settlement.All)
			{
				if (settlement.IsTown)
				{
					foreach (Workshop workshop in settlement.Town.Workshops)
					{
						if (workshop.Owner.IsDead)
						{
							Debug.FailedAssert("Workshop owner is dead", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\CampaignBehaviors\\WorkshopsCampaignBehavior.cs", "RemoveDeadOwnersFromWorkshops", 176);
							Hero notableOwnerForWorkshop = Campaign.Current.Models.WorkshopModel.GetNotableOwnerForWorkshop(workshop);
							ChangeOwnerOfWorkshopAction.ApplyByDeath(workshop, notableOwnerForWorkshop);
						}
					}
				}
			}
		}

		// Token: 0x060047C8 RID: 18376 RVA: 0x00168F3C File Offset: 0x0016713C
		private void EnsureBehaviorDataSize()
		{
			if (this._workshopData.Length < Campaign.Current.Models.WorkshopModel.MaximumWorkshopsPlayerCanHave)
			{
				WorkshopsCampaignBehavior.WorkshopData[] array = new WorkshopsCampaignBehavior.WorkshopData[Campaign.Current.Models.WorkshopModel.MaximumWorkshopsPlayerCanHave];
				for (int i = 0; i < Campaign.Current.Models.WorkshopModel.MaximumWorkshopsPlayerCanHave; i++)
				{
					if (i < this._workshopData.Length)
					{
						array[i] = this._workshopData[i];
					}
					else
					{
						array[i] = null;
					}
				}
				this._workshopData = array;
			}
			if (this._warehouseRosterPerSettlement.Length < Campaign.Current.Models.WorkshopModel.MaximumWorkshopsPlayerCanHave)
			{
				KeyValuePair<Settlement, ItemRoster>[] array2 = new KeyValuePair<Settlement, ItemRoster>[Campaign.Current.Models.WorkshopModel.MaximumWorkshopsPlayerCanHave];
				for (int j = 0; j < Campaign.Current.Models.WorkshopModel.MaximumWorkshopsPlayerCanHave; j++)
				{
					if (j < this._warehouseRosterPerSettlement.Length && this._warehouseRosterPerSettlement[j].Key != null && this._warehouseRosterPerSettlement[j].Value != null)
					{
						array2[j] = this._warehouseRosterPerSettlement[j];
					}
				}
				this._warehouseRosterPerSettlement = array2;
			}
		}

		// Token: 0x060047C9 RID: 18377 RVA: 0x00169068 File Offset: 0x00167268
		private void OnWorkshopTypeChanged(Workshop workshop)
		{
			if (workshop.Owner == Hero.MainHero)
			{
				this.RemoveWorkshopData(workshop);
				this.AddNewWorkshopData(workshop);
			}
		}

		// Token: 0x060047CA RID: 18378 RVA: 0x00169088 File Offset: 0x00167288
		private void OnWorkshopOwnerChanged(Workshop workshop, Hero oldOwner)
		{
			Hero owner = workshop.Owner;
			if (owner == Hero.MainHero)
			{
				this.AddNewWarehouseDataIfNeeded(workshop.Settlement);
				this.AddNewWorkshopData(workshop);
				return;
			}
			if (oldOwner == Hero.MainHero && Clan.PlayerClan.Leader != owner)
			{
				if (Hero.MainHero.OwnedWorkshops.All<Workshop>((Workshop x) => x.Settlement != workshop.Settlement))
				{
					if (Settlement.CurrentSettlement != null && Settlement.CurrentSettlement == workshop.Settlement)
					{
						this.TransferWarehouseToPlayerParty(Settlement.CurrentSettlement);
					}
					this.RemoveWarehouseData(workshop.Settlement);
				}
				this.RemoveWorkshopData(workshop);
			}
		}

		// Token: 0x060047CB RID: 18379 RVA: 0x00169148 File Offset: 0x00167348
		private void DailyTickTown(Town town)
		{
			foreach (Workshop workshop in town.Workshops)
			{
				if (!town.InRebelliousState)
				{
					this.RunTownWorkshop(town, workshop);
				}
				this.HandleDailyExpense(workshop);
			}
		}

		// Token: 0x060047CC RID: 18380 RVA: 0x00169188 File Offset: 0x00167388
		private void OnHeroKilled(Hero victim, Hero killer, KillCharacterAction.KillCharacterActionDetail detail, bool showNotification = true)
		{
			if (!victim.IsHumanPlayerCharacter)
			{
				foreach (Workshop workshop in victim.OwnedWorkshops.ToList<Workshop>())
				{
					Hero notableOwnerForWorkshop = Campaign.Current.Models.WorkshopModel.GetNotableOwnerForWorkshop(workshop);
					ChangeOwnerOfWorkshopAction.ApplyByDeath(workshop, notableOwnerForWorkshop);
				}
			}
		}

		// Token: 0x060047CD RID: 18381 RVA: 0x00169200 File Offset: 0x00167400
		private void OnClanChangedKingdom(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail detail, bool showNotification = true)
		{
			this.TransferPlayerWorkshopsIfNeeded();
		}

		// Token: 0x060047CE RID: 18382 RVA: 0x00169208 File Offset: 0x00167408
		private void OnWarDeclared(IFaction faction1, IFaction faction2, DeclareWarAction.DeclareWarDetail detail)
		{
			this.TransferPlayerWorkshopsIfNeeded();
		}

		// Token: 0x060047CF RID: 18383 RVA: 0x00169210 File Offset: 0x00167410
		private void OnSettlementOwnerChanged(Settlement settlement, bool openToClaim, Hero newSettlementOwner, Hero oldSettlementOwner, Hero capturerHero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail detail)
		{
			if (settlement.IsTown)
			{
				foreach (Workshop workshop in settlement.Town.Workshops)
				{
					if (workshop.Owner != null && workshop.Owner.MapFaction.IsAtWarWith(newSettlementOwner.MapFaction) && workshop.Owner.GetPerkValue(DefaultPerks.Trade.RapidDevelopment))
					{
						GiveGoldAction.ApplyBetweenCharacters(null, workshop.Owner, MathF.Round(DefaultPerks.Trade.RapidDevelopment.PrimaryBonus), false);
					}
				}
				this.TransferPlayerWorkshopsIfNeeded();
			}
		}

		// Token: 0x060047D0 RID: 18384 RVA: 0x00169297 File Offset: 0x00167497
		private void OnAfterSessionLaunched(CampaignGameStarter campaignGameStarter)
		{
			this.InitializeGameMenus(campaignGameStarter);
		}

		// Token: 0x060047D1 RID: 18385 RVA: 0x001692A0 File Offset: 0x001674A0
		protected void InitializeGameMenus(CampaignGameStarter campaignGameStarter)
		{
			campaignGameStarter.AddGameMenuOption("town", "manage_warehouse", "{=LK4kNZkb}Enter the warehouse", new GameMenuOption.OnConditionDelegate(this.warehouse_manage_on_condition), new GameMenuOption.OnConsequenceDelegate(this.warehouse_manage_on_consequence), false, 7, false, null);
			campaignGameStarter.AddPlayerLine("workshop_worker_manage_warehouse", "player_options", "warehouse", "{=mBnoWa8R}I would like to access the Warehouse.", null, null, 100, null, null);
			campaignGameStarter.AddDialogLine("workshop_worker_manage_warehouse_answer", "warehouse", "player_options", "{=Y4LhmAdi}Sure, boss. Go ahead.", null, new ConversationSentence.OnConsequenceDelegate(this.warehouse_manage_on_consequence), 100, null);
		}

		// Token: 0x060047D2 RID: 18386 RVA: 0x0016932C File Offset: 0x0016752C
		private void warehouse_manage_on_consequence()
		{
			InventoryLogic.CapacityData capacityData = new InventoryLogic.CapacityData(new Func<int>(WorkshopsCampaignBehavior.<>c.<>9.<warehouse_manage_on_consequence>g__CapacityDelegate|21_0), new Func<TextObject>(WorkshopsCampaignBehavior.<>c.<>9.<warehouse_manage_on_consequence>g__CapacityExceededWarningDelegate|21_1), new Func<TextObject>(WorkshopsCampaignBehavior.<>c.<>9.<warehouse_manage_on_consequence>g__CapacityExceededHintDelegate|21_2), false);
			InventoryScreenHelper.OpenScreenAsWarehouse(this.GetWarehouseRoster(Settlement.CurrentSettlement), capacityData);
			Campaign.Current.ConversationManager.ContinueConversation();
		}

		// Token: 0x060047D3 RID: 18387 RVA: 0x00169390 File Offset: 0x00167590
		private void warehouse_manage_on_consequence(MenuCallbackArgs args)
		{
			InventoryLogic.CapacityData capacityData = new InventoryLogic.CapacityData(new Func<int>(WorkshopsCampaignBehavior.<>c.<>9.<warehouse_manage_on_consequence>g__CapacityDelegate|22_0), new Func<TextObject>(WorkshopsCampaignBehavior.<>c.<>9.<warehouse_manage_on_consequence>g__CapacityExceededWarningDelegate|22_1), new Func<TextObject>(WorkshopsCampaignBehavior.<>c.<>9.<warehouse_manage_on_consequence>g__CapacityExceededHintDelegate|22_2), false);
			InventoryScreenHelper.OpenScreenAsWarehouse(this.GetWarehouseRoster(Settlement.CurrentSettlement), capacityData);
		}

		// Token: 0x060047D4 RID: 18388 RVA: 0x001693E5 File Offset: 0x001675E5
		private bool warehouse_manage_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Warehouse;
			return this.GetWarehouseRoster(Settlement.CurrentSettlement) != null;
		}

		// Token: 0x060047D5 RID: 18389 RVA: 0x00169400 File Offset: 0x00167600
		bool IWorkshopWarehouseCampaignBehavior.IsGettingInputsFromWarehouse(Workshop workshop)
		{
			WorkshopsCampaignBehavior.WorkshopData dataOfWorkshop = this.GetDataOfWorkshop(workshop);
			return dataOfWorkshop != null && dataOfWorkshop.IsGettingInputsFromWarehouse;
		}

		// Token: 0x060047D6 RID: 18390 RVA: 0x00169420 File Offset: 0x00167620
		void IWorkshopWarehouseCampaignBehavior.SetIsGettingInputsFromWarehouse(Workshop workshop, bool isActive)
		{
			WorkshopsCampaignBehavior.WorkshopData dataOfWorkshop = this.GetDataOfWorkshop(workshop);
			if (dataOfWorkshop != null)
			{
				dataOfWorkshop.IsGettingInputsFromWarehouse = isActive;
			}
		}

		// Token: 0x060047D7 RID: 18391 RVA: 0x00169440 File Offset: 0x00167640
		float IWorkshopWarehouseCampaignBehavior.GetStockProductionInWarehouseRatio(Workshop workshop)
		{
			WorkshopsCampaignBehavior.WorkshopData dataOfWorkshop = this.GetDataOfWorkshop(workshop);
			if (dataOfWorkshop != null)
			{
				return dataOfWorkshop.StockProductionInWarehouseRatio;
			}
			return 0f;
		}

		// Token: 0x060047D8 RID: 18392 RVA: 0x00169464 File Offset: 0x00167664
		void IWorkshopWarehouseCampaignBehavior.SetStockProductionInWarehouseRatio(Workshop workshop, float ratio)
		{
			WorkshopsCampaignBehavior.WorkshopData dataOfWorkshop = this.GetDataOfWorkshop(workshop);
			if (dataOfWorkshop != null)
			{
				dataOfWorkshop.StockProductionInWarehouseRatio = ratio;
			}
		}

		// Token: 0x060047D9 RID: 18393 RVA: 0x00169484 File Offset: 0x00167684
		int IWorkshopWarehouseCampaignBehavior.GetInputCount(Workshop workshop)
		{
			ItemRoster warehouseRoster = this.GetWarehouseRoster(workshop.Settlement);
			int num = 0;
			List<ItemCategory> list = new List<ItemCategory>();
			foreach (WorkshopType.Production production in workshop.WorkshopType.Productions)
			{
				foreach (ValueTuple<ItemCategory, int> valueTuple in production.Inputs)
				{
					ItemCategory item = valueTuple.Item1;
					if (!list.Contains(item))
					{
						list.Add(item);
					}
				}
			}
			foreach (ItemCategory itemCategory in list)
			{
				foreach (ItemRosterElement itemRosterElement in warehouseRoster)
				{
					if (itemRosterElement.EquipmentElement.Item.ItemCategory == itemCategory)
					{
						num += itemRosterElement.Amount;
						break;
					}
				}
			}
			return num;
		}

		// Token: 0x060047DA RID: 18394 RVA: 0x001695D4 File Offset: 0x001677D4
		ExplainedNumber IWorkshopWarehouseCampaignBehavior.GetInputDailyChange(Workshop workshop)
		{
			ItemRoster warehouseRoster = this.GetWarehouseRoster(workshop.Settlement);
			ExplainedNumber explainedNumber = new ExplainedNumber(0f, true, null);
			Dictionary<ItemCategory, float> dictionary = new Dictionary<ItemCategory, float>();
			foreach (WorkshopType.Production production in workshop.WorkshopType.Productions)
			{
				foreach (ValueTuple<ItemCategory, int> valueTuple in production.Inputs)
				{
					float num = Campaign.Current.Models.WorkshopModel.GetEffectiveConversionSpeedOfProduction(workshop, production.ConversionSpeed, false).ResultNumber * (float)valueTuple.Item2;
					ItemCategory item = valueTuple.Item1;
					float num2;
					if (!dictionary.TryGetValue(item, out num2))
					{
						dictionary.Add(item, num);
					}
					else
					{
						dictionary[item] = num2 + num;
					}
				}
			}
			foreach (KeyValuePair<ItemCategory, float> keyValuePair in dictionary)
			{
				int num3 = 0;
				foreach (ItemRosterElement itemRosterElement in warehouseRoster)
				{
					if (itemRosterElement.EquipmentElement.Item.ItemCategory == keyValuePair.Key)
					{
						num3 += itemRosterElement.Amount;
					}
				}
				if (num3 > 0)
				{
					TextObject textObject = GameTexts.FindText("str_RANK_with_NUM_between_parenthesis", null);
					textObject.SetTextVariable("RANK", keyValuePair.Key.GetName());
					textObject.SetTextVariable("NUMBER", num3);
					explainedNumber.Add(-keyValuePair.Value, textObject, null);
				}
			}
			return explainedNumber;
		}

		// Token: 0x060047DB RID: 18395 RVA: 0x001697D8 File Offset: 0x001679D8
		int IWorkshopWarehouseCampaignBehavior.GetOutputCount(Workshop workshop)
		{
			ItemRoster warehouseRoster = this.GetWarehouseRoster(workshop.Settlement);
			int num = 0;
			List<ItemCategory> list = new List<ItemCategory>();
			foreach (WorkshopType.Production production in workshop.WorkshopType.Productions)
			{
				foreach (ValueTuple<ItemCategory, int> valueTuple in production.Outputs)
				{
					ItemCategory item = valueTuple.Item1;
					if (!list.Contains(item))
					{
						list.Add(item);
					}
				}
			}
			foreach (ItemCategory itemCategory in list)
			{
				foreach (ItemRosterElement itemRosterElement in warehouseRoster)
				{
					if (itemRosterElement.EquipmentElement.Item.ItemCategory == itemCategory)
					{
						num += itemRosterElement.Amount;
						break;
					}
				}
			}
			return num;
		}

		// Token: 0x060047DC RID: 18396 RVA: 0x00169928 File Offset: 0x00167B28
		ExplainedNumber IWorkshopWarehouseCampaignBehavior.GetOutputDailyChange(Workshop workshop)
		{
			ExplainedNumber explainedNumber = new ExplainedNumber(0f, true, null);
			ItemRoster warehouseRoster = this.GetWarehouseRoster(workshop.Settlement);
			Dictionary<ItemCategory, float> dictionary = new Dictionary<ItemCategory, float>();
			foreach (WorkshopType.Production production in workshop.WorkshopType.Productions)
			{
				foreach (ValueTuple<ItemCategory, int> valueTuple in production.Outputs)
				{
					ItemCategory item = valueTuple.Item1;
					if (item.IsTradeGood)
					{
						int item2 = valueTuple.Item2;
						float num = Campaign.Current.Models.WorkshopModel.GetEffectiveConversionSpeedOfProduction(workshop, production.ConversionSpeed, false).ResultNumber * (float)item2 * this.GetDataOfWorkshop(workshop).StockProductionInWarehouseRatio;
						float num2;
						if (!dictionary.TryGetValue(item, out num2))
						{
							dictionary.Add(item, num);
						}
						else
						{
							dictionary[item] = num2 + num;
						}
					}
				}
			}
			foreach (KeyValuePair<ItemCategory, float> keyValuePair in dictionary)
			{
				int num3 = 0;
				foreach (ItemRosterElement itemRosterElement in warehouseRoster)
				{
					if (itemRosterElement.EquipmentElement.Item.ItemCategory == keyValuePair.Key)
					{
						num3 += itemRosterElement.Amount;
					}
				}
				TextObject textObject = GameTexts.FindText("str_RANK_with_NUM_between_parenthesis", null);
				textObject.SetTextVariable("RANK", keyValuePair.Key.GetName());
				textObject.SetTextVariable("NUMBER", num3);
				explainedNumber.Add(keyValuePair.Value, textObject, null);
			}
			return explainedNumber;
		}

		// Token: 0x060047DD RID: 18397 RVA: 0x00169B48 File Offset: 0x00167D48
		bool IWorkshopWarehouseCampaignBehavior.IsRawMaterialsSufficientInTownMarket(Workshop workshop)
		{
			for (int i = 0; i < workshop.WorkshopType.Productions.Count; i++)
			{
				WorkshopType.Production production = workshop.WorkshopType.Productions[i];
				int num;
				if (this.DetermineItemRosterHasSufficientInputs(production, workshop.Settlement.Town.Owner.ItemRoster, workshop.Settlement.Town, out num))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060047DE RID: 18398 RVA: 0x00169BB0 File Offset: 0x00167DB0
		public float GetWarehouseItemRosterWeight(Settlement settlement)
		{
			ItemRoster warehouseRoster = this.GetWarehouseRoster(settlement);
			float num = 0f;
			foreach (ItemRosterElement itemRosterElement in warehouseRoster)
			{
				num += itemRosterElement.GetRosterElementWeight();
			}
			return num;
		}

		// Token: 0x060047DF RID: 18399 RVA: 0x00169C08 File Offset: 0x00167E08
		private bool TickOneProductionCycleForPlayerWorkshop(WorkshopType.Production production, Workshop workshop, bool effectCapital)
		{
			bool flag = false;
			int num = 0;
			Town town = workshop.Settlement.Town;
			WorkshopsCampaignBehavior.WorkshopData dataOfWorkshop = this.GetDataOfWorkshop(workshop);
			bool flag2 = dataOfWorkshop.IsGettingInputsFromWarehouse;
			if (flag2)
			{
				flag = this.DetermineItemRosterHasSufficientInputs(production, this.GetWarehouseRoster(workshop.Settlement), town, out num);
				if (flag)
				{
					num = 0;
				}
				else
				{
					flag2 = false;
				}
			}
			if (!flag)
			{
				flag = this.DetermineItemRosterHasSufficientInputs(production, town.Owner.ItemRoster, town, out num);
			}
			if (flag)
			{
				int num2;
				List<EquipmentElement> itemsToProduce = this.GetItemsToProduce(production, workshop, out num2);
				float num3 = dataOfWorkshop.StockProductionInWarehouseRatio;
				bool flag3 = num3.ApproximatelyEqualsTo(1f, 1E-05f);
				if (this.CanPlayerWorkshopProduceThisCycle(production, workshop, num, num2, effectCapital, flag3))
				{
					Dictionary<ItemObject, int> dictionary = new Dictionary<ItemObject, int>();
					foreach (ValueTuple<ItemCategory, int> valueTuple in production.Inputs)
					{
						if (flag2)
						{
							this.ConsumeInputFromWarehouse(valueTuple.Item1, valueTuple.Item2, workshop);
						}
						else
						{
							this.ConsumeInputFromTownMarket(valueTuple.Item1, valueTuple.Item2, town, workshop, effectCapital);
						}
					}
					foreach (EquipmentElement equipmentElement in itemsToProduce)
					{
						WorkshopsCampaignBehavior.WorkshopData dataOfWorkshop2 = this.GetDataOfWorkshop(workshop);
						if (equipmentElement.Item.IsTradeGood && this.CanItemFitInWarehouse(workshop.Settlement, equipmentElement))
						{
							this.AddOutputProgressForWarehouse(workshop, num3);
							if (dataOfWorkshop2.ProductionProgressForWarehouse >= 1f)
							{
								this.ProduceAnOutputToWarehouse(equipmentElement, workshop);
								this.AddOutputProgressForWarehouse(workshop, -1f);
								if (dictionary.ContainsKey(equipmentElement.Item))
								{
									Dictionary<ItemObject, int> dictionary2 = dictionary;
									ItemObject itemObject = equipmentElement.Item;
									int num4 = dictionary2[itemObject];
									dictionary2[itemObject] = num4 + 1;
								}
								else
								{
									dictionary.Add(equipmentElement.Item, 1);
								}
							}
						}
						else
						{
							num3 = 0f;
						}
						this.AddOutputProgressForTown(workshop, 1f - num3);
						if (dataOfWorkshop2.ProductionProgressForTown >= 1f)
						{
							this.ProduceAnOutputToTown(equipmentElement, workshop, effectCapital);
							SkillLevelingManager.OnProductionProducedToWarehouse(equipmentElement);
							this.AddOutputProgressForTown(workshop, -1f);
							if (dictionary.ContainsKey(equipmentElement.Item))
							{
								Dictionary<ItemObject, int> dictionary3 = dictionary;
								ItemObject itemObject = equipmentElement.Item;
								int num4 = dictionary3[itemObject];
								dictionary3[itemObject] = num4 + 1;
							}
							else
							{
								dictionary.Add(equipmentElement.Item, 1);
							}
						}
					}
					foreach (KeyValuePair<ItemObject, int> keyValuePair in dictionary)
					{
						ItemObject key = keyValuePair.Key;
						int value = keyValuePair.Value;
						CampaignEventDispatcher.Instance.OnItemProduced(key, workshop.Settlement, value);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x060047E0 RID: 18400 RVA: 0x00169F0C File Offset: 0x0016810C
		private void ProduceAnOutputToWarehouse(EquipmentElement outputItem, Workshop workshop)
		{
			this.GetWarehouseRoster(workshop.Settlement).AddToCounts(outputItem, 1);
		}

		// Token: 0x060047E1 RID: 18401 RVA: 0x00169F24 File Offset: 0x00168124
		private void ConsumeInputFromWarehouse(ItemCategory productionInput, int inputCount, Workshop workshop)
		{
			ItemRoster warehouseRoster = this.GetWarehouseRoster(workshop.Settlement);
			int num = inputCount;
			for (int i = 0; i < warehouseRoster.Count; i++)
			{
				if (num == 0)
				{
					return;
				}
				ItemObject itemAtIndex = warehouseRoster.GetItemAtIndex(i);
				if (itemAtIndex.ItemCategory == productionInput)
				{
					int elementNumber = warehouseRoster.GetElementNumber(i);
					int num2 = MathF.Min(num, elementNumber);
					num -= num2;
					warehouseRoster.AddToCounts(itemAtIndex, -inputCount);
					CampaignEventDispatcher.Instance.OnItemConsumed(itemAtIndex, workshop.Settlement, inputCount);
				}
			}
		}

		// Token: 0x060047E2 RID: 18402 RVA: 0x00169F9C File Offset: 0x0016819C
		private bool CanPlayerWorkshopProduceThisCycle(WorkshopType.Production production, Workshop workshop, int inputMaterialCost, int outputIncome, bool effectCapital, bool allOutputsWillBeSentToWarehouse)
		{
			float num = (workshop.WorkshopType.IsHidden ? ((float)inputMaterialCost) : ((float)inputMaterialCost + 200f / production.ConversionSpeed));
			if (Campaign.Current.GameStarted && (float)outputIncome <= num)
			{
				return false;
			}
			if (workshop.Capital < inputMaterialCost)
			{
				return false;
			}
			if (effectCapital)
			{
				bool flag = workshop.Settlement.Town.Gold >= outputIncome;
				bool flag2 = !this.IsWarehouseAtLimit(workshop.Settlement);
				if (!flag && (!allOutputsWillBeSentToWarehouse || flag2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060047E3 RID: 18403 RVA: 0x0016A024 File Offset: 0x00168224
		private void HandlePlayerWorkshopExpense(Workshop shop)
		{
			int expense = shop.Expense;
			if (shop.Capital > Campaign.Current.Models.WorkshopModel.CapitalLowLimit)
			{
				shop.ChangeGold(-expense);
				return;
			}
			if (shop.Owner.Gold >= expense)
			{
				shop.Owner.Gold -= expense;
				return;
			}
			if (shop.Capital >= expense)
			{
				shop.ChangeGold(-expense);
				return;
			}
			this.ChangeWorkshopOwnerByBankruptcy(shop);
		}

		// Token: 0x060047E4 RID: 18404 RVA: 0x0016A098 File Offset: 0x00168298
		private bool TickOneProductionCycleForNotableWorkshop(WorkshopType.Production production, Workshop workshop, bool effectCapital)
		{
			Town town = workshop.Settlement.Town;
			int num = 0;
			if (!this.DetermineItemRosterHasSufficientInputs(production, town.Owner.ItemRoster, town, out num))
			{
				return false;
			}
			int num2;
			List<EquipmentElement> itemsToProduce = this.GetItemsToProduce(production, workshop, out num2);
			if (this.CanNotableWorkshopProduceThisCycle(production, workshop, num, num2, effectCapital))
			{
				foreach (ValueTuple<ItemCategory, int> valueTuple in production.Inputs)
				{
					this.ConsumeInputFromTownMarket(valueTuple.Item1, valueTuple.Item2, town, workshop, effectCapital);
				}
				foreach (EquipmentElement equipmentElement in itemsToProduce)
				{
					this.ProduceAnOutputToTown(equipmentElement, workshop, effectCapital);
					CampaignEventDispatcher.Instance.OnItemProduced(equipmentElement.Item, workshop.Settlement, 1);
				}
				return true;
			}
			return false;
		}

		// Token: 0x060047E5 RID: 18405 RVA: 0x0016A1A0 File Offset: 0x001683A0
		private bool CanNotableWorkshopProduceThisCycle(WorkshopType.Production production, Workshop workshop, int inputMaterialCost, int outputIncome, bool effectCapital)
		{
			float num = (workshop.WorkshopType.IsHidden ? ((float)inputMaterialCost) : ((float)inputMaterialCost + 200f / production.ConversionSpeed));
			return (!Campaign.Current.GameStarted || (float)outputIncome > num) && (workshop.Settlement.Town.Gold >= outputIncome || !effectCapital) && workshop.Capital >= inputMaterialCost;
		}

		// Token: 0x060047E6 RID: 18406 RVA: 0x0016A20C File Offset: 0x0016840C
		private void HandleNotableWorkshopExpense(Workshop shop)
		{
			int expense = shop.Expense;
			if (shop.Capital >= expense)
			{
				shop.ChangeGold(-expense);
				return;
			}
			this.ChangeWorkshopOwnerByBankruptcy(shop);
		}

		// Token: 0x060047E7 RID: 18407 RVA: 0x0016A23C File Offset: 0x0016843C
		private WorkshopsCampaignBehavior.WorkshopData GetDataOfWorkshop(Workshop workshop)
		{
			for (int i = 0; i < this._workshopData.Length; i++)
			{
				WorkshopsCampaignBehavior.WorkshopData workshopData = this._workshopData[i];
				if (workshopData != null && workshopData.Workshop == workshop)
				{
					return workshopData;
				}
			}
			return null;
		}

		// Token: 0x060047E8 RID: 18408 RVA: 0x0016A274 File Offset: 0x00168474
		private List<EquipmentElement> GetItemsToProduce(WorkshopType.Production production, Workshop workshop, out int income)
		{
			List<EquipmentElement> list = new List<EquipmentElement>();
			income = 0;
			for (int i = 0; i < production.Outputs.Count; i++)
			{
				int item = production.Outputs[i].Item2;
				for (int j = 0; j < item; j++)
				{
					EquipmentElement randomItem = this.GetRandomItem(production.Outputs[i].Item1, workshop.Settlement.Town);
					if (!randomItem.IsEmpty)
					{
						list.Add(randomItem);
						income += workshop.Settlement.Town.GetItemPrice(randomItem, null, true);
					}
					else
					{
						Debug.FailedAssert("Workshop produces empty items", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\CampaignBehaviors\\WorkshopsCampaignBehavior.cs", "GetItemsToProduce", 916);
					}
				}
			}
			return list;
		}

		// Token: 0x060047E9 RID: 18409 RVA: 0x0016A334 File Offset: 0x00168534
		private void ProduceAnOutputToTown(EquipmentElement outputItem, Workshop workshop, bool effectCapital)
		{
			Town town = workshop.Settlement.Town;
			int itemPrice = town.GetItemPrice(outputItem, null, false);
			town.Owner.ItemRoster.AddToCounts(outputItem, 1);
			if (Campaign.Current.GameStarted && effectCapital)
			{
				int num = MathF.Min(1000, itemPrice);
				workshop.ChangeGold(num);
				town.ChangeGold(-num);
			}
		}

		// Token: 0x060047EA RID: 18410 RVA: 0x0016A394 File Offset: 0x00168594
		private void ConsumeInputFromTownMarket(ItemCategory productionInput, int productionInputCount, Town town, Workshop workshop, bool effectCapital)
		{
			ItemRoster itemRoster = town.Owner.ItemRoster;
			int num = itemRoster.FindIndex((ItemObject x) => x.ItemCategory == productionInput);
			if (num >= 0)
			{
				ItemObject itemAtIndex = itemRoster.GetItemAtIndex(num);
				if (Campaign.Current.GameStarted && effectCapital)
				{
					int itemPrice = town.GetItemPrice(itemAtIndex, null, false);
					workshop.ChangeGold(-itemPrice);
					town.ChangeGold(itemPrice);
				}
				itemRoster.AddToCounts(itemAtIndex, -productionInputCount);
				CampaignEventDispatcher.Instance.OnItemConsumed(itemAtIndex, town.Owner.Settlement, productionInputCount);
			}
		}

		// Token: 0x060047EB RID: 18411 RVA: 0x0016A426 File Offset: 0x00168626
		private bool IsItemPreferredForTown(ItemObject item, Town townComponent)
		{
			return item.Culture == null || item.Culture.StringId == "neutral_culture" || item.Culture == townComponent.Culture;
		}

		// Token: 0x060047EC RID: 18412 RVA: 0x0016A458 File Offset: 0x00168658
		private bool DetermineItemRosterHasSufficientInputs(WorkshopType.Production production, ItemRoster itemRoster, Town town, out int inputMaterialCost)
		{
			List<ValueTuple<ItemCategory, int>> inputs = production.Inputs;
			inputMaterialCost = 0;
			foreach (ValueTuple<ItemCategory, int> valueTuple in inputs)
			{
				ItemCategory item = valueTuple.Item1;
				int num = valueTuple.Item2;
				for (int i = 0; i < itemRoster.Count; i++)
				{
					ItemObject itemAtIndex = itemRoster.GetItemAtIndex(i);
					if (itemAtIndex.ItemCategory == item)
					{
						int elementNumber = itemRoster.GetElementNumber(i);
						int num2 = MathF.Min(num, elementNumber);
						num -= num2;
						inputMaterialCost += town.GetItemPrice(itemAtIndex, null, false) * num2;
					}
				}
				if (num > 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060047ED RID: 18413 RVA: 0x0016A514 File Offset: 0x00168714
		private void AddOutputProgressForWarehouse(Workshop workshop, float progressToAdd)
		{
			this.GetDataOfWorkshop(workshop).ProductionProgressForWarehouse += progressToAdd;
		}

		// Token: 0x060047EE RID: 18414 RVA: 0x0016A52A File Offset: 0x0016872A
		private void AddOutputProgressForTown(Workshop workshop, float progressToAdd)
		{
			this.GetDataOfWorkshop(workshop).ProductionProgressForTown += progressToAdd;
		}

		// Token: 0x060047EF RID: 18415 RVA: 0x0016A540 File Offset: 0x00168740
		private bool CanItemFitInWarehouse(Settlement settlement, EquipmentElement equipmentElement)
		{
			return this.GetWarehouseItemRosterWeight(settlement) + equipmentElement.Weight <= (float)Campaign.Current.Models.WorkshopModel.WarehouseCapacity;
		}

		// Token: 0x060047F0 RID: 18416 RVA: 0x0016A56B File Offset: 0x0016876B
		private bool IsWarehouseAtLimit(Settlement settlement)
		{
			return this.GetWarehouseItemRosterWeight(settlement) >= (float)Campaign.Current.Models.WorkshopModel.WarehouseCapacity;
		}

		// Token: 0x060047F1 RID: 18417 RVA: 0x0016A590 File Offset: 0x00168790
		private void AddNewWorkshopData(Workshop workshop)
		{
			for (int i = 0; i < this._workshopData.Length; i++)
			{
				if (this._workshopData[i] == null)
				{
					this._workshopData[i] = new WorkshopsCampaignBehavior.WorkshopData(workshop);
					return;
				}
			}
		}

		// Token: 0x060047F2 RID: 18418 RVA: 0x0016A5CC File Offset: 0x001687CC
		private void RemoveWorkshopData(Workshop workshop)
		{
			for (int i = 0; i < this._workshopData.Length; i++)
			{
				WorkshopsCampaignBehavior.WorkshopData workshopData = this._workshopData[i];
				if (workshopData != null && workshopData.Workshop == workshop)
				{
					this._workshopData[i] = null;
					return;
				}
			}
		}

		// Token: 0x060047F3 RID: 18419 RVA: 0x0016A60C File Offset: 0x0016880C
		private ItemRoster GetWarehouseRoster(Settlement settlement)
		{
			foreach (KeyValuePair<Settlement, ItemRoster> keyValuePair in this._warehouseRosterPerSettlement)
			{
				if (keyValuePair.Key == settlement)
				{
					return keyValuePair.Value;
				}
			}
			return null;
		}

		// Token: 0x060047F4 RID: 18420 RVA: 0x0016A64C File Offset: 0x0016884C
		private void FillItemsInAllCategories()
		{
			foreach (ItemObject itemObject in Game.Current.ObjectManager.GetObjectTypeList<ItemObject>())
			{
				if (this.IsProducable(itemObject))
				{
					ItemCategory itemCategory = itemObject.ItemCategory;
					if (itemCategory != null)
					{
						List<ItemObject> list;
						if (!this._itemsInCategory.TryGetValue(itemCategory, out list))
						{
							list = new List<ItemObject>();
							this._itemsInCategory[itemCategory] = list;
						}
						list.Add(itemObject);
					}
				}
			}
		}

		// Token: 0x060047F5 RID: 18421 RVA: 0x0016A6E0 File Offset: 0x001688E0
		private bool IsProducable(ItemObject item)
		{
			return !item.MultiplayerItem && !item.NotMerchandise && !item.IsCraftedByPlayer;
		}

		// Token: 0x060047F6 RID: 18422 RVA: 0x0016A700 File Offset: 0x00168900
		private void RemoveWarehouseData(Settlement settlement)
		{
			for (int i = 0; i < this._warehouseRosterPerSettlement.Length; i++)
			{
				if (this._warehouseRosterPerSettlement[i].Key == settlement)
				{
					this._warehouseRosterPerSettlement[i] = new KeyValuePair<Settlement, ItemRoster>(null, null);
					return;
				}
			}
		}

		// Token: 0x060047F7 RID: 18423 RVA: 0x0016A748 File Offset: 0x00168948
		private void AddNewWarehouseDataIfNeeded(Settlement settlement)
		{
			bool flag = false;
			foreach (KeyValuePair<Settlement, ItemRoster> keyValuePair in this._warehouseRosterPerSettlement)
			{
				if (keyValuePair.Key == settlement)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				for (int j = 0; j < this._warehouseRosterPerSettlement.Length; j++)
				{
					KeyValuePair<Settlement, ItemRoster> keyValuePair2 = this._warehouseRosterPerSettlement[j];
					if (keyValuePair2.Value == null)
					{
						this._warehouseRosterPerSettlement[j] = new KeyValuePair<Settlement, ItemRoster>(settlement, new ItemRoster());
						return;
					}
				}
			}
		}

		// Token: 0x060047F8 RID: 18424 RVA: 0x0016A7D0 File Offset: 0x001689D0
		private EquipmentElement GetRandomItem(ItemCategory itemGroupBase, Town townComponent)
		{
			EquipmentElement randomItemAux = this.GetRandomItemAux(itemGroupBase, townComponent);
			if (randomItemAux.Item != null)
			{
				return randomItemAux;
			}
			return this.GetRandomItemAux(itemGroupBase, null);
		}

		// Token: 0x060047F9 RID: 18425 RVA: 0x0016A7FC File Offset: 0x001689FC
		private EquipmentElement GetRandomItemAux(ItemCategory itemGroupBase, Town townComponent = null)
		{
			ItemObject itemObject = null;
			ItemModifier itemModifier = null;
			List<ValueTuple<ItemObject, float>> list = new List<ValueTuple<ItemObject, float>>();
			List<ItemObject> list2;
			if (this._itemsInCategory.TryGetValue(itemGroupBase, out list2))
			{
				foreach (ItemObject itemObject2 in list2)
				{
					if ((townComponent == null || this.IsItemPreferredForTown(itemObject2, townComponent)) && itemObject2.ItemCategory == itemGroupBase)
					{
						float num = 1f / ((float)MathF.Max(100, itemObject2.Value) + 100f);
						list.Add(new ValueTuple<ItemObject, float>(itemObject2, num));
					}
				}
				itemObject = MBRandom.ChooseWeighted<ItemObject>(list);
				ItemModifierGroup itemModifierGroup;
				if (itemObject == null)
				{
					itemModifierGroup = null;
				}
				else
				{
					ItemComponent itemComponent = itemObject.ItemComponent;
					itemModifierGroup = ((itemComponent != null) ? itemComponent.ItemModifierGroup : null);
				}
				ItemModifierGroup itemModifierGroup2 = itemModifierGroup;
				if (itemModifierGroup2 != null)
				{
					itemModifier = itemModifierGroup2.GetRandomItemModifierProductionScoreBased();
				}
			}
			return new EquipmentElement(itemObject, itemModifier, null, false);
		}

		// Token: 0x060047FA RID: 18426 RVA: 0x0016A8DC File Offset: 0x00168ADC
		private void TransferPlayerWorkshopsIfNeeded()
		{
			int count = Hero.MainHero.OwnedWorkshops.Count;
			List<Workshop> list = Hero.MainHero.OwnedWorkshops.ToList<Workshop>();
			for (int i = 0; i < count; i++)
			{
				Workshop workshop = list[i];
				if (workshop.Settlement.MapFaction.IsAtWarWith(Hero.MainHero.MapFaction))
				{
					Hero notableOwnerForWorkshop = Campaign.Current.Models.WorkshopModel.GetNotableOwnerForWorkshop(workshop);
					if (notableOwnerForWorkshop != null)
					{
						WorkshopType workshopType = this.DecideBestWorkshopType(workshop.Settlement, false, workshop.WorkshopType);
						ChangeOwnerOfWorkshopAction.ApplyByWar(workshop, notableOwnerForWorkshop, workshopType);
					}
				}
			}
		}

		// Token: 0x060047FB RID: 18427 RVA: 0x0016A974 File Offset: 0x00168B74
		private void ChangeWorkshopOwnerByBankruptcy(Workshop workshop)
		{
			int costForNotable = Campaign.Current.Models.WorkshopModel.GetCostForNotable(workshop);
			Hero notableOwnerForWorkshop = Campaign.Current.Models.WorkshopModel.GetNotableOwnerForWorkshop(workshop);
			WorkshopType workshopType = this.DecideBestWorkshopType(workshop.Settlement, false, workshop.WorkshopType);
			ChangeOwnerOfWorkshopAction.ApplyByBankruptcy(workshop, notableOwnerForWorkshop, workshopType, costForNotable);
		}

		// Token: 0x060047FC RID: 18428 RVA: 0x0016A9CA File Offset: 0x00168BCA
		private void HandleDailyExpense(Workshop shop)
		{
			if (!shop.WorkshopType.IsHidden)
			{
				if (shop.Owner != Hero.MainHero)
				{
					this.HandleNotableWorkshopExpense(shop);
					return;
				}
				this.HandlePlayerWorkshopExpense(shop);
			}
		}

		// Token: 0x060047FD RID: 18429 RVA: 0x0016A9F8 File Offset: 0x00168BF8
		private float FindTotalInputDensityScore(Settlement settlement, WorkshopType workshopType, IDictionary<ItemCategory, float> productionDict, bool atGameStart)
		{
			float num = 0f;
			for (int i = 0; i < settlement.Town.Workshops.Length; i++)
			{
				Workshop workshop = settlement.Town.Workshops[i];
				if (workshop.WorkshopType != null && !workshop.WorkshopType.IsHidden)
				{
					if (workshop.WorkshopType == workshopType)
					{
						num += 1f;
					}
					else
					{
						ValueTuple<float, float> inputOutputSimilarityForWorkshopTypes = this.GetInputOutputSimilarityForWorkshopTypes(workshopType, workshop.WorkshopType);
						float item = inputOutputSimilarityForWorkshopTypes.Item1;
						float item2 = inputOutputSimilarityForWorkshopTypes.Item2;
						num += item;
						num += item2;
					}
				}
			}
			float num2 = 0.01f;
			float num3 = 0f;
			foreach (WorkshopType.Production production in workshopType.Productions)
			{
				bool flag = false;
				using (List<ValueTuple<ItemCategory, int>>.Enumerator enumerator2 = production.Outputs.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.Item1.IsTradeGood)
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					foreach (ValueTuple<ItemCategory, int> valueTuple in production.Inputs)
					{
						ItemCategory item3 = valueTuple.Item1;
						int item4 = valueTuple.Item2;
						float num4;
						if (productionDict.TryGetValue(item3, out num4))
						{
							num2 += num4 / (production.ConversionSpeed * (float)item4);
						}
						if (!atGameStart)
						{
							float priceFactor = settlement.Town.MarketData.GetPriceFactor(item3);
							num3 += Math.Max(0f, 1f - priceFactor);
						}
					}
				}
			}
			float num5 = 1f + num * 6f;
			num2 *= (float)workshopType.Frequency * (1f / (float)Math.Pow((double)num5, 3.0));
			num2 += num3;
			num2 = MathF.Pow(num2, 0.6f);
			return num2;
		}

		// Token: 0x060047FE RID: 18430 RVA: 0x0016AC0C File Offset: 0x00168E0C
		private ValueTuple<float, float> GetInputOutputSimilarityForWorkshopTypes(WorkshopType workshop, WorkshopType otherWorkshop)
		{
			List<ItemCategory> list = new List<ItemCategory>();
			List<ItemCategory> list2 = new List<ItemCategory>();
			ValueTuple<Dictionary<ItemCategory, float>, Dictionary<ItemCategory, float>> inputOutputProductionForWorkshop = this.GetInputOutputProductionForWorkshop(workshop, ref list2, ref list);
			Dictionary<ItemCategory, float> item = inputOutputProductionForWorkshop.Item1;
			Dictionary<ItemCategory, float> item2 = inputOutputProductionForWorkshop.Item2;
			ValueTuple<Dictionary<ItemCategory, float>, Dictionary<ItemCategory, float>> inputOutputProductionForWorkshop2 = this.GetInputOutputProductionForWorkshop(otherWorkshop, ref list2, ref list);
			Dictionary<ItemCategory, float> item3 = inputOutputProductionForWorkshop2.Item1;
			Dictionary<ItemCategory, float> item4 = inputOutputProductionForWorkshop2.Item2;
			float num = item.SumQ<KeyValuePair<ItemCategory, float>>((KeyValuePair<ItemCategory, float> x) => x.Value);
			float num2 = item3.SumQ<KeyValuePair<ItemCategory, float>>((KeyValuePair<ItemCategory, float> x) => x.Value);
			float num3 = 0f;
			foreach (ItemCategory itemCategory in list2)
			{
				if (item.ContainsKey(itemCategory) && item3.ContainsKey(itemCategory))
				{
					float num4 = item[itemCategory] / num;
					float num5 = item3[itemCategory] / num2;
					num3 += Math.Min(num4, num5);
				}
			}
			float num6 = item2.SumQ<KeyValuePair<ItemCategory, float>>((KeyValuePair<ItemCategory, float> x) => x.Value);
			float num7 = item4.SumQ<KeyValuePair<ItemCategory, float>>((KeyValuePair<ItemCategory, float> x) => x.Value);
			float num8 = 0f;
			foreach (ItemCategory itemCategory2 in list)
			{
				if (item2.ContainsKey(itemCategory2) && item4.ContainsKey(itemCategory2))
				{
					float num9 = item2[itemCategory2] / num6;
					float num10 = item4[itemCategory2] / num7;
					num8 += Math.Min(num9, num10);
				}
			}
			return new ValueTuple<float, float>(num3, num8);
		}

		// Token: 0x060047FF RID: 18431 RVA: 0x0016ADFC File Offset: 0x00168FFC
		private ValueTuple<Dictionary<ItemCategory, float>, Dictionary<ItemCategory, float>> GetInputOutputProductionForWorkshop(WorkshopType workshop, ref List<ItemCategory> allInputItems, ref List<ItemCategory> allOutputItems)
		{
			Dictionary<ItemCategory, float> dictionary = new Dictionary<ItemCategory, float>();
			Dictionary<ItemCategory, float> dictionary2 = new Dictionary<ItemCategory, float>();
			foreach (WorkshopType.Production production in workshop.Productions)
			{
				foreach (ValueTuple<ItemCategory, int> valueTuple in production.Inputs)
				{
					if (valueTuple.Item1.IsTradeGood)
					{
						if (dictionary.ContainsKey(valueTuple.Item1))
						{
							Dictionary<ItemCategory, float> dictionary3 = dictionary;
							ItemCategory itemCategory = valueTuple.Item1;
							dictionary3[itemCategory] += (float)valueTuple.Item2 * production.ConversionSpeed;
						}
						else
						{
							dictionary.Add(valueTuple.Item1, (float)valueTuple.Item2 * production.ConversionSpeed);
						}
						if (!allInputItems.Contains(valueTuple.Item1))
						{
							allInputItems.Add(valueTuple.Item1);
						}
					}
				}
				foreach (ValueTuple<ItemCategory, int> valueTuple2 in production.Outputs)
				{
					if (valueTuple2.Item1.IsTradeGood)
					{
						if (dictionary2.ContainsKey(valueTuple2.Item1))
						{
							Dictionary<ItemCategory, float> dictionary3 = dictionary2;
							ItemCategory itemCategory = valueTuple2.Item1;
							dictionary3[itemCategory] += (float)valueTuple2.Item2 * production.ConversionSpeed;
						}
						else
						{
							dictionary2.Add(valueTuple2.Item1, (float)valueTuple2.Item2 * production.ConversionSpeed);
						}
						if (!allOutputItems.Contains(valueTuple2.Item1))
						{
							allOutputItems.Add(valueTuple2.Item1);
						}
					}
				}
			}
			return new ValueTuple<Dictionary<ItemCategory, float>, Dictionary<ItemCategory, float>>(dictionary, dictionary2);
		}

		// Token: 0x06004800 RID: 18432 RVA: 0x0016B020 File Offset: 0x00169220
		private void BuildWorkshopForHeroAtGameStart(Hero ownerHero)
		{
			Settlement bornSettlement = ownerHero.BornSettlement;
			WorkshopType workshopType = this.DecideBestWorkshopType(bornSettlement, true, null);
			if (workshopType != null)
			{
				int num = -1;
				for (int i = 0; i < bornSettlement.Town.Workshops.Length; i++)
				{
					if (bornSettlement.Town.Workshops[i].WorkshopType == null)
					{
						num = i;
						break;
					}
				}
				if (num >= 0)
				{
					InitializeWorkshopAction.ApplyByNewGame(bornSettlement.Town.Workshops[num], ownerHero, workshopType);
				}
			}
		}

		// Token: 0x06004801 RID: 18433 RVA: 0x0016B094 File Offset: 0x00169294
		private WorkshopType DecideBestWorkshopType(Settlement currentSettlement, bool atGameStart, WorkshopType workshopToExclude = null)
		{
			IDictionary<ItemCategory, float> dictionary = new Dictionary<ItemCategory, float>();
			foreach (Village village in Village.All.Where<Village>((Village x) => x.TradeBound == currentSettlement))
			{
				foreach (ValueTuple<ItemObject, float> valueTuple in village.VillageType.Productions)
				{
					ItemCategory itemCategory = valueTuple.Item1.ItemCategory;
					if (itemCategory != DefaultItemCategories.Grain || village.VillageType.PrimaryProduction == DefaultItems.Grain)
					{
						float item = valueTuple.Item2;
						if (itemCategory == DefaultItemCategories.Cow)
						{
							itemCategory = DefaultItemCategories.Hides;
						}
						if (itemCategory == DefaultItemCategories.Sheep)
						{
							itemCategory = DefaultItemCategories.Wool;
						}
						float num;
						if (dictionary.TryGetValue(itemCategory, out num))
						{
							dictionary[itemCategory] = num + item;
						}
						else
						{
							dictionary.Add(itemCategory, item);
						}
					}
				}
			}
			Dictionary<WorkshopType, float> dictionary2 = new Dictionary<WorkshopType, float>();
			float num2 = 0f;
			foreach (WorkshopType workshopType in WorkshopType.All)
			{
				if (!workshopType.IsHidden && (workshopToExclude == null || workshopToExclude != workshopType))
				{
					float num3 = this.FindTotalInputDensityScore(currentSettlement, workshopType, dictionary, atGameStart);
					dictionary2.Add(workshopType, num3);
					num2 += num3;
				}
			}
			float num4 = num2 * MBRandom.RandomFloat;
			WorkshopType workshopType2 = null;
			foreach (WorkshopType workshopType3 in WorkshopType.All)
			{
				if (!workshopType3.IsHidden && (workshopToExclude == null || workshopToExclude != workshopType3))
				{
					num4 -= dictionary2[workshopType3];
					if (num4 < 0f)
					{
						workshopType2 = workshopType3;
						break;
					}
				}
			}
			if (workshopType2 == null)
			{
				workshopType2 = WorkshopType.All[MBRandom.RandomInt(1, WorkshopType.All.Count)];
			}
			return workshopType2;
		}

		// Token: 0x06004802 RID: 18434 RVA: 0x0016B2DC File Offset: 0x001694DC
		private void InitializeWorkshops()
		{
			foreach (Town town in Town.AllTowns)
			{
				town.InitializeWorkshops(Campaign.Current.Models.WorkshopModel.DefaultWorkshopCountInSettlement);
			}
		}

		// Token: 0x06004803 RID: 18435 RVA: 0x0016B340 File Offset: 0x00169540
		private void BuildWorkshopsAtGameStart()
		{
			foreach (Town town in Town.AllTowns)
			{
				this.BuildArtisanWorkshop(town);
				for (int i = 1; i < town.Workshops.Length; i++)
				{
					Hero notableOwnerForWorkshop = Campaign.Current.Models.WorkshopModel.GetNotableOwnerForWorkshop(town.Workshops[i]);
					this.BuildWorkshopForHeroAtGameStart(notableOwnerForWorkshop);
				}
			}
		}

		// Token: 0x06004804 RID: 18436 RVA: 0x0016B3CC File Offset: 0x001695CC
		private void BuildArtisanWorkshop(Town town)
		{
			Hero hero = town.Settlement.Notables.FirstOrDefault<Hero>((Hero x) => x.IsArtisan);
			if (hero == null)
			{
				hero = town.Settlement.Notables.FirstOrDefault<Hero>();
			}
			if (hero != null)
			{
				WorkshopType workshopType = WorkshopType.Find("artisans");
				town.Workshops[0].InitializeWorkshop(hero, workshopType);
			}
		}

		// Token: 0x06004805 RID: 18437 RVA: 0x0016B43C File Offset: 0x0016963C
		private void RunTownShopsAtGameStart()
		{
			foreach (Town town in Town.AllTowns)
			{
				foreach (Workshop workshop in town.Workshops)
				{
					this.RunTownWorkshop(town, workshop);
				}
			}
		}

		// Token: 0x06004806 RID: 18438 RVA: 0x0016B4AC File Offset: 0x001696AC
		private void RunTownWorkshop(Town townComponent, Workshop workshop)
		{
			WorkshopType workshopType = workshop.WorkshopType;
			bool flag = false;
			for (int i = 0; i < workshopType.Productions.Count; i++)
			{
				float num = workshop.GetProductionProgress(i);
				if (num > 1f)
				{
					num = 1f;
				}
				num += Campaign.Current.Models.WorkshopModel.GetEffectiveConversionSpeedOfProduction(workshop, workshopType.Productions[i].ConversionSpeed, false).ResultNumber;
				if (num >= 1f)
				{
					bool flag2 = true;
					while (flag2 && num >= 1f)
					{
						WorkshopType.Production production = workshopType.Productions[i];
						bool flag3;
						if (!production.Inputs.Any<ValueTuple<ItemCategory, int>>((ValueTuple<ItemCategory, int> x) => !x.Item1.IsTradeGood))
						{
							flag3 = !production.Outputs.Any<ValueTuple<ItemCategory, int>>((ValueTuple<ItemCategory, int> x) => !x.Item1.IsTradeGood);
						}
						else
						{
							flag3 = false;
						}
						bool flag4 = flag3;
						flag2 = ((workshop.Owner == Hero.MainHero) ? this.TickOneProductionCycleForPlayerWorkshop(production, workshop, flag4) : this.TickOneProductionCycleForNotableWorkshop(production, workshop, flag4));
						if (flag2 && flag4)
						{
							flag = true;
						}
						num -= 1f;
					}
				}
				workshop.SetProgress(i, num);
			}
			if (flag)
			{
				workshop.UpdateLastRunTime();
			}
		}

		// Token: 0x06004807 RID: 18439 RVA: 0x0016B604 File Offset: 0x00169804
		public void TransferWarehouseToPlayerParty(Settlement settlement)
		{
			foreach (ItemRosterElement itemRosterElement in this.GetWarehouseRoster(settlement))
			{
				MobileParty.MainParty.ItemRoster.AddToCounts(itemRosterElement.EquipmentElement, itemRosterElement.Amount);
			}
			this.RemoveWarehouseData(settlement);
		}

		// Token: 0x040013FC RID: 5116
		private KeyValuePair<Settlement, ItemRoster>[] _warehouseRosterPerSettlement;

		// Token: 0x040013FD RID: 5117
		private WorkshopsCampaignBehavior.WorkshopData[] _workshopData;

		// Token: 0x040013FE RID: 5118
		private readonly Dictionary<ItemCategory, List<ItemObject>> _itemsInCategory = new Dictionary<ItemCategory, List<ItemObject>>();

		// Token: 0x02000874 RID: 2164
		public class WorkshopsCampaignBehaviorTypeDefiner : SaveableTypeDefiner
		{
			// Token: 0x06006876 RID: 26742 RVA: 0x001CA5D0 File Offset: 0x001C87D0
			public WorkshopsCampaignBehaviorTypeDefiner()
				: base(155828)
			{
			}

			// Token: 0x06006877 RID: 26743 RVA: 0x001CA5DD File Offset: 0x001C87DD
			protected override void DefineClassTypes()
			{
				base.AddClassDefinition(typeof(WorkshopsCampaignBehavior.WorkshopData), 10, null);
			}

			// Token: 0x06006878 RID: 26744 RVA: 0x001CA5F2 File Offset: 0x001C87F2
			protected override void DefineContainerDefinitions()
			{
				base.ConstructContainerDefinition(typeof(Dictionary<Workshop, WorkshopsCampaignBehavior.WorkshopData>));
				base.ConstructContainerDefinition(typeof(WorkshopsCampaignBehavior.WorkshopData[]));
			}
		}

		// Token: 0x02000875 RID: 2165
		internal class WorkshopData
		{
			// Token: 0x06006879 RID: 26745 RVA: 0x001CA614 File Offset: 0x001C8814
			public WorkshopData(Workshop workshop)
			{
				this.Workshop = workshop;
			}

			// Token: 0x0600687A RID: 26746 RVA: 0x001CA623 File Offset: 0x001C8823
			public override string ToString()
			{
				return this.Workshop.WorkshopType.ToString() + " in " + this.Workshop.Settlement.GetName();
			}

			// Token: 0x0600687B RID: 26747 RVA: 0x001CA64F File Offset: 0x001C884F
			internal static void AutoGeneratedStaticCollectObjectsWorkshopData(object o, List<object> collectedObjects)
			{
				((WorkshopsCampaignBehavior.WorkshopData)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x0600687C RID: 26748 RVA: 0x001CA65D File Offset: 0x001C885D
			protected virtual void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				collectedObjects.Add(this.Workshop);
			}

			// Token: 0x0600687D RID: 26749 RVA: 0x001CA66B File Offset: 0x001C886B
			internal static object AutoGeneratedGetMemberValueWorkshop(object o)
			{
				return ((WorkshopsCampaignBehavior.WorkshopData)o).Workshop;
			}

			// Token: 0x0600687E RID: 26750 RVA: 0x001CA678 File Offset: 0x001C8878
			internal static object AutoGeneratedGetMemberValueIsGettingInputsFromWarehouse(object o)
			{
				return ((WorkshopsCampaignBehavior.WorkshopData)o).IsGettingInputsFromWarehouse;
			}

			// Token: 0x0600687F RID: 26751 RVA: 0x001CA68A File Offset: 0x001C888A
			internal static object AutoGeneratedGetMemberValueProductionProgressForWarehouse(object o)
			{
				return ((WorkshopsCampaignBehavior.WorkshopData)o).ProductionProgressForWarehouse;
			}

			// Token: 0x06006880 RID: 26752 RVA: 0x001CA69C File Offset: 0x001C889C
			internal static object AutoGeneratedGetMemberValueProductionProgressForTown(object o)
			{
				return ((WorkshopsCampaignBehavior.WorkshopData)o).ProductionProgressForTown;
			}

			// Token: 0x06006881 RID: 26753 RVA: 0x001CA6AE File Offset: 0x001C88AE
			internal static object AutoGeneratedGetMemberValueStockProductionInWarehouseRatio(object o)
			{
				return ((WorkshopsCampaignBehavior.WorkshopData)o).StockProductionInWarehouseRatio;
			}

			// Token: 0x04002446 RID: 9286
			[SaveableField(1)]
			public Workshop Workshop;

			// Token: 0x04002447 RID: 9287
			[SaveableField(2)]
			public bool IsGettingInputsFromWarehouse;

			// Token: 0x04002448 RID: 9288
			[SaveableField(3)]
			public float ProductionProgressForWarehouse;

			// Token: 0x04002449 RID: 9289
			[SaveableField(4)]
			public float ProductionProgressForTown;

			// Token: 0x0400244A RID: 9290
			[SaveableField(5)]
			public float StockProductionInWarehouseRatio;
		}
	}
}
