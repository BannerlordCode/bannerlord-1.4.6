using System;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.CharacterDeveloper.PerkSelection
{
	// Token: 0x0200014A RID: 330
	public class PerkSelectionToggleEvent : EventBase
	{
		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x06001F8B RID: 8075 RVA: 0x00073D88 File Offset: 0x00071F88
		// (set) Token: 0x06001F8C RID: 8076 RVA: 0x00073D90 File Offset: 0x00071F90
		public bool IsCurrentlyActive { get; private set; }

		// Token: 0x06001F8D RID: 8077 RVA: 0x00073D99 File Offset: 0x00071F99
		public PerkSelectionToggleEvent(bool isCurrentlyActive)
		{
			this.IsCurrentlyActive = isCurrentlyActive;
		}
	}
}
