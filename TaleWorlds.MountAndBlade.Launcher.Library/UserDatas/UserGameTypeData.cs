using System;
using System.Collections.Generic;

namespace TaleWorlds.MountAndBlade.Launcher.Library.UserDatas
{
	// Token: 0x0200001A RID: 26
	public class UserGameTypeData
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00005C87 File Offset: 0x00003E87
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00005C8F File Offset: 0x00003E8F
		public List<UserModData> ModDatas { get; set; }

		// Token: 0x0600011B RID: 283 RVA: 0x00005C98 File Offset: 0x00003E98
		public UserGameTypeData()
		{
			this.ModDatas = new List<UserModData>();
		}
	}
}
