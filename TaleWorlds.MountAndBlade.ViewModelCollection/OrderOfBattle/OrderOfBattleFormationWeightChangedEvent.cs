using System;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.OrderOfBattle
{
	// Token: 0x02000039 RID: 57
	public class OrderOfBattleFormationWeightChangedEvent : EventBase
	{
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x0001391A File Offset: 0x00011B1A
		// (set) Token: 0x060004E1 RID: 1249 RVA: 0x00013922 File Offset: 0x00011B22
		public Formation Formation { get; private set; }

		// Token: 0x060004E2 RID: 1250 RVA: 0x0001392B File Offset: 0x00011B2B
		public OrderOfBattleFormationWeightChangedEvent(Formation formation)
		{
			this.Formation = formation;
		}
	}
}
