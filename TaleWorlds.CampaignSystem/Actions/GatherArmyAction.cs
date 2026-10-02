using System;
using TaleWorlds.CampaignSystem.Map;
using TaleWorlds.CampaignSystem.Party;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x020004B4 RID: 1204
	public static class GatherArmyAction
	{
		// Token: 0x06004AA1 RID: 19105 RVA: 0x001795E4 File Offset: 0x001777E4
		private static void ApplyInternal(MobileParty leaderParty, IMapPoint gatheringPoint, float playerInvolvement = 0f)
		{
			Army army = leaderParty.Army;
			CampaignEventDispatcher.Instance.OnArmyGathered(army, gatheringPoint);
		}

		// Token: 0x06004AA2 RID: 19106 RVA: 0x00179604 File Offset: 0x00177804
		public static void Apply(MobileParty leaderParty, IMapPoint gatheringPoint)
		{
			GatherArmyAction.ApplyInternal(leaderParty, gatheringPoint, (leaderParty == MobileParty.MainParty) ? 1f : 0f);
		}
	}
}
