using System;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ArmyManagement
{
	// Token: 0x0200015F RID: 351
	public class PartyAddedToArmyByPlayerEvent : EventBase
	{
		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x060021F5 RID: 8693 RVA: 0x0007B1C0 File Offset: 0x000793C0
		// (set) Token: 0x060021F6 RID: 8694 RVA: 0x0007B1C8 File Offset: 0x000793C8
		public MobileParty AddedParty { get; private set; }

		// Token: 0x060021F7 RID: 8695 RVA: 0x0007B1D1 File Offset: 0x000793D1
		public PartyAddedToArmyByPlayerEvent(MobileParty addedParty)
		{
			this.AddedParty = addedParty;
		}
	}
}
