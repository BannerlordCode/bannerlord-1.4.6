using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000211 RID: 529
	public class DefaultTeamDeploymentPlan : ITeamDeploymentPlan
	{
		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001ED0 RID: 7888 RVA: 0x0006ADA7 File Offset: 0x00068FA7
		// (set) Token: 0x06001ED1 RID: 7889 RVA: 0x0006ADAF File Offset: 0x00068FAF
		public Team Team { get; private set; }

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001ED2 RID: 7890 RVA: 0x0006ADB8 File Offset: 0x00068FB8
		// (set) Token: 0x06001ED3 RID: 7891 RVA: 0x0006ADC0 File Offset: 0x00068FC0
		public bool SpawnWithHorses { get; private set; }

		// Token: 0x06001ED4 RID: 7892 RVA: 0x0006ADCC File Offset: 0x00068FCC
		public DefaultTeamDeploymentPlan(Mission mission, Team team)
		{
			this._mission = mission;
			this.Team = team;
			this.SpawnWithHorses = false;
			this._initialPlan = DefaultDeploymentPlan.CreateInitialPlan(this._mission, this.Team);
			this._deploymentBoundaries.Clear();
			this._reinforcementPlans = new List<DefaultDeploymentPlan>();
			this._currentReinforcementPlan = this._initialPlan;
			if (this._mission.HasSpawnPath)
			{
				foreach (SpawnPathData spawnPathData in this._mission.GetReinforcementPathsDataOfSide(this.Team.Side))
				{
					DefaultDeploymentPlan defaultDeploymentPlan = DefaultDeploymentPlan.CreateReinforcementPlanWithSpawnPath(this._mission, this.Team, spawnPathData);
					this._reinforcementPlans.Add(defaultDeploymentPlan);
				}
				this._currentReinforcementPlan = this._reinforcementPlans[0];
				return;
			}
			DefaultDeploymentPlan defaultDeploymentPlan2 = DefaultDeploymentPlan.CreateReinforcementPlan(this._mission, this.Team);
			this._reinforcementPlans.Add(defaultDeploymentPlan2);
			this._currentReinforcementPlan = defaultDeploymentPlan2;
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x0006AEEC File Offset: 0x000690EC
		public void SetSpawnWithHorses(bool value)
		{
			this.SpawnWithHorses = value;
			this._initialPlan.SetSpawnWithHorses(value);
			foreach (DefaultDeploymentPlan defaultDeploymentPlan in this._reinforcementPlans)
			{
				defaultDeploymentPlan.SetSpawnWithHorses(value);
			}
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x0006AF50 File Offset: 0x00069150
		public void MakeDeploymentPlan(float spawnPathOffset = 0f, float targetOffset = 0f, FormationSceneSpawnEntry[,] formationSceneSpawnEntries = null, bool isReinforcement = false)
		{
			if (isReinforcement)
			{
				using (List<DefaultDeploymentPlan>.Enumerator enumerator = this._reinforcementPlans.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DefaultDeploymentPlan defaultDeploymentPlan = enumerator.Current;
						defaultDeploymentPlan.MakeDeploymentPlan(0f, 0f, formationSceneSpawnEntries);
					}
					return;
				}
			}
			this._initialPlan.MakeDeploymentPlan(spawnPathOffset, targetOffset, formationSceneSpawnEntries);
			this.PlanDeploymentZone();
		}

		// Token: 0x06001ED7 RID: 7895 RVA: 0x0006AFC4 File Offset: 0x000691C4
		public void UpdateReinforcementPlans()
		{
			if (this._reinforcementPlans.Count <= 1)
			{
				return;
			}
			foreach (DefaultDeploymentPlan defaultDeploymentPlan in this._reinforcementPlans)
			{
				defaultDeploymentPlan.UpdateSafetyScore();
			}
			if (!this._currentReinforcementPlan.IsSafeToDeploy)
			{
				this._currentReinforcementPlan = this._reinforcementPlans.MaxBy<DefaultDeploymentPlan, float>((DefaultDeploymentPlan plan) => plan.SafetyScore);
			}
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x0006B060 File Offset: 0x00069260
		public void ClearPlan(bool isReinforcement = false)
		{
			if (isReinforcement)
			{
				using (List<DefaultDeploymentPlan>.Enumerator enumerator = this._reinforcementPlans.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DefaultDeploymentPlan defaultDeploymentPlan = enumerator.Current;
						defaultDeploymentPlan.ClearPlan();
					}
					return;
				}
			}
			this._initialPlan.ClearPlan();
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x0006B0C0 File Offset: 0x000692C0
		public void ClearAddedTroops(bool isReinforcement = false)
		{
			if (isReinforcement)
			{
				using (List<DefaultDeploymentPlan>.Enumerator enumerator = this._reinforcementPlans.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DefaultDeploymentPlan defaultDeploymentPlan = enumerator.Current;
						defaultDeploymentPlan.ClearAddedTroops();
					}
					return;
				}
			}
			this._initialPlan.ClearAddedTroops();
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x0006B120 File Offset: 0x00069320
		public void AddTroops(FormationClass formationClass, int footTroopCount, int mountedTroopCount, bool isReinforcement = false)
		{
			if (isReinforcement)
			{
				using (List<DefaultDeploymentPlan>.Enumerator enumerator = this._reinforcementPlans.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DefaultDeploymentPlan defaultDeploymentPlan = enumerator.Current;
						defaultDeploymentPlan.AddTroops(formationClass, footTroopCount, mountedTroopCount);
					}
					return;
				}
			}
			this._initialPlan.AddTroops(formationClass, footTroopCount, mountedTroopCount);
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x0006B188 File Offset: 0x00069388
		public int GetTroopCount(bool isReinforcement = false)
		{
			if (isReinforcement)
			{
				return this._currentReinforcementPlan.TroopCount;
			}
			return this._initialPlan.TroopCount;
		}

		// Token: 0x06001EDC RID: 7900 RVA: 0x0006B1A4 File Offset: 0x000693A4
		public bool IsFirstPlan(bool isReinforcement = false)
		{
			if (isReinforcement)
			{
				return this._currentReinforcementPlan.PlanCount == 1;
			}
			return this._initialPlan.PlanCount == 1;
		}

		// Token: 0x06001EDD RID: 7901 RVA: 0x0006B1C6 File Offset: 0x000693C6
		public bool IsPlanMade(bool isReinforcement = false)
		{
			if (isReinforcement)
			{
				return this._currentReinforcementPlan.IsPlanMade;
			}
			return this._initialPlan.IsPlanMade;
		}

		// Token: 0x06001EDE RID: 7902 RVA: 0x0006B1E2 File Offset: 0x000693E2
		[return: TupleElementNames(new string[] { "id", "points" })]
		public MBReadOnlyList<ValueTuple<string, MBList<Vec2>>> GetDeploymentBoundaries()
		{
			return this._deploymentBoundaries;
		}

		// Token: 0x06001EDF RID: 7903 RVA: 0x0006B1EA File Offset: 0x000693EA
		public float GetSpawnPathOffset(bool isReinforcement = false)
		{
			if (isReinforcement)
			{
				return this._currentReinforcementPlan.SpawnPathOffset;
			}
			return this._initialPlan.SpawnPathOffset;
		}

		// Token: 0x06001EE0 RID: 7904 RVA: 0x0006B206 File Offset: 0x00069406
		public float GetTargetOffset(bool isReinforcement = false)
		{
			if (isReinforcement)
			{
				return this._currentReinforcementPlan.TargetOffset;
			}
			return this._initialPlan.TargetOffset;
		}

		// Token: 0x06001EE1 RID: 7905 RVA: 0x0006B222 File Offset: 0x00069422
		public MatrixFrame GetDeploymentFrame()
		{
			return this._deploymentFrame;
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x0006B22A File Offset: 0x0006942A
		public bool HasDeploymentBoundaries()
		{
			return !this._deploymentBoundaries.IsEmpty<ValueTuple<string, MBList<Vec2>>>();
		}

		// Token: 0x06001EE3 RID: 7907 RVA: 0x0006B23A File Offset: 0x0006943A
		public IFormationDeploymentPlan GetFormationPlan(FormationClass fClass, bool isReinforcement = false)
		{
			if (isReinforcement)
			{
				return this._currentReinforcementPlan.GetFormationPlan(fClass);
			}
			return this._initialPlan.GetFormationPlan(fClass);
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x0006B258 File Offset: 0x00069458
		public Vec3 GetMeanPosition(bool isReinforcement = false)
		{
			if (isReinforcement)
			{
				return this._currentReinforcementPlan.MeanPosition;
			}
			return this._initialPlan.MeanPosition;
		}

		// Token: 0x06001EE5 RID: 7909 RVA: 0x0006B274 File Offset: 0x00069474
		public bool IsInitialPlanSuitableForFormations(ValueTuple<int, int>[] troopDataPerFormationClass)
		{
			return this._initialPlan.IsPlanSuitableForFormations(troopDataPerFormationClass);
		}

		// Token: 0x06001EE6 RID: 7910 RVA: 0x0006B284 File Offset: 0x00069484
		public bool IsPositionInsideDeploymentBoundaries(in Vec2 position, [TupleElementNames(new string[] { "id", "points" })] out ValueTuple<string, MBList<Vec2>> containingBoundaryTuple)
		{
			bool flag = false;
			containingBoundaryTuple = new ValueTuple<string, MBList<Vec2>>("", null);
			foreach (ValueTuple<string, MBList<Vec2>> valueTuple in this._deploymentBoundaries)
			{
				MBList<Vec2> item = valueTuple.Item2;
				if (MBSceneUtilities.IsPointInsideBoundaries(in position, item, 0.05f))
				{
					containingBoundaryTuple = valueTuple;
					flag = true;
					break;
				}
			}
			return flag;
		}

		// Token: 0x06001EE7 RID: 7911 RVA: 0x0006B304 File Offset: 0x00069504
		public Vec2 GetClosestDeploymentBoundaryPosition(in Vec2 position)
		{
			Vec2 vec = position;
			float num = float.MaxValue;
			foreach (ValueTuple<string, MBList<Vec2>> valueTuple in this._deploymentBoundaries)
			{
				MBList<Vec2> item = valueTuple.Item2;
				if (item.Count > 2)
				{
					Vec2 vec2;
					float num2 = MBSceneUtilities.FindClosestPointToBoundaries(in position, item, out vec2);
					if (num2 < num)
					{
						num = num2;
						vec = vec2;
					}
				}
			}
			return vec;
		}

		// Token: 0x06001EE8 RID: 7912 RVA: 0x0006B384 File Offset: 0x00069584
		public bool GetPathDeploymentBoundaryIntersection(in WorldPosition startPosition, in WorldPosition endPosition, out WorldPosition intersection)
		{
			WorldPosition worldPosition = startPosition;
			Vec2 vec = worldPosition.AsVec2;
			ValueTuple<string, MBList<Vec2>> valueTuple;
			this.IsPositionInsideDeploymentBoundaries(in vec, out valueTuple);
			intersection = WorldPosition.Invalid;
			NavigationPath value = DefaultTeamDeploymentPlan._navigationPath.Value;
			Scene scene = Mission.Current.Scene;
			worldPosition = startPosition;
			UIntPtr nearestNavMesh = worldPosition.GetNearestNavMesh();
			worldPosition = endPosition;
			UIntPtr nearestNavMesh2 = worldPosition.GetNearestNavMesh();
			worldPosition = startPosition;
			Vec2 asVec = worldPosition.AsVec2;
			worldPosition = endPosition;
			if (scene.GetPathBetweenAIFaces(nearestNavMesh, nearestNavMesh2, asVec, worldPosition.AsVec2, 0f, value, null) && value.Size > 0)
			{
				worldPosition = startPosition;
				Vec2 vec2 = worldPosition.AsVec2;
				ValueTuple<string, MBList<Vec2>> valueTuple2 = valueTuple;
				Vec2 vec3 = Vec2.Invalid;
				for (int i = 0; i < value.Size; i++)
				{
					Vec2 vec4 = value[i];
					ValueTuple<string, MBList<Vec2>> valueTuple3;
					if (!this.IsPositionInsideDeploymentBoundaries(in vec4, out valueTuple3))
					{
						vec3 = vec4;
						break;
					}
					vec2 = vec4;
					valueTuple2 = valueTuple3;
				}
				if (vec3.IsValid)
				{
					intersection = startPosition;
					intersection.SetVec2(vec2);
					vec = vec3 - vec2;
					Vec2 vec5 = vec.Normalized();
					Vec2 vec6;
					MBMath.IntersectRayWithPolygon(vec2, vec5, valueTuple2.Item2, out vec6);
					intersection.SetVec2(Mission.Current.Scene.GetLastPointOnNavigationMeshFromWorldPositionToDestination(ref intersection, vec6).AsVec2);
				}
				else
				{
					intersection = endPosition;
				}
			}
			else
			{
				intersection = startPosition;
			}
			DefaultTeamDeploymentPlan._navigationPath.Value.Size = 0;
			return intersection.IsValid;
		}

		// Token: 0x06001EE9 RID: 7913 RVA: 0x0006B50C File Offset: 0x0006970C
		private void PlanDeploymentZone()
		{
			if (this._mission.HasSpawnPath || this._mission.IsFieldBattle || this._mission.IsNavalRaidBattle)
			{
				this.ComputeDeploymentZoneFromFormations();
				return;
			}
			if (this._mission.IsSiegeBattle)
			{
				this.ComputeDeploymentZoneFromSceneDeploymentBoundaries();
				return;
			}
			this._deploymentBoundaries.Clear();
		}

		// Token: 0x06001EEA RID: 7914 RVA: 0x0006B568 File Offset: 0x00069768
		private void ComputeDeploymentZoneFromFormations()
		{
			this._initialPlan.GetFirstValidFormationDeploymentFrame(out this._deploymentFrame);
			float num = 0f;
			float num2 = 0f;
			for (int i = 0; i < 10; i++)
			{
				FormationClass formationClass = (FormationClass)i;
				DefaultFormationDeploymentPlan formationPlan = this._initialPlan.GetFormationPlan(formationClass);
				if (formationPlan.HasFrame())
				{
					MatrixFrame frame = formationPlan.GetFrame();
					MatrixFrame matrixFrame = this._deploymentFrame.TransformToLocal(in frame);
					num = Math.Max(matrixFrame.origin.y, num);
					num2 = Math.Max(Math.Abs(matrixFrame.origin.x), num2);
				}
			}
			num += 10f;
			this._deploymentFrame.Advance(num);
			this._deploymentBoundaries.Clear();
			float num3 = 2f * num2 + 1.5f * (float)this._initialPlan.TroopCount;
			num3 = Math.Max(num3, 100f);
			foreach (KeyValuePair<string, ICollection<Vec2>> keyValuePair in this._mission.Boundaries)
			{
				string key = keyValuePair.Key;
				MBList<Vec2> mblist = DefaultTeamDeploymentPlan.ComputeDeploymentBoundariesFromMissionBoundaries(keyValuePair.Value, ref this._deploymentFrame, num3);
				this._deploymentBoundaries.Add(new ValueTuple<string, MBList<Vec2>>(key, mblist));
			}
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x0006B6B8 File Offset: 0x000698B8
		private void ComputeDeploymentZoneFromSceneDeploymentBoundaries()
		{
			this._deploymentBoundaries.Clear();
			foreach (ValueTuple<string, MBList<Vec2>, bool> valueTuple in MBSceneUtilities.GetDeploymentBoundaries(this.Team.Side))
			{
				MBList<Vec2> mblist = new MBList<Vec2>(valueTuple.Item2);
				MBSceneUtilities.RadialSortBoundary(ref mblist);
				MBSceneUtilities.FindConvexHull(ref mblist);
				this._deploymentBoundaries.Add(new ValueTuple<string, MBList<Vec2>>(valueTuple.Item1, mblist));
			}
			this._deploymentFrame = this._mission.Scene.FindWeakEntityWithTag((this.Team.Side == BattleSideEnum.Attacker) ? "attacker_infantry" : "defender_infantry").GetGlobalFrame();
		}

		// Token: 0x06001EEC RID: 7916 RVA: 0x0006B784 File Offset: 0x00069984
		private static MBList<Vec2> ComputeDeploymentBoundariesFromMissionBoundaries(ICollection<Vec2> missionBoundaries, ref MatrixFrame deploymentFrame, float desiredWidth)
		{
			MBList<Vec2> mblist = new MBList<Vec2>();
			float num = desiredWidth / 2f;
			if (missionBoundaries.Count > 2)
			{
				Vec2 asVec = deploymentFrame.origin.AsVec2;
				Vec2 vec = deploymentFrame.rotation.s.AsVec2.Normalized();
				Vec2 vec2 = deploymentFrame.rotation.f.AsVec2.Normalized();
				MBList<Vec2> mblist2 = missionBoundaries.ToMBList<Vec2>();
				List<ValueTuple<Vec2, Vec2>> list = new List<ValueTuple<Vec2, Vec2>>();
				Vec2 vec3 = DefaultTeamDeploymentPlan.ClampRayToMissionBoundaries(mblist2, asVec, vec, num);
				DefaultTeamDeploymentPlan.AddDeploymentBoundaryPoint(mblist, vec3);
				Vec2 vec4 = DefaultTeamDeploymentPlan.ClampRayToMissionBoundaries(mblist2, asVec, -vec, num);
				DefaultTeamDeploymentPlan.AddDeploymentBoundaryPoint(mblist, vec4);
				Vec2 vec5;
				if (MBMath.IntersectRayWithPolygon(vec3, -vec2, mblist2, out vec5) && (vec5 - vec3).Length > 0.1f)
				{
					list.Add(new ValueTuple<Vec2, Vec2>(vec5, vec3));
					DefaultTeamDeploymentPlan.AddDeploymentBoundaryPoint(mblist, vec5);
				}
				list.Add(new ValueTuple<Vec2, Vec2>(vec3, vec4));
				Vec2 vec6;
				if (MBMath.IntersectRayWithPolygon(vec4, -vec2, mblist2, out vec6) && (vec6 - vec4).Length > 0.1f)
				{
					list.Add(new ValueTuple<Vec2, Vec2>(vec4, vec6));
					DefaultTeamDeploymentPlan.AddDeploymentBoundaryPoint(mblist, vec6);
				}
				foreach (Vec2 vec7 in missionBoundaries)
				{
					bool flag = true;
					foreach (ValueTuple<Vec2, Vec2> valueTuple in list)
					{
						Vec2 vec8 = vec7 - valueTuple.Item1;
						Vec2 vec9 = valueTuple.Item2 - valueTuple.Item1;
						if (vec9.x * vec8.y - vec9.y * vec8.x <= 0f)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						DefaultTeamDeploymentPlan.AddDeploymentBoundaryPoint(mblist, vec7);
					}
				}
				MBSceneUtilities.RadialSortBoundary(ref mblist);
			}
			return mblist;
		}

		// Token: 0x06001EED RID: 7917 RVA: 0x0006B9A4 File Offset: 0x00069BA4
		private static void AddDeploymentBoundaryPoint(MBList<Vec2> deploymentBoundaries, Vec2 point)
		{
			if (!deploymentBoundaries.Exists((Vec2 boundaryPoint) => boundaryPoint.Distance(point) <= 0.1f))
			{
				deploymentBoundaries.Add(point);
			}
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x0006B9E0 File Offset: 0x00069BE0
		private static Vec2 ClampRayToMissionBoundaries(MBList<Vec2> boundaries, Vec2 origin, Vec2 direction, float maxLength)
		{
			if (Mission.Current.IsPositionInsideBoundaries(origin))
			{
				Vec2 vec = origin + direction * maxLength;
				if (Mission.Current.IsPositionInsideBoundaries(vec))
				{
					return vec;
				}
			}
			Vec2 vec2;
			if (MBMath.IntersectRayWithPolygon(origin, direction, boundaries, out vec2))
			{
				return vec2;
			}
			return origin;
		}

		// Token: 0x06001EF0 RID: 7920 RVA: 0x0006BA42 File Offset: 0x00069C42
		bool ITeamDeploymentPlan.IsPositionInsideDeploymentBoundaries(in Vec2 position, [TupleElementNames(new string[] { "id", "points" })] out ValueTuple<string, MBList<Vec2>> containingBoundaryTuple)
		{
			return this.IsPositionInsideDeploymentBoundaries(in position, out containingBoundaryTuple);
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x0006BA4C File Offset: 0x00069C4C
		Vec2 ITeamDeploymentPlan.GetClosestDeploymentBoundaryPosition(in Vec2 position)
		{
			return this.GetClosestDeploymentBoundaryPosition(in position);
		}

		// Token: 0x04000A85 RID: 2693
		public const float DeployZoneMinimumWidth = 100f;

		// Token: 0x04000A86 RID: 2694
		public const float DeployZoneForwardMargin = 10f;

		// Token: 0x04000A87 RID: 2695
		public const float DeployZoneExtraWidthPerTroop = 1.5f;

		// Token: 0x04000A88 RID: 2696
		public const string DefenderDeploymentFrameEntityTag = "defender_infantry";

		// Token: 0x04000A89 RID: 2697
		public const string AttackerDeploymentFrameEntityTag = "attacker_infantry";

		// Token: 0x04000A8C RID: 2700
		private Mission _mission;

		// Token: 0x04000A8D RID: 2701
		private readonly DefaultDeploymentPlan _initialPlan;

		// Token: 0x04000A8E RID: 2702
		private readonly List<DefaultDeploymentPlan> _reinforcementPlans;

		// Token: 0x04000A8F RID: 2703
		private DefaultDeploymentPlan _currentReinforcementPlan;

		// Token: 0x04000A90 RID: 2704
		[TupleElementNames(new string[] { "id", "points" })]
		private readonly MBList<ValueTuple<string, MBList<Vec2>>> _deploymentBoundaries = new MBList<ValueTuple<string, MBList<Vec2>>>();

		// Token: 0x04000A91 RID: 2705
		private MatrixFrame _deploymentFrame;

		// Token: 0x04000A92 RID: 2706
		private static ThreadLocal<NavigationPath> _navigationPath = new ThreadLocal<NavigationPath>(() => new NavigationPath());
	}
}
