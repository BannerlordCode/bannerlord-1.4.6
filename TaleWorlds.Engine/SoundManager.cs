using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200008F RID: 143
	public static class SoundManager
	{
		// Token: 0x06000CBC RID: 3260 RVA: 0x0000E2C1 File Offset: 0x0000C4C1
		public static void SetListenerFrame(MatrixFrame frame)
		{
			EngineApplicationInterface.ISoundManager.SetListenerFrame(ref frame, ref frame.origin);
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x0000E2D6 File Offset: 0x0000C4D6
		public static void SetListenerFrame(MatrixFrame frame, Vec3 attenuationPosition)
		{
			EngineApplicationInterface.ISoundManager.SetListenerFrame(ref frame, ref attenuationPosition);
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x0000E2E8 File Offset: 0x0000C4E8
		public static MatrixFrame GetListenerFrame()
		{
			MatrixFrame matrixFrame;
			EngineApplicationInterface.ISoundManager.GetListenerFrame(out matrixFrame);
			return matrixFrame;
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0000E304 File Offset: 0x0000C504
		public static Vec3 GetAttenuationPosition()
		{
			Vec3 vec;
			EngineApplicationInterface.ISoundManager.GetAttenuationPosition(out vec);
			return vec;
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x0000E31E File Offset: 0x0000C51E
		public static void Reset()
		{
			EngineApplicationInterface.ISoundManager.Reset();
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x0000E32A File Offset: 0x0000C52A
		public static bool StartOneShotEvent(string eventFullName, in Vec3 position, string paramName, float paramValue)
		{
			return EngineApplicationInterface.ISoundManager.StartOneShotEventWithParam(eventFullName, position, paramName, paramValue);
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x0000E33F File Offset: 0x0000C53F
		public static bool StartOneShotEvent(string eventFullName, in Vec3 position)
		{
			return EngineApplicationInterface.ISoundManager.StartOneShotEvent(eventFullName, position);
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0000E352 File Offset: 0x0000C552
		public static bool StartOneShotEventWithIndex(int index, in Vec3 position)
		{
			return EngineApplicationInterface.ISoundManager.StartOneShotEventWithIndex(index, position);
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0000E365 File Offset: 0x0000C565
		public static void SetState(string stateGroup, string state)
		{
			EngineApplicationInterface.ISoundManager.SetState(stateGroup, state);
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x0000E373 File Offset: 0x0000C573
		public static SoundEvent CreateEvent(string eventFullName, Scene scene)
		{
			return SoundEvent.CreateEventFromString(eventFullName, scene);
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0000E37C File Offset: 0x0000C57C
		public static void LoadEventFileAux(string soundBank, bool decompressSamples)
		{
			if (!SoundManager._loaded)
			{
				EngineApplicationInterface.ISoundManager.LoadEventFileAux(soundBank, decompressSamples);
				SoundManager._loaded = true;
			}
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0000E397 File Offset: 0x0000C597
		public static void AddSoundClientWithId(ulong clientId)
		{
			EngineApplicationInterface.ISoundManager.AddSoundClientWithId(clientId);
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x0000E3A4 File Offset: 0x0000C5A4
		public static void DeleteSoundClientWithId(ulong clientId)
		{
			EngineApplicationInterface.ISoundManager.DeleteSoundClientWithId(clientId);
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0000E3B1 File Offset: 0x0000C5B1
		public static void SetGlobalParameter(string parameterName, float value)
		{
			EngineApplicationInterface.ISoundManager.SetGlobalParameter(parameterName, value);
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0000E3BF File Offset: 0x0000C5BF
		public static int GetEventGlobalIndex(string eventFullName)
		{
			if (string.IsNullOrEmpty(eventFullName))
			{
				return -1;
			}
			return EngineApplicationInterface.ISoundManager.GetGlobalIndexOfEvent(eventFullName);
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0000E3D6 File Offset: 0x0000C5D6
		public static void PauseBus(string busName)
		{
			EngineApplicationInterface.ISoundManager.PauseBus(busName);
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x0000E3E3 File Offset: 0x0000C5E3
		public static void UnpauseBus(string busName)
		{
			EngineApplicationInterface.ISoundManager.UnpauseBus(busName);
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x0000E3F0 File Offset: 0x0000C5F0
		public static void InitializeVoicePlayEvent()
		{
			EngineApplicationInterface.ISoundManager.InitializeVoicePlayEvent();
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x0000E3FC File Offset: 0x0000C5FC
		public static void CreateVoiceEvent()
		{
			EngineApplicationInterface.ISoundManager.CreateVoiceEvent();
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x0000E408 File Offset: 0x0000C608
		public static void DestroyVoiceEvent(int id)
		{
			EngineApplicationInterface.ISoundManager.DestroyVoiceEvent(id);
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x0000E415 File Offset: 0x0000C615
		public static void FinalizeVoicePlayEvent()
		{
			EngineApplicationInterface.ISoundManager.FinalizeVoicePlayEvent();
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x0000E421 File Offset: 0x0000C621
		public static void StartVoiceRecording()
		{
			EngineApplicationInterface.ISoundManager.StartVoiceRecord();
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x0000E42D File Offset: 0x0000C62D
		public static void StopVoiceRecording()
		{
			EngineApplicationInterface.ISoundManager.StopVoiceRecord();
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0000E439 File Offset: 0x0000C639
		public static void GetVoiceData(byte[] voiceBuffer, int chunkSize, out int readBytesLength)
		{
			readBytesLength = 0;
			EngineApplicationInterface.ISoundManager.GetVoiceData(voiceBuffer, chunkSize, ref readBytesLength);
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x0000E44B File Offset: 0x0000C64B
		public static void UpdateVoiceToPlay(byte[] voiceBuffer, int length, int index)
		{
			EngineApplicationInterface.ISoundManager.UpdateVoiceToPlay(voiceBuffer, length, index);
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x0000E45A File Offset: 0x0000C65A
		public static void AddXBOXRemoteUser(ulong XUID, ulong deviceID, bool canSendMicSound, bool canSendTextSound, bool canSendText, bool canReceiveSound, bool canReceiveText)
		{
			EngineApplicationInterface.ISoundManager.AddXBOXRemoteUser(XUID, deviceID, canSendMicSound, canSendTextSound, canSendText, canReceiveSound, canReceiveText);
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x0000E470 File Offset: 0x0000C670
		public static void InitializeXBOXSoundManager()
		{
			EngineApplicationInterface.ISoundManager.InitializeXBOXSoundManager();
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x0000E47C File Offset: 0x0000C67C
		public static void ApplyPushToTalk(bool pushed)
		{
			EngineApplicationInterface.ISoundManager.ApplyPushToTalk(pushed);
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0000E489 File Offset: 0x0000C689
		public static void ClearXBOXSoundManager()
		{
			EngineApplicationInterface.ISoundManager.ClearXBOXSoundManager();
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x0000E495 File Offset: 0x0000C695
		public static void UpdateXBOXLocalUser()
		{
			EngineApplicationInterface.ISoundManager.UpdateXBOXLocalUser();
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x0000E4A1 File Offset: 0x0000C6A1
		public static void UpdateXBOXChatCommunicationFlags(ulong XUID, bool canSendMicSound, bool canSendTextSound, bool canSendText, bool canReceiveSound, bool canReceiveText)
		{
			EngineApplicationInterface.ISoundManager.UpdateXBOXChatCommunicationFlags(XUID, canSendMicSound, canSendTextSound, canSendText, canReceiveSound, canReceiveText);
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x0000E4B5 File Offset: 0x0000C6B5
		public static void RemoveXBOXRemoteUser(ulong XUID)
		{
			EngineApplicationInterface.ISoundManager.RemoveXBOXRemoteUser(XUID);
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x0000E4C2 File Offset: 0x0000C6C2
		public static void ProcessDataToBeReceived(ulong senderDeviceID, byte[] data, uint dataSize)
		{
			EngineApplicationInterface.ISoundManager.ProcessDataToBeReceived(senderDeviceID, data, dataSize);
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x0000E4D1 File Offset: 0x0000C6D1
		public static void ProcessDataToBeSent(ref int numData)
		{
			EngineApplicationInterface.ISoundManager.ProcessDataToBeSent(ref numData);
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0000E4DE File Offset: 0x0000C6DE
		public static void HandleStateChanges()
		{
			EngineApplicationInterface.ISoundManager.HandleStateChanges();
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x0000E4EA File Offset: 0x0000C6EA
		public static void GetSizeOfDataToBeSentAt(int index, ref uint byteCount, ref uint numReceivers)
		{
			EngineApplicationInterface.ISoundManager.GetSizeOfDataToBeSentAt(index, ref byteCount, ref numReceivers);
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x0000E4F9 File Offset: 0x0000C6F9
		public static bool GetDataToBeSentAt(int index, byte[] buffer, ulong[] receivers, ref bool transportGuaranteed)
		{
			return EngineApplicationInterface.ISoundManager.GetDataToBeSentAt(index, buffer, receivers, ref transportGuaranteed);
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0000E509 File Offset: 0x0000C709
		public static void ClearDataToBeSent()
		{
			EngineApplicationInterface.ISoundManager.ClearDataToBeSent();
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0000E515 File Offset: 0x0000C715
		public static void CompressData(int clientID, byte[] buffer, int length, byte[] compressedBuffer, out int compressedBufferLength)
		{
			compressedBufferLength = 0;
			EngineApplicationInterface.ISoundManager.CompressData((ulong)((long)clientID), buffer, length, compressedBuffer, ref compressedBufferLength);
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x0000E52C File Offset: 0x0000C72C
		public static void DecompressData(int clientID, byte[] compressedBuffer, int compressedBufferLength, byte[] decompressedBuffer, out int decompressedBufferLength)
		{
			decompressedBufferLength = 0;
			EngineApplicationInterface.ISoundManager.DecompressData((ulong)((long)clientID), compressedBuffer, compressedBufferLength, decompressedBuffer, ref decompressedBufferLength);
		}

		// Token: 0x040001CA RID: 458
		private static bool _loaded;
	}
}
