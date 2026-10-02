using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000289 RID: 649
	public struct MissionFormationSpawnData
	{
		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06002410 RID: 9232 RVA: 0x000826A6 File Offset: 0x000808A6
		public int NumTroops
		{
			get
			{
				return this.FootTroopCount + this.MountedTroopCount;
			}
		}

		// Token: 0x04000DE8 RID: 3560
		public int FootTroopCount;

		// Token: 0x04000DE9 RID: 3561
		public int MountedTroopCount;
	}
}
