using System;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x02000490 RID: 1168
	public static class AddCompanionAction
	{
		// Token: 0x06004A04 RID: 18948 RVA: 0x0017630D File Offset: 0x0017450D
		private static void ApplyInternal(Clan clan, Hero companion)
		{
			if (companion.CompanionOf != null)
			{
				RemoveCompanionAction.ApplyByFire(companion.CompanionOf, companion);
			}
			companion.CompanionOf = clan;
			CampaignEventDispatcher.Instance.OnNewCompanionAdded(companion);
		}

		// Token: 0x06004A05 RID: 18949 RVA: 0x00176335 File Offset: 0x00174535
		public static void Apply(Clan clan, Hero companion)
		{
			AddCompanionAction.ApplyInternal(clan, companion);
		}
	}
}
