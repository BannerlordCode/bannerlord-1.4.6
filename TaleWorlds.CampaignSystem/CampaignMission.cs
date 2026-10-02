using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Naval;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x0200005B RID: 91
	public static class CampaignMission
	{
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x00027646 File Offset: 0x00025846
		// (set) Token: 0x06000904 RID: 2308 RVA: 0x0002764D File Offset: 0x0002584D
		public static ICampaignMission Current { get; set; }

		// Token: 0x06000905 RID: 2309 RVA: 0x00027655 File Offset: 0x00025855
		public static IMission OpenBattleMission(string scene, bool usesTownDecalAtlas, string sceneLevels = "")
		{
			return Campaign.Current.CampaignMissionManager.OpenBattleMission(scene, usesTownDecalAtlas, sceneLevels);
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00027669 File Offset: 0x00025869
		public static IMission OpenNavalRaidMission(TroopRoster attackerSideTroops, BattleSideEnum navalSide, List<Ship> allShips)
		{
			return Campaign.Current.CampaignMissionManager.OpenNavalRaidMission(attackerSideTroops, navalSide, allShips);
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0002767D File Offset: 0x0002587D
		public static IMission OpenAlleyFightMission(string scene, int upgradeLevel, Location location, TroopRoster playerSideTroops, TroopRoster rivalSideTroops)
		{
			return Campaign.Current.CampaignMissionManager.OpenAlleyFightMission(scene, upgradeLevel, location, playerSideTroops, rivalSideTroops);
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00027694 File Offset: 0x00025894
		public static IMission OpenCombatMissionWithDialogue(string scene, CharacterObject characterToTalkTo, int upgradeLevel)
		{
			return Campaign.Current.CampaignMissionManager.OpenCombatMissionWithDialogue(scene, characterToTalkTo, upgradeLevel);
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x000276A8 File Offset: 0x000258A8
		public static IMission OpenBattleMissionWhileEnteringSettlement(string scene, int upgradeLevel, int numberOfMaxTroopToBeSpawnedForPlayer, int numberOfMaxTroopToBeSpawnedForOpponent)
		{
			return Campaign.Current.CampaignMissionManager.OpenBattleMissionWhileEnteringSettlement(scene, upgradeLevel, numberOfMaxTroopToBeSpawnedForPlayer, numberOfMaxTroopToBeSpawnedForOpponent);
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x000276BD File Offset: 0x000258BD
		public static IMission OpenHideoutBattleMission(string scene, FlattenedTroopRoster playerTroops, bool isTutorial)
		{
			return Campaign.Current.CampaignMissionManager.OpenHideoutBattleMission(scene, playerTroops, isTutorial);
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x000276D4 File Offset: 0x000258D4
		public static IMission OpenSiegeMissionWithDeployment(string scene, float[] wallHitPointsPercentages, bool hasAnySiegeTower, List<MissionSiegeWeapon> siegeWeaponsOfAttackers, List<MissionSiegeWeapon> siegeWeaponsOfDefenders, bool isPlayerAttacker, int upgradeLevel = 0, bool isSallyOut = false, bool isReliefForceAttack = false)
		{
			return Campaign.Current.CampaignMissionManager.OpenSiegeMissionWithDeployment(scene, wallHitPointsPercentages, hasAnySiegeTower, siegeWeaponsOfAttackers, siegeWeaponsOfDefenders, isPlayerAttacker, upgradeLevel, isSallyOut, isReliefForceAttack);
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x000276FE File Offset: 0x000258FE
		public static IMission OpenSiegeMissionNoDeployment(string scene, bool isSallyOut = false, bool isReliefForceAttack = false)
		{
			return Campaign.Current.CampaignMissionManager.OpenSiegeMissionNoDeployment(scene, isSallyOut, isReliefForceAttack);
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00027712 File Offset: 0x00025912
		public static IMission OpenSiegeLordsHallFightMission(string scene, FlattenedTroopRoster attackerPriorityList)
		{
			return Campaign.Current.CampaignMissionManager.OpenSiegeLordsHallFightMission(scene, attackerPriorityList);
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00027725 File Offset: 0x00025925
		public static IMission OpenBattleMission(MissionInitializerRecord rec)
		{
			return Campaign.Current.CampaignMissionManager.OpenBattleMission(rec);
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00027737 File Offset: 0x00025937
		public static IMission OpenNavalBattleMission(MissionInitializerRecord rec)
		{
			return Campaign.Current.CampaignMissionManager.OpenNavalBattleMission(rec);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00027749 File Offset: 0x00025949
		public static IMission OpenNavalSetPieceBattleMission(MissionInitializerRecord rec, MBList<IShipOrigin> playerShips, MBList<IShipOrigin> playerAllyShips, MBList<IShipOrigin> enemyShips)
		{
			return Campaign.Current.CampaignMissionManager.OpenNavalSetPieceBattleMission(rec, playerShips, playerAllyShips, enemyShips);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0002775E File Offset: 0x0002595E
		public static IMission OpenCaravanBattleMission(MissionInitializerRecord rec, bool isCaravan)
		{
			return Campaign.Current.CampaignMissionManager.OpenCaravanBattleMission(rec, isCaravan);
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00027771 File Offset: 0x00025971
		public static IMission OpenTownCenterMission(string scene, Location location, CharacterObject talkToChar, int townUpgradeLevel, string playerSpawnTag)
		{
			return Campaign.Current.CampaignMissionManager.OpenTownCenterMission(scene, townUpgradeLevel, location, talkToChar, playerSpawnTag);
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00027788 File Offset: 0x00025988
		public static IMission OpenCastleCourtyardMission(string scene, Location location, CharacterObject talkToChar, int castleUpgradeLevel)
		{
			return Campaign.Current.CampaignMissionManager.OpenCastleCourtyardMission(scene, castleUpgradeLevel, location, talkToChar);
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0002779D File Offset: 0x0002599D
		public static IMission OpenVillageMission(string scene, Location location, CharacterObject talkToChar)
		{
			return Campaign.Current.CampaignMissionManager.OpenVillageMission(scene, location, talkToChar);
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x000277B1 File Offset: 0x000259B1
		public static IMission OpenIndoorMission(string scene, int upgradeLevel, Location location, CharacterObject talkToChar)
		{
			return Campaign.Current.CampaignMissionManager.OpenIndoorMission(scene, upgradeLevel, location, talkToChar);
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x000277C6 File Offset: 0x000259C6
		public static IMission OpenPrisonBreakMission(string scene, Location location, CharacterObject prisonerCharacter)
		{
			return Campaign.Current.CampaignMissionManager.OpenPrisonBreakMission(scene, location, prisonerCharacter);
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x000277DA File Offset: 0x000259DA
		public static IMission OpenArenaStartMission(string scene, Location location, CharacterObject talkToChar)
		{
			return Campaign.Current.CampaignMissionManager.OpenArenaStartMission(scene, location, talkToChar);
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x000277EE File Offset: 0x000259EE
		public static IMission OpenArenaDuelMission(string scene, Location location, CharacterObject talkToChar, bool requireCivilianEquipment, bool spawnBothSidesWithHorse, Action<CharacterObject> onDuelEnd, float customAgentHealth)
		{
			return Campaign.Current.CampaignMissionManager.OpenArenaDuelMission(scene, location, talkToChar, requireCivilianEquipment, spawnBothSidesWithHorse, onDuelEnd, customAgentHealth);
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x00027809 File Offset: 0x00025A09
		public static IMission OpenConversationMission(ConversationCharacterData playerCharacterData, ConversationCharacterData conversationPartnerData, string specialScene = "", string sceneLevels = "", bool isMultiAgentConversation = false)
		{
			return Campaign.Current.CampaignMissionManager.OpenConversationMission(playerCharacterData, conversationPartnerData, specialScene, sceneLevels, isMultiAgentConversation);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x00027820 File Offset: 0x00025A20
		public static IMission OpenRetirementMission(string scene, Location location, CharacterObject talkToChar = null, string sceneLevels = null, string unconsciousMenuId = "")
		{
			return Campaign.Current.CampaignMissionManager.OpenRetirementMission(scene, location, talkToChar, sceneLevels, unconsciousMenuId);
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x00027837 File Offset: 0x00025A37
		public static IMission OpenHideoutAmbushMission(string sceneName, FlattenedTroopRoster playerTroops, Location location)
		{
			return Campaign.Current.CampaignMissionManager.OpenHideoutAmbushMission(sceneName, playerTroops, location);
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0002784B File Offset: 0x00025A4B
		public static IMission OpenDisguiseMission(string scene, bool willSetUpContact, string sceneLevels, Location fromLocation)
		{
			return Campaign.Current.CampaignMissionManager.OpenDisguiseMission(scene, willSetUpContact, sceneLevels, fromLocation);
		}

		// Token: 0x02000516 RID: 1302
		public interface ICampaignMissionManager
		{
			// Token: 0x06004C36 RID: 19510
			IMission OpenSiegeMissionWithDeployment(string scene, float[] wallHitPointsPercentages, bool hasAnySiegeTower, List<MissionSiegeWeapon> siegeWeaponsOfAttackers, List<MissionSiegeWeapon> siegeWeaponsOfDefenders, bool isPlayerAttacker, int upgradeLevel = 0, bool isSallyOut = false, bool isReliefForceAttack = false);

			// Token: 0x06004C37 RID: 19511
			IMission OpenSiegeMissionNoDeployment(string scene, bool isSallyOut = false, bool isReliefForceAttack = false);

			// Token: 0x06004C38 RID: 19512
			IMission OpenSiegeLordsHallFightMission(string scene, FlattenedTroopRoster attackerPriorityList);

			// Token: 0x06004C39 RID: 19513
			IMission OpenBattleMission(MissionInitializerRecord rec);

			// Token: 0x06004C3A RID: 19514
			IMission OpenCaravanBattleMission(MissionInitializerRecord rec, bool isCaravan);

			// Token: 0x06004C3B RID: 19515
			IMission OpenBattleMission(string scene, bool usesTownDecalAtlas, string sceneLevels);

			// Token: 0x06004C3C RID: 19516
			IMission OpenNavalRaidMission(TroopRoster navalRaidTroops, BattleSideEnum navalSide, List<Ship> allShips);

			// Token: 0x06004C3D RID: 19517
			IMission OpenNavalBattleMission(MissionInitializerRecord rec);

			// Token: 0x06004C3E RID: 19518
			IMission OpenNavalSetPieceBattleMission(MissionInitializerRecord rec, MBList<IShipOrigin> playerShips, MBList<IShipOrigin> playerAllyShips, MBList<IShipOrigin> enemyShips);

			// Token: 0x06004C3F RID: 19519
			IMission OpenHideoutBattleMission(string scene, FlattenedTroopRoster playerTroops, bool isTutorial);

			// Token: 0x06004C40 RID: 19520
			IMission OpenTownCenterMission(string scene, int townUpgradeLevel, Location location, CharacterObject talkToChar, string playerSpawnTag);

			// Token: 0x06004C41 RID: 19521
			IMission OpenCastleCourtyardMission(string scene, int castleUpgradeLevel, Location location, CharacterObject talkToChar);

			// Token: 0x06004C42 RID: 19522
			IMission OpenVillageMission(string scene, Location location, CharacterObject talkToChar);

			// Token: 0x06004C43 RID: 19523
			IMission OpenIndoorMission(string scene, int upgradeLevel, Location location, CharacterObject talkToChar);

			// Token: 0x06004C44 RID: 19524
			IMission OpenPrisonBreakMission(string scene, Location location, CharacterObject prisonerCharacter);

			// Token: 0x06004C45 RID: 19525
			IMission OpenArenaStartMission(string scene, Location location, CharacterObject talkToChar);

			// Token: 0x06004C46 RID: 19526
			IMission OpenArenaDuelMission(string scene, Location location, CharacterObject duelCharacter, bool requireCivilianEquipment, bool spawnBOthSidesWithHorse, Action<CharacterObject> onDuelEndAction, float customAgentHealth);

			// Token: 0x06004C47 RID: 19527
			IMission OpenConversationMission(ConversationCharacterData playerCharacterData, ConversationCharacterData conversationPartnerData, string specialScene = "", string sceneLevels = "", bool isMultiAgentConversation = false);

			// Token: 0x06004C48 RID: 19528
			IMission OpenMeetingMission(string scene, CharacterObject character);

			// Token: 0x06004C49 RID: 19529
			IMission OpenAlleyFightMission(string scene, int upgradeLevel, Location location, TroopRoster playerSideTroops, TroopRoster rivalSideTroops);

			// Token: 0x06004C4A RID: 19530
			IMission OpenCombatMissionWithDialogue(string scene, CharacterObject characterToTalkTo, int upgradeLevel);

			// Token: 0x06004C4B RID: 19531
			IMission OpenBattleMissionWhileEnteringSettlement(string scene, int upgradeLevel, int numberOfMaxTroopToBeSpawnedForPlayer, int numberOfMaxTroopToBeSpawnedForOpponent);

			// Token: 0x06004C4C RID: 19532
			IMission OpenRetirementMission(string scene, Location location, CharacterObject talkToChar = null, string sceneLevels = null, string unconsciousMenuId = "");

			// Token: 0x06004C4D RID: 19533
			IMission OpenHideoutAmbushMission(string sceneName, FlattenedTroopRoster playerTroops, Location location);

			// Token: 0x06004C4E RID: 19534
			IMission OpenDisguiseMission(string scene, bool willSetUpContact, string sceneLevels, Location fromLocation);
		}
	}
}
