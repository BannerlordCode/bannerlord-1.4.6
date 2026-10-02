using System;
using System.Collections.Generic;
using TaleWorlds.InputSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000226 RID: 550
	public sealed class ConversationHotKeyCategory : GameKeyContext
	{
		// Token: 0x060020A9 RID: 8361 RVA: 0x00072F5B File Offset: 0x0007115B
		public ConversationHotKeyCategory()
			: base("ConversationHotKeyCategory", 116, GameKeyContext.GameKeyContextType.Default)
		{
			this.RegisterHotKeys();
			this.RegisterGameKeys();
			this.RegisterGameAxisKeys();
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x00072F80 File Offset: 0x00071180
		private void RegisterHotKeys()
		{
			List<Key> list = new List<Key>
			{
				new Key(InputKey.Space),
				new Key(InputKey.Enter),
				new Key(InputKey.NumpadEnter)
			};
			base.RegisterHotKey(new HotKey("ContinueKey", "ConversationHotKeyCategory", list, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			List<Key> list2 = new List<Key>
			{
				new Key(InputKey.LeftMouseButton),
				new Key(InputKey.ControllerRDown)
			};
			base.RegisterHotKey(new HotKey("ContinueClick", "ConversationHotKeyCategory", list2, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x00073015 File Offset: 0x00071215
		private void RegisterGameKeys()
		{
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x00073017 File Offset: 0x00071217
		private void RegisterGameAxisKeys()
		{
		}

		// Token: 0x04000B6E RID: 2926
		public const string CategoryId = "ConversationHotKeyCategory";

		// Token: 0x04000B6F RID: 2927
		public const string ContinueKey = "ContinueKey";

		// Token: 0x04000B70 RID: 2928
		public const string ContinueClick = "ContinueClick";
	}
}
