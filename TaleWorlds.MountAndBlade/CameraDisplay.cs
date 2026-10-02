using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000329 RID: 809
	public class CameraDisplay : ScriptComponentBehavior
	{
		// Token: 0x06002DCE RID: 11726 RVA: 0x000B0EF8 File Offset: 0x000AF0F8
		private void BuildView()
		{
			this._sceneView = SceneView.CreateSceneView();
			this._myCamera = Camera.CreateCamera();
			this._sceneView.SetScene(base.GameEntity.Scene);
			this._sceneView.SetPostfxFromConfig();
			this._sceneView.SetRenderOption(View.ViewRenderOptions.ClearColor, false);
			this._sceneView.SetRenderOption(View.ViewRenderOptions.ClearDepth, true);
			this._sceneView.SetScale(new Vec2(0.2f, 0.2f));
		}

		// Token: 0x06002DCF RID: 11727 RVA: 0x000B0F74 File Offset: 0x000AF174
		private void SetCamera()
		{
			Vec2 realScreenResolution = Screen.RealScreenResolution;
			float num = realScreenResolution.x / realScreenResolution.y;
			MatrixFrame globalFrame = base.GameEntity.GetGlobalFrame();
			this._myCamera.SetFovVertical(0.7853982f, num, 0.2f, 200f);
			this._myCamera.Frame = globalFrame;
			this._sceneView.SetCamera(this._myCamera);
		}

		// Token: 0x06002DD0 RID: 11728 RVA: 0x000B0FDC File Offset: 0x000AF1DC
		private void RenderCameraFrustrum()
		{
			this._myCamera.RenderFrustrum();
		}

		// Token: 0x06002DD1 RID: 11729 RVA: 0x000B0FE9 File Offset: 0x000AF1E9
		protected internal override void OnEditorInit()
		{
			this.BuildView();
		}

		// Token: 0x06002DD2 RID: 11730 RVA: 0x000B0FF1 File Offset: 0x000AF1F1
		protected internal override void OnInit()
		{
			this.BuildView();
		}

		// Token: 0x06002DD3 RID: 11731 RVA: 0x000B0FF9 File Offset: 0x000AF1F9
		protected internal override void OnEditorTick(float dt)
		{
			base.OnEditorTick(dt);
			if (MBEditor.IsEntitySelected(base.GameEntity))
			{
				this.RenderCameraFrustrum();
				this._sceneView.SetEnable(true);
				return;
			}
			this._sceneView.SetEnable(false);
		}

		// Token: 0x06002DD4 RID: 11732 RVA: 0x000B102E File Offset: 0x000AF22E
		protected override void OnRemoved(int removeReason)
		{
			base.OnRemoved(removeReason);
			this._sceneView = null;
			this._myCamera = null;
		}

		// Token: 0x0400120F RID: 4623
		private Camera _myCamera;

		// Token: 0x04001210 RID: 4624
		private SceneView _sceneView;

		// Token: 0x04001211 RID: 4625
		public int renderOrder;
	}
}
