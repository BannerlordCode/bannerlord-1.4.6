using System;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x020004A4 RID: 1188
	public static class ChangeVillageStateAction
	{
		// Token: 0x06004A51 RID: 19025 RVA: 0x00178470 File Offset: 0x00176670
		private static void ApplyInternal(Village village, Village.VillageStates newState, MobileParty raiderParty)
		{
			Village.VillageStates villageState = village.VillageState;
			if (newState != villageState)
			{
				village.VillageState = newState;
				CampaignEventDispatcher.Instance.OnVillageStateChanged(village, villageState, village.VillageState, raiderParty);
				village.Settlement.Party.SetLevelMaskIsDirty();
			}
		}

		// Token: 0x06004A52 RID: 19026 RVA: 0x001784B2 File Offset: 0x001766B2
		public static void ApplyBySettingToNormal(Settlement settlement)
		{
			ChangeVillageStateAction.ApplyInternal(settlement.Village, Village.VillageStates.Normal, null);
		}

		// Token: 0x06004A53 RID: 19027 RVA: 0x001784C1 File Offset: 0x001766C1
		public static void ApplyBySettingToBeingRaided(Settlement settlement, MobileParty raider)
		{
			ChangeVillageStateAction.ApplyInternal(settlement.Village, Village.VillageStates.BeingRaided, raider);
		}

		// Token: 0x06004A54 RID: 19028 RVA: 0x001784D0 File Offset: 0x001766D0
		public static void ApplyBySettingToBeingForcedForSupplies(Settlement settlement, MobileParty raider)
		{
			ChangeVillageStateAction.ApplyInternal(settlement.Village, Village.VillageStates.ForcedForSupplies, raider);
		}

		// Token: 0x06004A55 RID: 19029 RVA: 0x001784DF File Offset: 0x001766DF
		public static void ApplyBySettingToBeingForcedForVolunteers(Settlement settlement, MobileParty raider)
		{
			ChangeVillageStateAction.ApplyInternal(settlement.Village, Village.VillageStates.ForcedForVolunteers, raider);
		}

		// Token: 0x06004A56 RID: 19030 RVA: 0x001784EE File Offset: 0x001766EE
		public static void ApplyBySettingToLooted(Settlement settlement, MobileParty raider)
		{
			ChangeVillageStateAction.ApplyInternal(settlement.Village, Village.VillageStates.Looted, raider);
		}
	}
}
