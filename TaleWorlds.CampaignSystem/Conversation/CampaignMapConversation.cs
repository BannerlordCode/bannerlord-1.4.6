using System;

namespace TaleWorlds.CampaignSystem.Conversation
{
	// Token: 0x02000230 RID: 560
	public static class CampaignMapConversation
	{
		// Token: 0x06002238 RID: 8760 RVA: 0x000974E2 File Offset: 0x000956E2
		public static void OpenConversation(ConversationCharacterData playerCharacterData, ConversationCharacterData conversationPartnerData)
		{
			Campaign.Current.ConversationManager.OpenMapConversation(playerCharacterData, conversationPartnerData);
		}
	}
}
