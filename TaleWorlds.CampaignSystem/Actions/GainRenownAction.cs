using System;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x020004B3 RID: 1203
	public static class GainRenownAction
	{
		// Token: 0x06004A9F RID: 19103 RVA: 0x001795B5 File Offset: 0x001777B5
		private static void ApplyInternal(Hero hero, float gainedRenown, bool doNotNotify)
		{
			if (gainedRenown > 0f)
			{
				hero.Clan.AddRenown(gainedRenown, true);
				CampaignEventDispatcher.Instance.OnRenownGained(hero, (int)gainedRenown, doNotNotify);
			}
		}

		// Token: 0x06004AA0 RID: 19104 RVA: 0x001795DA File Offset: 0x001777DA
		public static void Apply(Hero hero, float renownValue, bool doNotNotify = false)
		{
			GainRenownAction.ApplyInternal(hero, renownValue, doNotNotify);
		}
	}
}
