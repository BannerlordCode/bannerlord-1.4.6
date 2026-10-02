using System;
using TaleWorlds.Core;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.CharacterDeveloper
{
	// Token: 0x02000146 RID: 326
	public class FocusAddedByPlayerEvent : EventBase
	{
		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06001F6B RID: 8043 RVA: 0x00073886 File Offset: 0x00071A86
		// (set) Token: 0x06001F6C RID: 8044 RVA: 0x0007388E File Offset: 0x00071A8E
		public Hero AddedPlayer { get; private set; }

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06001F6D RID: 8045 RVA: 0x00073897 File Offset: 0x00071A97
		// (set) Token: 0x06001F6E RID: 8046 RVA: 0x0007389F File Offset: 0x00071A9F
		public SkillObject AddedSkill { get; private set; }

		// Token: 0x06001F6F RID: 8047 RVA: 0x000738A8 File Offset: 0x00071AA8
		public FocusAddedByPlayerEvent(Hero addedPlayer, SkillObject addedSkill)
		{
			this.AddedPlayer = addedPlayer;
			this.AddedSkill = addedSkill;
		}
	}
}
