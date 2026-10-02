using System;
using TaleWorlds.CampaignSystem.LogEntries;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.CommentBehaviors
{
	// Token: 0x02000461 RID: 1121
	public class CommentOnKingdomDestroyedBehavior : CampaignBehaviorBase
	{
		// Token: 0x0600483A RID: 18490 RVA: 0x0016BC4E File Offset: 0x00169E4E
		public override void RegisterEvents()
		{
			CampaignEvents.KingdomDestroyedEvent.AddNonSerializedListener(this, new Action<Kingdom>(this.OnKingdomDestroyed));
		}

		// Token: 0x0600483B RID: 18491 RVA: 0x0016BC67 File Offset: 0x00169E67
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x0600483C RID: 18492 RVA: 0x0016BC69 File Offset: 0x00169E69
		private void OnKingdomDestroyed(Kingdom destroyedKingdom)
		{
			LogEntry.AddLogEntry(new KingdomDestroyedLogEntry(destroyedKingdom));
		}
	}
}
