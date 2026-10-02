using System;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.LogEntries;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.CommentBehaviors
{
	// Token: 0x02000462 RID: 1122
	public class CommentOnLeaveFactionBehavior : CampaignBehaviorBase
	{
		// Token: 0x0600483E RID: 18494 RVA: 0x0016BC7E File Offset: 0x00169E7E
		public override void RegisterEvents()
		{
			CampaignEvents.OnClanChangedKingdomEvent.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, ChangeKingdomAction.ChangeKingdomActionDetail, bool>(this.OnClanLeaveKingdom));
		}

		// Token: 0x0600483F RID: 18495 RVA: 0x0016BC97 File Offset: 0x00169E97
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06004840 RID: 18496 RVA: 0x0016BC99 File Offset: 0x00169E99
		private void OnClanLeaveKingdom(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail detail, bool showNotification)
		{
			LogEntry.AddLogEntry(new ClanChangeKingdomLogEntry(clan, oldKingdom, newKingdom, detail == ChangeKingdomAction.ChangeKingdomActionDetail.LeaveWithRebellion));
		}
	}
}
