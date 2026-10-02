using System;
using System.Reflection;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001C8 RID: 456
	public class MissionInfo
	{
		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001B64 RID: 7012 RVA: 0x0005F915 File Offset: 0x0005DB15
		// (set) Token: 0x06001B65 RID: 7013 RVA: 0x0005F91D File Offset: 0x0005DB1D
		public string Name { get; set; }

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001B66 RID: 7014 RVA: 0x0005F926 File Offset: 0x0005DB26
		// (set) Token: 0x06001B67 RID: 7015 RVA: 0x0005F92E File Offset: 0x0005DB2E
		public MethodInfo Creator { get; set; }

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001B68 RID: 7016 RVA: 0x0005F937 File Offset: 0x0005DB37
		// (set) Token: 0x06001B69 RID: 7017 RVA: 0x0005F93F File Offset: 0x0005DB3F
		public Type Manager { get; set; }

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001B6A RID: 7018 RVA: 0x0005F948 File Offset: 0x0005DB48
		// (set) Token: 0x06001B6B RID: 7019 RVA: 0x0005F950 File Offset: 0x0005DB50
		public bool UsableByEditor { get; set; }
	}
}
