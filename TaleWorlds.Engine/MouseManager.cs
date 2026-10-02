using System;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.Engine
{
	// Token: 0x0200006D RID: 109
	public static class MouseManager
	{
		// Token: 0x06000A37 RID: 2615 RVA: 0x0000A5A2 File Offset: 0x000087A2
		public static void ActivateMouseCursor(CursorType mouseId)
		{
			EngineApplicationInterface.IMouseManager.ActivateMouseCursor((int)mouseId);
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0000A5AF File Offset: 0x000087AF
		public static void SetMouseCursor(CursorType mouseId, string mousePath)
		{
			EngineApplicationInterface.IMouseManager.SetMouseCursor((int)mouseId, mousePath);
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0000A5BD File Offset: 0x000087BD
		public static void ShowCursor(bool show)
		{
			EngineApplicationInterface.IMouseManager.ShowCursor(show);
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0000A5CA File Offset: 0x000087CA
		public static void LockCursorAtCurrentPosition(bool lockCursor)
		{
			EngineApplicationInterface.IMouseManager.LockCursorAtCurrentPosition(lockCursor);
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0000A5D7 File Offset: 0x000087D7
		public static void LockCursorAtPosition(float x, float y)
		{
			EngineApplicationInterface.IMouseManager.LockCursorAtPosition(x, y);
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0000A5E5 File Offset: 0x000087E5
		public static void UnlockCursor()
		{
			EngineApplicationInterface.IMouseManager.UnlockCursor();
		}
	}
}
