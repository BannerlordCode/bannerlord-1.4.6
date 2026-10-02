using System;

namespace SandBox.View.Map.Visuals
{
	// Token: 0x02000061 RID: 97
	public abstract class MapEntityVisual<T> : MapEntityVisual
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0001DFA5 File Offset: 0x0001C1A5
		// (set) Token: 0x060003D9 RID: 985 RVA: 0x0001DFAD File Offset: 0x0001C1AD
		public T MapEntity { get; private set; }

		// Token: 0x060003DA RID: 986 RVA: 0x0001DFB6 File Offset: 0x0001C1B6
		public MapEntityVisual(T entity)
		{
			this.MapEntity = entity;
		}
	}
}
