using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;

namespace TaleWorlds.CampaignSystem.Conversation.Tags
{
	// Token: 0x0200025F RID: 607
	public class AmoralTag : ConversationTag
	{
		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x0600235A RID: 9050 RVA: 0x0009B23B File Offset: 0x0009943B
		public override string StringId
		{
			get
			{
				return "AmoralTag";
			}
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x0009B242 File Offset: 0x00099442
		public override bool IsApplicableTo(CharacterObject character)
		{
			return character.GetTraitLevel(DefaultTraits.Honor) + character.GetTraitLevel(DefaultTraits.Mercy) < 0;
		}

		// Token: 0x04000A95 RID: 2709
		public const string Id = "AmoralTag";
	}
}
