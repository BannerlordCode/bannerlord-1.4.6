using System;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.OrderOfBattle
{
	// Token: 0x02000038 RID: 56
	public class OrderOfBattleFormationClassChangedEvent : EventBase
	{
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x000138FA File Offset: 0x00011AFA
		// (set) Token: 0x060004DE RID: 1246 RVA: 0x00013902 File Offset: 0x00011B02
		public Formation Formation { get; private set; }

		// Token: 0x060004DF RID: 1247 RVA: 0x0001390B File Offset: 0x00011B0B
		public OrderOfBattleFormationClassChangedEvent(Formation formation)
		{
			this.Formation = formation;
		}
	}
}
