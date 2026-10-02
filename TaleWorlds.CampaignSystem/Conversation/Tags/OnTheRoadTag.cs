using System;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x0200026B RID: 619
	public class OnTheRoadTag : ConversationTag
	{
		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x0600237E RID: 9086 RVA: 0x0009B589 File Offset: 0x00099789
		public override string StringId
		{
			get
			{
				return "OnTheRoadTag";
			}
		}

		// Token: 0x0600237F RID: 9087 RVA: 0x0009B590 File Offset: 0x00099790
		public override bool IsApplicableTo(CharacterObject character)
		{
			return Settlement.CurrentSettlement == null;
		}

		// Token: 0x04000AA2 RID: 2722
		public const string Id = "OnTheRoadTag";
	}
}
