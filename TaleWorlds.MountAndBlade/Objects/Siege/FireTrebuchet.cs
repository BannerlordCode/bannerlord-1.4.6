using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Objects.Siege
{
	// Token: 0x020003AE RID: 942
	public class FireTrebuchet : Trebuchet
	{
		// Token: 0x0600353E RID: 13630 RVA: 0x000DAF26 File Offset: 0x000D9126
		public override SiegeEngineType GetSiegeEngineType()
		{
			return DefaultSiegeEngineTypes.FireTrebuchet;
		}

		// Token: 0x0600353F RID: 13631 RVA: 0x000DAF30 File Offset: 0x000D9130
		public override float ProcessTargetValue(float baseValue, TargetFlags flags)
		{
			if (flags.HasAnyFlag(TargetFlags.NotAThreat))
			{
				return -1000f;
			}
			if (flags.HasAllFlags(TargetFlags.IsFlammable | TargetFlags.IsSiegeEngine))
			{
				baseValue *= 1.5f;
			}
			if (flags.HasAnyFlag(TargetFlags.IsSiegeEngine))
			{
				baseValue *= 2f;
			}
			if (flags.HasAnyFlag(TargetFlags.IsStructure))
			{
				baseValue *= 1.5f;
			}
			if (flags.HasAnyFlag(TargetFlags.IsSmall))
			{
				baseValue *= 0.5f;
			}
			if (flags.HasAnyFlag(TargetFlags.IsMoving))
			{
				baseValue *= 0.8f;
			}
			if (flags.HasAnyFlag(TargetFlags.DebugThreat))
			{
				baseValue *= 10000f;
			}
			return baseValue;
		}
	}
}
