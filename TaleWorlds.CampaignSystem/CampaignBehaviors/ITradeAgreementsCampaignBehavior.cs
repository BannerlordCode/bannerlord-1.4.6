using System;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x0200040F RID: 1039
	public interface ITradeAgreementsCampaignBehavior
	{
		// Token: 0x06004174 RID: 16756
		void MakeTradeAgreement(Kingdom kingdom1, Kingdom kingdom2, CampaignTime duration);

		// Token: 0x06004175 RID: 16757
		bool HasTradeAgreement(Kingdom kingdom, Kingdom other, out TradeAgreementsCampaignBehavior.TradeAgreement tradeAgreement);

		// Token: 0x06004176 RID: 16758
		void EndTradeAgreement(Kingdom kingdom, Kingdom other);

		// Token: 0x06004177 RID: 16759
		void OnTradeAgreementOfferedToPlayer(Kingdom fromKingdom);

		// Token: 0x06004178 RID: 16760
		CampaignTime GetTradeAgreementEndDate(Kingdom kingdom, Kingdom other);

		// Token: 0x06004179 RID: 16761
		void OnTradeGoldDistributedInKingdom(Kingdom kingdom1, Kingdom kingdom2, Clan clan, int share);
	}
}
