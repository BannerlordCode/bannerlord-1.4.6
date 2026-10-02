using System;
using Helpers;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Buildings;

namespace TaleWorlds.CampaignSystem.GameComponents
{
	// Token: 0x0200010C RID: 268
	public class DefaultDailyTroopXpBonusModel : DailyTroopXpBonusModel
	{
		// Token: 0x06001764 RID: 5988 RVA: 0x0006E1C0 File Offset: 0x0006C3C0
		public override int CalculateDailyTroopXpBonus(Town town)
		{
			return this.CalculateTroopXpBonusInternal(town);
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x0006E1CC File Offset: 0x0006C3CC
		private int CalculateTroopXpBonusInternal(Town town)
		{
			ExplainedNumber explainedNumber = new ExplainedNumber(0f, false, null);
			town.AddEffectOfBuildings(BuildingEffectEnum.ExperiencePerDay, ref explainedNumber);
			PerkHelper.AddPerkBonusForTown(DefaultPerks.Leadership.RaiseTheMeek, town, ref explainedNumber);
			PerkHelper.AddPerkBonusForTown(DefaultPerks.TwoHanded.ProjectileDeflection, town, ref explainedNumber);
			return (int)explainedNumber.ResultNumber;
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x0006E213 File Offset: 0x0006C413
		public override float CalculateGarrisonXpBonusMultiplier(Town town)
		{
			return 1f;
		}
	}
}
