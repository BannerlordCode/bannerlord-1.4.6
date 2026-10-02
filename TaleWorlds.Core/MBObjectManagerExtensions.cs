using System;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.Core
{
	// Token: 0x020000B1 RID: 177
	public static class MBObjectManagerExtensions
	{
		// Token: 0x06000951 RID: 2385 RVA: 0x0001E658 File Offset: 0x0001C858
		public static void LoadXML(this MBObjectManager objectManager, string id, bool skipXmlFilterForEditor = false)
		{
			Game game = Game.Current;
			bool flag = false;
			string text = "";
			if (game != null)
			{
				flag = game.GameType.IsDevelopment;
				text = game.GameType.GameTypeStringId;
			}
			objectManager.LoadXML(id, flag, text, skipXmlFilterForEditor);
		}
	}
}
