using System;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Naval;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.GameComponents
{
	// Token: 0x02000117 RID: 279
	public class DefaultFleetManagementModel : FleetManagementModel
	{
		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x0600180B RID: 6155 RVA: 0x0007382B File Offset: 0x00071A2B
		public override int MinimumTroopCountRequiredToSendShips
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x0007382E File Offset: 0x00071A2E
		public override bool CanSendShipToPlayerClan(Ship ship, int playerShipsCount, int troopsCountToSend, out TextObject hint)
		{
			hint = TextObject.GetEmpty();
			return false;
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x00073839 File Offset: 0x00071A39
		public override bool CanTroopsReturn()
		{
			return false;
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x0007383C File Offset: 0x00071A3C
		public override CampaignTime GetReturnTimeForTroops(Ship ship)
		{
			return CampaignTime.Never;
		}
	}
}
