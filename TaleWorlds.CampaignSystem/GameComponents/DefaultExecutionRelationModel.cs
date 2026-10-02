using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.ComponentInterfaces;

namespace TaleWorlds.CampaignSystem.GameComponents
{
	// Token: 0x02000116 RID: 278
	public class DefaultExecutionRelationModel : ExecutionRelationModel
	{
		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x060017FF RID: 6143 RVA: 0x00073630 File Offset: 0x00071830
		public override int HeroKillingHeroClanRelationPenalty
		{
			get
			{
				return -40;
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001800 RID: 6144 RVA: 0x00073634 File Offset: 0x00071834
		public override int HeroKillingHeroFriendRelationPenalty
		{
			get
			{
				return -10;
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001801 RID: 6145 RVA: 0x00073638 File Offset: 0x00071838
		public override int PlayerExecutingHeroFactionRelationPenaltyDishonorable
		{
			get
			{
				return -5;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001802 RID: 6146 RVA: 0x0007363C File Offset: 0x0007183C
		public override int PlayerExecutingHeroClanRelationPenaltyDishonorable
		{
			get
			{
				return -30;
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x00073640 File Offset: 0x00071840
		public override int PlayerExecutingHeroFriendRelationPenaltyDishonorable
		{
			get
			{
				return -15;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001804 RID: 6148 RVA: 0x00073644 File Offset: 0x00071844
		public override int PlayerExecutingHeroHonorPenalty
		{
			get
			{
				return -1000;
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001805 RID: 6149 RVA: 0x0007364B File Offset: 0x0007184B
		public override int PlayerExecutingHeroFactionRelationPenalty
		{
			get
			{
				return -10;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001806 RID: 6150 RVA: 0x0007364F File Offset: 0x0007184F
		public override int PlayerExecutingHeroHonorableNobleRelationPenalty
		{
			get
			{
				return -10;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001807 RID: 6151 RVA: 0x00073653 File Offset: 0x00071853
		public override int PlayerExecutingHeroClanRelationPenalty
		{
			get
			{
				return -60;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001808 RID: 6152 RVA: 0x00073657 File Offset: 0x00071857
		public override int PlayerExecutingHeroFriendRelationPenalty
		{
			get
			{
				return -30;
			}
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x0007365C File Offset: 0x0007185C
		public override int GetRelationChangeForExecutingHero(Hero victim, Hero hero, out bool showQuickNotification)
		{
			int num = 0;
			showQuickNotification = false;
			if (victim.GetTraitLevel(DefaultTraits.Honor) < 0)
			{
				if (!hero.IsHumanPlayerCharacter && hero != victim && hero.Clan != null && hero.Clan.Leader == hero)
				{
					if (hero.Clan == victim.Clan)
					{
						num = Campaign.Current.Models.ExecutionRelationModel.PlayerExecutingHeroClanRelationPenaltyDishonorable;
						showQuickNotification = true;
					}
					else if (victim.IsFriend(hero))
					{
						num = Campaign.Current.Models.ExecutionRelationModel.PlayerExecutingHeroFriendRelationPenaltyDishonorable;
						showQuickNotification = true;
					}
					else if (hero.MapFaction == victim.MapFaction && hero.CharacterObject.Occupation == Occupation.Lord)
					{
						num = Campaign.Current.Models.ExecutionRelationModel.PlayerExecutingHeroFactionRelationPenaltyDishonorable;
						showQuickNotification = true;
					}
				}
			}
			else if (!hero.IsHumanPlayerCharacter && hero != victim && hero.Clan != null && hero.Clan.Leader == hero)
			{
				if (hero.Clan == victim.Clan)
				{
					num = Campaign.Current.Models.ExecutionRelationModel.PlayerExecutingHeroClanRelationPenalty;
					showQuickNotification = true;
				}
				else if (victim.IsFriend(hero))
				{
					num = Campaign.Current.Models.ExecutionRelationModel.PlayerExecutingHeroFriendRelationPenalty;
					showQuickNotification = true;
				}
				else if (hero.MapFaction == victim.MapFaction && hero.CharacterObject.Occupation == Occupation.Lord)
				{
					num = Campaign.Current.Models.ExecutionRelationModel.PlayerExecutingHeroFactionRelationPenalty;
					showQuickNotification = false;
				}
				else if (hero.GetTraitLevel(DefaultTraits.Honor) > 0 && !victim.Clan.IsRebelClan)
				{
					num = Campaign.Current.Models.ExecutionRelationModel.PlayerExecutingHeroHonorableNobleRelationPenalty;
					showQuickNotification = true;
				}
			}
			return num;
		}
	}
}
