using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.Engine
{
	// Token: 0x0200009A RID: 154
	[EngineClass("rglVideo_player_view")]
	public sealed class VideoPlayerView : View
	{
		// Token: 0x06000DC7 RID: 3527 RVA: 0x0000F70A File Offset: 0x0000D90A
		internal VideoPlayerView(UIntPtr meshPointer)
			: base(meshPointer)
		{
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x0000F713 File Offset: 0x0000D913
		public static VideoPlayerView CreateVideoPlayerView()
		{
			return EngineApplicationInterface.IVideoPlayerView.CreateVideoPlayerView();
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x0000F71F File Offset: 0x0000D91F
		public void PlayVideo(string videoFileName, string soundFileName, float framerate, bool looping)
		{
			EngineApplicationInterface.IVideoPlayerView.PlayVideo(base.Pointer, videoFileName, soundFileName, framerate, looping);
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x0000F736 File Offset: 0x0000D936
		public void StopVideo()
		{
			EngineApplicationInterface.IVideoPlayerView.StopVideo(base.Pointer);
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x0000F748 File Offset: 0x0000D948
		public bool IsVideoFinished()
		{
			return EngineApplicationInterface.IVideoPlayerView.IsVideoFinished(base.Pointer);
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x0000F75A File Offset: 0x0000D95A
		public void FinalizePlayer()
		{
			EngineApplicationInterface.IVideoPlayerView.Finalize(base.Pointer);
		}
	}
}
