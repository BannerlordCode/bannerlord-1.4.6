using System;
using TaleWorlds.Library.EventSystem;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.Events
{
	// Token: 0x020000BF RID: 191
	public class SettlementOverlayTalkPermissionEvent : EventBase
	{
		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x0600131E RID: 4894 RVA: 0x0004DB0D File Offset: 0x0004BD0D
		// (set) Token: 0x0600131F RID: 4895 RVA: 0x0004DB15 File Offset: 0x0004BD15
		public Action<bool, TextObject> IsTalkAvailable { get; private set; }

		// Token: 0x06001320 RID: 4896 RVA: 0x0004DB1E File Offset: 0x0004BD1E
		public SettlementOverlayTalkPermissionEvent(Hero heroToTalkTo, Action<bool, TextObject> isTalkAvailable)
		{
			this.HeroToTalkTo = heroToTalkTo;
			this.IsTalkAvailable = isTalkAvailable;
		}

		// Token: 0x040008B4 RID: 2228
		public Hero HeroToTalkTo;
	}
}
