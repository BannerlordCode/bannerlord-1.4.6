using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000378 RID: 888
	public class ResetGravityExclusionAndEntityAttachmentOnStopUsageComponent : UsableMissionObjectComponent
	{
		// Token: 0x06003266 RID: 12902 RVA: 0x000CD498 File Offset: 0x000CB698
		public ResetGravityExclusionAndEntityAttachmentOnStopUsageComponent(Action<Agent> onUseAction)
		{
			this.OnUseAction = onUseAction;
		}

		// Token: 0x06003267 RID: 12903 RVA: 0x000CD4A7 File Offset: 0x000CB6A7
		protected internal override void OnUse(Agent userAgent)
		{
			this.OnUseAction(userAgent);
		}

		// Token: 0x06003268 RID: 12904 RVA: 0x000CD4B5 File Offset: 0x000CB6B5
		protected internal override void OnUseStopped(Agent userAgent, bool isSuccessful = true)
		{
			userAgent.SetExcludedFromGravity(false, false);
			userAgent.SetForceAttachedEntity(WeakGameEntity.Invalid);
		}

		// Token: 0x0400155C RID: 5468
		public Action<Agent> OnUseAction;
	}
}
