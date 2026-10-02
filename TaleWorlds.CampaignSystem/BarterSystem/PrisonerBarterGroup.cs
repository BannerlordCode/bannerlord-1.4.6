using System;

namespace TaleWorlds.CampaignSystem.BarterSystem
{
	// Token: 0x0200047A RID: 1146
	public class PrisonerBarterGroup : BarterGroup
	{
		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x060048DE RID: 18654 RVA: 0x00172A5D File Offset: 0x00170C5D
		public override float AIDecisionWeight
		{
			get
			{
				return 0.7f;
			}
		}
	}
}
