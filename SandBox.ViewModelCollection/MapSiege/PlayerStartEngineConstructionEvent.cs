using System;
using TaleWorlds.Core;
using TaleWorlds.Library.EventSystem;

namespace SandBox.ViewModelCollection.MapSiege
{
	// Token: 0x02000055 RID: 85
	public class PlayerStartEngineConstructionEvent : EventBase
	{
		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x00014285 File Offset: 0x00012485
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x0001428D File Offset: 0x0001248D
		public SiegeEngineType Engine { get; private set; }

		// Token: 0x06000559 RID: 1369 RVA: 0x00014296 File Offset: 0x00012496
		public PlayerStartEngineConstructionEvent(SiegeEngineType engine)
		{
			this.Engine = engine;
		}
	}
}
