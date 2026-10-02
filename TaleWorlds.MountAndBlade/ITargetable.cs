using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000373 RID: 883
	public interface ITargetable
	{
		// Token: 0x06003255 RID: 12885
		TargetFlags GetTargetFlags();

		// Token: 0x06003256 RID: 12886
		float GetTargetValue(List<Vec3> referencePositions);

		// Token: 0x06003257 RID: 12887
		WeakGameEntity GetTargetEntity();

		// Token: 0x06003258 RID: 12888
		Vec3 GetTargetingOffset();

		// Token: 0x06003259 RID: 12889
		BattleSideEnum GetSide();

		// Token: 0x0600325A RID: 12890
		Vec3 GetTargetGlobalVelocity();

		// Token: 0x0600325B RID: 12891
		bool IsDestructable();

		// Token: 0x0600325C RID: 12892
		WeakGameEntity Entity();

		// Token: 0x0600325D RID: 12893
		ValueTuple<Vec3, Vec3> ComputeGlobalPhysicsBoundingBoxMinMax();
	}
}
