using System;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.Core
{
	// Token: 0x020000DA RID: 218
	public class TutorialContextChangedEvent : EventBase
	{
		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x00024CEE File Offset: 0x00022EEE
		// (set) Token: 0x06000B4A RID: 2890 RVA: 0x00024CF6 File Offset: 0x00022EF6
		public TutorialContexts NewContext { get; private set; }

		// Token: 0x06000B4B RID: 2891 RVA: 0x00024CFF File Offset: 0x00022EFF
		public TutorialContextChangedEvent(TutorialContexts newContext)
		{
			this.NewContext = newContext;
		}
	}
}
