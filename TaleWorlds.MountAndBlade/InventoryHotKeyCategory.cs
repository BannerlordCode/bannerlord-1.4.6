using System;
using TaleWorlds.InputSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200022F RID: 559
	public sealed class InventoryHotKeyCategory : GameKeyContext
	{
		// Token: 0x060020CA RID: 8394 RVA: 0x00073AA9 File Offset: 0x00071CA9
		public InventoryHotKeyCategory()
			: base("InventoryHotKeyCategory", 116, GameKeyContext.GameKeyContextType.Default)
		{
			this.RegisterHotKeys();
			this.RegisterGameKeys();
			this.RegisterGameAxisKeys();
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x00073ACB File Offset: 0x00071CCB
		private void RegisterHotKeys()
		{
			base.RegisterHotKey(new HotKey("SwitchAlternative", "InventoryHotKeyCategory", InputKey.LeftAlt, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x00073AE7 File Offset: 0x00071CE7
		private void RegisterGameKeys()
		{
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x00073AE9 File Offset: 0x00071CE9
		private void RegisterGameAxisKeys()
		{
		}

		// Token: 0x04000C2D RID: 3117
		public const string CategoryId = "InventoryHotKeyCategory";

		// Token: 0x04000C2E RID: 3118
		public const string SwitchAlternative = "SwitchAlternative";
	}
}
