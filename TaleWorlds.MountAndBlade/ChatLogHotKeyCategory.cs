using System;
using System.Collections.Generic;
using TaleWorlds.InputSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000224 RID: 548
	public sealed class ChatLogHotKeyCategory : GameKeyContext
	{
		// Token: 0x060020A1 RID: 8353 RVA: 0x0007281A File Offset: 0x00070A1A
		public ChatLogHotKeyCategory()
			: base("ChatLogHotKeyCategory", 116, GameKeyContext.GameKeyContextType.Default)
		{
			this.RegisterHotKeys();
			this.RegisterGameKeys();
			this.RegisterGameAxisKeys();
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x0007283C File Offset: 0x00070A3C
		private void RegisterHotKeys()
		{
			List<Key> list = new List<Key>
			{
				new Key(InputKey.Tab),
				new Key(InputKey.ControllerRUp)
			};
			List<Key> list2 = new List<Key>
			{
				new Key(InputKey.NumpadEnter)
			};
			list2.Add(new Key(InputKey.ControllerLOption));
			List<Key> list3 = new List<Key>
			{
				new Key(InputKey.ControllerRLeft)
			};
			base.RegisterHotKey(new HotKey("CycleChatTypes", "ChatLogHotKeyCategory", list, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("FinalizeChatAlternative", "ChatLogHotKeyCategory", list2, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("SendMessage", "ChatLogHotKeyCategory", list3, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x000728F4 File Offset: 0x00070AF4
		private void RegisterGameKeys()
		{
			base.RegisterGameKey(new GameKey(6, "InitiateAllChat", "ChatLogHotKeyCategory", InputKey.T, GameKeyMainCategories.ChatCategory), true);
			base.RegisterGameKey(new GameKey(7, "InitiateTeamChat", "ChatLogHotKeyCategory", InputKey.Y, GameKeyMainCategories.ChatCategory), true);
			base.RegisterGameKey(new GameKey(8, "FinalizeChat", "ChatLogHotKeyCategory", InputKey.Enter, InputKey.ControllerLOption, GameKeyMainCategories.ChatCategory), true);
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x00072960 File Offset: 0x00070B60
		private void RegisterGameAxisKeys()
		{
		}

		// Token: 0x04000B3B RID: 2875
		public const string CategoryId = "ChatLogHotKeyCategory";

		// Token: 0x04000B3C RID: 2876
		public const int InitiateAllChat = 6;

		// Token: 0x04000B3D RID: 2877
		public const int InitiateTeamChat = 7;

		// Token: 0x04000B3E RID: 2878
		public const int FinalizeChat = 8;

		// Token: 0x04000B3F RID: 2879
		public const string CycleChatTypes = "CycleChatTypes";

		// Token: 0x04000B40 RID: 2880
		public const string FinalizeChatAlternative = "FinalizeChatAlternative";

		// Token: 0x04000B41 RID: 2881
		public const string SendMessage = "SendMessage";
	}
}
