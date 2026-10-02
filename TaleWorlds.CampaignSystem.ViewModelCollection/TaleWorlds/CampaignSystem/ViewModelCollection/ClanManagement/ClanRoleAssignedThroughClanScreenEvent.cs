using System;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement
{
	// Token: 0x0200012E RID: 302
	public class ClanRoleAssignedThroughClanScreenEvent : EventBase
	{
		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06001C55 RID: 7253 RVA: 0x00068EB9 File Offset: 0x000670B9
		// (set) Token: 0x06001C56 RID: 7254 RVA: 0x00068EC1 File Offset: 0x000670C1
		public PartyRole Role { get; private set; }

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06001C57 RID: 7255 RVA: 0x00068ECA File Offset: 0x000670CA
		// (set) Token: 0x06001C58 RID: 7256 RVA: 0x00068ED2 File Offset: 0x000670D2
		public Hero HeroObject { get; private set; }

		// Token: 0x06001C59 RID: 7257 RVA: 0x00068EDB File Offset: 0x000670DB
		public ClanRoleAssignedThroughClanScreenEvent(PartyRole role, Hero heroObject)
		{
			this.Role = role;
			this.HeroObject = heroObject;
		}
	}
}
