using System;
using TaleWorlds.InputSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000231 RID: 561
	public sealed class MapNotificationHotKeyCategory : GameKeyContext
	{
		// Token: 0x060020D2 RID: 8402 RVA: 0x00073F27 File Offset: 0x00072127
		public MapNotificationHotKeyCategory()
			: base("MapNotificationHotKeyCategory", 116, GameKeyContext.GameKeyContextType.Default)
		{
			this.RegisterHotKeys();
		}

		// Token: 0x060020D3 RID: 8403 RVA: 0x00073F3D File Offset: 0x0007213D
		private void RegisterHotKeys()
		{
			base.RegisterHotKey(new HotKey("RemoveNotification", "MapNotificationHotKeyCategory", InputKey.ControllerRUp, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
		}

		// Token: 0x04000C4C RID: 3148
		public const string CategoryId = "MapNotificationHotKeyCategory";

		// Token: 0x04000C4D RID: 3149
		public const string RemoveNotification = "RemoveNotification";
	}
}
