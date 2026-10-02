using System;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Naval;
using TaleWorlds.CampaignSystem.Party;

namespace TaleWorlds.CampaignSystem.GameComponents
{
	// Token: 0x020000FD RID: 253
	public class DefaultCampaignShipDamageModel : CampaignShipDamageModel
	{
		// Token: 0x060016A7 RID: 5799 RVA: 0x00069047 File Offset: 0x00067247
		public override int GetHourlyShipDamage(MobileParty owner, Ship ship)
		{
			return 0;
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x0006904A File Offset: 0x0006724A
		public override float GetEstimatedSafeSailDuration(MobileParty mobileParty)
		{
			return 0f;
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x00069051 File Offset: 0x00067251
		public override float GetShipDamage(Ship ship, Ship rammingShip, float rawDamage)
		{
			return rawDamage;
		}
	}
}
