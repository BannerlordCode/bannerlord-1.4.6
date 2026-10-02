using System;

namespace TaleWorlds.Engine
{
	// Token: 0x02000095 RID: 149
	public static class Time
	{
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000D21 RID: 3361 RVA: 0x0000EB2C File Offset: 0x0000CD2C
		public static float ApplicationTime
		{
			get
			{
				return EngineApplicationInterface.ITime.GetApplicationTime();
			}
		}
	}
}
