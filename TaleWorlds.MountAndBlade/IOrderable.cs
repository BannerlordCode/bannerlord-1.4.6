using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000371 RID: 881
	public interface IOrderable
	{
		// Token: 0x06003253 RID: 12883
		OrderType GetOrder(BattleSideEnum side);
	}
}
