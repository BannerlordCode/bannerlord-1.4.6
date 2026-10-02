using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x02000496 RID: 1174
	public static class BribeGuardsAction
	{
		// Token: 0x06004A17 RID: 18967 RVA: 0x00176F94 File Offset: 0x00175194
		private static void ApplyInternal(Settlement settlement, int gold)
		{
			if (gold > 0)
			{
				if (MBRandom.RandomFloat < (float)gold / 1000f)
				{
					SkillLevelingManager.OnBribeGiven(gold);
				}
				GiveGoldAction.ApplyBetweenCharacters(Hero.MainHero, null, gold, false);
				settlement.BribePaid += gold;
			}
		}

		// Token: 0x06004A18 RID: 18968 RVA: 0x00176FCA File Offset: 0x001751CA
		public static void Apply(Settlement settlement, int gold)
		{
			BribeGuardsAction.ApplyInternal(settlement, gold);
		}
	}
}
