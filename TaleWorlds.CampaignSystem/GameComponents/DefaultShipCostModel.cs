using System;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Naval;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.GameComponents
{
	// Token: 0x02000154 RID: 340
	public class DefaultShipCostModel : ShipCostModel
	{
		// Token: 0x06001A75 RID: 6773 RVA: 0x00086533 File Offset: 0x00084733
		public override float GetShipTradeValue(Ship ship, PartyBase seller, PartyBase buyer)
		{
			return 0f;
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x0008653A File Offset: 0x0008473A
		public override float GetShipRepairCost(Ship ship, PartyBase owner)
		{
			return 0f;
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x00086541 File Offset: 0x00084741
		public override int GetShipUpgradePieceCost(Ship ship, ShipUpgradePiece piece, PartyBase owner)
		{
			return 0;
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x00086544 File Offset: 0x00084744
		public override float GetShipSellingPenalty()
		{
			return 0f;
		}
	}
}
