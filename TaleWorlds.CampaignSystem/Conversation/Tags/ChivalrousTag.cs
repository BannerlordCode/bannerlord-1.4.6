using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x02000260 RID: 608
	public class ChivalrousTag : ConversationTag
	{
		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x0600235D RID: 9053 RVA: 0x0009B266 File Offset: 0x00099466
		public override string StringId
		{
			get
			{
				return "ChivalrousTag";
			}
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x0009B26D File Offset: 0x0009946D
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.GetTraitLevel(DefaultTraits.Honor) + character.GetTraitLevel(DefaultTraits.Valor) > 0;
		}

		// Token: 0x04000A96 RID: 2710
		public const string Id = "ChivalrousTag";
	}
}
