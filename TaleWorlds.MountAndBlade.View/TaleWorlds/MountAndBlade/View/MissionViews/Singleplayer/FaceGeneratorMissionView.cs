using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade.View.MissionViews.Singleplayer
{
	// Token: 0x0200008E RID: 142
	public class FaceGeneratorMissionView : MissionView
	{
		// Token: 0x0600053C RID: 1340 RVA: 0x00026616 File Offset: 0x00024816
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			if (base.Input.IsGameKeyPressed(37) && !GameNetwork.IsSessionActive)
			{
				LoadingWindow.EnableGlobalLoadingWindow();
				ScreenManager.PushScreen(ViewCreator.CreateMBFaceGeneratorScreen(Game.Current.PlayerTroop, false, null));
			}
		}
	}
}
