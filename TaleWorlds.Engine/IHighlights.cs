using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000038 RID: 56
	[ApplicationInterfaceBase]
	internal interface IHighlights
	{
		// Token: 0x0600057C RID: 1404
		[EngineMethod("initialize", false, null, false)]
		void Initialize();

		// Token: 0x0600057D RID: 1405
		[EngineMethod("open_group", false, null, false)]
		void OpenGroup(string id);

		// Token: 0x0600057E RID: 1406
		[EngineMethod("close_group", false, null, false)]
		void CloseGroup(string id, bool destroy = false);

		// Token: 0x0600057F RID: 1407
		[EngineMethod("save_screenshot", false, null, false)]
		void SaveScreenshot(string highlightId, string groupId);

		// Token: 0x06000580 RID: 1408
		[EngineMethod("save_video", false, null, false)]
		void SaveVideo(string highlightId, string groupId, int startDelta, int endDelta);

		// Token: 0x06000581 RID: 1409
		[EngineMethod("open_summary", false, null, false)]
		void OpenSummary(string groups);

		// Token: 0x06000582 RID: 1410
		[EngineMethod("add_highlight", false, null, false)]
		void AddHighlight(string id, string name);

		// Token: 0x06000583 RID: 1411
		[EngineMethod("remove_highlight", false, null, false)]
		void RemoveHighlight(string id);
	}
}
