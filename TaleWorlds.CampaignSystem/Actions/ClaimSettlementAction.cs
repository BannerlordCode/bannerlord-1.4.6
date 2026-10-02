using System;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x020004A5 RID: 1189
	public static class ClaimSettlementAction
	{
		// Token: 0x06004A57 RID: 19031 RVA: 0x001784FD File Offset: 0x001766FD
		private static void ApplyInternal(Hero claimant, Settlement claimedSettlement)
		{
			ClaimSettlementAction.ImpactRelations(claimant, claimedSettlement);
		}

		// Token: 0x06004A58 RID: 19032 RVA: 0x00178508 File Offset: 0x00176708
		private static void ImpactRelations(Hero claimant, Settlement claimedSettlement)
		{
			if (claimedSettlement.OwnerClan.Leader != null)
			{
				ChangeRelationAction.ApplyRelationChangeBetweenHeroes(claimant, claimedSettlement.OwnerClan.Leader, -50, false);
				if (!claimedSettlement.OwnerClan.IsMapFaction)
				{
					ChangeRelationAction.ApplyRelationChangeBetweenHeroes(claimant, claimedSettlement.OwnerClan.Leader, -20, false);
				}
			}
		}

		// Token: 0x06004A59 RID: 19033 RVA: 0x00178557 File Offset: 0x00176757
		public static void Apply(Hero claimant, Settlement claimedSettlement)
		{
			ClaimSettlementAction.ApplyInternal(claimant, claimedSettlement);
		}
	}
}
