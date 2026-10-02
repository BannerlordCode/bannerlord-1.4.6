using System;

namespace TaleWorlds.MountAndBlade.Source.Missions
{
	// Token: 0x020003D6 RID: 982
	public class MissionOptionsComponent : MissionLogic
	{
		// Token: 0x140000AC RID: 172
		// (add) Token: 0x0600366D RID: 13933 RVA: 0x000E14CC File Offset: 0x000DF6CC
		// (remove) Token: 0x0600366E RID: 13934 RVA: 0x000E1504 File Offset: 0x000DF704
		public event OnMissionAddOptionsDelegate OnOptionsAdded;

		// Token: 0x0600366F RID: 13935 RVA: 0x000E1539 File Offset: 0x000DF739
		public void OnAddOptionsUIHandler()
		{
			if (this.OnOptionsAdded != null)
			{
				this.OnOptionsAdded();
			}
		}
	}
}
