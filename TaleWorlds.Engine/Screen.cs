using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000085 RID: 133
	public static class Screen
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x0000D3FF File Offset: 0x0000B5FF
		// (set) Token: 0x06000C11 RID: 3089 RVA: 0x0000D406 File Offset: 0x0000B606
		public static float RealScreenResolutionWidth { get; private set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x0000D40E File Offset: 0x0000B60E
		// (set) Token: 0x06000C13 RID: 3091 RVA: 0x0000D415 File Offset: 0x0000B615
		public static float RealScreenResolutionHeight { get; private set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x0000D41D File Offset: 0x0000B61D
		public static Vec2 RealScreenResolution
		{
			get
			{
				return new Vec2(Screen.RealScreenResolutionWidth, Screen.RealScreenResolutionHeight);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x0000D42E File Offset: 0x0000B62E
		// (set) Token: 0x06000C16 RID: 3094 RVA: 0x0000D435 File Offset: 0x0000B635
		public static float AspectRatio { get; private set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x0000D43D File Offset: 0x0000B63D
		// (set) Token: 0x06000C18 RID: 3096 RVA: 0x0000D444 File Offset: 0x0000B644
		public static Vec2 DesktopResolution { get; private set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000C19 RID: 3097 RVA: 0x0000D44C File Offset: 0x0000B64C
		// (set) Token: 0x06000C1A RID: 3098 RVA: 0x0000D453 File Offset: 0x0000B653
		public static Vec2 ScreenScale { get; private set; }

		// Token: 0x06000C1B RID: 3099 RVA: 0x0000D45C File Offset: 0x0000B65C
		internal static void Update()
		{
			Screen.RealScreenResolutionWidth = EngineApplicationInterface.IScreen.GetRealScreenResolutionWidth();
			Screen.RealScreenResolutionHeight = EngineApplicationInterface.IScreen.GetRealScreenResolutionHeight();
			Screen.AspectRatio = EngineApplicationInterface.IScreen.GetAspectRatio();
			Screen.DesktopResolution = new Vec2(EngineApplicationInterface.IScreen.GetDesktopWidth(), EngineApplicationInterface.IScreen.GetDesktopHeight());
			Screen.ScreenScale = new Vec2(Screen.RealScreenResolutionWidth / Screen.DesktopResolution.x, Screen.RealScreenResolutionHeight / Screen.DesktopResolution.y);
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x0000D4DE File Offset: 0x0000B6DE
		public static bool GetMouseVisible()
		{
			return EngineApplicationInterface.IScreen.GetMouseVisible();
		}
	}
}
