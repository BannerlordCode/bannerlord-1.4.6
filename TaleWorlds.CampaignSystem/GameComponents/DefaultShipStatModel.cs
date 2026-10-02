using System;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Naval;

namespace TaleWorlds.CampaignSystem.GameComponents
{
	// Token: 0x02000155 RID: 341
	public class DefaultShipStatModel : ShipStatModel
	{
		// Token: 0x06001A7A RID: 6778 RVA: 0x00086553 File Offset: 0x00084753
		public override float GetShipFlagshipScore(Ship ship)
		{
			return 0f;
		}
	}
}
