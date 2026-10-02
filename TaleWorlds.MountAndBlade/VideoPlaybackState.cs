using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000246 RID: 582
	public class VideoPlaybackState : GameState
	{
		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06002175 RID: 8565 RVA: 0x00075627 File Offset: 0x00073827
		// (set) Token: 0x06002176 RID: 8566 RVA: 0x0007562F File Offset: 0x0007382F
		public string VideoPath { get; private set; }

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06002177 RID: 8567 RVA: 0x00075638 File Offset: 0x00073838
		// (set) Token: 0x06002178 RID: 8568 RVA: 0x00075640 File Offset: 0x00073840
		public string AudioPath { get; private set; }

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06002179 RID: 8569 RVA: 0x00075649 File Offset: 0x00073849
		// (set) Token: 0x0600217A RID: 8570 RVA: 0x00075651 File Offset: 0x00073851
		public float FrameRate { get; private set; }

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x0600217B RID: 8571 RVA: 0x0007565A File Offset: 0x0007385A
		// (set) Token: 0x0600217C RID: 8572 RVA: 0x00075662 File Offset: 0x00073862
		public string SubtitleFileBasePath { get; private set; }

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x0600217D RID: 8573 RVA: 0x0007566B File Offset: 0x0007386B
		// (set) Token: 0x0600217E RID: 8574 RVA: 0x00075673 File Offset: 0x00073873
		public bool CanUserSkip { get; private set; }

		// Token: 0x0600217F RID: 8575 RVA: 0x0007567C File Offset: 0x0007387C
		public void SetStartingParameters(string videoPath, string audioPath, string subtitleFileBasePath, float frameRate = 30f, bool canUserSkip = true)
		{
			this.VideoPath = videoPath;
			this.AudioPath = audioPath;
			this.FrameRate = frameRate;
			this.SubtitleFileBasePath = subtitleFileBasePath;
			this.CanUserSkip = canUserSkip;
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x000756A3 File Offset: 0x000738A3
		public void SetOnVideoFinisedDelegate(Action onVideoFinised)
		{
			this._onVideoFinised = onVideoFinised;
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x000756AC File Offset: 0x000738AC
		public void OnVideoStarted()
		{
			MBMusicManager.Current.DeactivateCurrentMode();
			MBMusicManager.Current.PauseMusicManagerSystem();
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x000756C2 File Offset: 0x000738C2
		public void OnVideoFinished()
		{
			MBMusicManager.Current.UnpauseMusicManagerSystem();
			Action onVideoFinised = this._onVideoFinised;
			if (onVideoFinised == null)
			{
				return;
			}
			onVideoFinised();
		}

		// Token: 0x04000CD6 RID: 3286
		private Action _onVideoFinised;
	}
}
