using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000364 RID: 868
	public interface ISynchedMissionObjectReadableRecord
	{
		// Token: 0x060031D9 RID: 12761
		bool ReadFromNetwork(ref bool bufferReadValid);
	}
}
