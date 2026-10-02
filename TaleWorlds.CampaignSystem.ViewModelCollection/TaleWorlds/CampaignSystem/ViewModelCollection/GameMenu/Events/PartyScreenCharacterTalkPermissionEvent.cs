using System;
using TaleWorlds.Library.EventSystem;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.Events
{
	// Token: 0x020000BE RID: 190
	public class PartyScreenCharacterTalkPermissionEvent : EventBase
	{
		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x0600131B RID: 4891 RVA: 0x0004DAE6 File Offset: 0x0004BCE6
		// (set) Token: 0x0600131C RID: 4892 RVA: 0x0004DAEE File Offset: 0x0004BCEE
		public Action<bool, TextObject> IsTalkAvailable { get; private set; }

		// Token: 0x0600131D RID: 4893 RVA: 0x0004DAF7 File Offset: 0x0004BCF7
		public PartyScreenCharacterTalkPermissionEvent(Hero heroToTalkTo, Action<bool, TextObject> isTalkAvailable)
		{
			this.HeroToTalkTo = heroToTalkTo;
			this.IsTalkAvailable = isTalkAvailable;
		}

		// Token: 0x040008B2 RID: 2226
		public Hero HeroToTalkTo;
	}
}
