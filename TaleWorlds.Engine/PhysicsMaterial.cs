using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.Engine
{
	// Token: 0x02000078 RID: 120
	[EngineStruct("int", false, null)]
	public readonly struct PhysicsMaterial
	{
		// Token: 0x06000A98 RID: 2712 RVA: 0x0000ADE7 File Offset: 0x00008FE7
		internal PhysicsMaterial(int index)
		{
			this = default(PhysicsMaterial);
			this.Index = index;
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x0000ADF7 File Offset: 0x00008FF7
		public bool IsValid
		{
			get
			{
				return this.Index >= 0;
			}
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0000AE05 File Offset: 0x00009005
		public PhysicsMaterialFlags GetFlags()
		{
			return PhysicsMaterial.GetFlagsAtIndex(this.Index);
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0000AE12 File Offset: 0x00009012
		public float GetDynamicFriction()
		{
			return PhysicsMaterial.GetDynamicFrictionAtIndex(this.Index);
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0000AE1F File Offset: 0x0000901F
		public float GetStaticFriction()
		{
			return PhysicsMaterial.GetStaticFrictionAtIndex(this.Index);
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0000AE2C File Offset: 0x0000902C
		public float GetRestitution()
		{
			return PhysicsMaterial.GetRestitutionAtIndex(this.Index);
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0000AE39 File Offset: 0x00009039
		public float GetLinearDamping()
		{
			return PhysicsMaterial.GetLinearDampingAtIndex(this.Index);
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0000AE46 File Offset: 0x00009046
		public float GetAngularDamping()
		{
			return PhysicsMaterial.GetAngularDampingAtIndex(this.Index);
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x0000AE53 File Offset: 0x00009053
		public string Name
		{
			get
			{
				return PhysicsMaterial.GetNameAtIndex(this.Index);
			}
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0000AE60 File Offset: 0x00009060
		public bool Equals(PhysicsMaterial m)
		{
			return this.Index == m.Index;
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0000AE70 File Offset: 0x00009070
		public static int GetMaterialCount()
		{
			return EngineApplicationInterface.IPhysicsMaterial.GetMaterialCount();
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0000AE7C File Offset: 0x0000907C
		public static PhysicsMaterial GetFromName(string id)
		{
			return EngineApplicationInterface.IPhysicsMaterial.GetIndexWithName(id);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0000AE89 File Offset: 0x00009089
		public static string GetNameAtIndex(int index)
		{
			return EngineApplicationInterface.IPhysicsMaterial.GetMaterialNameAtIndex(index);
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0000AE96 File Offset: 0x00009096
		public static PhysicsMaterialFlags GetFlagsAtIndex(int index)
		{
			return EngineApplicationInterface.IPhysicsMaterial.GetFlagsAtIndex(index);
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0000AEA3 File Offset: 0x000090A3
		public static float GetRestitutionAtIndex(int index)
		{
			return EngineApplicationInterface.IPhysicsMaterial.GetRestitutionAtIndex(index);
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0000AEB0 File Offset: 0x000090B0
		public static float GetDynamicFrictionAtIndex(int index)
		{
			return EngineApplicationInterface.IPhysicsMaterial.GetDynamicFrictionAtIndex(index);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0000AEBD File Offset: 0x000090BD
		public static float GetStaticFrictionAtIndex(int index)
		{
			return EngineApplicationInterface.IPhysicsMaterial.GetStaticFrictionAtIndex(index);
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0000AECA File Offset: 0x000090CA
		public static float GetLinearDampingAtIndex(int index)
		{
			return EngineApplicationInterface.IPhysicsMaterial.GetLinearDampingAtIndex(index);
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0000AED7 File Offset: 0x000090D7
		public static float GetAngularDampingAtIndex(int index)
		{
			return EngineApplicationInterface.IPhysicsMaterial.GetAngularDampingAtIndex(index);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0000AEE4 File Offset: 0x000090E4
		public static PhysicsMaterial GetFromIndex(int index)
		{
			return new PhysicsMaterial(index);
		}

		// Token: 0x0400016A RID: 362
		[CustomEngineStructMemberData("ignoredMember", true)]
		public readonly int Index;

		// Token: 0x0400016B RID: 363
		public static readonly PhysicsMaterial InvalidPhysicsMaterial = new PhysicsMaterial(-1);
	}
}
