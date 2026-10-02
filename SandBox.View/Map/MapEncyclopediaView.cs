using System;

namespace SandBox.View.Map
{
	// Token: 0x0200004C RID: 76
	public class MapEncyclopediaView : MapView
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000288 RID: 648 RVA: 0x00017A5D File Offset: 0x00015C5D
		// (set) Token: 0x06000289 RID: 649 RVA: 0x00017A65 File Offset: 0x00015C65
		public bool IsEncyclopediaOpen { get; protected set; }

		// Token: 0x0600028A RID: 650 RVA: 0x00017A6E File Offset: 0x00015C6E
		public virtual void CloseEncyclopedia()
		{
		}
	}
}
