using System;
using TaleWorlds.CampaignSystem.ComponentInterfaces;

namespace TaleWorlds.CampaignSystem.GameComponents
{
	// Token: 0x0200010F RID: 271
	public class DefaultDifficultyModel : DifficultyModel
	{
		// Token: 0x0600176F RID: 5999 RVA: 0x0006E3EC File Offset: 0x0006C5EC
		public override float GetPlayerTroopsReceivedDamageMultiplier()
		{
			switch (CampaignOptions.PlayerTroopsReceivedDamage)
			{
			case CampaignOptions.Difficulty.VeryEasy:
				return 0.5f;
			case CampaignOptions.Difficulty.Easy:
				return 0.75f;
			case CampaignOptions.Difficulty.Realistic:
				return 1f;
			default:
				return 1f;
			}
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x0006E42C File Offset: 0x0006C62C
		public override int GetPlayerRecruitSlotBonus()
		{
			switch (CampaignOptions.RecruitmentDifficulty)
			{
			case CampaignOptions.Difficulty.VeryEasy:
				return 2;
			case CampaignOptions.Difficulty.Easy:
				return 1;
			case CampaignOptions.Difficulty.Realistic:
				return 0;
			default:
				return 0;
			}
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x0006E45C File Offset: 0x0006C65C
		public override float GetPlayerMapMovementSpeedBonusMultiplier()
		{
			switch (CampaignOptions.PlayerMapMovementSpeed)
			{
			case CampaignOptions.Difficulty.VeryEasy:
				return 0.1f;
			case CampaignOptions.Difficulty.Easy:
				return 0.05f;
			case CampaignOptions.Difficulty.Realistic:
				return 0f;
			default:
				return 0f;
			}
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x0006E49C File Offset: 0x0006C69C
		public override float GetStealthDifficultyMultiplier()
		{
			switch (CampaignOptions.StealthAndDisguiseDifficulty)
			{
			case CampaignOptions.Difficulty.VeryEasy:
				return 0.5f;
			case CampaignOptions.Difficulty.Easy:
				return 0.75f;
			case CampaignOptions.Difficulty.Realistic:
				return 1f;
			default:
				return 0f;
			}
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x0006E4DC File Offset: 0x0006C6DC
		public override float GetDisguiseDifficultyMultiplier()
		{
			switch (CampaignOptions.StealthAndDisguiseDifficulty)
			{
			case CampaignOptions.Difficulty.VeryEasy:
				return 0.4f;
			case CampaignOptions.Difficulty.Easy:
				return 1f;
			case CampaignOptions.Difficulty.Realistic:
				return 1.2f;
			default:
				return 0f;
			}
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x0006E51C File Offset: 0x0006C71C
		public override float GetCombatAIDifficultyMultiplier()
		{
			switch (CampaignOptions.CombatAIDifficulty)
			{
			case CampaignOptions.Difficulty.VeryEasy:
				return 0f;
			case CampaignOptions.Difficulty.Easy:
				return 0.5f;
			}
			return 1f;
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x0006E554 File Offset: 0x0006C754
		public override float GetPersuasionBonusChance()
		{
			switch (CampaignOptions.PersuasionSuccessChance)
			{
			case CampaignOptions.Difficulty.VeryEasy:
				return 0.1f;
			case CampaignOptions.Difficulty.Easy:
				return 0.05f;
			case CampaignOptions.Difficulty.Realistic:
				return 0f;
			default:
				return 0f;
			}
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x0006E594 File Offset: 0x0006C794
		public override float GetClanMemberDeathChanceMultiplier()
		{
			switch (CampaignOptions.ClanMemberDeathChance)
			{
			case CampaignOptions.Difficulty.VeryEasy:
				return -1f;
			case CampaignOptions.Difficulty.Easy:
				return -0.5f;
			case CampaignOptions.Difficulty.Realistic:
				return 0f;
			default:
				return 0f;
			}
		}
	}
}
