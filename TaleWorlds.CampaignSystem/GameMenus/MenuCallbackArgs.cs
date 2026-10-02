using System;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.GameMenus
{
	// Token: 0x020000EA RID: 234
	public class MenuCallbackArgs
	{
		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x060015C8 RID: 5576 RVA: 0x00062929 File Offset: 0x00060B29
		// (set) Token: 0x060015C9 RID: 5577 RVA: 0x00062931 File Offset: 0x00060B31
		public MenuContext MenuContext { get; private set; }

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x060015CA RID: 5578 RVA: 0x0006293A File Offset: 0x00060B3A
		// (set) Token: 0x060015CB RID: 5579 RVA: 0x00062942 File Offset: 0x00060B42
		public MapState MapState { get; private set; }

		// Token: 0x060015CC RID: 5580 RVA: 0x0006294B File Offset: 0x00060B4B
		public MenuCallbackArgs(MenuContext menuContext, TextObject text)
		{
			this.MenuContext = menuContext;
			this.Text = text;
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x00062968 File Offset: 0x00060B68
		public MenuCallbackArgs(MapState mapState, TextObject text)
		{
			this.MapState = mapState;
			this.Text = text;
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x00062985 File Offset: 0x00060B85
		public MenuCallbackArgs(MapState mapState, TextObject text, float dt)
		{
			this.MapState = mapState;
			this.Text = text;
			this.DeltaTime = dt;
		}

		// Token: 0x04000730 RID: 1840
		public float DeltaTime;

		// Token: 0x04000731 RID: 1841
		public bool IsEnabled = true;

		// Token: 0x04000732 RID: 1842
		public TextObject Text;

		// Token: 0x04000733 RID: 1843
		public TextObject Tooltip;

		// Token: 0x04000734 RID: 1844
		public GameMenuOption.IssueQuestFlags OptionQuestData;

		// Token: 0x04000735 RID: 1845
		public GameMenuOption.LeaveType optionLeaveType;

		// Token: 0x04000736 RID: 1846
		public TextObject MenuTitle;
	}
}
