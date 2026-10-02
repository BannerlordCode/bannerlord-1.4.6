using System;

namespace TaleWorlds.Engine
{
	// Token: 0x02000087 RID: 135
	public class EditorVisibleScriptComponentVariable : Attribute
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x0000D513 File Offset: 0x0000B713
		// (set) Token: 0x06000C21 RID: 3105 RVA: 0x0000D51B File Offset: 0x0000B71B
		public bool Visible { get; set; }

		// Token: 0x06000C22 RID: 3106 RVA: 0x0000D524 File Offset: 0x0000B724
		public EditorVisibleScriptComponentVariable(bool visible)
		{
			this.Visible = visible;
		}
	}
}
