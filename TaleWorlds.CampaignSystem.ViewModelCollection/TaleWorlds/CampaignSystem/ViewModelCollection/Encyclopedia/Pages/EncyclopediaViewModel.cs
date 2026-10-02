using System;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages
{
	// Token: 0x020000D7 RID: 215
	public class EncyclopediaViewModel : Attribute
	{
		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001496 RID: 5270 RVA: 0x00052012 File Offset: 0x00050212
		// (set) Token: 0x06001497 RID: 5271 RVA: 0x0005201A File Offset: 0x0005021A
		public Type PageTargetType { get; private set; }

		// Token: 0x06001498 RID: 5272 RVA: 0x00052023 File Offset: 0x00050223
		public EncyclopediaViewModel(Type pageTargetType)
		{
			this.PageTargetType = pageTargetType;
		}
	}
}
