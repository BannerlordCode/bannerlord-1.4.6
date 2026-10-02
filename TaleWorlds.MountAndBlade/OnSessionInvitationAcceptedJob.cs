using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.PlatformService;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200024E RID: 590
	internal class OnSessionInvitationAcceptedJob : Job
	{
		// Token: 0x060021A0 RID: 8608 RVA: 0x00075A24 File Offset: 0x00073C24
		public OnSessionInvitationAcceptedJob(SessionInvitationType sessionInvitationType)
		{
			this._sessionInvitationType = sessionInvitationType;
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x00075A34 File Offset: 0x00073C34
		public override void DoJob(float dt)
		{
			base.DoJob(dt);
			if (MBGameManager.Current != null)
			{
				MBGameManager.Current.OnSessionInvitationAccepted(this._sessionInvitationType);
			}
			else if (GameStateManager.Current != null && GameStateManager.Current.ActiveState != null)
			{
				GameStateManager.Current.CleanStates(0);
			}
			base.Finished = true;
		}

		// Token: 0x04000CE8 RID: 3304
		private readonly SessionInvitationType _sessionInvitationType;
	}
}
