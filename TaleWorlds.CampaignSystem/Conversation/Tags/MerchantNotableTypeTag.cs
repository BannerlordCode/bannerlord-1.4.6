using System;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x02000272 RID: 626
	public class MerchantNotableTypeTag : ConversationTag
	{
		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06002393 RID: 9107 RVA: 0x0009B705 File Offset: 0x00099905
		public override string StringId
		{
			get
			{
				return "MerchantNotableTypeTag";
			}
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x0009B70C File Offset: 0x0009990C
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.IsHero && character.Occupation == Occupation.Merchant;
		}

		// Token: 0x04000AA9 RID: 2729
		public const string Id = "MerchantNotableTypeTag";
	}
}
