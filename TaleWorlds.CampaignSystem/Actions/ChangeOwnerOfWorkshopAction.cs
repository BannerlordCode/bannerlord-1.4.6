using System;
using TaleWorlds.CampaignSystem.Settlements.Workshops;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x0200049D RID: 1181
	public static class ChangeOwnerOfWorkshopAction
	{
		// Token: 0x06004A39 RID: 19001 RVA: 0x00177D78 File Offset: 0x00175F78
		private static void ApplyInternal(Workshop workshop, Hero newOwner, WorkshopType workshopType, int capital, int cost)
		{
			Hero owner = workshop.Owner;
			workshop.ChangeOwnerOfWorkshop(newOwner, workshopType, capital);
			if (newOwner == Hero.MainHero)
			{
				GiveGoldAction.ApplyBetweenCharacters(newOwner, owner, cost, false);
			}
			if (owner == Hero.MainHero)
			{
				GiveGoldAction.ApplyBetweenCharacters(null, Hero.MainHero, cost, false);
			}
			CampaignEventDispatcher.Instance.OnWorkshopOwnerChanged(workshop, owner);
		}

		// Token: 0x06004A3A RID: 19002 RVA: 0x00177DC9 File Offset: 0x00175FC9
		public static void ApplyByBankruptcy(Workshop workshop, Hero newOwner, WorkshopType workshopType, int cost)
		{
			ChangeOwnerOfWorkshopAction.ApplyInternal(workshop, newOwner, workshopType, Campaign.Current.Models.WorkshopModel.InitialCapital, cost);
		}

		// Token: 0x06004A3B RID: 19003 RVA: 0x00177DE8 File Offset: 0x00175FE8
		public static void ApplyByPlayerBuying(Workshop workshop)
		{
			int costForPlayer = Campaign.Current.Models.WorkshopModel.GetCostForPlayer(workshop);
			ChangeOwnerOfWorkshopAction.ApplyInternal(workshop, Hero.MainHero, workshop.WorkshopType, Campaign.Current.Models.WorkshopModel.InitialCapital, costForPlayer);
		}

		// Token: 0x06004A3C RID: 19004 RVA: 0x00177E34 File Offset: 0x00176034
		public static void ApplyByPlayerSelling(Workshop workshop, Hero newOwner, WorkshopType workshopType)
		{
			int costForNotable = Campaign.Current.Models.WorkshopModel.GetCostForNotable(workshop);
			ChangeOwnerOfWorkshopAction.ApplyInternal(workshop, newOwner, workshopType, Campaign.Current.Models.WorkshopModel.InitialCapital, costForNotable);
		}

		// Token: 0x06004A3D RID: 19005 RVA: 0x00177E74 File Offset: 0x00176074
		public static void ApplyByDeath(Workshop workshop, Hero newOwner)
		{
			ChangeOwnerOfWorkshopAction.ApplyInternal(workshop, newOwner, workshop.WorkshopType, workshop.Capital, 0);
		}

		// Token: 0x06004A3E RID: 19006 RVA: 0x00177E8A File Offset: 0x0017608A
		public static void ApplyByWar(Workshop workshop, Hero newOwner, WorkshopType workshopType)
		{
			ChangeOwnerOfWorkshopAction.ApplyInternal(workshop, newOwner, workshopType, Campaign.Current.Models.WorkshopModel.InitialCapital, 0);
		}
	}
}
