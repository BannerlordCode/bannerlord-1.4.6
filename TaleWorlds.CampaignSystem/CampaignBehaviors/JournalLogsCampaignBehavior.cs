using System;
using System.Linq;
using TaleWorlds.CampaignSystem.Issues;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x02000413 RID: 1043
	public class JournalLogsCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x06004186 RID: 16774 RVA: 0x001332C4 File Offset: 0x001314C4
		public override void RegisterEvents()
		{
			CampaignEvents.OnQuestStartedEvent.AddNonSerializedListener(this, new Action<QuestBase>(this.OnQuestStarted));
			CampaignEvents.OnQuestCompletedEvent.AddNonSerializedListener(this, new Action<QuestBase, QuestBase.QuestCompleteDetails>(this.OnQuestCompleted));
			CampaignEvents.OnIssueUpdatedEvent.AddNonSerializedListener(this, new Action<IssueBase, IssueBase.IssueUpdateDetails, Hero>(this.OnIssueUpdated));
			CampaignEvents.IssueLogAddedEvent.AddNonSerializedListener(this, new Action<IssueBase, bool>(this.OnIssueLogAdded));
			CampaignEvents.QuestLogAddedEvent.AddNonSerializedListener(this, new Action<QuestBase, bool>(this.OnQuestLogAdded));
		}

		// Token: 0x06004187 RID: 16775 RVA: 0x00133344 File Offset: 0x00131544
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06004188 RID: 16776 RVA: 0x00133348 File Offset: 0x00131548
		private void OnIssueLogAdded(IssueBase issue, bool hideInformation)
		{
			JournalLogEntry journalLogEntry = this.GetRelatedLog(issue);
			if (journalLogEntry == null)
			{
				journalLogEntry = this.CreateRelatedLog(issue);
				LogEntry.AddLogEntry(journalLogEntry);
			}
			journalLogEntry.Update(this.GetEntries(issue), IssueBase.IssueUpdateDetails.None);
		}

		// Token: 0x06004189 RID: 16777 RVA: 0x0013337C File Offset: 0x0013157C
		private void OnQuestLogAdded(QuestBase quest, bool hideInformation)
		{
			JournalLogEntry journalLogEntry = this.GetRelatedLog(quest);
			if (journalLogEntry == null)
			{
				journalLogEntry = this.CreateRelatedLog(quest);
				LogEntry.AddLogEntry(journalLogEntry);
			}
			journalLogEntry.Update(this.GetEntries(quest), IssueBase.IssueUpdateDetails.None);
		}

		// Token: 0x0600418A RID: 16778 RVA: 0x001333B0 File Offset: 0x001315B0
		private void OnQuestStarted(QuestBase quest)
		{
			JournalLogEntry journalLogEntry = this.GetRelatedLog(quest);
			if (journalLogEntry == null)
			{
				journalLogEntry = this.CreateRelatedLog(quest);
				LogEntry.AddLogEntry(journalLogEntry);
			}
			journalLogEntry.Update(this.GetEntries(quest), IssueBase.IssueUpdateDetails.None);
			LogEntry.AddLogEntry(new IssueQuestStartLogEntry(journalLogEntry.RelatedHero));
		}

		// Token: 0x0600418B RID: 16779 RVA: 0x001333F4 File Offset: 0x001315F4
		private void OnQuestCompleted(QuestBase quest, QuestBase.QuestCompleteDetails detail)
		{
			JournalLogEntry journalLogEntry = this.GetRelatedLog(quest);
			if (journalLogEntry == null)
			{
				journalLogEntry = this.CreateRelatedLog(quest);
				LogEntry.AddLogEntry(journalLogEntry);
			}
			journalLogEntry.Update(this.GetEntries(quest), detail);
			LogEntry.AddLogEntry(new IssueQuestLogEntry(journalLogEntry.RelatedHero, journalLogEntry.Antagonist, detail));
		}

		// Token: 0x0600418C RID: 16780 RVA: 0x00133440 File Offset: 0x00131640
		private void OnIssueUpdated(IssueBase issue, IssueBase.IssueUpdateDetails details, Hero issueSolver)
		{
			if (issueSolver == Hero.MainHero)
			{
				JournalLogEntry journalLogEntry = this.GetRelatedLog(issue);
				if (journalLogEntry == null)
				{
					journalLogEntry = this.CreateRelatedLog(issue);
					LogEntry.AddLogEntry(journalLogEntry);
				}
				journalLogEntry.Update(this.GetEntries(issue), details);
			}
		}

		// Token: 0x0600418D RID: 16781 RVA: 0x0013347C File Offset: 0x0013167C
		private JournalLogEntry CreateRelatedLog(IssueBase issue)
		{
			if (issue.IssueQuest != null)
			{
				return new JournalLogEntry(issue.IssueQuest.Title, issue.IssueQuest.QuestGiver, null, issue.IssueQuest.IsSpecialQuest, new MBObjectBase[] { issue, issue.IssueQuest });
			}
			return new JournalLogEntry(issue.Title, issue.IssueOwner, issue.CounterOfferHero, false, new MBObjectBase[] { issue });
		}

		// Token: 0x0600418E RID: 16782 RVA: 0x001334F0 File Offset: 0x001316F0
		private JournalLogEntry CreateRelatedLog(QuestBase quest)
		{
			IssueBase issueOfQuest = IssueManager.GetIssueOfQuest(quest);
			if (issueOfQuest != null)
			{
				return this.CreateRelatedLog(issueOfQuest);
			}
			return new JournalLogEntry(quest.Title, quest.QuestGiver, null, quest.IsSpecialQuest, new MBObjectBase[] { quest });
		}

		// Token: 0x0600418F RID: 16783 RVA: 0x00133534 File Offset: 0x00131734
		private JournalLogEntry GetRelatedLog(IssueBase issue)
		{
			return Campaign.Current.LogEntryHistory.FindLastGameActionLog<JournalLogEntry>((JournalLogEntry x) => x.IsRelatedTo(issue));
		}

		// Token: 0x06004190 RID: 16784 RVA: 0x0013356C File Offset: 0x0013176C
		private JournalLogEntry GetRelatedLog(QuestBase quest)
		{
			IssueBase issueOfQuest = IssueManager.GetIssueOfQuest(quest);
			if (issueOfQuest != null)
			{
				return this.GetRelatedLog(issueOfQuest);
			}
			return Campaign.Current.LogEntryHistory.FindLastGameActionLog<JournalLogEntry>((JournalLogEntry x) => x.IsRelatedTo(quest));
		}

		// Token: 0x06004191 RID: 16785 RVA: 0x001335B8 File Offset: 0x001317B8
		private MBReadOnlyList<JournalLog> GetEntries(IssueBase issue)
		{
			if (issue.IssueQuest == null)
			{
				return issue.JournalEntries;
			}
			MBList<JournalLog> mblist = issue.JournalEntries.ToMBList<JournalLog>();
			JournalLog journalLog = issue.IssueQuest.JournalEntries.FirstOrDefault<JournalLog>();
			if (journalLog != null)
			{
				int i;
				for (i = 0; i < mblist.Count; i++)
				{
					if (mblist[i].LogTime > journalLog.LogTime)
					{
						i--;
						break;
					}
				}
				mblist.InsertRange(i, issue.IssueQuest.JournalEntries);
			}
			return mblist;
		}

		// Token: 0x06004192 RID: 16786 RVA: 0x00133638 File Offset: 0x00131838
		private MBReadOnlyList<JournalLog> GetEntries(QuestBase quest)
		{
			IssueBase issueOfQuest = IssueManager.GetIssueOfQuest(quest);
			if (issueOfQuest != null)
			{
				return this.GetEntries(issueOfQuest);
			}
			return quest.JournalEntries;
		}
	}
}
