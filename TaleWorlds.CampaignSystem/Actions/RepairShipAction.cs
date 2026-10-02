using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Naval;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x020004C3 RID: 1219
	public static class RepairShipAction
	{
		// Token: 0x06004ADB RID: 19163 RVA: 0x0017AE57 File Offset: 0x00179057
		private static void ApplyInternal(Ship ship, float newHitpoints, Settlement repairPort = null)
		{
			SkillLevelingManager.OnShipRepaired(ship, newHitpoints - ship.HitPoints);
			ship.HitPoints = newHitpoints;
			CampaignEventDispatcher.Instance.OnShipRepaired(ship, repairPort);
		}

		// Token: 0x06004ADC RID: 19164 RVA: 0x0017AE7C File Offset: 0x0017907C
		public static void Apply(Ship ship, Settlement repairPort)
		{
			PartyBase owner = ship.Owner;
			if (owner.IsMobile && (owner.MobileParty.IsCaravan || owner.MobileParty.IsLordParty))
			{
				int num = (int)Campaign.Current.Models.ShipCostModel.GetShipRepairCost(ship, owner);
				GiveGoldAction.ApplyForPartyToSettlement(owner, repairPort, num, false);
			}
			RepairShipAction.ApplyInternal(ship, ship.MaxHitPoints, repairPort);
		}

		// Token: 0x06004ADD RID: 19165 RVA: 0x0017AEE0 File Offset: 0x001790E0
		public static void ApplyForFree(Ship ship)
		{
			RepairShipAction.ApplyInternal(ship, ship.MaxHitPoints, null);
		}

		// Token: 0x06004ADE RID: 19166 RVA: 0x0017AEEF File Offset: 0x001790EF
		public static void ApplyForBanditShip(Ship ship)
		{
			if (ship.HitPoints < ship.MaxHitPoints * 0.8f)
			{
				RepairShipAction.ApplyInternal(ship, ship.MaxHitPoints * 0.8f, null);
			}
		}
	}
}
