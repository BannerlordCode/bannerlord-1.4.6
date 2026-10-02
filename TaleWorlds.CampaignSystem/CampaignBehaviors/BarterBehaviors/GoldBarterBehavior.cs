using System;
using TaleWorlds.CampaignSystem.BarterSystem;
using TaleWorlds.CampaignSystem.BarterSystem.Barterables;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.BarterBehaviors
{
	// Token: 0x02000468 RID: 1128
	public class GoldBarterBehavior : CampaignBehaviorBase
	{
		// Token: 0x0600485D RID: 18525 RVA: 0x0016C89C File Offset: 0x0016AA9C
		public override void RegisterEvents()
		{
			CampaignEvents.BarterablesRequested.AddNonSerializedListener(this, new Action<BarterData>(this.CheckForBarters));
		}

		// Token: 0x0600485E RID: 18526 RVA: 0x0016C8B5 File Offset: 0x0016AAB5
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x0600485F RID: 18527 RVA: 0x0016C8B8 File Offset: 0x0016AAB8
		public void CheckForBarters(BarterData args)
		{
			if ((args.OffererHero != null && args.OtherHero != null && args.OffererHero.Clan != args.OtherHero.Clan) || (args.OffererHero == null && args.OffererParty != null) || (args.OtherHero == null && args.OtherParty != null))
			{
				int num = ((args.OffererHero != null) ? args.OffererHero.Gold : args.OffererParty.MobileParty.PartyTradeGold);
				int num2 = ((args.OtherHero != null) ? args.OtherHero.Gold : args.OtherParty.MobileParty.PartyTradeGold);
				Barterable barterable = new GoldBarterable(args.OffererHero, args.OtherHero, args.OffererParty, args.OtherParty, num);
				args.AddBarterable<GoldBarterGroup>(barterable, false);
				Barterable barterable2 = new GoldBarterable(args.OtherHero, args.OffererHero, args.OtherParty, args.OffererParty, num2);
				args.AddBarterable<GoldBarterGroup>(barterable2, false);
			}
		}
	}
}
