using System;

namespace TaleWorlds.Core
{
	// Token: 0x02000088 RID: 136
	public interface IGameStateManagerOwner
	{
		// Token: 0x060008A5 RID: 2213
		void OnStateStackEmpty();

		// Token: 0x060008A6 RID: 2214
		void OnStateChanged(GameState oldState);
	}
}
