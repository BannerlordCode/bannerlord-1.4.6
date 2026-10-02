using System;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.Core
{
	// Token: 0x020000C5 RID: 197
	public interface IReadOnlyPropertyOwner<T> where T : MBObjectBase
	{
		// Token: 0x06000AD8 RID: 2776
		int GetPropertyValue(T attribute);

		// Token: 0x06000AD9 RID: 2777
		bool HasProperty(T attribute);
	}
}
