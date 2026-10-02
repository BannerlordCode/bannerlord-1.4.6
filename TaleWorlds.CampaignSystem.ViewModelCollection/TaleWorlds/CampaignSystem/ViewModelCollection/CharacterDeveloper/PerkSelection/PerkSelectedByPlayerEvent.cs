using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.CharacterDeveloper.PerkSelection
{
	// Token: 0x02000149 RID: 329
	public class PerkSelectedByPlayerEvent : EventBase
	{
		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06001F88 RID: 8072 RVA: 0x00073D68 File Offset: 0x00071F68
		// (set) Token: 0x06001F89 RID: 8073 RVA: 0x00073D70 File Offset: 0x00071F70
		public PerkObject SelectedPerk { get; private set; }

		// Token: 0x06001F8A RID: 8074 RVA: 0x00073D79 File Offset: 0x00071F79
		public PerkSelectedByPlayerEvent(PerkObject selectedPerk)
		{
			this.SelectedPerk = selectedPerk;
		}
	}
}
