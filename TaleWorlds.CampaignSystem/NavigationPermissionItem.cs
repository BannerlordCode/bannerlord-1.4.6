using System;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x02000097 RID: 151
	public struct NavigationPermissionItem
	{
		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x060012B7 RID: 4791 RVA: 0x00054C5C File Offset: 0x00052E5C
		// (set) Token: 0x060012B8 RID: 4792 RVA: 0x00054C64 File Offset: 0x00052E64
		public bool IsAuthorized { get; private set; }

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x060012B9 RID: 4793 RVA: 0x00054C6D File Offset: 0x00052E6D
		// (set) Token: 0x060012BA RID: 4794 RVA: 0x00054C75 File Offset: 0x00052E75
		public TextObject ReasonString { get; private set; }

		// Token: 0x060012BB RID: 4795 RVA: 0x00054C7E File Offset: 0x00052E7E
		public NavigationPermissionItem(bool isAuthorized, TextObject reasonString)
		{
			this.IsAuthorized = isAuthorized;
			this.ReasonString = reasonString;
		}
	}
}
