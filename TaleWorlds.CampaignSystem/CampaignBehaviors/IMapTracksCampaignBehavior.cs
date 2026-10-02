using System;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x02000402 RID: 1026
	public interface IMapTracksCampaignBehavior : ICampaignBehavior
	{
		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x06004064 RID: 16484
		MBReadOnlyList<Track> DetectedTracks { get; }

		// Token: 0x06004065 RID: 16485
		void AddTrack(MobileParty target, CampaignVec2 trackPosition, Vec2 trackDirection);

		// Token: 0x06004066 RID: 16486
		void AddMapArrow(TextObject pointerName, CampaignVec2 trackPosition, Vec2 trackDirection, float life);
	}
}
