using System;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003FF RID: 1023
	public interface IFacegenCampaignBehavior : ICampaignBehavior
	{
		// Token: 0x06004061 RID: 16481
		IFaceGeneratorCustomFilter GetFaceGenFilter();
	}
}
