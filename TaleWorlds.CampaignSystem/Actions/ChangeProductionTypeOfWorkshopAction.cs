using System;
using TaleWorlds.CampaignSystem.Settlements.Workshops;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x0200049F RID: 1183
	public static class ChangeProductionTypeOfWorkshopAction
	{
		// Token: 0x06004A41 RID: 19009 RVA: 0x00178100 File Offset: 0x00176300
		public static void Apply(Workshop workshop, WorkshopType newWorkshopType, bool ignoreCost = false)
		{
			int num = (ignoreCost ? 0 : Campaign.Current.Models.WorkshopModel.GetConvertProductionCost(newWorkshopType));
			workshop.ChangeWorkshopProduction(newWorkshopType);
			if (num > 0)
			{
				GiveGoldAction.ApplyBetweenCharacters(workshop.Owner, null, num, false);
			}
			CampaignEventDispatcher.Instance.OnWorkshopTypeChanged(workshop);
		}
	}
}
