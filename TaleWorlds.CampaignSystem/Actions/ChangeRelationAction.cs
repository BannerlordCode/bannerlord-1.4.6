using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x020004A0 RID: 1184
	public static class ChangeRelationAction
	{
		// Token: 0x06004A42 RID: 19010 RVA: 0x00178150 File Offset: 0x00176350
		private static void ApplyInternal(Hero originalHero, Hero originalGainedRelationWith, int relationChange, bool showQuickNotification, ChangeRelationAction.ChangeRelationDetail detail)
		{
			if (relationChange > 0)
			{
				relationChange = MBRandom.RoundRandomized(Campaign.Current.Models.DiplomacyModel.GetRelationIncreaseFactor(originalHero, originalGainedRelationWith, (float)relationChange));
			}
			if (relationChange != 0)
			{
				Hero hero;
				Hero hero2;
				Campaign.Current.Models.DiplomacyModel.GetHeroesForEffectiveRelation(originalHero, originalGainedRelationWith, out hero, out hero2);
				int num = CharacterRelationManager.GetHeroRelation(hero, hero2) + relationChange;
				num = MBMath.ClampInt(num, -100, 100);
				hero.SetPersonalRelation(hero2, num);
				CampaignEventDispatcher.Instance.OnHeroRelationChanged(hero, hero2, relationChange, showQuickNotification, detail, originalHero, originalGainedRelationWith);
			}
		}

		// Token: 0x06004A43 RID: 19011 RVA: 0x001781CC File Offset: 0x001763CC
		public static void ApplyPlayerRelation(Hero gainedRelationWith, int relation, bool affectRelatives = true, bool showQuickNotification = true)
		{
			ChangeRelationAction.ApplyInternal(Hero.MainHero, gainedRelationWith, relation, showQuickNotification, ChangeRelationAction.ChangeRelationDetail.Default);
		}

		// Token: 0x06004A44 RID: 19012 RVA: 0x001781DC File Offset: 0x001763DC
		public static void ApplyRelationChangeBetweenHeroes(Hero hero, Hero gainedRelationWith, int relationChange, bool showQuickNotification = true)
		{
			ChangeRelationAction.ApplyInternal(hero, gainedRelationWith, relationChange, showQuickNotification, ChangeRelationAction.ChangeRelationDetail.Default);
		}

		// Token: 0x06004A45 RID: 19013 RVA: 0x001781E8 File Offset: 0x001763E8
		public static void ApplyEmissaryRelation(Hero emissary, Hero gainedRelationWith, int relationChange, bool showQuickNotification = true)
		{
			ChangeRelationAction.ApplyInternal(emissary, gainedRelationWith, relationChange, showQuickNotification, ChangeRelationAction.ChangeRelationDetail.Emissary);
		}

		// Token: 0x0200088F RID: 2191
		public enum ChangeRelationDetail
		{
			// Token: 0x04002491 RID: 9361
			Default,
			// Token: 0x04002492 RID: 9362
			Emissary
		}
	}
}
