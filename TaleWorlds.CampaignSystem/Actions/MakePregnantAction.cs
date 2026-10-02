using System;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x020004BE RID: 1214
	public static class MakePregnantAction
	{
		// Token: 0x06004ACA RID: 19146 RVA: 0x0017A878 File Offset: 0x00178A78
		private static void ApplyInternal(Hero mother)
		{
			mother.IsPregnant = true;
			CampaignEventDispatcher.Instance.OnChildConceived(mother);
		}

		// Token: 0x06004ACB RID: 19147 RVA: 0x0017A88C File Offset: 0x00178A8C
		public static void Apply(Hero mother)
		{
			MakePregnantAction.ApplyInternal(mother);
		}
	}
}
