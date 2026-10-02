using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x020000A1 RID: 161
	public static class EngineExtensions
	{
		// Token: 0x06000F17 RID: 3863 RVA: 0x00011B15 File Offset: 0x0000FD15
		public static WorldPosition ToWorldPosition(this Vec3 vec3, Scene scene)
		{
			return new WorldPosition(scene, UIntPtr.Zero, vec3, false);
		}
	}
}
