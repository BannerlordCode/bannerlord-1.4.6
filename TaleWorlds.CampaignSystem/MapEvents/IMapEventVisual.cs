using System;

namespace TaleWorlds.CampaignSystem.MapEvents
{
	// Token: 0x0200031A RID: 794
	public interface IMapEventVisual
	{
		// Token: 0x06002FB6 RID: 12214
		void Initialize(CampaignVec2 position, bool isVisible);

		// Token: 0x06002FB7 RID: 12215
		void OnMapEventEnd();

		// Token: 0x06002FB8 RID: 12216
		void SetVisibility(bool isVisible);
	}
}
