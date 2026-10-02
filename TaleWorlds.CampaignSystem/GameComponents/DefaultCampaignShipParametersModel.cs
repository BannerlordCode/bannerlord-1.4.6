using System;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Naval;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.GameComponents
{
	// Token: 0x020000FE RID: 254
	public class DefaultCampaignShipParametersModel : CampaignShipParametersModel
	{
		// Token: 0x060016AB RID: 5803 RVA: 0x0006905C File Offset: 0x0006725C
		public override float GetShipSizeWeatherFactor(ShipHull shipHull)
		{
			return 0f;
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x00069063 File Offset: 0x00067263
		public override float GetDefaultCombatFactor(ShipHull shipHull)
		{
			return 0f;
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x0006906A File Offset: 0x0006726A
		public override float GetCampaignSpeedBonusFactor(Ship ship)
		{
			return 0f;
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x00069071 File Offset: 0x00067271
		public override float GetCrewCapacityBonusFactor(Ship ship)
		{
			return 0f;
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x00069078 File Offset: 0x00067278
		public override float GetShipWeightFactor(Ship ship)
		{
			return 0f;
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x0006907F File Offset: 0x0006727F
		public override float GetForwardDragFactor(Ship ship)
		{
			return 0f;
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x00069086 File Offset: 0x00067286
		public override float GetCrewShieldHitPointsFactor(Ship ship)
		{
			return 0f;
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x0006908D File Offset: 0x0006728D
		public override int GetAdditionalAmmoBonus(Ship ship)
		{
			return 0;
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x00069090 File Offset: 0x00067290
		public override float GetMaxOarPowerFactor(Ship ship)
		{
			return 0f;
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x00069097 File Offset: 0x00067297
		public override float GetMaxOarForceFactor(Ship ship)
		{
			return 0f;
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x0006909E File Offset: 0x0006729E
		public override float GetSailForceFactor(Ship ship)
		{
			return 0f;
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x000690A5 File Offset: 0x000672A5
		public override float GetCrewMeleeDamageFactor(Ship ship)
		{
			return 0f;
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x000690AC File Offset: 0x000672AC
		public override int GetAdditionalArcherQuivers(Ship ship)
		{
			return 0;
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x000690AF File Offset: 0x000672AF
		public override int GetAdditionalThrowingWeaponStack(Ship ship)
		{
			return 0;
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x000690B2 File Offset: 0x000672B2
		public override float GetSailRotationSpeedFactor(Ship ship)
		{
			return 0f;
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x000690B9 File Offset: 0x000672B9
		public override float GetFurlUnfurlSpeedFactor(Ship ship)
		{
			return 0f;
		}
	}
}
