using System;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.Encyclopedia
{
	// Token: 0x0200017A RID: 378
	public class EncyclopediaSortController
	{
		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06001B85 RID: 7045 RVA: 0x0008E0F7 File Offset: 0x0008C2F7
		public TextObject Name { get; }

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001B86 RID: 7046 RVA: 0x0008E0FF File Offset: 0x0008C2FF
		public EncyclopediaListItemComparerBase Comparer { get; }

		// Token: 0x06001B87 RID: 7047 RVA: 0x0008E107 File Offset: 0x0008C307
		public EncyclopediaSortController(TextObject name, EncyclopediaListItemComparerBase comparer)
		{
			this.Name = name;
			this.Comparer = comparer;
		}
	}
}
