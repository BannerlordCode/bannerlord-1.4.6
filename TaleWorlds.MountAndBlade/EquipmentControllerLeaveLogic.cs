using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000283 RID: 643
	public class EquipmentControllerLeaveLogic : MissionLogic
	{
		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x060023E2 RID: 9186 RVA: 0x00080D3B File Offset: 0x0007EF3B
		// (set) Token: 0x060023E3 RID: 9187 RVA: 0x00080D43 File Offset: 0x0007EF43
		public bool IsEquipmentSelectionActive { get; private set; }

		// Token: 0x060023E4 RID: 9188 RVA: 0x00080D4C File Offset: 0x0007EF4C
		public void SetIsEquipmentSelectionActive(bool isActive)
		{
			this.IsEquipmentSelectionActive = isActive;
			Debug.Print("IsEquipmentSelectionActive: " + isActive.ToString(), 0, Debug.DebugColor.White, 17592186044416UL);
		}

		// Token: 0x060023E5 RID: 9189 RVA: 0x00080D77 File Offset: 0x0007EF77
		public override InquiryData OnEndMissionRequest(out bool canLeave)
		{
			canLeave = !this.IsEquipmentSelectionActive;
			return null;
		}
	}
}
