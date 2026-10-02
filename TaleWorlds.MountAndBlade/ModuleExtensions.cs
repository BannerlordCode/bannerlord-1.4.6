using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002DA RID: 730
	public static class ModuleExtensions
	{
		// Token: 0x06002A8B RID: 10891 RVA: 0x000A36C8 File Offset: 0x000A18C8
		public static IEnumerable<UsableMachine> GetUsedMachines(this Formation formation)
		{
			return from d in formation.Detachments
				select d as UsableMachine into u
				where u != null
				select u;
		}

		// Token: 0x06002A8C RID: 10892 RVA: 0x000A3723 File Offset: 0x000A1923
		public static void StartUsingMachine(this Formation formation, UsableMachine usable, bool isPlayerOrder = false)
		{
			if (isPlayerOrder || (formation.IsAIControlled && !Mission.Current.IsMissionEnding))
			{
				formation.JoinDetachment(usable);
			}
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x000A3743 File Offset: 0x000A1943
		public static void StopUsingMachine(this Formation formation, UsableMachine usable, bool isPlayerOrder = false)
		{
			if (isPlayerOrder || formation.IsAIControlled)
			{
				formation.LeaveDetachment(usable);
			}
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x000A3757 File Offset: 0x000A1957
		public static WorldPosition ToWorldPosition(this Vec3 rawPosition)
		{
			return new WorldPosition(Mission.Current.Scene, rawPosition);
		}
	}
}
