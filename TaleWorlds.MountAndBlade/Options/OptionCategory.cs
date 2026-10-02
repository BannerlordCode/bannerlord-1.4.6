using System;
using System.Collections.Generic;
using TaleWorlds.Engine.Options;

namespace TaleWorlds.MountAndBlade.Options
{
	// Token: 0x02000396 RID: 918
	public class OptionCategory
	{
		// Token: 0x06003494 RID: 13460 RVA: 0x000D8EAB File Offset: 0x000D70AB
		public OptionCategory(IEnumerable<IOptionData> baseOptions, IEnumerable<OptionGroup> groups)
		{
			this.BaseOptions = baseOptions;
			this.Groups = groups;
		}

		// Token: 0x0400165A RID: 5722
		public readonly IEnumerable<IOptionData> BaseOptions;

		// Token: 0x0400165B RID: 5723
		public readonly IEnumerable<OptionGroup> Groups;
	}
}
