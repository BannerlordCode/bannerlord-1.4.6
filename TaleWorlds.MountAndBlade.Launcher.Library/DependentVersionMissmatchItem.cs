using System;
using System.Collections.Generic;
using TaleWorlds.Library;
using TaleWorlds.ModuleManager;

namespace TaleWorlds.MountAndBlade.Launcher.Library
{
	// Token: 0x0200000C RID: 12
	public struct DependentVersionMissmatchItem
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003549 File Offset: 0x00001749
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00003551 File Offset: 0x00001751
		public string MissmatchedModuleId { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000068 RID: 104 RVA: 0x0000355A File Offset: 0x0000175A
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00003562 File Offset: 0x00001762
		public List<Tuple<DependedModule, ApplicationVersion>> MissmatchedDependencies { get; private set; }

		// Token: 0x0600006A RID: 106 RVA: 0x0000356B File Offset: 0x0000176B
		public DependentVersionMissmatchItem(string missmatchedModuleId, List<Tuple<DependedModule, ApplicationVersion>> missmatchedDependencies)
		{
			this.MissmatchedModuleId = missmatchedModuleId;
			this.MissmatchedDependencies = missmatchedDependencies;
		}
	}
}
