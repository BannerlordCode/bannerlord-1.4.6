using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.Objects.Usables
{
	// Token: 0x020003AA RID: 938
	public class RemoveExtraWeaponOnStopUsageComponent : UsableMissionObjectComponent
	{
		// Token: 0x0600352B RID: 13611 RVA: 0x000DAA1C File Offset: 0x000D8C1C
		protected internal override void OnUseStopped(Agent userAgent, bool isSuccessful = true)
		{
			if (!GameNetwork.IsClientOrReplay && !userAgent.Equipment[EquipmentIndex.ExtraWeaponSlot].IsEmpty && !Mission.Current.MissionIsEnding)
			{
				userAgent.Mission.AddTickActionMT(Mission.MissionTickAction.RemoveEquippedWeapon, userAgent, 4, 0);
			}
		}
	}
}
