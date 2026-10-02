using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.ComponentInterfaces
{
	// Token: 0x020003F3 RID: 1011
	public class DefaultSiegeEngineCalculationModel : MissionSiegeEngineCalculationModel
	{
		// Token: 0x06003745 RID: 14149 RVA: 0x000E4BD3 File Offset: 0x000E2DD3
		public override float CalculateReloadSpeed(Agent userAgent, float baseSpeed)
		{
			return baseSpeed;
		}

		// Token: 0x06003746 RID: 14150 RVA: 0x000E4BD6 File Offset: 0x000E2DD6
		public override int CalculateShipSiegeWeaponAmmoCount(IShipOrigin shipOrigin, Agent captain, RangedSiegeWeapon weapon)
		{
			return weapon.AmmoCount;
		}

		// Token: 0x06003747 RID: 14151 RVA: 0x000E4BDE File Offset: 0x000E2DDE
		public override int CalculateDamage(Agent attackerAgent, float baseDamage)
		{
			return (int)baseDamage;
		}
	}
}
