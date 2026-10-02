using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Issues;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;
using TaleWorlds.MountAndBlade;

namespace SandBox.CampaignBehaviors
{
	// Token: 0x020000E1 RID: 225
	public class StatisticsCampaignBehavior : CampaignBehaviorBase, IStatisticsCampaignBehavior, ICampaignBehavior
	{
		// Token: 0x06000A9D RID: 2717 RVA: 0x0004FE34 File Offset: 0x0004E034
		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<int>("_highestTournamentRank", ref this._highestTournamentRank);
			dataStore.SyncData<int>("_numberOfTournamentWins", ref this._numberOfTournamentWins);
			dataStore.SyncData<int>("_numberOfChildrenBorn", ref this._numberOfChildrenBorn);
			dataStore.SyncData<int>("_numberOfPrisonersRecruited", ref this._numberOfPrisonersRecruited);
			dataStore.SyncData<int>("_numberOfTroopsRecruited", ref this._numberOfTroopsRecruited);
			dataStore.SyncData<int>("_numberOfClansDefected", ref this._numberOfClansDefected);
			dataStore.SyncData<int>("_numberOfIssuesSolved", ref this._numberOfIssuesSolved);
			dataStore.SyncData<int>("_totalInfluenceEarned", ref this._totalInfluenceEarned);
			dataStore.SyncData<int>("_totalCrimeRatingGained", ref this._totalCrimeRatingGained);
			dataStore.SyncData<ulong>("_totalTimePlayedInSeconds", ref this._totalTimePlayedInSeconds);
			dataStore.SyncData<int>("_numberOfbattlesWon", ref this._numberOfbattlesWon);
			dataStore.SyncData<int>("_numberOfbattlesLost", ref this._numberOfbattlesLost);
			dataStore.SyncData<int>("_largestBattleWonAsLeader", ref this._largestBattleWonAsLeader);
			dataStore.SyncData<int>("_largestArmyFormedByPlayer", ref this._largestArmyFormedByPlayer);
			dataStore.SyncData<int>("_numberOfEnemyClansDestroyed", ref this._numberOfEnemyClansDestroyed);
			dataStore.SyncData<int>("_numberOfHeroesKilledInBattle", ref this._numberOfHeroesKilledInBattle);
			dataStore.SyncData<int>("_numberOfTroopsKnockedOrKilledAsParty", ref this._numberOfTroopsKnockedOrKilledAsParty);
			dataStore.SyncData<int>("_numberOfTroopsKnockedOrKilledByPlayer", ref this._numberOfTroopsKnockedOrKilledByPlayer);
			dataStore.SyncData<int>("_numberOfHeroPrisonersTaken", ref this._numberOfHeroPrisonersTaken);
			dataStore.SyncData<int>("_numberOfTroopPrisonersTaken", ref this._numberOfTroopPrisonersTaken);
			dataStore.SyncData<int>("_numberOfTownsCaptured", ref this._numberOfTownsCaptured);
			dataStore.SyncData<int>("_numberOfHideoutsCleared", ref this._numberOfHideoutsCleared);
			dataStore.SyncData<int>("_numberOfCastlesCaptured", ref this._numberOfCastlesCaptured);
			dataStore.SyncData<int>("_numberOfVillagesRaided", ref this._numberOfVillagesRaided);
			dataStore.SyncData<CampaignTime>("_timeSpentAsPrisoner", ref this._timeSpentAsPrisoner);
			dataStore.SyncData<ulong>("_totalDenarsEarned", ref this._totalDenarsEarned);
			dataStore.SyncData<ulong>("_denarsEarnedFromCaravans", ref this._denarsEarnedFromCaravans);
			dataStore.SyncData<ulong>("_denarsEarnedFromWorkshops", ref this._denarsEarnedFromWorkshops);
			dataStore.SyncData<ulong>("_denarsEarnedFromRansoms", ref this._denarsEarnedFromRansoms);
			dataStore.SyncData<ulong>("_denarsEarnedFromTaxes", ref this._denarsEarnedFromTaxes);
			dataStore.SyncData<ulong>("_denarsEarnedFromTributes", ref this._denarsEarnedFromTributes);
			dataStore.SyncData<ulong>("_denarsPaidAsTributes", ref this._denarsPaidAsTributes);
			dataStore.SyncData<int>("_numberOfCraftingPartsUnlocked", ref this._numberOfCraftingPartsUnlocked);
			dataStore.SyncData<int>("_numberOfWeaponsCrafted", ref this._numberOfWeaponsCrafted);
			dataStore.SyncData<int>("_numberOfCraftingOrdersCompleted", ref this._numberOfCraftingOrdersCompleted);
			dataStore.SyncData<ValueTuple<string, int>>("_mostExpensiveItemCrafted", ref this._mostExpensiveItemCrafted);
			dataStore.SyncData<int>("_numberOfCompanionsHired", ref this._numberOfCompanionsHired);
			dataStore.SyncData<Dictionary<Hero, ValueTuple<int, int>>>("_companionData", ref this._companionData);
			dataStore.SyncData<int>("_lastPlayerBattleSize", ref this._lastPlayerBattleSize);
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00050100 File Offset: 0x0004E300
		public override void RegisterEvents()
		{
			CampaignEvents.HeroCreated.AddNonSerializedListener(this, new Action<Hero, bool>(this.OnHeroCreated));
			CampaignEvents.OnIssueUpdatedEvent.AddNonSerializedListener(this, new Action<IssueBase, IssueBase.IssueUpdateDetails, Hero>(this.OnIssueUpdated));
			CampaignEvents.TournamentFinished.AddNonSerializedListener(this, new Action<CharacterObject, MBReadOnlyList<CharacterObject>, Town, ItemObject>(this.OnTournamentFinished));
			CampaignEvents.OnClanInfluenceChangedEvent.AddNonSerializedListener(this, new Action<Clan, float>(this.OnClanInfluenceChanged));
			CampaignEvents.OnAfterSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnAfterSessionLaunched));
			CampaignEvents.CrimeRatingChanged.AddNonSerializedListener(this, new Action<IFaction, float>(this.OnCrimeRatingChanged));
			CampaignEvents.OnMainPartyPrisonerRecruitedEvent.AddNonSerializedListener(this, new Action<FlattenedTroopRoster>(this.OnMainPartyPrisonerRecruited));
			CampaignEvents.OnUnitRecruitedEvent.AddNonSerializedListener(this, new Action<CharacterObject, int>(this.OnUnitRecruited));
			CampaignEvents.OnBeforeSaveEvent.AddNonSerializedListener(this, new Action(this.OnBeforeSave));
			CampaignEvents.CraftingPartUnlockedEvent.AddNonSerializedListener(this, new Action<CraftingPiece>(this.OnCraftingPartUnlocked));
			CampaignEvents.OnNewItemCraftedEvent.AddNonSerializedListener(this, new Action<ItemObject, ItemModifier, bool>(this.OnNewItemCrafted));
			CampaignEvents.NewCompanionAdded.AddNonSerializedListener(this, new Action<Hero>(this.OnNewCompanionAdded));
			CampaignEvents.HeroOrPartyTradedGold.AddNonSerializedListener(this, new Action<ValueTuple<Hero, PartyBase>, ValueTuple<Hero, PartyBase>, ValueTuple<int, string>, bool>(this.OnHeroOrPartyTradedGold));
			CampaignEvents.MapEventEnded.AddNonSerializedListener(this, new Action<MapEvent>(this.OnMapEventEnd));
			CampaignEvents.OnClanDestroyedEvent.AddNonSerializedListener(this, new Action<Clan>(this.OnClanDestroyed));
			CampaignEvents.PartyAttachedAnotherParty.AddNonSerializedListener(this, new Action<MobileParty>(this.OnPartyAttachedAnotherParty));
			CampaignEvents.ArmyCreated.AddNonSerializedListener(this, new Action<Army>(this.OnArmyCreated));
			CampaignEvents.HeroPrisonerTaken.AddNonSerializedListener(this, new Action<PartyBase, Hero>(this.OnHeroPrisonerTaken));
			CampaignEvents.OnPrisonerTakenEvent.AddNonSerializedListener(this, new Action<FlattenedTroopRoster>(this.OnPrisonersTaken));
			CampaignEvents.RaidCompletedEvent.AddNonSerializedListener(this, new Action<BattleSideEnum, RaidEventComponent>(this.OnRaidCompleted));
			CampaignEvents.OnHideoutBattleCompletedEvent.AddNonSerializedListener(this, new Action<BattleSideEnum, HideoutEventComponent, HideoutEventComponent.HideoutBattleEndState>(this.OnHideoutBattleCompleted));
			CampaignEvents.MapEventStarted.AddNonSerializedListener(this, new Action<MapEvent, PartyBase, PartyBase>(this.OnMapEventStarted));
			CampaignEvents.HeroPrisonerReleased.AddNonSerializedListener(this, new Action<Hero, PartyBase, IFaction, EndCaptivityDetail, bool>(this.OnHeroPrisonerReleased));
			CampaignEvents.OnPlayerPartyKnockedOrKilledTroopEvent.AddNonSerializedListener(this, new Action<CharacterObject>(this.OnPlayerPartyKnockedOrKilledTroop));
			CampaignEvents.OnMissionStartedEvent.AddNonSerializedListener(this, new Action<IMission>(this.OnMissionStarted));
			CampaignEvents.OnPlayerEarnedGoldFromAssetEvent.AddNonSerializedListener(this, new Action<DefaultClanFinanceModel.AssetIncomeType, int>(this.OnPlayerEarnedGoldFromAsset));
			CampaignEvents.HeroKilledEvent.AddNonSerializedListener(this, new Action<Hero, Hero, KillCharacterAction.KillCharacterActionDetail, bool>(this.OnHeroKilled));
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0005037A File Offset: 0x0004E57A
		private void OnBeforeSave()
		{
			this.UpdateTotalTimePlayedInSeconds();
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00050382 File Offset: 0x0004E582
		private void OnAfterSessionLaunched(CampaignGameStarter starter)
		{
			this._lastGameplayTimeCheck = DateTime.Now;
			if (this._highestTournamentRank == 0)
			{
				this._highestTournamentRank = Campaign.Current.TournamentManager.GetLeaderBoardRank(Hero.MainHero);
			}
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x000503B1 File Offset: 0x0004E5B1
		public void OnDefectionPersuasionSucess()
		{
			this._numberOfClansDefected++;
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x000503C1 File Offset: 0x0004E5C1
		private void OnUnitRecruited(CharacterObject character, int amount)
		{
			this._numberOfTroopsRecruited += amount;
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x000503D1 File Offset: 0x0004E5D1
		private void OnMainPartyPrisonerRecruited(FlattenedTroopRoster flattenedTroopRoster)
		{
			this._numberOfPrisonersRecruited += flattenedTroopRoster.CountQ<FlattenedTroopRosterElement>();
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x000503E6 File Offset: 0x0004E5E6
		private void OnCrimeRatingChanged(IFaction kingdom, float deltaCrimeAmount)
		{
			if (deltaCrimeAmount > 0f)
			{
				this._totalCrimeRatingGained += (int)deltaCrimeAmount;
			}
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x000503FF File Offset: 0x0004E5FF
		private void OnClanInfluenceChanged(Clan clan, float change)
		{
			if (change > 0f && clan == Clan.PlayerClan)
			{
				this._totalInfluenceEarned += (int)change;
			}
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x00050420 File Offset: 0x0004E620
		private void OnTournamentFinished(CharacterObject winner, MBReadOnlyList<CharacterObject> participants, Town town, ItemObject prize)
		{
			if (winner.HeroObject == Hero.MainHero)
			{
				this._numberOfTournamentWins++;
				int leaderBoardRank = Campaign.Current.TournamentManager.GetLeaderBoardRank(Hero.MainHero);
				if (leaderBoardRank < this._highestTournamentRank)
				{
					this._highestTournamentRank = leaderBoardRank;
				}
			}
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00050470 File Offset: 0x0004E670
		private void OnIssueUpdated(IssueBase issue, IssueBase.IssueUpdateDetails details, Hero issueSolver = null)
		{
			if (details == IssueBase.IssueUpdateDetails.IssueFinishedWithSuccess || details == IssueBase.IssueUpdateDetails.SentTroopsFinishedQuest || details == IssueBase.IssueUpdateDetails.IssueFinishedWithBetrayal)
			{
				this._numberOfIssuesSolved++;
				if (issueSolver != null && issueSolver.IsPlayerCompanion)
				{
					if (this._companionData.ContainsKey(issueSolver))
					{
						this._companionData[issueSolver] = new ValueTuple<int, int>(this._companionData[issueSolver].Item1 + 1, this._companionData[issueSolver].Item2);
						return;
					}
					this._companionData.Add(issueSolver, new ValueTuple<int, int>(1, 0));
				}
			}
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x000504F9 File Offset: 0x0004E6F9
		private void OnHeroCreated(Hero hero, bool isBornNaturally = false)
		{
			if (hero.Mother == Hero.MainHero || hero.Father == Hero.MainHero)
			{
				this._numberOfChildrenBorn++;
			}
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00050523 File Offset: 0x0004E723
		private void OnHeroKilled(Hero victim, Hero killer, KillCharacterAction.KillCharacterActionDetail detail, bool showNotification = true)
		{
			if (killer != null && killer.PartyBelongedTo == MobileParty.MainParty && detail == KillCharacterAction.KillCharacterActionDetail.DiedInBattle)
			{
				this._numberOfHeroesKilledInBattle++;
			}
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00050548 File Offset: 0x0004E748
		private void OnMissionStarted(IMission mission)
		{
			StatisticsCampaignBehavior.StatisticsMissionLogic statisticsMissionLogic = new StatisticsCampaignBehavior.StatisticsMissionLogic();
			Mission.Current.AddMissionBehavior(statisticsMissionLogic);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00050568 File Offset: 0x0004E768
		private void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent)
		{
			if (affectorAgent != null)
			{
				if (affectorAgent == Agent.Main)
				{
					this._numberOfTroopsKnockedOrKilledByPlayer++;
				}
				else if (affectorAgent.IsPlayerTroop)
				{
					this._numberOfTroopsKnockedOrKilledAsParty++;
				}
				else if (affectorAgent.IsHero)
				{
					Hero heroObject = (affectorAgent.Character as CharacterObject).HeroObject;
					if (heroObject.IsPlayerCompanion)
					{
						if (this._companionData.ContainsKey(heroObject))
						{
							this._companionData[heroObject] = new ValueTuple<int, int>(this._companionData[heroObject].Item1, this._companionData[heroObject].Item2 + 1);
						}
						else
						{
							this._companionData.Add(heroObject, new ValueTuple<int, int>(0, 1));
						}
					}
				}
				if (affectedAgent.IsHero && affectedAgent.State == AgentState.Killed)
				{
					this._numberOfHeroesKilledInBattle++;
				}
			}
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00050646 File Offset: 0x0004E846
		private void OnPlayerPartyKnockedOrKilledTroop(CharacterObject troop)
		{
			this._numberOfTroopsKnockedOrKilledAsParty++;
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00050656 File Offset: 0x0004E856
		private void OnHeroPrisonerReleased(Hero prisoner, PartyBase party, IFaction capturerFaction, EndCaptivityDetail detail, bool showNotification)
		{
			if (prisoner == Hero.MainHero)
			{
				this._timeSpentAsPrisoner += CampaignTime.Now - PlayerCaptivity.CaptivityStartTime;
			}
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00050680 File Offset: 0x0004E880
		private void OnMapEventStarted(MapEvent mapEvent, PartyBase attackerParty, PartyBase defenderParty)
		{
			if (mapEvent.IsPlayerMapEvent)
			{
				this._lastPlayerBattleSize = mapEvent.AttackerSide.TroopCount + mapEvent.DefenderSide.TroopCount;
			}
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x000506A7 File Offset: 0x0004E8A7
		private void OnHideoutBattleCompleted(BattleSideEnum winnerSide, HideoutEventComponent hideoutEventComponent, HideoutEventComponent.HideoutBattleEndState battleEndState)
		{
			if (hideoutEventComponent.MapEvent.PlayerSide == winnerSide)
			{
				this._numberOfHideoutsCleared++;
			}
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x000506C5 File Offset: 0x0004E8C5
		private void OnRaidCompleted(BattleSideEnum winnerSide, RaidEventComponent raidEventComponent)
		{
			if (raidEventComponent.MapEvent.HasWinner && raidEventComponent.MapEvent.PlayerSide == winnerSide)
			{
				this._numberOfVillagesRaided++;
			}
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x000506F0 File Offset: 0x0004E8F0
		private void OnPrisonersTaken(FlattenedTroopRoster troopRoster)
		{
			this._numberOfTroopPrisonersTaken += troopRoster.CountQ<FlattenedTroopRosterElement>();
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00050705 File Offset: 0x0004E905
		private void OnHeroPrisonerTaken(PartyBase capturer, Hero prisoner)
		{
			if (capturer == PartyBase.MainParty)
			{
				this._numberOfHeroPrisonersTaken++;
			}
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0005071D File Offset: 0x0004E91D
		private void OnArmyCreated(Army army)
		{
			if (army.LeaderParty == MobileParty.MainParty && this._largestArmyFormedByPlayer < army.TotalManCount)
			{
				this._largestArmyFormedByPlayer = army.TotalManCount;
			}
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00050748 File Offset: 0x0004E948
		private void OnPartyAttachedAnotherParty(MobileParty mobileParty)
		{
			if (mobileParty.Army == MobileParty.MainParty.Army && MobileParty.MainParty.Army.LeaderParty == MobileParty.MainParty && this._largestArmyFormedByPlayer < MobileParty.MainParty.Army.TotalManCount)
			{
				this._largestArmyFormedByPlayer = MobileParty.MainParty.Army.TotalManCount;
			}
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x000507A9 File Offset: 0x0004E9A9
		private void OnClanDestroyed(Clan clan)
		{
			if (clan.MapFaction.IsAtWarWith(Clan.PlayerClan.MapFaction))
			{
				this._numberOfEnemyClansDestroyed++;
			}
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x000507D0 File Offset: 0x0004E9D0
		private void OnMapEventEnd(MapEvent mapEvent)
		{
			if (mapEvent.IsPlayerMapEvent && mapEvent.HasWinner)
			{
				if (mapEvent.WinningSide == mapEvent.PlayerSide)
				{
					this._numberOfbattlesWon++;
					if (mapEvent.IsSiegeAssault && !mapEvent.IsPlayerSergeant() && mapEvent.MapEventSettlement != null)
					{
						if (mapEvent.MapEventSettlement.IsTown)
						{
							this._numberOfTownsCaptured++;
						}
						else if (mapEvent.MapEventSettlement.IsCastle)
						{
							this._numberOfCastlesCaptured++;
						}
					}
					if (this._largestBattleWonAsLeader < this._lastPlayerBattleSize && !mapEvent.IsPlayerSergeant())
					{
						this._largestBattleWonAsLeader = this._lastPlayerBattleSize;
						return;
					}
				}
				else
				{
					this._numberOfbattlesLost++;
				}
			}
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00050893 File Offset: 0x0004EA93
		private void OnHeroOrPartyTradedGold(ValueTuple<Hero, PartyBase> giver, ValueTuple<Hero, PartyBase> recipient, ValueTuple<int, string> goldAmount, bool showNotification)
		{
			if (recipient.Item1 == Hero.MainHero || recipient.Item2 == PartyBase.MainParty)
			{
				this._totalDenarsEarned += (ulong)((long)goldAmount.Item1);
			}
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x000508C3 File Offset: 0x0004EAC3
		public void OnPlayerAcceptedRansomOffer(int ransomPrice)
		{
			this._denarsEarnedFromRansoms += (ulong)((long)ransomPrice);
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x000508D4 File Offset: 0x0004EAD4
		private void OnPlayerEarnedGoldFromAsset(DefaultClanFinanceModel.AssetIncomeType assetType, int amount)
		{
			switch (assetType)
			{
			case DefaultClanFinanceModel.AssetIncomeType.Workshop:
				this._denarsEarnedFromWorkshops += (ulong)((long)amount);
				return;
			case DefaultClanFinanceModel.AssetIncomeType.Caravan:
				this._denarsEarnedFromCaravans += (ulong)((long)amount);
				return;
			case DefaultClanFinanceModel.AssetIncomeType.Taxes:
				this._denarsEarnedFromTaxes += (ulong)((long)amount);
				return;
			case DefaultClanFinanceModel.AssetIncomeType.TributesEarned:
				this._denarsEarnedFromTributes += (ulong)((long)amount);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00050937 File Offset: 0x0004EB37
		private void OnNewCompanionAdded(Hero hero)
		{
			this._numberOfCompanionsHired++;
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00050948 File Offset: 0x0004EB48
		private void OnNewItemCrafted(ItemObject itemObject, ItemModifier overriddenItemModifier, bool isCraftingOrderItem)
		{
			this._numberOfWeaponsCrafted++;
			if (isCraftingOrderItem)
			{
				this._numberOfCraftingOrdersCompleted++;
			}
			if (this._mostExpensiveItemCrafted.Item2 == 0 || this._mostExpensiveItemCrafted.Item2 < itemObject.Value)
			{
				this._mostExpensiveItemCrafted.Item1 = itemObject.Name.ToString();
				this._mostExpensiveItemCrafted.Item2 = itemObject.Value;
			}
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x000509BB File Offset: 0x0004EBBB
		private void OnCraftingPartUnlocked(CraftingPiece craftingPiece)
		{
			this._numberOfCraftingPartsUnlocked++;
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x000509CC File Offset: 0x0004EBCC
		[return: TupleElementNames(new string[] { "name", "value" })]
		public ValueTuple<string, int> GetCompanionWithMostKills()
		{
			if (this._companionData.IsEmpty<KeyValuePair<Hero, ValueTuple<int, int>>>())
			{
				return new ValueTuple<string, int>(null, 0);
			}
			KeyValuePair<Hero, ValueTuple<int, int>> keyValuePair = this._companionData.MaxBy<KeyValuePair<Hero, ValueTuple<int, int>>, int>((KeyValuePair<Hero, ValueTuple<int, int>> kvp) => kvp.Value.Item2);
			return new ValueTuple<string, int>(keyValuePair.Key.Name.ToString(), keyValuePair.Value.Item2);
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00050A3C File Offset: 0x0004EC3C
		[return: TupleElementNames(new string[] { "name", "value" })]
		public ValueTuple<string, int> GetCompanionWithMostIssuesSolved()
		{
			if (this._companionData.IsEmpty<KeyValuePair<Hero, ValueTuple<int, int>>>())
			{
				return new ValueTuple<string, int>(null, 0);
			}
			KeyValuePair<Hero, ValueTuple<int, int>> keyValuePair = this._companionData.MaxBy<KeyValuePair<Hero, ValueTuple<int, int>>, int>((KeyValuePair<Hero, ValueTuple<int, int>> kvp) => kvp.Value.Item1);
			return new ValueTuple<string, int>(keyValuePair.Key.Name.ToString(), keyValuePair.Value.Item1);
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00050AAB File Offset: 0x0004ECAB
		public int GetHighestTournamentRank()
		{
			return this._highestTournamentRank;
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00050AB3 File Offset: 0x0004ECB3
		public int GetNumberOfTournamentWins()
		{
			return this._numberOfTournamentWins;
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00050ABB File Offset: 0x0004ECBB
		public int GetNumberOfChildrenBorn()
		{
			return this._numberOfChildrenBorn;
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00050AC3 File Offset: 0x0004ECC3
		public int GetNumberOfPrisonersRecruited()
		{
			return this._numberOfPrisonersRecruited;
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00050ACB File Offset: 0x0004ECCB
		public int GetNumberOfTroopsRecruited()
		{
			return this._numberOfTroopsRecruited;
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00050AD3 File Offset: 0x0004ECD3
		public int GetNumberOfClansDefected()
		{
			return this._numberOfClansDefected;
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00050ADB File Offset: 0x0004ECDB
		public int GetNumberOfIssuesSolved()
		{
			return this._numberOfIssuesSolved;
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00050AE3 File Offset: 0x0004ECE3
		public int GetTotalInfluenceEarned()
		{
			return this._totalInfluenceEarned;
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00050AEB File Offset: 0x0004ECEB
		public int GetTotalCrimeRatingGained()
		{
			return this._totalCrimeRatingGained;
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00050AF3 File Offset: 0x0004ECF3
		public int GetNumberOfBattlesWon()
		{
			return this._numberOfbattlesWon;
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00050AFB File Offset: 0x0004ECFB
		public int GetNumberOfBattlesLost()
		{
			return this._numberOfbattlesLost;
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00050B03 File Offset: 0x0004ED03
		public int GetLargestBattleWonAsLeader()
		{
			return this._largestBattleWonAsLeader;
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00050B0B File Offset: 0x0004ED0B
		public int GetLargestArmyFormedByPlayer()
		{
			return this._largestArmyFormedByPlayer;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00050B13 File Offset: 0x0004ED13
		public int GetNumberOfEnemyClansDestroyed()
		{
			return this._numberOfEnemyClansDestroyed;
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x00050B1B File Offset: 0x0004ED1B
		public int GetNumberOfHeroesKilledInBattle()
		{
			return this._numberOfHeroesKilledInBattle;
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00050B23 File Offset: 0x0004ED23
		public int GetNumberOfTroopsKnockedOrKilledAsParty()
		{
			return this._numberOfTroopsKnockedOrKilledAsParty;
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00050B2B File Offset: 0x0004ED2B
		public int GetNumberOfTroopsKnockedOrKilledByPlayer()
		{
			return this._numberOfTroopsKnockedOrKilledByPlayer;
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00050B33 File Offset: 0x0004ED33
		public int GetNumberOfHeroPrisonersTaken()
		{
			return this._numberOfHeroPrisonersTaken;
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00050B3B File Offset: 0x0004ED3B
		public int GetNumberOfTroopPrisonersTaken()
		{
			return this._numberOfTroopPrisonersTaken;
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00050B43 File Offset: 0x0004ED43
		public int GetNumberOfTownsCaptured()
		{
			return this._numberOfTownsCaptured;
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x00050B4B File Offset: 0x0004ED4B
		public int GetNumberOfHideoutsCleared()
		{
			return this._numberOfHideoutsCleared;
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00050B53 File Offset: 0x0004ED53
		public int GetNumberOfCastlesCaptured()
		{
			return this._numberOfCastlesCaptured;
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00050B5B File Offset: 0x0004ED5B
		public int GetNumberOfVillagesRaided()
		{
			return this._numberOfVillagesRaided;
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00050B63 File Offset: 0x0004ED63
		public int GetNumberOfCraftingPartsUnlocked()
		{
			return this._numberOfCraftingPartsUnlocked;
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x00050B6B File Offset: 0x0004ED6B
		public int GetNumberOfWeaponsCrafted()
		{
			return this._numberOfWeaponsCrafted;
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00050B73 File Offset: 0x0004ED73
		public int GetNumberOfCraftingOrdersCompleted()
		{
			return this._numberOfCraftingOrdersCompleted;
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00050B7B File Offset: 0x0004ED7B
		public int GetNumberOfCompanionsHired()
		{
			return this._numberOfCompanionsHired;
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00050B83 File Offset: 0x0004ED83
		public CampaignTime GetTimeSpentAsPrisoner()
		{
			return this._timeSpentAsPrisoner;
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x00050B8B File Offset: 0x0004ED8B
		public ulong GetTotalTimePlayedInSeconds()
		{
			this.UpdateTotalTimePlayedInSeconds();
			return this._totalTimePlayedInSeconds;
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x00050B99 File Offset: 0x0004ED99
		public ulong GetTotalDenarsEarned()
		{
			return this._totalDenarsEarned;
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00050BA1 File Offset: 0x0004EDA1
		public ulong GetDenarsEarnedFromCaravans()
		{
			return this._denarsEarnedFromCaravans;
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00050BA9 File Offset: 0x0004EDA9
		public ulong GetDenarsEarnedFromWorkshops()
		{
			return this._denarsEarnedFromWorkshops;
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x00050BB1 File Offset: 0x0004EDB1
		public ulong GetDenarsEarnedFromRansoms()
		{
			return this._denarsEarnedFromRansoms;
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x00050BB9 File Offset: 0x0004EDB9
		public ulong GetDenarsEarnedFromTaxes()
		{
			return this._denarsEarnedFromTaxes;
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00050BC1 File Offset: 0x0004EDC1
		public ulong GetDenarsEarnedFromTributes()
		{
			return this._denarsEarnedFromTributes;
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00050BC9 File Offset: 0x0004EDC9
		public ulong GetDenarsPaidAsTributes()
		{
			return this._denarsPaidAsTributes;
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00050BD1 File Offset: 0x0004EDD1
		public CampaignTime GetTotalTimePlayed()
		{
			return CampaignTime.Now - Campaign.Current.Models.CampaignTimeModel.CampaignStartTime;
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x00050BF1 File Offset: 0x0004EDF1
		public ValueTuple<string, int> GetMostExpensiveItemCrafted()
		{
			return this._mostExpensiveItemCrafted;
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00050BFC File Offset: 0x0004EDFC
		private void UpdateTotalTimePlayedInSeconds()
		{
			double totalSeconds = (DateTime.Now - this._lastGameplayTimeCheck).TotalSeconds;
			if (totalSeconds > 9.999999747378752E-06)
			{
				this._totalTimePlayedInSeconds += (ulong)totalSeconds;
				this._lastGameplayTimeCheck = DateTime.Now;
			}
		}

		// Token: 0x040004AC RID: 1196
		private int _highestTournamentRank;

		// Token: 0x040004AD RID: 1197
		private int _numberOfTournamentWins;

		// Token: 0x040004AE RID: 1198
		private int _numberOfChildrenBorn;

		// Token: 0x040004AF RID: 1199
		private int _numberOfPrisonersRecruited;

		// Token: 0x040004B0 RID: 1200
		private int _numberOfTroopsRecruited;

		// Token: 0x040004B1 RID: 1201
		private int _numberOfClansDefected;

		// Token: 0x040004B2 RID: 1202
		private int _numberOfIssuesSolved;

		// Token: 0x040004B3 RID: 1203
		private int _totalInfluenceEarned;

		// Token: 0x040004B4 RID: 1204
		private int _totalCrimeRatingGained;

		// Token: 0x040004B5 RID: 1205
		private ulong _totalTimePlayedInSeconds;

		// Token: 0x040004B6 RID: 1206
		private int _numberOfbattlesWon;

		// Token: 0x040004B7 RID: 1207
		private int _numberOfbattlesLost;

		// Token: 0x040004B8 RID: 1208
		private int _largestBattleWonAsLeader;

		// Token: 0x040004B9 RID: 1209
		private int _largestArmyFormedByPlayer;

		// Token: 0x040004BA RID: 1210
		private int _numberOfEnemyClansDestroyed;

		// Token: 0x040004BB RID: 1211
		private int _numberOfHeroesKilledInBattle;

		// Token: 0x040004BC RID: 1212
		private int _numberOfTroopsKnockedOrKilledAsParty;

		// Token: 0x040004BD RID: 1213
		private int _numberOfTroopsKnockedOrKilledByPlayer;

		// Token: 0x040004BE RID: 1214
		private int _numberOfHeroPrisonersTaken;

		// Token: 0x040004BF RID: 1215
		private int _numberOfTroopPrisonersTaken;

		// Token: 0x040004C0 RID: 1216
		private int _numberOfTownsCaptured;

		// Token: 0x040004C1 RID: 1217
		private int _numberOfHideoutsCleared;

		// Token: 0x040004C2 RID: 1218
		private int _numberOfCastlesCaptured;

		// Token: 0x040004C3 RID: 1219
		private int _numberOfVillagesRaided;

		// Token: 0x040004C4 RID: 1220
		private CampaignTime _timeSpentAsPrisoner;

		// Token: 0x040004C5 RID: 1221
		private ulong _totalDenarsEarned;

		// Token: 0x040004C6 RID: 1222
		private ulong _denarsEarnedFromCaravans;

		// Token: 0x040004C7 RID: 1223
		private ulong _denarsEarnedFromWorkshops;

		// Token: 0x040004C8 RID: 1224
		private ulong _denarsEarnedFromRansoms;

		// Token: 0x040004C9 RID: 1225
		private ulong _denarsEarnedFromTaxes;

		// Token: 0x040004CA RID: 1226
		private ulong _denarsEarnedFromTributes;

		// Token: 0x040004CB RID: 1227
		private ulong _denarsPaidAsTributes;

		// Token: 0x040004CC RID: 1228
		private int _numberOfCraftingPartsUnlocked;

		// Token: 0x040004CD RID: 1229
		private int _numberOfWeaponsCrafted;

		// Token: 0x040004CE RID: 1230
		private int _numberOfCraftingOrdersCompleted;

		// Token: 0x040004CF RID: 1231
		private ValueTuple<string, int> _mostExpensiveItemCrafted = new ValueTuple<string, int>(null, 0);

		// Token: 0x040004D0 RID: 1232
		private int _numberOfCompanionsHired;

		// Token: 0x040004D1 RID: 1233
		private Dictionary<Hero, ValueTuple<int, int>> _companionData = new Dictionary<Hero, ValueTuple<int, int>>();

		// Token: 0x040004D2 RID: 1234
		private int _lastPlayerBattleSize;

		// Token: 0x040004D3 RID: 1235
		private DateTime _lastGameplayTimeCheck;

		// Token: 0x02000208 RID: 520
		private class StatisticsMissionLogic : MissionLogic
		{
			// Token: 0x060013D7 RID: 5079 RVA: 0x00078A38 File Offset: 0x00076C38
			public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow blow)
			{
				if (this.behavior != null)
				{
					this.behavior.OnAgentRemoved(affectedAgent, affectorAgent);
				}
			}

			// Token: 0x0400095D RID: 2397
			private readonly StatisticsCampaignBehavior behavior = Campaign.Current.CampaignBehaviorManager.GetBehavior<StatisticsCampaignBehavior>();
		}
	}
}
