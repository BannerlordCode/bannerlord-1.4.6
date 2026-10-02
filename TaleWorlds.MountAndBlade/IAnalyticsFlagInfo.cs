using System;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Objects;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000263 RID: 611
	public interface IAnalyticsFlagInfo : IMissionBehavior
	{
		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06002267 RID: 8807
		MBReadOnlyList<FlagCapturePoint> AllCapturePoints { get; }

		// Token: 0x06002268 RID: 8808
		Team GetFlagOwnerTeam(FlagCapturePoint flag);
	}
}
