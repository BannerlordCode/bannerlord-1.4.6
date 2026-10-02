using System;

namespace TaleWorlds.MountAndBlade.View.MissionViews.Sound
{
	// Token: 0x02000087 RID: 135
	public class MusicSilencedMissionView : MissionView, IMusicHandler
	{
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x000260CC File Offset: 0x000242CC
		bool IMusicHandler.IsPausable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x000260CF File Offset: 0x000242CF
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			MBMusicManager.Current.DeactivateCurrentMode();
			MBMusicManager.Current.OnSilencedMusicHandlerInit(this);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x000260EC File Offset: 0x000242EC
		public override void OnMissionScreenFinalize()
		{
			MBMusicManager.Current.OnSilencedMusicHandlerFinalize();
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x000260F8 File Offset: 0x000242F8
		void IMusicHandler.OnUpdated(float dt)
		{
		}
	}
}
