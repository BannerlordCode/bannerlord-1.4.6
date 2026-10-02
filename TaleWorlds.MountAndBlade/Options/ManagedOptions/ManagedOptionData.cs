using System;
using TaleWorlds.Engine.Options;

namespace TaleWorlds.MountAndBlade.Options.ManagedOptions
{
	// Token: 0x0200039B RID: 923
	public abstract class ManagedOptionData : IOptionData
	{
		// Token: 0x060034BB RID: 13499 RVA: 0x000D92C0 File Offset: 0x000D74C0
		protected ManagedOptionData(ManagedOptions.ManagedOptionsType type)
		{
			this.Type = type;
			this._value = ManagedOptions.GetConfig(type);
		}

		// Token: 0x060034BC RID: 13500 RVA: 0x000D92DB File Offset: 0x000D74DB
		public virtual float GetDefaultValue()
		{
			return ManagedOptions.GetDefaultConfig(this.Type);
		}

		// Token: 0x060034BD RID: 13501 RVA: 0x000D92E8 File Offset: 0x000D74E8
		public void Commit()
		{
			if (this._value != ManagedOptions.GetConfig(this.Type))
			{
				ManagedOptions.SetConfig(this.Type, this._value);
			}
		}

		// Token: 0x060034BE RID: 13502 RVA: 0x000D930E File Offset: 0x000D750E
		public float GetValue(bool forceRefresh)
		{
			if (forceRefresh)
			{
				this._value = ManagedOptions.GetConfig(this.Type);
			}
			return this._value;
		}

		// Token: 0x060034BF RID: 13503 RVA: 0x000D932A File Offset: 0x000D752A
		public void SetValue(float value)
		{
			this._value = value;
		}

		// Token: 0x060034C0 RID: 13504 RVA: 0x000D9333 File Offset: 0x000D7533
		public object GetOptionType()
		{
			return this.Type;
		}

		// Token: 0x060034C1 RID: 13505 RVA: 0x000D9340 File Offset: 0x000D7540
		public bool IsNative()
		{
			return false;
		}

		// Token: 0x060034C2 RID: 13506 RVA: 0x000D9343 File Offset: 0x000D7543
		public bool IsAction()
		{
			return false;
		}

		// Token: 0x060034C3 RID: 13507 RVA: 0x000D9348 File Offset: 0x000D7548
		public ValueTuple<string, bool> GetIsDisabledAndReasonID()
		{
			ManagedOptions.ManagedOptionsType type = this.Type;
			if (type - ManagedOptions.ManagedOptionsType.ControlBlockDirection <= 1 && BannerlordConfig.GyroOverrideForAttackDefend)
			{
				return new ValueTuple<string, bool>("str_gyro_overrides_attack_block_direction", true);
			}
			return new ValueTuple<string, bool>(string.Empty, false);
		}

		// Token: 0x04001663 RID: 5731
		public readonly ManagedOptions.ManagedOptionsType Type;

		// Token: 0x04001664 RID: 5732
		private float _value;
	}
}
