using System;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia
{
	// Token: 0x020000C4 RID: 196
	public class EncyclopediaPageChangedEvent : EventBase
	{
		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001328 RID: 4904 RVA: 0x0004DB83 File Offset: 0x0004BD83
		// (set) Token: 0x06001329 RID: 4905 RVA: 0x0004DB8B File Offset: 0x0004BD8B
		public EncyclopediaPages NewPage { get; private set; }

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x0600132A RID: 4906 RVA: 0x0004DB94 File Offset: 0x0004BD94
		// (set) Token: 0x0600132B RID: 4907 RVA: 0x0004DB9C File Offset: 0x0004BD9C
		public bool NewPageHasHiddenInformation { get; private set; }

		// Token: 0x0600132C RID: 4908 RVA: 0x0004DBA5 File Offset: 0x0004BDA5
		public EncyclopediaPageChangedEvent(EncyclopediaPages newPage, bool hasHiddenInformation = false)
		{
			this.NewPage = newPage;
			this.NewPageHasHiddenInformation = hasHiddenInformation;
		}
	}
}
