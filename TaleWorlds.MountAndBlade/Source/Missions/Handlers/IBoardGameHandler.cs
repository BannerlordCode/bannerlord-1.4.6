using System;

namespace TaleWorlds.MountAndBlade.Source.Missions.Handlers
{
	// Token: 0x020003D9 RID: 985
	public interface IBoardGameHandler
	{
		// Token: 0x0600367F RID: 13951
		void SwitchTurns();

		// Token: 0x06003680 RID: 13952
		void DiceRoll(int roll);

		// Token: 0x06003681 RID: 13953
		void Install();

		// Token: 0x06003682 RID: 13954
		void Uninstall();

		// Token: 0x06003683 RID: 13955
		void Activate();
	}
}
