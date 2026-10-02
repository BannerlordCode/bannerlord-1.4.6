using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001C3 RID: 451
	public static class DebugExtensions
	{
		// Token: 0x06001B21 RID: 6945 RVA: 0x0005F4BE File Offset: 0x0005D6BE
		public static void RenderDebugCircleOnTerrain(Scene scene, MatrixFrame frame, float radius, uint color, bool depthCheck = true, bool isDotted = false)
		{
			MBAPI.IMBDebugExtensions.RenderDebugCircleOnTerrain(scene.Pointer, ref frame, radius, color, depthCheck, isDotted);
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x0005F4D8 File Offset: 0x0005D6D8
		public static void RenderDebugArcOnTerrain(Scene scene, MatrixFrame frame, float radius, float beginAngle, float endAngle, uint color, bool depthCheck = true, bool isDotted = false)
		{
			MBAPI.IMBDebugExtensions.RenderDebugArcOnTerrain(scene.Pointer, ref frame, radius, beginAngle, endAngle, color, depthCheck, isDotted);
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x0005F504 File Offset: 0x0005D704
		public static void RenderDebugLineOnTerrain(Scene scene, Vec3 position, Vec3 direction, uint color, bool depthCheck = true, float time = 0f, bool isDotted = false, float pointDensity = 1f)
		{
			MBAPI.IMBDebugExtensions.RenderDebugLineOnTerrain(scene.Pointer, position, direction, color, depthCheck, time, isDotted, pointDensity);
		}
	}
}
