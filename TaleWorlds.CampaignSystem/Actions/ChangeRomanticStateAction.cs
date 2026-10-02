using System;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x020004A1 RID: 1185
	public static class ChangeRomanticStateAction
	{
		// Token: 0x06004A46 RID: 19014 RVA: 0x001781F4 File Offset: 0x001763F4
		private static void ApplyInternal(Hero hero1, Hero hero2, Romance.RomanceLevelEnum toWhat)
		{
			Romance.SetRomanticState(hero1, hero2, toWhat);
			CampaignEventDispatcher.Instance.OnRomanticStateChanged(hero1, hero2, toWhat);
		}

		// Token: 0x06004A47 RID: 19015 RVA: 0x0017820B File Offset: 0x0017640B
		public static void Apply(Hero person1, Hero person2, Romance.RomanceLevelEnum toWhat)
		{
			ChangeRomanticStateAction.ApplyInternal(person1, person2, toWhat);
		}
	}
}
