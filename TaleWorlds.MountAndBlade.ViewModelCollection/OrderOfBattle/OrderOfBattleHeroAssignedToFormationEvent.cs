using System;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.OrderOfBattle
{
	// Token: 0x02000037 RID: 55
	public class OrderOfBattleHeroAssignedToFormationEvent : EventBase
	{
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x000138C2 File Offset: 0x00011AC2
		// (set) Token: 0x060004D9 RID: 1241 RVA: 0x000138CA File Offset: 0x00011ACA
		public Agent AssignedHero { get; private set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x000138D3 File Offset: 0x00011AD3
		// (set) Token: 0x060004DB RID: 1243 RVA: 0x000138DB File Offset: 0x00011ADB
		public Formation AssignedFormation { get; private set; }

		// Token: 0x060004DC RID: 1244 RVA: 0x000138E4 File Offset: 0x00011AE4
		public OrderOfBattleHeroAssignedToFormationEvent(Agent assignedHero, Formation assignedFormation)
		{
			this.AssignedHero = assignedHero;
			this.AssignedFormation = assignedFormation;
		}
	}
}
