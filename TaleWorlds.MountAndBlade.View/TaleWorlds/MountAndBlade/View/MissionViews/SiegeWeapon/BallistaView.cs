using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade.View.MissionViews.SiegeWeapon
{
	// Token: 0x020000A3 RID: 163
	public class BallistaView : RangedSiegeWeaponView
	{
		// Token: 0x06000586 RID: 1414 RVA: 0x00028121 File Offset: 0x00026321
		protected override void OnAdded(Scene scene)
		{
			base.OnAdded(scene);
			this.UsesMouseForAiming = true;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00028131 File Offset: 0x00026331
		protected override void StartUsingWeaponCamera()
		{
			base.StartUsingWeaponCamera();
			base.MissionScreen.SetExtraCameraParameters(true, 1.5f);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0002814A File Offset: 0x0002634A
		protected override void HandleUserCameraRotation(float dt)
		{
		}
	}
}
