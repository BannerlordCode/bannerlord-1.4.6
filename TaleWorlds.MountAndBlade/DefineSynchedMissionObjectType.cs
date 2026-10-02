using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002F0 RID: 752
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	internal sealed class DefineSynchedMissionObjectType : Attribute
	{
		// Token: 0x06002AE8 RID: 10984 RVA: 0x000A50AA File Offset: 0x000A32AA
		public DefineSynchedMissionObjectType(Type type)
		{
			this.Type = type;
		}

		// Token: 0x040010C0 RID: 4288
		public readonly Type Type;
	}
}
