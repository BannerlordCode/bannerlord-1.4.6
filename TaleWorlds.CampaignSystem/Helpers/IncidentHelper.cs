using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Library;

namespace Helpers
{
	// Token: 0x02000015 RID: 21
	public static class IncidentHelper
	{
		// Token: 0x060000BA RID: 186 RVA: 0x0000A5E0 File Offset: 0x000087E0
		public static T GetSeededRandomElement<T>(List<T> list, long seed)
		{
			if (list == null || list.Count == 0)
			{
				return default(T);
			}
			return list[MobileParty.MainParty.RandomIntWithSeed((uint)seed, list.Count)];
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000A61C File Offset: 0x0000881C
		public static T GetSeededRandomElement<T>(MBList<T> list, long seed)
		{
			if (list == null || list.Count == 0)
			{
				return default(T);
			}
			return list[MobileParty.MainParty.RandomIntWithSeed((uint)seed, list.Count)];
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000A658 File Offset: 0x00008858
		public static T GetSeededRandomElement<T>(MBReadOnlyList<T> list, long seed)
		{
			if (list == null || list.Count == 0)
			{
				return default(T);
			}
			return list[MobileParty.MainParty.RandomIntWithSeed((uint)seed, list.Count)];
		}
	}
}
