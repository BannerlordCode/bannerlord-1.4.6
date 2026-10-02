using System;
using TaleWorlds.CampaignSystem.Settlements.Workshops;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x020004B8 RID: 1208
	public static class InitializeWorkshopAction
	{
		// Token: 0x06004AB1 RID: 19121 RVA: 0x00179A28 File Offset: 0x00177C28
		public static void ApplyByNewGame(Workshop workshop, Hero workshopOwner, WorkshopType workshopType)
		{
			workshop.InitializeWorkshop(workshopOwner, workshopType);
			TextObject textObject;
			TextObject textObject2;
			NameGenerator.Current.GenerateHeroNameAndHeroFullName(workshopOwner, out textObject, out textObject2, true);
			workshopOwner.SetName(textObject2, textObject);
			CampaignEventDispatcher.Instance.OnWorkshopInitialized(workshop);
		}
	}
}
