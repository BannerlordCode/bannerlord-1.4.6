using System;

namespace TaleWorlds.CampaignSystem.BarterSystem
{
	// Token: 0x0200047B RID: 1147
	public class OtherBarterGroup : BarterGroup
	{
		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x060048E0 RID: 18656 RVA: 0x00172A6C File Offset: 0x00170C6C
		public override float AIDecisionWeight
		{
			get
			{
				return 0.25f;
			}
		}
	}
}
