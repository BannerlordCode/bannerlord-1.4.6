using System;

namespace TaleWorlds.CampaignSystem.Issues.IssueQuestTasks
{
	// Token: 0x02000384 RID: 900
	public class TalkToNpcQuestTask : QuestTaskBase
	{
		// Token: 0x0600349B RID: 13467 RVA: 0x000D907B File Offset: 0x000D727B
		public TalkToNpcQuestTask(Hero hero, Action onSucceededAction, DialogFlow dialogFlow = null)
			: base(dialogFlow, onSucceededAction, null, null)
		{
			this._character = hero.CharacterObject;
		}

		// Token: 0x0600349C RID: 13468 RVA: 0x000D9093 File Offset: 0x000D7293
		public TalkToNpcQuestTask(CharacterObject character, Action onSucceededAction, DialogFlow dialogFlow = null)
			: base(dialogFlow, onSucceededAction, null, null)
		{
			this._character = character;
		}

		// Token: 0x0600349D RID: 13469 RVA: 0x000D90A6 File Offset: 0x000D72A6
		public bool IsTaskCharacter()
		{
			return this._character == CharacterObject.OneToOneConversationCharacter;
		}

		// Token: 0x0600349E RID: 13470 RVA: 0x000D90B5 File Offset: 0x000D72B5
		protected override void OnFinished()
		{
			this._character = null;
		}

		// Token: 0x0600349F RID: 13471 RVA: 0x000D90BE File Offset: 0x000D72BE
		public override void SetReferences()
		{
		}

		// Token: 0x04000F0F RID: 3855
		private CharacterObject _character;
	}
}
