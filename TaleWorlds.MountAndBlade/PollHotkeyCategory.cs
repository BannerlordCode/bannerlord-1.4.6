using System;
using TaleWorlds.InputSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000235 RID: 565
	public sealed class PollHotkeyCategory : GameKeyContext
	{
		// Token: 0x060020E0 RID: 8416 RVA: 0x00074625 File Offset: 0x00072825
		public PollHotkeyCategory()
			: base("PollHotkeyCategory", 116, GameKeyContext.GameKeyContextType.Default)
		{
			this.RegisterGameKeys();
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x0007463C File Offset: 0x0007283C
		private void RegisterGameKeys()
		{
			base.RegisterGameKey(new GameKey(108, "AcceptPoll", "PollHotkeyCategory", InputKey.F10, InputKey.ControllerLBumper, GameKeyMainCategories.PollCategory), true);
			base.RegisterGameKey(new GameKey(109, "DeclinePoll", "PollHotkeyCategory", InputKey.F11, InputKey.ControllerRBumper, GameKeyMainCategories.PollCategory), true);
		}

		// Token: 0x04000C89 RID: 3209
		public const string CategoryId = "PollHotkeyCategory";

		// Token: 0x04000C8A RID: 3210
		public const int AcceptPoll = 108;

		// Token: 0x04000C8B RID: 3211
		public const int DeclinePoll = 109;
	}
}
