using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000352 RID: 850
	public interface IMoveableSiegeWeapon
	{
		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x060030BA RID: 12474
		SiegeWeaponMovementComponent MovementComponent { get; }

		// Token: 0x060030BB RID: 12475
		void HighlightPath();

		// Token: 0x060030BC RID: 12476
		void SwitchGhostEntityMovementMode(bool isGhostEnabled);

		// Token: 0x060030BD RID: 12477
		MatrixFrame GetInitialFrame();
	}
}
