using System;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.CampaignSystem.LogEntries
{
	// Token: 0x02000351 RID: 849
	public interface IEncyclopediaLog
	{
		// Token: 0x06003236 RID: 12854
		bool IsVisibleInEncyclopediaPageOf<T>(T obj) where T : MBObjectBase;

		// Token: 0x06003237 RID: 12855
		TextObject GetEncyclopediaText();

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06003238 RID: 12856
		CampaignTime GameTime { get; }
	}
}
