using System;
using System.Collections.Generic;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003FA RID: 1018
	public interface IAllianceCampaignBehavior
	{
		// Token: 0x06004036 RID: 16438
		void OnAllianceOfferedToPlayerKingdom(Kingdom proposerKingdom);

		// Token: 0x06004037 RID: 16439
		void OnAllianceOfferedToPlayer(Kingdom proposerKingdom);

		// Token: 0x06004038 RID: 16440
		void OnCallToWarAgreementProposedToPlayerKingdom(Kingdom proposerKingdom, Kingdom kingdomToCallToWarAgainst);

		// Token: 0x06004039 RID: 16441
		void OnCallToWarAgreementProposedByPlayerKingdom(Kingdom proposedKingdom, Kingdom kingdomToCallToWarAgainst);

		// Token: 0x0600403A RID: 16442
		void OnCallToWarAgreementProposedToPlayer(Kingdom proposerKingdom, Kingdom kingdomToCallToWarAgainst);

		// Token: 0x0600403B RID: 16443
		void OnCallToWarAgreementProposedByPlayer(Kingdom proposedKingdom, Kingdom kingdomToCallToWarAgainst);

		// Token: 0x0600403C RID: 16444
		bool IsAllyWithKingdom(Kingdom kingdom1, Kingdom kingdom2);

		// Token: 0x0600403D RID: 16445
		void StartAlliance(Kingdom proposerKingdom, Kingdom receiverKingdom);

		// Token: 0x0600403E RID: 16446
		void EndAlliance(Kingdom kingdom1, Kingdom kingdom2);

		// Token: 0x0600403F RID: 16447
		bool HasCalledToWar(Kingdom callingKingdom, Kingdom calledKingdom);

		// Token: 0x06004040 RID: 16448
		bool IsAtWarByCallToWarAgreement(Kingdom calledKingdom, Kingdom kingdomToCallToWarAgainst, out Kingdom callingKingdom);

		// Token: 0x06004041 RID: 16449
		void StartCallToWarAgreement(Kingdom callingKingdom, Kingdom calledKingdom, Kingdom kingdomToCallToWarAgainst, int callToWarCost, bool isPlayerPaying = false);

		// Token: 0x06004042 RID: 16450
		void EndCallToWarAgreement(Kingdom callingKingdom, Kingdom calledKingdom, Kingdom kingdomToCallToWarAgainst);

		// Token: 0x06004043 RID: 16451
		List<Kingdom> GetKingdomsToCallToWarAgainst(Kingdom callingKingdom, Kingdom calledKingdom);

		// Token: 0x06004044 RID: 16452
		CampaignTime GetAllianceEndDate(Kingdom kingdom1, Kingdom kingdom2);

		// Token: 0x06004045 RID: 16453
		void DenyCallToWarAgreement(Kingdom callingKingdom, Kingdom calledKingdom);
	}
}
