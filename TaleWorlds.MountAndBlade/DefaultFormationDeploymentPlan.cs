using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200020F RID: 527
	public class DefaultFormationDeploymentPlan : IFormationDeploymentPlan
	{
		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001E8B RID: 7819 RVA: 0x0006A007 File Offset: 0x00068207
		public FormationClass Class
		{
			get
			{
				return this._class;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001E8C RID: 7820 RVA: 0x0006A00F File Offset: 0x0006820F
		public FormationClass SpawnClass
		{
			get
			{
				return this._spawnClass;
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001E8D RID: 7821 RVA: 0x0006A017 File Offset: 0x00068217
		public float PlannedWidth
		{
			get
			{
				return this._plannedWidth;
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001E8E RID: 7822 RVA: 0x0006A01F File Offset: 0x0006821F
		public float PlannedDepth
		{
			get
			{
				return this._plannedDepth;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001E8F RID: 7823 RVA: 0x0006A027 File Offset: 0x00068227
		public int PlannedTroopCount
		{
			get
			{
				return this._plannedFootTroopCount + this._plannedMountedTroopCount;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001E90 RID: 7824 RVA: 0x0006A036 File Offset: 0x00068236
		public int PlannedFootTroopCount
		{
			get
			{
				return this._plannedFootTroopCount;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001E91 RID: 7825 RVA: 0x0006A03E File Offset: 0x0006823E
		public int PlannedMountedTroopCount
		{
			get
			{
				return this._plannedMountedTroopCount;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001E92 RID: 7826 RVA: 0x0006A046 File Offset: 0x00068246
		public bool HasDimensions
		{
			get
			{
				return this._plannedWidth >= 1E-05f && this._plannedDepth >= 1E-05f;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001E93 RID: 7827 RVA: 0x0006A067 File Offset: 0x00068267
		public bool HasSignificantMountedTroops
		{
			get
			{
				return DefaultMissionDeploymentPlan.HasSignificantMountedTroops(this._plannedFootTroopCount, this._plannedMountedTroopCount);
			}
		}

		// Token: 0x06001E94 RID: 7828 RVA: 0x0006A07A File Offset: 0x0006827A
		public DefaultFormationDeploymentPlan(FormationClass fClass)
		{
			this._class = fClass;
			this._spawnClass = fClass;
			this.Clear();
		}

		// Token: 0x06001E95 RID: 7829 RVA: 0x0006A096 File Offset: 0x00068296
		public bool HasFrame()
		{
			return this._spawnFrame.IsValid;
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x0006A0A3 File Offset: 0x000682A3
		public FormationDeploymentFlank GetDefaultFlank(int formationTroopCount, bool teamPlanHasAnyFootTroops, bool spawnWithHorses = false)
		{
			return DefaultFormationDeploymentPlan.GetFormationDefaultFlankAux(this._class, formationTroopCount, teamPlanHasAnyFootTroops, this.HasSignificantMountedTroops, spawnWithHorses);
		}

		// Token: 0x06001E97 RID: 7831 RVA: 0x0006A0B9 File Offset: 0x000682B9
		public FormationDeploymentOrder GetFlankDeploymentOrder(int offset = 0)
		{
			return FormationDeploymentOrder.GetDeploymentOrder(this._class, offset);
		}

		// Token: 0x06001E98 RID: 7832 RVA: 0x0006A0C7 File Offset: 0x000682C7
		public MatrixFrame GetFrame()
		{
			return this._spawnFrame.ToGroundMatrixFrame();
		}

		// Token: 0x06001E99 RID: 7833 RVA: 0x0006A0D4 File Offset: 0x000682D4
		public Vec3 GetPosition()
		{
			return this._spawnFrame.Origin.GetGroundVec3();
		}

		// Token: 0x06001E9A RID: 7834 RVA: 0x0006A0E8 File Offset: 0x000682E8
		public Vec2 GetDirection()
		{
			return this._spawnFrame.Rotation.f.AsVec2.Normalized();
		}

		// Token: 0x06001E9B RID: 7835 RVA: 0x0006A114 File Offset: 0x00068314
		public WorldPosition CreateNewDeploymentWorldPosition(WorldPosition.WorldPositionEnforcedCache worldPositionEnforcedCache)
		{
			if (worldPositionEnforcedCache == WorldPosition.WorldPositionEnforcedCache.NavMeshVec3)
			{
				return new WorldPosition(Mission.Current.Scene, UIntPtr.Zero, this._spawnFrame.Origin.GetNavMeshVec3(), false);
			}
			if (worldPositionEnforcedCache != WorldPosition.WorldPositionEnforcedCache.GroundVec3)
			{
				return this._spawnFrame.Origin;
			}
			return new WorldPosition(Mission.Current.Scene, UIntPtr.Zero, this._spawnFrame.Origin.GetGroundVec3(), false);
		}

		// Token: 0x06001E9C RID: 7836 RVA: 0x0006A182 File Offset: 0x00068382
		public void Clear()
		{
			this._plannedWidth = 0f;
			this._plannedDepth = 0f;
			this._plannedFootTroopCount = 0;
			this._plannedMountedTroopCount = 0;
			this._spawnFrame = WorldFrame.Invalid;
		}

		// Token: 0x06001E9D RID: 7837 RVA: 0x0006A1B3 File Offset: 0x000683B3
		public void SetPlannedTroopCount(int footTroopCount, int mountedTroopCount)
		{
			this._plannedFootTroopCount = footTroopCount;
			this._plannedMountedTroopCount = mountedTroopCount;
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x0006A1C3 File Offset: 0x000683C3
		public void SetPlannedDimensions(float width, float depth)
		{
			this._plannedWidth = MathF.Max(0f, width);
			this._plannedDepth = MathF.Max(0f, depth);
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x0006A1E7 File Offset: 0x000683E7
		public void SetFrame(in WorldFrame frame)
		{
			this._spawnFrame = frame;
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x0006A1F5 File Offset: 0x000683F5
		public void SetSpawnClass(FormationClass spawnClass)
		{
			this._spawnClass = spawnClass;
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x0006A200 File Offset: 0x00068400
		public static FormationDeploymentFlank GetFormationDefaultFlankAux(FormationClass formationClass, int formationTroopCount, bool teamPlanHasAnyFootTroops, bool hasSignificantMountedTroops, bool canSpawnWithHorses)
		{
			FormationDeploymentFlank formationDeploymentFlank;
			if (!formationClass.IsMounted() && formationTroopCount == 0)
			{
				formationDeploymentFlank = FormationDeploymentFlank.Rear;
			}
			else if (hasSignificantMountedTroops && (!canSpawnWithHorses || !teamPlanHasAnyFootTroops))
			{
				if (formationTroopCount == 0 || formationClass == FormationClass.LightCavalry || formationClass == FormationClass.HorseArcher)
				{
					formationDeploymentFlank = FormationDeploymentFlank.Rear;
				}
				else
				{
					formationDeploymentFlank = FormationDeploymentFlank.Front;
				}
			}
			else
			{
				switch (formationClass)
				{
				case FormationClass.Ranged:
				case FormationClass.NumberOfRegularFormations:
				case FormationClass.Bodyguard:
				case FormationClass.NumberOfAllFormations:
					return FormationDeploymentFlank.Rear;
				case FormationClass.Cavalry:
				case FormationClass.HeavyCavalry:
					return FormationDeploymentFlank.Left;
				case FormationClass.HorseArcher:
				case FormationClass.LightCavalry:
					return FormationDeploymentFlank.Right;
				}
				formationDeploymentFlank = FormationDeploymentFlank.Front;
			}
			return formationDeploymentFlank;
		}

		// Token: 0x04000A7A RID: 2682
		private WorldFrame _spawnFrame;

		// Token: 0x04000A7B RID: 2683
		private FormationClass _spawnClass;

		// Token: 0x04000A7C RID: 2684
		private readonly FormationClass _class;

		// Token: 0x04000A7D RID: 2685
		private float _plannedWidth;

		// Token: 0x04000A7E RID: 2686
		private float _plannedDepth;

		// Token: 0x04000A7F RID: 2687
		private int _plannedFootTroopCount;

		// Token: 0x04000A80 RID: 2688
		private int _plannedMountedTroopCount;
	}
}
