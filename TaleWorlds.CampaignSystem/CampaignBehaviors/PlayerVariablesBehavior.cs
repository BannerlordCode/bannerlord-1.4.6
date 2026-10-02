using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x02000433 RID: 1075
	public class PlayerVariablesBehavior : CampaignBehaviorBase
	{
		// Token: 0x06004500 RID: 17664 RVA: 0x00152580 File Offset: 0x00150780
		public override void RegisterEvents()
		{
			CampaignEvents.PlayerDesertedBattleEvent.AddNonSerializedListener(this, new Action<int>(this.OnPlayerDesertedBattle));
			CampaignEvents.VillageLooted.AddNonSerializedListener(this, new Action<Village>(this.OnVillageLooted));
			CampaignEvents.OnPlayerBattleEndEvent.AddNonSerializedListener(this, new Action<MapEvent>(this.OnPlayerBattleEnd));
		}

		// Token: 0x06004501 RID: 17665 RVA: 0x001525D2 File Offset: 0x001507D2
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06004502 RID: 17666 RVA: 0x001525D4 File Offset: 0x001507D4
		private void OnPlayerDesertedBattle(int sacrificedMenCount)
		{
			SkillLevelingManager.OnTacticsUsed(MobileParty.MainParty, (float)(sacrificedMenCount * 50));
			TraitLevelingHelper.OnTroopsSacrificed();
		}

		// Token: 0x06004503 RID: 17667 RVA: 0x001525EA File Offset: 0x001507EA
		private void OnVillageLooted(Village village)
		{
			if (PlayerEncounter.Current != null && PlayerEncounter.PlayerIsAttacker && PlayerEncounter.EncounterSettlement != null && PlayerEncounter.EncounterSettlement.Village == village)
			{
				TraitLevelingHelper.OnVillageRaided();
			}
		}

		// Token: 0x06004504 RID: 17668 RVA: 0x00152613 File Offset: 0x00150813
		private void OnPlayerBattleEnd(MapEvent mapEvent)
		{
			TraitLevelingHelper.OnBattleWon(mapEvent, mapEvent.GetPlayerBattleContributionRate());
		}
	}
}
