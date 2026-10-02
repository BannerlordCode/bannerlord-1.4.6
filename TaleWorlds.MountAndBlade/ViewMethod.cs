using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002D5 RID: 725
	public class ViewMethod : Attribute
	{
		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x060029C9 RID: 10697 RVA: 0x0009F10B File Offset: 0x0009D30B
		// (set) Token: 0x060029CA RID: 10698 RVA: 0x0009F113 File Offset: 0x0009D313
		public string Name { get; private set; }

		// Token: 0x060029CB RID: 10699 RVA: 0x0009F11C File Offset: 0x0009D31C
		public ViewMethod(string name)
		{
			this.Name = name;
		}
	}
}
