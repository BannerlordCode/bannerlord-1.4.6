using System;
using System.Linq;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Party;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x020004AC RID: 1196
	public static class DisbandArmyAction
	{
		// Token: 0x06004A72 RID: 19058 RVA: 0x00178C74 File Offset: 0x00176E74
		private static void ApplyInternal(Army army, Army.ArmyDispersionReason reason)
		{
			if (reason == Army.ArmyDispersionReason.DismissalRequestedWithInfluence)
			{
				DiplomacyModel diplomacyModel = Campaign.Current.Models.DiplomacyModel;
				ChangeClanInfluenceAction.Apply(Clan.PlayerClan, (float)(-(float)diplomacyModel.GetInfluenceCostOfDisbandingArmy()));
				foreach (MobileParty mobileParty in army.Parties.ToList<MobileParty>())
				{
					if (mobileParty != MobileParty.MainParty && mobileParty.LeaderHero != null)
					{
						ChangeRelationAction.ApplyPlayerRelation(mobileParty.LeaderHero, diplomacyModel.GetRelationCostOfDisbandingArmy(mobileParty == mobileParty.Army.LeaderParty), true, true);
					}
				}
			}
			army.DisperseInternal(reason);
		}

		// Token: 0x06004A73 RID: 19059 RVA: 0x00178D28 File Offset: 0x00176F28
		public static void ApplyByReleasedByPlayerAfterBattle(Army army)
		{
			DisbandArmyAction.ApplyInternal(army, Army.ArmyDispersionReason.DismissalRequestedWithInfluence);
		}

		// Token: 0x06004A74 RID: 19060 RVA: 0x00178D31 File Offset: 0x00176F31
		public static void ApplyByArmyLeaderIsDead(Army army)
		{
			DisbandArmyAction.ApplyInternal(army, Army.ArmyDispersionReason.ArmyLeaderIsDead);
		}

		// Token: 0x06004A75 RID: 19061 RVA: 0x00178D3B File Offset: 0x00176F3B
		public static void ApplyByNotEnoughParty(Army army)
		{
			DisbandArmyAction.ApplyInternal(army, Army.ArmyDispersionReason.NotEnoughParty);
		}

		// Token: 0x06004A76 RID: 19062 RVA: 0x00178D44 File Offset: 0x00176F44
		public static void ApplyByObjectiveFinished(Army army)
		{
			DisbandArmyAction.ApplyInternal(army, Army.ArmyDispersionReason.ObjectiveFinished);
		}

		// Token: 0x06004A77 RID: 19063 RVA: 0x00178D4D File Offset: 0x00176F4D
		public static void ApplyByPlayerTakenPrisoner(Army army)
		{
			DisbandArmyAction.ApplyInternal(army, Army.ArmyDispersionReason.PlayerTakenPrisoner);
		}

		// Token: 0x06004A78 RID: 19064 RVA: 0x00178D56 File Offset: 0x00176F56
		public static void ApplyByFoodProblem(Army army)
		{
			DisbandArmyAction.ApplyInternal(army, Army.ArmyDispersionReason.FoodProblem);
		}

		// Token: 0x06004A79 RID: 19065 RVA: 0x00178D60 File Offset: 0x00176F60
		public static void ApplyByInactivity(Army army)
		{
			DisbandArmyAction.ApplyInternal(army, Army.ArmyDispersionReason.Inactivity);
		}

		// Token: 0x06004A7A RID: 19066 RVA: 0x00178D6A File Offset: 0x00176F6A
		public static void ApplyByCohesionDepleted(Army army)
		{
			DisbandArmyAction.ApplyInternal(army, Army.ArmyDispersionReason.CohesionDepleted);
		}

		// Token: 0x06004A7B RID: 19067 RVA: 0x00178D73 File Offset: 0x00176F73
		public static void ApplyByNoActiveWar(Army army)
		{
			DisbandArmyAction.ApplyInternal(army, Army.ArmyDispersionReason.NoActiveWar);
		}

		// Token: 0x06004A7C RID: 19068 RVA: 0x00178D7D File Offset: 0x00176F7D
		public static void ApplyByUnknownReason(Army army)
		{
			DisbandArmyAction.ApplyInternal(army, Army.ArmyDispersionReason.Unknown);
		}

		// Token: 0x06004A7D RID: 19069 RVA: 0x00178D86 File Offset: 0x00176F86
		public static void ApplyByLeaderPartyRemoved(Army army)
		{
			DisbandArmyAction.ApplyInternal(army, Army.ArmyDispersionReason.LeaderPartyRemoved);
		}

		// Token: 0x06004A7E RID: 19070 RVA: 0x00178D8F File Offset: 0x00176F8F
		public static void ApplyByNoShip(Army army)
		{
			DisbandArmyAction.ApplyInternal(army, Army.ArmyDispersionReason.NoShipToUse);
		}
	}
}
