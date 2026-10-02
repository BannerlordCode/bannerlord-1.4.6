using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000387 RID: 903
	public static class ScreenFadeController
	{
		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x060033DB RID: 13275 RVA: 0x000D5EC8 File Offset: 0x000D40C8
		public static bool IsFadeActive
		{
			get
			{
				return ScreenFadeController._handler != null && ScreenFadeController._handler.GetScreenFadeState() > ScreenFadeController.ScreenFadeState.None;
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x060033DC RID: 13276 RVA: 0x000D5EE0 File Offset: 0x000D40E0
		public static bool IsFadingOut
		{
			get
			{
				return ScreenFadeController._handler != null && ScreenFadeController._handler.GetScreenFadeState() == ScreenFadeController.ScreenFadeState.FadingOut;
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x060033DD RID: 13277 RVA: 0x000D5EF8 File Offset: 0x000D40F8
		public static bool IsFadingIn
		{
			get
			{
				return ScreenFadeController._handler != null && ScreenFadeController._handler.GetScreenFadeState() == ScreenFadeController.ScreenFadeState.FadingIn;
			}
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x060033DE RID: 13278 RVA: 0x000D5F10 File Offset: 0x000D4110
		public static bool IsFadedOut
		{
			get
			{
				return ScreenFadeController._handler != null && ScreenFadeController._handler.GetScreenFadeState() == ScreenFadeController.ScreenFadeState.FadedOut;
			}
		}

		// Token: 0x060033DF RID: 13279 RVA: 0x000D5F28 File Offset: 0x000D4128
		public static void RegisterHandler(IScreenFadeHandler handler)
		{
			if (ScreenFadeController._handler == null)
			{
				ScreenFadeController._handler = handler;
				return;
			}
			Debug.FailedAssert("ScreenFade handler already registered!", "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\ScreenFadeController.cs", "RegisterHandler", 30);
		}

		// Token: 0x060033E0 RID: 13280 RVA: 0x000D5F4E File Offset: 0x000D414E
		public static void BeginFadeOutAndIn(float fadeOutDuration = 0.5f, float blackOutDuration = 0.5f, float fadeInDuration = 0.5f)
		{
			IScreenFadeHandler handler = ScreenFadeController._handler;
			if (handler == null)
			{
				return;
			}
			handler.BeginFadeOutAndIn(fadeOutDuration, blackOutDuration, fadeInDuration);
		}

		// Token: 0x060033E1 RID: 13281 RVA: 0x000D5F62 File Offset: 0x000D4162
		public static void BeginFadeOut(float fadeOutDuration = 0.5f)
		{
			IScreenFadeHandler handler = ScreenFadeController._handler;
			if (handler == null)
			{
				return;
			}
			handler.BeginFadeOut(fadeOutDuration);
		}

		// Token: 0x060033E2 RID: 13282 RVA: 0x000D5F74 File Offset: 0x000D4174
		public static void BeginFadeIn(float fadeInDuration = 0.5f)
		{
			IScreenFadeHandler handler = ScreenFadeController._handler;
			if (handler == null)
			{
				return;
			}
			handler.BeginFadeIn(fadeInDuration);
		}

		// Token: 0x040015EA RID: 5610
		private static IScreenFadeHandler _handler;

		// Token: 0x0200065C RID: 1628
		public enum ScreenFadeState
		{
			// Token: 0x040021A1 RID: 8609
			None,
			// Token: 0x040021A2 RID: 8610
			FadingOut,
			// Token: 0x040021A3 RID: 8611
			FadedOut,
			// Token: 0x040021A4 RID: 8612
			FadingIn
		}
	}
}
