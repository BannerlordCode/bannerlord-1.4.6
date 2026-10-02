using System;
using TaleWorlds.CampaignSystem.LogEntries;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.CommentBehaviors
{
	// Token: 0x0200045B RID: 1115
	public class CommentOnClanDestroyedBehavior : CampaignBehaviorBase
	{
		// Token: 0x06004822 RID: 18466 RVA: 0x0016BA78 File Offset: 0x00169C78
		public override void RegisterEvents()
		{
			CampaignEvents.OnClanDestroyedEvent.AddNonSerializedListener(this, new Action<Clan>(this.OnClanDestroyed));
		}

		// Token: 0x06004823 RID: 18467 RVA: 0x0016BA91 File Offset: 0x00169C91
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06004824 RID: 18468 RVA: 0x0016BA93 File Offset: 0x00169C93
		private void OnClanDestroyed(Clan destroyedClan)
		{
			LogEntry.AddLogEntry(new ClanDestroyedLogEntry(destroyedClan));
		}
	}
}
