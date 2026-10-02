using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.Issues;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Naval;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x0200044D RID: 1101
	public class ViewDataTrackerCampaignBehavior : CampaignBehaviorBase, IViewDataTracker
	{
		// Token: 0x060046C7 RID: 18119 RVA: 0x00162860 File Offset: 0x00160A60
		public ViewDataTrackerCampaignBehavior()
		{
			this._inventoryItemLocks = new List<string>();
			this._partyPrisonerLocks = new List<string>();
			this._partyTroopLocks = new List<string>();
			this._encyclopediaBookmarkedClans = new List<Clan>();
			this._encyclopediaBookmarkedConcepts = new List<Concept>();
			this._encyclopediaBookmarkedHeroes = new List<Hero>();
			this._encyclopediaBookmarkedShips = new List<ShipHull>();
			this._encyclopediaBookmarkedKingdoms = new List<Kingdom>();
			this._encyclopediaBookmarkedSettlements = new List<Settlement>();
			this._encyclopediaBookmarkedUnits = new List<CharacterObject>();
			this._inventorySortPreferences = new Dictionary<int, Tuple<int, int>>();
			this._plunderItems = new List<ItemRosterElement>();
			this._unexaminedFigureheads = new List<Figurehead>();
		}

		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x060046C8 RID: 18120 RVA: 0x00162961 File Offset: 0x00160B61
		// (set) Token: 0x060046C9 RID: 18121 RVA: 0x00162969 File Offset: 0x00160B69
		public bool IsPartyNotificationActive { get; private set; }

		// Token: 0x060046CA RID: 18122 RVA: 0x00162972 File Offset: 0x00160B72
		public TextObject GetPartyNotificationText()
		{
			return this._recruitNotificationText.CopyTextObject().SetTextVariable("NUMBER", this._numOfRecruitablePrisoners);
		}

		// Token: 0x060046CB RID: 18123 RVA: 0x0016298F File Offset: 0x00160B8F
		public void ClearPartyNotification()
		{
			this.IsPartyNotificationActive = false;
			this._numOfRecruitablePrisoners = 0;
		}

		// Token: 0x060046CC RID: 18124 RVA: 0x0016299F File Offset: 0x00160B9F
		public void UpdatePartyNotification()
		{
			this.UpdatePrisonerRecruitValue();
		}

		// Token: 0x060046CD RID: 18125 RVA: 0x001629A8 File Offset: 0x00160BA8
		private void UpdatePrisonerRecruitValue()
		{
			Dictionary<CharacterObject, int> dictionary = new Dictionary<CharacterObject, int>();
			foreach (TroopRosterElement troopRosterElement in MobileParty.MainParty.PrisonRoster.GetTroopRoster())
			{
				int num = Campaign.Current.Models.PrisonerRecruitmentCalculationModel.CalculateRecruitableNumber(PartyBase.MainParty, troopRosterElement.Character);
				int num2;
				if (this._examinedPrisonerCharacterList.TryGetValue(troopRosterElement.Character, out num2))
				{
					if (num2 != num)
					{
						this._examinedPrisonerCharacterList[troopRosterElement.Character] = num;
						if (num2 < num)
						{
							this.IsPartyNotificationActive = true;
							this._numOfRecruitablePrisoners += num - num2;
						}
					}
				}
				else
				{
					this._examinedPrisonerCharacterList.Add(troopRosterElement.Character, num);
					if (num > 0)
					{
						this.IsPartyNotificationActive = true;
						this._numOfRecruitablePrisoners += num;
					}
				}
				dictionary.Add(troopRosterElement.Character, num);
			}
			this._examinedPrisonerCharacterList = dictionary;
		}

		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x060046CE RID: 18126 RVA: 0x00162AB4 File Offset: 0x00160CB4
		public bool IsQuestNotificationActive
		{
			get
			{
				return this.UnExaminedQuestLogs.Count > 0;
			}
		}

		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x060046CF RID: 18127 RVA: 0x00162AC4 File Offset: 0x00160CC4
		public IReadOnlyList<JournalLog> UnExaminedQuestLogs
		{
			get
			{
				if (this._isUnExaminedQuestLogJournalEntriesDirty)
				{
					this.UpdateJournalLogEntries();
				}
				for (int i = this._unExaminedQuestLogs.Count - 1; i >= 0; i--)
				{
					JournalLogEntry journalLogEntry = this._unExaminedQuestLogJournalEntries[this._unExaminedQuestLogs[i]];
					CampaignTime campaignTime = journalLogEntry.KeepInHistoryTime + journalLogEntry.GameTime;
					if (!journalLogEntry.IsValid() || campaignTime.IsPast)
					{
						this._unExaminedQuestLogJournalEntries.Remove(this._unExaminedQuestLogs[i]);
						this._unExaminedQuestLogs.RemoveAt(i);
					}
				}
				if (this._unExaminedQuestLogsReadOnly == null || this._unExaminedQuestLogs.Count != this._unExaminedQuestLogsReadOnly.Count)
				{
					this._unExaminedQuestLogsReadOnly = this._unExaminedQuestLogs.AsReadOnly();
				}
				return this._unExaminedQuestLogsReadOnly;
			}
		}

		// Token: 0x060046D0 RID: 18128 RVA: 0x00162B8D File Offset: 0x00160D8D
		public TextObject GetQuestNotificationText()
		{
			return this._questNotificationText.CopyTextObject().SetTextVariable("NUMBER", this.UnExaminedQuestLogs.Count);
		}

		// Token: 0x060046D1 RID: 18129 RVA: 0x00162BB0 File Offset: 0x00160DB0
		public void OnQuestLogExamined(JournalLog log)
		{
			if (this._unExaminedQuestLogs.Contains(log))
			{
				this._unExaminedQuestLogs.Remove(log);
				this._unExaminedQuestLogsReadOnly = this._unExaminedQuestLogs.AsReadOnly();
				IEnumerable<JournalLog> entries = this._unExaminedQuestLogJournalEntries[log].GetEntries();
				if (this._unExaminedQuestLogs.All<JournalLog>((JournalLog x) => !entries.Contains(x)))
				{
					this._unExaminedQuestLogJournalEntries.Remove(log);
				}
			}
		}

		// Token: 0x060046D2 RID: 18130 RVA: 0x00162C2C File Offset: 0x00160E2C
		private void OnQuestLogAdded(QuestBase obj, bool hideInformation)
		{
			this._unExaminedQuestLogs.Add(obj.JournalEntries[obj.JournalEntries.Count - 1]);
			this._unExaminedQuestLogsReadOnly = this._unExaminedQuestLogs.AsReadOnly();
			this._isUnExaminedQuestLogJournalEntriesDirty = true;
		}

		// Token: 0x060046D3 RID: 18131 RVA: 0x00162C69 File Offset: 0x00160E69
		private void OnIssueLogAdded(IssueBase obj, bool hideInformation)
		{
			this._unExaminedQuestLogs.Add(obj.JournalEntries[obj.JournalEntries.Count - 1]);
			this._unExaminedQuestLogsReadOnly = this._unExaminedQuestLogs.AsReadOnly();
			this._isUnExaminedQuestLogJournalEntriesDirty = true;
		}

		// Token: 0x060046D4 RID: 18132 RVA: 0x00162CA8 File Offset: 0x00160EA8
		private void UpdateJournalLogEntries()
		{
			this._unExaminedQuestLogJournalEntries.Clear();
			JournalLogEntry[] array = Campaign.Current.LogEntryHistory.GetGameActionLogs<JournalLogEntry>((JournalLogEntry x) => true).ToArray<JournalLogEntry>();
			for (int i = this._unExaminedQuestLogs.Count - 1; i >= 0; i--)
			{
				JournalLog unExaminedQuestLog = this._unExaminedQuestLogs[i];
				JournalLogEntry journalLogEntry = array.FirstOrDefault<JournalLogEntry>((JournalLogEntry x) => x.GetEntries().Contains(unExaminedQuestLog));
				if (journalLogEntry == null)
				{
					this._unExaminedQuestLogs.RemoveAt(i);
				}
				else
				{
					this._unExaminedQuestLogJournalEntries.Add(unExaminedQuestLog, journalLogEntry);
				}
			}
			if (this._unExaminedQuestLogs.Count != this._unExaminedQuestLogsReadOnly.Count)
			{
				this._unExaminedQuestLogsReadOnly = this._unExaminedQuestLogs.AsReadOnly();
			}
			this._isUnExaminedQuestLogJournalEntriesDirty = false;
		}

		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x060046D5 RID: 18133 RVA: 0x00162D8A File Offset: 0x00160F8A
		public List<Army> UnExaminedArmies
		{
			get
			{
				return this._unExaminedArmies;
			}
		}

		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x060046D6 RID: 18134 RVA: 0x00162D92 File Offset: 0x00160F92
		public int NumOfKingdomArmyNotifications
		{
			get
			{
				return this.UnExaminedArmies.Count;
			}
		}

		// Token: 0x060046D7 RID: 18135 RVA: 0x00162D9F File Offset: 0x00160F9F
		public void OnArmyExamined(Army army)
		{
			this._unExaminedArmies.Remove(army);
		}

		// Token: 0x060046D8 RID: 18136 RVA: 0x00162DB0 File Offset: 0x00160FB0
		private void OnArmyDispersed(Army arg1, Army.ArmyDispersionReason arg2, bool isPlayersArmy)
		{
			Army army;
			if (isPlayersArmy && (army = this._unExaminedArmies.SingleOrDefault<Army>((Army a) => a == arg1)) != null)
			{
				this._unExaminedArmies.Remove(army);
			}
		}

		// Token: 0x060046D9 RID: 18137 RVA: 0x00162DF5 File Offset: 0x00160FF5
		private void OnNewArmyCreated(Army army)
		{
			if (army.Kingdom == Hero.MainHero.MapFaction && army.LeaderParty != MobileParty.MainParty)
			{
				this._unExaminedArmies.Add(army);
			}
		}

		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x060046DA RID: 18138 RVA: 0x00162E22 File Offset: 0x00161022
		public bool IsCharacterNotificationActive
		{
			get
			{
				return this._isCharacterNotificationActive;
			}
		}

		// Token: 0x060046DB RID: 18139 RVA: 0x00162E2A File Offset: 0x0016102A
		public void ClearCharacterNotification()
		{
			this._isCharacterNotificationActive = false;
			this._numOfPerks = 0;
		}

		// Token: 0x060046DC RID: 18140 RVA: 0x00162E3A File Offset: 0x0016103A
		public TextObject GetCharacterNotificationText()
		{
			return this._characterNotificationText.CopyTextObject().SetTextVariable("NUMBER", this._numOfPerks);
		}

		// Token: 0x060046DD RID: 18141 RVA: 0x00162E57 File Offset: 0x00161057
		private void OnHeroGainedSkill(Hero hero, SkillObject skill, int change = 1, bool shouldNotify = true)
		{
			if ((hero == Hero.MainHero || hero.Clan == Clan.PlayerClan) && PerkHelper.AvailablePerkCountOfHero(hero) > 0)
			{
				this._isCharacterNotificationActive = shouldNotify;
				this._numOfPerks++;
			}
		}

		// Token: 0x060046DE RID: 18142 RVA: 0x00162E8D File Offset: 0x0016108D
		private void OnHeroLevelledUp(Hero hero, bool shouldNotify)
		{
			if (hero == Hero.MainHero)
			{
				this._isCharacterNotificationActive = shouldNotify;
			}
		}

		// Token: 0x060046DF RID: 18143 RVA: 0x00162E9E File Offset: 0x0016109E
		private void OnGameLoaded(CampaignGameStarter campaignGameStarter)
		{
			this._unExaminedQuestLogsReadOnly = this._unExaminedQuestLogs.AsReadOnly();
			this.UpdatePartyNotification();
			this.UpdatePrisonerRecruitValue();
			this.UpdateJournalLogEntries();
		}

		// Token: 0x060046E0 RID: 18144 RVA: 0x00162EC3 File Offset: 0x001610C3
		public bool GetMapBarExtendedState()
		{
			return this._isMapBarExtended;
		}

		// Token: 0x060046E1 RID: 18145 RVA: 0x00162ECB File Offset: 0x001610CB
		public void SetMapBarExtendedState(bool isExtended)
		{
			this._isMapBarExtended = isExtended;
		}

		// Token: 0x060046E2 RID: 18146 RVA: 0x00162ED4 File Offset: 0x001610D4
		public void SetInventoryLocks(IEnumerable<string> locks)
		{
			this._inventoryItemLocks.Clear();
			foreach (string text in locks)
			{
				this._inventoryItemLocks.Add(text);
			}
		}

		// Token: 0x060046E3 RID: 18147 RVA: 0x00162F2C File Offset: 0x0016112C
		public IEnumerable<string> GetInventoryLocks()
		{
			return this._inventoryItemLocks;
		}

		// Token: 0x060046E4 RID: 18148 RVA: 0x00162F34 File Offset: 0x00161134
		public void InventorySetSortPreference(int inventoryMode, int sortOption, int sortState)
		{
			this._inventorySortPreferences[inventoryMode] = new Tuple<int, int>(sortOption, sortState);
		}

		// Token: 0x060046E5 RID: 18149 RVA: 0x00162F4C File Offset: 0x0016114C
		public Tuple<int, int> InventoryGetSortPreference(int inventoryMode)
		{
			Tuple<int, int> tuple;
			if (this._inventorySortPreferences.TryGetValue(inventoryMode, out tuple))
			{
				return tuple;
			}
			return new Tuple<int, int>(0, 0);
		}

		// Token: 0x060046E6 RID: 18150 RVA: 0x00162F74 File Offset: 0x00161174
		public void SetPartyTroopLocks(IEnumerable<string> locks)
		{
			this._partyTroopLocks.Clear();
			foreach (string text in locks)
			{
				this._partyTroopLocks.Add(text);
			}
		}

		// Token: 0x060046E7 RID: 18151 RVA: 0x00162FCC File Offset: 0x001611CC
		public void SetPartyPrisonerLocks(IEnumerable<string> locks)
		{
			this._partyPrisonerLocks.Clear();
			foreach (string text in locks)
			{
				this._partyPrisonerLocks.Add(text);
			}
		}

		// Token: 0x060046E8 RID: 18152 RVA: 0x00163024 File Offset: 0x00161224
		public void SetPartySortType(int sortType)
		{
			this._partySortType = sortType;
		}

		// Token: 0x060046E9 RID: 18153 RVA: 0x0016302D File Offset: 0x0016122D
		public void SetIsPartySortAscending(bool isAscending)
		{
			this._isPartySortAscending = isAscending;
		}

		// Token: 0x060046EA RID: 18154 RVA: 0x00163036 File Offset: 0x00161236
		public IEnumerable<string> GetPartyTroopLocks()
		{
			return this._partyTroopLocks;
		}

		// Token: 0x060046EB RID: 18155 RVA: 0x0016303E File Offset: 0x0016123E
		public IEnumerable<string> GetPartyPrisonerLocks()
		{
			return this._partyPrisonerLocks;
		}

		// Token: 0x060046EC RID: 18156 RVA: 0x00163046 File Offset: 0x00161246
		public int GetPartySortType()
		{
			return this._partySortType;
		}

		// Token: 0x060046ED RID: 18157 RVA: 0x0016304E File Offset: 0x0016124E
		public bool GetIsPartySortAscending()
		{
			return this._isPartySortAscending;
		}

		// Token: 0x060046EE RID: 18158 RVA: 0x00163056 File Offset: 0x00161256
		public void AddEncyclopediaBookmarkToItem(Hero item)
		{
			this._encyclopediaBookmarkedHeroes.Add(item);
		}

		// Token: 0x060046EF RID: 18159 RVA: 0x00163064 File Offset: 0x00161264
		public void AddEncyclopediaBookmarkToItem(ShipHull shipHull)
		{
			this._encyclopediaBookmarkedShips.Add(shipHull);
		}

		// Token: 0x060046F0 RID: 18160 RVA: 0x00163072 File Offset: 0x00161272
		public void AddEncyclopediaBookmarkToItem(Clan clan)
		{
			this._encyclopediaBookmarkedClans.Add(clan);
		}

		// Token: 0x060046F1 RID: 18161 RVA: 0x00163080 File Offset: 0x00161280
		public void AddEncyclopediaBookmarkToItem(Concept concept)
		{
			this._encyclopediaBookmarkedConcepts.Add(concept);
		}

		// Token: 0x060046F2 RID: 18162 RVA: 0x0016308E File Offset: 0x0016128E
		public void AddEncyclopediaBookmarkToItem(Kingdom kingdom)
		{
			this._encyclopediaBookmarkedKingdoms.Add(kingdom);
		}

		// Token: 0x060046F3 RID: 18163 RVA: 0x0016309C File Offset: 0x0016129C
		public void AddEncyclopediaBookmarkToItem(Settlement settlement)
		{
			this._encyclopediaBookmarkedSettlements.Add(settlement);
		}

		// Token: 0x060046F4 RID: 18164 RVA: 0x001630AA File Offset: 0x001612AA
		public void AddEncyclopediaBookmarkToItem(CharacterObject unit)
		{
			this._encyclopediaBookmarkedUnits.Add(unit);
		}

		// Token: 0x060046F5 RID: 18165 RVA: 0x001630B8 File Offset: 0x001612B8
		public void RemoveEncyclopediaBookmarkFromItem(Hero hero)
		{
			this._encyclopediaBookmarkedHeroes.Remove(hero);
		}

		// Token: 0x060046F6 RID: 18166 RVA: 0x001630C7 File Offset: 0x001612C7
		public void RemoveEncyclopediaBookmarkFromItem(ShipHull shipHull)
		{
			this._encyclopediaBookmarkedShips.Remove(shipHull);
		}

		// Token: 0x060046F7 RID: 18167 RVA: 0x001630D6 File Offset: 0x001612D6
		public void RemoveEncyclopediaBookmarkFromItem(Clan clan)
		{
			this._encyclopediaBookmarkedClans.Remove(clan);
		}

		// Token: 0x060046F8 RID: 18168 RVA: 0x001630E5 File Offset: 0x001612E5
		public void RemoveEncyclopediaBookmarkFromItem(Concept concept)
		{
			this._encyclopediaBookmarkedConcepts.Remove(concept);
		}

		// Token: 0x060046F9 RID: 18169 RVA: 0x001630F4 File Offset: 0x001612F4
		public void RemoveEncyclopediaBookmarkFromItem(Kingdom kingdom)
		{
			this._encyclopediaBookmarkedKingdoms.Remove(kingdom);
		}

		// Token: 0x060046FA RID: 18170 RVA: 0x00163103 File Offset: 0x00161303
		public void RemoveEncyclopediaBookmarkFromItem(Settlement settlement)
		{
			this._encyclopediaBookmarkedSettlements.Remove(settlement);
		}

		// Token: 0x060046FB RID: 18171 RVA: 0x00163112 File Offset: 0x00161312
		public void RemoveEncyclopediaBookmarkFromItem(CharacterObject unit)
		{
			this._encyclopediaBookmarkedUnits.Remove(unit);
		}

		// Token: 0x060046FC RID: 18172 RVA: 0x00163121 File Offset: 0x00161321
		public bool IsEncyclopediaBookmarked(Hero hero)
		{
			return this._encyclopediaBookmarkedHeroes.Contains(hero);
		}

		// Token: 0x060046FD RID: 18173 RVA: 0x0016312F File Offset: 0x0016132F
		public bool IsEncyclopediaBookmarked(ShipHull shipHull)
		{
			return this._encyclopediaBookmarkedShips.Contains(shipHull);
		}

		// Token: 0x060046FE RID: 18174 RVA: 0x0016313D File Offset: 0x0016133D
		public bool IsEncyclopediaBookmarked(Clan clan)
		{
			return this._encyclopediaBookmarkedClans.Contains(clan);
		}

		// Token: 0x060046FF RID: 18175 RVA: 0x0016314B File Offset: 0x0016134B
		public bool IsEncyclopediaBookmarked(Concept concept)
		{
			return this._encyclopediaBookmarkedConcepts.Contains(concept);
		}

		// Token: 0x06004700 RID: 18176 RVA: 0x00163159 File Offset: 0x00161359
		public bool IsEncyclopediaBookmarked(Kingdom kingdom)
		{
			return this._encyclopediaBookmarkedKingdoms.Contains(kingdom);
		}

		// Token: 0x06004701 RID: 18177 RVA: 0x00163167 File Offset: 0x00161367
		public bool IsEncyclopediaBookmarked(Settlement settlement)
		{
			return this._encyclopediaBookmarkedSettlements.Contains(settlement);
		}

		// Token: 0x06004702 RID: 18178 RVA: 0x00163175 File Offset: 0x00161375
		public bool IsEncyclopediaBookmarked(CharacterObject unit)
		{
			return this._encyclopediaBookmarkedUnits.Contains(unit);
		}

		// Token: 0x06004703 RID: 18179 RVA: 0x00163183 File Offset: 0x00161383
		public void SetQuestSelection(QuestBase selection)
		{
			this._questSelection = selection;
		}

		// Token: 0x06004704 RID: 18180 RVA: 0x0016318C File Offset: 0x0016138C
		public QuestBase GetQuestSelection()
		{
			return this._questSelection;
		}

		// Token: 0x06004705 RID: 18181 RVA: 0x00163194 File Offset: 0x00161394
		public MBReadOnlyList<ItemRosterElement> GetPlunderItems()
		{
			return new MBReadOnlyList<ItemRosterElement>(this._plunderItems);
		}

		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x06004706 RID: 18182 RVA: 0x001631A1 File Offset: 0x001613A1
		public IReadOnlyList<Figurehead> UnexaminedFigureheads
		{
			get
			{
				return this._unexaminedFigureheads;
			}
		}

		// Token: 0x06004707 RID: 18183 RVA: 0x001631A9 File Offset: 0x001613A9
		private void OnFigureheadUnlocked(Figurehead newFigurehead)
		{
			this._unexaminedFigureheads.Add(newFigurehead);
		}

		// Token: 0x06004708 RID: 18184 RVA: 0x001631B7 File Offset: 0x001613B7
		public void OnFigureheadExamined(Figurehead figurehead)
		{
			this._unexaminedFigureheads.Remove(figurehead);
		}

		// Token: 0x06004709 RID: 18185 RVA: 0x001631C8 File Offset: 0x001613C8
		public override void RegisterEvents()
		{
			CampaignEvents.HeroGainedSkill.AddNonSerializedListener(this, new Action<Hero, SkillObject, int, bool>(this.OnHeroGainedSkill));
			CampaignEvents.HeroLevelledUp.AddNonSerializedListener(this, new Action<Hero, bool>(this.OnHeroLevelledUp));
			CampaignEvents.ArmyCreated.AddNonSerializedListener(this, new Action<Army>(this.OnNewArmyCreated));
			CampaignEvents.ArmyDispersed.AddNonSerializedListener(this, new Action<Army, Army.ArmyDispersionReason, bool>(this.OnArmyDispersed));
			CampaignEvents.QuestLogAddedEvent.AddNonSerializedListener(this, new Action<QuestBase, bool>(this.OnQuestLogAdded));
			CampaignEvents.IssueLogAddedEvent.AddNonSerializedListener(this, new Action<IssueBase, bool>(this.OnIssueLogAdded));
			CampaignEvents.OnGameLoadedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnGameLoaded));
			CampaignEvents.ItemsLooted.AddNonSerializedListener(this, new Action<MobileParty, ItemRoster>(this.OnPlayerPlunderedItems));
			CampaignEvents.RaidCompletedEvent.AddNonSerializedListener(this, new Action<BattleSideEnum, RaidEventComponent>(this.OnRaidCompleted));
			CampaignEvents.OnFigureheadUnlockedEvent.AddNonSerializedListener(this, new Action<Figurehead>(this.OnFigureheadUnlocked));
		}

		// Token: 0x0600470A RID: 18186 RVA: 0x001632BB File Offset: 0x001614BB
		private void OnRaidCompleted(BattleSideEnum winnerSide, RaidEventComponent raidEvent)
		{
			if (raidEvent.IsPlayerMapEvent)
			{
				this._plunderItems.Clear();
			}
		}

		// Token: 0x0600470B RID: 18187 RVA: 0x001632D0 File Offset: 0x001614D0
		private void OnPlayerPlunderedItems(MobileParty mobileParty, ItemRoster items)
		{
			if (mobileParty == MobileParty.MainParty)
			{
				for (int i = 0; i < items.Count; i++)
				{
					ItemRosterElement itemRosterElement = items[i];
					bool flag = false;
					for (int j = 0; j < this._plunderItems.Count; j++)
					{
						if (this._plunderItems[j].EquipmentElement.IsEqualTo(itemRosterElement.EquipmentElement))
						{
							ItemRosterElement itemRosterElement2 = this._plunderItems[j];
							itemRosterElement2.Amount += itemRosterElement.Amount;
							this._plunderItems[j] = itemRosterElement2;
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						this._plunderItems.Add(itemRosterElement);
					}
				}
			}
		}

		// Token: 0x0600470C RID: 18188 RVA: 0x0016338A File Offset: 0x0016158A
		public void SetQuestSortTypeSelection(int questSortTypeSelection)
		{
			this._questSortTypeSelection = questSortTypeSelection;
		}

		// Token: 0x0600470D RID: 18189 RVA: 0x00163393 File Offset: 0x00161593
		public int GetQuestSortTypeSelection()
		{
			return this._questSortTypeSelection;
		}

		// Token: 0x0600470E RID: 18190 RVA: 0x0016339C File Offset: 0x0016159C
		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<bool>("_isMapBarExtended", ref this._isMapBarExtended);
			dataStore.SyncData<List<string>>("_inventoryItemLocks", ref this._inventoryItemLocks);
			dataStore.SyncData<Dictionary<int, Tuple<int, int>>>("_inventorySortPreferences", ref this._inventorySortPreferences);
			dataStore.SyncData<int>("_partySortType", ref this._partySortType);
			dataStore.SyncData<bool>("_isPartySortAscending", ref this._isPartySortAscending);
			dataStore.SyncData<List<string>>("_partyTroopLocks", ref this._partyTroopLocks);
			dataStore.SyncData<List<string>>("_partyPrisonerLocks", ref this._partyPrisonerLocks);
			dataStore.SyncData<List<Hero>>("_encyclopediaBookmarkedHeroes", ref this._encyclopediaBookmarkedHeroes);
			dataStore.SyncData<List<ShipHull>>("_encyclopediaBookmarkedShips", ref this._encyclopediaBookmarkedShips);
			dataStore.SyncData<List<Clan>>("_encyclopediaBookmarkedClans", ref this._encyclopediaBookmarkedClans);
			dataStore.SyncData<List<Concept>>("_encyclopediaBookmarkedConcepts", ref this._encyclopediaBookmarkedConcepts);
			dataStore.SyncData<List<Kingdom>>("_encyclopediaBookmarkedKingdoms", ref this._encyclopediaBookmarkedKingdoms);
			dataStore.SyncData<List<Settlement>>("_encyclopediaBookmarkedSettlements", ref this._encyclopediaBookmarkedSettlements);
			dataStore.SyncData<List<CharacterObject>>("_encyclopediaBookmarkedUnits", ref this._encyclopediaBookmarkedUnits);
			dataStore.SyncData<QuestBase>("_questSelection", ref this._questSelection);
			dataStore.SyncData<List<JournalLog>>("_unExaminedQuestLogs", ref this._unExaminedQuestLogs);
			dataStore.SyncData<List<Army>>("_unExaminedArmies", ref this._unExaminedArmies);
			dataStore.SyncData<bool>("_isCharacterNotificationActive", ref this._isCharacterNotificationActive);
			dataStore.SyncData<int>("_numOfPerks", ref this._numOfPerks);
			dataStore.SyncData<Dictionary<CharacterObject, int>>("_examinedPrisonerCharacterList", ref this._examinedPrisonerCharacterList);
			dataStore.SyncData<List<ItemRosterElement>>("_plunderItems", ref this._plunderItems);
			dataStore.SyncData<List<Figurehead>>("_unexaminedFigureheads", ref this._unexaminedFigureheads);
		}

		// Token: 0x040013CC RID: 5068
		private readonly TextObject _characterNotificationText = new TextObject("{=rlqjkZ9Q}You have {NUMBER} new perks available for selection.", null);

		// Token: 0x040013CD RID: 5069
		private readonly TextObject _questNotificationText = new TextObject("{=FAIYN0vN}You have {NUMBER} new updates to your quests.", null);

		// Token: 0x040013CE RID: 5070
		private readonly TextObject _recruitNotificationText = new TextObject("{=PJMbfSPJ}You have {NUMBER} new prisoners to recruit.", null);

		// Token: 0x040013D0 RID: 5072
		private Dictionary<CharacterObject, int> _examinedPrisonerCharacterList = new Dictionary<CharacterObject, int>();

		// Token: 0x040013D1 RID: 5073
		private int _numOfRecruitablePrisoners;

		// Token: 0x040013D2 RID: 5074
		private List<JournalLog> _unExaminedQuestLogs = new List<JournalLog>();

		// Token: 0x040013D3 RID: 5075
		private IReadOnlyList<JournalLog> _unExaminedQuestLogsReadOnly;

		// Token: 0x040013D4 RID: 5076
		private readonly Dictionary<JournalLog, JournalLogEntry> _unExaminedQuestLogJournalEntries = new Dictionary<JournalLog, JournalLogEntry>();

		// Token: 0x040013D5 RID: 5077
		private bool _isUnExaminedQuestLogJournalEntriesDirty;

		// Token: 0x040013D6 RID: 5078
		private List<Army> _unExaminedArmies = new List<Army>();

		// Token: 0x040013D7 RID: 5079
		private bool _isCharacterNotificationActive;

		// Token: 0x040013D8 RID: 5080
		private int _numOfPerks;

		// Token: 0x040013D9 RID: 5081
		private bool _isMapBarExtended;

		// Token: 0x040013DA RID: 5082
		private List<string> _inventoryItemLocks;

		// Token: 0x040013DB RID: 5083
		[SaveableField(21)]
		private Dictionary<int, Tuple<int, int>> _inventorySortPreferences;

		// Token: 0x040013DC RID: 5084
		private int _partySortType;

		// Token: 0x040013DD RID: 5085
		private bool _isPartySortAscending;

		// Token: 0x040013DE RID: 5086
		private List<string> _partyTroopLocks;

		// Token: 0x040013DF RID: 5087
		private List<string> _partyPrisonerLocks;

		// Token: 0x040013E0 RID: 5088
		private List<Hero> _encyclopediaBookmarkedHeroes;

		// Token: 0x040013E1 RID: 5089
		private List<ShipHull> _encyclopediaBookmarkedShips;

		// Token: 0x040013E2 RID: 5090
		private List<Clan> _encyclopediaBookmarkedClans;

		// Token: 0x040013E3 RID: 5091
		private List<Concept> _encyclopediaBookmarkedConcepts;

		// Token: 0x040013E4 RID: 5092
		private List<Kingdom> _encyclopediaBookmarkedKingdoms;

		// Token: 0x040013E5 RID: 5093
		private List<Settlement> _encyclopediaBookmarkedSettlements;

		// Token: 0x040013E6 RID: 5094
		private List<CharacterObject> _encyclopediaBookmarkedUnits;

		// Token: 0x040013E7 RID: 5095
		private QuestBase _questSelection;

		// Token: 0x040013E8 RID: 5096
		[SaveableField(51)]
		private int _questSortTypeSelection;

		// Token: 0x040013E9 RID: 5097
		private List<ItemRosterElement> _plunderItems;

		// Token: 0x040013EA RID: 5098
		private List<Figurehead> _unexaminedFigureheads;
	}
}
