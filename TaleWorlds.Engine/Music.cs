using System;

namespace TaleWorlds.Engine
{
	// Token: 0x0200006E RID: 110
	public class Music
	{
		// Token: 0x06000A3D RID: 2621 RVA: 0x0000A5F1 File Offset: 0x000087F1
		public static int GetFreeMusicChannelIndex()
		{
			return EngineApplicationInterface.IMusic.GetFreeMusicChannelIndex();
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0000A5FD File Offset: 0x000087FD
		public static void LoadClip(int index, string pathToClip)
		{
			EngineApplicationInterface.IMusic.LoadClip(index, pathToClip);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0000A60B File Offset: 0x0000880B
		public static void UnloadClip(int index)
		{
			EngineApplicationInterface.IMusic.UnloadClip(index);
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0000A618 File Offset: 0x00008818
		public static bool IsClipLoaded(int index)
		{
			return EngineApplicationInterface.IMusic.IsClipLoaded(index);
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0000A625 File Offset: 0x00008825
		public static void PlayMusic(int index)
		{
			EngineApplicationInterface.IMusic.PlayMusic(index);
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0000A632 File Offset: 0x00008832
		public static void PlayDelayed(int index, int deltaMilliseconds)
		{
			EngineApplicationInterface.IMusic.PlayDelayed(index, deltaMilliseconds);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0000A640 File Offset: 0x00008840
		public static bool IsMusicPlaying(int index)
		{
			return EngineApplicationInterface.IMusic.IsMusicPlaying(index);
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0000A64D File Offset: 0x0000884D
		public static void PauseMusic(int index)
		{
			EngineApplicationInterface.IMusic.PauseMusic(index);
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0000A65A File Offset: 0x0000885A
		public static void StopMusic(int index)
		{
			EngineApplicationInterface.IMusic.StopMusic(index);
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0000A667 File Offset: 0x00008867
		public static void SetVolume(int index, float volume)
		{
			EngineApplicationInterface.IMusic.SetVolume(index, volume);
		}
	}
}
