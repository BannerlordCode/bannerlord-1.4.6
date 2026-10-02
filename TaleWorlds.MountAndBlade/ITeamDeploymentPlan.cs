using System;
using System.Runtime.CompilerServices;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000217 RID: 535
	public interface ITeamDeploymentPlan
	{
		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001F1F RID: 7967
		Team Team { get; }

		// Token: 0x06001F20 RID: 7968
		void MakeDeploymentPlan(float spawnPathOffset = 0f, float targetOffset = 0f, FormationSceneSpawnEntry[,] formationSceneSpawnEntries = null, bool isReinforcement = false);

		// Token: 0x06001F21 RID: 7969
		void ClearPlan(bool isReinforcement = false);

		// Token: 0x06001F22 RID: 7970
		bool IsFirstPlan(bool isReinforcement = false);

		// Token: 0x06001F23 RID: 7971
		bool IsPlanMade(bool isReinforcement = false);

		// Token: 0x06001F24 RID: 7972
		[return: TupleElementNames(new string[] { "id", "points" })]
		MBReadOnlyList<ValueTuple<string, MBList<Vec2>>> GetDeploymentBoundaries();

		// Token: 0x06001F25 RID: 7973
		float GetSpawnPathOffset(bool isReinforcement = false);

		// Token: 0x06001F26 RID: 7974
		float GetTargetOffset(bool isReinforcement = false);

		// Token: 0x06001F27 RID: 7975
		MatrixFrame GetDeploymentFrame();

		// Token: 0x06001F28 RID: 7976
		bool HasDeploymentBoundaries();

		// Token: 0x06001F29 RID: 7977
		IFormationDeploymentPlan GetFormationPlan(FormationClass formationIndex, bool isReinforcement = false);

		// Token: 0x06001F2A RID: 7978
		Vec3 GetMeanPosition(bool isReinforcement = false);

		// Token: 0x06001F2B RID: 7979
		bool IsPositionInsideDeploymentBoundaries(in Vec2 position, [TupleElementNames(new string[] { "id", "points" })] out ValueTuple<string, MBList<Vec2>> containingBoundaryTuple);

		// Token: 0x06001F2C RID: 7980
		Vec2 GetClosestDeploymentBoundaryPosition(in Vec2 position);
	}
}
