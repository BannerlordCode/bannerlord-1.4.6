using System;
using TaleWorlds.Engine.Options;

namespace TaleWorlds.MountAndBlade.Options.ManagedOptions
{
	// Token: 0x0200039A RID: 922
	public class ManagedNumericOptionData : ManagedOptionData, INumericOptionData, IOptionData
	{
		// Token: 0x060034B4 RID: 13492 RVA: 0x000D91A0 File Offset: 0x000D73A0
		public ManagedNumericOptionData(ManagedOptions.ManagedOptionsType type)
			: base(type)
		{
			this._minValue = ManagedNumericOptionData.GetLimitValue(this.Type, true);
			this._maxValue = ManagedNumericOptionData.GetLimitValue(this.Type, false);
		}

		// Token: 0x060034B5 RID: 13493 RVA: 0x000D91CD File Offset: 0x000D73CD
		public float GetMinValue()
		{
			return this._minValue;
		}

		// Token: 0x060034B6 RID: 13494 RVA: 0x000D91D5 File Offset: 0x000D73D5
		public float GetMaxValue()
		{
			return this._maxValue;
		}

		// Token: 0x060034B7 RID: 13495 RVA: 0x000D91E0 File Offset: 0x000D73E0
		private static float GetLimitValue(ManagedOptions.ManagedOptionsType type, bool isMin)
		{
			if (type <= ManagedOptions.ManagedOptionsType.AutoSaveInterval)
			{
				if (type == ManagedOptions.ManagedOptionsType.BattleSize)
				{
					return (float)(isMin ? BannerlordConfig.MinBattleSize : BannerlordConfig.MaxBattleSize);
				}
				if (type == ManagedOptions.ManagedOptionsType.AutoSaveInterval)
				{
					if (!isMin)
					{
						return 60f;
					}
					return 4f;
				}
			}
			else if (type != ManagedOptions.ManagedOptionsType.FirstPersonFov)
			{
				if (type != ManagedOptions.ManagedOptionsType.CombatCameraDistance)
				{
					if (type == ManagedOptions.ManagedOptionsType.UIScale)
					{
						if (!isMin)
						{
							return 1f;
						}
						return 0.75f;
					}
				}
				else
				{
					if (!isMin)
					{
						return 2.4f;
					}
					return 0.7f;
				}
			}
			else
			{
				if (!isMin)
				{
					return 100f;
				}
				return 45f;
			}
			if (!isMin)
			{
				return 1f;
			}
			return 0f;
		}

		// Token: 0x060034B8 RID: 13496 RVA: 0x000D926C File Offset: 0x000D746C
		public bool GetIsDiscrete()
		{
			ManagedOptions.ManagedOptionsType type = this.Type;
			if (type <= ManagedOptions.ManagedOptionsType.AutoSaveInterval)
			{
				if (type != ManagedOptions.ManagedOptionsType.BattleSize && type != ManagedOptions.ManagedOptionsType.AutoSaveInterval)
				{
					return false;
				}
			}
			else if (type != ManagedOptions.ManagedOptionsType.FirstPersonFov)
			{
				if (type != ManagedOptions.ManagedOptionsType.UIScale)
				{
					return false;
				}
				return false;
			}
			return true;
		}

		// Token: 0x060034B9 RID: 13497 RVA: 0x000D929F File Offset: 0x000D749F
		public int GetDiscreteIncrementInterval()
		{
			return 1;
		}

		// Token: 0x060034BA RID: 13498 RVA: 0x000D92A4 File Offset: 0x000D74A4
		public bool GetShouldUpdateContinuously()
		{
			ManagedOptions.ManagedOptionsType type = this.Type;
			return type != ManagedOptions.ManagedOptionsType.UIScale;
		}

		// Token: 0x04001661 RID: 5729
		private readonly float _minValue;

		// Token: 0x04001662 RID: 5730
		private readonly float _maxValue;
	}
}
