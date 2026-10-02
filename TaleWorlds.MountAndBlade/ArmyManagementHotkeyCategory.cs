using System;
using TaleWorlds.InputSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000222 RID: 546
	public sealed class ArmyManagementHotkeyCategory : GameKeyContext
	{
		// Token: 0x0600209B RID: 8347 RVA: 0x000726B7 File Offset: 0x000708B7
		public ArmyManagementHotkeyCategory()
			: base("ArmyManagementHotkeyCategory", 116, GameKeyContext.GameKeyContextType.Default)
		{
			this.RegisterHotKeys();
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x000726CD File Offset: 0x000708CD
		private void RegisterHotKeys()
		{
			base.RegisterHotKey(new HotKey("RemoveParty", "ArmyManagementHotkeyCategory", InputKey.ControllerRBumper, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
		}

		// Token: 0x04000B34 RID: 2868
		public const string CategoryId = "ArmyManagementHotkeyCategory";

		// Token: 0x04000B35 RID: 2869
		public const string RemoveParty = "RemoveParty";
	}
}
