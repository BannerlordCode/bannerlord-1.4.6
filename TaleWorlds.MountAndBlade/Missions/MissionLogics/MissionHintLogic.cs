using System;
using TaleWorlds.MountAndBlade.Missions.Hints;

namespace TaleWorlds.MountAndBlade.Missions.MissionLogics
{
	// Token: 0x020003EA RID: 1002
	public class MissionHintLogic : MissionLogic
	{
		// Token: 0x140000AE RID: 174
		// (add) Token: 0x06003706 RID: 14086 RVA: 0x000E37A4 File Offset: 0x000E19A4
		// (remove) Token: 0x06003707 RID: 14087 RVA: 0x000E37DC File Offset: 0x000E19DC
		public event MissionHintLogic.MissionHintChangedDelegate OnActiveHintChanged;

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06003708 RID: 14088 RVA: 0x000E3811 File Offset: 0x000E1A11
		// (set) Token: 0x06003709 RID: 14089 RVA: 0x000E3819 File Offset: 0x000E1A19
		public MissionHint ActiveHint { get; private set; }

		// Token: 0x0600370A RID: 14090 RVA: 0x000E3824 File Offset: 0x000E1A24
		public void SetActiveHint(MissionHint hint)
		{
			MissionHint activeHint = this.ActiveHint;
			this.ActiveHint = hint;
			MissionHintLogic.MissionHintChangedDelegate onActiveHintChanged = this.OnActiveHintChanged;
			if (onActiveHintChanged == null)
			{
				return;
			}
			onActiveHintChanged(activeHint, this.ActiveHint);
		}

		// Token: 0x0600370B RID: 14091 RVA: 0x000E3856 File Offset: 0x000E1A56
		public void Clear()
		{
			this.SetActiveHint(null);
		}

		// Token: 0x02000699 RID: 1689
		// (Invoke) Token: 0x060041AB RID: 16811
		public delegate void MissionHintChangedDelegate(MissionHint previousHint, MissionHint newHint);
	}
}
