using System;
using TaleWorlds.CampaignSystem.LogEntries;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.CommentBehaviors
{
	// Token: 0x0200045C RID: 1116
	public class CommentOnClanLeaderChangedBehavior : CampaignBehaviorBase
	{
		// Token: 0x06004826 RID: 18470 RVA: 0x0016BAA8 File Offset: 0x00169CA8
		public override void RegisterEvents()
		{
			CampaignEvents.OnClanLeaderChangedEvent.AddNonSerializedListener(this, new Action<Hero, Hero>(CommentOnClanLeaderChangedBehavior.OnClanLeaderChanged));
		}

		// Token: 0x06004827 RID: 18471 RVA: 0x0016BAC1 File Offset: 0x00169CC1
		private static void OnClanLeaderChanged(Hero oldLeader, Hero newLeader)
		{
			LogEntry.AddLogEntry(new ClanLeaderChangedLogEntry(oldLeader, newLeader));
		}

		// Token: 0x06004828 RID: 18472 RVA: 0x0016BACF File Offset: 0x00169CCF
		public override void SyncData(IDataStore dataStore)
		{
		}
	}
}
