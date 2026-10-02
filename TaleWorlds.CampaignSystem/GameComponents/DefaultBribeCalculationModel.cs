using System;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.GameComponents
{
	// Token: 0x020000F8 RID: 248
	public class DefaultBribeCalculationModel : BribeCalculationModel
	{
		// Token: 0x0600168C RID: 5772 RVA: 0x000685FC File Offset: 0x000667FC
		public override bool IsBribeNotNeededToEnterKeep(Settlement settlement)
		{
			SettlementAccessModel.AccessDetails accessDetails;
			Campaign.Current.Models.SettlementAccessModel.CanMainHeroEnterLordsHall(settlement, out accessDetails);
			return accessDetails.AccessLevel != SettlementAccessModel.AccessLevel.LimitedAccess || (accessDetails.AccessLevel == SettlementAccessModel.AccessLevel.LimitedAccess && accessDetails.LimitedAccessSolution != SettlementAccessModel.LimitedAccessSolution.Bribe);
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x00068644 File Offset: 0x00066844
		public override bool IsBribeNotNeededToEnterDungeon(Settlement settlement)
		{
			SettlementAccessModel.AccessDetails accessDetails;
			Campaign.Current.Models.SettlementAccessModel.CanMainHeroEnterDungeon(settlement, out accessDetails);
			return accessDetails.AccessLevel != SettlementAccessModel.AccessLevel.LimitedAccess || (accessDetails.AccessLevel == SettlementAccessModel.AccessLevel.LimitedAccess && accessDetails.LimitedAccessSolution != SettlementAccessModel.LimitedAccessSolution.Bribe);
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x0006868A File Offset: 0x0006688A
		private float GetSkillFactor()
		{
			return (1f - (float)Hero.MainHero.GetSkillValue(DefaultSkills.Roguery) / 300f) * 0.65f + 0.35f;
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x000686B4 File Offset: 0x000668B4
		private int GetBribeForCriminalRating(IFaction faction)
		{
			return MathF.Round(Campaign.Current.Models.CrimeModel.GetCost(faction, CrimeModel.PaymentMethod.Gold, 0f)) / 5;
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x000686D8 File Offset: 0x000668D8
		private int GetBaseBribeValue(IFaction faction)
		{
			if (faction.IsAtWarWith(Clan.PlayerClan))
			{
				return 5000;
			}
			if (faction.IsAtWarWith(Hero.MainHero.MapFaction))
			{
				return 3000;
			}
			if (FactionManager.IsNeutralWithFaction(faction, Clan.PlayerClan))
			{
				return 100;
			}
			if (Hero.MainHero.Clan == faction)
			{
				return 0;
			}
			if (Hero.MainHero.MapFaction == faction)
			{
				return 0;
			}
			if (faction is Clan)
			{
				IFaction mapFaction = Hero.MainHero.MapFaction;
				Kingdom kingdom = (faction as Clan).Kingdom;
				return 0;
			}
			return 0;
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x00068760 File Offset: 0x00066960
		public override int GetBribeToEnterLordsHall(Settlement settlement)
		{
			if (this.IsBribeNotNeededToEnterKeep(settlement))
			{
				return 0;
			}
			return this.GetBribeInternal(settlement);
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x00068774 File Offset: 0x00066974
		public override int GetBribeToEnterDungeon(Settlement settlement)
		{
			return this.GetBribeToEnterLordsHall(settlement);
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x00068780 File Offset: 0x00066980
		private int GetBribeInternal(Settlement settlement)
		{
			int num = this.GetBaseBribeValue(settlement.MapFaction);
			num += this.GetBribeForCriminalRating(settlement.MapFaction);
			if (Clan.PlayerClan.Renown < 500f)
			{
				num += (500 - (int)Clan.PlayerClan.Renown) * 15 / 10;
				num = MathF.Max(num, 50);
			}
			num = (int)((float)num * this.GetSkillFactor() / 25f) * 25;
			return MathF.Max(num - settlement.BribePaid, 0);
		}
	}
}
