using System;

namespace TaleWorlds.MountAndBlade.View.MissionViews.Singleplayer
{
	// Token: 0x0200008D RID: 141
	public class DeploymentView : MissionView
	{
		// Token: 0x06000537 RID: 1335 RVA: 0x000265DD File Offset: 0x000247DD
		public override void AfterStart()
		{
			base.AfterStart();
			this._deploymentHandler = base.Mission.GetMissionBehavior<DeploymentHandler>();
			this.CreateWidgets();
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x000265FC File Offset: 0x000247FC
		public override void OnRemoveBehavior()
		{
			this.RemoveWidgets();
			base.OnRemoveBehavior();
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0002660A File Offset: 0x0002480A
		protected virtual void CreateWidgets()
		{
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0002660C File Offset: 0x0002480C
		protected virtual void RemoveWidgets()
		{
		}

		// Token: 0x040002ED RID: 749
		private DeploymentHandler _deploymentHandler;
	}
}
