using System;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200022B RID: 555
	public static class GameKeyTextExtensions
	{
		// Token: 0x060020B6 RID: 8374 RVA: 0x0007338D File Offset: 0x0007158D
		public static TextObject GetHotKeyGameText(this GameTextManager gameTextManager, string categoryName, string hotKeyId)
		{
			return gameTextManager.GetHotKeyGameTextFromKeyID(HotKeyManager.GetHotKeyId(categoryName, hotKeyId));
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x0007339C File Offset: 0x0007159C
		public static TextObject GetHotKeyGameText(this GameTextManager gameTextManager, string categoryName, int gameKeyId)
		{
			return gameTextManager.GetHotKeyGameTextFromKeyID(HotKeyManager.GetHotKeyId(categoryName, gameKeyId));
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x000733AB File Offset: 0x000715AB
		public static TextObject GetHotKeyGameTextFromKeyID(this GameTextManager gameTextManager, string keyId)
		{
			return gameTextManager.FindText("str_game_key_text", keyId.ToLower());
		}
	}
}
