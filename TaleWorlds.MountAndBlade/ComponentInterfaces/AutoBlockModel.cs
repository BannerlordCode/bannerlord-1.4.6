using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.ComponentInterfaces
{
	// Token: 0x02000401 RID: 1025
	public abstract class AutoBlockModel : MBGameModel<AutoBlockModel>
	{
		// Token: 0x060037B9 RID: 14265
		public abstract Agent.UsageDirection GetBlockDirection(Mission mission);
	}
}
