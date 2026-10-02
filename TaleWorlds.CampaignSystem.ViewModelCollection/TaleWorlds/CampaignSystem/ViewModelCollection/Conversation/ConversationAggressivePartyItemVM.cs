using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.ViewModelCollection.Quests;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.ImageIdentifiers;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Conversation
{
	// Token: 0x02000116 RID: 278
	public class ConversationAggressivePartyItemVM : ViewModel
	{
		// Token: 0x06001975 RID: 6517 RVA: 0x00060D00 File Offset: 0x0005EF00
		public ConversationAggressivePartyItemVM(MobileParty party, CharacterObject leader = null)
		{
			this.Party = party;
			if (leader != null)
			{
				this.LeaderVisual = new CharacterImageIdentifierVM(CampaignUIHelper.GetCharacterCode(leader, false));
			}
			else if (party != null)
			{
				CharacterObject visualPartyLeader = CampaignUIHelper.GetVisualPartyLeader(party.Party);
				if (visualPartyLeader != null)
				{
					this.LeaderVisual = new CharacterImageIdentifierVM(CampaignUIHelper.GetCharacterCode(visualPartyLeader, false));
				}
			}
			this.HealthyAmount = ((party != null) ? party.Party.NumberOfHealthyMembers : 0);
			this.RefreshQuests();
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x00060D74 File Offset: 0x0005EF74
		private void RefreshQuests()
		{
			this.Quests = new MBBindingList<QuestMarkerVM>();
			if (this.Party != null)
			{
				List<QuestBase> questsRelatedToParty = CampaignUIHelper.GetQuestsRelatedToParty(this.Party);
				CampaignUIHelper.IssueQuestFlags issueQuestFlags = CampaignUIHelper.IssueQuestFlags.None;
				for (int i = 0; i < questsRelatedToParty.Count; i++)
				{
					issueQuestFlags |= CampaignUIHelper.GetQuestType(questsRelatedToParty[i], this.Party.LeaderHero);
				}
				Hero leaderHero = this.Party.LeaderHero;
				if (((leaderHero != null) ? leaderHero.Issue : null) != null)
				{
					issueQuestFlags |= CampaignUIHelper.GetIssueType(this.Party.LeaderHero.Issue);
				}
				if ((issueQuestFlags & CampaignUIHelper.IssueQuestFlags.TrackedIssue) != CampaignUIHelper.IssueQuestFlags.None)
				{
					this.Quests.Add(new QuestMarkerVM(CampaignUIHelper.IssueQuestFlags.TrackedIssue, null, null));
				}
				else if ((issueQuestFlags & CampaignUIHelper.IssueQuestFlags.ActiveIssue) != CampaignUIHelper.IssueQuestFlags.None)
				{
					this.Quests.Add(new QuestMarkerVM(CampaignUIHelper.IssueQuestFlags.ActiveIssue, null, null));
				}
				else if ((issueQuestFlags & CampaignUIHelper.IssueQuestFlags.AvailableIssue) != CampaignUIHelper.IssueQuestFlags.None)
				{
					this.Quests.Add(new QuestMarkerVM(CampaignUIHelper.IssueQuestFlags.AvailableIssue, null, null));
				}
				if ((issueQuestFlags & CampaignUIHelper.IssueQuestFlags.TrackedStoryQuest) != CampaignUIHelper.IssueQuestFlags.None)
				{
					this.Quests.Add(new QuestMarkerVM(CampaignUIHelper.IssueQuestFlags.TrackedStoryQuest, null, null));
					return;
				}
				if ((issueQuestFlags & CampaignUIHelper.IssueQuestFlags.ActiveStoryQuest) != CampaignUIHelper.IssueQuestFlags.None)
				{
					this.Quests.Add(new QuestMarkerVM(CampaignUIHelper.IssueQuestFlags.ActiveStoryQuest, null, null));
				}
			}
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x00060E80 File Offset: 0x0005F080
		public void ExecuteShowPartyTooltip()
		{
			if (this.Party != null)
			{
				InformationManager.ShowTooltip(typeof(MobileParty), new object[] { this.Party, true, true });
			}
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x00060EBA File Offset: 0x0005F0BA
		public void ExecuteHideTooltip()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06001979 RID: 6521 RVA: 0x00060EC1 File Offset: 0x0005F0C1
		// (set) Token: 0x0600197A RID: 6522 RVA: 0x00060EC9 File Offset: 0x0005F0C9
		[DataSourceProperty]
		public CharacterImageIdentifierVM LeaderVisual
		{
			get
			{
				return this._leaderVisual;
			}
			set
			{
				if (value != this._leaderVisual)
				{
					this._leaderVisual = value;
					base.OnPropertyChangedWithValue<CharacterImageIdentifierVM>(value, "LeaderVisual");
				}
			}
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x0600197B RID: 6523 RVA: 0x00060EE7 File Offset: 0x0005F0E7
		// (set) Token: 0x0600197C RID: 6524 RVA: 0x00060EEF File Offset: 0x0005F0EF
		[DataSourceProperty]
		public int HealthyAmount
		{
			get
			{
				return this._healthyAmount;
			}
			set
			{
				if (value != this._healthyAmount)
				{
					this._healthyAmount = value;
					base.OnPropertyChangedWithValue(value, "HealthyAmount");
				}
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x0600197D RID: 6525 RVA: 0x00060F0D File Offset: 0x0005F10D
		// (set) Token: 0x0600197E RID: 6526 RVA: 0x00060F15 File Offset: 0x0005F115
		[DataSourceProperty]
		public MBBindingList<QuestMarkerVM> Quests
		{
			get
			{
				return this._quests;
			}
			set
			{
				if (value != this._quests)
				{
					this._quests = value;
					base.OnPropertyChangedWithValue<MBBindingList<QuestMarkerVM>>(value, "Quests");
				}
			}
		}

		// Token: 0x04000BB3 RID: 2995
		public readonly MobileParty Party;

		// Token: 0x04000BB4 RID: 2996
		private MBBindingList<QuestMarkerVM> _quests;

		// Token: 0x04000BB5 RID: 2997
		private CharacterImageIdentifierVM _leaderVisual;

		// Token: 0x04000BB6 RID: 2998
		private int _healthyAmount;
	}
}
