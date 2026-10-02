using System;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x02000497 RID: 1175
	public static class ChangeClanInfluenceAction
	{
		// Token: 0x06004A19 RID: 18969 RVA: 0x00176FD3 File Offset: 0x001751D3
		private static void ApplyInternal(Clan clan, float amount)
		{
			clan.Influence += amount;
			CampaignEventDispatcher.Instance.OnClanInfluenceChanged(clan, amount);
		}

		// Token: 0x06004A1A RID: 18970 RVA: 0x00176FEF File Offset: 0x001751EF
		public static void Apply(Clan clan, float amount)
		{
			ChangeClanInfluenceAction.ApplyInternal(clan, amount);
		}
	}
}
