using System;
using System.Collections.Generic;
using System.Linq;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000137 RID: 311
	public static class ModuleInfoModelExtensions
	{
		// Token: 0x0600086F RID: 2159 RVA: 0x0000C404 File Offset: 0x0000A604
		public static bool IsCompatibleWith(this IEnumerable<ModuleInfoModel> a, IEnumerable<ModuleInfoModel> b, bool allowOptionalModules)
		{
			bool flag = (from m in a
				where !m.IsOptional
				orderby m.Id
				select m).SequenceEqual<ModuleInfoModel>(from m in b
				where !m.IsOptional
				orderby m.Id
				select m);
			bool flag2;
			if (!a.Any<ModuleInfoModel>((ModuleInfoModel m) => m.IsOptional))
			{
				flag2 = b.Any<ModuleInfoModel>((ModuleInfoModel m) => m.IsOptional);
			}
			else
			{
				flag2 = true;
			}
			bool flag3 = flag2;
			return flag && (allowOptionalModules || !flag3);
		}
	}
}
