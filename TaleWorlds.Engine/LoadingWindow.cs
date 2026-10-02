using System;

namespace TaleWorlds.Engine
{
	// Token: 0x02000056 RID: 86
	public static class LoadingWindow
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x00006F0C File Offset: 0x0000510C
		// (set) Token: 0x060008B7 RID: 2231 RVA: 0x00006F13 File Offset: 0x00005113
		public static bool IsLoadingWindowActive { get; private set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x00006F1B File Offset: 0x0000511B
		// (set) Token: 0x060008B9 RID: 2233 RVA: 0x00006F22 File Offset: 0x00005122
		public static ILoadingWindowManager LoadingWindowManager { get; private set; }

		// Token: 0x060008BB RID: 2235 RVA: 0x00006F2C File Offset: 0x0000512C
		public static void InitializeWith<T>() where T : class, ILoadingWindowManager, new()
		{
			LoadingWindow.Destroy();
			LoadingWindow.LoadingWindowManager = new T();
			LoadingWindow.LoadingWindowManager.Initialize();
			if (LoadingWindow.IsLoadingWindowActive)
			{
				LoadingWindow.LoadingWindowManager.EnableLoadingWindow();
			}
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00006F5D File Offset: 0x0000515D
		public static void Destroy()
		{
			ILoadingWindowManager loadingWindowManager = LoadingWindow.LoadingWindowManager;
			if (loadingWindowManager != null)
			{
				loadingWindowManager.DisableLoadingWindow();
			}
			ILoadingWindowManager loadingWindowManager2 = LoadingWindow.LoadingWindowManager;
			if (loadingWindowManager2 != null)
			{
				loadingWindowManager2.Destroy();
			}
			LoadingWindow.LoadingWindowManager = null;
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00006F85 File Offset: 0x00005185
		public static void DisableGlobalLoadingWindow()
		{
			if (LoadingWindow.LoadingWindowManager == null)
			{
				return;
			}
			if (LoadingWindow.IsLoadingWindowActive)
			{
				LoadingWindow.LoadingWindowManager.DisableLoadingWindow();
				Utilities.DisableGlobalLoadingWindow();
				Utilities.OnLoadingWindowDisabled();
			}
			LoadingWindow.IsLoadingWindowActive = false;
			Utilities.DebugSetGlobalLoadingWindowState(false);
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00006FB6 File Offset: 0x000051B6
		public static void EnableGlobalLoadingWindow()
		{
			if (LoadingWindow.LoadingWindowManager == null)
			{
				return;
			}
			LoadingWindow.IsLoadingWindowActive = true;
			Utilities.DebugSetGlobalLoadingWindowState(true);
			if (LoadingWindow.IsLoadingWindowActive)
			{
				LoadingWindow.LoadingWindowManager.EnableLoadingWindow();
				Utilities.OnLoadingWindowEnabled();
			}
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00006FE2 File Offset: 0x000051E2
		public static void SetCurrentModeIsMultiplayer(bool isMultiplayer)
		{
			ILoadingWindowManager loadingWindowManager = LoadingWindow.LoadingWindowManager;
			if (loadingWindowManager == null)
			{
				return;
			}
			loadingWindowManager.SetCurrentModeIsMultiplayer(isMultiplayer);
		}
	}
}
