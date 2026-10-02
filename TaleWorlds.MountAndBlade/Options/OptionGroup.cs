using System;
using System.Collections.Generic;
using TaleWorlds.Engine.Options;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.Options
{
	// Token: 0x02000397 RID: 919
	public class OptionGroup
	{
		// Token: 0x06003495 RID: 13461 RVA: 0x000D8EC1 File Offset: 0x000D70C1
		public OptionGroup(TextObject groupName, IEnumerable<IOptionData> options)
		{
			this.GroupName = groupName;
			this.Options = options;
		}

		// Token: 0x0400165C RID: 5724
		public readonly TextObject GroupName;

		// Token: 0x0400165D RID: 5725
		public readonly IEnumerable<IOptionData> Options;
	}
}
