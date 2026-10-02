using System;
using TaleWorlds.CampaignSystem.ComponentInterfaces;

namespace TaleWorlds.CampaignSystem.GameComponents
{
	// Token: 0x0200011F RID: 287
	public class DefaultIncidentModel : IncidentModel
	{
		// Token: 0x0600183F RID: 6207 RVA: 0x00075567 File Offset: 0x00073767
		public override CampaignTime GetMinGlobalCooldownTime()
		{
			return CampaignTime.Days(8f);
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x00075573 File Offset: 0x00073773
		public override CampaignTime GetMaxGlobalCooldownTime()
		{
			return CampaignTime.Days(15f);
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x0007557F File Offset: 0x0007377F
		public override float GetIncidentTriggerGlobalProbability()
		{
			return 0.5f;
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x00075586 File Offset: 0x00073786
		public override float GetIncidentTriggerProbabilityDuringSiege()
		{
			return 0.143f;
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x0007558D File Offset: 0x0007378D
		public override float GetIncidentTriggerProbabilityDuringWait()
		{
			return 0.143f;
		}
	}
}
