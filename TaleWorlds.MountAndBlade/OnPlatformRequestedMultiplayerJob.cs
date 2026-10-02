using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200024D RID: 589
	internal class OnPlatformRequestedMultiplayerJob : Job
	{
		// Token: 0x0600219E RID: 8606 RVA: 0x000759D0 File Offset: 0x00073BD0
		public override void DoJob(float dt)
		{
			base.DoJob(dt);
			if (MBGameManager.Current != null)
			{
				MBGameManager.Current.OnPlatformRequestedMultiplayer();
			}
			else if (GameStateManager.Current != null && GameStateManager.Current.ActiveState != null)
			{
				GameStateManager.Current.CleanStates(0);
			}
			base.Finished = true;
		}
	}
}
