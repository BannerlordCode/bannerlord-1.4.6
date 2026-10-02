using System;

namespace TaleWorlds.Core
{
	// Token: 0x020000E0 RID: 224
	public static class WeaponComponentDataExtensions
	{
		// Token: 0x06000B74 RID: 2932 RVA: 0x000252A3 File Offset: 0x000234A3
		public static int GetModifiedThrustDamage(this WeaponComponentData componentData, ItemModifier itemModifier)
		{
			if (itemModifier != null && componentData.ThrustDamage > 0)
			{
				return itemModifier.ModifyDamage(componentData.ThrustDamage);
			}
			return componentData.ThrustDamage;
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x000252C4 File Offset: 0x000234C4
		public static int GetModifiedSwingDamage(this WeaponComponentData componentData, ItemModifier itemModifier)
		{
			if (itemModifier != null && componentData.SwingDamage > 0)
			{
				return itemModifier.ModifyDamage(componentData.SwingDamage);
			}
			return componentData.SwingDamage;
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x000252E5 File Offset: 0x000234E5
		public static int GetModifiedMissileDamage(this WeaponComponentData componentData, ItemModifier itemModifier)
		{
			if (itemModifier != null && componentData.MissileDamage > 0)
			{
				return itemModifier.ModifyDamage(componentData.MissileDamage);
			}
			return componentData.MissileDamage;
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x00025306 File Offset: 0x00023506
		public static int GetModifiedThrustSpeed(this WeaponComponentData componentData, ItemModifier itemModifier)
		{
			if (itemModifier != null && componentData.ThrustSpeed > 0)
			{
				return itemModifier.ModifySpeed(componentData.ThrustSpeed);
			}
			return componentData.ThrustSpeed;
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x00025327 File Offset: 0x00023527
		public static int GetModifiedSwingSpeed(this WeaponComponentData componentData, ItemModifier itemModifier)
		{
			if (itemModifier != null && componentData.SwingSpeed > 0)
			{
				return itemModifier.ModifySpeed(componentData.SwingSpeed);
			}
			return componentData.SwingSpeed;
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x00025348 File Offset: 0x00023548
		public static int GetModifiedMissileSpeed(this WeaponComponentData componentData, ItemModifier itemModifier)
		{
			if (itemModifier != null && componentData.MissileSpeed > 0)
			{
				return itemModifier.ModifyMissileSpeed(componentData.MissileSpeed);
			}
			return componentData.MissileSpeed;
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00025369 File Offset: 0x00023569
		public static int GetModifiedHandling(this WeaponComponentData componentData, ItemModifier itemModifier)
		{
			if (itemModifier != null && componentData.Handling > 0)
			{
				return itemModifier.ModifySpeed(componentData.Handling);
			}
			return componentData.Handling;
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0002538A File Offset: 0x0002358A
		public static short GetModifiedStackCount(this WeaponComponentData componentData, ItemModifier itemModifier)
		{
			if (itemModifier != null && componentData.MaxDataValue > 0)
			{
				return itemModifier.ModifyStackCount(componentData.MaxDataValue);
			}
			return componentData.MaxDataValue;
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x000253AB File Offset: 0x000235AB
		public static short GetModifiedMaximumHitPoints(this WeaponComponentData componentData, ItemModifier itemModifier)
		{
			if (itemModifier != null && componentData.MaxDataValue > 0)
			{
				return itemModifier.ModifyHitPoints(componentData.MaxDataValue);
			}
			return componentData.MaxDataValue;
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x000253CC File Offset: 0x000235CC
		public static int GetModifiedArmor(this WeaponComponentData componentData, ItemModifier itemModifier)
		{
			if (itemModifier != null && componentData.BodyArmor > 0)
			{
				return itemModifier.ModifyArmor(componentData.BodyArmor);
			}
			return componentData.BodyArmor;
		}
	}
}
