using System;

namespace TaleWorlds.Core
{
	// Token: 0x02000087 RID: 135
	public interface IGameStateManagerListener
	{
		// Token: 0x060008A0 RID: 2208
		void OnCreateState(GameState gameState);

		// Token: 0x060008A1 RID: 2209
		void OnPushState(GameState gameState, bool isTopGameState);

		// Token: 0x060008A2 RID: 2210
		void OnPopState(GameState gameState);

		// Token: 0x060008A3 RID: 2211
		void OnCleanStates();

		// Token: 0x060008A4 RID: 2212
		void OnSavedGameLoadFinished();
	}
}
