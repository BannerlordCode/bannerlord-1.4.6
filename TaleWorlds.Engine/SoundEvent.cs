using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200008D RID: 141
	public class SoundEvent
	{
		// Token: 0x06000C9B RID: 3227 RVA: 0x0000DFC6 File Offset: 0x0000C1C6
		public int GetSoundId()
		{
			return this._soundId;
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x0000DFCE File Offset: 0x0000C1CE
		private SoundEvent(int soundId)
		{
			this._soundId = soundId;
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x0000DFE0 File Offset: 0x0000C1E0
		public static SoundEvent CreateEventFromString(string eventId, Scene scene)
		{
			UIntPtr uintPtr = ((scene == null) ? UIntPtr.Zero : scene.Pointer);
			return new SoundEvent(EngineApplicationInterface.ISoundEvent.CreateEventFromString(eventId, uintPtr));
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x0000E015 File Offset: 0x0000C215
		public void SetEventMinMaxDistance(Vec3 newRadius)
		{
			EngineApplicationInterface.ISoundEvent.SetEventMinMaxDistance(this._soundId, newRadius);
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x0000E028 File Offset: 0x0000C228
		public static int GetEventIdFromString(string name)
		{
			return EngineApplicationInterface.ISoundEvent.GetEventIdFromString(name);
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x0000E035 File Offset: 0x0000C235
		public static bool PlaySound2D(int soundCodeId)
		{
			return EngineApplicationInterface.ISoundEvent.PlaySound2D(soundCodeId);
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x0000E042 File Offset: 0x0000C242
		public static bool PlaySound2D(string soundName)
		{
			return SoundEvent.PlaySound2D(SoundEvent.GetEventIdFromString(soundName));
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x0000E04F File Offset: 0x0000C24F
		public static int GetTotalEventCount()
		{
			return EngineApplicationInterface.ISoundEvent.GetTotalEventCount();
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x0000E05B File Offset: 0x0000C25B
		public static SoundEvent CreateEvent(int soundCodeId, Scene scene)
		{
			return new SoundEvent(EngineApplicationInterface.ISoundEvent.CreateEvent(soundCodeId, scene.Pointer));
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x0000E073 File Offset: 0x0000C273
		public bool IsNullSoundEvent()
		{
			return this == SoundEvent.NullSoundEvent;
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x0000E07D File Offset: 0x0000C27D
		public bool IsValid
		{
			get
			{
				return this._soundId != -1 && EngineApplicationInterface.ISoundEvent.IsValid(this._soundId);
			}
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x0000E09A File Offset: 0x0000C29A
		public bool Play()
		{
			return EngineApplicationInterface.ISoundEvent.StartEvent(this._soundId);
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x0000E0AC File Offset: 0x0000C2AC
		public void Pause()
		{
			EngineApplicationInterface.ISoundEvent.PauseEvent(this._soundId);
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x0000E0BE File Offset: 0x0000C2BE
		public void Resume()
		{
			EngineApplicationInterface.ISoundEvent.ResumeEvent(this._soundId);
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x0000E0D0 File Offset: 0x0000C2D0
		public void PlayExtraEvent(string eventName)
		{
			EngineApplicationInterface.ISoundEvent.PlayExtraEvent(this._soundId, eventName);
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x0000E0E3 File Offset: 0x0000C2E3
		public void SetSwitch(string switchGroupName, string newSwitchStateName)
		{
			EngineApplicationInterface.ISoundEvent.SetSwitch(this._soundId, switchGroupName, newSwitchStateName);
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0000E0F7 File Offset: 0x0000C2F7
		public void TriggerCue()
		{
			EngineApplicationInterface.ISoundEvent.TriggerCue(this._soundId);
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0000E109 File Offset: 0x0000C309
		public bool PlayInPosition(Vec3 position)
		{
			return EngineApplicationInterface.ISoundEvent.StartEventInPosition(this._soundId, ref position);
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0000E11D File Offset: 0x0000C31D
		public void Stop()
		{
			if (!this.IsValid)
			{
				return;
			}
			EngineApplicationInterface.ISoundEvent.StopEvent(this._soundId);
			this._soundId = -1;
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0000E13F File Offset: 0x0000C33F
		public void SetParameter(string parameterName, float value)
		{
			EngineApplicationInterface.ISoundEvent.SetEventParameterFromString(this._soundId, parameterName, value);
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0000E153 File Offset: 0x0000C353
		public void SetParameter(int parameterIndex, float value)
		{
			EngineApplicationInterface.ISoundEvent.SetEventParameterAtIndex(this._soundId, parameterIndex, value);
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0000E167 File Offset: 0x0000C367
		public Vec3 GetEventMinMaxDistance()
		{
			return EngineApplicationInterface.ISoundEvent.GetEventMinMaxDistance(this._soundId);
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x0000E179 File Offset: 0x0000C379
		public void SetPosition(Vec3 vec)
		{
			if (!this.IsValid)
			{
				return;
			}
			EngineApplicationInterface.ISoundEvent.SetEventPosition(this._soundId, ref vec);
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0000E196 File Offset: 0x0000C396
		public void SetVelocity(Vec3 vec)
		{
			if (!this.IsValid)
			{
				return;
			}
			EngineApplicationInterface.ISoundEvent.SetEventVelocity(this._soundId, ref vec);
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0000E1B4 File Offset: 0x0000C3B4
		public void Release()
		{
			MBDebug.Print("Release Sound Event " + this._soundId, 0, Debug.DebugColor.Red, 17592186044416UL);
			if (this.IsValid)
			{
				if (this.IsPlaying())
				{
					this.Stop();
				}
				EngineApplicationInterface.ISoundEvent.ReleaseEvent(this._soundId);
			}
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x0000E20C File Offset: 0x0000C40C
		public bool IsPlaying()
		{
			return EngineApplicationInterface.ISoundEvent.IsPlaying(this._soundId);
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x0000E21E File Offset: 0x0000C41E
		public bool IsPaused()
		{
			return EngineApplicationInterface.ISoundEvent.IsPaused(this._soundId);
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x0000E230 File Offset: 0x0000C430
		public bool IsStopped()
		{
			return EngineApplicationInterface.ISoundEvent.IsStopped(this._soundId);
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x0000E242 File Offset: 0x0000C442
		public static SoundEvent CreateEventFromSoundBuffer(string eventId, byte[] soundData, Scene scene, bool is3d, bool isBlocking)
		{
			return new SoundEvent(EngineApplicationInterface.ISoundEvent.CreateEventFromSoundBuffer(eventId, soundData, (scene != null) ? scene.Pointer : UIntPtr.Zero, is3d, isBlocking));
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x0000E26E File Offset: 0x0000C46E
		public static SoundEvent CreateEventFromExternalFile(string programmerEventName, string soundFilePath, Scene scene, bool is3d, bool isBlocking)
		{
			return new SoundEvent(EngineApplicationInterface.ISoundEvent.CreateEventFromExternalFile(programmerEventName, soundFilePath, (scene != null) ? scene.Pointer : UIntPtr.Zero, is3d, isBlocking));
		}

		// Token: 0x040001C5 RID: 453
		private const int NullSoundId = -1;

		// Token: 0x040001C6 RID: 454
		private static readonly SoundEvent NullSoundEvent = new SoundEvent(-1);

		// Token: 0x040001C7 RID: 455
		private int _soundId;
	}
}
