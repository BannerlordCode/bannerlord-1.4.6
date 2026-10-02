using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200027A RID: 634
	public class BattleMissionStarterLogic : MissionLogic
	{
		// Token: 0x06002356 RID: 9046 RVA: 0x0007DBAC File Offset: 0x0007BDAC
		public BattleMissionStarterLogic()
		{
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x0007DBB4 File Offset: 0x0007BDB4
		public BattleMissionStarterLogic(IMissionTroopSupplier defenderTroopSupplier = null, IMissionTroopSupplier attackerTroopSupplier = null)
		{
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x0007DBBC File Offset: 0x0007BDBC
		public override void AfterStart()
		{
			base.Mission.SetMissionMode(MissionMode.Battle, true);
		}
	}
}
