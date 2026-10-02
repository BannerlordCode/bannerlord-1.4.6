using System;
using System.Collections.Generic;
using Helpers;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Naval;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x02000450 RID: 1104
	public class VillageHostileActionCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x0600471D RID: 18205 RVA: 0x00163ED1 File Offset: 0x001620D1
		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<Dictionary<string, CampaignTime>>("_villageLastHostileActionTimeDictionary", ref this._villageLastHostileActionTimeDictionary);
			dataStore.SyncData<IFaction>("_raiderPartyMapFaction", ref this._raiderPartyMapFaction);
			dataStore.SyncData<Village>("_raidedVillage", ref this._raidedVillage);
		}

		// Token: 0x0600471E RID: 18206 RVA: 0x00163F0C File Offset: 0x0016210C
		public override void RegisterEvents()
		{
			CampaignEvents.OnAfterSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnAfterSessionLaunched));
			CampaignEvents.ItemsLooted.AddNonSerializedListener(this, new Action<MobileParty, ItemRoster>(VillageHostileActionCampaignBehavior.OnItemsLooted));
			CampaignEvents.MapEventEnded.AddNonSerializedListener(this, new Action<MapEvent>(this.OnMapEventEnded));
			CampaignEvents.BeforeGameMenuOpenedEvent.AddNonSerializedListener(this, new Action<MenuCallbackArgs>(VillageHostileActionCampaignBehavior.BeforeGameMenuOpened));
		}

		// Token: 0x0600471F RID: 18207 RVA: 0x00163F78 File Offset: 0x00162178
		private static void BeforeGameMenuOpened(MenuCallbackArgs args)
		{
			if (MBSaveLoad.IsUpdatingGameVersion && MBSaveLoad.LastLoadedGameVersion.IsOlderThan(ApplicationVersion.FromString("v1.3.0", 0)) && args.MenuContext.GameMenu.StringId == "raiding_village" && PlayerEncounter.Current == null)
			{
				GameMenu.ExitToLast();
			}
		}

		// Token: 0x06004720 RID: 18208 RVA: 0x00163FD0 File Offset: 0x001621D0
		private void OnMapEventEnded(MapEvent mapEvent)
		{
			if (mapEvent.IsRaid && mapEvent.IsPlayerMapEvent)
			{
				MobileParty mobileParty = mapEvent.GetMapEventSide(BattleSideEnum.Attacker).LeaderParty.MobileParty;
				this._raidedVillage = mapEvent.GetMapEventSide(BattleSideEnum.Defender).LeaderParty.Settlement.Village;
				this._raiderPartyMapFaction = mobileParty.MapFaction;
				if (!mapEvent.DiplomaticallyFinished)
				{
					if (this._raidedVillage.Settlement.IsRaided && mobileParty.Party == MobileParty.MainParty.Party)
					{
						GameMenu.ActivateGameMenu("village_player_raid_ended");
						return;
					}
					if (MobileParty.MainParty.IsActive && !Hero.MainHero.IsPrisoner && !mapEvent.EndedByRetreat)
					{
						GameMenu.ActivateGameMenu("village_raid_ended_leaded_by_someone_else");
					}
				}
			}
		}

		// Token: 0x06004721 RID: 18209 RVA: 0x0016408E File Offset: 0x0016228E
		private void OnAfterSessionLaunched(CampaignGameStarter campaignGameSystemStarter)
		{
			this.AddGameMenus(campaignGameSystemStarter);
		}

		// Token: 0x06004722 RID: 18210 RVA: 0x00164098 File Offset: 0x00162298
		private void AddGameMenus(CampaignGameStarter campaignGameSystemStarter)
		{
			campaignGameSystemStarter.AddGameMenuOption("village", "hostile_action", "{=GM3tAYMr}Take a hostile action", new GameMenuOption.OnConditionDelegate(VillageHostileActionCampaignBehavior.game_menu_village_hostile_action_on_condition), new GameMenuOption.OnConsequenceDelegate(VillageHostileActionCampaignBehavior.game_menu_village_hostile_action_on_consequence), false, 1, false, null);
			campaignGameSystemStarter.AddGameMenu("village_hostile_action", "{=YVNZaVCA}What action do you have in mind?", new OnInitDelegate(VillageHostileActionCampaignBehavior.game_menu_village_hostile_menu_on_init), GameMenu.MenuOverlayType.SettlementWithBoth, GameMenu.MenuFlags.None, null);
			campaignGameSystemStarter.AddGameMenuOption("village_hostile_action", "raid_village", "{=CTi0ml5F}Raid the village", new GameMenuOption.OnConditionDelegate(this.game_menu_village_hostile_action_raid_village_on_condition), new GameMenuOption.OnConsequenceDelegate(VillageHostileActionCampaignBehavior.game_menu_village_hostile_action_raid_village_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("village_hostile_action", "force_peasants_to_give_volunteers", "{=RL8z99Dt}Force notables to give you recruits", new GameMenuOption.OnConditionDelegate(this.game_menu_village_hostile_action_force_volunteers_condition), new GameMenuOption.OnConsequenceDelegate(VillageHostileActionCampaignBehavior.game_menu_village_hostile_action_force_volunteers_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("village_hostile_action", "force_peasants_to_give_supplies", "{=eAzwpqE1}Force peasants to give you goods", new GameMenuOption.OnConditionDelegate(this.game_menu_village_hostile_action_take_food_on_condition), new GameMenuOption.OnConsequenceDelegate(VillageHostileActionCampaignBehavior.game_menu_village_hostile_action_take_food_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("village_hostile_action", "forget_it", "{=sP9ohQTs}Forget it", new GameMenuOption.OnConditionDelegate(VillageHostileActionCampaignBehavior.hostile_action_common_back_on_condition), new GameMenuOption.OnConsequenceDelegate(VillageHostileActionCampaignBehavior.game_menu_village_hostile_action_forget_it_on_consequence), true, -1, false, null);
			campaignGameSystemStarter.AddWaitGameMenu("raiding_village", "{=hWwr3mrC}You are raiding {VILLAGE_NAME}.", new OnInitDelegate(VillageHostileActionCampaignBehavior.village_raid_game_menu_init), new OnConditionDelegate(VillageHostileActionCampaignBehavior.wait_menu_start_raiding_on_condition), new OnConsequenceDelegate(VillageHostileActionCampaignBehavior.wait_menu_end_raiding_on_consequence), new OnTickDelegate(VillageHostileActionCampaignBehavior.wait_menu_raiding_village_on_tick), GameMenu.MenuAndOptionType.WaitMenuShowOnlyProgressOption, GameMenu.MenuOverlayType.None, 0f, GameMenu.MenuFlags.None, null);
			campaignGameSystemStarter.AddGameMenuOption("raiding_village", "raiding_village_end", "{=M7CcfbIx}End Raiding", new GameMenuOption.OnConditionDelegate(VillageHostileActionCampaignBehavior.wait_menu_end_raiding_on_condition), new GameMenuOption.OnConsequenceDelegate(VillageHostileActionCampaignBehavior.wait_menu_end_raiding_on_consequence), true, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("raiding_village", "leave_army", "{=hSdJ0UUv}Leave Army", new GameMenuOption.OnConditionDelegate(VillageHostileActionCampaignBehavior.wait_menu_end_raiding_at_army_by_leaving_on_condition), new GameMenuOption.OnConsequenceDelegate(VillageHostileActionCampaignBehavior.wait_menu_end_raiding_at_army_by_leaving_on_consequence), true, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("raiding_village", "abandon_army", "{=0vnegjxf}Abandon Army", new GameMenuOption.OnConditionDelegate(VillageHostileActionCampaignBehavior.wait_menu_end_raiding_at_army_by_abandoning_on_condition), new GameMenuOption.OnConsequenceDelegate(VillageHostileActionCampaignBehavior.wait_menu_end_raiding_at_army_by_abandoning_on_consequence), true, -1, false, null);
			campaignGameSystemStarter.AddGameMenu("raid_occupied", "{=!}{RAID_OCCUPPIED_TEXT}", new OnInitDelegate(VillageHostileActionCampaignBehavior.raid_occupied_on_init), GameMenu.MenuOverlayType.None, GameMenu.MenuFlags.None, null);
			campaignGameSystemStarter.AddGameMenuOption("raid_occupied", "raid_occuppied_continue", "{=DM6luo3c}Continue", new GameMenuOption.OnConditionDelegate(VillageHostileActionCampaignBehavior.raid_occupied_on_condition), new GameMenuOption.OnConsequenceDelegate(VillageHostileActionCampaignBehavior.raid_occupied_on_consequence), true, -1, false, null);
			campaignGameSystemStarter.AddGameMenu("raid_village_no_resist_warn_player", "{=!}{RAID_WARN_PLAYER_EXPLANATION}", new OnInitDelegate(VillageHostileActionCampaignBehavior.game_menu_raid_warn_player_on_init), GameMenu.MenuOverlayType.None, GameMenu.MenuFlags.None, null);
			campaignGameSystemStarter.AddGameMenuOption("raid_village_no_resist_warn_player", "raid_village_warn_continue", "{=DM6luo3c}Continue", new GameMenuOption.OnConditionDelegate(VillageHostileActionCampaignBehavior.game_menu_village_hostile_action_raid_village_warn_continue_on_condition), new GameMenuOption.OnConsequenceDelegate(VillageHostileActionCampaignBehavior.game_menu_village_hostile_action_raid_village_warn_continue_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("raid_village_no_resist_warn_player", "raid_village_warn_leave", "{=sP9ohQTs}Forget it", new GameMenuOption.OnConditionDelegate(VillageHostileActionCampaignBehavior.hostile_action_common_back_on_condition), new GameMenuOption.OnConsequenceDelegate(VillageHostileActionCampaignBehavior.game_menu_village_hostile_action_warn_leave_on_consequence), true, -1, false, null);
			campaignGameSystemStarter.AddGameMenu("force_supplies_village", "{=EqFbNha8}The villagers grudgingly bring out what they have for you.", new OnInitDelegate(VillageHostileActionCampaignBehavior.force_supply_game_menu_init), GameMenu.MenuOverlayType.None, GameMenu.MenuFlags.None, null);
			campaignGameSystemStarter.AddGameMenuOption("force_supplies_village", "force_supplies_village_continue", "{=DM6luo3c}Continue", new GameMenuOption.OnConditionDelegate(VillageHostileActionCampaignBehavior.hostile_action_common_continue_on_condition), new GameMenuOption.OnConsequenceDelegate(this.village_force_supplies_ended_successfully_on_consequence), true, -1, false, null);
			campaignGameSystemStarter.AddGameMenu("force_supplies_village_resist_warn_player", "{=!}{FORCE_SUPPLY_WARN_PLAYER_EXPLANATION}", new OnInitDelegate(VillageHostileActionCampaignBehavior.game_menu_force_supply_warn_player_on_init), GameMenu.MenuOverlayType.None, GameMenu.MenuFlags.None, null);
			campaignGameSystemStarter.AddGameMenuOption("force_supplies_village_resist_warn_player", "force_supplies_village_resist_warn_player_continue", "{=DM6luo3c}Continue", new GameMenuOption.OnConditionDelegate(VillageHostileActionCampaignBehavior.game_menu_force_supplies_village_resist_warn_player_continue_on_condition), new GameMenuOption.OnConsequenceDelegate(VillageHostileActionCampaignBehavior.game_menu_force_supplies_village_resist_warn_player_continue_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("force_supplies_village_resist_warn_player", "force_supplies_village_resist_warn_player_leave", "{=sP9ohQTs}Forget it", new GameMenuOption.OnConditionDelegate(VillageHostileActionCampaignBehavior.hostile_action_common_back_on_condition), new GameMenuOption.OnConsequenceDelegate(VillageHostileActionCampaignBehavior.game_menu_village_hostile_action_warn_leave_on_consequence), true, -1, false, null);
			campaignGameSystemStarter.AddGameMenu("force_troops_village_resist_warn_player", "{=!}{FORCE_TROOP_WARN_PLAYER_EXPLANATION}", new OnInitDelegate(VillageHostileActionCampaignBehavior.game_menu_force_troop_warn_player_on_init), GameMenu.MenuOverlayType.None, GameMenu.MenuFlags.None, null);
			campaignGameSystemStarter.AddGameMenuOption("force_troops_village_resist_warn_player", "force_supplies_village_resist_warn_player_continue", "{=DM6luo3c}Continue", new GameMenuOption.OnConditionDelegate(VillageHostileActionCampaignBehavior.game_menu_force_troops_village_resist_warn_player_continue_on_condition), new GameMenuOption.OnConsequenceDelegate(VillageHostileActionCampaignBehavior.game_menu_force_troops_village_resist_warn_player_continue_on_consequence), false, -1, false, null);
			campaignGameSystemStarter.AddGameMenuOption("force_troops_village_resist_warn_player", "force_supplies_village_resist_warn_player_leave", "{=sP9ohQTs}Forget it", new GameMenuOption.OnConditionDelegate(VillageHostileActionCampaignBehavior.hostile_action_common_back_on_condition), new GameMenuOption.OnConsequenceDelegate(VillageHostileActionCampaignBehavior.game_menu_village_hostile_action_warn_leave_on_consequence), true, -1, false, null);
			campaignGameSystemStarter.AddGameMenu("force_volunteers_village", "{=BqkD4YWr}You manage to round up some men from the village who look like they might make decent recruits.", new OnInitDelegate(VillageHostileActionCampaignBehavior.force_troop_game_menu_init), GameMenu.MenuOverlayType.None, GameMenu.MenuFlags.None, null);
			campaignGameSystemStarter.AddGameMenuOption("force_volunteers_village", "force_supplies_village_continue", "{=DM6luo3c}Continue", new GameMenuOption.OnConditionDelegate(VillageHostileActionCampaignBehavior.hostile_action_common_continue_on_condition), new GameMenuOption.OnConsequenceDelegate(this.village_force_volunteers_ended_successfully_on_consequence), true, -1, false, null);
			campaignGameSystemStarter.AddGameMenu("village_looted", "{=NxcXfUxu}The village has been looted. A handful of souls scatter as you pass through the burnt-out houses.", new OnInitDelegate(VillageHostileActionCampaignBehavior.village_looted_init), GameMenu.MenuOverlayType.None, GameMenu.MenuFlags.None, null);
			campaignGameSystemStarter.AddGameMenuOption("village_looted", "leave", "{=2YYRyrOO}Leave...", new GameMenuOption.OnConditionDelegate(VillageHostileActionCampaignBehavior.hostile_action_common_back_on_condition), new GameMenuOption.OnConsequenceDelegate(VillageHostileActionCampaignBehavior.village_looted_leave_on_consequence), true, -1, false, null);
			campaignGameSystemStarter.AddGameMenu("village_player_raid_ended", "{=m1rzHfxI}{VILLAGE_ENCOUNTER_RESULT}", new OnInitDelegate(VillageHostileActionCampaignBehavior.village_player_raid_ended_on_init), GameMenu.MenuOverlayType.None, GameMenu.MenuFlags.None, null);
			campaignGameSystemStarter.AddGameMenuOption("village_player_raid_ended", "continue", "{=DM6luo3c}Continue", new GameMenuOption.OnConditionDelegate(VillageHostileActionCampaignBehavior.hostile_action_common_continue_on_condition), new GameMenuOption.OnConsequenceDelegate(VillageHostileActionCampaignBehavior.village_player_raid_ended_on_consequence), true, -1, false, null);
			campaignGameSystemStarter.AddGameMenu("village_raid_ended_leaded_by_someone_else", "{=m1rzHfxI}{VILLAGE_ENCOUNTER_RESULT}", new OnInitDelegate(this.village_raid_ended_leaded_by_someone_else_on_init), GameMenu.MenuOverlayType.None, GameMenu.MenuFlags.None, null);
			campaignGameSystemStarter.AddGameMenuOption("village_raid_ended_leaded_by_someone_else", "continue", "{=DM6luo3c}Continue", new GameMenuOption.OnConditionDelegate(VillageHostileActionCampaignBehavior.hostile_action_common_continue_on_condition), new GameMenuOption.OnConsequenceDelegate(VillageHostileActionCampaignBehavior.village_raid_ended_leaded_by_someone_else_on_consequence), true, -1, false, null);
		}

		// Token: 0x06004723 RID: 18211 RVA: 0x001645F8 File Offset: 0x001627F8
		private static void raid_occupied_on_init(MenuCallbackArgs args)
		{
			string encounterCultureBackgroundMesh = MenuHelper.GetEncounterCultureBackgroundMesh(PlayerEncounter.EncounteredParty.MapFaction.Culture);
			args.MenuContext.SetBackgroundMeshName(encounterCultureBackgroundMesh);
			TextObject textObject = new TextObject("{=kkdkQYVN}This village is being raided by {HERO.NAME}.", null);
			MobileParty mobileParty = null;
			if (PlayerEncounter.Current != null && PlayerEncounter.Current.EncounterSettlementAux != null && PlayerEncounter.Current.EncounterSettlementAux.LastAttackerParty != null)
			{
				mobileParty = PlayerEncounter.Current.EncounterSettlementAux.LastAttackerParty;
			}
			else if (PlayerEncounter.EncounteredBattle != null && PlayerEncounter.EncounteredBattle.IsRaid)
			{
				mobileParty = PlayerEncounter.EncounteredBattle.AttackerSide.LeaderParty.MobileParty;
			}
			if (mobileParty == null)
			{
				mobileParty = PlayerEncounter.EncounteredParty.MobileParty;
			}
			if (mobileParty != null && mobileParty.LeaderHero != null)
			{
				textObject.SetCharacterProperties("HERO", mobileParty.LeaderHero.CharacterObject, false);
			}
			else
			{
				textObject.SetTextVariable("HERO", new TextObject("{=C0BEkIZq}hostile forces", null));
			}
			MBTextManager.SetTextVariable("RAID_OCCUPPIED_TEXT", textObject, false);
		}

		// Token: 0x06004724 RID: 18212 RVA: 0x001646E7 File Offset: 0x001628E7
		private static bool raid_occupied_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Leave;
			return true;
		}

		// Token: 0x06004725 RID: 18213 RVA: 0x001646F2 File Offset: 0x001628F2
		private static void raid_occupied_on_consequence(MenuCallbackArgs args)
		{
			PlayerEncounter.Finish(true);
			MobileParty.MainParty.SetMoveModeHold();
		}

		// Token: 0x06004726 RID: 18214 RVA: 0x00164704 File Offset: 0x00162904
		private static bool wait_menu_end_raiding_at_army_by_leaving_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Leave;
			return MobileParty.MainParty.Army != null && MobileParty.MainParty.Army.LeaderParty != MobileParty.MainParty && MobileParty.MainParty.MapEvent == null;
		}

		// Token: 0x06004727 RID: 18215 RVA: 0x00164740 File Offset: 0x00162940
		private static void game_menu_village_hostile_menu_on_init(MenuCallbackArgs args)
		{
			PlayerEncounter.LeaveEncounter = false;
			if (Campaign.Current.GameMenuManager.NextLocation != null)
			{
				PlayerEncounter.LocationEncounter.CreateAndOpenMissionController(Campaign.Current.GameMenuManager.NextLocation, Campaign.Current.GameMenuManager.PreviousLocation, null, null);
				Campaign.Current.GameMenuManager.NextLocation = null;
				Campaign.Current.GameMenuManager.PreviousLocation = null;
				return;
			}
			if (Settlement.CurrentSettlement.SettlementHitPoints <= 0f)
			{
				Debug.FailedAssert("This case should not be possible, check here", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\CampaignBehaviors\\VillageHostileActionCampaignBehavior.cs", "game_menu_village_hostile_menu_on_init", 236);
			}
		}

		// Token: 0x06004728 RID: 18216 RVA: 0x001647DC File Offset: 0x001629DC
		private static bool game_menu_village_hostile_action_on_condition(MenuCallbackArgs args)
		{
			Village village = Settlement.CurrentSettlement.Village;
			if (MobileParty.MainParty.IsCurrentlyAtSea)
			{
				int minimumNumberOfMenForAttackingVillageViaScene = Campaign.Current.Models.EncounterModel.MinimumNumberOfMenForAttackingVillageViaScene;
				if (MobileParty.MainParty.MemberRoster.TotalHealthyCount < minimumNumberOfMenForAttackingVillageViaScene)
				{
					args.IsEnabled = false;
					args.Tooltip = new TextObject("{=7b4WZVyU}You should at least have {NUMBER} healthy men in your party to take a hostile action.", null);
					args.Tooltip.SetTextVariable("NUMBER", minimumNumberOfMenForAttackingVillageViaScene);
				}
				else if (!ShipHelper.GetOrderedNavalRaidShipsOfPlayerParty().AnyQ<Ship>())
				{
					args.IsEnabled = false;
					args.Tooltip = new TextObject("{=hd626n2z}You don't have any shallow draft ship.", null);
				}
				else if (Math.Min(MobileParty.MainParty.MemberRoster.TotalHealthyCount, ShipHelper.GetOrderedNavalRaidShipsOfPlayerParty().SumQ<Ship>((Ship x) => x.MainDeckCrewCapacity)) < minimumNumberOfMenForAttackingVillageViaScene)
				{
					args.IsEnabled = false;
					args.Tooltip = new TextObject("{=*}Your shallow ship's crew capacity is too low for a hostile action. A minimum of {NUMBER} crew is required. Use a larger or additional vessel.", null);
					args.Tooltip.SetTextVariable("NUMBER", minimumNumberOfMenForAttackingVillageViaScene);
				}
			}
			args.optionLeaveType = GameMenuOption.LeaveType.Submenu;
			return (MobileParty.MainParty.Army == null || MobileParty.MainParty.Army.LeaderParty == MobileParty.MainParty) && village != null && Hero.MainHero.MapFaction != village.Owner.MapFaction && village.VillageState == Village.VillageStates.Normal;
		}

		// Token: 0x06004729 RID: 18217 RVA: 0x00164939 File Offset: 0x00162B39
		private bool game_menu_village_hostile_action_raid_village_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Submenu;
			this.CheckVillageAttackableHonorably(args);
			return !DiplomacyHelper.IsSameFactionAndNotEliminated(Hero.MainHero.MapFaction, Settlement.CurrentSettlement.MapFaction);
		}

		// Token: 0x0600472A RID: 18218 RVA: 0x00164965 File Offset: 0x00162B65
		private static void game_menu_village_hostile_action_raid_village_on_consequence(MenuCallbackArgs args)
		{
			if (!FactionManager.IsAtWarAgainstFaction(Hero.MainHero.MapFaction, Settlement.CurrentSettlement.MapFaction))
			{
				GameMenu.SwitchToMenu("raid_village_no_resist_warn_player");
				return;
			}
			VillageHostileActionCampaignBehavior.StartHostileAction(VillageHostileActionCampaignBehavior.HostileActionType.Raid);
		}

		// Token: 0x0600472B RID: 18219 RVA: 0x00164993 File Offset: 0x00162B93
		private static void game_menu_village_hostile_action_force_volunteers_on_consequence(MenuCallbackArgs args)
		{
			if (!FactionManager.IsAtWarAgainstFaction(Clan.PlayerClan.MapFaction, Settlement.CurrentSettlement.MapFaction))
			{
				GameMenu.SwitchToMenu("force_troops_village_resist_warn_player");
				return;
			}
			VillageHostileActionCampaignBehavior.StartHostileAction(VillageHostileActionCampaignBehavior.HostileActionType.ForceTroop);
		}

		// Token: 0x0600472C RID: 18220 RVA: 0x001649C4 File Offset: 0x00162BC4
		private bool game_menu_village_hostile_action_force_volunteers_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.ForceToGiveTroops;
			this.CheckVillageAttackableHonorably(args);
			CampaignTime campaignTime;
			if (this._villageLastHostileActionTimeDictionary.TryGetValue(Settlement.CurrentSettlement.StringId, out campaignTime) && campaignTime.ElapsedDaysUntilNow <= 10f)
			{
				args.IsEnabled = false;
				args.Tooltip = new TextObject("{=q5OOjkXe}You have already taken hostile action against this village recently.", null);
			}
			else if (this._villageLastHostileActionTimeDictionary.ContainsKey(Settlement.CurrentSettlement.StringId))
			{
				this._villageLastHostileActionTimeDictionary.Remove(Settlement.CurrentSettlement.StringId);
			}
			else if (Settlement.CurrentSettlement.Village.Hearth <= 0f)
			{
				args.IsEnabled = false;
				args.Tooltip = new TextObject("{=wRo6hOka}The notables don't have any troops to give.", null);
			}
			return true;
		}

		// Token: 0x0600472D RID: 18221 RVA: 0x00164A86 File Offset: 0x00162C86
		private static void game_menu_village_hostile_action_forget_it_on_consequence(MenuCallbackArgs args)
		{
			GameMenu.SwitchToMenu("village");
		}

		// Token: 0x0600472E RID: 18222 RVA: 0x00164A92 File Offset: 0x00162C92
		private static void game_menu_village_hostile_action_take_food_on_consequence(MenuCallbackArgs args)
		{
			if (!FactionManager.IsAtWarAgainstFaction(Clan.PlayerClan.MapFaction, Settlement.CurrentSettlement.MapFaction))
			{
				GameMenu.SwitchToMenu("force_supplies_village_resist_warn_player");
				return;
			}
			VillageHostileActionCampaignBehavior.StartHostileAction(VillageHostileActionCampaignBehavior.HostileActionType.ForceSupply);
		}

		// Token: 0x0600472F RID: 18223 RVA: 0x00164AC0 File Offset: 0x00162CC0
		private bool game_menu_village_hostile_action_take_food_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.ForceToGiveGoods;
			this.CheckVillageAttackableHonorably(args);
			if (this._villageLastHostileActionTimeDictionary.ContainsKey(Settlement.CurrentSettlement.StringId))
			{
				if (this._villageLastHostileActionTimeDictionary[Settlement.CurrentSettlement.StringId].ElapsedDaysUntilNow <= 10f)
				{
					args.IsEnabled = false;
					args.Tooltip = new TextObject("{=q5OOjkXe}You have already taken hostile action against this village recently.", null);
				}
				else
				{
					this._villageLastHostileActionTimeDictionary.Remove(Settlement.CurrentSettlement.StringId);
				}
			}
			return true;
		}

		// Token: 0x06004730 RID: 18224 RVA: 0x00164B47 File Offset: 0x00162D47
		private static void game_menu_village_hostile_action_on_consequence(MenuCallbackArgs args)
		{
			GameMenu.SwitchToMenu("village_hostile_action");
		}

		// Token: 0x06004731 RID: 18225 RVA: 0x00164B53 File Offset: 0x00162D53
		private static bool game_menu_village_hostile_action_raid_village_warn_continue_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Raid;
			return true;
		}

		// Token: 0x06004732 RID: 18226 RVA: 0x00164B5E File Offset: 0x00162D5E
		private static bool game_menu_force_supplies_village_resist_warn_player_continue_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.ForceToGiveGoods;
			return true;
		}

		// Token: 0x06004733 RID: 18227 RVA: 0x00164B68 File Offset: 0x00162D68
		private static void game_menu_force_supplies_village_resist_warn_player_continue_on_consequence(MenuCallbackArgs args)
		{
			VillageHostileActionCampaignBehavior.StartHostileAction(VillageHostileActionCampaignBehavior.HostileActionType.ForceSupply);
		}

		// Token: 0x06004734 RID: 18228 RVA: 0x00164B70 File Offset: 0x00162D70
		private static bool game_menu_force_troops_village_resist_warn_player_continue_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.ForceToGiveTroops;
			return true;
		}

		// Token: 0x06004735 RID: 18229 RVA: 0x00164B7A File Offset: 0x00162D7A
		private static void village_raid_game_menu_init(MenuCallbackArgs args)
		{
			if (PlayerEncounter.EncounterSettlement != null)
			{
				MBTextManager.SetTextVariable("VILLAGE_NAME", PlayerEncounter.EncounterSettlement.Name, false);
				VillageHostileActionCampaignBehavior.UpdateWaitMenuProgress(args);
				return;
			}
			Debug.FailedAssert("Party is in raid but mapevent is empty!", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\CampaignBehaviors\\VillageHostileActionCampaignBehavior.cs", "village_raid_game_menu_init", 414);
		}

		// Token: 0x06004736 RID: 18230 RVA: 0x00164BB8 File Offset: 0x00162DB8
		private static void game_menu_force_troops_village_resist_warn_player_continue_on_consequence(MenuCallbackArgs args)
		{
			VillageHostileActionCampaignBehavior.StartHostileAction(VillageHostileActionCampaignBehavior.HostileActionType.ForceTroop);
		}

		// Token: 0x06004737 RID: 18231 RVA: 0x00164BC0 File Offset: 0x00162DC0
		private static bool wait_menu_start_raiding_on_condition(MenuCallbackArgs args)
		{
			MapEvent battle = PlayerEncounter.Battle;
			if (((battle != null) ? battle.MapEventSettlement : null) != null)
			{
				MBTextManager.SetTextVariable("SETTLEMENT_NAME", PlayerEncounter.Battle.MapEventSettlement.Name, false);
				return true;
			}
			Debug.FailedAssert("Party is in raid but mapevent is empty!", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\CampaignBehaviors\\VillageHostileActionCampaignBehavior.cs", "wait_menu_start_raiding_on_condition", 431);
			return false;
		}

		// Token: 0x06004738 RID: 18232 RVA: 0x00164C18 File Offset: 0x00162E18
		private static void game_menu_raid_warn_player_on_init(MenuCallbackArgs args)
		{
			VillageHostileActionCampaignBehavior.SetHostileActionWarnPlayerInitBackground(args);
			TextObject textObject = new TextObject("{=Hhq7nq9U}Villagers gathering around to defend their land. {DETAILED_HOSTILE_EXPLANATION}", null);
			textObject.SetTextVariable("DETAILED_HOSTILE_EXPLANATION", VillageHostileActionCampaignBehavior.GetHostileActionGenericWarnExplanation());
			MBTextManager.SetTextVariable("RAID_WARN_PLAYER_EXPLANATION", textObject, false);
		}

		// Token: 0x06004739 RID: 18233 RVA: 0x00164C54 File Offset: 0x00162E54
		private static void wait_menu_end_raiding_on_consequence(MenuCallbackArgs args)
		{
			PlayerEncounter.Current.ForceRaid = false;
			PlayerEncounter.Finish(true);
		}

		// Token: 0x0600473A RID: 18234 RVA: 0x00164C67 File Offset: 0x00162E67
		private static void village_player_raid_ended_on_consequence(MenuCallbackArgs args)
		{
			GameMenu.ExitToLast();
		}

		// Token: 0x0600473B RID: 18235 RVA: 0x00164C6E File Offset: 0x00162E6E
		private static void wait_menu_end_raiding_at_army_by_leaving_on_consequence(MenuCallbackArgs args)
		{
			PlayerEncounter.Current.ForceRaid = false;
			PlayerEncounter.Finish(true);
			MobileParty.MainParty.Army = null;
		}

		// Token: 0x0600473C RID: 18236 RVA: 0x00164C8C File Offset: 0x00162E8C
		private void village_raid_ended_leaded_by_someone_else_on_init(MenuCallbackArgs args)
		{
			if (this._raidedVillage == null)
			{
				VillageStateChangedLogEntry villageStateChangedLogEntry = Campaign.Current.LogEntryHistory.FindLastGameActionLog<VillageStateChangedLogEntry>((VillageStateChangedLogEntry entry) => entry.Village.Settlement == MobileParty.MainParty.LastVisitedSettlement);
				if (villageStateChangedLogEntry != null)
				{
					this._raidedVillage = villageStateChangedLogEntry.Village;
					this._raiderPartyMapFaction = villageStateChangedLogEntry.RaiderPartyMapFaction;
				}
			}
			if (this._raidedVillage != null)
			{
				if (MobileParty.MainParty.MapEvent != null && MobileParty.MainParty.Army == null && MobileParty.MainParty.MapEvent.AttackerSide.LeaderParty != PartyBase.MainParty && MobileParty.MainParty.MapEvent.DefenderSide.LeaderParty != PartyBase.MainParty)
				{
					MBTextManager.SetTextVariable("VILLAGE_ENCOUNTER_RESULT", new TextObject("{=3OW1QQNx}The raid was ended by the battle outside of the village.", null), false);
					return;
				}
				if (!this._raidedVillage.Settlement.SettlementHitPoints.ApproximatelyEqualsTo(0f, 1E-05f) && this._raiderPartyMapFaction != null && !this._raiderPartyMapFaction.IsAtWarWith(Hero.MainHero.MapFaction))
				{
					if (MobileParty.MainParty.Army == null || MobileParty.MainParty.Army.LeaderParty == MobileParty.MainParty)
					{
						MBTextManager.SetTextVariable("VILLAGE_ENCOUNTER_RESULT", new TextObject("{=ZJOikvf4}You called off your raid on the village.", null), false);
						return;
					}
					MBTextManager.SetTextVariable("VILLAGE_ENCOUNTER_RESULT", new TextObject("{=VYKc665f}The army leader called off the raid on the village.", null), false);
					return;
				}
				else if (MobileParty.MainParty.Army == null && this._raiderPartyMapFaction != null)
				{
					if (!this._raiderPartyMapFaction.IsAtWarWith(Hero.MainHero.MapFaction))
					{
						MBTextManager.SetTextVariable("VILLAGE_ENCOUNTER_RESULT", new TextObject("{=MEuuuOiF}The village was successfully raided with your help.", null), false);
						return;
					}
					MBTextManager.SetTextVariable("VILLAGE_ENCOUNTER_RESULT", new TextObject("{=sHy7VHbw}The village was successfully saved with your help.", null), false);
					return;
				}
				else if (MobileParty.MainParty.Army != null && this._raidedVillage.Settlement.MapFaction != null)
				{
					if (this._raidedVillage.Settlement.MapFaction.IsAtWarWith(Hero.MainHero.MapFaction))
					{
						if (MobileParty.MainParty.Army.LeaderParty == MobileParty.MainParty)
						{
							MBTextManager.SetTextVariable("VILLAGE_ENCOUNTER_RESULT", new TextObject("{=jaiwriZc}The village was successfully raided by the army you are leading.", null), false);
							return;
						}
						MBTextManager.SetTextVariable("VILLAGE_ENCOUNTER_RESULT", new TextObject("{=zzRJ7jqR}The village was successfully raided by the army you are following.", null), false);
						return;
					}
					else
					{
						if (MobileParty.MainParty.Army.LeaderParty == MobileParty.MainParty)
						{
							MBTextManager.SetTextVariable("VILLAGE_ENCOUNTER_RESULT", new TextObject("{=XzDDwHbc}The village is saved by the army you are leading.", null), false);
							return;
						}
						MBTextManager.SetTextVariable("VILLAGE_ENCOUNTER_RESULT", new TextObject("{=ibiQdZLf}The village is saved by the army you are following.", null), false);
						return;
					}
				}
			}
			else
			{
				MBTextManager.SetTextVariable("VILLAGE_ENCOUNTER_RESULT", new TextObject("{=HkcYydHe}The raid has ended.", null), false);
			}
		}

		// Token: 0x0600473D RID: 18237 RVA: 0x00164F34 File Offset: 0x00163134
		private static bool wait_menu_end_raiding_at_army_by_abandoning_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Leave;
			if (MobileParty.MainParty.Army == null || MobileParty.MainParty.Army.LeaderParty == MobileParty.MainParty || MobileParty.MainParty.MapEvent == null)
			{
				return false;
			}
			args.Tooltip = GameTexts.FindText("str_abandon_army", null);
			args.Tooltip.SetTextVariable("INFLUENCE_COST", Campaign.Current.Models.DiplomacyModel.GetInfluenceCostOfAbandoningArmy());
			return true;
		}

		// Token: 0x0600473E RID: 18238 RVA: 0x00164FB8 File Offset: 0x001631B8
		private static void wait_menu_end_raiding_at_army_by_abandoning_on_consequence(MenuCallbackArgs args)
		{
			Clan.PlayerClan.Influence -= (float)Campaign.Current.Models.DiplomacyModel.GetInfluenceCostOfAbandoningArmy();
			PlayerEncounter.Current.ForceRaid = false;
			PlayerEncounter.Finish(true);
			MobileParty.MainParty.Army = null;
		}

		// Token: 0x0600473F RID: 18239 RVA: 0x00165007 File Offset: 0x00163207
		private static bool wait_menu_end_raiding_on_condition(MenuCallbackArgs args)
		{
			if (MobileParty.MainParty.Army == null || MobileParty.MainParty.Army.LeaderParty == MobileParty.MainParty)
			{
				args.optionLeaveType = GameMenuOption.LeaveType.Leave;
				return true;
			}
			return false;
		}

		// Token: 0x06004740 RID: 18240 RVA: 0x00165036 File Offset: 0x00163236
		private static void wait_menu_raiding_village_on_tick(MenuCallbackArgs args, CampaignTime dt)
		{
			MapEvent battle = PlayerEncounter.Battle;
			if (((battle != null) ? battle.MapEventSettlement : null) != null)
			{
				VillageHostileActionCampaignBehavior.UpdateWaitMenuProgress(args);
				return;
			}
			Debug.FailedAssert("Party is in raid but mapEvent is empty!", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\CampaignBehaviors\\VillageHostileActionCampaignBehavior.cs", "wait_menu_raiding_village_on_tick", 595);
		}

		// Token: 0x06004741 RID: 18241 RVA: 0x0016506B File Offset: 0x0016326B
		private static void force_supply_game_menu_init(MenuCallbackArgs args)
		{
		}

		// Token: 0x06004742 RID: 18242 RVA: 0x0016506D File Offset: 0x0016326D
		private static void village_raid_ended_leaded_by_someone_else_on_consequence(MenuCallbackArgs args)
		{
			if (MobileParty.MainParty.Army != null && MobileParty.MainParty.Army.LeaderParty != MobileParty.MainParty)
			{
				GameMenu.SwitchToMenu("army_wait");
				return;
			}
			GameMenu.ExitToLast();
		}

		// Token: 0x06004743 RID: 18243 RVA: 0x001650A1 File Offset: 0x001632A1
		private static void SetHostileActionWarnPlayerInitBackground(MenuCallbackArgs args)
		{
			args.MenuContext.SetBackgroundMeshName(Settlement.CurrentSettlement.SettlementComponent.WaitMeshName);
		}

		// Token: 0x06004744 RID: 18244 RVA: 0x001650C0 File Offset: 0x001632C0
		private static void game_menu_force_supply_warn_player_on_init(MenuCallbackArgs args)
		{
			VillageHostileActionCampaignBehavior.SetHostileActionWarnPlayerInitBackground(args);
			TextObject textObject = new TextObject("{=EBQ8qOYA}The villagers seem ready to resist the seizure of their goods.{DETAILED_HOSTILE_EXPLANATION}", null);
			textObject.SetTextVariable("DETAILED_HOSTILE_EXPLANATION", VillageHostileActionCampaignBehavior.GetHostileActionGenericWarnExplanation());
			MBTextManager.SetTextVariable("FORCE_SUPPLY_WARN_PLAYER_EXPLANATION", textObject, false);
		}

		// Token: 0x06004745 RID: 18245 RVA: 0x001650FC File Offset: 0x001632FC
		private static void game_menu_village_hostile_action_raid_village_warn_continue_on_consequence(MenuCallbackArgs args)
		{
			VillageHostileActionCampaignBehavior.StartHostileAction(VillageHostileActionCampaignBehavior.HostileActionType.Raid);
		}

		// Token: 0x06004746 RID: 18246 RVA: 0x00165104 File Offset: 0x00163304
		private void village_force_supplies_ended_successfully_on_consequence(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Leave;
			GameMenu.SwitchToMenu("village");
			ItemRoster itemRoster = new ItemRoster();
			int num = MathF.Max((int)(Settlement.CurrentSettlement.Village.Hearth * 0.15f), 20);
			GiveGoldAction.ApplyBetweenCharacters(null, Hero.MainHero, num * Campaign.Current.Models.RaidModel.GoldRewardForEachLostHearth, false);
			for (int i = 0; i < Settlement.CurrentSettlement.Village.VillageType.Productions.Count; i++)
			{
				ValueTuple<ItemObject, float> valueTuple = Settlement.CurrentSettlement.Village.VillageType.Productions[i];
				ItemObject item = valueTuple.Item1;
				int num2 = (int)(valueTuple.Item2 / 60f * (float)num);
				if (num2 > 0)
				{
					itemRoster.AddToCounts(item, num2);
				}
			}
			if (!this._villageLastHostileActionTimeDictionary.ContainsKey(Settlement.CurrentSettlement.StringId))
			{
				this._villageLastHostileActionTimeDictionary.Add(Settlement.CurrentSettlement.StringId, CampaignTime.Now);
			}
			else
			{
				this._villageLastHostileActionTimeDictionary[Settlement.CurrentSettlement.StringId] = CampaignTime.Now;
			}
			Settlement.CurrentSettlement.SettlementHitPoints -= Settlement.CurrentSettlement.SettlementHitPoints * 0.8f;
			InventoryScreenHelper.OpenScreenAsLoot(new Dictionary<PartyBase, ItemRoster> { 
			{
				PartyBase.MainParty,
				itemRoster
			} });
			bool flag = MapEvent.PlayerMapEvent == null;
			SkillLevelingManager.OnForceSupplies(MobileParty.MainParty, itemRoster, flag);
			PlayerEncounter.Current.ForceSupplies = false;
			PlayerEncounter.Current.FinalizeBattle();
		}

		// Token: 0x06004747 RID: 18247 RVA: 0x0016527E File Offset: 0x0016347E
		private static void force_troop_game_menu_init(MenuCallbackArgs args)
		{
		}

		// Token: 0x06004748 RID: 18248 RVA: 0x00165280 File Offset: 0x00163480
		private static void game_menu_force_troop_warn_player_on_init(MenuCallbackArgs args)
		{
			VillageHostileActionCampaignBehavior.SetHostileActionWarnPlayerInitBackground(args);
			TextObject textObject = new TextObject("{=BsEeUfbk}The village elder balks at your demand. He says the villagers might resist.{DETAILED_HOSTILE_EXPLANATION}", null);
			textObject.SetTextVariable("DETAILED_HOSTILE_EXPLANATION", VillageHostileActionCampaignBehavior.GetHostileActionGenericWarnExplanation());
			MBTextManager.SetTextVariable("FORCE_TROOP_WARN_PLAYER_EXPLANATION", textObject, false);
		}

		// Token: 0x06004749 RID: 18249 RVA: 0x001652BC File Offset: 0x001634BC
		private void village_force_volunteers_ended_successfully_on_consequence(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Leave;
			GameMenu.SwitchToMenu("village");
			TroopRoster troopRoster = TroopRoster.CreateDummyTroopRoster();
			int num = (int)Math.Ceiling((double)(Settlement.CurrentSettlement.Village.Hearth / 30f));
			if (MobileParty.MainParty.HasPerk(DefaultPerks.Roguery.InBestLight, false))
			{
				num += Settlement.CurrentSettlement.Notables.Count;
			}
			troopRoster.AddToCounts(Settlement.CurrentSettlement.Culture.BasicTroop, num, false, 0, 0, true, -1);
			if (!this._villageLastHostileActionTimeDictionary.ContainsKey(Settlement.CurrentSettlement.StringId))
			{
				this._villageLastHostileActionTimeDictionary.Add(Settlement.CurrentSettlement.StringId, CampaignTime.Now);
			}
			else
			{
				this._villageLastHostileActionTimeDictionary[Settlement.CurrentSettlement.StringId] = CampaignTime.Now;
			}
			Settlement.CurrentSettlement.SettlementHitPoints -= Settlement.CurrentSettlement.SettlementHitPoints * 0.8f;
			Settlement.CurrentSettlement.Village.Hearth -= (float)(num / 2);
			PartyScreenHelper.OpenScreenAsLoot(troopRoster, TroopRoster.CreateDummyTroopRoster(), MobileParty.MainParty.CurrentSettlement.Name, troopRoster.TotalManCount, null);
			PlayerEncounter.Current.ForceVolunteers = false;
			SkillLevelingManager.OnForceVolunteers(MobileParty.MainParty, Settlement.CurrentSettlement.Party);
			PlayerEncounter.Current.FinalizeBattle();
		}

		// Token: 0x0600474A RID: 18250 RVA: 0x00165410 File Offset: 0x00163610
		private static void game_menu_village_hostile_action_warn_leave_on_consequence(MenuCallbackArgs args)
		{
			GameMenu.SwitchToMenu("village_hostile_action");
			PlayerEncounter.Finish(true);
		}

		// Token: 0x0600474B RID: 18251 RVA: 0x00165422 File Offset: 0x00163622
		private static void village_looted_init(MenuCallbackArgs args)
		{
		}

		// Token: 0x0600474C RID: 18252 RVA: 0x00165424 File Offset: 0x00163624
		private static void UpdateWaitMenuProgress(MenuCallbackArgs args)
		{
			args.MenuContext.GameMenu.SetProgressOfWaitingInMenu(1f - PlayerEncounter.Battle.MapEventSettlement.SettlementHitPoints);
		}

		// Token: 0x0600474D RID: 18253 RVA: 0x0016544B File Offset: 0x0016364B
		private static void village_looted_leave_on_consequence(MenuCallbackArgs args)
		{
			PlayerEncounter.LeaveSettlement();
			PlayerEncounter.Finish(true);
			MobileParty.MainParty.SetMoveModeHold();
			Campaign.Current.SaveHandler.SignalAutoSave();
		}

		// Token: 0x0600474E RID: 18254 RVA: 0x00165474 File Offset: 0x00163674
		private static void village_player_raid_ended_on_init(MenuCallbackArgs args)
		{
			if (MobileParty.MainParty.LastVisitedSettlement == null)
			{
				MBTextManager.SetTextVariable("VILLAGE_ENCOUNTER_RESULT", new TextObject("{=HkcYydHe}The raid has ended.", null), false);
				return;
			}
			if (!MobileParty.MainParty.LastVisitedSettlement.MapFaction.IsAtWarWith(Hero.MainHero.MapFaction))
			{
				MBTextManager.SetTextVariable("VILLAGE_ENCOUNTER_RESULT", "{=aih1Y62W}You have saved the village.", false);
				return;
			}
			if (!MobileParty.MainParty.LastVisitedSettlement.SettlementHitPoints.ApproximatelyEqualsTo(0f, 1E-05f))
			{
				MBTextManager.SetTextVariable("VILLAGE_ENCOUNTER_RESULT", new TextObject("{=ZJOikvf4}You called off your raid on the village.", null), false);
				return;
			}
			MBTextManager.SetTextVariable("VILLAGE_ENCOUNTER_RESULT", "{=6snepBi5}You have successfully raided the village.", false);
		}

		// Token: 0x0600474F RID: 18255 RVA: 0x0016551C File Offset: 0x0016371C
		private static TextObject GetHostileActionGenericWarnExplanation()
		{
			Settlement currentSettlement = Settlement.CurrentSettlement;
			TextObject textObject;
			if (Clan.PlayerClan.IsUnderMercenaryService)
			{
				textObject = new TextObject("{=d6KbdIWg}As a result of your hostile intent towards a neutral village, the {MERCENARY_KINGDOM} ends its contract with you, and the {KINGDOM} declares war on you.", null);
				textObject.SetTextVariable("MERCENARY_KINGDOM", Clan.PlayerClan.Kingdom.EncyclopediaTitle);
			}
			else
			{
				textObject = new TextObject("{=bjEN2OzZ}As a result of your hostile intent towards a neutral village, the {KINGDOM} declares war on you.", null);
			}
			textObject.SetTextVariable("KINGDOM", currentSettlement.MapFaction.IsKingdomFaction ? ((Kingdom)currentSettlement.MapFaction).EncyclopediaTitle : currentSettlement.MapFaction.Name);
			return textObject;
		}

		// Token: 0x06004750 RID: 18256 RVA: 0x001655A8 File Offset: 0x001637A8
		[GameMenuInitializationHandler("village_player_raid_ended")]
		private static void game_menu_village_raid_ended_menu_sound_on_init(MenuCallbackArgs args)
		{
			args.MenuContext.SetBackgroundMeshName("wait_raiding_village");
			if (MobileParty.MainParty.LastVisitedSettlement != null && MobileParty.MainParty.LastVisitedSettlement.MapFaction.IsAtWarWith(Hero.MainHero.MapFaction))
			{
				args.MenuContext.SetAmbientSound("event:/map/ambient/node/settlements/2d/village_raided");
			}
		}

		// Token: 0x06004751 RID: 18257 RVA: 0x00165601 File Offset: 0x00163801
		[GameMenuInitializationHandler("village_looted")]
		[GameMenuInitializationHandler("village_raid_ended_leaded_by_someone_else")]
		[GameMenuInitializationHandler("raiding_village")]
		private static void game_menu_ui_village_hostile_raid_on_init(MenuCallbackArgs args)
		{
			args.MenuContext.SetBackgroundMeshName("wait_raiding_village");
		}

		// Token: 0x06004752 RID: 18258 RVA: 0x00165614 File Offset: 0x00163814
		[GameMenuInitializationHandler("village_hostile_action")]
		[GameMenuInitializationHandler("force_volunteers_village")]
		[GameMenuInitializationHandler("force_supplies_village")]
		[GameMenuInitializationHandler("raid_village_no_resist_warn_player")]
		[GameMenuInitializationHandler("raid_village_resisted")]
		[GameMenuInitializationHandler("village_loot_no_resist")]
		[GameMenuInitializationHandler("village_take_food_confirm")]
		[GameMenuInitializationHandler("village_press_into_service_confirm")]
		[GameMenuInitializationHandler("menu_press_into_service_success")]
		[GameMenuInitializationHandler("menu_village_take_food_success")]
		private static void game_menu_village_menu_on_init(MenuCallbackArgs args)
		{
			Village village = Settlement.CurrentSettlement.Village;
			args.MenuContext.SetBackgroundMeshName(village.WaitMeshName);
		}

		// Token: 0x06004753 RID: 18259 RVA: 0x0016563D File Offset: 0x0016383D
		private static bool hostile_action_common_back_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Leave;
			return true;
		}

		// Token: 0x06004754 RID: 18260 RVA: 0x00165648 File Offset: 0x00163848
		private static bool hostile_action_common_continue_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Continue;
			return true;
		}

		// Token: 0x06004755 RID: 18261 RVA: 0x00165654 File Offset: 0x00163854
		private static void StartHostileAction(VillageHostileActionCampaignBehavior.HostileActionType hostileActionType)
		{
			BeHostileAction.ApplyEncounterHostileAction(PartyBase.MainParty, Settlement.CurrentSettlement.Party);
			if (hostileActionType == VillageHostileActionCampaignBehavior.HostileActionType.Raid)
			{
				PlayerEncounter.Current.ForceRaid = true;
			}
			else if (hostileActionType == VillageHostileActionCampaignBehavior.HostileActionType.ForceTroop)
			{
				PlayerEncounter.Current.ForceVolunteers = true;
			}
			else if (hostileActionType == VillageHostileActionCampaignBehavior.HostileActionType.ForceSupply)
			{
				PlayerEncounter.Current.ForceSupplies = true;
			}
			GameMenu.SwitchToMenu("encounter");
		}

		// Token: 0x06004756 RID: 18262 RVA: 0x001656B0 File Offset: 0x001638B0
		private void CheckVillageAttackableHonorably(MenuCallbackArgs args)
		{
			Settlement currentSettlement = MobileParty.MainParty.CurrentSettlement;
			IFaction faction = ((currentSettlement != null) ? currentSettlement.MapFaction : null);
			this.CheckFactionAttackableHonorably(args, faction);
		}

		// Token: 0x06004757 RID: 18263 RVA: 0x001656DC File Offset: 0x001638DC
		private static void OnItemsLooted(MobileParty mobileParty, ItemRoster lootedItems)
		{
			SkillLevelingManager.OnRaid(mobileParty, lootedItems);
		}

		// Token: 0x06004758 RID: 18264 RVA: 0x001656E8 File Offset: 0x001638E8
		private void CheckFactionAttackableHonorably(MenuCallbackArgs args, IFaction faction)
		{
			if (faction.NotAttackableByPlayerUntilTime.IsFuture)
			{
				args.IsEnabled = false;
				args.Tooltip = this.EnemyNotAttackableTooltip;
			}
		}

		// Token: 0x040013EC RID: 5100
		private const int IntervalForHostileActionAsDay = 10;

		// Token: 0x040013ED RID: 5101
		private readonly TextObject EnemyNotAttackableTooltip = GameTexts.FindText("str_enemy_not_attackable_tooltip", null);

		// Token: 0x040013EE RID: 5102
		private Dictionary<string, CampaignTime> _villageLastHostileActionTimeDictionary = new Dictionary<string, CampaignTime>();

		// Token: 0x040013EF RID: 5103
		private IFaction _raiderPartyMapFaction;

		// Token: 0x040013F0 RID: 5104
		private Village _raidedVillage;

		// Token: 0x0200086C RID: 2156
		private enum HostileActionType
		{
			// Token: 0x0400242E RID: 9262
			Raid,
			// Token: 0x0400242F RID: 9263
			ForceTroop,
			// Token: 0x04002430 RID: 9264
			ForceSupply
		}
	}
}
