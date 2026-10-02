using System;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.Core
{
	// Token: 0x020000C6 RID: 198
	public interface IReadOnlyPropertyOwnerF<T> where T : MBObjectBase
	{
		// Token: 0x06000ADA RID: 2778
		float GetPropertyValue(T attribute);

		// Token: 0x06000ADB RID: 2779
		bool HasProperty(T attribute);
	}
}
