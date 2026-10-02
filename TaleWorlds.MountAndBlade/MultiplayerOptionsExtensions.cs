using System;
using System.Linq;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200030A RID: 778
	public static class MultiplayerOptionsExtensions
	{
		// Token: 0x06002C87 RID: 11399 RVA: 0x000AB6F8 File Offset: 0x000A98F8
		public static string GetValueText(this MultiplayerOptions.OptionType optionType, MultiplayerOptions.MultiplayerOptionsAccessMode mode = MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions)
		{
			switch (optionType.GetOptionProperty().OptionValueType)
			{
			case MultiplayerOptions.OptionValueType.Bool:
				return optionType.GetBoolValue(mode).ToString();
			case MultiplayerOptions.OptionValueType.Integer:
			case MultiplayerOptions.OptionValueType.Enum:
				return optionType.GetIntValue(mode).ToString();
			case MultiplayerOptions.OptionValueType.String:
				return optionType.GetStrValue(mode);
			default:
				Debug.FailedAssert("Missing OptionValueType for optionType: " + optionType, "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Network\\Gameplay\\MultiplayerOptions.cs", "GetValueText", 1014);
				return null;
			}
		}

		// Token: 0x06002C88 RID: 11400 RVA: 0x000AB778 File Offset: 0x000A9978
		public static bool GetBoolValue(this MultiplayerOptions.OptionType optionType, MultiplayerOptions.MultiplayerOptionsAccessMode mode = MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions)
		{
			int num;
			MultiplayerOptions.Instance.GetOptionFromOptionType(optionType, mode).GetValue(out num);
			return num == 1;
		}

		// Token: 0x06002C89 RID: 11401 RVA: 0x000AB79C File Offset: 0x000A999C
		public static int GetIntValue(this MultiplayerOptions.OptionType optionType, MultiplayerOptions.MultiplayerOptionsAccessMode mode = MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions)
		{
			int num;
			MultiplayerOptions.Instance.GetOptionFromOptionType(optionType, mode).GetValue(out num);
			return num;
		}

		// Token: 0x06002C8A RID: 11402 RVA: 0x000AB7C0 File Offset: 0x000A99C0
		public static string GetStrValue(this MultiplayerOptions.OptionType optionType, MultiplayerOptions.MultiplayerOptionsAccessMode mode = MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions)
		{
			string text;
			MultiplayerOptions.Instance.GetOptionFromOptionType(optionType, mode).GetValue(out text);
			return text;
		}

		// Token: 0x06002C8B RID: 11403 RVA: 0x000AB7E1 File Offset: 0x000A99E1
		public static void SetValue(this MultiplayerOptions.OptionType optionType, bool value, MultiplayerOptions.MultiplayerOptionsAccessMode mode = MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions)
		{
			MultiplayerOptions.Instance.GetOptionFromOptionType(optionType, mode).UpdateValue(value ? 1 : 0);
		}

		// Token: 0x06002C8C RID: 11404 RVA: 0x000AB7FC File Offset: 0x000A99FC
		public static void SetValue(this MultiplayerOptions.OptionType optionType, int value, MultiplayerOptions.MultiplayerOptionsAccessMode mode = MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions)
		{
			MultiplayerOptions.Instance.GetOptionFromOptionType(optionType, mode).UpdateValue(value);
		}

		// Token: 0x06002C8D RID: 11405 RVA: 0x000AB811 File Offset: 0x000A9A11
		public static void SetValue(this MultiplayerOptions.OptionType optionType, string value, MultiplayerOptions.MultiplayerOptionsAccessMode mode = MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions)
		{
			MultiplayerOptions.Instance.GetOptionFromOptionType(optionType, mode).UpdateValue(value);
		}

		// Token: 0x06002C8E RID: 11406 RVA: 0x000AB826 File Offset: 0x000A9A26
		public static int GetMinimumValue(this MultiplayerOptions.OptionType optionType)
		{
			return optionType.GetOptionProperty().BoundsMin;
		}

		// Token: 0x06002C8F RID: 11407 RVA: 0x000AB833 File Offset: 0x000A9A33
		public static int GetMaximumValue(this MultiplayerOptions.OptionType optionType)
		{
			return optionType.GetOptionProperty().BoundsMax;
		}

		// Token: 0x06002C90 RID: 11408 RVA: 0x000AB840 File Offset: 0x000A9A40
		public static MultiplayerOptionsProperty GetOptionProperty(this MultiplayerOptions.OptionType optionType)
		{
			return (MultiplayerOptionsProperty)optionType.GetType().GetField(optionType.ToString()).GetCustomAttributesSafe(typeof(MultiplayerOptionsProperty), false)
				.Single<object>();
		}
	}
}
