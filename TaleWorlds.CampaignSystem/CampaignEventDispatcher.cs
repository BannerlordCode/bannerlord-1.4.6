using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.BarterSystem;
using TaleWorlds.CampaignSystem.BarterSystem.Barterables;
using TaleWorlds.CampaignSystem.CharacterCreationContent;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Conversation.Persuasion;
using TaleWorlds.CampaignSystem.CraftingSystem;
using TaleWorlds.CampaignSystem.Election;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.Incidents;
using TaleWorlds.CampaignSystem.Issues;
using TaleWorlds.CampaignSystem.Map;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Naval;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Party.PartyComponents;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Buildings;
using TaleWorlds.CampaignSystem.Settlements.Workshops;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x0200003C RID: 60
	public class CampaignEventDispatcher : CampaignEventReceiver
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0001EA05 File Offset: 0x0001CC05
		public static CampaignEventDispatcher Instance
		{
			get
			{
				Campaign campaign = Campaign.Current;
				if (campaign == null)
				{
					return null;
				}
				return campaign.CampaignEventDispatcher;
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0001EA17 File Offset: 0x0001CC17
		internal CampaignEventDispatcher(IEnumerable<CampaignEventReceiver> eventReceivers)
		{
			this._eventReceivers = eventReceivers.ToArray<CampaignEventReceiver>();
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0001EA2C File Offset: 0x0001CC2C
		internal void AddCampaignEventReceiver(CampaignEventReceiver receiver)
		{
			CampaignEventReceiver[] array = new CampaignEventReceiver[this._eventReceivers.Length + 1];
			for (int i = 0; i < this._eventReceivers.Length; i++)
			{
				array[i] = this._eventReceivers[i];
			}
			array[this._eventReceivers.Length] = receiver;
			this._eventReceivers = array;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0001EA7C File Offset: 0x0001CC7C
		public override void RemoveListeners(object o)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].RemoveListeners(o);
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0001EAA8 File Offset: 0x0001CCA8
		public override void OnPlayerBodyPropertiesChanged()
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPlayerBodyPropertiesChanged();
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0001EAD4 File Offset: 0x0001CCD4
		public override void OnHeroLevelledUp(Hero hero, bool shouldNotify = true)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnHeroLevelledUp(hero, shouldNotify);
			}
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0001EB00 File Offset: 0x0001CD00
		public override void OnHomeHideoutChanged(BanditPartyComponent banditPartyComponent, Hideout oldHomeHideout)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnHomeHideoutChanged(banditPartyComponent, oldHomeHideout);
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0001EB2C File Offset: 0x0001CD2C
		public override void OnCharacterCreationIsOver()
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnCharacterCreationIsOver();
			}
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0001EB58 File Offset: 0x0001CD58
		public override void OnHeroGainedSkill(Hero hero, SkillObject skill, int change = 1, bool shouldNotify = true)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnHeroGainedSkill(hero, skill, change, shouldNotify);
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0001EB88 File Offset: 0x0001CD88
		public override void OnHeroWounded(Hero woundedHero)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnHeroWounded(woundedHero);
			}
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0001EBB4 File Offset: 0x0001CDB4
		public override void OnHeroRelationChanged(Hero effectiveHero, Hero effectiveHeroGainedRelationWith, int relationChange, bool showNotification, ChangeRelationAction.ChangeRelationDetail detail, Hero originalHero, Hero originalGainedRelationWith)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnHeroRelationChanged(effectiveHero, effectiveHeroGainedRelationWith, relationChange, showNotification, detail, originalHero, originalGainedRelationWith);
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0001EBEC File Offset: 0x0001CDEC
		public override void OnLootDistributedToParty(PartyBase winnerParty, PartyBase defeatedParty, ItemRoster lootedItems)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnLootDistributedToParty(winnerParty, defeatedParty, lootedItems);
			}
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0001EC1C File Offset: 0x0001CE1C
		public override void OnHeroOccupationChanged(Hero hero, Occupation oldOccupation)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnHeroOccupationChanged(hero, oldOccupation);
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0001EC48 File Offset: 0x0001CE48
		public override void OnBarterAccepted(Hero offererHero, Hero otherHero, List<Barterable> barters)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnBarterAccepted(offererHero, otherHero, barters);
			}
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0001EC78 File Offset: 0x0001CE78
		public override void OnBarterCanceled(Hero offererHero, Hero otherHero, List<Barterable> barters)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnBarterCanceled(offererHero, otherHero, barters);
			}
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0001ECA8 File Offset: 0x0001CEA8
		public override void OnHeroCreated(Hero hero, bool isBornNaturally = false)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnHeroCreated(hero, isBornNaturally);
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0001ECD4 File Offset: 0x0001CED4
		public override void OnHeroActivated(Hero hero, Hero.CharacterStates previousState)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnHeroActivated(hero, previousState);
			}
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0001ED00 File Offset: 0x0001CF00
		public override void OnQuestLogAdded(QuestBase quest, bool hideInformation)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnQuestLogAdded(quest, hideInformation);
			}
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0001ED2C File Offset: 0x0001CF2C
		public override void OnIssueLogAdded(IssueBase issue, bool hideInformation)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnIssueLogAdded(issue, hideInformation);
			}
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0001ED58 File Offset: 0x0001CF58
		public override void OnClanTierChanged(Clan clan, bool shouldNotify = true)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnClanTierChanged(clan, shouldNotify);
			}
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0001ED84 File Offset: 0x0001CF84
		public override void OnClanChangedKingdom(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail actionDetail, bool showNotification = true)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnClanChangedKingdom(clan, oldKingdom, newKingdom, actionDetail, showNotification);
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0001EDB8 File Offset: 0x0001CFB8
		public override void OnClanDefected(Clan clan, Kingdom oldKingdom, Kingdom newKingdom)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnClanDefected(clan, oldKingdom, newKingdom);
			}
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0001EDE8 File Offset: 0x0001CFE8
		public override void OnClanCreated(Clan clan, bool isCompanion)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnClanCreated(clan, isCompanion);
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0001EE14 File Offset: 0x0001D014
		public override void OnHeroJoinedParty(Hero hero, MobileParty party)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnHeroJoinedParty(hero, party);
			}
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0001EE40 File Offset: 0x0001D040
		public override void OnKingdomDecisionAdded(KingdomDecision decision, bool isPlayerInvolved)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnKingdomDecisionAdded(decision, isPlayerInvolved);
			}
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0001EE6C File Offset: 0x0001D06C
		public override void OnKingdomDecisionCancelled(KingdomDecision decision, bool isPlayerInvolved)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnKingdomDecisionCancelled(decision, isPlayerInvolved);
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0001EE98 File Offset: 0x0001D098
		public override void OnKingdomDecisionConcluded(KingdomDecision decision, DecisionOutcome chosenOutcome, bool isPlayerInvolved)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnKingdomDecisionConcluded(decision, chosenOutcome, isPlayerInvolved);
			}
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0001EEC8 File Offset: 0x0001D0C8
		public override void OnHeroOrPartyTradedGold(ValueTuple<Hero, PartyBase> giver, ValueTuple<Hero, PartyBase> recipient, ValueTuple<int, string> goldAmount, bool showNotification)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnHeroOrPartyTradedGold(giver, recipient, goldAmount, showNotification);
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0001EEF8 File Offset: 0x0001D0F8
		public override void OnHeroOrPartyGaveItem(ValueTuple<Hero, PartyBase> giver, ValueTuple<Hero, PartyBase> receiver, ItemRosterElement itemRosterElement, bool showNotification)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnHeroOrPartyGaveItem(giver, receiver, itemRosterElement, showNotification);
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0001EF28 File Offset: 0x0001D128
		public override void OnBanditPartyRecruited(MobileParty banditParty)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnBanditPartyRecruited(banditParty);
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0001EF54 File Offset: 0x0001D154
		public override void OnArmyCreated(Army army)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnArmyCreated(army);
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0001EF80 File Offset: 0x0001D180
		public override void OnPartyAttachedAnotherParty(MobileParty mobileParty)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPartyAttachedAnotherParty(mobileParty);
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0001EFAC File Offset: 0x0001D1AC
		public override void OnNearbyPartyAddedToPlayerMapEvent(MobileParty mobileParty)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnNearbyPartyAddedToPlayerMapEvent(mobileParty);
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0001EFD8 File Offset: 0x0001D1D8
		public override void OnArmyDispersed(Army army, Army.ArmyDispersionReason reason, bool isPlayersArmy)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnArmyDispersed(army, reason, isPlayersArmy);
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0001F008 File Offset: 0x0001D208
		public override void OnArmyGathered(Army army, IMapPoint gatheringPoint)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnArmyGathered(army, gatheringPoint);
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0001F034 File Offset: 0x0001D234
		public override void OnPerkOpened(Hero hero, PerkObject perk)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPerkOpened(hero, perk);
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0001F060 File Offset: 0x0001D260
		public override void OnPerkReset(Hero hero, PerkObject perk)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPerkReset(hero, perk);
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0001F08C File Offset: 0x0001D28C
		public override void OnPlayerTraitChanged(TraitObject trait, int previousLevel)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPlayerTraitChanged(trait, previousLevel);
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0001F0B8 File Offset: 0x0001D2B8
		public override void OnVillageStateChanged(Village village, Village.VillageStates oldState, Village.VillageStates newState, MobileParty raiderParty)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnVillageStateChanged(village, oldState, newState, raiderParty);
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0001F0E8 File Offset: 0x0001D2E8
		public override void OnSettlementEntered(MobileParty party, Settlement settlement, Hero hero)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnSettlementEntered(party, settlement, hero);
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001F118 File Offset: 0x0001D318
		public override void OnAfterSettlementEntered(MobileParty party, Settlement settlement, Hero hero)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnAfterSettlementEntered(party, settlement, hero);
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0001F148 File Offset: 0x0001D348
		public override void OnBeforeSettlementEntered(MobileParty party, Settlement settlement, Hero hero)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnBeforeSettlementEntered(party, settlement, hero);
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0001F178 File Offset: 0x0001D378
		public override void OnMercenaryTroopChangedInTown(Town town, CharacterObject oldTroopType, CharacterObject newTroopType)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnMercenaryTroopChangedInTown(town, oldTroopType, newTroopType);
			}
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0001F1A8 File Offset: 0x0001D3A8
		public override void OnMercenaryNumberChangedInTown(Town town, int oldNumber, int newNumber)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnMercenaryNumberChangedInTown(town, oldNumber, newNumber);
			}
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0001F1D8 File Offset: 0x0001D3D8
		public override void OnAlleyOccupiedByPlayer(Alley alley, TroopRoster troops)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnAlleyOccupiedByPlayer(alley, troops);
			}
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0001F204 File Offset: 0x0001D404
		public override void OnAlleyOwnerChanged(Alley alley, Hero newOwner, Hero oldOwner)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnAlleyOwnerChanged(alley, newOwner, oldOwner);
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0001F234 File Offset: 0x0001D434
		public override void OnAlleyClearedByPlayer(Alley alley)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnAlleyClearedByPlayer(alley);
			}
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0001F260 File Offset: 0x0001D460
		public override void OnRomanticStateChanged(Hero hero1, Hero hero2, Romance.RomanceLevelEnum romanceLevel)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnRomanticStateChanged(hero1, hero2, romanceLevel);
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0001F290 File Offset: 0x0001D490
		public override void OnBeforeHeroesMarried(Hero hero1, Hero hero2, bool showNotification)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnBeforeHeroesMarried(hero1, hero2, showNotification);
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0001F2C0 File Offset: 0x0001D4C0
		public override void OnPlayerEliminatedFromTournament(int round, Town town)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPlayerEliminatedFromTournament(round, town);
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0001F2EC File Offset: 0x0001D4EC
		public override void OnPlayerStartedTournamentMatch(Town town)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPlayerStartedTournamentMatch(town);
			}
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0001F318 File Offset: 0x0001D518
		public override void OnTournamentStarted(Town town)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnTournamentStarted(town);
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0001F344 File Offset: 0x0001D544
		public override void OnTournamentFinished(CharacterObject winner, MBReadOnlyList<CharacterObject> participants, Town town, ItemObject prize)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnTournamentFinished(winner, participants, town, prize);
			}
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0001F374 File Offset: 0x0001D574
		public override void OnTournamentCancelled(Town town)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnTournamentCancelled(town);
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0001F3A0 File Offset: 0x0001D5A0
		public override void OnWarDeclared(IFaction faction1, IFaction faction2, DeclareWarAction.DeclareWarDetail declareWarDetail)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnWarDeclared(faction1, faction2, declareWarDetail);
			}
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0001F3D0 File Offset: 0x0001D5D0
		public override void OnRulingClanChanged(Kingdom kingdom, Clan oldRulingClan)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnRulingClanChanged(kingdom, oldRulingClan);
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0001F3FC File Offset: 0x0001D5FC
		public override void OnStartBattle(PartyBase attackerParty, PartyBase defenderParty, object subject, bool showNotification)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnStartBattle(attackerParty, defenderParty, subject, showNotification);
			}
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0001F42C File Offset: 0x0001D62C
		public override void OnRebellionFinished(Settlement settlement, Clan oldOwnerClan)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnRebellionFinished(settlement, oldOwnerClan);
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0001F458 File Offset: 0x0001D658
		public override void TownRebelliousStateChanged(Town town, bool rebelliousState)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].TownRebelliousStateChanged(town, rebelliousState);
			}
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0001F484 File Offset: 0x0001D684
		public override void OnRebelliousClanDisbandedAtSettlement(Settlement settlement, Clan rebelliousClan)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnRebelliousClanDisbandedAtSettlement(settlement, rebelliousClan);
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0001F4B0 File Offset: 0x0001D6B0
		public override void OnItemsLooted(MobileParty mobileParty, ItemRoster items)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnItemsLooted(mobileParty, items);
			}
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0001F4DC File Offset: 0x0001D6DC
		public override void OnMobilePartyDestroyed(MobileParty mobileParty, PartyBase destroyerParty)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnMobilePartyDestroyed(mobileParty, destroyerParty);
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0001F508 File Offset: 0x0001D708
		public override void OnMobilePartyCreated(MobileParty party)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnMobilePartyCreated(party);
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0001F534 File Offset: 0x0001D734
		public override void OnMapInteractableCreated(IInteractablePoint interactable)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnMapInteractableCreated(interactable);
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0001F560 File Offset: 0x0001D760
		public override void OnMapInteractableDestroyed(IInteractablePoint interactable)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnMapInteractableDestroyed(interactable);
			}
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0001F58C File Offset: 0x0001D78C
		public override void OnMobilePartyQuestStatusChanged(MobileParty party, bool isUsedByQuest)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnMobilePartyQuestStatusChanged(party, isUsedByQuest);
			}
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0001F5B8 File Offset: 0x0001D7B8
		public override void OnHeroKilled(Hero victim, Hero killer, KillCharacterAction.KillCharacterActionDetail detail, bool showNotification = true)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnHeroKilled(victim, killer, detail, showNotification);
			}
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0001F5E8 File Offset: 0x0001D7E8
		public override void OnBeforeHeroKilled(Hero victim, Hero killer, KillCharacterAction.KillCharacterActionDetail detail, bool showNotification = true)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnBeforeHeroKilled(victim, killer, detail, showNotification);
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0001F618 File Offset: 0x0001D818
		public override void OnChildEducationCompleted(Hero hero, int age)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnChildEducationCompleted(hero, age);
			}
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0001F644 File Offset: 0x0001D844
		public override void OnHeroComesOfAge(Hero hero)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnHeroComesOfAge(hero);
			}
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0001F670 File Offset: 0x0001D870
		public override void OnHeroReachesTeenAge(Hero hero)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnHeroReachesTeenAge(hero);
			}
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0001F69C File Offset: 0x0001D89C
		public override void OnHeroGrowsOutOfInfancy(Hero hero)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnHeroGrowsOutOfInfancy(hero);
			}
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0001F6C8 File Offset: 0x0001D8C8
		public override void OnCharacterDefeated(Hero winner, Hero loser)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnCharacterDefeated(winner, loser);
			}
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0001F6F4 File Offset: 0x0001D8F4
		public override void OnHeroPrisonerTaken(PartyBase capturer, Hero prisoner)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnHeroPrisonerTaken(capturer, prisoner);
			}
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0001F720 File Offset: 0x0001D920
		public override void OnHeroPrisonerReleased(Hero prisoner, PartyBase party, IFaction capturerFaction, EndCaptivityDetail detail, bool showNotification = true)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnHeroPrisonerReleased(prisoner, party, capturerFaction, detail, showNotification);
			}
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0001F754 File Offset: 0x0001D954
		public override void OnCharacterBecameFugitive(Hero hero, bool showNotification)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnCharacterBecameFugitive(hero, showNotification);
			}
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0001F780 File Offset: 0x0001D980
		public override void OnPlayerLearnsAboutHero(Hero hero)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPlayerLearnsAboutHero(hero);
			}
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0001F7AC File Offset: 0x0001D9AC
		public override void OnPlayerMetHero(Hero hero)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPlayerMetHero(hero);
			}
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0001F7D8 File Offset: 0x0001D9D8
		public override void OnRenownGained(Hero hero, int gainedRenown, bool doNotNotify)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnRenownGained(hero, gainedRenown, doNotNotify);
			}
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0001F808 File Offset: 0x0001DA08
		public override void OnCrimeRatingChanged(IFaction kingdom, float deltaCrimeAmount)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnCrimeRatingChanged(kingdom, deltaCrimeAmount);
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0001F834 File Offset: 0x0001DA34
		public override void OnNewCompanionAdded(Hero newCompanion)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnNewCompanionAdded(newCompanion);
			}
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0001F860 File Offset: 0x0001DA60
		public override void OnAfterMissionStarted(IMission iMission)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnAfterMissionStarted(iMission);
			}
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0001F88C File Offset: 0x0001DA8C
		public override void OnGameMenuOpened(MenuCallbackArgs args)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnGameMenuOpened(args);
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0001F8B8 File Offset: 0x0001DAB8
		public override void OnMakePeace(IFaction side1Faction, IFaction side2Faction, MakePeaceAction.MakePeaceDetail detail)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnMakePeace(side1Faction, side2Faction, detail);
			}
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0001F8E8 File Offset: 0x0001DAE8
		public override void OnKingdomDestroyed(Kingdom destroyedKingdom)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnKingdomDestroyed(destroyedKingdom);
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0001F914 File Offset: 0x0001DB14
		public override void CanKingdomBeDiscontinued(Kingdom kingdom, ref bool result)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].CanKingdomBeDiscontinued(kingdom, ref result);
			}
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0001F940 File Offset: 0x0001DB40
		public override void OnKingdomCreated(Kingdom createdKingdom)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnKingdomCreated(createdKingdom);
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0001F96C File Offset: 0x0001DB6C
		public override void OnVillageBecomeNormal(Village village)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnVillageBecomeNormal(village);
			}
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0001F998 File Offset: 0x0001DB98
		public override void OnVillageBeingRaided(Village village)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnVillageBeingRaided(village);
			}
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0001F9C4 File Offset: 0x0001DBC4
		public override void OnVillageLooted(Village village)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnVillageLooted(village);
			}
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0001F9F0 File Offset: 0x0001DBF0
		public override void OnConversationEnded(IEnumerable<CharacterObject> characters)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnConversationEnded(characters);
			}
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0001FA1C File Offset: 0x0001DC1C
		public override void OnAgentJoinedConversation(IAgent agent)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnAgentJoinedConversation(agent);
			}
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0001FA48 File Offset: 0x0001DC48
		public override void OnMapEventEnded(MapEvent mapEvent)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnMapEventEnded(mapEvent);
			}
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0001FA74 File Offset: 0x0001DC74
		public override void OnMapEventStarted(MapEvent mapEvent, PartyBase attackerParty, PartyBase defenderParty)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnMapEventStarted(mapEvent, attackerParty, defenderParty);
			}
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0001FAA4 File Offset: 0x0001DCA4
		public override void OnPrisonersChangeInSettlement(Settlement settlement, FlattenedTroopRoster prisonerRoster, Hero prisonerHero, bool takenFromDungeon)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPrisonersChangeInSettlement(settlement, prisonerRoster, prisonerHero, takenFromDungeon);
			}
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0001FAD4 File Offset: 0x0001DCD4
		public override void OnMissionStarted(IMission mission)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnMissionStarted(mission);
			}
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0001FB00 File Offset: 0x0001DD00
		public override void OnPlayerBoardGameOver(Hero opposingHero, BoardGameHelper.BoardGameState state)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPlayerBoardGameOver(opposingHero, state);
			}
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0001FB2C File Offset: 0x0001DD2C
		public override void OnRansomOfferedToPlayer(Hero captiveHero)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnRansomOfferedToPlayer(captiveHero);
			}
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0001FB58 File Offset: 0x0001DD58
		public override void OnRansomOfferCancelled(Hero captiveHero)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnRansomOfferCancelled(captiveHero);
			}
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0001FB84 File Offset: 0x0001DD84
		public override void OnPeaceOfferedToPlayer(IFaction opponentFaction, int tributeAmount, int tributeDurationInDays)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPeaceOfferedToPlayer(opponentFaction, tributeAmount, tributeDurationInDays);
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0001FBB4 File Offset: 0x0001DDB4
		public override void OnTradeAgreementSigned(Kingdom kingdom, Kingdom other)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnTradeAgreementSigned(kingdom, other);
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0001FBE0 File Offset: 0x0001DDE0
		public override void OnPeaceOfferResolved(IFaction opponentFaction)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPeaceOfferResolved(opponentFaction);
			}
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0001FC0C File Offset: 0x0001DE0C
		public override void OnMarriageOfferedToPlayer(Hero suitor, Hero maiden)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnMarriageOfferedToPlayer(suitor, maiden);
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0001FC38 File Offset: 0x0001DE38
		public override void OnMarriageOfferCanceled(Hero suitor, Hero maiden)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnMarriageOfferCanceled(suitor, maiden);
			}
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0001FC64 File Offset: 0x0001DE64
		public override void OnVassalOrMercenaryServiceOfferedToPlayer(Kingdom offeredKingdom)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnVassalOrMercenaryServiceOfferedToPlayer(offeredKingdom);
			}
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0001FC90 File Offset: 0x0001DE90
		public override void OnCommonAreaStateChanged(Alley alley, Alley.AreaState oldState, Alley.AreaState newState)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnCommonAreaStateChanged(alley, oldState, newState);
			}
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0001FCC0 File Offset: 0x0001DEC0
		public override void OnVassalOrMercenaryServiceOfferCanceled(Kingdom offeredKingdom)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnVassalOrMercenaryServiceOfferCanceled(offeredKingdom);
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0001FCEC File Offset: 0x0001DEEC
		public override void BeforeMissionOpened()
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].BeforeMissionOpened();
			}
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0001FD18 File Offset: 0x0001DF18
		public override void OnPartyRemoved(PartyBase party)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPartyRemoved(party);
			}
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0001FD44 File Offset: 0x0001DF44
		public override void OnPartySizeChanged(PartyBase party)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPartySizeChanged(party);
			}
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0001FD70 File Offset: 0x0001DF70
		public override void OnSettlementOwnerChanged(Settlement settlement, bool openToClaim, Hero newOwner, Hero oldOwner, Hero capturerHero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail detail)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnSettlementOwnerChanged(settlement, openToClaim, newOwner, oldOwner, capturerHero, detail);
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0001FDA4 File Offset: 0x0001DFA4
		public override void OnGovernorChanged(Town fortification, Hero oldGovernor, Hero newGovernor)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnGovernorChanged(fortification, oldGovernor, newGovernor);
			}
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0001FDD4 File Offset: 0x0001DFD4
		public override void OnSettlementLeft(MobileParty party, Settlement settlement)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnSettlementLeft(party, settlement);
			}
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0001FE00 File Offset: 0x0001E000
		public override void Tick(float dt)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].Tick(dt);
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0001FE2C File Offset: 0x0001E02C
		public override void OnSessionStart(CampaignGameStarter campaignGameStarter)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnSessionStart(campaignGameStarter);
			}
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0001FE58 File Offset: 0x0001E058
		public override void OnAfterSessionStart(CampaignGameStarter campaignGameStarter)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnAfterSessionStart(campaignGameStarter);
			}
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0001FE84 File Offset: 0x0001E084
		public override void OnNewGameCreated(CampaignGameStarter campaignGameStarter)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnNewGameCreated(campaignGameStarter);
			}
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0001FEB0 File Offset: 0x0001E0B0
		public override void OnGameEarlyLoaded(CampaignGameStarter campaignGameStarter)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnGameEarlyLoaded(campaignGameStarter);
			}
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0001FEDC File Offset: 0x0001E0DC
		public override void OnGameLoaded(CampaignGameStarter campaignGameStarter)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnGameLoaded(campaignGameStarter);
			}
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0001FF08 File Offset: 0x0001E108
		public override void OnGameLoadFinished()
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnGameLoadFinished();
			}
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0001FF34 File Offset: 0x0001E134
		public override void OnPartyJoinedArmy(MobileParty mobileParty)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPartyJoinedArmy(mobileParty);
			}
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0001FF60 File Offset: 0x0001E160
		public override void OnPartyRemovedFromArmy(MobileParty mobileParty)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPartyRemovedFromArmy(mobileParty);
			}
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0001FF8C File Offset: 0x0001E18C
		public override void OnPlayerArmyLeaderChangedBehavior()
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPlayerArmyLeaderChangedBehavior();
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0001FFB8 File Offset: 0x0001E1B8
		public override void OnArmyOverlaySetDirty()
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnArmyOverlaySetDirty();
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0001FFE4 File Offset: 0x0001E1E4
		public override void OnPlayerDesertedBattle(int sacrificedMenCount)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPlayerDesertedBattle(sacrificedMenCount);
			}
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00020010 File Offset: 0x0001E210
		public override void MissionTick(float dt)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].MissionTick(dt);
			}
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0002003C File Offset: 0x0001E23C
		public override void OnChildConceived(Hero mother)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnChildConceived(mother);
			}
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00020068 File Offset: 0x0001E268
		public override void OnGivenBirth(Hero mother, List<Hero> aliveChildren, int stillbornCount)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnGivenBirth(mother, aliveChildren, stillbornCount);
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00020098 File Offset: 0x0001E298
		public override void OnUnitRecruited(CharacterObject character, int amount)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnUnitRecruited(character, amount);
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x000200C4 File Offset: 0x0001E2C4
		public override void OnPlayerBattleEnd(MapEvent mapEvent)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPlayerBattleEnd(mapEvent);
			}
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x000200F0 File Offset: 0x0001E2F0
		public override void OnMissionEnded(IMission mission)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnMissionEnded(mission);
			}
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0002011C File Offset: 0x0001E31C
		public override void TickPartialHourlyAi(MobileParty party)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].TickPartialHourlyAi(party);
			}
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00020148 File Offset: 0x0001E348
		public override void QuarterDailyPartyTick(MobileParty party)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].QuarterDailyPartyTick(party);
			}
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00020174 File Offset: 0x0001E374
		public override void AiHourlyTick(MobileParty party, PartyThinkParams partyThinkParams)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].AiHourlyTick(party, partyThinkParams);
			}
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x000201A0 File Offset: 0x0001E3A0
		public override void HourlyTick()
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].HourlyTick();
			}
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x000201CC File Offset: 0x0001E3CC
		public override void QuarterHourlyTick()
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].QuarterHourlyTick();
			}
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x000201F8 File Offset: 0x0001E3F8
		public override void HourlyTickParty(MobileParty mobileParty)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].HourlyTickParty(mobileParty);
			}
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00020224 File Offset: 0x0001E424
		public override void HourlyTickSettlement(Settlement settlement)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].HourlyTickSettlement(settlement);
			}
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00020250 File Offset: 0x0001E450
		public override void HourlyTickClan(Clan clan)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].HourlyTickClan(clan);
			}
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0002027C File Offset: 0x0001E47C
		public override void DailyTick()
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].DailyTick();
			}
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x000202A8 File Offset: 0x0001E4A8
		public override void DailyTickParty(MobileParty mobileParty)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].DailyTickParty(mobileParty);
			}
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x000202D4 File Offset: 0x0001E4D4
		public override void DailyTickTown(Town town)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].DailyTickTown(town);
			}
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00020300 File Offset: 0x0001E500
		public override void DailyTickSettlement(Settlement settlement)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].DailyTickSettlement(settlement);
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0002032C File Offset: 0x0001E52C
		public override void DailyTickHero(Hero hero)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].DailyTickHero(hero);
			}
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00020358 File Offset: 0x0001E558
		public override void DailyTickClan(Clan clan)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].DailyTickClan(clan);
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00020384 File Offset: 0x0001E584
		public override void WeeklyTick()
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].WeeklyTick();
			}
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x000203B0 File Offset: 0x0001E5B0
		public override void CollectAvailableTutorials(ref List<CampaignTutorial> tutorials)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].CollectAvailableTutorials(ref tutorials);
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x000203DC File Offset: 0x0001E5DC
		public override void OnTutorialCompleted(string tutorial)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnTutorialCompleted(tutorial);
			}
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00020408 File Offset: 0x0001E608
		public override void BeforeGameMenuOpened(MenuCallbackArgs args)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].BeforeGameMenuOpened(args);
			}
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00020434 File Offset: 0x0001E634
		public override void AfterGameMenuInitialized(MenuCallbackArgs args)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].AfterGameMenuInitialized(args);
			}
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00020460 File Offset: 0x0001E660
		public override void OnBarterablesRequested(BarterData args)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnBarterablesRequested(args);
			}
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0002048C File Offset: 0x0001E68C
		public override void OnPartyVisibilityChanged(PartyBase party)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPartyVisibilityChanged(party);
			}
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x000204B8 File Offset: 0x0001E6B8
		public override void OnCompanionRemoved(Hero companion, RemoveCompanionAction.RemoveCompanionDetail detail)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnCompanionRemoved(companion, detail);
			}
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x000204E4 File Offset: 0x0001E6E4
		public override void TrackDetected(Track track)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].TrackDetected(track);
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00020510 File Offset: 0x0001E710
		public override void TrackLost(Track track)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].TrackLost(track);
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0002053C File Offset: 0x0001E73C
		public override void LocationCharactersAreReadyToSpawn(Dictionary<string, int> unusedUsablePointCount)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].LocationCharactersAreReadyToSpawn(unusedUsablePointCount);
			}
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00020568 File Offset: 0x0001E768
		public override void LocationCharactersSimulated()
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].LocationCharactersSimulated();
			}
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00020594 File Offset: 0x0001E794
		public override void OnBeforePlayerAgentSpawn(ref MatrixFrame spawnFrame)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnBeforePlayerAgentSpawn(ref spawnFrame);
			}
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x000205C0 File Offset: 0x0001E7C0
		public override void OnPlayerAgentSpawned()
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPlayerAgentSpawned();
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x000205EC File Offset: 0x0001E7EC
		public override void OnPlayerUpgradedTroops(CharacterObject upgradeFromTroop, CharacterObject upgradeToTroop, int number)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPlayerUpgradedTroops(upgradeFromTroop, upgradeToTroop, number);
			}
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0002061C File Offset: 0x0001E81C
		public override void OnHeroCombatHit(CharacterObject attackerTroop, CharacterObject attackedTroop, PartyBase party, WeaponComponentData usedWeapon, bool isFatal, int xp)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnHeroCombatHit(attackerTroop, attackedTroop, party, usedWeapon, isFatal, xp);
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00020650 File Offset: 0x0001E850
		public override void OnCharacterPortraitPopUpOpened(CharacterObject character)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnCharacterPortraitPopUpOpened(character);
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0002067C File Offset: 0x0001E87C
		public override void OnCharacterPortraitPopUpClosed()
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnCharacterPortraitPopUpClosed();
			}
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x000206A8 File Offset: 0x0001E8A8
		public override void OnPlayerStartTalkFromMenu(Hero hero)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPlayerStartTalkFromMenu(hero);
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x000206D4 File Offset: 0x0001E8D4
		public override void OnGameMenuOptionSelected(GameMenu gameMenu, GameMenuOption gameMenuOption)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnGameMenuOptionSelected(gameMenu, gameMenuOption);
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00020700 File Offset: 0x0001E900
		public override void OnPlayerStartRecruitment(CharacterObject recruitTroopCharacter)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPlayerStartRecruitment(recruitTroopCharacter);
			}
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0002072C File Offset: 0x0001E92C
		public override void OnBeforePlayerCharacterChanged(Hero oldPlayer, Hero newPlayer)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnBeforePlayerCharacterChanged(oldPlayer, newPlayer);
			}
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00020758 File Offset: 0x0001E958
		public override void OnPlayerCharacterChanged(Hero oldPlayer, Hero newPlayer, MobileParty newPlayerParty, bool isMainPartyChanged)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPlayerCharacterChanged(oldPlayer, newPlayer, newPlayerParty, isMainPartyChanged);
			}
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00020788 File Offset: 0x0001E988
		public override void OnClanLeaderChanged(Hero oldLeader, Hero newLeader)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnClanLeaderChanged(oldLeader, newLeader);
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x000207B4 File Offset: 0x0001E9B4
		public override void OnSiegeEventStarted(SiegeEvent siegeEvent)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnSiegeEventStarted(siegeEvent);
			}
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x000207E0 File Offset: 0x0001E9E0
		public override void OnPlayerSiegeStarted()
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPlayerSiegeStarted();
			}
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0002080C File Offset: 0x0001EA0C
		public override void OnSiegeEventEnded(SiegeEvent siegeEvent)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnSiegeEventEnded(siegeEvent);
			}
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00020838 File Offset: 0x0001EA38
		public override void OnSiegeAftermathApplied(MobileParty attackerParty, Settlement settlement, SiegeAftermathAction.SiegeAftermath aftermathType, Clan previousSettlementOwner, Dictionary<MobileParty, float> partyContributions)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnSiegeAftermathApplied(attackerParty, settlement, aftermathType, previousSettlementOwner, partyContributions);
			}
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0002086C File Offset: 0x0001EA6C
		public override void OnSiegeBombardmentHit(MobileParty besiegerParty, Settlement besiegedSettlement, BattleSideEnum side, SiegeEngineType weapon, SiegeBombardTargets target)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnSiegeBombardmentHit(besiegerParty, besiegedSettlement, side, weapon, target);
			}
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x000208A0 File Offset: 0x0001EAA0
		public override void OnSiegeBombardmentWallHit(MobileParty besiegerParty, Settlement besiegedSettlement, BattleSideEnum side, SiegeEngineType weapon, bool isWallCracked)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnSiegeBombardmentWallHit(besiegerParty, besiegedSettlement, side, weapon, isWallCracked);
			}
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x000208D4 File Offset: 0x0001EAD4
		public override void OnSiegeEngineDestroyed(MobileParty besiegerParty, Settlement besiegedSettlement, BattleSideEnum side, SiegeEngineType destroyedEngine)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnSiegeEngineDestroyed(besiegerParty, besiegedSettlement, side, destroyedEngine);
			}
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00020904 File Offset: 0x0001EB04
		public override void OnTradeRumorIsTaken(List<TradeRumor> newRumors, Settlement sourceSettlement = null)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnTradeRumorIsTaken(newRumors, sourceSettlement);
			}
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00020930 File Offset: 0x0001EB30
		public override void OnCheckForIssue(Hero hero)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnCheckForIssue(hero);
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0002095C File Offset: 0x0001EB5C
		public override void OnIssueUpdated(IssueBase issue, IssueBase.IssueUpdateDetails details, Hero issueSolver)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnIssueUpdated(issue, details, issueSolver);
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0002098C File Offset: 0x0001EB8C
		public override void OnTroopsDeserted(MobileParty mobileParty, TroopRoster desertedTroops)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnTroopsDeserted(mobileParty, desertedTroops);
			}
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x000209B8 File Offset: 0x0001EBB8
		public override void OnTroopRecruited(Hero recruiterHero, Settlement recruitmentSettlement, Hero recruitmentSource, CharacterObject troop, int amount)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnTroopRecruited(recruiterHero, recruitmentSettlement, recruitmentSource, troop, amount);
			}
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x000209EC File Offset: 0x0001EBEC
		public override void OnTroopGivenToSettlement(Hero giverHero, Settlement recipientSettlement, TroopRoster roster)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnTroopGivenToSettlement(giverHero, recipientSettlement, roster);
			}
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00020A1C File Offset: 0x0001EC1C
		public override void OnItemSold(PartyBase receiverParty, PartyBase payerParty, ItemRosterElement itemRosterElement, int number, Settlement currentSettlement)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnItemSold(receiverParty, payerParty, itemRosterElement, number, currentSettlement);
			}
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00020A50 File Offset: 0x0001EC50
		public override void OnCaravanTransactionCompleted(MobileParty caravanParty, Town town, List<ValueTuple<EquipmentElement, int>> itemRosterElements)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnCaravanTransactionCompleted(caravanParty, town, itemRosterElements);
			}
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00020A80 File Offset: 0x0001EC80
		public override void OnPrisonerSold(PartyBase sellerParty, PartyBase buyerParty, TroopRoster prisoners)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPrisonerSold(sellerParty, buyerParty, prisoners);
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00020AB0 File Offset: 0x0001ECB0
		public override void OnPartyDisbanded(MobileParty disbandParty, Settlement relatedSettlement)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPartyDisbanded(disbandParty, relatedSettlement);
			}
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00020ADC File Offset: 0x0001ECDC
		public override void OnPartyDisbandStarted(MobileParty disbandParty)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPartyDisbandStarted(disbandParty);
			}
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00020B08 File Offset: 0x0001ED08
		public override void OnPartyDisbandCanceled(MobileParty disbandParty)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPartyDisbandCanceled(disbandParty);
			}
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00020B34 File Offset: 0x0001ED34
		public override void OnBuildingLevelChanged(Town town, Building building, int levelChange)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnBuildingLevelChanged(town, building, levelChange);
			}
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00020B64 File Offset: 0x0001ED64
		public override void OnHideoutSpotted(PartyBase party, PartyBase hideoutParty)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnHideoutSpotted(party, hideoutParty);
			}
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00020B90 File Offset: 0x0001ED90
		public override void OnHideoutDeactivated(Settlement hideout)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnHideoutDeactivated(hideout);
			}
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00020BBC File Offset: 0x0001EDBC
		public override void OnHeroSharedFoodWithAnother(Hero supporterHero, Hero supportedHero, float influence)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnHeroSharedFoodWithAnother(supporterHero, supportedHero, influence);
			}
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00020BEC File Offset: 0x0001EDEC
		public override void OnItemsDiscardedByPlayer(ItemRoster roster)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnItemsDiscardedByPlayer(roster);
			}
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00020C18 File Offset: 0x0001EE18
		public override void OnPlayerInventoryExchange(List<ValueTuple<ItemRosterElement, int>> purchasedItems, List<ValueTuple<ItemRosterElement, int>> soldItems, bool isTrading)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPlayerInventoryExchange(purchasedItems, soldItems, isTrading);
			}
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00020C48 File Offset: 0x0001EE48
		public override void OnPersuasionProgressCommitted(Tuple<PersuasionOptionArgs, PersuasionOptionResult> progress)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPersuasionProgressCommitted(progress);
			}
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00020C74 File Offset: 0x0001EE74
		public override void OnQuestCompleted(QuestBase quest, QuestBase.QuestCompleteDetails detail)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnQuestCompleted(quest, detail);
			}
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00020CA0 File Offset: 0x0001EEA0
		public override void OnQuestStarted(QuestBase quest)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnQuestStarted(quest);
			}
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00020CCC File Offset: 0x0001EECC
		public override void OnItemProduced(ItemObject itemObject, Settlement settlement, int count)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnItemProduced(itemObject, settlement, count);
			}
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00020CFC File Offset: 0x0001EEFC
		public override void OnItemConsumed(ItemObject itemObject, Settlement settlement, int count)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnItemConsumed(itemObject, settlement, count);
			}
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00020D2C File Offset: 0x0001EF2C
		public override void OnPartyConsumedFood(MobileParty party)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPartyConsumedFood(party);
			}
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00020D58 File Offset: 0x0001EF58
		public override void OnNewIssueCreated(IssueBase issue)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnNewIssueCreated(issue);
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00020D84 File Offset: 0x0001EF84
		public override void OnIssueOwnerChanged(IssueBase issue, Hero oldOwner)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnIssueOwnerChanged(issue, oldOwner);
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00020DB0 File Offset: 0x0001EFB0
		public override void OnBeforeMainCharacterDied(Hero victim, Hero killer, KillCharacterAction.KillCharacterActionDetail detail, bool showNotification = true)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnBeforeMainCharacterDied(victim, killer, detail, showNotification);
			}
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00020DE0 File Offset: 0x0001EFE0
		public override void OnGameOver()
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnGameOver();
			}
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00020E0C File Offset: 0x0001F00C
		public override void SiegeCompleted(Settlement siegeSettlement, MobileParty attackerParty, bool isWin, MapEvent.BattleTypes battleType)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].SiegeCompleted(siegeSettlement, attackerParty, isWin, battleType);
			}
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00020E3C File Offset: 0x0001F03C
		public override void AfterSiegeCompleted(Settlement siegeSettlement, MobileParty attackerParty, bool isWin, MapEvent.BattleTypes battleType)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].AfterSiegeCompleted(siegeSettlement, attackerParty, isWin, battleType);
			}
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00020E6C File Offset: 0x0001F06C
		public override void SiegeEngineBuilt(SiegeEvent siegeEvent, BattleSideEnum side, SiegeEngineType siegeEngine)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].SiegeEngineBuilt(siegeEvent, side, siegeEngine);
			}
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00020E9C File Offset: 0x0001F09C
		public override void RaidCompleted(BattleSideEnum winnerSide, RaidEventComponent raidEvent)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].RaidCompleted(winnerSide, raidEvent);
			}
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00020EC8 File Offset: 0x0001F0C8
		public override void ForceSuppliesCompleted(BattleSideEnum winnerSide, ForceSuppliesEventComponent forceSuppliesEvent)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].ForceSuppliesCompleted(winnerSide, forceSuppliesEvent);
			}
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00020EF4 File Offset: 0x0001F0F4
		public override void ForceVolunteersCompleted(BattleSideEnum winnerSide, ForceVolunteersEventComponent forceVolunteersEvent)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].ForceVolunteersCompleted(winnerSide, forceVolunteersEvent);
			}
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00020F20 File Offset: 0x0001F120
		public override void OnHideoutBattleCompleted(BattleSideEnum winnerSide, HideoutEventComponent hideoutEventComponent, HideoutEventComponent.HideoutBattleEndState battleEndState)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnHideoutBattleCompleted(winnerSide, hideoutEventComponent, battleEndState);
			}
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00020F50 File Offset: 0x0001F150
		public override void OnClanDestroyed(Clan destroyedClan)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnClanDestroyed(destroyedClan);
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00020F7C File Offset: 0x0001F17C
		public override void OnNewItemCrafted(ItemObject itemObject, ItemModifier overriddenItemModifier, bool isCraftingOrderItem)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnNewItemCrafted(itemObject, overriddenItemModifier, isCraftingOrderItem);
			}
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00020FAC File Offset: 0x0001F1AC
		public override void OnWorkshopOwnerChanged(Workshop workshop, Hero oldOwner)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnWorkshopOwnerChanged(workshop, oldOwner);
			}
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00020FD8 File Offset: 0x0001F1D8
		public override void OnWorkshopInitialized(Workshop workshop)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnWorkshopInitialized(workshop);
			}
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00021004 File Offset: 0x0001F204
		public override void OnWorkshopTypeChanged(Workshop workshop)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnWorkshopTypeChanged(workshop);
			}
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00021030 File Offset: 0x0001F230
		public override void OnMainPartyPrisonerRecruited(FlattenedTroopRoster roster)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnMainPartyPrisonerRecruited(roster);
			}
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0002105C File Offset: 0x0001F25C
		public override void OnPrisonerDonatedToSettlement(MobileParty donatingParty, FlattenedTroopRoster donatedPrisoners, Settlement donatedSettlement)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPrisonerDonatedToSettlement(donatingParty, donatedPrisoners, donatedSettlement);
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0002108C File Offset: 0x0001F28C
		public override void OnEquipmentSmeltedByHero(Hero hero, EquipmentElement equipmentElement)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnEquipmentSmeltedByHero(hero, equipmentElement);
			}
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x000210B8 File Offset: 0x0001F2B8
		public override void OnPrisonerTaken(FlattenedTroopRoster roster)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPrisonerTaken(roster);
			}
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x000210E4 File Offset: 0x0001F2E4
		public override void OnBeforeSave()
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnBeforeSave();
			}
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00021110 File Offset: 0x0001F310
		public override void OnSaveStarted()
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnSaveStarted();
			}
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0002113C File Offset: 0x0001F33C
		public override void OnSaveOver(bool isSuccessful, string saveName)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnSaveOver(isSuccessful, saveName);
			}
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00021168 File Offset: 0x0001F368
		public override void CollectMetadataEntries(List<KeyValuePair<string, string>> pairs)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].CollectMetadataEntries(pairs);
			}
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00021194 File Offset: 0x0001F394
		public override void OnPrisonerReleased(FlattenedTroopRoster roster)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPrisonerReleased(roster);
			}
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x000211C0 File Offset: 0x0001F3C0
		public override void OnHeroChangedClan(Hero hero, Clan oldClan)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnHeroChangedClan(hero, oldClan);
			}
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x000211EC File Offset: 0x0001F3EC
		public override void OnHeroGetsBusy(Hero hero, HeroGetsBusyReasons heroGetsBusyReason)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnHeroGetsBusy(hero, heroGetsBusyReason);
			}
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00021218 File Offset: 0x0001F418
		public override void OnPlayerTradeProfit(int profit)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPlayerTradeProfit(profit);
			}
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00021244 File Offset: 0x0001F444
		public override void CraftingPartUnlocked(CraftingPiece craftingPiece)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].CraftingPartUnlocked(craftingPiece);
			}
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00021270 File Offset: 0x0001F470
		public override void OnClanEarnedGoldFromTribute(Clan receiverClan, IFaction payingFaction)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnClanEarnedGoldFromTribute(receiverClan, payingFaction);
			}
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0002129C File Offset: 0x0001F49C
		public override void OnCollectLootItems(PartyBase winnerParty, ItemRoster gainedLoots)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnCollectLootItems(winnerParty, gainedLoots);
			}
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x000212C8 File Offset: 0x0001F4C8
		public override void OnHeroTeleportationRequested(Hero hero, Settlement targetSettlement, MobileParty targetParty, TeleportHeroAction.TeleportationDetail detail)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnHeroTeleportationRequested(hero, targetSettlement, targetParty, detail);
			}
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x000212F8 File Offset: 0x0001F4F8
		public override void OnClanInfluenceChanged(Clan clan, float change)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnClanInfluenceChanged(clan, change);
			}
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00021324 File Offset: 0x0001F524
		public override void OnPlayerPartyKnockedOrKilledTroop(CharacterObject strikedTroop)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPlayerPartyKnockedOrKilledTroop(strikedTroop);
			}
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00021350 File Offset: 0x0001F550
		public override void OnPlayerEarnedGoldFromAsset(DefaultClanFinanceModel.AssetIncomeType incomeType, int incomeAmount)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPlayerEarnedGoldFromAsset(incomeType, incomeAmount);
			}
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0002137C File Offset: 0x0001F57C
		public override void OnPartyLeaderChangeOfferCanceled(MobileParty party)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPartyLeaderChangeOfferCanceled(party);
			}
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x000213A8 File Offset: 0x0001F5A8
		public override void OnPartyLeaderChanged(MobileParty mobileParty, Hero oldLeader)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPartyLeaderChanged(mobileParty, oldLeader);
			}
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x000213D4 File Offset: 0x0001F5D4
		public override void OnMainPartyStarving()
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnMainPartyStarving();
			}
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00021400 File Offset: 0x0001F600
		public override void OnPlayerJoinedTournament(Town town, bool isParticipant)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPlayerJoinedTournament(town, isParticipant);
			}
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0002142C File Offset: 0x0001F62C
		public override void OnCraftingOrderCompleted(Town town, CraftingOrder craftingOrder, ItemObject craftedItem, Hero completerHero)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnCraftingOrderCompleted(town, craftingOrder, craftedItem, completerHero);
			}
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0002145C File Offset: 0x0001F65C
		public override void OnItemsRefined(Hero hero, Crafting.RefiningFormula refineFormula)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnItemsRefined(hero, refineFormula);
			}
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00021488 File Offset: 0x0001F688
		public override void OnMapEventContinuityNeedsUpdate(IFaction faction)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnMapEventContinuityNeedsUpdate(faction);
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x000214B4 File Offset: 0x0001F6B4
		public override void OnHeirSelectionRequested(Dictionary<Hero, int> heirApparents)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnHeirSelectionRequested(heirApparents);
			}
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x000214E0 File Offset: 0x0001F6E0
		public override void OnHeirSelectionOver(Hero selectedHeir)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnHeirSelectionOver(selectedHeir);
			}
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0002150C File Offset: 0x0001F70C
		public override void OnCharacterCreationInitialized(CharacterCreationManager characterCreationManager)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnCharacterCreationInitialized(characterCreationManager);
			}
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00021538 File Offset: 0x0001F738
		public override void OnShipDestroyed(PartyBase owner, Ship ship, DestroyShipAction.ShipDestroyDetail detail)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnShipDestroyed(owner, ship, detail);
			}
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00021568 File Offset: 0x0001F768
		public override void OnPartyLeftArmy(MobileParty party, Army army)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPartyLeftArmy(party, army);
			}
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00021594 File Offset: 0x0001F794
		public override void OnShipOwnerChanged(Ship ship, PartyBase oldOwner, ChangeShipOwnerAction.ShipOwnerChangeDetail changeDetail)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnShipOwnerChanged(ship, oldOwner, changeDetail);
			}
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x000215C4 File Offset: 0x0001F7C4
		public override void OnShipRepaired(Ship ship, Settlement repairPort)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnShipRepaired(ship, repairPort);
			}
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000215F0 File Offset: 0x0001F7F0
		public override void OnFigureheadUnlocked(Figurehead figurehead)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnFigureheadUnlocked(figurehead);
			}
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0002161C File Offset: 0x0001F81C
		public override void OnPartyAddedToMapEvent(PartyBase party)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnPartyAddedToMapEvent(party);
			}
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00021648 File Offset: 0x0001F848
		public override void OnIncidentResolved(Incident incident)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnIncidentResolved(incident);
			}
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00021674 File Offset: 0x0001F874
		public override void OnMobilePartyNavigationStateChanged(MobileParty mobileParty)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnMobilePartyNavigationStateChanged(mobileParty);
			}
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x000216A0 File Offset: 0x0001F8A0
		public override void OnMobilePartyJoinedToSiegeEvent(MobileParty mobileParty)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnMobilePartyJoinedToSiegeEvent(mobileParty);
			}
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x000216CC File Offset: 0x0001F8CC
		public override void OnMobilePartyLeftSiegeEvent(MobileParty mobileParty)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnMobilePartyLeftSiegeEvent(mobileParty);
			}
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x000216F8 File Offset: 0x0001F8F8
		public override void OnBlockadeActivated(SiegeEvent siegeEvent)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnBlockadeActivated(siegeEvent);
			}
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00021724 File Offset: 0x0001F924
		public override void OnBlockadeDeactivated(SiegeEvent siegeEvent)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnBlockadeDeactivated(siegeEvent);
			}
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00021750 File Offset: 0x0001F950
		public override void OnMapMarkerCreated(MapMarker mapMarker)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnMapMarkerCreated(mapMarker);
			}
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0002177C File Offset: 0x0001F97C
		public override void OnMapMarkerRemoved(MapMarker mapMarker)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnMapMarkerRemoved(mapMarker);
			}
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x000217A8 File Offset: 0x0001F9A8
		public override void OnMercenaryServiceStarted(Clan mercenaryClan, StartMercenaryServiceAction.StartMercenaryServiceActionDetails details)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnMercenaryServiceStarted(mercenaryClan, details);
			}
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x000217D4 File Offset: 0x0001F9D4
		public override void OnMercenaryServiceEnded(Clan mercenaryClan, EndMercenaryServiceAction.EndMercenaryServiceActionDetails details)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnMercenaryServiceEnded(mercenaryClan, details);
			}
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00021800 File Offset: 0x0001FA00
		public override void OnAllianceStarted(Kingdom kingdom1, Kingdom kingdom2)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnAllianceStarted(kingdom1, kingdom2);
			}
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0002182C File Offset: 0x0001FA2C
		public override void OnAllianceEnded(Kingdom kingdom1, Kingdom kingdom2)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnAllianceEnded(kingdom1, kingdom2);
			}
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00021858 File Offset: 0x0001FA58
		public override void OnCallToWarAgreementStarted(Kingdom callingKingdom, Kingdom calledKingdom, Kingdom kingdomToCallToWarAgainst)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnCallToWarAgreementStarted(callingKingdom, calledKingdom, kingdomToCallToWarAgainst);
			}
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00021888 File Offset: 0x0001FA88
		public override void OnCallToWarAgreementEnded(Kingdom callingKingdom, Kingdom calledKingdom, Kingdom kingdomToCallToWarAgainst)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnCallToWarAgreementEnded(callingKingdom, calledKingdom, kingdomToCallToWarAgainst);
			}
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x000218B8 File Offset: 0x0001FAB8
		public override void CanHeroLeadParty(Hero hero, ref bool result)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].CanHeroLeadParty(hero, ref result);
				if (!result)
				{
					return;
				}
			}
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x000218EC File Offset: 0x0001FAEC
		public override void CanHeroMarry(Hero hero, ref bool result)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].CanHeroMarry(hero, ref result);
				if (!result)
				{
					return;
				}
			}
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00021920 File Offset: 0x0001FB20
		public override void CanHeroEquipmentBeChanged(Hero hero, ref bool result)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].CanHeroEquipmentBeChanged(hero, ref result);
				if (!result)
				{
					return;
				}
			}
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00021954 File Offset: 0x0001FB54
		public override void CanBeGovernorOrHavePartyRole(Hero hero, ref bool result)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].CanBeGovernorOrHavePartyRole(hero, ref result);
				if (!result)
				{
					return;
				}
			}
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00021988 File Offset: 0x0001FB88
		public override void CanHeroDie(Hero hero, KillCharacterAction.KillCharacterActionDetail causeOfDeath, ref bool result)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].CanHeroDie(hero, causeOfDeath, ref result);
				if (!result)
				{
					return;
				}
			}
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x000219BC File Offset: 0x0001FBBC
		public override void CanHeroBecomePrisoner(Hero hero, ref bool result)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].CanHeroBecomePrisoner(hero, ref result);
				if (!result)
				{
					return;
				}
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x000219F0 File Offset: 0x0001FBF0
		public override void CanPlayerMeetWithHeroAfterConversation(Hero hero, ref bool result)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].CanPlayerMeetWithHeroAfterConversation(hero, ref result);
				if (!result)
				{
					return;
				}
			}
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00021A24 File Offset: 0x0001FC24
		public override void CanMoveToSettlement(Hero hero, ref bool result)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].CanMoveToSettlement(hero, ref result);
				if (!result)
				{
					return;
				}
			}
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00021A58 File Offset: 0x0001FC58
		public override void CanHaveCampaignIssues(Hero hero, ref bool result)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].CanHaveCampaignIssues(hero, ref result);
				if (!result)
				{
					return;
				}
			}
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00021A8C File Offset: 0x0001FC8C
		public override void IsSettlementBusy(Settlement settlement, object asker, ref int priority)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].IsSettlementBusy(settlement, asker, ref priority);
			}
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00021ABC File Offset: 0x0001FCBC
		public override void OnHeroUnregistered(Hero hero)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnHeroUnregistered(hero);
			}
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00021AE8 File Offset: 0x0001FCE8
		public override void OnShipCreated(Ship ship, Settlement createdSettlement)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnShipCreated(ship, createdSettlement);
			}
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00021B14 File Offset: 0x0001FD14
		public override void OnConfigChanged()
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnConfigChanged();
			}
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00021B40 File Offset: 0x0001FD40
		public override void OnMobilePartyRaftStateChanged(MobileParty mobileParty)
		{
			CampaignEventReceiver[] eventReceivers = this._eventReceivers;
			for (int i = 0; i < eventReceivers.Length; i++)
			{
				eventReceivers[i].OnMobilePartyRaftStateChanged(mobileParty);
			}
		}

		// Token: 0x0400018B RID: 395
		private CampaignEventReceiver[] _eventReceivers;
	}
}
