using System;
using TaleWorlds.CampaignSystem.LogEntries;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.CommentBehaviors
{
	// Token: 0x02000465 RID: 1125
	public class CommentPregnancyBehavior : CampaignBehaviorBase
	{
		// Token: 0x0600484A RID: 18506 RVA: 0x0016BD77 File Offset: 0x00169F77
		public override void RegisterEvents()
		{
			CampaignEvents.OnChildConceivedEvent.AddNonSerializedListener(this, new Action<Hero>(this.OnChildConceived));
		}

		// Token: 0x0600484B RID: 18507 RVA: 0x0016BD90 File Offset: 0x00169F90
		private void OnChildConceived(Hero mother)
		{
			LogEntry.AddLogEntry(new PregnancyLogEntry(mother));
		}

		// Token: 0x0600484C RID: 18508 RVA: 0x0016BD9D File Offset: 0x00169F9D
		public override void SyncData(IDataStore dataStore)
		{
		}
	}
}
