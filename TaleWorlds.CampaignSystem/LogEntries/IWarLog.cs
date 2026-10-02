using System;

namespace TaleWorlds.CampaignSystem.LogEntries
{
	// Token: 0x02000352 RID: 850
	public interface IWarLog
	{
		// Token: 0x06003239 RID: 12857
		bool IsRelatedToWar(StanceLink stance, out IFaction effector, out IFaction effected);
	}
}
