using System;

namespace TaleWorlds.Core
{
	// Token: 0x0200001F RID: 31
	public static class BattleSideEnumExtensions
	{
		// Token: 0x0600018D RID: 397 RVA: 0x000069FC File Offset: 0x00004BFC
		public static bool IsValid(this BattleSideEnum battleSide)
		{
			return battleSide >= BattleSideEnum.Defender && battleSide < BattleSideEnum.NumSides;
		}
	}
}
