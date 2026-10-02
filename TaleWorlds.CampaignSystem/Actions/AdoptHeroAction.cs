using System;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x02000492 RID: 1170
	public static class AdoptHeroAction
	{
		// Token: 0x06004A08 RID: 18952 RVA: 0x001763F1 File Offset: 0x001745F1
		private static void ApplyInternal(Hero adoptedHero)
		{
			if (Hero.MainHero.IsFemale)
			{
				adoptedHero.Mother = Hero.MainHero;
			}
			else
			{
				adoptedHero.Father = Hero.MainHero;
			}
			adoptedHero.Clan = Clan.PlayerClan;
		}

		// Token: 0x06004A09 RID: 18953 RVA: 0x00176422 File Offset: 0x00174622
		public static void Apply(Hero adoptedHero)
		{
			AdoptHeroAction.ApplyInternal(adoptedHero);
		}
	}
}
