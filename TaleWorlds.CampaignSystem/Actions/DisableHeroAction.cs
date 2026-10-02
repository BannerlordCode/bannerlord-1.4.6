using System;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x020004AB RID: 1195
	public static class DisableHeroAction
	{
		// Token: 0x06004A70 RID: 19056 RVA: 0x00178BD4 File Offset: 0x00176DD4
		private static void ApplyInternal(Hero hero)
		{
			if (hero.IsAlive)
			{
				if (hero.PartyBelongedTo != null)
				{
					if (hero.PartyBelongedTo.LeaderHero == hero)
					{
						DestroyPartyAction.Apply(null, hero.PartyBelongedTo);
					}
					else
					{
						hero.PartyBelongedTo.MemberRoster.RemoveTroop(hero.CharacterObject, 1, default(UniqueTroopDescriptor), 0);
					}
				}
				if (hero.StayingInSettlement != null)
				{
					hero.ChangeState(Hero.CharacterStates.Disabled);
					hero.StayingInSettlement = null;
				}
				if (hero.CurrentSettlement != null)
				{
					LeaveSettlementAction.ApplyForCharacterOnly(hero);
				}
				if (hero.IsPrisoner)
				{
					EndCaptivityAction.ApplyByEscape(hero, null, true);
				}
				hero.ChangeState(Hero.CharacterStates.Disabled);
			}
		}

		// Token: 0x06004A71 RID: 19057 RVA: 0x00178C6C File Offset: 0x00176E6C
		public static void Apply(Hero hero)
		{
			DisableHeroAction.ApplyInternal(hero);
		}
	}
}
