using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade.ComponentInterfaces;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000203 RID: 515
	public class MultiplayerBattleInitializationModel : BattleInitializationModel
	{
		// Token: 0x06001DFF RID: 7679 RVA: 0x000678DB File Offset: 0x00065ADB
		public override List<FormationClass> GetAllAvailableTroopTypes()
		{
			return new List<FormationClass>();
		}

		// Token: 0x06001E00 RID: 7680 RVA: 0x000678E2 File Offset: 0x00065AE2
		protected override bool CanPlayerSideDeployWithOrderOfBattleAux()
		{
			return false;
		}
	}
}
