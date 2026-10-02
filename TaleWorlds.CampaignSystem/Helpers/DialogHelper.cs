using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Localization;

namespace Helpers
{
	// Token: 0x02000017 RID: 23
	public static class DialogHelper
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x0000C68F File Offset: 0x0000A88F
		public static void SetDialogString(string stringVariable, string gameTextId)
		{
			MBTextManager.SetTextVariable(stringVariable, Campaign.Current.ConversationManager.FindMatchingTextOrNull(gameTextId, CharacterObject.OneToOneConversationCharacter), false);
		}
	}
}
