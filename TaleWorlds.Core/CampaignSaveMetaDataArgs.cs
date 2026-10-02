using System;
using System.Collections.Generic;

namespace TaleWorlds.Core
{
	// Token: 0x020000B5 RID: 181
	public readonly struct CampaignSaveMetaDataArgs
	{
		// Token: 0x0600096C RID: 2412 RVA: 0x0001EC4E File Offset: 0x0001CE4E
		public CampaignSaveMetaDataArgs(string[] moduleName, params KeyValuePair<string, string>[] otherArgs)
		{
			this.ModuleNames = moduleName;
			this.OtherData = otherArgs;
		}

		// Token: 0x0400052D RID: 1325
		public readonly string[] ModuleNames;

		// Token: 0x0400052E RID: 1326
		public readonly KeyValuePair<string, string>[] OtherData;
	}
}
