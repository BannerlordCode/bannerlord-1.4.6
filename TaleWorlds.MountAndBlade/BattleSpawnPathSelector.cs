using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200020D RID: 525
	public class BattleSpawnPathSelector
	{
		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001E57 RID: 7767 RVA: 0x00068BA0 File Offset: 0x00066DA0
		// (set) Token: 0x06001E58 RID: 7768 RVA: 0x00068BA8 File Offset: 0x00066DA8
		public bool IsInitialized { get; private set; }

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001E59 RID: 7769 RVA: 0x00068BB1 File Offset: 0x00066DB1
		public Path InitialPath
		{
			get
			{
				return this._initialPath;
			}
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x00068BB9 File Offset: 0x00066DB9
		public BattleSpawnPathSelector(Mission mission)
		{
			this.IsInitialized = false;
			this._initialPath = null;
			this._mission = mission;
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x00068BD8 File Offset: 0x00066DD8
		public void Initialize()
		{
			float num;
			float num2;
			bool flag;
			Path path = BattleSpawnPathSelector.FindBestInitialPath(this._mission, out num, out num2, out flag);
			if (path != null)
			{
				this._initialPath = path;
				this._battleSideSelectors = new BattleSideSpawnPathSelector[2];
				this._battleSideSelectors[0] = new BattleSideSpawnPathSelector(this._mission, path, num, flag);
				float num3 = MathF.Clamp(num2 - num, 1f, num2 - 1f);
				this._battleSideSelectors[1] = new BattleSideSpawnPathSelector(this._mission, path, num3, !flag);
				this.IsInitialized = true;
				return;
			}
			this._initialPath = null;
			this.IsInitialized = false;
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x00068C70 File Offset: 0x00066E70
		public bool HasPath(Path path)
		{
			if (!this.IsInitialized)
			{
				Debug.FailedAssert("BattleSpawnPathSelector must be initialized.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Deployment\\BattleSpawnPathSelector.cs", "HasPath", 68);
				return false;
			}
			BattleSideSpawnPathSelector battleSideSpawnPathSelector = this._battleSideSelectors[1];
			BattleSideSpawnPathSelector battleSideSpawnPathSelector2 = this._battleSideSelectors[0];
			return path != null && (this._initialPath.Pointer == path.Pointer || battleSideSpawnPathSelector.HasReinforcementPath(path) || battleSideSpawnPathSelector2.HasReinforcementPath(path));
		}

		// Token: 0x06001E5D RID: 7773 RVA: 0x00068CE4 File Offset: 0x00066EE4
		public bool GetInitialPathDataOfSide(BattleSideEnum side, out SpawnPathData pathPathData)
		{
			if (!this.IsInitialized)
			{
				Debug.FailedAssert("BattleSpawnPathSelector must be initialized.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Deployment\\BattleSpawnPathSelector.cs", "GetInitialPathDataOfSide", 82);
				pathPathData = null;
				return false;
			}
			pathPathData = this._battleSideSelectors[(int)side].InitialSpawnPath;
			return pathPathData.IsValid;
		}

		// Token: 0x06001E5E RID: 7774 RVA: 0x00068D1F File Offset: 0x00066F1F
		public MBReadOnlyList<SpawnPathData> GetReinforcementPathsDataOfSide(BattleSideEnum side)
		{
			if (!this.IsInitialized)
			{
				Debug.FailedAssert("BattleSpawnPathSelector must be initialized.", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Deployment\\BattleSpawnPathSelector.cs", "GetReinforcementPathsDataOfSide", 96);
				return null;
			}
			return this._battleSideSelectors[(int)side].ReinforcementPaths;
		}

		// Token: 0x06001E5F RID: 7775 RVA: 0x00068D50 File Offset: 0x00066F50
		public static Path FindBestInitialPath(Mission mission, out float pathPivotOffset, out float pathLength, out bool isPathInverted)
		{
			pathPivotOffset = 0f;
			isPathInverted = false;
			pathLength = 0f;
			MBList<Path> allSpawnPaths = MBSceneUtilities.GetAllSpawnPaths(mission.Scene);
			if (allSpawnPaths.IsEmpty<Path>())
			{
				return null;
			}
			int num = 2;
			foreach (Path path in allSpawnPaths)
			{
				num = MathF.Max(path.NumberOfPoints, num);
			}
			Path path2 = null;
			if (mission.HasSceneMapPatch())
			{
				Path path3 = null;
				bool flag = false;
				float num2 = float.MinValue;
				MatrixFrame[] array = new MatrixFrame[num];
				Vec3 vec;
				mission.GetPatchSceneEncounterPosition(out vec);
				Vec2 asVec = vec.AsVec2;
				Vec2 vec2;
				mission.GetPatchSceneEncounterDirection(out vec2);
				foreach (Path path4 in allSpawnPaths)
				{
					if (path4.NumberOfPoints > 1)
					{
						path4.GetPoints(array);
						float num3 = 0f;
						for (int i = 1; i < path4.NumberOfPoints; i++)
						{
							Vec2 asVec2 = array[i - 1].origin.AsVec2;
							Vec2 vec3 = (array[i].origin.AsVec2 - asVec2).Normalized();
							float num4 = vec2.DotProduct(vec3);
							float num5 = 1000f / (1f + asVec2.Distance(asVec));
							num3 += num5 * num4;
						}
						num3 /= (float)(path4.NumberOfPoints - 1);
						bool flag2 = false;
						if (num3 < 0f)
						{
							num3 = -num3;
							flag2 = true;
						}
						if (num3 >= num2)
						{
							path3 = path4;
							num2 = num3;
							flag = flag2;
						}
					}
				}
				if (path3 != null)
				{
					path3.GetPoints(array);
					float num6 = array[0].origin.AsVec2.DistanceSquared(asVec);
					float num7 = 0f;
					float num8 = 0f;
					for (int j = 1; j < path3.NumberOfPoints; j++)
					{
						float num9 = array[j].origin.AsVec2.DistanceSquared(asVec);
						num8 += path3.GetArcLength(j - 1);
						if (num9 < num6)
						{
							num6 = num9;
							num7 = num8;
						}
					}
					path2 = path3;
					pathLength = path2.GetTotalLength();
					pathPivotOffset = MathF.Clamp(num7, 1f, pathLength - 1f);
					isPathInverted = flag;
				}
			}
			else
			{
				Path randomElement = allSpawnPaths.GetRandomElement<Path>();
				if (randomElement.NumberOfPoints > 1)
				{
					path2 = randomElement;
					float num10 = 0.37f + MBRandom.RandomFloat * 0.26f;
					pathLength = path2.GetTotalLength();
					pathPivotOffset = MathF.Clamp(pathLength * num10, 1f, pathLength - 1f);
					isPathInverted = false;
				}
			}
			return path2;
		}

		// Token: 0x04000A65 RID: 2661
		private readonly Mission _mission;

		// Token: 0x04000A66 RID: 2662
		private Path _initialPath;

		// Token: 0x04000A67 RID: 2663
		private BattleSideSpawnPathSelector[] _battleSideSelectors;
	}
}
