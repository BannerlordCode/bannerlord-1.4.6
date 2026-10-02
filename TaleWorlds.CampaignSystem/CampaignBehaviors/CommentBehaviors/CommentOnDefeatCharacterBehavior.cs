using System;
using TaleWorlds.CampaignSystem.LogEntries;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.CommentBehaviors
{
	// Token: 0x0200045E RID: 1118
	public class CommentOnDefeatCharacterBehavior : CampaignBehaviorBase
	{
		// Token: 0x0600482E RID: 18478 RVA: 0x0016BB51 File Offset: 0x00169D51
		public override void RegisterEvents()
		{
			CampaignEvents.CharacterDefeated.AddNonSerializedListener(this, new Action<Hero, Hero>(this.OnCharacterDefeated));
		}

		// Token: 0x0600482F RID: 18479 RVA: 0x0016BB6A File Offset: 0x00169D6A
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06004830 RID: 18480 RVA: 0x0016BB6C File Offset: 0x00169D6C
		private void OnCharacterDefeated(Hero winner, Hero loser)
		{
			LogEntry.AddLogEntry(new DefeatCharacterLogEntry(winner, loser));
		}
	}
}
