using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000377 RID: 887
	public class ResetAnimationOnStopUsageComponent : UsableMissionObjectComponent
	{
		// Token: 0x06003263 RID: 12899 RVA: 0x000CD3D4 File Offset: 0x000CB5D4
		public ResetAnimationOnStopUsageComponent(ActionIndexCache successfulResetActionCode, bool alwaysResetWithAction)
		{
			this._successfulResetAction = successfulResetActionCode;
			this._alwaysResetWithAction = alwaysResetWithAction;
		}

		// Token: 0x06003264 RID: 12900 RVA: 0x000CD3EA File Offset: 0x000CB5EA
		public void UpdateSuccessfulResetAction(ActionIndexCache successfulResetActionCode)
		{
			this._successfulResetAction = successfulResetActionCode;
		}

		// Token: 0x06003265 RID: 12901 RVA: 0x000CD3F4 File Offset: 0x000CB5F4
		protected internal override void OnUseStopped(Agent userAgent, bool isSuccessful = true)
		{
			ActionIndexCache actionIndexCache = ((isSuccessful || this._alwaysResetWithAction) ? this._successfulResetAction : ActionIndexCache.act_none);
			float num = ((userAgent.Mission.Mode == MissionMode.Deployment) ? 0f : (-0.2f));
			if (actionIndexCache == ActionIndexCache.act_none)
			{
				userAgent.SetActionChannel(1, in actionIndexCache, false, (AnimFlags)72UL, 0f, 1f, num, 0.4f, 0f, false, -0.2f, 0, true);
			}
			userAgent.SetActionChannel(0, in actionIndexCache, false, (AnimFlags)72UL, 0f, 1f, num, 0.4f, 0f, false, -0.2f, 0, true);
		}

		// Token: 0x0400155A RID: 5466
		private ActionIndexCache _successfulResetAction;

		// Token: 0x0400155B RID: 5467
		private readonly bool _alwaysResetWithAction;
	}
}
