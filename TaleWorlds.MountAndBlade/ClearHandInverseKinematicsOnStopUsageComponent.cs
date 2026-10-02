using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200036B RID: 875
	public class ClearHandInverseKinematicsOnStopUsageComponent : UsableMissionObjectComponent
	{
		// Token: 0x06003240 RID: 12864 RVA: 0x000CD1C5 File Offset: 0x000CB3C5
		protected internal override void OnUseStopped(Agent userAgent, bool isSuccessful = true)
		{
			userAgent.ClearHandInverseKinematics();
		}
	}
}
