using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000318 RID: 792
	public static class RoundStateExtensions
	{
		// Token: 0x06002D30 RID: 11568 RVA: 0x000AF538 File Offset: 0x000AD738
		public static bool StateHasVisualTimer(this MultiplayerRoundState roundState)
		{
			return roundState - MultiplayerRoundState.Preparation <= 1;
		}
	}
}
