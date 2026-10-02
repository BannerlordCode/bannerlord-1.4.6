using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000261 RID: 609
	public interface IFlagRemoved : IMissionBehavior
	{
		// Token: 0x0600225D RID: 8797
		void OnFlagsRemoved(int remainingFlagIndex);
	}
}
