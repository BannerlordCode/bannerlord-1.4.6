using System;
using System.Runtime.InteropServices;
using TaleWorlds.Core;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200024F RID: 591
	[EngineStruct("Killing_blow", false, null)]
	public struct KillingBlow
	{
		// Token: 0x060021A2 RID: 8610 RVA: 0x00075A88 File Offset: 0x00073C88
		public KillingBlow(Blow b, Vec3 ragdollImpulsePoint, Vec3 ragdollImpulseAmount, int deathAction, int weaponItemKind, Agent.KillInfo overrideKillInfo = Agent.KillInfo.Invalid)
		{
			this.RagdollImpulseLocalPoint = ragdollImpulsePoint;
			this.RagdollImpulseAmount = ragdollImpulseAmount;
			this.DeathAction = deathAction;
			this.OverrideKillInfo = overrideKillInfo;
			this.DamageType = b.DamageType;
			this.AttackType = b.AttackType;
			this.OwnerId = b.OwnerId;
			this.VictimBodyPart = b.VictimBodyPart;
			this.WeaponClass = (int)b.WeaponRecord.WeaponClass;
			this.BlowPosition = b.GlobalPosition;
			this.WeaponRecordWeaponFlags = b.WeaponRecord.WeaponFlags;
			this.WeaponItemKind = weaponItemKind;
			this.InflictedDamage = b.InflictedDamage;
			this.IsMissile = b.IsMissile;
			this.IsValid = true;
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x00075B39 File Offset: 0x00073D39
		public bool IsHeadShot()
		{
			return this.VictimBodyPart == BoneBodyPartType.Head;
		}

		// Token: 0x04000CE9 RID: 3305
		public Vec3 RagdollImpulseLocalPoint;

		// Token: 0x04000CEA RID: 3306
		public Vec3 RagdollImpulseAmount;

		// Token: 0x04000CEB RID: 3307
		public int DeathAction;

		// Token: 0x04000CEC RID: 3308
		public DamageTypes DamageType;

		// Token: 0x04000CED RID: 3309
		public AgentAttackType AttackType;

		// Token: 0x04000CEE RID: 3310
		public int OwnerId;

		// Token: 0x04000CEF RID: 3311
		public BoneBodyPartType VictimBodyPart;

		// Token: 0x04000CF0 RID: 3312
		public int WeaponClass;

		// Token: 0x04000CF1 RID: 3313
		public Agent.KillInfo OverrideKillInfo;

		// Token: 0x04000CF2 RID: 3314
		public Vec3 BlowPosition;

		// Token: 0x04000CF3 RID: 3315
		public WeaponFlags WeaponRecordWeaponFlags;

		// Token: 0x04000CF4 RID: 3316
		public int WeaponItemKind;

		// Token: 0x04000CF5 RID: 3317
		public int InflictedDamage;

		// Token: 0x04000CF6 RID: 3318
		[MarshalAs(UnmanagedType.U1)]
		public bool IsMissile;

		// Token: 0x04000CF7 RID: 3319
		[MarshalAs(UnmanagedType.U1)]
		public bool IsValid;
	}
}
