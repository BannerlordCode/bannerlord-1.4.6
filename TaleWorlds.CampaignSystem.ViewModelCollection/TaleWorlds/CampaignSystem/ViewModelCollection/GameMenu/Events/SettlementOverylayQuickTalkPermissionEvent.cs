using System;
using TaleWorlds.Library.EventSystem;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.Events
{
	// Token: 0x020000C0 RID: 192
	public class SettlementOverylayQuickTalkPermissionEvent : EventBase
	{
		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001321 RID: 4897 RVA: 0x0004DB34 File Offset: 0x0004BD34
		// (set) Token: 0x06001322 RID: 4898 RVA: 0x0004DB3C File Offset: 0x0004BD3C
		public Action<bool, TextObject> IsTalkAvailable { get; private set; }

		// Token: 0x06001323 RID: 4899 RVA: 0x0004DB45 File Offset: 0x0004BD45
		public SettlementOverylayQuickTalkPermissionEvent(Hero heroToTalkTo, Action<bool, TextObject> isTalkAvailable)
		{
			this.HeroToTalkTo = heroToTalkTo;
			this.IsTalkAvailable = isTalkAvailable;
		}

		// Token: 0x040008B6 RID: 2230
		public Hero HeroToTalkTo;
	}
}
