using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000266 RID: 614
	public interface IVehicleHandler : IMissionBehavior
	{
		// Token: 0x0600226B RID: 8811
		bool IsAgentInVehicle(Agent agent, out WeakGameEntity vehicleEntity);
	}
}
