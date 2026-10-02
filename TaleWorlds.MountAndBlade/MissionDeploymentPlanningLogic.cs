using System;
using System.Runtime.CompilerServices;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200028E RID: 654
	public abstract class MissionDeploymentPlanningLogic : MissionLogic, IMissionDeploymentPlan
	{
		// Token: 0x0600245A RID: 9306 RVA: 0x00084820 File Offset: 0x00082A20
		public virtual void Initialize()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x00084827 File Offset: 0x00082A27
		public virtual void ClearAll()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x0008482E File Offset: 0x00082A2E
		public virtual void MakeDefaultDeploymentPlans()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x00084835 File Offset: 0x00082A35
		public virtual void MakeDeploymentPlan(Team team, float spawnPathOffset = 0f, float targetPathOffset = 0f)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x0008483C File Offset: 0x00082A3C
		public virtual bool RemakeDeploymentPlan(Team team)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x00084843 File Offset: 0x00082A43
		public virtual void ClearDeploymentPlan(Team team)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x0008484A File Offset: 0x00082A4A
		public virtual bool IsPlanMade(Team team)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x00084851 File Offset: 0x00082A51
		public virtual bool IsPlanMade(Team team, out bool isFirstPlan)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x00084858 File Offset: 0x00082A58
		public virtual bool IsPositionInsideDeploymentBoundaries(Team team, in Vec2 position)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x0008485F File Offset: 0x00082A5F
		public virtual bool HasDeploymentBoundaries(Team team)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x00084866 File Offset: 0x00082A66
		[return: TupleElementNames(new string[] { "id", "points" })]
		public virtual MBReadOnlyList<ValueTuple<string, MBList<Vec2>>> GetDeploymentBoundaries(Team team)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x0008486D File Offset: 0x00082A6D
		public virtual bool SupportsReinforcements()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x00084874 File Offset: 0x00082A74
		public virtual void UpdateReinforcementPlan(Team team)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x0008487B File Offset: 0x00082A7B
		public virtual bool SupportsNavmesh(Team team)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x00084882 File Offset: 0x00082A82
		public virtual bool HasPlayerSpawnFrame(BattleSideEnum battleSide)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x00084889 File Offset: 0x00082A89
		public virtual bool GetPlayerSpawnFrame(BattleSideEnum battleSide, out WorldPosition position, out Vec2 direction)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x00084890 File Offset: 0x00082A90
		public virtual Vec2 GetClosestDeploymentBoundaryPosition(Team team, in Vec2 position)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x00084897 File Offset: 0x00082A97
		public virtual void ProjectPositionToDeploymentBoundaries(Team team, ref WorldPosition position)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x0008489E File Offset: 0x00082A9E
		public virtual bool GetPathDeploymentBoundaryIntersection(Team team, in WorldPosition startPosition, in WorldPosition endPosition, out WorldPosition foundPosition)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x000848A5 File Offset: 0x00082AA5
		public virtual MatrixFrame GetDeploymentFrame(Team team)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x000848AC File Offset: 0x00082AAC
		public virtual IFormationDeploymentPlan GetFormationPlan(Team team, FormationClass fClass, bool isReinforcement = false)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x000848B3 File Offset: 0x00082AB3
		public virtual float GetSpawnPathOffset(Team team)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x000848BA File Offset: 0x00082ABA
		public virtual MatrixFrame GetZoomFocusFrame(Team team)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002471 RID: 9329 RVA: 0x000848C1 File Offset: 0x00082AC1
		public virtual float GetZoomOffset(Team team, float fovAngle)
		{
			throw new NotImplementedException();
		}
	}
}
