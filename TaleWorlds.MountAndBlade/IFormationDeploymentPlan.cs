using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000215 RID: 533
	public interface IFormationDeploymentPlan
	{
		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001EFC RID: 7932
		FormationClass Class { get; }

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001EFD RID: 7933
		FormationClass SpawnClass { get; }

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001EFE RID: 7934
		float PlannedWidth { get; }

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001EFF RID: 7935
		float PlannedDepth { get; }

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001F00 RID: 7936
		int PlannedTroopCount { get; }

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001F01 RID: 7937
		bool HasDimensions { get; }

		// Token: 0x06001F02 RID: 7938
		bool HasFrame();

		// Token: 0x06001F03 RID: 7939
		MatrixFrame GetFrame();

		// Token: 0x06001F04 RID: 7940
		Vec3 GetPosition();

		// Token: 0x06001F05 RID: 7941
		Vec2 GetDirection();

		// Token: 0x06001F06 RID: 7942
		WorldPosition CreateNewDeploymentWorldPosition(WorldPosition.WorldPositionEnforcedCache worldPositionEnforcedCache);
	}
}
