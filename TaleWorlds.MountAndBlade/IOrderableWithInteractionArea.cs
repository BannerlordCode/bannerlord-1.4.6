using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000372 RID: 882
	public interface IOrderableWithInteractionArea : IOrderable
	{
		// Token: 0x06003254 RID: 12884
		bool IsPointInsideInteractionArea(Vec3 point);
	}
}
