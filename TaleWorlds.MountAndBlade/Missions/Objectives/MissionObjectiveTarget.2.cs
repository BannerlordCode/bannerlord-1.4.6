using System;

namespace TaleWorlds.MountAndBlade.Missions.Objectives
{
	// Token: 0x020003E7 RID: 999
	public abstract class MissionObjectiveTarget<T> : MissionObjectiveTarget
	{
		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x060036FB RID: 14075 RVA: 0x000E3598 File Offset: 0x000E1798
		public T Target { get; }

		// Token: 0x060036FC RID: 14076 RVA: 0x000E35A0 File Offset: 0x000E17A0
		public MissionObjectiveTarget(T target)
		{
			this.Target = target;
		}
	}
}
