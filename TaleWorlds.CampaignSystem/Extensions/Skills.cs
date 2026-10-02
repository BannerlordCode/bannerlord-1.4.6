using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.Extensions
{
	// Token: 0x0200016B RID: 363
	public static class Skills
	{
		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06001B36 RID: 6966 RVA: 0x0008D485 File Offset: 0x0008B685
		public static MBReadOnlyList<SkillObject> All
		{
			get
			{
				return Campaign.Current.AllSkills;
			}
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x0008D491 File Offset: 0x0008B691
		public static SkillObject GetSkill(int i)
		{
			return Skills.All[i];
		}
	}
}
