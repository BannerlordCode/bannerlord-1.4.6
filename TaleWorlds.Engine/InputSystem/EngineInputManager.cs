using System;
using TaleWorlds.Engine.Options;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace TaleWorlds.Engine.InputSystem
{
	// Token: 0x020000AF RID: 175
	public class EngineInputManager : IInputManager
	{
		// Token: 0x06000F8B RID: 3979 RVA: 0x00013610 File Offset: 0x00011810
		float IInputManager.GetMousePositionX()
		{
			return EngineApplicationInterface.IInput.GetMousePositionX();
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0001361C File Offset: 0x0001181C
		float IInputManager.GetMousePositionY()
		{
			return EngineApplicationInterface.IInput.GetMousePositionY();
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x00013628 File Offset: 0x00011828
		float IInputManager.GetMouseScrollValue()
		{
			return EngineApplicationInterface.IInput.GetMouseScrollValue();
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x00013634 File Offset: 0x00011834
		bool IInputManager.IsMouseActive()
		{
			return EngineApplicationInterface.IInput.IsMouseActive();
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x00013640 File Offset: 0x00011840
		bool IInputManager.IsControllerConnected()
		{
			return EngineApplicationInterface.IInput.IsControllerConnected();
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x0001364C File Offset: 0x0001184C
		void IInputManager.PressKey(InputKey key)
		{
			EngineApplicationInterface.IInput.PressKey(key);
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x00013659 File Offset: 0x00011859
		void IInputManager.ClearKeys()
		{
			EngineApplicationInterface.IInput.ClearKeys();
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x00013665 File Offset: 0x00011865
		int IInputManager.GetVirtualKeyCode(InputKey key)
		{
			return EngineApplicationInterface.IInput.GetVirtualKeyCode(key);
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x00013672 File Offset: 0x00011872
		void IInputManager.SetClipboardText(string text)
		{
			EngineApplicationInterface.IInput.SetClipboardText(text);
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x0001367F File Offset: 0x0001187F
		string IInputManager.GetClipboardText()
		{
			return EngineApplicationInterface.IInput.GetClipboardText();
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x0001368B File Offset: 0x0001188B
		float IInputManager.GetMouseMoveX()
		{
			return EngineApplicationInterface.IInput.GetMouseMoveX();
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x00013697 File Offset: 0x00011897
		float IInputManager.GetMouseMoveY()
		{
			return EngineApplicationInterface.IInput.GetMouseMoveY();
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x000136A3 File Offset: 0x000118A3
		float IInputManager.GetNormalizedMouseMoveX()
		{
			return EngineApplicationInterface.IInput.GetMouseMoveX() / Screen.RealScreenResolutionWidth;
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x000136B5 File Offset: 0x000118B5
		float IInputManager.GetNormalizedMouseMoveY()
		{
			return EngineApplicationInterface.IInput.GetMouseMoveY() / Screen.RealScreenResolutionHeight;
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x000136C7 File Offset: 0x000118C7
		float IInputManager.GetGyroX()
		{
			return EngineApplicationInterface.IInput.GetGyroX();
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x000136D3 File Offset: 0x000118D3
		float IInputManager.GetGyroY()
		{
			return EngineApplicationInterface.IInput.GetGyroY();
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x000136DF File Offset: 0x000118DF
		float IInputManager.GetGyroZ()
		{
			return EngineApplicationInterface.IInput.GetGyroZ();
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x000136EB File Offset: 0x000118EB
		float IInputManager.GetMouseSensitivity()
		{
			return EngineApplicationInterface.IInput.GetMouseSensitivity();
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x000136F7 File Offset: 0x000118F7
		float IInputManager.GetMouseDeltaZ()
		{
			return EngineApplicationInterface.IInput.GetMouseDeltaZ();
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x00013703 File Offset: 0x00011903
		void IInputManager.UpdateKeyData(byte[] keyData)
		{
			EngineApplicationInterface.IInput.UpdateKeyData(keyData);
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x00013710 File Offset: 0x00011910
		Vec2 IInputManager.GetKeyState(InputKey key)
		{
			return EngineApplicationInterface.IInput.GetKeyState(key);
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x0001371D File Offset: 0x0001191D
		bool IInputManager.IsKeyPressed(InputKey key)
		{
			return EngineApplicationInterface.IInput.IsKeyPressed(key);
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x0001372A File Offset: 0x0001192A
		bool IInputManager.IsKeyDown(InputKey key)
		{
			return EngineApplicationInterface.IInput.IsKeyDown(key);
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x00013737 File Offset: 0x00011937
		bool IInputManager.IsKeyDownImmediate(InputKey key)
		{
			return EngineApplicationInterface.IInput.IsKeyDownImmediate(key);
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x00013744 File Offset: 0x00011944
		bool IInputManager.IsKeyReleased(InputKey key)
		{
			return EngineApplicationInterface.IInput.IsKeyReleased(key);
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x00013751 File Offset: 0x00011951
		Vec2 IInputManager.GetResolution()
		{
			return Screen.RealScreenResolution;
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x00013758 File Offset: 0x00011958
		Vec2 IInputManager.GetDesktopResolution()
		{
			return Screen.DesktopResolution;
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x00013760 File Offset: 0x00011960
		void IInputManager.SetCursorPosition(int x, int y)
		{
			float num = 1f;
			float num2 = 1f;
			if (NativeOptions.GetConfig(NativeOptions.NativeOptionsType.DisplayMode) != 0f)
			{
				num = Input.DesktopResolution.X / Input.Resolution.X;
				num2 = Input.DesktopResolution.Y / Input.Resolution.Y;
			}
			EngineApplicationInterface.IInput.SetCursorPosition((int)((float)x * num), (int)((float)y * num2));
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x000137D5 File Offset: 0x000119D5
		void IInputManager.SetCursorFriction(float frictionValue)
		{
			EngineApplicationInterface.IInput.SetCursorFrictionValue(frictionValue);
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x000137E4 File Offset: 0x000119E4
		InputKey[] IInputManager.GetClickKeys()
		{
			InputKey inputKey = (EngineApplicationInterface.IScreen.IsEnterButtonCross() ? InputKey.ControllerRDown : InputKey.ControllerRRight);
			if (NativeOptions.GetConfig(NativeOptions.NativeOptionsType.EnableTouchpadMouse) != 0f)
			{
				return new InputKey[]
				{
					InputKey.LeftMouseButton,
					inputKey,
					InputKey.ControllerLOptionTap
				};
			}
			return new InputKey[]
			{
				InputKey.LeftMouseButton,
				inputKey
			};
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x00013845 File Offset: 0x00011A45
		public void SetRumbleEffect(float[] lowFrequencyLevels, float[] lowFrequencyDurations, int numLowFrequencyElements, float[] highFrequencyLevels, float[] highFrequencyDurations, int numHighFrequencyElements)
		{
			EngineApplicationInterface.IInput.SetRumbleEffect(lowFrequencyLevels, lowFrequencyDurations, numLowFrequencyElements, highFrequencyLevels, highFrequencyDurations, numHighFrequencyElements);
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x0001385A File Offset: 0x00011A5A
		public void SetTriggerFeedback(byte leftTriggerPosition, byte leftTriggerStrength, byte rightTriggerPosition, byte rightTriggerStrength)
		{
			EngineApplicationInterface.IInput.SetTriggerFeedback(leftTriggerPosition, leftTriggerStrength, rightTriggerPosition, rightTriggerStrength);
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0001386B File Offset: 0x00011A6B
		public void SetTriggerWeaponEffect(byte leftStartPosition, byte leftEnd_position, byte leftStrength, byte rightStartPosition, byte rightEndPosition, byte rightStrength)
		{
			EngineApplicationInterface.IInput.SetTriggerWeaponEffect(leftStartPosition, leftEnd_position, leftStrength, rightStartPosition, rightEndPosition, rightStrength);
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x00013880 File Offset: 0x00011A80
		public void SetTriggerVibration(float[] leftTriggerAmplitudes, float[] leftTriggerFrequencies, float[] leftTriggerDurations, int numLeftTriggerElements, float[] rightTriggerAmplitudes, float[] rightTriggerFrequencies, float[] rightTriggerDurations, int numRightTriggerElements)
		{
			EngineApplicationInterface.IInput.SetTriggerVibration(leftTriggerAmplitudes, leftTriggerFrequencies, leftTriggerDurations, numLeftTriggerElements, rightTriggerAmplitudes, rightTriggerFrequencies, rightTriggerDurations, numRightTriggerElements);
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x000138A4 File Offset: 0x00011AA4
		public void SetLightbarColor(float red, float green, float blue)
		{
			EngineApplicationInterface.IInput.SetLightbarColor(red, green, blue);
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x000138B3 File Offset: 0x00011AB3
		Input.ControllerTypes IInputManager.GetControllerType()
		{
			return (Input.ControllerTypes)EngineApplicationInterface.IInput.GetControllerType();
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x000138BF File Offset: 0x00011ABF
		bool IInputManager.IsAnyTouchActive()
		{
			return EngineApplicationInterface.IInput.IsAnyTouchActive();
		}
	}
}
