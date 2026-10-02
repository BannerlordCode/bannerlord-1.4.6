using System;
using TaleWorlds.Library.EventSystem;
using TaleWorlds.Localization;

namespace SandBox.View.Map.Navigation.NavigationElements
{
	// Token: 0x0200006B RID: 107
	public class ClanScreenPermissionEvent : EventBase
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x00024BF6 File Offset: 0x00022DF6
		// (set) Token: 0x0600049C RID: 1180 RVA: 0x00024BFE File Offset: 0x00022DFE
		public Action<bool, TextObject> IsClanScreenAvailable { get; private set; }

		// Token: 0x0600049D RID: 1181 RVA: 0x00024C07 File Offset: 0x00022E07
		public ClanScreenPermissionEvent(Action<bool, TextObject> isClanScreenAvailable)
		{
			this.IsClanScreenAvailable = isClanScreenAvailable;
		}
	}
}
