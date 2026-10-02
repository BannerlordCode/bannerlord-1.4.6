using System;
using System.Collections.Generic;
using System.IO;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ModuleManager;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000389 RID: 905
	public class MBInitialScreenBase : ScreenBase, IGameStateListener
	{
		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x060033E7 RID: 13287 RVA: 0x000D5F86 File Offset: 0x000D4186
		// (set) Token: 0x060033E8 RID: 13288 RVA: 0x000D5F8E File Offset: 0x000D418E
		private protected InitialState _state { protected get; private set; }

		// Token: 0x060033E9 RID: 13289 RVA: 0x000D5F97 File Offset: 0x000D4197
		public MBInitialScreenBase(InitialState state)
		{
			this._state = state;
		}

		// Token: 0x060033EA RID: 13290 RVA: 0x000D5FB1 File Offset: 0x000D41B1
		void IGameStateListener.OnActivate()
		{
		}

		// Token: 0x060033EB RID: 13291 RVA: 0x000D5FB3 File Offset: 0x000D41B3
		void IGameStateListener.OnDeactivate()
		{
		}

		// Token: 0x060033EC RID: 13292 RVA: 0x000D5FB5 File Offset: 0x000D41B5
		void IGameStateListener.OnInitialize()
		{
			this._state.OnInitialMenuOptionInvoked += this.OnExecutedInitialStateOption;
		}

		// Token: 0x060033ED RID: 13293 RVA: 0x000D5FCE File Offset: 0x000D41CE
		void IGameStateListener.OnFinalize()
		{
			this._state.OnInitialMenuOptionInvoked -= this.OnExecutedInitialStateOption;
		}

		// Token: 0x060033EE RID: 13294 RVA: 0x000D5FE7 File Offset: 0x000D41E7
		private void OnExecutedInitialStateOption(InitialStateOption target)
		{
		}

		// Token: 0x060033EF RID: 13295 RVA: 0x000D5FE9 File Offset: 0x000D41E9
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this._camera = Camera.CreateCamera();
			Common.MemoryCleanupGC(false);
			if (Game.Current != null)
			{
				Game.Current.Destroy();
			}
			MBMusicManager.Initialize();
		}

		// Token: 0x060033F0 RID: 13296 RVA: 0x000D6018 File Offset: 0x000D4218
		protected override void OnFinalize()
		{
			this._camera = null;
			this._videoPlayerView.SetEnable(false);
			this._videoPlayerView.FinalizePlayer();
			base.OnFinalize();
		}

		// Token: 0x060033F1 RID: 13297 RVA: 0x000D6040 File Offset: 0x000D4240
		protected sealed override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			if (this._videoPlayerView == null)
			{
				Console.WriteLine("InitialScreen::OnFrameTick scene view null");
			}
			LoadingWindow.DisableGlobalLoadingWindow();
			if (Input.IsKeyDown(InputKey.LeftControl) && Input.IsKeyReleased(InputKey.E))
			{
				MBInitialScreenBase.OnEditModeEnterPress();
			}
			if (ScreenManager.TopScreen == this)
			{
				this.OnInitialScreenTick(dt);
			}
			Vec2 screenResolution = MBWindowManager.GetScreenResolution();
			if (this._screenResUsedForVideo != screenResolution)
			{
				this.RefreshVideoAspect(screenResolution);
				this._screenResUsedForVideo = screenResolution;
			}
		}

		// Token: 0x060033F2 RID: 13298 RVA: 0x000D60B9 File Offset: 0x000D42B9
		protected virtual void OnInitialScreenTick(float dt)
		{
			if (this._videoPlayerView == null || !this._isPlayingVideo)
			{
				this.RefreshScene();
			}
		}

		// Token: 0x060033F3 RID: 13299 RVA: 0x000D60D7 File Offset: 0x000D42D7
		protected override void OnActivate()
		{
			base.OnActivate();
			if (Utilities.renderingActive)
			{
				this.RefreshScene();
				Utilities.DisableGlobalLoadingWindow();
			}
			if (NativeConfig.DoLocalizationCheckAtStartup)
			{
				LocalizedTextManager.CheckValidity(new List<string>());
			}
			Module.CurrentModule.SetCanLoadModules(true);
		}

		// Token: 0x060033F4 RID: 13300 RVA: 0x000D6110 File Offset: 0x000D4310
		private void RefreshScene()
		{
			this._isPlayingVideo = false;
			if (this._videoPlayerView != null)
			{
				this._videoPlayerView.StopVideo();
				this._videoPlayerView.FinalizePlayer();
				this._videoPlayerView = null;
			}
			this._videoPlayerView = VideoPlayerView.CreateVideoPlayerView();
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			foreach (ModuleInfo moduleInfo in ModuleHelper.GetActiveModules())
			{
				string text = Path.Combine(moduleInfo.FolderPath, "Videos", "initial_menu").ToString();
				if (Directory.Exists(text))
				{
					string text2 = "*_pc.ivf";
					foreach (string text3 in Directory.GetDirectories(text))
					{
						string[] files = Directory.GetFiles(text3, "*.ogg");
						bool flag = files.Length != 0;
						string[] files2 = Directory.GetFiles(text3, text2);
						bool flag2 = files2.Length != 0;
						if (flag && flag2)
						{
							list.Add(new KeyValuePair<string, string>(files2[0], files[0]));
						}
					}
				}
			}
			float num = 24f;
			string text4 = string.Empty;
			string text5 = string.Empty;
			if (list.Count > 0)
			{
				int count = list.Count;
				int num2 = new Random(DateTime.Now.Second).Next(count);
				text4 = list[num2].Key;
				text5 = list[num2].Value;
				this._videoPlayerView.PlayVideo(text4, text5, num, true);
				this._isPlayingVideo = true;
			}
			Vec2 screenResolution = MBWindowManager.GetScreenResolution();
			this.RefreshVideoAspect(screenResolution);
			Debug.Print(string.Format("Initial Screen: Video is playing after refresh: {0} {1}::{2}", this._isPlayingVideo, text4, text5), 0, Debug.DebugColor.White, 17592186044416UL);
		}

		// Token: 0x060033F5 RID: 13301 RVA: 0x000D62E0 File Offset: 0x000D44E0
		private void RefreshVideoAspect(Vec2 screenRes)
		{
			float num = screenRes.x / screenRes.y;
			if (num > 1.7777778f)
			{
				float num2 = 1f - 1.7777778f / num;
				this._videoPlayerView.SetOffset(new Vec2(num2 * 0.5f, 0f));
				this._videoPlayerView.SetScale(new Vec2(1f - num2, 1f));
				return;
			}
			if (num < 1.7777778f)
			{
				float num3 = screenRes.y / screenRes.x;
				float num4 = 1f - 0.5625f / num3;
				this._videoPlayerView.SetOffset(new Vec2(0f, num4 * 0.5f));
				this._videoPlayerView.SetScale(new Vec2(1f, 1f - num4));
			}
		}

		// Token: 0x060033F6 RID: 13302 RVA: 0x000D63A6 File Offset: 0x000D45A6
		private void OnSceneEditorWindowOpen()
		{
			GameStateManager.Current.CleanAndPushState(GameStateManager.Current.CreateState<EditorState>(), 0);
		}

		// Token: 0x060033F7 RID: 13303 RVA: 0x000D63BD File Offset: 0x000D45BD
		protected override void OnDeactivate()
		{
			base.OnDeactivate();
			this._videoPlayerView.StopVideo();
			Module.CurrentModule.SetCanLoadModules(false);
		}

		// Token: 0x060033F8 RID: 13304 RVA: 0x000D63DB File Offset: 0x000D45DB
		protected override void OnPause()
		{
			LoadingWindow.DisableGlobalLoadingWindow();
			this._videoPlayerView.StopVideo();
			base.OnPause();
		}

		// Token: 0x060033F9 RID: 13305 RVA: 0x000D63F3 File Offset: 0x000D45F3
		protected override void OnResume()
		{
			base.OnResume();
		}

		// Token: 0x060033FA RID: 13306 RVA: 0x000D63FB File Offset: 0x000D45FB
		public static void DoExitButtonAction()
		{
			MBAPI.IMBScreen.OnExitButtonClick();
		}

		// Token: 0x060033FB RID: 13307 RVA: 0x000D6407 File Offset: 0x000D4607
		public bool StartedRendering()
		{
			return true;
		}

		// Token: 0x060033FC RID: 13308 RVA: 0x000D640A File Offset: 0x000D460A
		public static void OnEditModeEnterPress()
		{
			MBAPI.IMBScreen.OnEditModeEnterPress();
		}

		// Token: 0x060033FD RID: 13309 RVA: 0x000D6416 File Offset: 0x000D4616
		public static void OnEditModeEnterRelease()
		{
			MBAPI.IMBScreen.OnEditModeEnterRelease();
		}

		// Token: 0x040015EB RID: 5611
		private Camera _camera;

		// Token: 0x040015EC RID: 5612
		protected VideoPlayerView _videoPlayerView;

		// Token: 0x040015ED RID: 5613
		private Vec2 _screenResUsedForVideo = Vec2.Zero;

		// Token: 0x040015EF RID: 5615
		private bool _isPlayingVideo;
	}
}
