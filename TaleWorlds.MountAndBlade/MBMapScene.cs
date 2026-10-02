using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001CD RID: 461
	public static class MBMapScene
	{
		// Token: 0x06001B9E RID: 7070 RVA: 0x000602B7 File Offset: 0x0005E4B7
		public static Vec2 GetNearestFaceCenterForPosition(Scene mapScene, Vec2 position, bool isRegionMap0, int[] excludedFaceIds)
		{
			return MBAPI.IMBMapScene.GetNearestFaceCenterPositionForPosition(mapScene.Pointer, position.ToVec3(0f), isRegionMap0, excludedFaceIds, excludedFaceIds.Length);
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x000602DA File Offset: 0x0005E4DA
		public static Vec2 GetNearestFaceCenterForPositionWithPath(Scene mapScene, PathFaceRecord pathFaceRecord, bool targetRegionMap0, float maxDist, int[] excludedFaceIds)
		{
			return MBAPI.IMBMapScene.GetNearestFaceCenterForPositionWithPath(mapScene.Pointer, pathFaceRecord.FaceIndex, targetRegionMap0, maxDist, excludedFaceIds, excludedFaceIds.Length);
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x000602FC File Offset: 0x0005E4FC
		public static Vec2 GetAccessiblePointNearPosition(Scene mapScene, Vec2 position, bool isRegionMap1, float radius)
		{
			return MBAPI.IMBMapScene.GetAccessiblePointNearPosition(mapScene.Pointer, position, isRegionMap1, radius).AsVec2;
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x00060324 File Offset: 0x0005E524
		public static void RemoveZeroCornerBodies(Scene mapScene)
		{
			MBAPI.IMBMapScene.RemoveZeroCornerBodies(mapScene.Pointer);
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x00060336 File Offset: 0x0005E536
		public static void LoadAtmosphereData(Scene mapScene)
		{
			MBAPI.IMBMapScene.LoadAtmosphereData(mapScene.Pointer);
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x00060348 File Offset: 0x0005E548
		public static void TickStepSound(Scene mapScene, MBAgentVisuals visuals, int terrainType, TerrainTypeSoundSlot soundType, int partySize)
		{
			MBAPI.IMBMapScene.TickStepSound(mapScene.Pointer, visuals.Pointer, terrainType, soundType, partySize);
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x00060364 File Offset: 0x0005E564
		public static void TickAmbientSounds(Scene mapScene, int terrainType)
		{
			MBAPI.IMBMapScene.TickAmbientSounds(mapScene.Pointer, terrainType);
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x00060377 File Offset: 0x0005E577
		public static bool GetMouseVisible()
		{
			return MBAPI.IMBMapScene.GetMouseVisible();
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x00060383 File Offset: 0x0005E583
		public static bool GetApplyRainColorGrade()
		{
			return MBMapScene.ApplyRainColorGrade;
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x0006038A File Offset: 0x0005E58A
		public static void SendMouseKeyEvent(int mouseKeyId, bool isDown)
		{
			MBAPI.IMBMapScene.SendMouseKeyEvent(mouseKeyId, isDown);
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x00060398 File Offset: 0x0005E598
		public static void SetMousePos(int posX, int posY)
		{
			MBAPI.IMBMapScene.SetMousePos(posX, posY);
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x000603A8 File Offset: 0x0005E5A8
		public static void TickVisuals(Scene mapScene, float tod, Mesh[] tickedMapMeshes)
		{
			for (int i = 0; i < tickedMapMeshes.Length; i++)
			{
				MBMapScene._tickedMapMeshesCachedArray[i] = tickedMapMeshes[i].Pointer;
			}
			MBAPI.IMBMapScene.TickVisuals(mapScene.Pointer, tod, MBMapScene._tickedMapMeshesCachedArray, tickedMapMeshes.Length);
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x000603EB File Offset: 0x0005E5EB
		public static void ValidateTerrainSoundIds()
		{
			MBAPI.IMBMapScene.ValidateTerrainSoundIds();
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x000603F7 File Offset: 0x0005E5F7
		public static void GetGlobalIlluminationOfString(Scene mapScene, string value)
		{
			MBAPI.IMBMapScene.SetPoliticalColor(mapScene.Pointer, value);
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x0006040A File Offset: 0x0005E60A
		public static void GetColorGradeGridData(Scene mapScene, byte[] gridData, string textureName)
		{
			MBAPI.IMBMapScene.GetColorGradeGridData(mapScene.Pointer, gridData, textureName);
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x00060420 File Offset: 0x0005E620
		public static void GetBattleSceneIndexMap(Scene mapScene, ref byte[] indexData, ref int width, ref int height)
		{
			MBAPI.IMBMapScene.GetBattleSceneIndexMapResolution(mapScene.Pointer, ref width, ref height);
			int num = width * height * 2;
			if (indexData == null || indexData.Length != num)
			{
				indexData = new byte[num];
			}
			MBAPI.IMBMapScene.GetBattleSceneIndexMap(mapScene.Pointer, indexData);
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x0006046C File Offset: 0x0005E66C
		public static void SetFrameForAtmosphere(Scene mapScene, float tod, float cameraElevation, bool forceLoadTextures)
		{
			MBAPI.IMBMapScene.SetFrameForAtmosphere(mapScene.Pointer, tod, cameraElevation, forceLoadTextures);
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x00060481 File Offset: 0x0005E681
		public static void SetTerrainDynamicParams(Scene mapScene, Vec3 dynamic_params)
		{
			MBAPI.IMBMapScene.SetTerrainDynamicParams(mapScene.Pointer, dynamic_params);
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x00060494 File Offset: 0x0005E694
		public static void SetSeasonTimeFactor(Scene mapScene, float seasonTimeFactor)
		{
			MBAPI.IMBMapScene.SetSeasonTimeFactor(mapScene.Pointer, seasonTimeFactor);
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x000604A7 File Offset: 0x0005E6A7
		public static float GetSeasonTimeFactor(Scene mapScene)
		{
			return MBAPI.IMBMapScene.GetSeasonTimeFactor(mapScene.Pointer);
		}

		// Token: 0x0400090D RID: 2317
		public static bool ApplyRainColorGrade;

		// Token: 0x0400090E RID: 2318
		private static UIntPtr[] _tickedMapMeshesCachedArray = new UIntPtr[1024];
	}
}
