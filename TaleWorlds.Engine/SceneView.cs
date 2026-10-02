using System;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000084 RID: 132
	[EngineClass("rglScene_view")]
	public class SceneView : View
	{
		// Token: 0x06000BF0 RID: 3056 RVA: 0x0000D162 File Offset: 0x0000B362
		internal SceneView(UIntPtr meshPointer)
			: base(meshPointer)
		{
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x0000D16B File Offset: 0x0000B36B
		public static SceneView CreateSceneView()
		{
			return EngineApplicationInterface.ISceneView.CreateSceneView();
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0000D177 File Offset: 0x0000B377
		public void SetScene(Scene scene)
		{
			EngineApplicationInterface.ISceneView.SetScene(base.Pointer, scene.Pointer);
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x0000D18F File Offset: 0x0000B38F
		public void SetAcceptGlobalDebugRenderObjects(bool value)
		{
			EngineApplicationInterface.ISceneView.SetAcceptGlobalDebugRenderObjects(base.Pointer, value);
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x0000D1A2 File Offset: 0x0000B3A2
		public void SetRenderWithPostfx(bool value)
		{
			EngineApplicationInterface.ISceneView.SetRenderWithPostfx(base.Pointer, value);
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0000D1B5 File Offset: 0x0000B3B5
		public void SetPostfxConfigParams(int value)
		{
			EngineApplicationInterface.ISceneView.SetPostfxConfigParams(base.Pointer, value);
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0000D1C8 File Offset: 0x0000B3C8
		public void SetForceShaderCompilation(bool value)
		{
			EngineApplicationInterface.ISceneView.SetForceShaderCompilation(base.Pointer, value);
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x0000D1DB File Offset: 0x0000B3DB
		public bool CheckSceneReadyToRender()
		{
			return EngineApplicationInterface.ISceneView.CheckSceneReadyToRender(base.Pointer);
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0000D1ED File Offset: 0x0000B3ED
		public void SetDoQuickExposure(bool value)
		{
			EngineApplicationInterface.ISceneView.SetDoQuickExposure(base.Pointer, value);
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0000D200 File Offset: 0x0000B400
		public void SetCamera(Camera camera)
		{
			EngineApplicationInterface.ISceneView.SetCamera(base.Pointer, camera.Pointer);
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x0000D218 File Offset: 0x0000B418
		public void SetResolutionScaling(bool value)
		{
			EngineApplicationInterface.ISceneView.SetResolutionScaling(base.Pointer, value);
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x0000D22B File Offset: 0x0000B42B
		public void SetPostfxFromConfig()
		{
			EngineApplicationInterface.ISceneView.SetPostfxFromConfig(base.Pointer);
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0000D23D File Offset: 0x0000B43D
		public Vec2 WorldPointToScreenPoint(Vec3 position)
		{
			return EngineApplicationInterface.ISceneView.WorldPointToScreenPoint(base.Pointer, position);
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x0000D250 File Offset: 0x0000B450
		public Vec2 ScreenPointToViewportPoint(Vec2 position)
		{
			return EngineApplicationInterface.ISceneView.ScreenPointToViewportPoint(base.Pointer, position.x, position.y);
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x0000D26E File Offset: 0x0000B46E
		public bool ProjectedMousePositionOnGround(out Vec3 groundPosition, out Vec3 groundNormal, bool mouseVisible, BodyFlags excludeBodyOwnerFlags, bool checkOccludedSurface)
		{
			return EngineApplicationInterface.ISceneView.ProjectedMousePositionOnGround(base.Pointer, out groundPosition, out groundNormal, mouseVisible, excludeBodyOwnerFlags, checkOccludedSurface);
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x0000D287 File Offset: 0x0000B487
		public bool ProjectedMousePositionOnWater(out Vec3 waterPosition, bool mouseVisible)
		{
			return EngineApplicationInterface.ISceneView.ProjectedMousePositionOnWater(base.Pointer, out waterPosition, mouseVisible);
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x0000D29B File Offset: 0x0000B49B
		public void TranslateMouse(ref Vec3 worldMouseNear, ref Vec3 worldMouseFar, float maxDistance = -1f)
		{
			EngineApplicationInterface.ISceneView.TranslateMouse(base.Pointer, ref worldMouseNear, ref worldMouseFar, maxDistance);
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x0000D2B0 File Offset: 0x0000B4B0
		public void SetSceneUsesSkybox(bool value)
		{
			EngineApplicationInterface.ISceneView.SetSceneUsesSkybox(base.Pointer, value);
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x0000D2C3 File Offset: 0x0000B4C3
		public void SetSceneUsesShadows(bool value)
		{
			EngineApplicationInterface.ISceneView.SetSceneUsesShadows(base.Pointer, value);
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x0000D2D6 File Offset: 0x0000B4D6
		public void SetSceneUsesContour(bool value)
		{
			EngineApplicationInterface.ISceneView.SetSceneUsesContour(base.Pointer, value);
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0000D2E9 File Offset: 0x0000B4E9
		public void DoNotClear(bool value)
		{
			EngineApplicationInterface.ISceneView.DoNotClear(base.Pointer, value);
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x0000D2FC File Offset: 0x0000B4FC
		public void AddClearTask(bool clearOnlySceneview = false)
		{
			EngineApplicationInterface.ISceneView.AddClearTask(base.Pointer, clearOnlySceneview);
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x0000D30F File Offset: 0x0000B50F
		public bool ReadyToRender()
		{
			return EngineApplicationInterface.ISceneView.ReadyToRender(base.Pointer);
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0000D321 File Offset: 0x0000B521
		public void SetClearAndDisableAfterSucessfullRender(bool value)
		{
			EngineApplicationInterface.ISceneView.SetClearAndDisableAfterSucessfullRender(base.Pointer, value);
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x0000D334 File Offset: 0x0000B534
		public void SetClearGbuffer(bool value)
		{
			EngineApplicationInterface.ISceneView.SetClearGbuffer(base.Pointer, value);
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0000D347 File Offset: 0x0000B547
		public void SetShadowmapResolutionMultiplier(float value)
		{
			EngineApplicationInterface.ISceneView.SetShadowmapResolutionMultiplier(base.Pointer, value);
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0000D35A File Offset: 0x0000B55A
		public void SetPointlightResolutionMultiplier(float value)
		{
			EngineApplicationInterface.ISceneView.SetPointlightResolutionMultiplier(base.Pointer, value);
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x0000D36D File Offset: 0x0000B56D
		public void SetCleanScreenUntilLoadingDone(bool value)
		{
			EngineApplicationInterface.ISceneView.SetCleanScreenUntilLoadingDone(base.Pointer, value);
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0000D380 File Offset: 0x0000B580
		public void ClearAll(bool clearScene, bool removeTerrain)
		{
			EngineApplicationInterface.ISceneView.ClearAll(base.Pointer, clearScene, removeTerrain);
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0000D394 File Offset: 0x0000B594
		public void SetFocusedShadowmap(bool enable, ref Vec3 center, float radius)
		{
			EngineApplicationInterface.ISceneView.SetFocusedShadowmap(base.Pointer, enable, ref center, radius);
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0000D3A9 File Offset: 0x0000B5A9
		public Scene GetScene()
		{
			return EngineApplicationInterface.ISceneView.GetScene(base.Pointer);
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0000D3BC File Offset: 0x0000B5BC
		public bool RayCastForClosestEntityOrTerrain(Vec3 sourcePoint, Vec3 targetPoint, out float collisionDistance, out Vec3 closestPoint, float rayThickness = 0.01f, BodyFlags excludeBodyFlags = BodyFlags.CommonFocusRayCastExcludeFlags)
		{
			collisionDistance = float.NaN;
			closestPoint = Vec3.Invalid;
			UIntPtr zero = UIntPtr.Zero;
			return EngineApplicationInterface.ISceneView.RayCastForClosestEntityOrTerrain(base.Pointer, ref sourcePoint, ref targetPoint, rayThickness, ref collisionDistance, ref closestPoint, ref zero, excludeBodyFlags);
		}
	}
}
