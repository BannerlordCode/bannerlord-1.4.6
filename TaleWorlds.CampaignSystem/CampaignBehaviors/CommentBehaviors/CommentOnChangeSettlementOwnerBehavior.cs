using System;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.CampaignSystem.MapNotificationTypes;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.CommentBehaviors
{
	// Token: 0x02000458 RID: 1112
	public class CommentOnChangeSettlementOwnerBehavior : CampaignBehaviorBase
	{
		// Token: 0x06004815 RID: 18453 RVA: 0x0016B80A File Offset: 0x00169A0A
		public override void RegisterEvents()
		{
			CampaignEvents.OnSettlementOwnerChangedEvent.AddNonSerializedListener(this, new Action<Settlement, bool, Hero, Hero, Hero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail>(this.OnSettlementOwnerChanged));
		}

		// Token: 0x06004816 RID: 18454 RVA: 0x0016B823 File Offset: 0x00169A23
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06004817 RID: 18455 RVA: 0x0016B828 File Offset: 0x00169A28
		private void OnSettlementOwnerChanged(Settlement settlement, bool openToClaim, Hero newOwner, Hero previousOwner, Hero capturerHero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail detail)
		{
			ChangeSettlementOwnerLogEntry changeSettlementOwnerLogEntry = new ChangeSettlementOwnerLogEntry(settlement, newOwner, previousOwner, false);
			LogEntry.AddLogEntry(changeSettlementOwnerLogEntry);
			if (newOwner != null && newOwner.IsHumanPlayerCharacter)
			{
				Campaign.Current.CampaignInformationManager.NewMapNoticeAdded(new SettlementOwnerChangedMapNotification(settlement, newOwner, previousOwner, changeSettlementOwnerLogEntry.GetEncyclopediaText()));
			}
		}
	}
}
