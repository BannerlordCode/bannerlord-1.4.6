using System;
using System.Collections.Generic;
using TaleWorlds.InputSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000234 RID: 564
	public sealed class PartyHotKeyCategory : GameKeyContext
	{
		// Token: 0x060020DC RID: 8412 RVA: 0x0007443E File Offset: 0x0007263E
		public PartyHotKeyCategory()
			: base("PartyHotKeyCategory", 116, GameKeyContext.GameKeyContextType.Default)
		{
			this.RegisterHotKeys();
			this.RegisterGameKeys();
			this.RegisterGameAxisKeys();
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x00074460 File Offset: 0x00072660
		private void RegisterHotKeys()
		{
			List<Key> list = new List<Key>
			{
				new Key(InputKey.Q),
				new Key(InputKey.ControllerLTrigger)
			};
			List<Key> list2 = new List<Key>
			{
				new Key(InputKey.E),
				new Key(InputKey.ControllerRTrigger)
			};
			List<Key> list3 = new List<Key>
			{
				new Key(InputKey.A),
				new Key(InputKey.ControllerLBumper)
			};
			List<Key> list4 = new List<Key>
			{
				new Key(InputKey.D),
				new Key(InputKey.ControllerRBumper)
			};
			List<Key> list5 = new List<Key>
			{
				new Key(InputKey.ControllerLBumper)
			};
			List<Key> list6 = new List<Key>
			{
				new Key(InputKey.ControllerRBumper)
			};
			List<Key> list7 = new List<Key>
			{
				new Key(InputKey.ControllerLThumb)
			};
			List<Key> list8 = new List<Key>
			{
				new Key(InputKey.ControllerRThumb)
			};
			base.RegisterHotKey(new HotKey("TakeAllTroops", "PartyHotKeyCategory", list, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("GiveAllTroops", "PartyHotKeyCategory", list2, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("TakeAllPrisoners", "PartyHotKeyCategory", list3, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("GiveAllPrisoners", "PartyHotKeyCategory", list4, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("OpenUpgradePopup", "PartyHotKeyCategory", list7, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("OpenRecruitPopup", "PartyHotKeyCategory", list8, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("PopupItemPrimaryAction", "PartyHotKeyCategory", list5, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
			base.RegisterHotKey(new HotKey("PopupItemSecondaryAction", "PartyHotKeyCategory", list6, HotKey.Modifiers.None, HotKey.Modifiers.None), true);
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x00074621 File Offset: 0x00072821
		private void RegisterGameKeys()
		{
		}

		// Token: 0x060020DF RID: 8415 RVA: 0x00074623 File Offset: 0x00072823
		private void RegisterGameAxisKeys()
		{
		}

		// Token: 0x04000C80 RID: 3200
		public const string CategoryId = "PartyHotKeyCategory";

		// Token: 0x04000C81 RID: 3201
		public const string TakeAllTroops = "TakeAllTroops";

		// Token: 0x04000C82 RID: 3202
		public const string GiveAllTroops = "GiveAllTroops";

		// Token: 0x04000C83 RID: 3203
		public const string TakeAllPrisoners = "TakeAllPrisoners";

		// Token: 0x04000C84 RID: 3204
		public const string GiveAllPrisoners = "GiveAllPrisoners";

		// Token: 0x04000C85 RID: 3205
		public const string PopupItemPrimaryAction = "PopupItemPrimaryAction";

		// Token: 0x04000C86 RID: 3206
		public const string PopupItemSecondaryAction = "PopupItemSecondaryAction";

		// Token: 0x04000C87 RID: 3207
		public const string OpenUpgradePopup = "OpenUpgradePopup";

		// Token: 0x04000C88 RID: 3208
		public const string OpenRecruitPopup = "OpenRecruitPopup";
	}
}
