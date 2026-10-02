using System;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x0200041D RID: 1053
	public class NotablePowerManagementBehavior : CampaignBehaviorBase
	{
		// Token: 0x06004351 RID: 17233 RVA: 0x00146348 File Offset: 0x00144548
		public override void RegisterEvents()
		{
			CampaignEvents.HeroCreated.AddNonSerializedListener(this, new Action<Hero, bool>(this.OnHeroCreated));
			CampaignEvents.DailyTickHeroEvent.AddNonSerializedListener(this, new Action<Hero>(this.DailyTickHero));
			CampaignEvents.RaidCompletedEvent.AddNonSerializedListener(this, new Action<BattleSideEnum, RaidEventComponent>(this.OnRaidCompleted));
		}

		// Token: 0x06004352 RID: 17234 RVA: 0x0014639A File Offset: 0x0014459A
		private void OnHeroCreated(Hero hero, bool isMaternal)
		{
			if (hero.IsNotable)
			{
				hero.AddPower((float)Campaign.Current.Models.NotablePowerModel.GetInitialPower(hero));
			}
		}

		// Token: 0x06004353 RID: 17235 RVA: 0x001463C0 File Offset: 0x001445C0
		private void DailyTickHero(Hero hero)
		{
			if (hero.IsAlive && hero.IsNotable)
			{
				hero.AddPower(Campaign.Current.Models.NotablePowerModel.CalculateDailyPowerChangeForHero(hero, false).ResultNumber);
				this.BalanceGoldAndPowerOfNotable(hero);
			}
		}

		// Token: 0x06004354 RID: 17236 RVA: 0x00146408 File Offset: 0x00144608
		private void OnRaidCompleted(BattleSideEnum winnerSide, RaidEventComponent mapEvent)
		{
			foreach (Hero hero in mapEvent.MapEventSettlement.Notables)
			{
				hero.AddPower(-5f);
			}
		}

		// Token: 0x06004355 RID: 17237 RVA: 0x00146464 File Offset: 0x00144664
		private void BalanceGoldAndPowerOfNotable(Hero notable)
		{
			if (notable.Gold > 10500)
			{
				int num = (notable.Gold - 10000) / 500;
				GiveGoldAction.ApplyBetweenCharacters(notable, null, num * 500, true);
				notable.AddPower((float)num);
				return;
			}
			if (notable.Gold < 4500 && notable.Power > 0f)
			{
				int num2 = (5000 - notable.Gold) / 500;
				GiveGoldAction.ApplyBetweenCharacters(null, notable, num2 * 500, true);
				notable.AddPower((float)(-(float)num2));
			}
		}

		// Token: 0x06004356 RID: 17238 RVA: 0x001464EE File Offset: 0x001446EE
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x04001341 RID: 4929
		private const int GoldLimitForNotablesToStartGainingPower = 10000;

		// Token: 0x04001342 RID: 4930
		private const int GoldLimitForNotablesToStartLosingPower = 5000;

		// Token: 0x04001343 RID: 4931
		private const int GoldNeededToGainOnePower = 500;
	}
}
