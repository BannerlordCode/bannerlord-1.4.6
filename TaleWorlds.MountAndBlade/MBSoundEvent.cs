using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001DC RID: 476
	public static class MBSoundEvent
	{
		// Token: 0x06001C16 RID: 7190 RVA: 0x00060F17 File Offset: 0x0005F117
		public static bool PlaySound(int soundCodeId, in Vec3 position)
		{
			return MBAPI.IMBSoundEvent.PlaySound(soundCodeId, in position);
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x00060F28 File Offset: 0x0005F128
		public static bool PlaySound(int soundCodeId, Vec3 position)
		{
			Vec3 vec = position;
			return MBAPI.IMBSoundEvent.PlaySound(soundCodeId, in vec);
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x00060F44 File Offset: 0x0005F144
		public static bool PlaySound(int soundCodeId, ref SoundEventParameter parameter, Vec3 position)
		{
			Vec3 vec = position;
			return MBSoundEvent.PlaySound(soundCodeId, ref parameter, in vec);
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x00060F5C File Offset: 0x0005F15C
		public static bool PlaySound(string soundPath, ref SoundEventParameter parameter, Vec3 position)
		{
			int eventIdFromString = SoundEvent.GetEventIdFromString(soundPath);
			Vec3 vec = position;
			return MBSoundEvent.PlaySound(eventIdFromString, ref parameter, in vec);
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x00060F79 File Offset: 0x0005F179
		public static bool PlaySound(int soundCodeId, ref SoundEventParameter parameter, in Vec3 position)
		{
			return MBAPI.IMBSoundEvent.PlaySoundWithParam(soundCodeId, parameter, in position);
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x00060F8D File Offset: 0x0005F18D
		public static void PlayEventFromSoundBuffer(string eventId, byte[] soundData, Scene scene, bool is3d, bool isBlocking)
		{
			MBAPI.IMBSoundEvent.CreateEventFromSoundBuffer(eventId, soundData, (scene != null) ? scene.Pointer : UIntPtr.Zero, is3d, isBlocking);
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x00060FB5 File Offset: 0x0005F1B5
		public static void CreateEventFromExternalFile(string programmerEventName, string soundFilePath, Scene scene, bool is3d, bool isBlocking)
		{
			MBAPI.IMBSoundEvent.CreateEventFromExternalFile(programmerEventName, soundFilePath, (scene != null) ? scene.Pointer : UIntPtr.Zero, is3d, isBlocking);
		}
	}
}
