using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000376 RID: 886
	public class OverrideStrikeAndDeathActionDuringUsageComponent : UsableMissionObjectComponent
	{
		// Token: 0x06003260 RID: 12896 RVA: 0x000CD378 File Offset: 0x000CB578
		public OverrideStrikeAndDeathActionDuringUsageComponent(in ActionIndexCache strikeAction, in ActionIndexCache deathAction)
		{
			this._strikeAction = strikeAction;
			this._deathAction = deathAction;
		}

		// Token: 0x06003261 RID: 12897 RVA: 0x000CD3AE File Offset: 0x000CB5AE
		protected internal override void OnUse(Agent userAgent)
		{
			userAgent.SetOverridenStrikeAndDeathAction(in this._strikeAction, in this._deathAction);
		}

		// Token: 0x06003262 RID: 12898 RVA: 0x000CD3C2 File Offset: 0x000CB5C2
		protected internal override void OnUseStopped(Agent userAgent, bool isSuccessful = true)
		{
			userAgent.SetOverridenStrikeAndDeathAction(in ActionIndexCache.act_none, in ActionIndexCache.act_none);
		}

		// Token: 0x04001558 RID: 5464
		private readonly ActionIndexCache _strikeAction = ActionIndexCache.act_none;

		// Token: 0x04001559 RID: 5465
		private readonly ActionIndexCache _deathAction = ActionIndexCache.act_none;
	}
}
