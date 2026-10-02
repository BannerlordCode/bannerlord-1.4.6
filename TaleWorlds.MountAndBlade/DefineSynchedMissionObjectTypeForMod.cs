using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002EF RID: 751
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	public sealed class DefineSynchedMissionObjectTypeForMod : Attribute
	{
		// Token: 0x06002AE7 RID: 10983 RVA: 0x000A509B File Offset: 0x000A329B
		public DefineSynchedMissionObjectTypeForMod(Type type)
		{
			this.Type = type;
		}

		// Token: 0x040010BF RID: 4287
		public readonly Type Type;
	}
}
