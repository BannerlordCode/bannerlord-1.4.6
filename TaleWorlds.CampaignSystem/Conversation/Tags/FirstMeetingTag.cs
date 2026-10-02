using System;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x0200026E RID: 622
	public class FirstMeetingTag : ConversationTag
	{
		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06002387 RID: 9095 RVA: 0x0009B676 File Offset: 0x00099876
		public override string StringId
		{
			get
			{
				return "FirstMeetingTag";
			}
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x0009B67D File Offset: 0x0009987D
		public override bool IsApplicableTo(CharacterObject character)
		{
			return Campaign.Current.ConversationManager.CurrentConversationIsFirst;
		}

		// Token: 0x04000AA5 RID: 2725
		public const string Id = "FirstMeetingTag";
	}
}
