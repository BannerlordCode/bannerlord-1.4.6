using System;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.Inventory
{
	// Token: 0x020000D7 RID: 215
	public class FakeInventoryListener : InventoryListener
	{
		// Token: 0x06001483 RID: 5251 RVA: 0x0005F0FB File Offset: 0x0005D2FB
		public override int GetGold()
		{
			return 0;
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x0005F0FE File Offset: 0x0005D2FE
		public override TextObject GetTraderName()
		{
			return TextObject.GetEmpty();
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x0005F105 File Offset: 0x0005D305
		public override void SetGold(int gold)
		{
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x0005F107 File Offset: 0x0005D307
		public override void OnTransaction()
		{
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x0005F109 File Offset: 0x0005D309
		public override PartyBase GetOppositeParty()
		{
			return null;
		}
	}
}
