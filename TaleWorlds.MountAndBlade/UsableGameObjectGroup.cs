using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200037B RID: 891
	public class UsableGameObjectGroup : ScriptComponentBehavior, IVisible
	{
		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x060032B7 RID: 12983 RVA: 0x000D0A5C File Offset: 0x000CEC5C
		// (set) Token: 0x060032B8 RID: 12984 RVA: 0x000D0A78 File Offset: 0x000CEC78
		public bool IsVisible
		{
			get
			{
				return base.GameEntity.IsVisibleIncludeParents();
			}
			set
			{
				base.GameEntity.SetVisibilityExcludeParents(value);
			}
		}
	}
}
