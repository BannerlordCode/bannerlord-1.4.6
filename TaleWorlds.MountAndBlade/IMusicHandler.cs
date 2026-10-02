using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001D4 RID: 468
	public interface IMusicHandler
	{
		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001BDB RID: 7131
		bool IsPausable { get; }

		// Token: 0x06001BDC RID: 7132
		void OnUpdated(float dt);
	}
}
