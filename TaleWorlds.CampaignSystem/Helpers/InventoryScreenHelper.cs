using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.Inventory;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace Helpers
{
	// Token: 0x02000023 RID: 35
	public static class InventoryScreenHelper
	{
		// Token: 0x0600013B RID: 315 RVA: 0x0000F378 File Offset: 0x0000D578
		public static InventoryState GetActiveInventoryState()
		{
			GameStateManager gameStateManager = GameStateManager.Current;
			InventoryState inventoryState;
			if ((inventoryState = ((gameStateManager != null) ? gameStateManager.ActiveState : null) as InventoryState) != null)
			{
				return inventoryState;
			}
			Debug.FailedAssert("GetActiveInventoryState requested but the active state is not InventoryState!", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Helpers.cs", "GetActiveInventoryState", 8663);
			return null;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000F3BB File Offset: 0x0000D5BB
		public static void PlayerAcceptTradeOffer()
		{
			InventoryState activeInventoryState = InventoryScreenHelper.GetActiveInventoryState();
			if (activeInventoryState == null)
			{
				return;
			}
			InventoryLogic inventoryLogic = activeInventoryState.InventoryLogic;
			if (inventoryLogic == null)
			{
				return;
			}
			inventoryLogic.SetPlayerAcceptTraderOffer();
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000F3D6 File Offset: 0x0000D5D6
		public static void CloseScreen(bool fromCancel)
		{
			InventoryScreenHelper.CloseInventoryPresentation(fromCancel);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000F3E0 File Offset: 0x0000D5E0
		private static void CloseInventoryPresentation(bool fromCancel)
		{
			InventoryState activeInventoryState = InventoryScreenHelper.GetActiveInventoryState();
			InventoryLogic inventoryLogic = ((activeInventoryState != null) ? activeInventoryState.InventoryLogic : null);
			if (fromCancel && inventoryLogic != null)
			{
				inventoryLogic.Reset(fromCancel);
			}
			if (inventoryLogic != null && inventoryLogic.DoneLogic())
			{
				Action doneLogicExtrasDelegate = activeInventoryState.DoneLogicExtrasDelegate;
				if (doneLogicExtrasDelegate != null)
				{
					doneLogicExtrasDelegate();
				}
				activeInventoryState.DoneLogicExtrasDelegate = null;
				activeInventoryState.InventoryLogic = null;
				Game.Current.GameStateManager.PopState(0);
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000F448 File Offset: 0x0000D648
		private static void OpenInventoryPresentation(TextObject leftRosterName, Action doneLogicExtrasDelegate = null)
		{
			ItemRoster itemRoster = new ItemRoster();
			if (Game.Current.CheatMode)
			{
				TestCommonBase baseInstance = TestCommonBase.BaseInstance;
				if (baseInstance == null || !baseInstance.IsTestEnabled)
				{
					MBReadOnlyList<ItemObject> objectTypeList = Game.Current.ObjectManager.GetObjectTypeList<ItemObject>();
					for (int num = 0; num != objectTypeList.Count; num++)
					{
						ItemObject itemObject = objectTypeList[num];
						itemRoster.AddToCounts(itemObject, 10);
					}
				}
			}
			InventoryState inventoryState = Game.Current.GameStateManager.CreateState<InventoryState>();
			InventoryLogic inventoryLogic = new InventoryLogic(null);
			inventoryLogic.Initialize(itemRoster, MobileParty.MainParty, false, true, CharacterObject.PlayerCharacter, InventoryScreenHelper.InventoryCategoryType.None, InventoryScreenHelper.GetCurrentMarketDataForPlayer(), false, inventoryState.InventoryMode, leftRosterName, null, null);
			inventoryState.InventoryLogic = inventoryLogic;
			inventoryState.DoneLogicExtrasDelegate = doneLogicExtrasDelegate;
			Game.Current.GameStateManager.PushState(inventoryState, 0);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000F510 File Offset: 0x0000D710
		private static IMarketData GetCurrentMarketDataForPlayer()
		{
			IMarketData marketData = null;
			if (Campaign.Current.GameMode == CampaignGameMode.Campaign)
			{
				Settlement settlement = MobileParty.MainParty.CurrentSettlement;
				if (settlement == null)
				{
					Town town = SettlementHelper.FindNearestTownToMobileParty(MobileParty.MainParty, MobileParty.NavigationType.All, null);
					settlement = ((town != null) ? town.Settlement : null);
				}
				if (settlement != null)
				{
					if (settlement.IsVillage)
					{
						marketData = settlement.Village.MarketData;
					}
					else if (settlement.IsTown)
					{
						marketData = settlement.Town.MarketData;
					}
				}
			}
			if (marketData == null)
			{
				marketData = new FakeMarketData();
			}
			return marketData;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000F58C File Offset: 0x0000D78C
		public static void OpenScreenAsInventoryOfSubParty(MobileParty rightParty, MobileParty leftParty, Action doneLogicExtrasDelegate)
		{
			InventoryState inventoryState = Game.Current.GameStateManager.CreateState<InventoryState>();
			Hero leaderHero = rightParty.LeaderHero;
			InventoryLogic inventoryLogic = new InventoryLogic(rightParty, (leaderHero != null) ? leaderHero.CharacterObject : null, leftParty.Party);
			InventoryLogic inventoryLogic2 = inventoryLogic;
			ItemRoster itemRoster = leftParty.ItemRoster;
			ItemRoster itemRoster2 = rightParty.ItemRoster;
			TroopRoster memberRoster = rightParty.MemberRoster;
			bool flag = false;
			bool flag2 = false;
			Hero leaderHero2 = rightParty.LeaderHero;
			inventoryLogic2.Initialize(itemRoster, itemRoster2, memberRoster, flag, flag2, (leaderHero2 != null) ? leaderHero2.CharacterObject : null, InventoryScreenHelper.InventoryCategoryType.None, InventoryScreenHelper.GetCurrentMarketDataForPlayer(), false, inventoryState.InventoryMode, null, null, null);
			inventoryState.InventoryLogic = inventoryLogic;
			inventoryState.DoneLogicExtrasDelegate = doneLogicExtrasDelegate;
			Game.Current.GameStateManager.PushState(inventoryState, 0);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000F624 File Offset: 0x0000D824
		public static void OpenScreenAsInventoryForCraftedItemDecomposition(MobileParty party, CharacterObject character, Action doneLogicExtrasDelegate)
		{
			InventoryState inventoryState = Game.Current.GameStateManager.CreateState<InventoryState>();
			InventoryLogic inventoryLogic = new InventoryLogic(null);
			inventoryLogic.Initialize(new ItemRoster(), party.ItemRoster, party.MemberRoster, false, false, character, InventoryScreenHelper.InventoryCategoryType.None, InventoryScreenHelper.GetCurrentMarketDataForPlayer(), false, inventoryState.InventoryMode, null, null, null);
			inventoryState.InventoryLogic = inventoryLogic;
			inventoryState.DoneLogicExtrasDelegate = doneLogicExtrasDelegate;
			Game.Current.GameStateManager.PushState(inventoryState, 0);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000F694 File Offset: 0x0000D894
		public static void OpenScreenAsInventoryOf(MobileParty party, CharacterObject character)
		{
			InventoryState inventoryState = Game.Current.GameStateManager.CreateState<InventoryState>();
			InventoryLogic inventoryLogic = new InventoryLogic(null);
			inventoryLogic.Initialize(new ItemRoster(), party.ItemRoster, party.MemberRoster, false, true, character, InventoryScreenHelper.InventoryCategoryType.None, InventoryScreenHelper.GetCurrentMarketDataForPlayer(), false, inventoryState.InventoryMode, null, null, null);
			inventoryState.InventoryLogic = inventoryLogic;
			Game.Current.GameStateManager.PushState(inventoryState, 0);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000F6FA File Offset: 0x0000D8FA
		public static void OpenScreenAsInventoryOf(PartyBase rightParty, PartyBase leftParty)
		{
			Hero leaderHero = rightParty.LeaderHero;
			InventoryScreenHelper.OpenScreenAsInventoryOf(rightParty, leftParty, (leaderHero != null) ? leaderHero.CharacterObject : null, null, null, null);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000F718 File Offset: 0x0000D918
		public static void OpenScreenAsInventoryOf(PartyBase rightParty, PartyBase leftParty, CharacterObject character, TextObject leftRosterName = null, InventoryLogic.CapacityData capacityData = null, Action doneLogicExtrasDelegate = null)
		{
			InventoryState inventoryState = Game.Current.GameStateManager.CreateState<InventoryState>();
			InventoryLogic inventoryLogic = new InventoryLogic(leftParty);
			inventoryLogic.Initialize(leftParty.ItemRoster, rightParty.ItemRoster, rightParty.MemberRoster, false, false, character, InventoryScreenHelper.InventoryCategoryType.None, InventoryScreenHelper.GetCurrentMarketDataForPlayer(), false, InventoryScreenHelper.InventoryMode.Default, leftRosterName, leftParty.MemberRoster, capacityData);
			inventoryState.InventoryLogic = inventoryLogic;
			inventoryState.DoneLogicExtrasDelegate = doneLogicExtrasDelegate;
			Game.Current.GameStateManager.PushState(inventoryState, 0);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000F788 File Offset: 0x0000D988
		public static void OpenScreenAsInventory(Action doneLogicExtrasDelegate = null)
		{
			InventoryScreenHelper.OpenInventoryPresentation(new TextObject("{=02c5bQSM}Discard", null), doneLogicExtrasDelegate);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000F79C File Offset: 0x0000D99C
		public static void OpenScreenAsLoot(Dictionary<PartyBase, ItemRoster> itemRostersToLoot)
		{
			ItemRoster itemRoster = itemRostersToLoot[PartyBase.MainParty];
			InventoryState inventoryState = Game.Current.GameStateManager.CreateState<InventoryState>();
			inventoryState.InventoryMode = InventoryScreenHelper.InventoryMode.Loot;
			InventoryLogic inventoryLogic = new InventoryLogic(null);
			inventoryLogic.Initialize(itemRoster, MobileParty.MainParty.ItemRoster, MobileParty.MainParty.MemberRoster, false, true, CharacterObject.PlayerCharacter, InventoryScreenHelper.InventoryCategoryType.None, InventoryScreenHelper.GetCurrentMarketDataForPlayer(), false, inventoryState.InventoryMode, GameTexts.FindText("str_loot", null), null, null);
			inventoryState.InventoryLogic = inventoryLogic;
			Game.Current.GameStateManager.PushState(inventoryState, 0);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000F828 File Offset: 0x0000DA28
		public static void OpenScreenAsStash(ItemRoster stash)
		{
			InventoryState inventoryState = Game.Current.GameStateManager.CreateState<InventoryState>();
			inventoryState.InventoryMode = InventoryScreenHelper.InventoryMode.Stash;
			InventoryLogic inventoryLogic = new InventoryLogic(null);
			inventoryLogic.Initialize(stash, MobileParty.MainParty, false, false, CharacterObject.PlayerCharacter, InventoryScreenHelper.InventoryCategoryType.None, InventoryScreenHelper.GetCurrentMarketDataForPlayer(), false, inventoryState.InventoryMode, new TextObject("{=nZbaYvVx}Stash", null), null, null);
			inventoryState.InventoryLogic = inventoryLogic;
			Game.Current.GameStateManager.PushState(inventoryState, 0);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000F898 File Offset: 0x0000DA98
		public static void OpenScreenAsWarehouse(ItemRoster stash, InventoryLogic.CapacityData otherSideCapacity)
		{
			InventoryState inventoryState = Game.Current.GameStateManager.CreateState<InventoryState>();
			inventoryState.InventoryMode = InventoryScreenHelper.InventoryMode.Warehouse;
			InventoryLogic inventoryLogic = new InventoryLogic(null);
			inventoryLogic.Initialize(stash, MobileParty.MainParty, false, false, CharacterObject.PlayerCharacter, InventoryScreenHelper.InventoryCategoryType.None, InventoryScreenHelper.GetCurrentMarketDataForPlayer(), false, inventoryState.InventoryMode, new TextObject("{=anTRftmb}Warehouse", null), null, otherSideCapacity);
			inventoryState.InventoryLogic = inventoryLogic;
			Game.Current.GameStateManager.PushState(inventoryState, 0);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000F908 File Offset: 0x0000DB08
		public static void OpenScreenAsReceiveItems(ItemRoster items, TextObject leftRosterName, Action doneLogicDelegate = null)
		{
			InventoryState inventoryState = Game.Current.GameStateManager.CreateState<InventoryState>();
			InventoryLogic inventoryLogic = new InventoryLogic(null);
			inventoryLogic.Initialize(items, MobileParty.MainParty.ItemRoster, MobileParty.MainParty.MemberRoster, false, true, CharacterObject.PlayerCharacter, InventoryScreenHelper.InventoryCategoryType.None, InventoryScreenHelper.GetCurrentMarketDataForPlayer(), false, inventoryState.InventoryMode, leftRosterName, null, null);
			inventoryState.InventoryLogic = inventoryLogic;
			inventoryState.DoneLogicExtrasDelegate = doneLogicDelegate;
			Game.Current.GameStateManager.PushState(inventoryState, 0);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000F980 File Offset: 0x0000DB80
		public static void OpenTradeWithCaravanOrAlleyParty(MobileParty caravan, InventoryScreenHelper.InventoryCategoryType merchantItemType = InventoryScreenHelper.InventoryCategoryType.None)
		{
			InventoryState inventoryState = Game.Current.GameStateManager.CreateState<InventoryState>();
			inventoryState.InventoryMode = InventoryScreenHelper.InventoryMode.Trade;
			InventoryLogic inventoryLogic = new InventoryLogic(caravan.Party);
			inventoryLogic.Initialize(caravan.Party.ItemRoster, PartyBase.MainParty.ItemRoster, PartyBase.MainParty.MemberRoster, true, true, CharacterObject.PlayerCharacter, merchantItemType, InventoryScreenHelper.GetCurrentMarketDataForPlayer(), false, inventoryState.InventoryMode, null, null, null);
			inventoryLogic.SetInventoryListener(new InventoryScreenHelper.CaravanInventoryListener(caravan));
			inventoryState.InventoryLogic = inventoryLogic;
			Game.Current.GameStateManager.PushState(inventoryState, 0);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000FA10 File Offset: 0x0000DC10
		public static void ActivateTradeWithCurrentSettlement()
		{
			InventoryScreenHelper.OpenScreenAsTrade(Settlement.CurrentSettlement.ItemRoster, Settlement.CurrentSettlement.SettlementComponent, InventoryScreenHelper.InventoryCategoryType.None, null);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000FA30 File Offset: 0x0000DC30
		public static void OpenScreenAsTrade(ItemRoster leftRoster, SettlementComponent settlementComponent, InventoryScreenHelper.InventoryCategoryType merchantItemType = InventoryScreenHelper.InventoryCategoryType.None, Action doneLogicExtrasDelegate = null)
		{
			InventoryState inventoryState = Game.Current.GameStateManager.CreateState<InventoryState>();
			inventoryState.InventoryMode = InventoryScreenHelper.InventoryMode.Trade;
			InventoryLogic inventoryLogic = new InventoryLogic(settlementComponent.Owner);
			inventoryLogic.Initialize(leftRoster, PartyBase.MainParty.ItemRoster, PartyBase.MainParty.MemberRoster, true, true, CharacterObject.PlayerCharacter, merchantItemType, InventoryScreenHelper.GetCurrentMarketDataForPlayer(), false, inventoryState.InventoryMode, null, null, null);
			inventoryLogic.SetInventoryListener(new InventoryScreenHelper.MerchantInventoryListener(settlementComponent));
			inventoryState.InventoryLogic = inventoryLogic;
			inventoryState.DoneLogicExtrasDelegate = doneLogicExtrasDelegate;
			Game.Current.GameStateManager.PushState(inventoryState, 0);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000FAC0 File Offset: 0x0000DCC0
		public static InventoryScreenHelper.InventoryItemType GetInventoryItemTypeOfItem(ItemObject item)
		{
			if (item != null)
			{
				switch (item.ItemType)
				{
				case ItemObject.ItemTypeEnum.Horse:
					return InventoryScreenHelper.InventoryItemType.Horse;
				case ItemObject.ItemTypeEnum.OneHandedWeapon:
				case ItemObject.ItemTypeEnum.TwoHandedWeapon:
				case ItemObject.ItemTypeEnum.Polearm:
				case ItemObject.ItemTypeEnum.Arrows:
				case ItemObject.ItemTypeEnum.Bolts:
				case ItemObject.ItemTypeEnum.SlingStones:
				case ItemObject.ItemTypeEnum.Bow:
				case ItemObject.ItemTypeEnum.Crossbow:
				case ItemObject.ItemTypeEnum.Sling:
				case ItemObject.ItemTypeEnum.Thrown:
				case ItemObject.ItemTypeEnum.Pistol:
				case ItemObject.ItemTypeEnum.Musket:
				case ItemObject.ItemTypeEnum.Bullets:
					return InventoryScreenHelper.InventoryItemType.Weapon;
				case ItemObject.ItemTypeEnum.Shield:
					return InventoryScreenHelper.InventoryItemType.Shield;
				case ItemObject.ItemTypeEnum.Goods:
					return InventoryScreenHelper.InventoryItemType.Goods;
				case ItemObject.ItemTypeEnum.HeadArmor:
					return InventoryScreenHelper.InventoryItemType.HeadArmor;
				case ItemObject.ItemTypeEnum.BodyArmor:
					return InventoryScreenHelper.InventoryItemType.BodyArmor;
				case ItemObject.ItemTypeEnum.LegArmor:
					return InventoryScreenHelper.InventoryItemType.LegArmor;
				case ItemObject.ItemTypeEnum.HandArmor:
					return InventoryScreenHelper.InventoryItemType.HandArmor;
				case ItemObject.ItemTypeEnum.Animal:
					return InventoryScreenHelper.InventoryItemType.Animal;
				case ItemObject.ItemTypeEnum.Book:
					return InventoryScreenHelper.InventoryItemType.Book;
				case ItemObject.ItemTypeEnum.Cape:
					return InventoryScreenHelper.InventoryItemType.Cape;
				case ItemObject.ItemTypeEnum.HorseHarness:
					return InventoryScreenHelper.InventoryItemType.HorseHarness;
				case ItemObject.ItemTypeEnum.Banner:
					return InventoryScreenHelper.InventoryItemType.Banner;
				}
			}
			return InventoryScreenHelper.InventoryItemType.None;
		}

		// Token: 0x020004EB RID: 1259
		public enum InventoryMode
		{
			// Token: 0x0400151D RID: 5405
			Default,
			// Token: 0x0400151E RID: 5406
			Trade,
			// Token: 0x0400151F RID: 5407
			Loot,
			// Token: 0x04001520 RID: 5408
			Stash,
			// Token: 0x04001521 RID: 5409
			Warehouse
		}

		// Token: 0x020004EC RID: 1260
		// (Invoke) Token: 0x06004B8B RID: 19339
		public delegate void InventoryFinishDelegate();

		// Token: 0x020004ED RID: 1261
		[Flags]
		public enum InventoryItemType
		{
			// Token: 0x04001523 RID: 5411
			None = 0,
			// Token: 0x04001524 RID: 5412
			Weapon = 1,
			// Token: 0x04001525 RID: 5413
			Shield = 2,
			// Token: 0x04001526 RID: 5414
			HeadArmor = 4,
			// Token: 0x04001527 RID: 5415
			BodyArmor = 8,
			// Token: 0x04001528 RID: 5416
			LegArmor = 16,
			// Token: 0x04001529 RID: 5417
			HandArmor = 32,
			// Token: 0x0400152A RID: 5418
			Horse = 64,
			// Token: 0x0400152B RID: 5419
			HorseHarness = 128,
			// Token: 0x0400152C RID: 5420
			Goods = 256,
			// Token: 0x0400152D RID: 5421
			Book = 512,
			// Token: 0x0400152E RID: 5422
			Animal = 1024,
			// Token: 0x0400152F RID: 5423
			Cape = 2048,
			// Token: 0x04001530 RID: 5424
			Banner = 4096,
			// Token: 0x04001531 RID: 5425
			HorseCategory = 192,
			// Token: 0x04001532 RID: 5426
			Armors = 2108,
			// Token: 0x04001533 RID: 5427
			Equipable = 6399,
			// Token: 0x04001534 RID: 5428
			All = 4095
		}

		// Token: 0x020004EE RID: 1262
		public enum InventoryCategoryType
		{
			// Token: 0x04001536 RID: 5430
			None = -1,
			// Token: 0x04001537 RID: 5431
			All,
			// Token: 0x04001538 RID: 5432
			Armors,
			// Token: 0x04001539 RID: 5433
			Weapon,
			// Token: 0x0400153A RID: 5434
			Shield,
			// Token: 0x0400153B RID: 5435
			HorseCategory,
			// Token: 0x0400153C RID: 5436
			Goods,
			// Token: 0x0400153D RID: 5437
			CategoryTypeAmount
		}

		// Token: 0x020004EF RID: 1263
		private class CaravanInventoryListener : InventoryListener
		{
			// Token: 0x06004B8E RID: 19342 RVA: 0x0017D139 File Offset: 0x0017B339
			public CaravanInventoryListener(MobileParty caravan)
			{
				this._caravan = caravan;
			}

			// Token: 0x06004B8F RID: 19343 RVA: 0x0017D148 File Offset: 0x0017B348
			public override int GetGold()
			{
				return this._caravan.PartyTradeGold;
			}

			// Token: 0x06004B90 RID: 19344 RVA: 0x0017D155 File Offset: 0x0017B355
			public override TextObject GetTraderName()
			{
				if (this._caravan.LeaderHero == null)
				{
					return this._caravan.Name;
				}
				return this._caravan.LeaderHero.Name;
			}

			// Token: 0x06004B91 RID: 19345 RVA: 0x0017D180 File Offset: 0x0017B380
			public override void SetGold(int gold)
			{
				this._caravan.PartyTradeGold = gold;
			}

			// Token: 0x06004B92 RID: 19346 RVA: 0x0017D18E File Offset: 0x0017B38E
			public override PartyBase GetOppositeParty()
			{
				return this._caravan.Party;
			}

			// Token: 0x06004B93 RID: 19347 RVA: 0x0017D19B File Offset: 0x0017B39B
			public override void OnTransaction()
			{
				throw new NotImplementedException();
			}

			// Token: 0x0400153E RID: 5438
			private MobileParty _caravan;
		}

		// Token: 0x020004F0 RID: 1264
		private class MerchantInventoryListener : InventoryListener
		{
			// Token: 0x06004B94 RID: 19348 RVA: 0x0017D1A2 File Offset: 0x0017B3A2
			public MerchantInventoryListener(SettlementComponent settlementComponent)
			{
				this._settlementComponent = settlementComponent;
			}

			// Token: 0x06004B95 RID: 19349 RVA: 0x0017D1B1 File Offset: 0x0017B3B1
			public override TextObject GetTraderName()
			{
				return this._settlementComponent.Owner.Name;
			}

			// Token: 0x06004B96 RID: 19350 RVA: 0x0017D1C3 File Offset: 0x0017B3C3
			public override PartyBase GetOppositeParty()
			{
				return this._settlementComponent.Owner;
			}

			// Token: 0x06004B97 RID: 19351 RVA: 0x0017D1D0 File Offset: 0x0017B3D0
			public override int GetGold()
			{
				return this._settlementComponent.Gold;
			}

			// Token: 0x06004B98 RID: 19352 RVA: 0x0017D1DD File Offset: 0x0017B3DD
			public override void SetGold(int gold)
			{
				this._settlementComponent.ChangeGold(gold - this._settlementComponent.Gold);
			}

			// Token: 0x06004B99 RID: 19353 RVA: 0x0017D1F7 File Offset: 0x0017B3F7
			public override void OnTransaction()
			{
				throw new NotImplementedException();
			}

			// Token: 0x0400153F RID: 5439
			private SettlementComponent _settlementComponent;
		}
	}
}
