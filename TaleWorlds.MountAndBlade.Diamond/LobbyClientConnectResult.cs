using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Diamond;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000123 RID: 291
	public class LobbyClientConnectResult
	{
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x0000B4F4 File Offset: 0x000096F4
		// (set) Token: 0x0600077F RID: 1919 RVA: 0x0000B4FC File Offset: 0x000096FC
		public bool Connected { get; private set; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x0000B505 File Offset: 0x00009705
		// (set) Token: 0x06000781 RID: 1921 RVA: 0x0000B50D File Offset: 0x0000970D
		public TextObject Error { get; private set; }

		// Token: 0x06000782 RID: 1922 RVA: 0x0000B516 File Offset: 0x00009716
		public LobbyClientConnectResult(bool connected, TextObject error)
		{
			this.Connected = connected;
			this.Error = error;
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0000B52C File Offset: 0x0000972C
		public static LobbyClientConnectResult FromServerConnectResult(string errorCode, Dictionary<string, string> parameters)
		{
			TextObject textObject = GameTexts.FindText("str_login_error", errorCode);
			if (textObject == null)
			{
				Debug.FailedAssert("Error text is not handled: " + errorCode, "C:\\BuildAgent\\work\\mb3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\LobbyClient.cs", "FromServerConnectResult", 2216);
				textObject = new TextObject("{=tzQxtv27}Unknown error.", null);
			}
			else if (parameters != null)
			{
				foreach (string text in parameters.Keys)
				{
					if (text == "BANREASON")
					{
						if (parameters[text].StartsWith("Custom:"))
						{
							textObject.SetTextVariable(text, parameters[text].Substring("Custom:".Length));
						}
						else
						{
							TextObject textObject2 = GameTexts.FindText("str_ban_reason", parameters[text]);
							textObject.SetTextVariable(text, textObject2.ToString());
						}
					}
					else if (text == "ACCESSERROR")
					{
						TextObject textObject3 = GameTexts.FindText("str_access_error", parameters[text]);
						textObject.SetTextVariable(text, textObject3.ToString());
					}
					else
					{
						textObject.SetTextVariable(text, parameters[text]);
					}
				}
			}
			return new LobbyClientConnectResult(errorCode == LoginErrorCode.None.ToString(), textObject);
		}
	}
}
