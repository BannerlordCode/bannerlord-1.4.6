using System;
using System.Collections.Generic;

namespace TaleWorlds.MountAndBlade.Launcher.Library.UserDatas
{
	// Token: 0x0200001C RID: 28
	public class DLLCheckDataCollection
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00005D3A File Offset: 0x00003F3A
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00005D42 File Offset: 0x00003F42
		public List<DLLCheckData> DLLData { get; set; }

		// Token: 0x06000127 RID: 295 RVA: 0x00005D4B File Offset: 0x00003F4B
		public DLLCheckDataCollection()
		{
			this.DLLData = new List<DLLCheckData>();
		}
	}
}
