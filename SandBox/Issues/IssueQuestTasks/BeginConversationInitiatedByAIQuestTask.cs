using System;
using SandBox.Conversation;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace SandBox.Issues.IssueQuestTasks
{
	// Token: 0x020000BD RID: 189
	public class BeginConversationInitiatedByAIQuestTask : QuestTaskBase
	{
		// Token: 0x060007CA RID: 1994 RVA: 0x000349CE File Offset: 0x00032BCE
		public BeginConversationInitiatedByAIQuestTask(Agent agent, Action onSucceededAction, Action onFailedAction, Action onCanceledAction, DialogFlow dialogFlow = null)
			: base(dialogFlow, onSucceededAction, onFailedAction, onCanceledAction)
		{
			this._conversationAgent = agent;
			base.IsLogged = false;
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x000349EA File Offset: 0x00032BEA
		public void MissionTick(float dt)
		{
			if (Mission.Current.MainAgent == null || this._conversationAgent == null)
			{
				return;
			}
			if (!this._conversationOpened && Mission.Current.Mode != MissionMode.Conversation)
			{
				this.OpenConversation(this._conversationAgent);
			}
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00034A22 File Offset: 0x00032C22
		private void OpenConversation(Agent agent)
		{
			ConversationMission.StartConversationWithAgent(agent);
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00034A2A File Offset: 0x00032C2A
		protected override void OnFinished()
		{
			this._conversationAgent = null;
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00034A33 File Offset: 0x00032C33
		public override void SetReferences()
		{
			CampaignEvents.MissionTickEvent.AddNonSerializedListener(this, new Action<float>(this.MissionTick));
		}

		// Token: 0x04000427 RID: 1063
		private bool _conversationOpened;

		// Token: 0x04000428 RID: 1064
		private Agent _conversationAgent;
	}
}
