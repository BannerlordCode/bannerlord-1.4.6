using System;

namespace TaleWorlds.MountAndBlade.Missions.Objectives
{
	// Token: 0x020003E5 RID: 997
	public struct MissionObjectiveProgressInfo
	{
		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x060036F6 RID: 14070 RVA: 0x000E3585 File Offset: 0x000E1785
		public bool HasProgress
		{
			get
			{
				return this.RequiredProgressAmount > 0;
			}
		}

		// Token: 0x040017A7 RID: 6055
		public int RequiredProgressAmount;

		// Token: 0x040017A8 RID: 6056
		public int CurrentProgressAmount;
	}
}
